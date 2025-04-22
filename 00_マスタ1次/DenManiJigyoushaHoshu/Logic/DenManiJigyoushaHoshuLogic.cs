// $Id: DenManiJigyoushaHoshuLogic.cs 53608 2015-06-26 00:09:57Z miya@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DenManiJigyoushaHoshu.APP;
using MasterCommon.Logic;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Event;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace DenManiJigyoushaHoshu.Logic
{
    /// <summary>
    /// 事業者入力のビジネスロジック
    /// </summary>
    public class DenManiJigyoushaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "DenManiJigyoushaHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_JIGYOUSHA_DATA_BY_CD_SQL = "DenManiJigyoushaHoshu.Sql.GetJigyoushaDataByCdSql.sql";

        private readonly string CHECK_DELETE_HAIKI_SHURUI_SQL = "DenManiJigyoushaHoshu.Sql.CheckDeleteJigyoushaSql.sql";

        private static readonly string ASSEMBLY_NAME = "JushoKensakuPopup2";

        private static readonly string CALASS_NAME_SPACE = "APP.JushoKensakuPopupForm2";

        /// <summary>
        /// 事業者入力Form
        /// </summary>
        private DenManiJigyoushaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 電子事業者マスタのentity
        /// </summary>
        private M_DENSHI_JIGYOUSHA entitys;

        /// <summary>
        /// 業者マスタのentity
        /// </summary>
        private M_GYOUSHA gyoushaEntity;

        /// <summary>
        /// 電子事業者マスタのtable
        /// </summary>
        private DataTable table;

        /// <summary>
        /// 電子事業者マスタのdao
        /// </summary>
        private IM_DENSHI_JIGYOUSHADao dao;

        /// <summary>
        /// 業者マスタのdao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// MS_JWNET_MEMBERのdao
        /// </summary>
        private IMS_JWNET_MEMBERDao jwnetMemberDao;

        /// <summary>
        /// 郵便辞書マスタのDao
        /// </summary>
        private IS_ZIP_CODEDao zipCodeDao;

        /// <summary>
        /// MasterCommon格納用
        /// </summary>
        private Type CommonType;
        private object CommonInstance;


        #endregion

        #region プロパティ

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_DENSHI_JIGYOUSHA SearchString { get; set; }

        /// <summary>
        /// 加入者番号
        /// </summary>
        public string kanyushaId { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public DenManiJigyoushaHoshuLogic(DenManiJigyoushaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.jwnetMemberDao = DaoInitUtility.GetComponent<IMS_JWNET_MEMBERDao>();
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

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);
                this.allControl = this.form.allControl;

                //MasterCommonへのインスタンスを生成しておく
                var CommonAssembly = Assembly.LoadFrom("MasterCommon.dll");
                this.CommonType = CommonAssembly.GetType("MasterCommon.Logic.MasterCommonLogic");
                this.CommonInstance = this.CommonType.InvokeMember(null, BindingFlags.CreateInstance, null, null, null);

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
            if (string.IsNullOrEmpty(this.kanyushaId))
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
                this.Search();

                // 検索結果を画面に設定
                this.SetWindowData();

                // 修正モード固有UI設定
                this.form.EDI_MEMBER_ID.Enabled = false;   // 加入者CD

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                this.form.EDI_PASSWORD.Focus();
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
            this.SetWindowData();

            // 削除モード固有UI設定
            this.form.EDI_MEMBER_ID.Enabled = false;   // 加入者CD

            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = true;     // 修正
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = false;    // 取消

            this.form.EDI_PASSWORD.Focus();
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
                this.SetWindowData();

                // 参照モード固有UI設定
                this.form.EDI_MEMBER_ID.Enabled = false;   // 加入者CD

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func9.Enabled = false;    // 登録
                parentForm.bt_func11.Enabled = false;   // 取消

                this.form.EDI_PASSWORD.Focus();
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
                this.form.EDI_MEMBER_ID.Text = string.Empty;
                this.form.EDI_PASSWORD.Text = string.Empty;
                this.form.JIGYOUSHA_NAME.Text = string.Empty;
                this.form.JIGYOUSHA_POST.Text = string.Empty;
                this.form.JIGYOUSHA_ADDRESS1.Text = string.Empty;
                this.form.JIGYOUSHA_ADDRESS2.Text = string.Empty;
                this.form.JIGYOUSHA_ADDRESS3.Text = string.Empty;
                this.form.JIGYOUSHA_ADDRESS4.Text = string.Empty;
                this.form.JIGYOUSHA_TEL.Text = string.Empty;
                this.form.JIGYOUSHA_FAX.Text = string.Empty;
                this.form.HST_KBN.Checked = false;
                this.form.UPN_KBN.Checked = false;
                this.form.SBN_KBN.Checked = false;

                this.form.HOUKOKU_HUYOU_KBN.Checked = false;

                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME.Text = string.Empty;

                // functionボタン
                parentForm.bt_func2.Enabled = true;     // 新規
                parentForm.bt_func3.Enabled = false;    // 修正
                parentForm.bt_func7.Enabled = true;     // 一覧
                parentForm.bt_func9.Enabled = true;     // 登録
                parentForm.bt_func11.Enabled = true;    // 取消

                this.form.EDI_MEMBER_ID.Focus();
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
            this.Search();

            // 検索結果を画面に設定
            this.SetWindowData();

            // 複写モード固有UI設定
            DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = string.Empty;
            header.CreateUser.Text = string.Empty;
            header.LastUpdateDate.Text = string.Empty;
            header.LastUpdateUser.Text = string.Empty;
            this.form.EDI_MEMBER_ID.Text = string.Empty;
            this.form.EDI_PASSWORD.Text = string.Empty;
            
            // functionボタン
            parentForm.bt_func2.Enabled = true;     // 新規
            parentForm.bt_func3.Enabled = false;    // 修正
            parentForm.bt_func7.Enabled = true;     // 新規
            parentForm.bt_func9.Enabled = true;     // 登録
            parentForm.bt_func11.Enabled = true;    // 取消

            this.form.EDI_MEMBER_ID.Focus();
        }

        /// <summary>
        /// データを取得し、画面に設定
        /// </summary>
        private void SetWindowData()
        {
            // ヘッダー項目
            DetailedHeaderForm header = (DetailedHeaderForm)((BusinessBaseForm)this.form.ParentForm).headerForm;
            header.CreateDate.Text = this.entitys.CREATE_DATE.ToString();
            header.CreateUser.Text = this.entitys.CREATE_USER;
            header.LastUpdateDate.Text = this.entitys.UPDATE_DATE.ToString();
            header.LastUpdateUser.Text = this.entitys.UPDATE_USER;

            this.form.EDI_MEMBER_ID.Text = this.entitys.EDI_MEMBER_ID;
            this.form.EDI_PASSWORD.Text = this.entitys.EDI_PASSWORD;
            this.form.JIGYOUSHA_NAME.Text = this.entitys.JIGYOUSHA_NAME;
            this.form.JIGYOUSHA_POST.Text = this.entitys.JIGYOUSHA_POST;
            this.form.JIGYOUSHA_ADDRESS1.Text = this.entitys.JIGYOUSHA_ADDRESS1;
            this.form.JIGYOUSHA_ADDRESS2.Text = this.entitys.JIGYOUSHA_ADDRESS2;
            this.form.JIGYOUSHA_ADDRESS3.Text = this.entitys.JIGYOUSHA_ADDRESS3;
            this.form.JIGYOUSHA_ADDRESS4.Text = this.entitys.JIGYOUSHA_ADDRESS4;
            this.form.JIGYOUSHA_TEL.Text = this.entitys.JIGYOUSHA_TEL;
            this.form.JIGYOUSHA_FAX.Text = this.entitys.JIGYOUSHA_FAX;
            this.form.HST_KBN.Checked = (bool)this.entitys.HST_KBN;
            this.form.UPN_KBN.Checked = (bool)this.entitys.UPN_KBN;
            this.form.SBN_KBN.Checked = (bool)this.entitys.SBN_KBN;
            this.form.HOUKOKU_HUYOU_KBN.Checked = (bool)this.entitys.HOUKOKU_HUYOU_KBN;
            this.form.GYOUSHA_CD.Text = this.entitys.GYOUSHA_CD;
            this.form.GYOUSHA_NAME.Text = string.Empty;
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                this.gyoushaEntity = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                if (this.gyoushaEntity != null)
                {
                    this.form.GYOUSHA_NAME.Text = this.gyoushaEntity.GYOUSHA_NAME_RYAKU;
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
            this.form.EDI_MEMBER_ID.Enabled = !isBool;
            this.form.EDI_PASSWORD.ReadOnly = isBool;
            this.form.JIGYOUSHA_NAME.ReadOnly = isBool;
            this.form.JIGYOUSHA_POST.ReadOnly = isBool;
            this.form.JIGYOUSHA_ADDRESS1.ReadOnly = isBool;
            this.form.JIGYOUSHA_ADDRESS2.ReadOnly = isBool;
            this.form.JIGYOUSHA_ADDRESS3.ReadOnly = isBool;
            this.form.JIGYOUSHA_ADDRESS4.ReadOnly = isBool;
            this.form.JIGYOUSHA_TEL.ReadOnly = isBool;
            this.form.JIGYOUSHA_FAX.ReadOnly = isBool;

            this.form.HST_KBN.Enabled = !isBool;
            this.form.UPN_KBN.Enabled = !isBool;
            this.form.SBN_KBN.Enabled = !isBool;

            this.form.HOUKOKU_HUYOU_KBN.Enabled = !isBool;

            this.form.GYOUSHA_CD.Enabled = !isBool;
            this.form.GYOUSHA_SEARCH_BUTTON.Enabled = !isBool;

            this.form.JIGYOUSHA_POST_SEACRH_BUTTON.Enabled = !isBool;
            this.form.SIKUCHOUSON_SEARCH_BUTTON.Enabled  =!isBool;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int count;

            this.entitys = new M_DENSHI_JIGYOUSHA();
            this.entitys.EDI_MEMBER_ID = this.kanyushaId;
            this.table = this.dao.GetDataBySqlFile(GET_JIGYOUSHA_DATA_BY_CD_SQL, entitys);
            foreach (DataRow row in table.Rows)
            {
                this.entitys.EDI_MEMBER_ID = row["EDI_MEMBER_ID"].ToString();
                this.entitys.EDI_PASSWORD = row["EDI_PASSWORD"].ToString();
                this.entitys.JIGYOUSHA_NAME = row["JIGYOUSHA_NAME"].ToString();
                this.entitys.JIGYOUSHA_POST = row["JIGYOUSHA_POST"].ToString();
                this.entitys.JIGYOUSHA_ADDRESS1 = row["JIGYOUSHA_ADDRESS1"].ToString();
                this.entitys.JIGYOUSHA_ADDRESS2 = row["JIGYOUSHA_ADDRESS2"].ToString();
                this.entitys.JIGYOUSHA_ADDRESS3 = row["JIGYOUSHA_ADDRESS3"].ToString();
                this.entitys.JIGYOUSHA_ADDRESS4 = row["JIGYOUSHA_ADDRESS4"].ToString();
                this.entitys.JIGYOUSHA_TEL = row["JIGYOUSHA_TEL"].ToString();
                this.entitys.JIGYOUSHA_FAX = row["JIGYOUSHA_FAX"].ToString();
                this.entitys.HST_KBN = (bool)row["HST_KBN"];
                this.entitys.UPN_KBN = (bool)row["UPN_KBN"];
                this.entitys.SBN_KBN = (bool)row["SBN_KBN"];
                this.entitys.HOUKOKU_HUYOU_KBN = (bool)row["HOUKOKU_HUYOU_KBN"];
                this.entitys.GYOUSHA_CD = row["GYOUSHA_CD"].ToString();
                this.entitys.CREATE_USER = row["CREATE_USER"].ToString();
                this.entitys.CREATE_DATE = (DateTime)row["CREATE_DATE"];
                this.entitys.CREATE_PC = row["CREATE_PC"].ToString();
                this.entitys.UPDATE_USER = row["UPDATE_USER"].ToString();
                this.entitys.UPDATE_DATE = (DateTime)row["UPDATE_DATE"];
                this.entitys.UPDATE_PC = row["UPDATE_PC"].ToString();
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
                this.entitys.EDI_PASSWORD = this.form.EDI_PASSWORD.Text;
                this.entitys.JIGYOUSHA_NAME = this.form.JIGYOUSHA_NAME.Text;
                this.entitys.JIGYOUSHA_POST = this.form.JIGYOUSHA_POST.Text;
                this.entitys.JIGYOUSHA_ADDRESS1 = this.form.JIGYOUSHA_ADDRESS1.Text;
                this.entitys.JIGYOUSHA_ADDRESS2 = this.form.JIGYOUSHA_ADDRESS2.Text;
                this.entitys.JIGYOUSHA_ADDRESS3 = this.form.JIGYOUSHA_ADDRESS3.Text;
                this.entitys.JIGYOUSHA_ADDRESS4 = this.form.JIGYOUSHA_ADDRESS4.Text;
                this.entitys.JIGYOUSHA_TEL = this.form.JIGYOUSHA_TEL.Text;
                this.entitys.JIGYOUSHA_FAX = this.form.JIGYOUSHA_FAX.Text;
                this.entitys.HST_KBN = this.form.HST_KBN.Checked;
                this.entitys.UPN_KBN = this.form.UPN_KBN.Checked;
                this.entitys.SBN_KBN = this.form.SBN_KBN.Checked;
                this.entitys.HOUKOKU_HUYOU_KBN = this.form.HOUKOKU_HUYOU_KBN.Checked;
                this.entitys.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;

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

                if (form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
                {
                    catchErr = this.WindowInitUpdate((BusinessBaseForm)this.form.ParentForm);
                }

                else
                {
                    ClearCondition();
                    SetSearchString();
                    this.form.WindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;
                    catchErr = this.WindowInitNewMode((BusinessBaseForm)this.form.ParentForm);
                }

                LogUtility.DebugMethodEnd(false);
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
   

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();

            LogUtility.DebugMethodEnd();
        }

        #region 登録/更新/削除
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void  Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                //独自チェックの記述例を書く
                Boolean flg;

                flg = false;

                if (!errorFlag)
                {
                    table = this.dao.GetDataBySqlFile(GET_JIGYOUSHA_DATA_BY_CD_SQL, entitys);

                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_JIGYOUSHA>(entitys);
                    dataBinderLogic.SetSystemProperty(entitys, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), entitys);

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            if (entitys.EDI_MEMBER_ID == (string)row["EDI_MEMBER_ID"])
                            {
                                this.dao.Update(entitys);
                                flg = true;
                            }
                        }

                        if (flg == false)
                        {
                            this.dao.Insert(entitys);
                        }

                        // MS_JWNET_MEMBERへ登録 or 削除を行う
                        var jwnetMemberEnt = new MS_JWNET_MEMBER();
                        jwnetMemberEnt.EDI_MEMBER_ID = entitys.EDI_MEMBER_ID;
                        jwnetMemberEnt.EDI_PASSWORD = entitys.EDI_PASSWORD;
                        var search = this.jwnetMemberDao.GetDataByCd(entitys.EDI_MEMBER_ID);
                        if (!string.IsNullOrEmpty(entitys.EDI_MEMBER_ID) && !string.IsNullOrEmpty(entitys.EDI_PASSWORD))
                        {
                            if (search == null)
                            {
                                this.jwnetMemberDao.Insert(jwnetMemberEnt);
                            }
                            else
                            {
                                this.jwnetMemberDao.Update(jwnetMemberEnt);
                            }
                        }
                        else if (search != null)
                        {
                            this.jwnetMemberDao.Delete(jwnetMemberEnt);
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    this.isRegist = true;

                }

                //this.dao.Update(entitys);

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
                    table = this.dao.GetDataBySqlFile(GET_JIGYOUSHA_DATA_BY_CD_SQL, entitys);

                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_JIGYOUSHA>(entitys);
                    dataBinderLogic.SetSystemProperty(entitys, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), entitys);

                    foreach (DataRow row in table.Rows)
                    {
                        entitys.EDI_MEMBER_ID = (string)row["EDI_MEMBER_ID"];
                        // 20150825 TIME_STAMP対応 Start
                        entitys.TIME_STAMP = (byte[])row["TIME_STAMP"];
                        // 20150825 TIME_STAMP対応 End
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

                        // MS_JWNET_MEMBERへ登録 or 削除を行う
                        var jwnetMemberEnt = new MS_JWNET_MEMBER();
                        jwnetMemberEnt.EDI_MEMBER_ID = entitys.EDI_MEMBER_ID;
                        jwnetMemberEnt.EDI_PASSWORD = entitys.EDI_PASSWORD;
                        var search = this.jwnetMemberDao.GetDataByCd(entitys.EDI_MEMBER_ID);
                        if (!string.IsNullOrEmpty(entitys.EDI_MEMBER_ID) && !string.IsNullOrEmpty(entitys.EDI_PASSWORD))
                        {
                            if (search == null)
                            {
                                this.jwnetMemberDao.Insert(jwnetMemberEnt);
                            }
                            else
                            {
                                this.jwnetMemberDao.Update(jwnetMemberEnt);
                            }
                        }
                        else if (search != null)
                        {
                            this.jwnetMemberDao.Delete(jwnetMemberEnt);
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

                if (!this.CheckDelete())
                {
                    LogUtility.DebugMethodEnd();
                    return;
                }

                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    table = this.dao.GetDataBySqlFile(GET_JIGYOUSHA_DATA_BY_CD_SQL, entitys);

                    var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_JIGYOUSHA>(entitys);
                    dataBinderLogic.SetSystemProperty(entitys, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), entitys);

                    foreach (DataRow row in table.Rows)
                    {
                        entitys.EDI_MEMBER_ID = (string)row["EDI_MEMBER_ID"];
                        entitys.TIME_STAMP = (byte[])row["TIME_STAMP"];
                    }

                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        this.dao.Delete(entitys);

                        // MS_JWNET_MEMBERの削除を行う
                        var jwnetMemberEnt = new MS_JWNET_MEMBER();
                        jwnetMemberEnt.EDI_MEMBER_ID = entitys.EDI_MEMBER_ID;
                        jwnetMemberEnt.EDI_PASSWORD = entitys.EDI_PASSWORD;
                        var search = this.jwnetMemberDao.GetDataByCd(entitys.EDI_MEMBER_ID);
                        if (search != null)
                        {
                            this.jwnetMemberDao.Delete(jwnetMemberEnt);
                        }
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
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var cd = entitys.EDI_MEMBER_ID;

                if (!string.IsNullOrEmpty(cd))
                {
                    DataTable dtTable = this.dao.GetDataBySqlFileCheck(this.CHECK_DELETE_HAIKI_SHURUI_SQL, cd);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        new MessageBoxShowLogic().MessageBoxShow("E258", "電子事業者", "加入者番号", strName);

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

                    // MS_JWNET_MEMBERの削除を行う
                    var jwnetMemberEnt = new MS_JWNET_MEMBER();
                    jwnetMemberEnt.EDI_MEMBER_ID = entitys.EDI_MEMBER_ID;
                    jwnetMemberEnt.EDI_PASSWORD = entitys.EDI_PASSWORD;
                    var search = this.jwnetMemberDao.GetDataByCd(entitys.EDI_MEMBER_ID);
                    if (search != null)
                    {
                        this.jwnetMemberDao.Delete(jwnetMemberEnt);
                    }
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

            DenManiJigyoushaHoshuLogic localLogic = other as DenManiJigyoushaHoshuLogic;
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
        private void SetSearchString()
        {
            M_DENSHI_JIGYOUSHA entity = new M_DENSHI_JIGYOUSHA();

            if (!string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.DBFieldsName)
                && !this.form.EDI_MEMBER_ID.DBFieldsName.Equals(this.form.EDI_MEMBER_ID.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
                {
                    // 検索条件の設定
                    entity.SetValue(this.form.EDI_MEMBER_ID);
                }
            }
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
            parentForm.bt_func9.Click += new EventHandler(this.RegistBefore);
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.Click += new EventHandler(this.RegistAfter);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取り消し(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
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
            this.form.EDI_MEMBER_ID.Text = string.Empty;
            this.form.EDI_PASSWORD.Text = string.Empty;
            this.form.JIGYOUSHA_NAME.Text = string.Empty;
            this.form.JIGYOUSHA_POST.Text = string.Empty;
            this.form.JIGYOUSHA_ADDRESS1.Text = string.Empty;
            this.form.JIGYOUSHA_ADDRESS2.Text = string.Empty;
            this.form.JIGYOUSHA_ADDRESS3.Text = string.Empty;
            this.form.JIGYOUSHA_ADDRESS4.Text = string.Empty;
            this.form.JIGYOUSHA_TEL.Text = string.Empty;
            this.form.HST_KBN.Checked = false;
            this.form.UPN_KBN.Checked = false;
            this.form.SBN_KBN.Checked = false;
            this.form.HOUKOKU_HUYOU_KBN.Checked = false;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME.Text = string.Empty;
        }

        /// <summary>
        /// 加入者ID変更後処理
        /// </summary>
        public bool ChangeKanyuushaID()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrEmpty(this.form.EDI_MEMBER_ID.Text))
                {
                    this.entitys = new M_DENSHI_JIGYOUSHA();
                    this.entitys.EDI_MEMBER_ID = this.form.EDI_MEMBER_ID.Text;
                    this.table = this.dao.GetDataBySqlFile(this.GET_JIGYOUSHA_DATA_BY_CD_SQL, this.entitys);

                    if (this.table.Rows.Count > 0)
                    {
                        bool catchErr = false;
                        DialogResult res = msgLogic.MessageBoxShow("C017");
                        if (res == DialogResult.Yes)
                        {
                            // 権限チェック
                            // 修正権限無し＆参照権限があるなら降格し、どちらもなければアラート
                            if (r_framework.Authority.Manager.CheckAuthority("M309", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                            {
                                this.kanyushaId = this.form.EDI_MEMBER_ID.Text;
                                this.form.WindowType = WINDOW_TYPE.UPDATE_WINDOW_FLAG;
                                this.form.HeaderFormInit();
                                catchErr = this.WindowInitUpdate((BusinessBaseForm)this.form.ParentForm);
                            }
                            else if (r_framework.Authority.Manager.CheckAuthority("M309", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                            {
                                this.kanyushaId = this.form.EDI_MEMBER_ID.Text;
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
                                return true;
                            }
                        }
                        else
                        {
                            this.form.EDI_MEMBER_ID.Text = string.Empty;
                            this.form.EDI_MEMBER_ID.Focus();
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("ChangeKanyuushaID", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeKanyuushaID", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 一覧画面表示処理
        /// </summary>
        internal bool ShowIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();

                r_framework.FormManager.FormManager.OpenFormWithAuth("M310", r_framework.Const.WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DENSHU_KBN.DENNSHI_MANIFEST_JIGYOUSHA);

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
        /// 登録処理の事前処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegistBefore(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 必須チェックを実施するため、加入者番号を一時的に有効とする
            this.form.EDI_MEMBER_ID.Enabled = true;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録処理の事後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegistAfter(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 事前処理の適用を選択されているモードに応じて設定する
            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.form.EDI_MEMBER_ID.Enabled = false;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool RegistCheck(object sender, RegistCheckEventArgs e)
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


                    if (id.Equals(""))
                    {
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E001").MESSAGE, this.form.EDI_MEMBER_ID.DisplayItemName));
                    }
                    else if (id.Equals(string.Format("{0:D" + this.form.EDI_MEMBER_ID.CharactersNumber + "}", 0)))
                    {
                        result = true;
                    }
                    else if (!this.HanSuujiCheck(id))
                    {
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E140").MESSAGE, this.form.EDI_MEMBER_ID.DisplayItemName));
                    }
                    else if (id.Substring(0, 1).Equals("1") || id.Substring(0, 1).Equals("2") || id.Substring(0, 1).Equals("3"))
                    {
                        result = true;
                    }
                    else
                    {
                        e.errorMessages.Add(string.Format(msgUtil.GetMessage("E082").MESSAGE, this.form.EDI_MEMBER_ID.DisplayItemName));
                    }
                }

                LogUtility.DebugMethodEnd(result);
                return result;
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

        /// <summary>
        /// 登録時の都道府県、市区町村の存在チェック
        /// </summary>
        /// <returns>true：正常　false：エラー</returns>
        internal bool TodoufukenSikuchousonCheck()
        {
            S_ZIP_CODE[] result = this.zipCodeDao.GetDataByTdkScsSearch(this.form.JIGYOUSHA_ADDRESS1.Text, this.form.JIGYOUSHA_ADDRESS2.Text);
            if (result.Length == 0)
            {
                this.form.JIGYOUSHA_ADDRESS1.IsInputErrorOccured = true;
                this.form.JIGYOUSHA_ADDRESS2.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShowError("都道府県、または市区町村が正しくありません。\n（市区町村の検索を行い正しい住所を入力してください）");
                this.form.JIGYOUSHA_ADDRESS1.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 郵便番号の存在チェック
        /// </summary>
        internal void JigyoushaPostCheck()
        {
            if (string.IsNullOrEmpty(this.form.JIGYOUSHA_POST.Text))
            {
                return;
            }

            // 郵便番号　前方一致検索
            S_ZIP_CODE[] zipCodeArray = this.zipCodeDao.GetDataByPost7LikeSearch(this.form.JIGYOUSHA_POST.Text + "%");

            if (zipCodeArray.Length == 0)
            {
                this.form.JIGYOUSHA_POST.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShowError("入力された郵便番号は、マスタに登録されておりません。");
                this.form.JIGYOUSHA_POST.Focus();
                return;
            }
            else if (zipCodeArray.Length == 1)
            {
                S_ZIP_CODE entity = zipCodeArray.First();

                this.form.JIGYOUSHA_ADDRESS1.Text = entity.TODOUFUKEN;
                this.form.JIGYOUSHA_ADDRESS2.Text = entity.SIKUCHOUSON;
                this.form.JIGYOUSHA_ADDRESS3.Text = entity.OTHER1;
                this.form.JIGYOUSHA_ADDRESS4.Clear();

                // 町域のフォーカスアウト処理を呼び出すため（最大文字数チェック）
                this.form.JIGYOUSHA_ADDRESS3.Focus();
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

                            this.form.JIGYOUSHA_ADDRESS1.Text = returnParamList[1].Value.ToString();
                            this.form.JIGYOUSHA_ADDRESS2.Text = returnParamList[2].Value.ToString();
                            this.form.JIGYOUSHA_ADDRESS3.Text = returnParamList[3].Value.ToString();
                            this.form.JIGYOUSHA_ADDRESS4.Clear();
                            
                            // 町域のフォーカスアウト処理を呼び出すため（最大文字数チェック）
                            this.form.JIGYOUSHA_ADDRESS3.Focus();
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
            if (string.IsNullOrEmpty(this.form.JIGYOUSHA_ADDRESS1.Text) || string.IsNullOrEmpty(this.form.JIGYOUSHA_ADDRESS2.Text))
            {
                this.form.JIGYOUSHA_ADDRESS1.IsInputErrorOccured = true;
                this.form.JIGYOUSHA_ADDRESS2.IsInputErrorOccured = true;
                this.form.errmessage.MessageBoxShowError("都道府県、市区町村のどちらも入力を行ってください。\n（この検索機能は、住所の補正機能となっております）");
                this.form.JIGYOUSHA_ADDRESS1.Focus();
                return;
            }

            // 郵便番号　前方一致検索
            S_ZIP_CODE[] result = this.zipCodeDao.GetDataByTdkScsLikeSearch(this.form.JIGYOUSHA_ADDRESS1.Text, "%" + this.form.JIGYOUSHA_ADDRESS2.Text + "%");
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

                            this.form.JIGYOUSHA_POST.Text = returnParamList[0].Value.ToString();
                            this.form.JIGYOUSHA_ADDRESS2.Text = returnParamList[2].Value.ToString();
                            this.form.JIGYOUSHA_ADDRESS3.Text = returnParamList[3].Value.ToString();
                            this.form.JIGYOUSHA_ADDRESS4.Clear();

                            // 町域のフォーカスアウト処理を呼び出すため（最大文字数チェック）
                            this.form.JIGYOUSHA_ADDRESS3.Focus();
                        }
                    }
                }
            }
        }
    }
}