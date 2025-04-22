// $Id: BunruiHoshuLogic.cs 18796 $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using BookmarkHoshu.APP;
using BookmarkHoshu.Const;
using BookmarkHoshu.Dto;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Menu;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace BookmarkHoshu.Logic
{
    /// <summary>
    /// マイメニュー選択保守画面のビジネスロジック
    /// </summary>
    public class BookmarkHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        /// <summary>
        /// ボタン設定XMLパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "BookmarkHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// お気に入れの一覧取得用SQL
        /// </summary>
        private readonly string GET_ICHIRAN_DATA_SQL = "BookmarkHoshu.Sql.GetIchiranDataSql.sql";

        /// <summary>
        /// 社員取得用SQL
        /// </summary>
        private readonly string GET_SHAIN_DATA_SQL = "BookmarkHoshu.Sql.GetShainDataSql.sql";

        /// <summary>
        /// マイメニュー選択保守画面Form
        /// </summary>
        private BookmarkHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// お気に入れのDao
        /// </summary>
        private IM_MY_FAVORITEDao daoFavorite;

        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao daoShain;

        /// <summary>
        /// 部署Dao
        /// </summary>
        private IM_BUSHODao daoBusho;

        /// <summary>
        /// 検索条件（お気に入れ）
        /// </summary>
        private M_MY_FAVORITE entMyFavorite_ForSearch;

        /// <summary>
        /// 検索条件（社員）
        /// </summary>
        private M_SHAIN entShain_ForSearch;

        /// <summary>
        /// マイメニュー選択保守画面のDTO
        /// </summary>
        private BookmarkHoshuDto dto;

        /// <summary>
        /// メニューアイテムリスト
        /// リボンフォームより取得
        /// </summary>
        private List<MenuItemComm> menuItems;

        /// <summary>
        /// ポップアップ用メニューテーブル
        /// </summary>
        private DataTable menuTable;

        /// <summary>
        /// 行番号
        /// </summary>
        private int rowNo;

        /// <summary>
        /// メニューへ表示させないフォームIDリスト
        /// </summary>
        /// <remarks>予備</remarks>
        private readonly List<string> NotDispFormIdList = new List<string>() { };

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        #endregion

        #region - Constructor -

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public BookmarkHoshuLogic(BookmarkHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            // フォーム
            this.form = targetForm;

            // DTO
            this.dto = new BookmarkHoshuDto();

            // DAO
            this.daoFavorite = DaoInitUtility.GetComponent<IM_MY_FAVORITEDao>();
            this.daoBusho = DaoInitUtility.GetComponent<IM_BUSHODao>();
            this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

                // 全コントロール格納
                this.allControl = this.form.allControl;

                // コントロールを初期化
                this.ControlInit();

                // メニューアイテムクラス取得
                this.GetMenuItems();

                // メニューテーブル作成
                this.CreateMenuTable();

                // 前回設定値取得
                //this.GetPrevData();

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
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 検索条件の設定
                this.SetSearchString();

                // マイメニューデータ検索
                var tblBookMark = this.SearchBookMark();

                // 社員データ検索
                var tblShain = this.daoShain.GetShainDataSqlFile(this.GET_SHAIN_DATA_SQL, this.entShain_ForSearch);

                // マイメニューデータテーブル用初期化処理
                BookmarkHoshuLogic.Init_DataTable(tblBookMark);

                // マイメニューデータテーブル設定
                this.SetDataTable(tblBookMark);

                // 検索結果設定
                this.SearchResult = tblBookMark;

                // 検索条件を保存
                //this.SetPrevData();

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
        /// 検索チェック
        /// </summary>
        /// <param name="bDispMessage"></param>
        /// <returns></returns>
        public bool SearchCheck(bool bDispMessage)
        {
            try
            {
                var result = true;
                var msgItem = new StringBuilder();
                Control ctrlFocus = null;

                // 部署と社員が入力されていれば検索OK
                // 部署、社員
                if (string.IsNullOrWhiteSpace(this.form.BUSHO_CD.Text) &&
                    string.IsNullOrWhiteSpace(this.form.SHAIN_CD.Text))
                {
                    msgItem.Append("部署、社員");
                    ctrlFocus = this.form.BUSHO_CD;
                }
                // 部署
                else if (string.IsNullOrWhiteSpace(this.form.BUSHO_CD.Text))
                {
                    msgItem.Append("部署");
                    ctrlFocus = this.form.BUSHO_CD;
                }
                // 社員
                else if (string.IsNullOrWhiteSpace(this.form.SHAIN_CD.Text))
                {
                    msgItem.Append("社員");
                    ctrlFocus = this.form.SHAIN_CD;
                }
                else
                {
                    result = false;
                }

                // エラーメッセージ表示
                if (bDispMessage && result)
                {
                    if (ctrlFocus != null)
                    {
                        ctrlFocus.Focus();
                    }
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E001", msgItem.ToString());
                }

                LogUtility.DebugMethodEnd(result);
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                // 初期化
                var entityList = new M_MY_FAVORITE[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_MY_FAVORITE();
                }

                // 現在のデータソースを保存
                var orgSource = this.form.Ichiran.DataSource as DataTable;

                // 画面データ取得
                DataTable dt = orgSource.DefaultView.ToTable();
                //this.form.Ichiran.DataSource = orgSource.DefaultView.ToTable();

                // バインドロジック作成
                var binderLogic = new DataBinderLogic<M_MY_FAVORITE>(entityList);
                var addList = new List<M_MY_FAVORITE>();
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        int? dispNum = BookmarkHoshuLogic.GetNullableInt_ByObject(row[BookmarkHoshuConstans.MY_FAVORITE]);
                        if (dispNum != null)
                        {
                            // 行データからエンティティを作成し、リストに追加
                            addList.Add(this.CreateEntity_ByRow(row, binderLogic));
                        }
                    }
                }
                // 新規/修正対象データを設定
                this.dto.MyFavorite = addList.ToArray();

                // データソースを元に戻す
                //this.form.Ichiran.DataSource = orgSource;

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

                // エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    if (this.dto.MyFavorite != null && this.dto.MyFavorite.Length > 10)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("MOD003");
                    }
                    else
                    {
                        // トランザクション開始
                        using (var tran = new Transaction())
                        {
                            // 既存データの物理削除
                            this.daoFavorite.DeleteByPrimaryKey(this.entMyFavorite_ForSearch);

                            // データ登録
                            foreach (M_MY_FAVORITE myFavoriteEntity in this.dto.MyFavorite)
                            {
                                this.daoFavorite.Insert(myFavoriteEntity);
                            }
                            // トランザクション終了
                            tran.Commit();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("I001", "登録");
                    }
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
                return;
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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

                // DataViewとソート設定
                DataView dv = new DataView(table);
                dv.Sort = BookmarkHoshuConstans.ROW_NO;
                this.form.Ichiran.DataSource = dv.ToTable();

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M654", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // ファンクションボタンを活性
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

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click -= new EventHandler(this.form.Search);
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click -= new EventHandler(this.form.Regist);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            ////取消ボタン(F11)イベント生成
            //parentForm.bt_func11.Click -= new EventHandler(this.form.Cancel);
            //parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click -= new EventHandler(this.form.FormClose);
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
        }

        /// <summary>
        /// コントロールを初期化
        /// </summary>
        private void ControlInit()
        {
            this.form.BUSHO_CD.Text = SystemProperty.Shain.BushoCD;
            this.form.SHAIN_CD.Text = SystemProperty.Shain.CD;
            this.form.SHAIN_CD.Enabled = false;
            this.form.BUSHO_CD.Enabled = false;
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
        /// 行データからエンティティを作成
        /// </summary>
        /// <param name="row">行データ</param>
        /// <param name="binderLogic">バインドロジック</param>
        /// <returns></returns>
        private M_MY_FAVORITE CreateEntity_ByRow(DataRow row, DataBinderLogic<M_MY_FAVORITE> binderLogic)
        {
            var entity = new M_MY_FAVORITE();
            entity.INDEX_NO = BookmarkHoshuLogic.GetString_ByObject(row[BookmarkHoshuConstans.INDEX_NO]);
            entity.FORM_ID = BookmarkHoshuLogic.GetString_ByObject(row[BookmarkHoshuConstans.FORM_ID]);
            entity.BUSHO_CD = BookmarkHoshuLogic.GetString_ByObject(row[BookmarkHoshuConstans.BUSHO_CD]);
            entity.SHAIN_CD = BookmarkHoshuLogic.GetString_ByObject(row[BookmarkHoshuConstans.SHAIN_CD]);
            int? dispNum = BookmarkHoshuLogic.GetNullableInt_ByObject(row[BookmarkHoshuConstans.MY_FAVORITE]);
            if (dispNum != null)
            {
                entity.MY_FAVORITE = SqlInt32.Parse(dispNum.ToString());
            }

            binderLogic.SetSystemProperty(entity, false);

            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), entity);
            entity.DELETE_FLG = BookmarkHoshuLogic.GetBool_ByObject(row[Const.BookmarkHoshuConstans.DELETE_FLG]);

            return entity;
        }

        #region データテーブルのクローン

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

        #region 部署、社員名の取得

        /// <summary>
        /// 部署CDをキーにして業者マスターから部署名を取得する。
        /// </summary>
        /// <param name="bushoCD"></param>
        /// <param name="bushoNameRyaku"></param>
        /// <returns></returns>
        public bool GetBushoName(string bushoCD, out string bushoNameRyaku, out bool catchErr)
        {
            bool result = false;
            catchErr = false;
            try
            {
                bushoNameRyaku = string.Empty;
                LogUtility.DebugMethodStart(bushoCD, bushoNameRyaku);

                var search = new M_BUSHO();
                search.BUSHO_CD = bushoCD;

                var bushoArray = this.daoBusho.GetAllValidData(search);
                if (bushoArray != null && bushoArray.Length > 0)
                {
                    bushoNameRyaku = bushoArray[0].BUSHO_NAME_RYAKU;
                    result = true;
                }

                LogUtility.DebugMethodEnd(bushoCD, bushoNameRyaku, catchErr);
                return result;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                bushoNameRyaku = string.Empty;
                LogUtility.Error("GetBushoName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(bushoCD, bushoNameRyaku, catchErr);
                return result;
            }
            catch (Exception ex)
            {
                catchErr = true;
                bushoNameRyaku = string.Empty;
                LogUtility.Error("GetBushoName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(bushoCD, bushoNameRyaku, catchErr);
                return result;
            }
        }

        /// <summary>
        /// 社員CDをキーにして現場マスターから社員名を取得する。
        /// 部署CDが一致する場合のみ値を返す。
        /// </summary>
        /// <param name="shainCD"></param>
        /// <param name="bushoCD"></param>
        /// <param name="shainNameRyaku"></param>
        /// <returns></returns>
        public bool GetShainName(string shainCD, ref string bushoCD, out string shainNameRyaku, out bool catchErr)
        {
            try
            {
                catchErr = false;
                shainNameRyaku = string.Empty;
                LogUtility.DebugMethodStart(shainCD, bushoCD, shainNameRyaku);

                var result = false;
                var search = new M_SHAIN();
                search.SHAIN_CD = shainCD;
                if (!string.IsNullOrEmpty(bushoCD))
                {
                    search.BUSHO_CD = bushoCD;
                }
                var shainArray = this.daoShain.GetAllValidData(search);
                if (shainArray != null && shainArray.Length > 0)
                {
                    var shain = shainArray[0];
                    if (string.IsNullOrEmpty(bushoCD))
                    {
                        bushoCD = shain.BUSHO_CD;
                        shainNameRyaku = shain.SHAIN_NAME;
                        result = true;
                    }
                    else if (bushoCD.Equals(shain.BUSHO_CD))
                    {
                        shainNameRyaku = shain.SHAIN_NAME;
                        result = true;
                    }
                }

                LogUtility.DebugMethodEnd(shainCD, bushoCD, shainNameRyaku, catchErr);
                return result;
            }
            catch (SQLRuntimeException ex2)
            {
                shainNameRyaku = string.Empty;
                catchErr = true;
                LogUtility.Error("GetShainName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(shainCD, bushoCD, shainNameRyaku, catchErr);
                return false;
            }
            catch (Exception ex)
            {
                shainNameRyaku = string.Empty;
                catchErr = true;
                LogUtility.Error("GetShainName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(shainCD, bushoCD, shainNameRyaku, catchErr);
                return false;
            }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        internal bool ClearCondition()
        {
            try
            {
                this.SetSearchString();

                // 一覧の初期化
                this.ClearIchiran();
                if (this.Search() > 0)
                {
                    this.SetIchiran();
                }
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ClearCondition", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 一覧の初期化
        /// </summary>
        internal bool ClearIchiran()
        {
            try
            {
                this.SearchResult = null;
                this.form.Ichiran.DataSource = this.SearchResult;
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #endregion

        #region 前回データ保存

        /// <summary>
        /// 前回設定値取得
        /// </summary>
        public void GetPrevData()
        {
            // 部署
            this.form.BUSHO_CD.Text = Properties.Settings.Default.BUSHO_CD_Text;
            if (!string.IsNullOrEmpty(this.form.BUSHO_CD.Text))
            {
                var strBushoName = string.Empty;
                bool ret = true;
                if (this.GetBushoName(this.form.BUSHO_CD.Text, out strBushoName, out ret))
                {
                    this.form.BUSHO_NAME_RYAKU.Text = strBushoName;
                }
                if (ret)
                {
                    return;
                }
                if (this.form.BUSHO_CD.Text.Equals("999"))
                {
                    this.form.BUSHO_CD_HIDDEN.Text = string.Empty;
                }
                else
                {
                    this.form.BUSHO_CD_HIDDEN.Text = this.form.BUSHO_CD.Text;
                }
            }
            else
            {
                this.form.BUSHO_CD_HIDDEN.Text = string.Empty;
            }

            // 社員
            this.form.SHAIN_CD.Text = Properties.Settings.Default.SHAIN_CD_Text;
            if (!string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
            {
                var strShainName = string.Empty;
                var strBushoCD = this.form.BUSHO_CD.Text;
                bool ret = true;
                if (this.GetShainName(this.form.SHAIN_CD.Text, ref strBushoCD, out strShainName, out ret))
                {
                    if (ret)
                    {
                        return;
                    }
                    this.form.BUSHO_CD_HIDDEN.Text = strBushoCD;
                    this.form.SHAIN_NAME_RYAKU.Text = strShainName;
                }
                else
                {
                    if (ret)
                    {
                        return;
                    }
                    this.form.SHAIN_CD.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 前回設定値保存
        /// </summary>
        public bool SetPrevData()
        {
            try
            {
                // 部署
                Properties.Settings.Default.BUSHO_CD_Text = this.form.BUSHO_CD.Text;

                // 社員
                Properties.Settings.Default.SHAIN_CD_Text = this.form.SHAIN_CD.Text;

                // 保存
                Properties.Settings.Default.Save();
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetPrevData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        #endregion

        #region メニューテーブル作成

        /// <summary>
        /// メニューアイテムクラス取得
        /// </summary>
        private void GetMenuItems()
        {
            MasterBaseForm baseForm;
            r_framework.APP.Base.RibbonMainMenu ribbonForm;

            baseForm = this.form.Parent as MasterBaseForm;
            if (baseForm != null)
            {
                ribbonForm = baseForm.ribbonForm as r_framework.APP.Base.RibbonMainMenu;
                if (ribbonForm != null)
                {
                    this.menuItems = ribbonForm.menuItems;
                }
            }
        }

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        private void CreateMenuTable()
        {
            this.menuTable = new DataTable();

            BookmarkHoshuLogic.AddColumn_ForDataTable(this.menuTable, BookmarkHoshuConstans.KUBUN_NAME, typeof(string));
            BookmarkHoshuLogic.AddColumn_ForDataTable(this.menuTable, BookmarkHoshuConstans.KINOU_NAME, typeof(string));
            BookmarkHoshuLogic.AddColumn_ForDataTable(this.menuTable, BookmarkHoshuConstans.INDEX_NO, typeof(string));
            BookmarkHoshuLogic.AddColumn_ForDataTable(this.menuTable, BookmarkHoshuConstans.FORM_ID, typeof(string));
            BookmarkHoshuLogic.AddColumn_ForDataTable(this.menuTable, BookmarkHoshuConstans.MENU_NAME, typeof(string));

            // メニューテーブル作成
            this.SetMenuTable(this.menuTable);
        }

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        /// <param name="table"></param>
        private void SetMenuTable(DataTable table)
        {
            // データテーブル設定
            foreach (var menuItem in this.menuItems)
            {
                var groupItem = menuItem as GroupItem;
                if (groupItem != null && !groupItem.Disabled)
                {
                    this.SetMenuTable(table, groupItem);
                }
            }
        }

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem)
        {
            foreach (var item in groupItem.SubItems)
            {
                var subItem = item as SubItem;
                if (subItem != null && !subItem.Disabled)
                {
                    this.SetMenuTable(table, groupItem, subItem);
                }
            }
        }

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        /// <param name="subItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem, SubItem subItem)
        {
            // マイメニューの場合は飛ばす
            if (subItem.Name.Equals("マイメニュー"))
            {
                return;
            }

            foreach (var item in subItem.AssemblyItems)
            {
                var assemblyItem = item as AssemblyItem;
                if (assemblyItem != null)
                {
                    // 画面IDがNULLの場合、飛ばす
                    // ※ログアウト等
                    if (string.IsNullOrEmpty(assemblyItem.FormID))
                    {
                        continue;
                    }

                    // 無効の場合は飛ばす
                    if (assemblyItem.Disabled)
                    {
                        continue;
                    }

                    // 表示させないメニューの場合は飛ばす
                    if (this.NotDispFormIdList.Contains(assemblyItem.FormID))
                    {
                        continue;
                    }

                    this.SetMenuTable(table, groupItem, subItem, assemblyItem);
                }
            }
        }

        /// <summary>
        /// メニューテーブル作成
        /// </summary>
        /// <param name="table"></param>
        /// <param name="groupItem"></param>
        /// <param name="subItem"></param>
        /// <param name="assemblyItem"></param>
        private void SetMenuTable(DataTable table, GroupItem groupItem, SubItem subItem, AssemblyItem assemblyItem)
        {
            var row = table.NewRow();

            row[BookmarkHoshuConstans.KUBUN_NAME] = groupItem.Name;
            row[BookmarkHoshuConstans.KINOU_NAME] = subItem.Name;
            row[BookmarkHoshuConstans.INDEX_NO] = string.Format("{0}_{1}_{2}", groupItem.IndexNo, subItem.IndexNo, assemblyItem.IndexNo);
            row[BookmarkHoshuConstans.FORM_ID] = assemblyItem.FormID;
            row[BookmarkHoshuConstans.MENU_NAME] = assemblyItem.Name;

            table.Rows.Add(row);
        }

        #endregion

        #region マイメニューテーブル設定

        /// <summary>
        /// マイメニューデータテーブル設定
        /// </summary>
        /// <param name="table"></param>
        private void SetDataTable(DataTable table)
        {
            // 新規追加行の為、クローンテーブル作成
            var tblNewData = table.Clone();

            // 行番号初期化
            this.rowNo = 0;

            // データテーブル設定
            foreach (DataRow menuRow in this.menuTable.Rows)
            {
                this.SetDataTable(table, tblNewData, menuRow);
            }

            // 新規追加行保存用テーブルから新規追加行を加える
            // ※GetChangesで新規行と見なされるように
            table.Clear();
            foreach (DataRow row in tblNewData.Rows)
            {
                table.ImportRow(row);
            }

            // 新規/編集フラグを初期化
            // ※GetChangesの変更分取得の為
            // ※Insertに時間がかかる為、変更分のみ追加する場合
            table.AcceptChanges();
        }

        /// <summary>
        /// マイメニューデータテーブル設定
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tableNewData">新規追加行保存用テーブル</param>
        private void SetDataTable(DataTable table, DataTable tableNewData, DataRow menuRow)
        {
            var indexNo = BookmarkHoshuLogic.GetString_ByObject(menuRow[BookmarkHoshuConstans.INDEX_NO]);
            var formID = BookmarkHoshuLogic.GetString_ByObject(menuRow[BookmarkHoshuConstans.FORM_ID]);
            var kubunName = BookmarkHoshuLogic.GetString_ByObject(menuRow[BookmarkHoshuConstans.KUBUN_NAME]);
            var kinouName = BookmarkHoshuLogic.GetString_ByObject(menuRow[BookmarkHoshuConstans.KINOU_NAME]);
            var menuName = BookmarkHoshuLogic.GetString_ByObject(menuRow[BookmarkHoshuConstans.MENU_NAME]);

            // 対象の画面ID(&WindowID)データが存在するかをチェック
            DataRow[] rows;
            if (BookmarkHoshuLogic.FindDataRows_ByFormID(table, indexNo, formID, out rows))
            {
                // 対象の画面ID(&WindowID)データが存在する
                foreach (var row in rows)
                {
                    this.rowNo++;
                    row[BookmarkHoshuConstans.ROW_NO] = this.rowNo;

                    row[BookmarkHoshuConstans.KUBUN_NAME] = kubunName;
                    row[BookmarkHoshuConstans.KINOU_NAME] = kinouName;
                    row[BookmarkHoshuConstans.MENU_NAME] = menuName;

                    var newRow = tableNewData.NewRow();
                    newRow[BookmarkHoshuConstans.INDEX_NO] = indexNo;
                    newRow[BookmarkHoshuConstans.FORM_ID] = formID;
                    newRow[BookmarkHoshuConstans.BUSHO_CD] = (this.entMyFavorite_ForSearch.BUSHO_CD == null) ? string.Empty : this.entMyFavorite_ForSearch.BUSHO_CD;
                    newRow[BookmarkHoshuConstans.SHAIN_CD] = (this.entMyFavorite_ForSearch.SHAIN_CD == null) ? string.Empty : this.entMyFavorite_ForSearch.SHAIN_CD;
                    newRow[BookmarkHoshuConstans.DELETE_FLG] = false;
                    newRow[BookmarkHoshuConstans.KUBUN_NAME] = row[BookmarkHoshuConstans.KUBUN_NAME];
                    newRow[BookmarkHoshuConstans.KINOU_NAME] = row[BookmarkHoshuConstans.KINOU_NAME];
                    newRow[BookmarkHoshuConstans.MENU_NAME] = row[BookmarkHoshuConstans.MENU_NAME];
                    newRow[BookmarkHoshuConstans.MY_FAVORITE] = row[BookmarkHoshuConstans.MY_FAVORITE];
                    newRow[BookmarkHoshuConstans.CREATE_USER] = row[BookmarkHoshuConstans.CREATE_USER];
                    newRow[BookmarkHoshuConstans.CREATE_DATE] = row[BookmarkHoshuConstans.CREATE_DATE];
                    newRow[BookmarkHoshuConstans.UPDATE_USER] = row[BookmarkHoshuConstans.UPDATE_USER];
                    newRow[BookmarkHoshuConstans.UPDATE_DATE] = row[BookmarkHoshuConstans.UPDATE_DATE];
                    newRow[BookmarkHoshuConstans.ROW_NO] = this.rowNo;
                    tableNewData.Rows.Add(newRow);
                }
            }
            else
            {
                // 対象の画面ID(&WindowID)データが存在しない
                var newRow = tableNewData.NewRow();
                newRow[BookmarkHoshuConstans.INDEX_NO] = indexNo;
                newRow[BookmarkHoshuConstans.FORM_ID] = formID;
                newRow[BookmarkHoshuConstans.BUSHO_CD] = (this.entMyFavorite_ForSearch.BUSHO_CD == null) ? string.Empty : this.entMyFavorite_ForSearch.BUSHO_CD;
                newRow[BookmarkHoshuConstans.SHAIN_CD] = (this.entMyFavorite_ForSearch.SHAIN_CD == null) ? string.Empty : this.entMyFavorite_ForSearch.SHAIN_CD;
                newRow[BookmarkHoshuConstans.DELETE_FLG] = false;
                newRow[BookmarkHoshuConstans.KUBUN_NAME] = kubunName;
                newRow[BookmarkHoshuConstans.KINOU_NAME] = kinouName;
                newRow[BookmarkHoshuConstans.MENU_NAME] = menuName;
                //newRow[BookmarkHoshuConstans.MY_FAVORITE] = string.Empty;
                this.rowNo++;
                newRow[BookmarkHoshuConstans.ROW_NO] = this.rowNo;
                tableNewData.Rows.Add(newRow);
            }
        }

        #endregion

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            this.entMyFavorite_ForSearch = new M_MY_FAVORITE();
            this.entShain_ForSearch = new M_SHAIN();

            var strTemp = string.Empty;

            // 部署CD
            strTemp = this.form.BUSHO_CD.Text;
            if (!string.IsNullOrEmpty(strTemp))
            {
                this.entMyFavorite_ForSearch.BUSHO_CD = strTemp;
                this.entShain_ForSearch.BUSHO_CD = strTemp;
            }

            // 社員CD
            strTemp = this.form.SHAIN_CD.Text;
            if (!string.IsNullOrEmpty(strTemp))
            {
                this.entMyFavorite_ForSearch.SHAIN_CD = strTemp;
                this.entShain_ForSearch.SHAIN_CD = strTemp;
            }
        }

        /// <summary>
        /// マイメニューデータ検索
        /// </summary>
        /// <returns></returns>
        private DataTable SearchBookMark()
        {
            return this.daoFavorite.GetDataBySqlFile(this.GET_ICHIRAN_DATA_SQL, this.entMyFavorite_ForSearch);
        }

        /// <summary>
        /// マイメニューデータテーブル用初期化処理
        /// </summary>
        /// <param name="table"></param>
        private static void Init_DataTable(DataTable table)
        {
            // 表示用カラムを追加
            BookmarkHoshuLogic.AddColumn_ForDataTable(table, BookmarkHoshuConstans.KUBUN_NAME, typeof(string));
            BookmarkHoshuLogic.AddColumn_ForDataTable(table, BookmarkHoshuConstans.KINOU_NAME, typeof(string));
            BookmarkHoshuLogic.AddColumn_ForDataTable(table, BookmarkHoshuConstans.MENU_NAME, typeof(string));
            BookmarkHoshuLogic.AddColumn_ForDataTable(table, BookmarkHoshuConstans.MY_FAVORITE, typeof(int));
            BookmarkHoshuLogic.AddColumn_ForDataTable(table, BookmarkHoshuConstans.ROW_NO, typeof(int));
            BookmarkHoshuLogic.AddColumn_ForDataTable(table, BookmarkHoshuConstans.INDEX_NO, typeof(string));

            // DBNull許容設定
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.MY_FAVORITE, true);
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.CREATE_USER, true);
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.CREATE_DATE, true);
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.CREATE_PC, true);
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.UPDATE_USER, true);
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.UPDATE_DATE, true);
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.UPDATE_PC, true);
            BookmarkHoshuLogic.SetAllowDBNull_ForDataTable(table, BookmarkHoshuConstans.TIME_STAMP, true);

            // Unique設定
            BookmarkHoshuLogic.SetUnique_ForDataTable(table, BookmarkHoshuConstans.TIME_STAMP, false);
        }

        /// <summary>
        /// データテーブルへカラム追加
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="Type">データタイプ</param>
        private static void AddColumn_ForDataTable(DataTable table, string columnName, Type type)
        {
            if (!table.Columns.Contains(columnName))
            {
                table.Columns.Add(columnName, type);
            }
        }

        /// <summary>
        /// データテーブルへDBNull許容設定を行う
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="isAllowDBNull">DBNull許容</param>
        private static void SetAllowDBNull_ForDataTable(DataTable table, string columnName, bool isAllowDBNull)
        {
            if (table.Columns.Contains(columnName))
            {
                table.Columns[columnName].AllowDBNull = isAllowDBNull;
            }
        }

        /// <summary>
        /// データテーブルへUnique設定を行う
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="isUnique">Unique</param>
        private static void SetUnique_ForDataTable(DataTable table, string columnName, bool isUnique)
        {
            if (table.Columns.Contains(columnName))
            {
                table.Columns[columnName].Unique = isUnique;
            }
        }

        /// <summary>
        /// データテーブルに対象の画面IDが存在するか
        /// </summary>
        /// <param name="table">データテーブル</param>
        /// <param name="indexNo">indexNo</param>
        /// <param name="formID">画面ID</param>
        /// <param name="rows">データ行</param>
        /// <returns>true:存在する／false:存在しない</returns>
        private static bool FindDataRows_ByFormID(DataTable table, string indexNo, string formID, out DataRow[] rows)
        {
            var sbFilter = new StringBuilder(256);
            sbFilter.AppendFormat("{0} = '{1}'", BookmarkHoshuConstans.INDEX_NO, indexNo);
            sbFilter.Append(" AND ");
            sbFilter.AppendFormat("{0} = '{1}'", BookmarkHoshuConstans.FORM_ID, formID);

            rows = table.Select(sbFilter.ToString());
            return 0 < rows.Length;
        }

        /// <summary>
        /// 文字列取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetString_ByObject(object value)
        {
            var result = value as string;
            if (result == null)
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// INT取得
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private static int GetInt_ByObject(object value, int defaultValue = 0)
        {
            int result = defaultValue;
            if (value is int)
            {
                result = (int)value;
            }

            return result;
        }

        /// <summary>
        /// NULL許容INT取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static int? GetNullableInt_ByObject(object value)
        {
            int? result = null;
            int iTemp;
            if (value != null && int.TryParse(value.ToString(), out iTemp))
            {
                result = iTemp;
            }
            return result;
        }

        /// <summary>
        /// Bool取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool GetBool_ByObject(object value)
        {
            var result = value as bool?;
            if (!result.HasValue)
            {
                result = false;
            }

            return result.Value;
        }

        /// <summary>
        /// 一覧セル値変更時処理
        /// </summary>
        /// <param name="e"></param>
        public void Ichiran_CellValueChanged(CellEventArgs e)
        {
            // 行領域以外の場合、何もしない
            if (e.Scope != CellScope.Row ||
                e.RowIndex < 0)
            {
                return;
            }

            // 行取得
            var row = this.form.Ichiran.Rows[e.RowIndex];
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
            parentForm.bt_func9.Enabled = false;
        }

        /// <summary>
        /// 一覧入力内容チェック
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 1. 一覧にデータ存在チェック
        /// 2. 1～10以外の入力チェック
        /// 3. 表示順序重複チェック
        /// </remarks>
        internal bool RegistCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 一覧行数チェック
                if (this.form.Ichiran.Rows.Count <= 0)
                {
                    MessageBoxShowLogic msg = new MessageBoxShowLogic();
                    msg.MessageBoxShow("E061");
                    return true;
                }

                var groupMyFavorite = (this.form.Ichiran.DataSource as DataTable).AsEnumerable().
                    GroupBy(s => Convert.IsDBNull(s[BookmarkHoshuConstans.MY_FAVORITE]) ? -1 : Convert.ToInt32(s[BookmarkHoshuConstans.MY_FAVORITE]));
                foreach (var myFavorite in groupMyFavorite)
                {
                    if (myFavorite.Key > 0 && myFavorite.Count() > 1)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E034", "重複しない表示順序");
                        return true;
                    }

                    if (myFavorite.Key == 0)
                    {
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShowError("表示順序は1～10間の数字で入力してください。");
                        return true;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
    }
}