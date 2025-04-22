// $Id: DenManiJigyoujouHoshuLogic.cs 53608 2015-06-26 00:09:57Z miya@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DenManiJigyoujouHoshu.APP;
using MasterCommon.Logic;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace DenManiJigyoujouHoshu.Logic
{
    /// <summary>
    /// 事業者入力のビジネスロジック
    /// </summary>
    public class DenManiJigyoujouHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "DenManiJigyoujouHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_JIGYOUJOU_DATA_BY_CD_SQL = "DenManiJigyoujouHoshu.Sql.GetJigyoujouDataByCdSql.sql";

        private readonly string GET_JIGYOUSHAMEISHO_SQL = "DenManiJigyoujouHoshu.Sql.GetJigyoushameiByCdSql.sql";
        
        private static readonly string ASSEMBLY_NAME = "JushoKensakuPopup2";

        private static readonly string CALASS_NAME_SPACE = "APP.JushoKensakuPopupForm2";

        /// <summary>
        /// 事業者入力Form
        /// </summary>
        private DenManiJigyoujouHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 事業場マスタのエンティティ
        /// </summary>
        private M_DENSHI_JIGYOUJOU entitys;

        /// <summary>
        /// データ取得用テーブル
        /// </summary>
        private DataTable table;

        /// <summary>
        /// 事業場マスタのDao
        /// </summary>
        private IM_DENSHI_JIGYOUJOUDao dao;

        /// <summary>
        /// 事業者マスタのDao
        /// </summary>
        private IM_DENSHI_JIGYOUSHADao nameDao;

        /// <summary>
        /// 業者マスタのDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場マスタのDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 郵便辞書マスタのDao
        /// </summary>
        private IS_ZIP_CODEDao zipCodeDao;

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
        public M_DENSHI_JIGYOUJOU SearchString { get; set; }

        /// <summary>
        /// 加入者番号
        /// </summary>
        public string kanyushaId { get; set; }

        /// <summary>
        /// 事業場CD
        /// </summary>
        public string jigyoujouCd { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public DenManiJigyoujouHoshuLogic(DenManiJigyoujouHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            entitys = new M_DENSHI_JIGYOUJOU();

            table = new DataTable();

            this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
            this.nameDao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.zipCodeDao = DaoInitUtility.GetComponent<IS_ZIP_CODEDao>();

            LogUtility.DebugMethodEnd();
        }


        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;


                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.InitPopupPropertyForEDI_MEMBER_ID();

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);
                this.allControl = this.form.allControl;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            if (string.IsNullOrEmpty(this.kanyushaId) && string.IsNullOrEmpty(this.jigyoujouCd))
            {
                // 【新規】モードで初期化
                bool catchErr = this.WindowInitNewMode(parentForm);
                if (catchErr)
                {
                    throw new Exception("");
                    
                }
            }
            else
            {
                // 【複写】モードで初期化
                this.WindowInitNewCopyMode(parentForm);
            }
        }

        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロールを操作可能とする
                this.AllControlLock(false);

                // データ検索
                this.SetSearchString();
                this.Search();

                // 検索結果を画面に設定
                this.SetWindowData();

                // 修正モード固有UI設定
                this.form.EDI_MEMBER_ID.Enabled = false;   // 加入者ID
                this.form.JIGYOUJOU_CD.Enabled = false;    // 事業場CD

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                this.form.JIGYOUSHA_KBN.Focus();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitUpdate", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public void WindowInitDelete(BusinessBaseForm parentForm)
        {
            // 削除モード固有UI設定
            this.AllControlLock(true);

            // データ検索
            this.Search();

            // 検索結果を画面に設定
            this.SetSearchString();
            this.SetWindowData();

            // 修正モード固有UI設定
            this.form.EDI_MEMBER_ID.Enabled = false;   // 加入者ID
            this.form.JIGYOUJOU_CD.Enabled = false;    // 事業場CD

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = false;   // 取消
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            try
            {
                // 参照モード固有UI設定
                this.AllControlLock(true);

                // データ検索
                this.Search();

                // 検索結果を画面に設定
                this.SetSearchString();
                this.SetWindowData();

                // 参照モード固有UI設定
                this.form.EDI_MEMBER_ID.Enabled = false;   // 加入者ID
                this.form.JIGYOUJOU_CD.Enabled = false;    // 事業場CD

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = false;    // 登録
                parentForm.bt_func11.Enabled = false;   // 取消
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitReference", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitReference", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        public bool WindowInitNewMode(BusinessBaseForm parentForm)
        {
            try
            {
                // 全コントロール操作可能とする
                this.AllControlLock(false);

                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)parentForm.headerForm;
                header.CreateDate.Text = string.Empty;
                header.CreateUser.Text = string.Empty;
                header.LastUpdateDate.Text = string.Empty;
                header.LastUpdateUser.Text = string.Empty;
                header.windowTypeLabel.Text = "新規";
                header.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                header.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 入力項目
                this.form.JIGYOUSHA_KBN.Text = "1";
                this.form.EDI_MEMBER_ID.Text = string.Empty;
                this.form.JIGYOUSHA_NAME.Text = string.Empty;
                this.form.JIGYOUJOU_CD.Text = string.Empty;
                this.form.JIGYOUJOU_NAME.Text = string.Empty;
                this.form.JIGYOUJOU_POST.Text = string.Empty;
                this.form.JIGYOUJOU_ADDRESS1.Text = string.Empty;
                this.form.JIGYOUJOU_ADDRESS2.Text = string.Empty;
                this.form.JIGYOUJOU_ADDRESS3.Text = string.Empty;
                this.form.JIGYOUJOU_ADDRESS4.Text = string.Empty;
                this.form.JIGYOUJOU_TEL.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME.Text = string.Empty;
                this.form.JWNET_JIGYOUJOU_CD.Text = string.Empty;

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                this.form.JIGYOUSHA_KBN.Focus();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        public void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            DetailedHeaderForm header = (DetailedHeaderForm)parentForm.headerForm;
            bool catchErr = false;

            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNew(parentForm);
                    header.windowTypeLabel.Text = "新規";
                    header.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                    header.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    catchErr = this.WindowInitUpdate(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    header.windowTypeLabel.Text = "修正";
                    header.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                    header.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    break;

                // 【削除】モード
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    this.WindowInitDelete(parentForm);
                    header.windowTypeLabel.Text = "削除";
                    header.windowTypeLabel.BackColor = System.Drawing.Color.Red;
                    header.windowTypeLabel.ForeColor = System.Drawing.Color.White;
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    catchErr = this.WindowInitReference(parentForm);
                    if (catchErr)
                    {
                        throw new Exception("");
                    }
                    header.windowTypeLabel.Text = "参照";
                    header.windowTypeLabel.BackColor = System.Drawing.Color.Orange;
                    header.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNew(parentForm);
                    header.windowTypeLabel.Text = "新規";
                    header.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                    header.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
                    break;
            }
        }

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        /// <param name="parentForm">親フォーム</param>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            // 全コントロール操作可能とする
            this.AllControlLock(false);

            // データ検索
            this.SetSearchString();
            this.Search();

            // 検索結果を画面に設定
            this.SetWindowData();

            // 複写モード固有UI設定
            DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = string.Empty;
            header.CreateUser.Text = string.Empty;
            header.LastUpdateDate.Text = string.Empty;
            header.LastUpdateUser.Text = string.Empty;
            this.form.JIGYOUJOU_CD.Text = string.Empty;
            //20150617 #3746 hoanghm start
            //this.form.EDI_MEMBER_ID.Text = string.Empty;
            //20150617 #3746 hoanghm end

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;    // 取消

            this.form.JIGYOUSHA_KBN.Focus();
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// <summary>
        private void SetWindowData()
        {
            this.SetSearchString();
            if (Search() > 0)
            {
                // ヘッダー項目
                DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
                header.CreateDate.Text = this.entitys.CREATE_DATE.ToString();
                header.CreateUser.Text = this.entitys.CREATE_USER;
                header.LastUpdateDate.Text = this.entitys.UPDATE_DATE.ToString();
                header.LastUpdateUser.Text = this.entitys.UPDATE_USER;

                this.form.JIGYOUSHA_KBN.Text = this.entitys.JIGYOUSHA_KBN.ToString();
                this.form.EDI_MEMBER_ID.Text = this.entitys.EDI_MEMBER_ID;
                this.form.JIGYOUSHA_NAME.Text = this.getJigyoushaName(this.form.EDI_MEMBER_ID.Text);
                this.form.JIGYOUJOU_CD.Text = this.entitys.JIGYOUJOU_CD;
                this.form.JIGYOUJOU_POST.Text = this.entitys.JIGYOUJOU_POST;
                this.form.JIGYOUJOU_NAME.Text = this.entitys.JIGYOUJOU_NAME;
                this.form.JIGYOUJOU_ADDRESS1.Text = this.entitys.JIGYOUJOU_ADDRESS1;
                this.form.JIGYOUJOU_ADDRESS2.Text = this.entitys.JIGYOUJOU_ADDRESS2;
                this.form.JIGYOUJOU_ADDRESS3.Text = this.entitys.JIGYOUJOU_ADDRESS3;
                this.form.JIGYOUJOU_ADDRESS4.Text = this.entitys.JIGYOUJOU_ADDRESS4;
                this.form.JIGYOUJOU_TEL.Text = this.entitys.JIGYOUJOU_TEL;
                this.form.GYOUSHA_CD.Text = this.entitys.GYOUSHA_CD;
                bool catchErr = false;
                this.form.GYOUSHA_NAME.Text = this.getGyoushaName(this.form.GYOUSHA_CD.Text, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
                this.form.GENBA_CD.Text = this.entitys.GENBA_CD;
                this.form.JWNET_JIGYOUJOU_CD.Text = this.entitys.JWNET_JIGYOUJOU_CD;
                this.form.GENBA_NAME.Text = this.getGenbaName(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, out catchErr);
                if (catchErr)
                {
                    throw new Exception("");
                }
            }
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作不可、false:操作可</param>
        private void AllControlLock(bool isBool)
        {
            // 入力項目
            this.form.JIGYOUSHA_KBN.ReadOnly = isBool;
            this.form.JIGYOUSHA_KBN_1.Enabled = !isBool;
            this.form.JIGYOUSHA_KBN_2.Enabled = !isBool;
            this.form.JIGYOUSHA_KBN_3.Enabled = !isBool;
            this.form.EDI_MEMBER_ID.Enabled = !isBool;
            this.form.JIGYOUJOU_CD.Enabled = !isBool;
            this.form.JIGYOUJOU_NAME.ReadOnly = isBool;
            this.form.JIGYOUJOU_POST.ReadOnly = isBool;
            this.form.JIGYOUJOU_ADDRESS1.ReadOnly = isBool;
            this.form.JIGYOUJOU_ADDRESS2.ReadOnly = isBool;
            this.form.JIGYOUJOU_ADDRESS3.ReadOnly = isBool;
            this.form.JIGYOUJOU_ADDRESS4.ReadOnly = isBool;
            this.form.JIGYOUJOU_TEL.ReadOnly = isBool;
            this.form.JWNET_JIGYOUJOU_CD.ReadOnly = isBool;
            this.form.GYOUSHA_CD.ReadOnly = isBool;
            this.form.GYOUSHA_CD.Enabled = !isBool;
            this.form.GENBA_CD.ReadOnly = isBool;
            this.form.GENBA_CD.Enabled = !isBool;
            this.form.GYOUSHA_SEARCH_BUTTON.Enabled = !isBool;
            this.form.GENBA_SEARCH_BUTTON.Enabled = !isBool;
            this.form.JIGYOUJOU_POST_SEACRH_BUTTON.Enabled = !isBool;
            this.form.SIKUCHOUSON_SEARCH_BUTTON.Enabled = !isBool;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        /// 
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int count;

            this.table = this.dao.GetDataBySqlFile(GET_JIGYOUJOU_DATA_BY_CD_SQL, this.SearchString);
            if (table.Rows.Count > 0)
            {
                this.entitys.JIGYOUSHA_KBN = Int16.Parse(this.table.Rows[0]["JIGYOUSHA_KBN"].ToString());
                this.entitys.EDI_MEMBER_ID = this.table.Rows[0]["EDI_MEMBER_ID"].ToString();
                this.entitys.JIGYOUJOU_CD = this.table.Rows[0]["JIGYOUJOU_CD"].ToString();
                this.entitys.JIGYOUJOU_NAME = this.table.Rows[0]["JIGYOUJOU_NAME"].ToString();
                this.entitys.JIGYOUJOU_POST = this.table.Rows[0]["JIGYOUJOU_POST"].ToString();
                this.entitys.JIGYOUJOU_ADDRESS1 = this.table.Rows[0]["JIGYOUJOU_ADDRESS1"].ToString();
                this.entitys.JIGYOUJOU_ADDRESS2 = this.table.Rows[0]["JIGYOUJOU_ADDRESS2"].ToString();
                this.entitys.JIGYOUJOU_ADDRESS3 = this.table.Rows[0]["JIGYOUJOU_ADDRESS3"].ToString();
                this.entitys.JIGYOUJOU_ADDRESS4 = this.table.Rows[0]["JIGYOUJOU_ADDRESS4"].ToString();
                this.entitys.JIGYOUJOU_TEL = this.table.Rows[0]["JIGYOUJOU_TEL"].ToString();
                this.entitys.GYOUSHA_CD = this.table.Rows[0]["GYOUSHA_CD"].ToString();
                this.entitys.GENBA_CD = this.table.Rows[0]["GENBA_CD"].ToString();
                this.entitys.JWNET_JIGYOUJOU_CD = this.table.Rows[0]["JWNET_JIGYOUJOU_CD"].ToString();
                this.entitys.CREATE_USER = this.table.Rows[0]["CREATE_USER"].ToString();
                this.entitys.CREATE_DATE = (DateTime)this.table.Rows[0]["CREATE_DATE"];
                this.entitys.CREATE_PC = this.table.Rows[0]["CREATE_PC"].ToString();
                this.entitys.UPDATE_USER = this.table.Rows[0]["UPDATE_USER"].ToString();
                this.entitys.UPDATE_DATE = (DateTime)this.table.Rows[0]["UPDATE_DATE"];
                this.entitys.UPDATE_PC = this.table.Rows[0]["UPDATE_PC"].ToString();
            }

            count = table.Rows.Count;

            return count;
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.entitys.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                this.entitys.JIGYOUJOU_CD = this.form.JIGYOUJOU_CD.Text;
                this.entitys.JIGYOUSHA_KBN = Int16.Parse(this.form.JIGYOUSHA_KBN.Text);
                this.entitys.JIGYOUJOU_KBN = Int16.Parse(this.form.JIGYOUSHA_KBN.Text);
                this.entitys.JIGYOUJOU_NAME = this.form.JIGYOUJOU_NAME.Text;
                this.entitys.JIGYOUJOU_POST = this.form.JIGYOUJOU_POST.Text;
                this.entitys.JIGYOUJOU_ADDRESS1 = this.form.JIGYOUJOU_ADDRESS1.Text;
                this.entitys.JIGYOUJOU_ADDRESS2 = this.form.JIGYOUJOU_ADDRESS2.Text;
                this.entitys.JIGYOUJOU_ADDRESS3 = this.form.JIGYOUJOU_ADDRESS3.Text;
                this.entitys.JIGYOUJOU_ADDRESS4 = this.form.JIGYOUJOU_ADDRESS4.Text;
                this.entitys.JIGYOUJOU_TEL = this.form.JIGYOUJOU_TEL.Text;
                this.entitys.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                this.entitys.GENBA_CD = this.form.GENBA_CD.Text;
                this.entitys.JWNET_JIGYOUJOU_CD = this.form.JWNET_JIGYOUJOU_CD.Text;

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

                bool catchErr = false;
                if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    this.kanyushaId = this.form.EDI_MEMBER_ID.Text;
                    this.jigyoujouCd = this.form.JIGYOUJOU_CD.Text;
                    this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                    this.form.HeaderFormInit();
                    catchErr = this.WindowInitUpdate((BusinessBaseForm)this.form.ParentForm);
                }
                else
                {
                    this.ClearCondition();
                    this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    this.form.HeaderFormInit();
                    this.kanyushaId = string.Empty;
                    this.jigyoujouCd = string.Empty;
                    catchErr = this.WindowInitNewMode((BusinessBaseForm)this.form.ParentForm);
                }

                LogUtility.DebugMethodEnd(catchErr);
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
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

                if (!errorFlag)
                {
                    this.table = this.dao.GetDataBySqlFile(GET_JIGYOUJOU_DATA_BY_CD_SQL, entitys);

                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_JIGYOUJOU>(this.entitys);
                    dataBinderLogic.SetSystemProperty(this.entitys, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitys);

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        if (table.Rows.Count <= 0)
                        {
                            this.dao.Insert(this.entitys);
                        }
                        else
                        {
                            this.dao.Update(this.entitys);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    this.isRegist = true;

                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "登録");
                this.isRegist = true;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
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
            try
            {
                if (!errorFlag)
                {
                    table = this.dao.GetDataBySqlFile(GET_JIGYOUJOU_DATA_BY_CD_SQL, entitys);

                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_JIGYOUJOU>(entitys);
                    dataBinderLogic.SetSystemProperty(entitys, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitys);

                    foreach (DataRow row in table.Rows)
                    {
                        entitys.EDI_MEMBER_ID = (string)row["EDI_MEMBER_ID"];
                        entitys.TIME_STAMP = (byte[])row["TIME_STAMP"];
                    }

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        if (table.Rows.Count == 0)
                        {
                            this.dao.Insert(entitys);
                        }
                        else
                        {
                            this.dao.Update(entitys);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("I001", "更新");
                this.isRegist = true;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                LogUtility.Error("Update", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                LogUtility.Error("Update", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
                LogUtility.Error("Update", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
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

                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    table = this.dao.GetDataBySqlFile(GET_JIGYOUJOU_DATA_BY_CD_SQL, entitys);

                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_JIGYOUJOU>(entitys);
                    dataBinderLogic.SetSystemProperty(entitys, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), this.entitys);

                    foreach (DataRow row in table.Rows)
                    {
                        entitys.EDI_MEMBER_ID = (string)row["EDI_MEMBER_ID"];
                        entitys.TIME_STAMP = (byte[])row["TIME_STAMP"];
                    }

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        this.dao.Delete(entitys);
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");

                    this.isRegist = true;
                }

                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.isRegist = false;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.isRegist = false;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.isRegist = false;
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
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            var result = msgLogic.MessageBoxShow("C026");
            if (result == DialogResult.Yes)
            {
                // トランザクション開始
                using (var tran = new Transaction())
                {
                    this.dao.Delete(entitys);
                    // トランザクション終了
                    tran.Commit();
                }

                msgLogic.MessageBoxShow("I001", "削除");

                this.isRegist = true;
            }

            LogUtility.DebugMethodEnd();            
        }
        #endregion

        #region Equals/GetHashCode/ToString
         //<summary>
         //クラスが等しいかどうか判定
         //</summary>
         //<param name="other"></param>
         //<returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            DenManiJigyoujouHoshuLogic localLogic = other as DenManiJigyoujouHoshuLogic;
            return localLogic == null ? false : true;
        }

        //<summary>
        //ハッシュコード取得
        //</summary>
        //<returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        // <summary>
        //該当するオブジェクトを文字列形式で取得
        //</summary>
        //<returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        internal void SetSearchString()
        {
            M_DENSHI_JIGYOUJOU entity = new M_DENSHI_JIGYOUJOU();

            // 検索条件の設定
            entity.EDI_MEMBER_ID = this.kanyushaId;
            entity.JIGYOUJOU_CD = this.jigyoujouCd;
            this.SearchString = entity;
        }


        //<summary>
        //ボタン初期化処理
        //</summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

         //<summary>
         //イベントの初期化処理
         //</summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;


            //新規(F2)イベント生成
            parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

            //修正(F3)イベント生成
            parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);;

            //一覧(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取り消し(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            this.form.GYOUSHA_CD.Enter += new EventHandler(this.form.GYOUSHA_CD_Enter);
        }

        /// <summary>
        /// 加入者番号をキーにして電子事業者マスターから事業者名を取得する。
        /// </summary>
        /// <param name="inp">string 加入者番号</param>
        /// <returns>string 事業者名</returns>
        public string getJigyoushaName(string inp)
        {
            LogUtility.DebugMethodStart(inp);

            M_DENSHI_JIGYOUSHA nameEnt = new M_DENSHI_JIGYOUSHA();
            string st = string.Empty;

            nameEnt.EDI_MEMBER_ID = inp;
            this.table = this.nameDao.GetDataBySqlFile(GET_JIGYOUSHAMEISHO_SQL, nameEnt);
            if (this.table.Rows.Count > 0)
            {
                nameEnt.JIGYOUSHA_NAME = this.table.Rows[0]["JIGYOUSHA_NAME"].ToString();
            }
            st = nameEnt.JIGYOUSHA_NAME;

            LogUtility.DebugMethodEnd(inp);
            return st;
        }

        /// <summary>
        /// 電子事業者情報の取得を行う
        /// </summary>
        public bool SearchDenshiJigyoushaData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
                {
                    this.form.JIGYOUSHA_NAME.Text = string.Empty;
                    LogUtility.DebugMethodEnd();
                    return true;
                }

                bool ret = false;
                M_DENSHI_JIGYOUSHA searchEntity = new M_DENSHI_JIGYOUSHA();
                searchEntity.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                DataTable dt = this.nameDao.GetDataBySqlFile(GET_JIGYOUSHAMEISHO_SQL, searchEntity);

                if (dt.Rows.Count > 0)
                {
                    if (this.form.JIGYOUSHA_KBN.Text == "1" && dt.Rows[0]["HST_KBN"].ToString() == "True")
                    {
                        this.form.JIGYOUSHA_NAME.Text = dt.Rows[0]["JIGYOUSHA_NAME"].ToString();
                        ret = true;
                    }
                    else if (this.form.JIGYOUSHA_KBN.Text == "2" && dt.Rows[0]["UPN_KBN"].ToString() == "True")
                    {
                        this.form.JIGYOUSHA_NAME.Text = dt.Rows[0]["JIGYOUSHA_NAME"].ToString();
                        ret = true;
                    }
                    else if (this.form.JIGYOUSHA_KBN.Text == "3" && dt.Rows[0]["SBN_KBN"].ToString() == "True")
                    {
                        this.form.JIGYOUSHA_NAME.Text = dt.Rows[0]["JIGYOUSHA_NAME"].ToString();
                        ret = true;
                    }
                    else if (string.IsNullOrWhiteSpace(this.form.JIGYOUSHA_KBN.Text))
                    {
                        this.form.JIGYOUSHA_NAME.Text = dt.Rows[0]["JIGYOUSHA_NAME"].ToString();
                        ret = true;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", this.form.EDI_MEMBER_ID.DisplayItemName);
                    }
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", this.form.EDI_MEMBER_ID.DisplayItemName);
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchDenshiJigyoushaData", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchDenshiJigyoushaData", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 業者CDをキーにして業者マスターから業者名を取得する。
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <returns></returns>
        public string getGyoushaName(string gyoushaCD,out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(gyoushaCD);

                string st = string.Empty;
                catchErr = false;

                M_GYOUSHA gyousha = this.gyoushaDao.GetDataByCd(gyoushaCD);
                if (gyousha != null)
                {
                    st = gyousha.GYOUSHA_NAME_RYAKU;
                }

                LogUtility.DebugMethodEnd(gyoushaCD, catchErr);
                return st;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("getGyoushaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(gyoushaCD, catchErr);
                return "";
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("getGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(gyoushaCD, catchErr);
                return "";
            }
        }

        /// <summary>
        /// 現場CDをキーにして現場マスターから現場名を取得する。
        /// </summary>
        /// <param name="gyoushaCD"></param>
        /// <param name="genbaCD"></param>
        /// <returns></returns>
        public string getGenbaName(string gyoushaCD, string genbaCD,out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(gyoushaCD, genbaCD);

                string st = string.Empty;
                catchErr = false;

                M_GENBA searchParam = new M_GENBA();
                searchParam.GYOUSHA_CD = gyoushaCD;
                searchParam.GENBA_CD = genbaCD;
                M_GENBA genba = this.genbaDao.GetDataByCd(searchParam);
                if (genba != null)
                {
                    st = genba.GENBA_NAME_RYAKU;
                }

                LogUtility.DebugMethodEnd(gyoushaCD, genbaCD, catchErr);
                return st;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("getGenbaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(gyoushaCD, genbaCD, catchErr);
                return "";
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("getGenbaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(gyoushaCD, genbaCD, catchErr);
                return "";
            }
        }

        /// <summary>
        /// 事業場変更後処理
        /// </summary>
        public bool ChangeJigyoujouCD()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
                {
                    this.entitys = new M_DENSHI_JIGYOUJOU();
                    this.entitys.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                    this.entitys.JIGYOUJOU_CD = this.form.JIGYOUJOU_CD.Text;
                    this.table = this.dao.GetDataBySqlFile(this.GET_JIGYOUJOU_DATA_BY_CD_SQL, this.entitys);

                    if (this.table.Rows.Count > 0)
                    {
                        DialogResult res = msgLogic.MessageBoxShow("C017");
                        if (res == DialogResult.Yes)
                        {
                            bool catchErr = false;
                            // 権限チェック
                            // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                            if (r_framework.Authority.Manager.CheckAuthority("M312", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                            {
                                this.kanyushaId = this.form.EDI_MEMBER_ID.Text;
                                this.jigyoujouCd = this.form.JIGYOUJOU_CD.Text;
                                this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                                this.form.HeaderFormInit();
                                catchErr = this.WindowInitUpdate((BusinessBaseForm)this.form.ParentForm);
                            }
                            else if (r_framework.Authority.Manager.CheckAuthority("M312", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                this.kanyushaId = this.form.EDI_MEMBER_ID.Text;
                                this.jigyoujouCd = this.form.JIGYOUJOU_CD.Text;
                                this.form.WindowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                                this.form.HeaderFormInit();
                                catchErr = this.WindowInitReference((BusinessBaseForm)this.form.ParentForm);
                            }
                            else
                            {
                                msgLogic.MessageBoxShow("E158", "修正");
                                this.form.EDI_MEMBER_ID.Text = string.Empty;
                                this.form.EDI_MEMBER_ID.Focus();
                            }
                            if (catchErr)
                            {
                                LogUtility.DebugMethodEnd(true);
                                return true;
                            }
                        }
                        else
                        {
                            this.form.JIGYOUJOU_CD.Focus();
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeJigyoujouCD", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeJigyoujouCD", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
 
        //<summary>
        //ボタン設定の読込
        //</summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
 

        //<summary>
        //検索条件初期化
        //</summary>
        private void ClearCondition()
        {
            this.form.JIGYOUSHA_KBN.Text = string.Empty;
            this.form.EDI_MEMBER_ID.Text = string.Empty;
            this.form.JIGYOUSHA_NAME.Text = string.Empty;
            this.form.JIGYOUJOU_CD.Text = string.Empty;
            this.form.JIGYOUJOU_NAME.Text = string.Empty;
            this.form.JIGYOUJOU_POST.Text = string.Empty;
            this.form.JIGYOUJOU_ADDRESS1.Text = string.Empty;
            this.form.JIGYOUJOU_ADDRESS2.Text = string.Empty;
            this.form.JIGYOUJOU_ADDRESS3.Text = string.Empty;
            this.form.JIGYOUJOU_ADDRESS4.Text = string.Empty;
            this.form.JIGYOUJOU_TEL.Text = string.Empty;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME.Text = string.Empty;
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME.Text = string.Empty;
            this.form.JWNET_JIGYOUJOU_CD.Text = string.Empty;
        }

        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        internal bool ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                r_framework.FormManager.FormManager.OpenFormWithAuth("M313", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUJO);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShowIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool RegistCheck(object sender, r_framework.Event.RegistCheckEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool result = false;
                MessageUtility msgUtil = new MessageUtility();

                // 加入者番号のチェックを行う
                var isControl = sender is Control;
                if (isControl && ((Control)sender).Name.Equals("EDI_MEMBER_ID"))
                {
                    string id = ((Control)sender).Text;
                    if (!this.HanSuujiCheck(id))
                    {
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E140").MESSAGE, this.form.EDI_MEMBER_ID.DisplayItemName));
                    }
                    else
                    {
                        result = true;
                    }
                }
                else if (isControl && ((Control)sender).Name.Equals("JIGYOUJOU_CD"))
                {
                    string cd = ((Control)sender).Text;
                    int num;
                    bool parseResult = Int32.TryParse(cd, out num);
                    if (parseResult)
                    {
                        if (this.form.JIGYOUSHA_KBN.Text == "2" || this.form.JIGYOUSHA_KBN.Text == "3")
                        {
                            string jkb = "";
                            if (this.form.JIGYOUSHA_KBN.Text == "2")
                            {
                                jkb = "収集運搬業者";
                            }
                            else
                            {
                                jkb = "処分事業者";
                            }

                            if (num < 1 || num > 999)
                            {
                                e.errorMessages.Add(string.Format(msgUtil.GetMessage("E168").MESSAGE, jkb));
                                this.form.JIGYOUJOU_CD.Focus();
                            }
                            else
                            {
                                result = true;
                            }
                        }
                        else
                        {
                            result = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(result);
                return result;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("RegistCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// 半角数字形式かチェックする
        /// </summary>
        internal bool HanSuujiCheck(string value)
        {
            var result = true;

            if (string.IsNullOrEmpty(value))
            {
                return result;
            }

            // 半角数字形式か調べる
            result = System.Text.RegularExpressions.Regex.IsMatch(
                value,
                @"^[0-9]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            return result;
        }

        #region 加入者番号のポップアップ設定
        /// <summary>
        /// 加入者番号のポップアップ設定
        /// </summary>
        internal void InitPopupPropertyForEDI_MEMBER_ID()
        {
            this.form.EDI_MEMBER_ID.PopupWindowId = WINDOW_ID.M_DENSHI_JIGYOUSHA;
            this.form.EDI_MEMBER_ID.PopupWindowName = "検索共通ポップアップ";
            this.form.EDI_MEMBER_ID.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.EDI_MEMBER_ID.PopupSetFormField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.form.EDI_MEMBER_ID.PopupDataHeaderTitle = new string[] { "加入者番号", "事業者名" };
            this.form.EDI_MEMBER_ID.PopupAfterExecuteMethod = "EDI_MEMBER_ID_AfterPopup";
            // PopupDataSourceは事業者区分の変更時に設定
        }

        private string sqlStrForEDI_MEMBER_IDPooup = "SELECT * FROM dbo.M_DENSHI_JIGYOUSHA";

        /// <summary>
        /// EDI_MEMBER_ID用のポップアップ表示データの作成
        /// </summary>
        /// <returns></returns>
        internal DataTable GetPopupDispDataForEDI_MEMBER_ID(out bool catchErr)
        {
            try
            {
                catchErr = false;
                // 条件設定
                string whereStr = string.Empty;
                PopupSearchSendParamDto dto = new PopupSearchSendParamDto();
                dto.And_Or = CONDITION_OPERATOR.AND;
                switch (this.form.JIGYOUSHA_KBN.Text)
                {
                    case "1":
                        whereStr = " where HST_KBN = 1";
                        dto.KeyName = "HST_KBN";
                        dto.Value = "1";
                        break;

                    case "2":
                        whereStr = " where UPN_KBN = 1";
                        dto.KeyName = "UPN_KBN";
                        dto.Value = "1";
                        break;

                    case "3":
                        whereStr = " where SBN_KBN = 1";
                        dto.KeyName = "SBN_KBN";
                        dto.Value = "1";
                        break;

                    default:
                        break;

                }

                // ポップアップでの絞り込み条件設定
                this.form.EDI_MEMBER_ID.PopupSearchSendParams.Clear();
                this.form.EDI_MEMBER_ID.PopupSearchSendParams.Add(dto);

                // データ取得
                var denshiJigyoujou = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>().GetDateForStringSql(sqlStrForEDI_MEMBER_IDPooup + whereStr);

                var dispCol = this.form.EDI_MEMBER_ID.PopupGetMasterField.Split(',');
                DataTable returnTable = new DataTable();
                foreach (var col in dispCol)
                {
                    var trimColName = col.Trim();
                    returnTable.Columns.Add(denshiJigyoujou.Columns[trimColName].ColumnName, denshiJigyoujou.Columns[trimColName].DataType);
                }

                foreach (DataRow row in denshiJigyoujou.Rows)
                {
                    returnTable.Rows.Add(returnTable.Columns.OfType<DataColumn>().Select(s => row[s.ColumnName]).ToArray());
                }

                return returnTable;
            }
            catch (SQLRuntimeException ex2)
            {
                catchErr = true;
                LogUtility.Error("GetPopupDispDataForEDI_MEMBER_ID", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return new DataTable();
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("GetPopupDispDataForEDI_MEMBER_ID", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return new DataTable();
            }
        }
        #endregion

        /// <summary>
        /// 現場チェック
        /// </summary>
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
                    return true;
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
                    return true;
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GenbaValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 登録時の都道府県、市区町村の存在チェック
        /// </summary>
        /// <returns>true：正常　false：エラー</returns>
        internal bool TodoufukenSikuchousonCheck()
        {
            S_ZIP_CODE[] result = this.zipCodeDao.GetDataByTdkScsSearch(this.form.JIGYOUJOU_ADDRESS1.Text, this.form.JIGYOUJOU_ADDRESS2.Text);
            if (result.Length == 0)
            {
                this.form.JIGYOUJOU_ADDRESS1.IsInputErrorOccured = true;
                this.form.JIGYOUJOU_ADDRESS2.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShowError("都道府県、または市区町村が正しくありません。\n（市区町村の検索を行い正しい住所を入力してください）");
                this.form.JIGYOUJOU_ADDRESS1.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 郵便番号の存在チェック
        /// </summary>
        internal void JigyoujouPostCheck()
        {
            if (string.IsNullOrEmpty(this.form.JIGYOUJOU_POST.Text))
            {
                return;
            }

            // 郵便番号　前方一致検索
            S_ZIP_CODE[] zipCodeArray = this.zipCodeDao.GetDataByPost7LikeSearch(this.form.JIGYOUJOU_POST.Text + "%");

            if (zipCodeArray.Length == 0)
            {
                this.form.JIGYOUJOU_POST.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShowError("入力された郵便番号は、マスタに登録されておりません。");
                this.form.JIGYOUJOU_POST.Focus();
                return;
            }
            else if (zipCodeArray.Length == 1)
            {
                S_ZIP_CODE entity = zipCodeArray.First();

                this.form.JIGYOUJOU_ADDRESS1.Text = entity.TODOUFUKEN;
                this.form.JIGYOUJOU_ADDRESS2.Text = entity.SIKUCHOUSON;
                this.form.JIGYOUJOU_ADDRESS3.Text = entity.OTHER1;
                this.form.JIGYOUJOU_ADDRESS4.Clear();

                // 町域のフォーカスアウト処理を呼び出すため（最大文字数チェック）
                this.form.JIGYOUJOU_ADDRESS3.Focus();
            }
            else if (1 < zipCodeArray.Length)
            {
                // 住所検索ポップアップ表示
                var assembltyName = ASSEMBLY_NAME + ".dll";

                var m = Assembly.LoadFrom(assembltyName);
                var objectHandler = Activator.CreateInstanceFrom(m.CodeBase, ASSEMBLY_NAME + "." + CALASS_NAME_SPACE);
                using (var classinfo = objectHandler.Unwrap() as SuperPopupForm)
                {
                    if (classinfo != null)
                    {
                        // 検索結果を設定
                        classinfo.Params = new object[1] { zipCodeArray };

                        classinfo.ShowDialog();

                        if (classinfo.ReturnParams != null)
                        {
                            List<PopupReturnParam> returnParamList = classinfo.ReturnParams[0];

                            this.form.JIGYOUJOU_ADDRESS1.Text = returnParamList[1].Value.ToString();
                            this.form.JIGYOUJOU_ADDRESS2.Text = returnParamList[2].Value.ToString();
                            this.form.JIGYOUJOU_ADDRESS3.Text = returnParamList[3].Value.ToString();
                            this.form.JIGYOUJOU_ADDRESS4.Clear();
                            
                            // 町域のフォーカスアウト処理を呼び出すため（最大文字数チェック）
                            this.form.JIGYOUJOU_ADDRESS3.Focus();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 都道府県、市区町村の存在チェック
        /// </summary>
        internal void TodoufukenSikuchousonLikeCheck()
        {
            if (string.IsNullOrEmpty(this.form.JIGYOUJOU_ADDRESS1.Text) || string.IsNullOrEmpty(this.form.JIGYOUJOU_ADDRESS2.Text))
            {
                this.form.JIGYOUJOU_ADDRESS1.IsInputErrorOccured = true;
                this.form.JIGYOUJOU_ADDRESS2.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShowError("都道府県、市区町村のどちらも入力を行ってください。\n（この検索機能は、住所の補正機能となっております）");
                this.form.JIGYOUJOU_ADDRESS1.Focus();
                return;
            }

            // 郵便番号　前方一致検索
            S_ZIP_CODE[] result = this.zipCodeDao.GetDataByTdkScsLikeSearch(this.form.JIGYOUJOU_ADDRESS1.Text, "%" + this.form.JIGYOUJOU_ADDRESS2.Text + "%");
            if (result.Length == 0)
            {
                this.form.errmessage.MessageBoxShowInformation("該当する市区町村の取得が行えませんでした。\n市区町村の情報を変更し再検索を行ってください。");
                return;
            }
            else if (0 < result.Length)
            {
                // 住所検索ポップアップ表示
                var assembltyName = ASSEMBLY_NAME + ".dll";

                var m = Assembly.LoadFrom(assembltyName);
                var objectHandler = Activator.CreateInstanceFrom(m.CodeBase, ASSEMBLY_NAME + "." + CALASS_NAME_SPACE);
                using (var classinfo = objectHandler.Unwrap() as SuperPopupForm)
                {
                    if (classinfo != null)
                    {
                        // 検索結果を設定
                        classinfo.Params = new object[1] { result };

                        classinfo.ShowDialog();

                        if (classinfo.ReturnParams != null)
                        {
                            List<PopupReturnParam> returnParamList = classinfo.ReturnParams[0];

                            this.form.JIGYOUJOU_POST.Text = returnParamList[0].Value.ToString();
                            this.form.JIGYOUJOU_ADDRESS2.Text = returnParamList[2].Value.ToString();
                            this.form.JIGYOUJOU_ADDRESS3.Text = returnParamList[3].Value.ToString();
                            this.form.JIGYOUJOU_ADDRESS4.Clear();

                            // 町域のフォーカスアウト処理を呼び出すため（最大文字数チェック）
                            this.form.JIGYOUJOU_ADDRESS3.Focus();
                        }
                    }
                }
            }
        }
    }
}