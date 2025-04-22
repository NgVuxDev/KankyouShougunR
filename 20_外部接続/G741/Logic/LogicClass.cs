using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Configuration;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.ExternalConnection.GenbamemoNyuryoku.Const;
using Shougun.Core.ExternalConnection.GenbamemoNyuryoku;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.Message;

namespace Shougun.Core.ExternalConnection.GenbamemoNyuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.GenbamemoNyuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        private string mcreateSql { get; set; }

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable chiikiKyokaNoSearchResult { get; set; }

        /// <summary>
        /// 現場メモEntryDao
        /// </summary>
        public GenbamemoEntryDAO genbamemoEntryDAO;

        /// <summary>
        /// 現場メモDetailDao
        /// </summary>
        public GenbamemoDetailDAO genbamemoDetailDAO;

        /// <summary>
        /// システム設定
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 現場メモEntity
        /// </summary>
        public T_GENBAMEMO_ENTRY genbamemoEntry;

        /// <summary>
        /// 現場メモEntity(削除用)
        /// </summary>
        public T_GENBAMEMO_ENTRY delGenbamemoEntry;

        /// <summary>
        /// 現場メモ詳細Entity
        /// </summary>
        public List<T_GENBAMEMO_DETAIL> genbamemoDetailList;

        /// <summary>
        /// システム設定のEntity
        /// </summary>
        private M_SYS_INFO SysInfo;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private UIHeader HeaderForm;

        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false;

        /// <summary>
        /// IM_GYOUSHADao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// IM_GENBADao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// IM_TORIHIKISAKIDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// IM_GENBAMEMO_BUNRUIDao
        /// </summary>
        private IM_GENBAMEMO_BUNRUIDao genbamemoBunruiDao;

        /// <summary>
        /// IM_FILE_LINK_GENBAMEMO_ENTRYDao
        /// </summary>
        public IM_FILE_LINK_GENBAMEMO_ENTRYDao fileLinkGenbamemoDao;

        /// <summary>
        /// 削除用SEQ
        /// </summary>
        private string delSEQ;

        /// <summary>
        /// 発生元CD(前回値)
        /// </summary>
        public string beforeHsseimotoCd;

        /// <summary>
        /// 発生元番号(前回値)
        /// </summary>
        public string beforeHsseimotoNumber;

        /// <summary>
        /// 発生元明細(前回値)
        /// </summary>
        public string beforeHsseimotoMeisaiNumber;

        #endregion

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int result = 0;
            try
            {
                LogUtility.DebugMethodStart();

                // 入力データを検索
                this.genbamemoEntry = this.genbamemoEntryDAO.GetDataByGenbamemoNumber(this.form.GenbamemoNumber);
                // 件数
                if (this.genbamemoEntry != null)
                {
                    result = 1;
                }
                // 0件の場合
                else
                {
                    // 処理終了
                    return result;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                result = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                result = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }

            return result;
        }

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">UIForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            // メッセージ用
            this.msgLogic = new MessageBoxShowLogic();

            this.genbamemoEntryDAO = DaoInitUtility.GetComponent<GenbamemoEntryDAO>();
            this.genbamemoDetailDAO = DaoInitUtility.GetComponent<GenbamemoDetailDAO>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_TORIHIKISAKIDao>();
            this.genbamemoBunruiDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBAMEMO_BUNRUIDao>();
            this.fileLinkGenbamemoDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_FILE_LINK_GENBAMEMO_ENTRYDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;

            try
            {
                LogUtility.DebugMethodStart();

                //ボタンのテキストを初期化
                this.ButtonInit();

                //ボタン制御
                this.ButtonEnabledControl();

                //イベントの初期化処理
                this.EventInit();

                // キー入力設定
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.parentForm.headerForm;

                // システム設定を取得する。
                this.SysInfo = this.daoSysInfo.GetAllDataForCode("0");

                // ファイルアップロードボタン制御
                this.FileUploadButtonSetting();

                // 表題欄のイベント設定
                this.SetHyoudaiEvent();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }

        /// <summary>
        /// 表題欄のイベント設定
        /// </summary>
        public void SetHyoudaiEvent()
        {
            this.form.HYOUDAI.PopupGetMasterField = this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.PopupGetMasterField;
            this.form.HYOUDAI.PopupSetFormField = this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.PopupSetFormField;
            this.form.HYOUDAI.PopupMultiSelect = this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.PopupMultiSelect;
            this.form.HYOUDAI.PopupSearchSendParams = this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.PopupSearchSendParams;
            this.form.HYOUDAI.PopupWindowId = this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.PopupWindowId;
            this.form.HYOUDAI.PopupWindowName = this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.PopupWindowName;
            this.form.HYOUDAI.popupWindowSetting = this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.popupWindowSetting;
        }

        /// <summary>
        /// 表題欄のイベント削除
        /// </summary>
        public void DeleteHyoudaiEvent()
        {
            this.form.HYOUDAI.PopupGetMasterField = null;
            this.form.HYOUDAI.PopupSetFormField = null;
            this.form.HYOUDAI.PopupMultiSelect = false;
            this.form.HYOUDAI.PopupSearchSendParams = null;
            this.form.HYOUDAI.PopupWindowId = WINDOW_ID.NONE;
            this.form.HYOUDAI.PopupWindowName = null;
            this.form.HYOUDAI.popupWindowSetting = null;
        }

        /// <summary>
        /// ボタン設定の読み込み
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (!AppConfig.AppOptions.IsFileUploadGenbaMemo())
            {
                foreach (var button in buttonSetting)
                {
                    if (button.ButtonName == "bt_process1")
                    {
                        button.NewButtonName = string.Empty;
                        button.UpdateButtonName = string.Empty;
                        button.ReferButtonName = string.Empty;
                        button.DeleteButtonName = string.Empty;
                        button.DefaultHintText = string.Empty;
                    }
                }
            }
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 新規(F2)イベント生成
            parentForm.bt_func2.Click -= new EventHandler(this.form.ChangeNewWindow);
            parentForm.bt_func2.Click += new EventHandler(this.form.ChangeNewWindow);
            // 修正(F3)イベント生成
            parentForm.bt_func3.Click -= new EventHandler(this.form.ChangeUpdateWindow);
            parentForm.bt_func3.Click += new EventHandler(this.form.ChangeUpdateWindow);
            // 一覧ボタン(F7)イベント
            parentForm.bt_func7.Click -= new EventHandler(this.form.ShowIchiran);
            parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);
            // 登録(F9)イベント作成
            parentForm.bt_func9.Click -= new EventHandler(this.form.bt_func9_Click);
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            // 閉じる(F12)イベント作成
            parentForm.bt_func12.Click -= new EventHandler(this.form.bt_func12_Click);
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // ファイルアップロード
            parentForm.bt_process1.Click -= new EventHandler(this.form.bt_process1_Click);
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            // 一覧のEnterイベント生成
            this.form.customDataGridView1.CellEnter -= new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);
            this.form.customDataGridView1.CellEnter += new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);
        }

        #endregion

        #region ボタン制御
        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        private void ButtonEnabledControl()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            // 初期化
            // 新規モードの場合
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                parentForm.bt_func3.Enabled = false;
            }
            // 修正モードの場合
            else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                parentForm.bt_func2.Enabled = true;
            }
            // 削除モードの場合
            else if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                parentForm.bt_func2.Enabled = false;
                parentForm.bt_process1.Enabled = false;
            }
            // 参照モードの場合
            else
            {
                parentForm.bt_func2.Enabled = false;
                parentForm.bt_func3.Enabled = false;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_process1.Enabled = false;
            }

            parentForm.bt_func12.Enabled = true;
        }
        #endregion

        /// <summary>
        /// テーブルからデータを取得する。(システムID, SEQ)
        /// </summary>
        private void GetTableData()
        {
            // 現場メモEntry
            this.genbamemoEntry = this.genbamemoEntryDAO.GetDataByKey(this.form.SystemId, this.form.SEQ);
            
            // 現場メモDetail
            this.genbamemoDetailList = this.genbamemoDetailDAO.GetDataByKey(this.form.SystemId, this.form.SEQ);
        }

        /// <summary>
        /// テーブルからデータを取得する。(現場メモ番号)
        /// </summary>
        private void GetTableDataByGenbamemoNunmer()
        {
            // 現場メモEntry
            this.genbamemoEntry = this.genbamemoEntryDAO.GetDataByGenbamemoNumber(this.form.GenbamemoNumber);

            if (this.genbamemoEntry != null)
            {
                this.form.SystemId = this.genbamemoEntry.SYSTEM_ID.ToString();
                this.form.SEQ = this.genbamemoEntry.SEQ.ToString();
                // 現場メモDetail
                this.genbamemoDetailList = this.genbamemoDetailDAO.GetDataByKey(this.form.SystemId, this.form.SEQ);
            }
        }

        /// <summary>
        /// 画面に反映
        /// </summary>
        internal void SetValue()
        {
            // データを取得する。
            if (string.IsNullOrEmpty(this.form.GenbamemoNumber))
            {
                this.GetTableData();
            }
            else
            {
                this.GetTableDataByGenbamemoNunmer();
            }

            if (this.genbamemoEntry != null)
            {
                // ヘッダ部
                // 非表示
                if (this.genbamemoEntry.HIHYOUJI_FLG)
                {
                    this.HeaderForm.HIHYOUJI.Checked = true;
                }
                else
                {
                    this.HeaderForm.HIHYOUJI.Checked = false;
                }
                // 非表示日
                if (!this.genbamemoEntry.HIHYOUJI_DATE.IsNull)
                {
                    this.HeaderForm.HIHYOUJI_DATE.Text = this.genbamemoEntry.HIHYOUJI_DATE.ToString();
                }
                else
                {
                    this.HeaderForm.HIHYOUJI_DATE.Text = string.Empty;
                }
                // 非表示登録者
                this.HeaderForm.HIHYOUJI_TOUROKUSHA_NAME.Text = this.genbamemoEntry.HIHYOUJI_TOUROKUSHA_NAME;

                // データ部
                // 現場メモ番号
                this.form.GENBAMEMO_NUMBER.Text = this.genbamemoEntry.GENBAMEMO_NUMBER.ToString();
                // 編集権限
                if (this.genbamemoEntry.HENKOU_KANOU_FLG)
                {
                    this.form.HENKOU_KANOU_FLG.Checked = true;
                }
                else
                {
                    this.form.HENKOU_KANOU_FLG.Checked = false;
                }
                // 現場メモ分類
                this.form.GENBAMEMO_BUNRUI_CD.Text = this.genbamemoEntry.GENBAMEMO_BUNRUI_CD;
                this.form.GENBAMEMO_BUNRUI_NAME_RYAKU.Text = this.genbamemoEntry.GENBAMEMO_BUNRUI_NAME;
                // 取引先
                this.form.TORIHIKISAKI_CD.Text = this.genbamemoEntry.TORIHIKISAKI_CD;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.genbamemoEntry.TORIHIKISAKI_NAME;
                // 業者
                this.form.GYOUSHA_CD.Text = this.genbamemoEntry.GYOUSHA_CD;
                this.form.GYOUSHA_NAME_RYAKU.Text = this.genbamemoEntry.GYOUSHA_NAME;
                // 業者の前回値を保持する。
                this.beforeGyoushaCd = this.genbamemoEntry.GYOUSHA_CD;

                // 現場
                this.form.GENBA_CD.Text = this.genbamemoEntry.GENBA_CD;
                this.form.GENBA_NAME_RYAKU.Text = this.genbamemoEntry.GENBA_NAME;
                // 現場の前回値を保持する。
                this.beforeGenbaCd = this.genbamemoEntry.GENBA_CD;

                // 表題
                this.form.HYOUDAI.Text = this.genbamemoEntry.HYOUDAI;
                // 内容
                this.form.NAIYOU1.Text = this.genbamemoEntry.NAIYOU1;
                this.form.NAIYOU2.Text = this.genbamemoEntry.NAIYOU2;
                // 発生元
                this.form.HASSEIMOTO_CD.Text = this.genbamemoEntry.HASSEIMOTO_CD;
                this.form.HASSEIMOTO_NAME.Text = this.genbamemoEntry.HASSEIMOTO_NAME;

                if (this.genbamemoEntry.HASSEIMOTO_CD.Equals("2") || this.genbamemoEntry.HASSEIMOTO_CD.Equals("3") || this.genbamemoEntry.HASSEIMOTO_CD.Equals("4"))
                {
                    String table = "";
                    if (this.genbamemoEntry.HASSEIMOTO_CD.Equals("2"))
                    {
                        table = "T_UKETSUKE_SS_ENTRY";
                    }
                    else if (this.genbamemoEntry.HASSEIMOTO_CD.Equals("3"))
                    {
                        table = "T_UKETSUKE_SK_ENTRY";
                    }
                    else if (this.genbamemoEntry.HASSEIMOTO_CD.Equals("4"))
                    {
                        table = "T_UKETSUKE_MK_ENTRY";
                    }

                    // システムIDから受付番号を取得する。
                    string sql = string.Format("SELECT UKETSUKE_NUMBER "
                                       + "FROM " + table
                                       + " WHERE SYSTEM_ID = {0} "
                                       + "  AND DELETE_FLG = 0"
                                        , this.genbamemoEntry.HASSEIMOTO_SYSTEM_ID.ToString());
                    // 検索
                    DataTable dt = this.genbamemoEntryDAO.getdateforstringsql(sql);
                    if (dt.Rows.Count != 0)
                    {
                        this.form.HASSEIMOTO_NUMBER.Text = dt.Rows[0]["UKETSUKE_NUMBER"].ToString();
                    }

                }
                else if (this.genbamemoEntry.HASSEIMOTO_CD.Equals("5"))
                {
                    // 明細システムIDから、定期配車番号と明細番号を取得する。
                    string sql = string.Format("SELECT DTL.TEIKI_HAISHA_NUMBER, DTL.ROW_NUMBER "
                                        + "FROM T_TEIKI_HAISHA_DETAIL DTL "
                                        + "INNER JOIN T_TEIKI_HAISHA_ENTRY ENT "
                                        + "        ON DTL.SYSTEM_ID = ENT.SYSTEM_ID "
                                        + "       AND DTL.SEQ = ENT.SEQ "
                                        + "       AND ENT.DELETE_FLG = 0 "
                                        + "WHERE DTL.DETAIL_SYSTEM_ID = {0} "
                                         , this.genbamemoEntry.HASSEIMOTO_DETAIL_SYSTEM_ID.ToString());
                    DataTable dt = this.genbamemoEntryDAO.getdateforstringsql(sql);
                    if (dt.Rows.Count != 0)
                    {
                        this.form.HASSEIMOTO_NUMBER.Text = dt.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();
                        this.form.HASSEIMOTO_MEISAI_NUMBER.Text = dt.Rows[0]["ROW_NUMBER"].ToString();
                    }
                }

                // 初回登録日
                this.form.CREATE_DATE.Text = this.genbamemoEntry.CREATE_DATE.ToString();
                // 初回登録者
                this.form.CREATE_USER.Text = this.genbamemoEntry.CREATE_USER;
                // 最終更新日
                this.form.UPDATE_DATE.Text = this.genbamemoEntry.UPDATE_DATE.ToString();
                // 最終更新者
                this.form.UPDATE_USER.Text = this.genbamemoEntry.UPDATE_USER;

                // 明細部
                this.form.customDataGridView1.Rows.Clear();
                for (int i = 0; i < this.genbamemoDetailList.Count; i++)
                {
                    this.form.customDataGridView1.Rows.Add();
                    this.form.customDataGridView1["COMMENT", i].Value = this.genbamemoDetailList[i].COMMENT;
                    this.form.customDataGridView1["TOUROKUSHA_NAME", i].Value = this.genbamemoDetailList[i].TOUROKUSHA_NAME;
                    this.form.customDataGridView1["TOUROKU_DATE", i].Value = this.genbamemoDetailList[i].TOUROKU_DATE;
                    this.form.customDataGridView1["DETAIL_SYSTEM_ID", i].Value = this.genbamemoDetailList[i].DETAIL_SYSTEM_ID;
                }
            }
            else
            {
                this.HeaderForm.HIHYOUJI.Checked = false;
                this.HeaderForm.HIHYOUJI_DATE.Text = string.Empty;
                this.HeaderForm.HIHYOUJI_TOUROKUSHA_NAME.Text = string.Empty;
                this.form.GENBAMEMO_NUMBER.Text = string.Empty;
                this.form.HENKOU_KANOU_FLG.Checked = false;
                this.form.GENBAMEMO_BUNRUI_CD.Text = string.Empty;
                this.form.GENBAMEMO_BUNRUI_NAME_RYAKU.Text = string.Empty;
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.HYOUDAI.Text = string.Empty;
                this.form.NAIYOU1.Text = string.Empty;
                this.form.NAIYOU2.Text = string.Empty;
                this.form.HASSEIMOTO_CD.Text = string.Empty;
                this.form.HASSEIMOTO_NAME.Text = string.Empty;
                this.form.HASSEIMOTO_NUMBER.Text = string.Empty;
                this.form.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;
                this.form.CREATE_DATE.Text = string.Empty;
                this.form.CREATE_USER.Text = string.Empty;
                this.form.UPDATE_DATE.Text = string.Empty;
                this.form.UPDATE_USER.Text = string.Empty;
                this.form.customDataGridView1.Rows.Clear();

                if (this.form.dicControl.ContainsKey("GENBAMEMO_NUMBER"))
                {
                    this.form.dicControl["GENBAMEMO_NUMBER"] = string.Empty;
                }

                // 遷移元画面からのパラメータが存在する場合は設定する。
                if (this.form.paramEntry != null)
                {
                    // 取引先一覧の場合
                    if (this.form.winId.Equals(WINDOW_ID.M_TORIHIKISAKI_ICHIRAN.ToString()))
                    {
                        this.form.TORIHIKISAKI_CD.Text = this.form.paramEntry.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.form.paramEntry.TORIHIKISAKI_NAME;
                        this.form.HASSEIMOTO_CD.Text = this.form.paramEntry.HASSEIMOTO_CD;
                        this.form.HASSEIMOTO_NAME.Text = this.form.paramEntry.HASSEIMOTO_NAME;
                    }
                    // 業者一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.M_GYOUSHA_ICHIRAN.ToString()))
                    {
                        this.form.GYOUSHA_CD.Text = this.form.paramEntry.GYOUSHA_CD;
                        this.form.GYOUSHA_NAME_RYAKU.Text = this.form.paramEntry.GYOUSHA_NAME;
                        this.form.HASSEIMOTO_CD.Text = this.form.paramEntry.HASSEIMOTO_CD;
                        this.form.HASSEIMOTO_NAME.Text = this.form.paramEntry.HASSEIMOTO_NAME;
                    }
                    // 現場一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.M_GENBA_ICHIRAN.ToString()))
                    {
                        this.form.GYOUSHA_CD.Text = this.form.paramEntry.GYOUSHA_CD;
                        this.form.GYOUSHA_NAME_RYAKU.Text = this.form.paramEntry.GYOUSHA_NAME;
                        this.form.GENBA_CD.Text = this.form.paramEntry.GENBA_CD;
                        this.form.GENBA_NAME_RYAKU.Text = this.form.paramEntry.GENBA_NAME;
                        this.form.HASSEIMOTO_CD.Text = this.form.paramEntry.HASSEIMOTO_CD;
                        this.form.HASSEIMOTO_NAME.Text = this.form.paramEntry.HASSEIMOTO_NAME;
                    }
                    // 受付一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.T_UKETSUKE_ICHIRAN.ToString()))
                    {
                        this.form.TORIHIKISAKI_CD.Text = this.form.paramEntry.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = this.form.paramEntry.TORIHIKISAKI_NAME;
                        this.form.GYOUSHA_CD.Text = this.form.paramEntry.GYOUSHA_CD;
                        this.form.GYOUSHA_NAME_RYAKU.Text = this.form.paramEntry.GYOUSHA_NAME;
                        this.form.GENBA_CD.Text = this.form.paramEntry.GENBA_CD;
                        this.form.GENBA_NAME_RYAKU.Text = this.form.paramEntry.GENBA_NAME;
                        this.form.HASSEIMOTO_CD.Text = this.form.paramEntry.HASSEIMOTO_CD;
                        this.form.HASSEIMOTO_NAME.Text = this.form.paramEntry.HASSEIMOTO_NAME;

                        String table = "";
                        if (this.form.paramEntry.HASSEIMOTO_CD.Equals("2"))
                        {
                            table = "T_UKETSUKE_SS_ENTRY";
                        }
                        else if (this.form.paramEntry.HASSEIMOTO_CD.Equals("3"))
                        {
                            table = "T_UKETSUKE_SK_ENTRY";
                        }
                        else if (this.form.paramEntry.HASSEIMOTO_CD.Equals("4"))
                        {
                            table = "T_UKETSUKE_MK_ENTRY";
                        }

                        // システムIDから受付番号を取得する。
                        string sql = string.Format("SELECT UKETSUKE_NUMBER "
                                           + "FROM " + table
                                           + " WHERE SYSTEM_ID = {0} "
                                           + "  AND DELETE_FLG = 0"
                                            , this.form.paramEntry.HASSEIMOTO_SYSTEM_ID.ToString());
                        // 検索
                        DataTable dt = this.genbamemoEntryDAO.getdateforstringsql(sql);
                        if (dt.Rows.Count != 0)
                        {
                            this.form.HASSEIMOTO_NUMBER.Text = dt.Rows[0]["UKETSUKE_NUMBER"].ToString();
                        }

                    }
                    // 定期配車一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN.ToString()))
                    {
                        this.form.GYOUSHA_CD.Text = this.form.paramEntry.GYOUSHA_CD;
                        this.form.GYOUSHA_NAME_RYAKU.Text = this.form.paramEntry.GYOUSHA_NAME;
                        this.form.GENBA_CD.Text = this.form.paramEntry.GENBA_CD;
                        this.form.GENBA_NAME_RYAKU.Text = this.form.paramEntry.GENBA_NAME;
                        this.form.HASSEIMOTO_CD.Text = this.form.paramEntry.HASSEIMOTO_CD;
                        this.form.HASSEIMOTO_NAME.Text = this.form.paramEntry.HASSEIMOTO_NAME;
                        // 明細システムIDから、定期配車番号と明細番号を取得する。
                        string sql = string.Format("SELECT DTL.TEIKI_HAISHA_NUMBER, DTL.ROW_NUMBER "
                                            + "FROM T_TEIKI_HAISHA_DETAIL DTL "
                                            + "INNER JOIN T_TEIKI_HAISHA_ENTRY ENT "
                                            + "        ON DTL.SYSTEM_ID = ENT.SYSTEM_ID "
                                            + "       AND DTL.SEQ = ENT.SEQ "
                                            + "       AND ENT.DELETE_FLG = 0 "
                                            + "WHERE DTL.DETAIL_SYSTEM_ID = {0} "
                                             , this.form.paramEntry.HASSEIMOTO_DETAIL_SYSTEM_ID.ToString());
                        DataTable dt = this.genbamemoEntryDAO.getdateforstringsql(sql);
                        if (dt.Rows.Count != 0)
                        {
                            this.form.HASSEIMOTO_NUMBER.Text = dt.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();
                            this.form.HASSEIMOTO_MEISAI_NUMBER.Text = dt.Rows[0]["ROW_NUMBER"].ToString();
                        }
                    }
                }
                else
                {
                    //遷移元パラメータが無い場合、現場メモ一覧からの遷移として[発生元無し]とする
                    this.form.HASSEIMOTO_CD.Text = "1";
                    this.form.HASSEIMOTO_NAME.Text = "発生元無し";
                }

                // 業者の前回値を保持する。
                this.beforeGyoushaCd = this.form.GYOUSHA_CD.Text;
                // 現場の前回値を保持する。
                this.beforeGenbaCd = this.form.GENBA_CD.Text;
            }

            // 発生元CDの前回値を保持する。
            this.beforeHsseimotoCd = this.form.HASSEIMOTO_CD.Text;

            // 新規モード時
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.form.customDataGridView1.Rows.Clear();

                // 一覧に新規行の追加を許可する。
                this.form.customDataGridView1.AllowUserToAddRows = true;

                this.HeaderForm.windowTypeLabel.Text = "新規";
                this.HeaderForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                this.HeaderForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 現場メモ番号をクリアする。
                this.form.GENBAMEMO_NUMBER.Text = string.Empty;
                // 現場メモ番号を活性にする。
                this.form.GENBAMEMO_NUMBER.Enabled = true;

                // 全ての項目を活性にする。
                this.HeaderForm.HIHYOUJI.Enabled = true;
                this.form.GENBAMEMO_NUMBER.Enabled = true;
                this.form.HENKOU_KANOU_FLG.Enabled = true;
                this.form.GENBAMEMO_BUNRUI_CD.Enabled = true;
                this.form.TORIHIKISAKI_CD.Enabled = true;
                this.form.TORIHIKISAKI_SEARCH_BUTTON.Enabled = true;
                this.form.GYOUSHA_CD.Enabled = true;
                this.form.GYOUSHA_SEARCH_BUTTON.Enabled = true;
                this.form.GENBA_CD.Enabled = true;
                this.form.GENBA_SEARCH_BUTTON.Enabled = true;
                this.form.HYOUDAI.Enabled = true;
                this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.Enabled = true;
                this.form.NAIYOU1.Enabled = true;
                this.form.NAIYOU2.Enabled = true;
                this.form.HASSEIMOTO_CD.Enabled = true;
                this.form.HASSEIMOTO_NUMBER.Enabled = true;
                this.form.HASSEIMOTO_MEISAI_NUMBER.Enabled = true;
                this.form.customDataGridView1.Columns["colCheckBox"].ReadOnly = false;
                this.form.customDataGridView1.Columns["COMMENT"].ReadOnly = false;

                // 発生元番号、発生元明細番号の制御
                this.form.setEnableHasseimotoNumber(this.beforeHsseimotoCd);

                // 複写の場合、以下の項目を制御する。
                if (this.form.hukushaFlg.Equals("ON"))
                {
                    this.HeaderForm.HIHYOUJI.Checked = false;
                    this.HeaderForm.HIHYOUJI_DATE.Text = string.Empty;
                    this.HeaderForm.HIHYOUJI_TOUROKUSHA_NAME.Text = string.Empty;
                    this.form.CREATE_DATE.Text = string.Empty;
                    this.form.CREATE_USER.Text = string.Empty;
                    this.form.UPDATE_DATE.Text = string.Empty;
                    this.form.UPDATE_USER.Text = string.Empty;
                    this.form.HENKOU_KANOU_FLG.Checked = false;

                    // 複写モードのフラグはOFFにする。
                    this.form.hukushaFlg = "OFF";
                }
            }
            // 修正モード時
            else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                // 一覧に新規行の追加を許可する。
                this.form.customDataGridView1.AllowUserToAddRows = true;

                this.HeaderForm.windowTypeLabel.Text = "修正";
                this.HeaderForm.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                this.HeaderForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 現場メモ番号を非活性にする。
                this.form.GENBAMEMO_NUMBER.Enabled = false;

                // 現場メモ番号以外を活性にする。
                this.HeaderForm.HIHYOUJI.Enabled = true;
                this.form.HENKOU_KANOU_FLG.Enabled = true;
                this.form.GENBAMEMO_BUNRUI_CD.Enabled = true;
                this.form.TORIHIKISAKI_CD.Enabled = true;
                this.form.TORIHIKISAKI_SEARCH_BUTTON.Enabled = true;
                this.form.GYOUSHA_CD.Enabled = true;
                this.form.GYOUSHA_SEARCH_BUTTON.Enabled = true;
                this.form.GENBA_CD.Enabled = true;
                this.form.GENBA_SEARCH_BUTTON.Enabled = true;
                this.form.HYOUDAI.Enabled = true;
                this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.Enabled = true;
                this.form.NAIYOU1.Enabled = true;
                this.form.NAIYOU2.Enabled = true;
                this.form.HASSEIMOTO_CD.Enabled = true;
                this.form.HASSEIMOTO_NUMBER.Enabled = true;
                this.form.HASSEIMOTO_MEISAI_NUMBER.Enabled = true;
                this.form.customDataGridView1.Columns["COMMENT"].ReadOnly = false;
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].IsNewRow)
                    {
                        continue;
                    }

                    this.form.customDataGridView1.Rows[i].Cells["COMMENT"].Style.BackColor = Constans.NOMAL_COLOR;
                    this.form.customDataGridView1.Rows[i].Cells["COMMENT"].Style.ForeColor = Constans.NOMAL_COLOR_FORE;
                    this.form.customDataGridView1.Rows[i].Cells["COMMENT"].Style.SelectionBackColor = Constans.SELECT_COLOR;
                    this.form.customDataGridView1.Rows[i].Cells["COMMENT"].Style.SelectionForeColor = Constans.SELECT_COLOR_FORE;
                    this.form.customDataGridView1.Rows[i].Cells["COMMENT"].ReadOnly = true;
                }

                // 発生元番号、発生元明細番号の制御
                this.form.setEnableHasseimotoNumber(this.beforeHsseimotoCd);

                // 初回登録者とログイン者が異なる場合、削除列を非活性にする。
                if (!this.genbamemoEntry.SHOKAI_TOUROKUSHA_CD.Equals(SystemProperty.Shain.CD))
                {
                    this.form.customDataGridView1.Columns["colCheckBox"].ReadOnly = true;

                    // 初回登録者以外の修正/削除が禁止されている場合は、コメント以外を非活性にする。
                    if (this.genbamemoEntry.HENKOU_KANOU_FLG)
                    {
                        this.HeaderForm.HIHYOUJI.Enabled = false;
                        this.form.GENBAMEMO_NUMBER.Enabled = false;
                        this.form.HENKOU_KANOU_FLG.Enabled = false;
                        this.form.GENBAMEMO_BUNRUI_CD.Enabled = false;
                        this.form.TORIHIKISAKI_CD.Enabled = false;
                        this.form.TORIHIKISAKI_SEARCH_BUTTON.Enabled = false;
                        this.form.GYOUSHA_CD.Enabled = false;
                        this.form.GYOUSHA_SEARCH_BUTTON.Enabled = false;
                        this.form.GENBA_CD.Enabled = false;
                        this.form.GENBA_SEARCH_BUTTON.Enabled = false;
                        this.form.HYOUDAI.Enabled = false;
                        this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.Enabled = false;
                        this.form.NAIYOU1.Enabled = false;
                        this.form.NAIYOU2.Enabled = false;
                        this.form.HASSEIMOTO_CD.Enabled = false;
                        this.form.HASSEIMOTO_NUMBER.Enabled = false;
                        this.form.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                    }
                }
                else
                {
                    this.form.customDataGridView1.Columns["colCheckBox"].ReadOnly = false;
                }
            }
            // 削除モード時
            else if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                // 一覧に新規行の追加を許可しない。
                this.form.customDataGridView1.AllowUserToAddRows = false;

                // 全ての項目を非活性にする。
                this.HeaderForm.HIHYOUJI.Enabled = false;
                this.form.GENBAMEMO_NUMBER.Enabled = false;
                this.form.HENKOU_KANOU_FLG.Enabled = false;
                this.form.GENBAMEMO_BUNRUI_CD.Enabled = false;
                this.form.TORIHIKISAKI_CD.Enabled = false;
                this.form.TORIHIKISAKI_SEARCH_BUTTON.Enabled = false;
                this.form.GYOUSHA_CD.Enabled = false;
                this.form.GYOUSHA_SEARCH_BUTTON.Enabled = false;
                this.form.GENBA_CD.Enabled = false;
                this.form.GENBA_SEARCH_BUTTON.Enabled = false;
                this.form.HYOUDAI.Enabled = false;
                this.form.GENBAMEMO_HYOUDAI_SEARCH_BUTTON.Enabled = false;
                this.form.NAIYOU1.Enabled = false;
                this.form.NAIYOU2.Enabled = false;
                this.form.HASSEIMOTO_CD.Enabled = false;
                this.form.HASSEIMOTO_NUMBER.Enabled = false;
                this.form.HASSEIMOTO_MEISAI_NUMBER.Enabled = false;
                this.form.customDataGridView1.Columns["colCheckBox"].ReadOnly = true;
                this.form.customDataGridView1.Columns["COMMENT"].ReadOnly = true;

                //明細欄の背景色と文字色を変更
                var rows = this.form.customDataGridView1.Rows;
                foreach (DataGridViewRow row in rows)
                {
                    var cells = row.Cells;
                    foreach (DataGridViewCell cell in cells)
                    {
                        var changeColor = cell as ICustomAutoChangeBackColor;
                        if (changeColor != null)
                        {
                            ((ICustomAutoChangeBackColor)cell).AutoChangeBackColorEnabled = false;
                            cell.Style.BackColor = Constans.READONLY_COLOR;
                            cell.Style.ForeColor = Constans.READONLY_COLOR_FORE;
                            cell.Style.SelectionBackColor = Constans.READONLY_COLOR;
                            cell.Style.SelectionForeColor = Constans.READONLY_COLOR_FORE;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        internal void CheckPopup(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.KeyCode == Keys.Space)
            {
                if (this.form.customDataGridView1.Columns[this.form.customDataGridView1.CurrentCell.ColumnIndex].Name.Equals("colFileShuruiCD"))
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;

                    DataRow row;
                    row = dt.NewRow();
                    row["CD"] = "11";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_11;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "12";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_12;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "13";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_13;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "21";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_21;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "22";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_22;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "23";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_23;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "31";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_31;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "32";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_32;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "33";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_33;
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "99";
                    row["VALUE"] = ConstCls.FILE_SHURUI_NAME_99;
                    dt.Rows.Add(row);

                    dt.TableName = "ファイル種類";
                    form.table = dt;
                    form.PopupTitleLabel = "ファイル種類";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "ファイル種類CD", "ファイル種類名" };

                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.customDataGridView1.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentCell.RowIndex].Cells["colFileShuruiName"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
            }
            LogUtility.DebugMethodEnd(e);
        }

        #region 現場メモ番号を採番
        /// <summary>
        /// 現場メモ番号を採番
        /// </summary>
        /// <returns>採番した数値</returns>
        private SqlInt64 createGenbamemoNumber()
        {
            SqlInt64 returnVal = -1;

            try
            {
                LogUtility.DebugMethodStart();

                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.GENBA_MEMO.GetHashCode();

                // IS_NUMBER_DENSHUDao(共通)
                IS_NUMBER_DENSHUDao numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();
                var updateEntity = numberDenshuDao.GetNumberDenshuData(entity);
                returnVal = numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = (SqlInt16)DENSHU_KBN.GENBA_MEMO.GetHashCode();
                    updateEntity.CURRENT_NUMBER = returnVal;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    numberDenshuDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnVal;
                    numberDenshuDao.Update(updateEntity);
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 登録用Entity作成
        /// <summary>
        /// Entity作成
        /// </summary>
        internal void CreateEntity()
        {
            var entry = new T_GENBAMEMO_ENTRY();
            SqlInt16 denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.GENBA_MEMO).ToString());
            //INSERTのみ採番
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                entry.SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);
                entry.SEQ = 1;
                entry.GENBAMEMO_NUMBER = this.createGenbamemoNumber();
                entry.SHOKAI_TOUROKUSHA_CD = SystemProperty.Shain.CD;
            }
            else
            {
                if (this.form.HENKOU_KANOU_FLG.Checked)
                {
                    // 初回登録者とログイン者が異なる場合、entryの値は変更しない。
                    if (!this.genbamemoEntry.SHOKAI_TOUROKUSHA_CD.Equals(SystemProperty.Shain.CD))
                    {
                        this.genbamemoEntry.SEQ += 1;
                        return;
                    }
                }

                entry.SYSTEM_ID = this.genbamemoEntry.SYSTEM_ID;
                entry.SEQ = this.genbamemoEntry.SEQ + 1;
                entry.GENBAMEMO_NUMBER = this.genbamemoEntry.GENBAMEMO_NUMBER;
                entry.SHOKAI_TOUROKUSHA_CD = this.genbamemoEntry.SHOKAI_TOUROKUSHA_CD;
            }
            
            if (this.HeaderForm.HIHYOUJI.Checked)
            {
                entry.HIHYOUJI_FLG = true;

                if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG
                    || (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG
                        && !this.genbamemoEntry.HIHYOUJI_FLG))
                {
                    // 非表示チェックがONの場合、非表示日、非表示登録者を設定する。
                    DateTime now = DateTime.Now;
                    entry.HIHYOUJI_DATE = SqlDateTime.Parse(now.ToString());
                    entry.HIHYOUJI_TOUROKUSHA_NAME = SystemProperty.Shain.Name;
                }
                else
                {
                    entry.HIHYOUJI_DATE = this.genbamemoEntry.HIHYOUJI_DATE;
                    entry.HIHYOUJI_TOUROKUSHA_NAME = this.genbamemoEntry.HIHYOUJI_TOUROKUSHA_NAME;
                }
            }
            else
            {
                entry.HIHYOUJI_FLG = false;
                entry.HIHYOUJI_DATE = SqlDateTime.Null;
                entry.HIHYOUJI_TOUROKUSHA_NAME = string.Empty;
            }
            
            entry.HENKOU_KANOU_FLG = this.form.HENKOU_KANOU_FLG.Checked;
            entry.GENBAMEMO_BUNRUI_CD = this.form.GENBAMEMO_BUNRUI_CD.Text;
            entry.GENBAMEMO_BUNRUI_NAME = this.form.GENBAMEMO_BUNRUI_NAME_RYAKU.Text;
            entry.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            entry.TORIHIKISAKI_NAME = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
            entry.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            entry.GYOUSHA_NAME = this.form.GYOUSHA_NAME_RYAKU.Text;
            entry.GENBA_CD = this.form.GENBA_CD.Text;
            entry.GENBA_NAME = this.form.GENBA_NAME_RYAKU.Text;
            entry.HYOUDAI = this.form.HYOUDAI.Text;
            entry.NAIYOU1 = this.form.NAIYOU1.Text;
            entry.NAIYOU2 = this.form.NAIYOU2.Text;
            entry.HASSEIMOTO_CD = this.form.HASSEIMOTO_CD.Text;
            entry.HASSEIMOTO_NAME = this.form.HASSEIMOTO_NAME.Text;
            if ((this.form.HASSEIMOTO_CD.Text.Equals("2") || this.form.HASSEIMOTO_CD.Text.Equals("3") || this.form.HASSEIMOTO_CD.Text.Equals("4"))
                && !string.IsNullOrEmpty(this.form.HASSEIMOTO_NUMBER.Text))
            {
                String table = "";
                if (this.form.HASSEIMOTO_CD.Text.Equals("2"))
                {
                    table = "T_UKETSUKE_SS_ENTRY";
                }
                else if (this.form.HASSEIMOTO_CD.Text.Equals("3"))
                {
                    table = "T_UKETSUKE_SK_ENTRY";
                }
                else if (this.form.HASSEIMOTO_CD.Text.Equals("4"))
                {
                    table = "T_UKETSUKE_MK_ENTRY";
                }

                // システムIDから受付番号を取得する。
                string sql = string.Format("SELECT SYSTEM_ID "
                                   + "FROM " + table
                                   + " WHERE UKETSUKE_NUMBER = {0} "
                                   + "  AND DELETE_FLG = 0"
                                    , this.form.HASSEIMOTO_NUMBER.Text);
                // 検索
                DataTable dt = this.genbamemoEntryDAO.getdateforstringsql(sql);
                if (dt.Rows.Count != 0)
                {
                    entry.HASSEIMOTO_SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                }
            }
            // 発生元が「5:定期配車」の場合、定期配車DetailからSYSTEM_ID, DETAIL_SYSTEM_IDを取得して設定する。
            if (!string.IsNullOrEmpty(this.form.HASSEIMOTO_CD.Text) && this.form.HASSEIMOTO_CD.Text.Equals("5")
                && !string.IsNullOrEmpty(this.form.HASSEIMOTO_MEISAI_NUMBER.Text))
            {
                string sql = string.Format("SELECT DTL.SYSTEM_ID, DTL.DETAIL_SYSTEM_ID " 
                                            + "FROM T_TEIKI_HAISHA_DETAIL DTL "
                                            + "INNER JOIN T_TEIKI_HAISHA_ENTRY ENT "
                                            + "        ON DTL.SYSTEM_ID = ENT.SYSTEM_ID "
	                                        + "       AND DTL.SEQ = ENT.SEQ "
                                            + "       AND ENT.DELETE_FLG = 0 "
                                            + "WHERE DTL.TEIKI_HAISHA_NUMBER = {0} "
                                            + "  AND DTL.ROW_NUMBER = {1}"
                                             , this.form.HASSEIMOTO_NUMBER.Text, this.form.HASSEIMOTO_MEISAI_NUMBER.Text);
                // 検索
                DataTable dt =  this.genbamemoEntryDAO.getdateforstringsql(sql);
                if (dt.Rows.Count != 0)
                {
                    entry.HASSEIMOTO_SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                    entry.HASSEIMOTO_DETAIL_SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                }
            }
            
            entry.DELETE_FLG = false;

            var dataBinderDelivery = new DataBinderLogic<T_GENBAMEMO_ENTRY>(entry);
            dataBinderDelivery.SetSystemProperty(entry, false);

            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                entry.CREATE_DATE = this.genbamemoEntry.CREATE_DATE;
                entry.CREATE_PC = this.genbamemoEntry.CREATE_PC;
                entry.CREATE_USER = this.genbamemoEntry.CREATE_USER;
            }

            this.genbamemoEntry = entry;
        }
        /// <summary>
        /// DetailEntity作成
        /// </summary>
        internal void CreateDetailEntity(SqlInt64 systemID, int SEQ)
        {
            try
            {
                LogUtility.DebugMethodStart(systemID, SEQ);

                List<T_GENBAMEMO_DETAIL> tmpList = new List<T_GENBAMEMO_DETAIL>();

                for (int i = 0; i < this.form.customDataGridView1.RowCount - 1; i++)
                {
                    T_GENBAMEMO_DETAIL detailEntity = new T_GENBAMEMO_DETAIL();

                    DataGridViewRow row = this.form.customDataGridView1.Rows[i];

                    // 新規行は除外する。
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    // 削除チェックがONの行は除外する。
                    if (row.Cells["colCheckBox"].Value != null
                        && row.Cells["colCheckBox"].Value.ToString() == "True")
                    {
                        continue;
                    }

                    // 現場メモEntryのSYSTEM_ID
                    detailEntity.SYSTEM_ID = systemID;

                    // SEQ
                    detailEntity.SEQ = SEQ;

                    // 新規レコードの場合
                    if (row.Cells["DETAIL_SYSTEM_ID"].Value == null || string.IsNullOrEmpty(row.Cells["DETAIL_SYSTEM_ID"].Value.ToString()))
                    {
                        SqlInt16 denshuKbn = SqlInt16.Parse(((int)DENSHU_KBN.GENBA_MEMO).ToString());
                        // DETAIL_SYSTEM_ID(採番)
                        detailEntity.DETAIL_SYSTEM_ID = SaibanUtil.createSystemId(denshuKbn);

                        // コメント
                        detailEntity.COMMENT = row.Cells["COMMENT"].Value.ToString();

                        // 登録者
                        detailEntity.TOUROKUSHA_NAME = SystemProperty.Shain.Name;

                        // 登録日時
                        DateTime now = DateTime.Now;
                        detailEntity.TOUROKU_DATE = SqlDateTime.Parse(now.ToString());
                    }
                    else // レコード更新の場合
                    {
                        // DETAIL_SYSTEM_ID(元データのSYSTEM_ID)
                        detailEntity.DETAIL_SYSTEM_ID = Int64.Parse(row.Cells["DETAIL_SYSTEM_ID"].Value.ToString());

                        // コメント
                        detailEntity.COMMENT = this.genbamemoDetailList[i].COMMENT;

                        // 登録者
                        detailEntity.TOUROKUSHA_NAME = this.genbamemoDetailList[i].TOUROKUSHA_NAME;

                        // 登録日時
                        detailEntity.TOUROKU_DATE = this.genbamemoDetailList[i].TOUROKU_DATE;
                    }

                    // リストに追加
                    tmpList.Add(detailEntity);

                }

                this.genbamemoDetailList = tmpList;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region 論理削除のEntryEntityを作成
        /// <summary>
        /// 論理削除のEntityを作成
        /// </summary>
        public void CreateDelEntryEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.delGenbamemoEntry = this.genbamemoEntry;
                this.delSEQ = this.genbamemoEntry.SEQ.ToString();

                // 削除フラグ
                this.delGenbamemoEntry.DELETE_FLG = true;

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 登録前チェック

        internal bool RegistCheck()
        {
            // 削除モードチェック
            if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG) 
            {
                if (this.genbamemoEntry.HENKOU_KANOU_FLG
                    && !this.genbamemoEntry.SHOKAI_TOUROKUSHA_CD.Equals(SystemProperty.Shain.CD))
                {
                    this.msgLogic.MessageBoxShowError("初回登録者以外の削除が禁止されています。");
                    return false;
                }
            }

            // 取引先 or 業者 or 現場が入力されていること
            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text)
                && string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text)
                && string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                this.msgLogic.MessageBoxShow("E012", "取引先、業者、現場のいずれか");
                return false;
            }
            // 表題が入力されていること
            if (string.IsNullOrEmpty(this.form.HYOUDAI.Text))
            {
                this.msgLogic.MessageBoxShow("E012", "表題");
                return false;
            }
            // 内容が入力されていること
            if (string.IsNullOrEmpty(this.form.NAIYOU1.Text)
                && string.IsNullOrEmpty(this.form.NAIYOU2.Text))
            {
                this.msgLogic.MessageBoxShow("E012", "内容");
                return false;
            }

            // 発行元が「2,3,4,5」の場合、発行元番号を必須とする。
            if (!string.IsNullOrEmpty(this.form.HASSEIMOTO_CD.Text))
            {
                if (this.form.HASSEIMOTO_CD.Text.Equals("2")
                    || this.form.HASSEIMOTO_CD.Text.Equals("3")
                    || this.form.HASSEIMOTO_CD.Text.Equals("4")
                    || this.form.HASSEIMOTO_CD.Text.Equals("5"))
                {
                    if (string.IsNullOrEmpty(this.form.HASSEIMOTO_NUMBER.Text))
                    {
                        this.msgLogic.MessageBoxShow("E012", "発行元番号");
                        return false;
                    }
                }

                // 発行元が「5」の場合、発行元明細番号を必須とする。
                if (this.form.HASSEIMOTO_CD.Text.Equals("5"))
                {
                    if (string.IsNullOrEmpty(this.form.HASSEIMOTO_MEISAI_NUMBER.Text))
                    {
                        this.msgLogic.MessageBoxShow("E012", "明細No");
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        internal int RegistData()
        {
            int ret_cnt = 0;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    // entry
                    this.genbamemoEntryDAO.Insert(this.genbamemoEntry);

                    // detail
                    foreach (var dto in this.genbamemoDetailList)
                    {
                        this.genbamemoDetailDAO.Insert(dto);

                    }

                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret_cnt;
        }
        #endregion

        #region 修正登録
        /// <summary>
        /// 修正登録
        /// </summary>
        [Transaction]
        internal int UpdateData()
        {
            int ret_cnt = 0;
            try
            {
                using (Transaction tran = new Transaction())
                {
                    // 論理削除
                    this.delGenbamemoEntry.SEQ = Int32.Parse(this.delSEQ);
                    this.genbamemoEntryDAO.Update(this.delGenbamemoEntry);

                    // 登録
                    this.genbamemoEntry.SEQ = Int32.Parse(this.delSEQ) + 1;
                    this.genbamemoEntry.DELETE_FLG = false;

                    // 初回登録者とログイン者が異なる場合、entryの値は変更しない。
                    if (this.form.HENKOU_KANOU_FLG.Checked)
                    {
                        if (!this.genbamemoEntry.SHOKAI_TOUROKUSHA_CD.Equals(SystemProperty.Shain.CD))
                        {
                            var entry2 = new T_GENBAMEMO_ENTRY();
                            var dataBinderDelivery2 = new DataBinderLogic<T_GENBAMEMO_ENTRY>(entry2);
                            dataBinderDelivery2.SetSystemProperty(entry2, false);

                            this.genbamemoEntry.UPDATE_DATE = entry2.UPDATE_DATE;
                            this.genbamemoEntry.UPDATE_PC = entry2.UPDATE_PC;
                            this.genbamemoEntry.UPDATE_USER = entry2.UPDATE_USER;
                        }
                    }

                    this.genbamemoEntryDAO.Insert(this.genbamemoEntry);
                    foreach (var dto in this.genbamemoDetailList)
                    {
                        this.genbamemoDetailDAO.Insert(dto);

                    }

                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret_cnt;
        }
        #endregion

        #region 削除登録
        /// <summary>
        /// 削除登録
        /// </summary>
        [Transaction]
        internal int LogicalDeleteData()
        {
            int ret_cnt = 0;

            try
            {
                using (Transaction tran = new Transaction())
                {
                    // 論理削除
                    this.genbamemoEntryDAO.Update(this.delGenbamemoEntry);

                    // ファイルデータ削除
                    var list = this.fileLinkGenbamemoDao.GetDataBySystemId(this.delGenbamemoEntry.SYSTEM_ID.ToString());
                    if (list != null && 0 < list.Count)
                    {
                        // ファイルデータを物理削除する。
                        var fileIdList = list.Select(n => n.FILE_ID.Value).ToList();
                        this.form.uploadLogic.DeleteFileData(fileIdList);

                        // 連携データ削除
                        string sql = string.Format("DELETE FROM M_FILE_LINK_GENBAMEMO_ENTRY WHERE SYSTEM_ID = {0}"
                            , this.delGenbamemoEntry.SYSTEM_ID.ToString());
                        this.fileLinkGenbamemoDao.GetDateForStringSql(sql);
                    }

                    tran.Commit();
                    ret_cnt = 1;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDeleteData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return ret_cnt;
        }
        #endregion



        /// <summary>
        /// 一覧 - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="columnIndex"></param>
        internal void CellEnter(int columnIndex)
        {
            string cellName = this.form.customDataGridView1.Columns[columnIndex].Name;

            if (string.IsNullOrEmpty(cellName))
            {
                return;
            }

            // IME制御
            switch (cellName)
            {
                case "colFileShuruiCD":
                    this.form.customDataGridView1.ImeMode = ImeMode.Disable;
                    break;

                default:
                    this.form.customDataGridView1.ImeMode = ImeMode.Hiragana;
                    break;
            }
        }

        /// <summary>
        /// 有効行であるか判断する。
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsAllNullOrEmpty(DataGridViewRow row)
        {
            if ((row.Cells["colFileShuruiCD"].Value == null || string.IsNullOrEmpty(row.Cells["colFileShuruiCD"].Value.ToString()))
                && (row.Cells["colBiko"].Value == null || string.IsNullOrEmpty(row.Cells["colBiko"].Value.ToString()))
                && (row.Cells["colFilePath"].Value == null || string.IsNullOrEmpty(row.Cells["colFilePath"].Value.ToString()))
                )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 現場メモ分類CDで現場メモ分類を取得します
        /// </summary>
        /// <param name="torihikisakiCd">現場メモ分類CD</param>
        /// <returns>現場メモ分類エンティティ</returns>
        public M_GENBAMEMO_BUNRUI GetGenbamemoBunrui(string genbamemoBunruiCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(genbamemoBunruiCd);

                var keyEntity = new M_GENBAMEMO_BUNRUI();
                keyEntity.GENBAMEMO_BUNRUI_CD = genbamemoBunruiCd;
                var genbamemoBunrui = this.genbamemoBunruiDao.GetAllValidData(keyEntity).FirstOrDefault();

                LogUtility.DebugMethodEnd(genbamemoBunrui, catchErr);

                return genbamemoBunrui;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenbamemoBunrui", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbamemoBunrui", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 取引CDで取引先を取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns>取引先エンティティ</returns>
        public M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                var keyEntity = new M_TORIHIKISAKI();
                keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
                var torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity).FirstOrDefault();

                LogUtility.DebugMethodEnd(torihikisaki, catchErr);

                return torihikisaki;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 業者CDで業者を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <returns>業者エンティティ</returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                var keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity).FirstOrDefault();

                LogUtility.DebugMethodEnd(gyousha, catchErr);

                return gyousha;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 現場CDと業者CDで現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティ</returns>
        public M_GENBA GetGenba(string genbaCd, string gyoushaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                var keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity).FirstOrDefault();

                LogUtility.DebugMethodEnd(genba, catchErr);

                return genba;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 業者CD、現場CDで現場リストを取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティリスト</returns>
        public M_GENBA[] GetGenbaList(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            List<M_GENBA> retlist = new List<M_GENBA>();
            IEnumerable<M_GENBA> gList = null;

            var keyEntity = new M_GENBA();
            keyEntity.GYOUSHA_CD = gyoushaCd;
            keyEntity.GENBA_CD = genbaCd;
            gList = this.genbaDao.GetAllValidData(keyEntity);


            if (null != gList && gList.Count() != 0)
            {
                M_GENBA[] temp = gList.ToArray();
                for (int i = 0; i < temp.Length; i++)
                {
                    retlist.Add(temp[i]);
                }
            }

            LogUtility.DebugMethodEnd();

            return retlist.ToArray();
        }

        /// <summary>
        /// ポップアップボタン名により、テキスト名を取得
        /// </summary>
        /// <returns></returns>
        internal string GetTextName(String buttonName)
        {
            string textName = "";

            try
            {
                LogUtility.DebugMethodStart(buttonName);

                switch (buttonName)
                {
                    case "TORIHIKISAKI_SEARCH_BUTTON":
                        textName = "TORIHIKISAKI_CD";
                        break;
                    case "GYOUSHA_SEARCH_BUTTON":
                        textName = "GYOUSHA_CD";
                        break;
                    case "GENBA_SEARCH_BUTTON":
                        textName = "GENBA_CD";
                        break;
                    default:
                        break;
                }
                return textName;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(textName);
            }
        }

        /// <summary>
        /// 現場メモ分類チェック
        /// </summary>
        internal bool CheckGenbamemoBunrui()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var genbamemoBunruiCd = this.form.GENBAMEMO_BUNRUI_CD.Text;

                // 入力されていない場合
                if (string.IsNullOrEmpty(genbamemoBunruiCd))
                {
                    // 関連項目クリア
                    this.form.GENBAMEMO_BUNRUI_NAME_RYAKU.Text = string.Empty;

                    return true;
                }

                // 現場メモ分類を取得
                bool catchErr = true;
                var genbamemoBunrui = this.GetGenbamemoBunrui(genbamemoBunruiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (genbamemoBunrui == null)
                {
                    // 現場メモ分類設定
                    this.form.GENBAMEMO_BUNRUI_CD.IsInputErrorOccured = true;
                    this.form.GENBAMEMO_BUNRUI_NAME_RYAKU.Clear();
                    this.msgLogic.MessageBoxShow("E020", "現場メモ分類");
                    this.form.GENBAMEMO_BUNRUI_CD.SelectAll();

                    return false;
                }

                // 現場メモ分類を設定
                this.form.GENBAMEMO_BUNRUI_NAME_RYAKU.Text = genbamemoBunrui.GENBAMEMO_BUNRUI_NAME_RYAKU;
                
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 取引先チェック
        /// </summary>
        internal bool CheckTorihikisaki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;

                // 入力されていない場合
                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    // 関連項目クリア
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;

                    return true;
                }

                // 取引先を取得
                bool catchErr = true;
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (torihikisaki == null)
                {
                    // 取引先名設定
                    this.form.TORIHIKISAKI_CD.IsInputErrorOccured = true;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Clear();
                    this.msgLogic.MessageBoxShow("E020", "取引先");
                    this.form.TORIHIKISAKI_CD.SelectAll();

                    return false;
                }
                // 取引先を設定
                if (!this.form.dicControl.ContainsKey("TORIHIKISAKI_CD") || !this.form.dicControl["TORIHIKISAKI_CD"].Equals(torihikisakiCd))
                {
                    // 取引先名設定
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #region 業者チェック

        private string beforeGyoushaCd = string.Empty;

        /// <summary>
        /// 業者チェック
        /// </summary>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var torihikisakiName = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var gyoushaName = this.form.GYOUSHA_NAME_RYAKU.Text;

                // 前回値と同じ場合は処理しない。
                if (this.beforeGyoushaCd.Equals(gyoushaCd))
                {
                    return true;
                }
                else
                {
                    // 現場をクリア
                    this.form.GENBA_CD.Clear();
                    this.form.GENBA_NAME_RYAKU.Clear();
                    // 現場の前回値を保持する。
                    this.beforeGenbaCd = this.form.GENBA_CD.Text;
                }

                // 入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    // 関連項目クリア
                    this.form.GYOUSHA_NAME_RYAKU.Text = String.Empty;

                    // 現場をクリア
                    this.form.GENBA_CD.Clear();
                    this.form.GENBA_NAME_RYAKU.Clear();

                    this.beforeGyoushaCd = gyoushaCd;

                    return true;
                }

                // 業者を取得
                // (入力エラーチェックは前回値と比較する前に行っているためここではチェックしない）
                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    this.beforeGyoushaCd = gyoushaCd;
                    return false;
                }
                if (gyousha == null)
                {
                    // 取引先名設定
                    this.form.GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.GYOUSHA_NAME_RYAKU.Clear();
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.form.GYOUSHA_CD.SelectAll();

                    return false;
                }

                // 業者を設定
                if (!this.form.dicControl.ContainsKey("GYOUSHA_CD") || !this.form.dicControl["GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)gyousha.SHOKUCHI_KBN)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 取引先を再設定
                // 取引先を取得
                var torihikisaki = this.GetTorihikisaki(gyousha.TORIHIKISAKI_CD, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (torihikisaki != null)
                {
                    this.form.TORIHIKISAKI_CD.Text = gyousha.TORIHIKISAKI_CD;

                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                }
                else
                {
                    this.form.TORIHIKISAKI_CD.Clear();
                    this.form.TORIHIKISAKI_NAME_RYAKU.Clear();
                }

                this.beforeGyoushaCd = gyoushaCd;

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 現場チェック

        private string beforeGenbaCd = string.Empty;

        /// <summary>
        /// 現場チェック
        /// </summary>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var genbaCd = this.form.GENBA_CD.Text;

                // 前回値と同じ場合は処理しない。
                if (this.beforeGenbaCd.Equals(genbaCd))
                {
                    return true;
                }

                // 入力されてない場合
                if (String.IsNullOrEmpty(genbaCd))
                {
                    this.form.GENBA_NAME_RYAKU.Clear();

                    this.beforeGenbaCd = genbaCd;

                    return true;
                }

                // 業者入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    this.msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = String.Empty;
                    this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                    this.isInputError = true;
                    this.form.GENBA_CD.SelectAll();

                    this.isInputError = true;
                    return false;
                }

                // 現場情報を取得
                if (this.GetGenbaList(gyoushaCd, genbaCd).Count() == 0)
                {
                    // マスタに現場が存在しない場合
                    // 現場の関連情報をクリア
                    this.form.GENBA_CD.IsInputErrorOccured = true;
                    this.form.GENBA_NAME_RYAKU.Clear();
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.SelectAll();

                    this.isInputError = true;
                    return false;
                }

                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    this.beforeGenbaCd = genbaCd;

                    return false;
                }
                var genba = this.GetGenba(genbaCd, gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    this.beforeGenbaCd = genbaCd;

                    return false;
                }

                //マスタに業者CDが存在しない場合
                //又は取引日外の業者CDが選択された場合

                if (null == genba)
                {
                    // 現場の関連情報をクリア
                    this.form.GENBA_NAME_RYAKU.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E062", "業者");

                    this.isInputError = true;
                    return false;
                }
                else
                {
                    // 現場が見つかったので現場名などをセット
                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)genba.SHOKUCHI_KBN)
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME1;
                    }
                    else
                    {
                        this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                    }
                    torihikisakiCd = genba.TORIHIKISAKI_CD;
                }

                // 業者を設定
                if (!this.form.dicControl.ContainsKey("GYOUSHA_CD") || !this.form.dicControl["GYOUSHA_CD"].Equals(gyoushaCd))
                {
                    // 業者名
                    if (gyousha.SHOKUCHI_KBN.IsTrue)
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME1;
                    }
                    else
                    {
                        this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 取引先を取得
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != torihikisaki)
                {
                    // 取引先設定
                    this.form.TORIHIKISAKI_CD.Text = torihikisaki.TORIHIKISAKI_CD;

                    // 20151021 katen #13337 品名手入力に関する機能修正 start
                    if ((bool)torihikisaki.SHOKUCHI_KBN)
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    }
                    else
                    {
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = torihikisaki.TORIHIKISAKI_NAME_RYAKU;
                    }
                    // 20151021 katen #13337 品名手入力に関する機能修正 end
                }
                else
                {
                    this.form.TORIHIKISAKI_CD.Clear();
                    this.form.TORIHIKISAKI_NAME_RYAKU.Clear();
                }

                this.beforeGenbaCd = genbaCd;

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 前の受付番号を取得
        /// <summary>
        /// 前の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>前の受付番号</returns>
        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)

                selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " < " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 ";

                // データ取得
                dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetPreviousNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }
        #endregion

        #region 次の受付番号を取得
        /// <summary>
        /// 次の受付番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">受付番号</param>
        /// <returns>次の受付番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = true;

            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue);

                DataTable dt = new DataTable();
                string selectStr;
                if (String.IsNullOrEmpty(numberValue))
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                    returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
                    return returnVal;
                }

                // SQL文作成(冗長にならないためsqlファイルで管理しない)
                selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE " + fieldName + " > " + long.Parse(numberValue);
                selectStr += "   AND DELETE_FLG = 0 ";

                // データ取得
                dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    // データ取得
                    dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                }

                // MAX_UKETSUKE_NUMBERをセット
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetNextNumber", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }

            return returnVal;
        }
        #endregion

        /// <summary>
        /// オプション対応時にファイルアップロードボタンを表示
        /// </summary>
        private void FileUploadButtonSetting()
        {
            // オプション非対応
            if (!AppConfig.AppOptions.IsFileUpload())
            {
                // ファイルアップロードボタン無効化
                this.parentForm.bt_process1.Text = string.Empty;
                this.parentForm.bt_process1.Enabled = false;
            }
            else
            {
                // オプション非対応（現場メモ）
                if (!AppConfig.AppOptions.IsFileUploadGenbaMemo())
                {
                    // ファイルアップロードボタン無効化
                    this.parentForm.bt_process1.Text = string.Empty;
                    this.parentForm.bt_process1.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 発生元のポップアップ用データ作成
        /// </summary>
        /// <returns></returns>
        public bool CheckListPopup()
        {
            try
            {
                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                DataTable dt = new DataTable();
                dt.Columns.Add("CD", typeof(string));
                dt.Columns.Add("VALUE", typeof(string));
                dt.Columns[0].ReadOnly = true;
                dt.Columns[1].ReadOnly = true;
                DataRow row;

                // 1
                row = dt.NewRow();
                row["CD"] = "1";
                row["VALUE"] = "発生元無し";
                dt.Rows.Add(row);
                // 2
                row = dt.NewRow();
                row["CD"] = "2";
                row["VALUE"] = "収集受付";
                dt.Rows.Add(row);
                // 3
                row = dt.NewRow();
                row["CD"] = "3";
                row["VALUE"] = "出荷受付";
                dt.Rows.Add(row);
                // 4
                row = dt.NewRow();
                row["CD"] = "4";
                row["VALUE"] = "持込受付";
                dt.Rows.Add(row);
                // 3
                row = dt.NewRow();
                row["CD"] = "5";
                row["VALUE"] = "定期配車";
                dt.Rows.Add(row);

                form.table = dt;

                form.PopupTitleLabel = "発生元";
                form.PopupGetMasterField = "CD,VALUE";
                form.PopupDataHeaderTitle = new string[] { "発生元CD", "発生元名" };
                form.ShowDialog();
                if (form.ReturnParams != null)
                {
                    this.form.HASSEIMOTO_CD.Text = form.ReturnParams[0][0].Value.ToString();
                    this.form.HASSEIMOTO_NAME.Text = form.ReturnParams[1][0].Value.ToString();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckListPopup", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 発生元番号の存在有無チェック
        /// </summary>
        public bool checkHasseimotoNumber()
        {
            string hasseimotoCd = this.form.HASSEIMOTO_CD.Text;
            // 発生元が空の場合は処理なし
            if (string.IsNullOrEmpty(hasseimotoCd))
            {
                return true;
            }

            string number = this.form.HASSEIMOTO_NUMBER.Text;
            // 発生元番号が空の場合は処理なし
            if (string.IsNullOrEmpty(number))
            {
                // 発生元番号を前回値の保持する。
                this.beforeHsseimotoNumber = number;

                return true;
            }

            DataTable dt = new DataTable();
            string selectStr = string.Empty;

            switch (hasseimotoCd)
            {
                case "2":
                    // 収集受付
                    selectStr = "SELECT UKETSUKE_NUMBER FROM T_UKETSUKE_SS_ENTRY ";
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    selectStr += "   AND UKETSUKE_NUMBER = " + number;

                    break;
                case "3":
                    // 出荷受付
                    selectStr = "SELECT UKETSUKE_NUMBER FROM T_UKETSUKE_SK_ENTRY ";
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    selectStr += "   AND UKETSUKE_NUMBER = " + number;

                    break;
                case "4":
                    // 持込受付
                    selectStr = "SELECT UKETSUKE_NUMBER FROM T_UKETSUKE_MK_ENTRY ";
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    selectStr += "   AND UKETSUKE_NUMBER = " + number;

                    break;
                case "5":
                    // 定期配車
                    selectStr = "SELECT TEIKI_HAISHA_NUMBER FROM T_TEIKI_HAISHA_ENTRY ";
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    selectStr += "   AND TEIKI_HAISHA_NUMBER = " + number;

                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(selectStr))
            {
                // データを取得する。
                dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    // 発生元番号を前回値の保持する。
                    this.beforeHsseimotoNumber = number;

                    return true;
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E045");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 発生元明細番号の存在有無チェック
        /// </summary>
        public bool checkHasseimotoMeisaiNumber()
        {
            string hasseimotoCd = this.form.HASSEIMOTO_CD.Text;
            // 発生元が空の場合は処理なし
            if (string.IsNullOrEmpty(hasseimotoCd))
            {
                return true;
            }

            string meisaiNumber = this.form.HASSEIMOTO_MEISAI_NUMBER.Text;
            // 発生元明細番号が空の場合は処理なし
            if (string.IsNullOrEmpty(meisaiNumber))
            {
                // 発生元明細番号を前回値に保持する。
                this.beforeHsseimotoMeisaiNumber = meisaiNumber;

                return true;
            }

            string number = this.form.HASSEIMOTO_NUMBER.Text;
            // 発生元番号が空の場合はエラー
            if (string.IsNullOrEmpty(number))
            {
                this.msgLogic.MessageBoxShowError("発生元番号を入力してください");
                this.form.HASSEIMOTO_NUMBER.Focus();
                this.form.HASSEIMOTO_NUMBER.SelectAll();
                return true;
            }

            DataTable dt = new DataTable();
            string selectStr = string.Empty;

            if (hasseimotoCd.Equals("5"))
            {
                selectStr = "SELECT entity.TEIKI_HAISHA_NUMBER FROM T_TEIKI_HAISHA_ENTRY entity ";
                selectStr += " LEFT JOIN T_TEIKI_HAISHA_DETAIL detail ";
                selectStr += "        ON entity.SYSTEM_ID = detail.SYSTEM_ID ";
                selectStr += "       AND entity.SEQ = detail.SEQ ";
                selectStr += " WHERE entity.DELETE_FLG = 0 ";
                selectStr += "   AND entity.TEIKI_HAISHA_NUMBER = " + number;
                selectStr += "   AND detail.ROW_NUMBER = " + meisaiNumber;
            }

            if (!string.IsNullOrEmpty(selectStr))
            {
                // データを取得する。
                dt = this.genbamemoEntryDAO.getdateforstringsql(selectStr);
                if (dt.Rows.Count > 0)
                {
                    // 発生元番号、発生元明細番号を前回値に保持する。
                    this.beforeHsseimotoNumber = number;
                    this.beforeHsseimotoMeisaiNumber = meisaiNumber;

                    return true;
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E045");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        #region 未使用
        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 物理削除
        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
