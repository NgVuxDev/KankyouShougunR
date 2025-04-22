// $Id: LogicCls.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using Shougun.Core.ExternalConnection.GenbamemoIchiran.CustomControls_Ex;
using System.Data.SqlTypes;
using CommonChouhyouPopup.App;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ExternalConnection.GenbamemoIchiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.GenbamemoIchiran.Setting.ButtonSetting.xml";
        private readonly string executeSqlFilePath = "Shougun.Core.ExternalConnection.GenbamemoIchiran.Sql.GetGenbaDataSql.sql";
        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeaderForm headForm;

        /// <summary>
        /// 検索用SQL
        /// </summary>
        public string searchSql { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 一覧検索用のDao
        /// </summary>
        private DAOClass mDetailDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// 拠点Dao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        /// <summary>
        /// 拠点CD
        /// </summary>
        public string strKYOTEN_CD = string.Empty;

        //2014/01/28 修正 仕様変更 qiao start
        /// <summary>
        /// 伝票種類
        /// </summary>
        public string strDenPyouSyurui = string.Empty;
        //2014/01/28 修正 仕様変更 qiao end

        /// <summary>
        /// 配車状況で使用するデータ
        /// </summary>
        private DataTable haishaJokyoDataTable;

        // No.3822-->
        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// タブストップ用
        /// </summary>
        private string[] tabUiFormControlNames = 
        {   "txtNum_DenPyouSyurui","HAISHA_JOKYO_CD","HAISHA_SHURUI_CD","txtNum_HidukeSentaku","HIDUKE_FROM","HIDUKE_TO",
            "cmbShimebi","cmbShihariaShimebi","TORIHIKISAKI_CD","GYOUSHA_CD","GENBA_CD","UNPAN_GYOUSHA_CD",//CongBinh 20200331 #134987
            "NIOROSHI_GYOUSHA_CD","NIOROSHI_GENBA_CD","customDataGridView1",
            "bt_ptn1","bt_ptn2","bt_ptn3","bt_ptn4","bt_ptn5"
        };
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }
        /// <summary>
        /// 検索条件
        /// </summary>
        public string searchString { get; set; }
        /// <summary>
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public int denShu_Kbn { get; set; }

        private M_SYS_INFO sysInfoEntity;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);
                this.form = targetForm;
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.mDetailDao = DaoInitUtility.GetComponent<DAOClass>();
                this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
                // Utility
                this.controlUtil = new ControlUtility();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(targetForm);
            }
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                // 親フォームオブジェクト取得
                parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダフォームオブジェクト取得
                headForm = (UIHeaderForm)parentForm.headerForm;
                headForm.logic = this;    // No.3123
                this.headForm.form = this.form;

                //ボタンのテキストを初期化
                this.ButtonInit();
                //イベントの初期化処理
                this.EventInit();
                
                this.allControl = this.form.allControl;
                //Start Sontt 20150710
                this.form.bt_ptn1.Location = new Point(this.form.bt_ptn1.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn2.Location = new Point(this.form.bt_ptn2.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn3.Location = new Point(this.form.bt_ptn3.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn4.Location = new Point(this.form.bt_ptn4.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                this.form.bt_ptn5.Location = new Point(this.form.bt_ptn5.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 30);
                //End Sontt 20150710
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// header設定
        /// </summary>
        public void SetHeader(UIHeaderForm headForm)
        {
            try
            {
                LogUtility.DebugMethodStart(headForm);
                this.headForm = headForm;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(headForm);
            }
        }

        /// <summary>
        /// 画面初期表示
        /// </summary>
        public void InitFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //並び替えと明細の設定
                this.form.customSearchHeader1.Visible = true;
                this.form.customSearchHeader1.Location = new System.Drawing.Point(0, 158);
                this.form.customSearchHeader1.Size = new System.Drawing.Size(992, 26);
                this.form.customSortHeader1.Location = new System.Drawing.Point(0, 184);
                this.form.customSortHeader1.Size = new Size(992, 26);
                //明細部：　ブランク
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.TabIndex = 60;
                this.form.customDataGridView1.Location = new Point(0, 209);
                this.form.customDataGridView1.Size = new Size(992, 230);
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();
                //headForm初期
                Init_HeadForm();
                
                // 検索条件の初期化
                this.Init_Heaher();
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitFrom", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// HeadForm初期表示
        /// </summary>
        public void Init_HeadForm()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //アラート件数:システム情報を取得する
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
                if (sysInfo != null)
                {
                    //システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                    this.headForm.alertNumber.Text = this.alertCount.ToString();
                }
                //読込データ件数：０ [件]
                this.headForm.readDataNumber.Text = "0";
            }
            catch (Exception ex)
            {
                LogUtility.Error("Init_HeadForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面Heaher初期表示
        /// </summary>
        public void Init_Heaher(bool initial = true)
        {
            try
            {
                LogUtility.DebugMethodStart();
                //汎用検索 : ブランク  
                this.form.searchString.Text = string.Empty;
                // 表示区分
                this.form.txtNum_HyoujiKubunSentaku.Text = "1";
                this.form.radbtnHyoujiKubunAll.Checked = true;
                // 現場メモ分類
                this.form.GENBAMEMO_BUNRUI_CD.Text = string.Empty;
                this.form.GENBAMEMO_BUNRUI_NAME_RYAKU.Text = string.Empty;
                //取引先CD、取引先名称 ： ブランク                
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME.Text = string.Empty;
                //業者CD、業者 : ブランク
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME.Text = string.Empty;
                //現場CD、現場 : ブランク               
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME.Text = string.Empty;
                this.form.testGENBA_CD.Text = string.Empty;
                // 発生元
                this.form.txtNum_HasseimotoSentaku.Text = "1";
                this.form.radbtnHasseimotoAll.Checked = true;
                this.form.chkHasseimotoNashi.Checked = true;
                this.form.chkShuushuuUketsuke.Checked = true;
                this.form.chkShukkaUketsuke.Checked = true;
                this.form.chkMochikomiUketsuke.Checked = true;
                this.form.chkTeikiHaisha.Checked = true;
                // 発生元番号
                this.form.HASSEIMOTO_NUMBER.Text = string.Empty;
                // 発生元明細No
                this.form.HASSEIMOTO_MEISAI_NUMBER.Text = string.Empty;
                // 初回登録者
                this.form.txtNum_ShokaiTourokushaSentaku.Text = "1";
                this.form.radbtnTourokushaAll.Checked = true;
                this.form.SHAIN_CD.Text = string.Empty;
                this.form.SHAIN_NAME.Text = string.Empty;
                //初回登録日(from) ：当日
                this.form.TourokuDate_From.Value = parentForm.sysDate;
                //初回登録日(to): 当日
                this.form.TourokuDate_To.Value = parentForm.sysDate;
                //並び順をクリア
                this.form.customSortHeader1.ClearCustomSortSetting();
                this.form.customSearchHeader1.ClearCustomSearchSetting();
                if (!initial)
                {
                    // Init_HeadForm呼んでないのでここでHeadFormの部分クリア
                    //読込データ件数：０ [件]
                    this.headForm.readDataNumber.Text = "0";
                }
                
                // 遷移元画面からのパラメータがある場合には設定する。
                if (this.form.paramEntry != null)
                {
                    //初回登録日(from) ：ブランク
                    this.form.TourokuDate_From.Value = string.Empty;
                    // 取引先一覧の場合
                    if (this.form.winId.Equals(WINDOW_ID.M_TORIHIKISAKI_ICHIRAN.ToString()))
                    {
                        this.form.TORIHIKISAKI_CD.Text = this.form.paramEntry.TORIHIKISAKI_CD;
                        this.form.TORIHIKISAKI_NAME.Text = this.form.paramEntry.TORIHIKISAKI_NAME;
                        this.form.txtNum_HasseimotoSentaku.Text = "2";
                        this.form.radbtnHasseimotoShitei.Checked = true;
                        this.form.chkHasseimotoNashi.Checked = true;
                        this.form.chkShuushuuUketsuke.Checked = false;
                        this.form.chkShukkaUketsuke.Checked = false;
                        this.form.chkMochikomiUketsuke.Checked = false;
                        this.form.chkTeikiHaisha.Checked = false;
                    }
                    // 業者一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.M_GYOUSHA_ICHIRAN.ToString()))
                    {
                        this.form.GYOUSHA_CD.Text = this.form.paramEntry.GYOUSHA_CD;
                        this.form.GYOUSHA_NAME.Text = this.form.paramEntry.GYOUSHA_NAME;
                        this.form.txtNum_HasseimotoSentaku.Text = "2";
                        this.form.radbtnHasseimotoShitei.Checked = true;
                        this.form.chkHasseimotoNashi.Checked = true;
                        this.form.chkShuushuuUketsuke.Checked = false;
                        this.form.chkShukkaUketsuke.Checked = false;
                        this.form.chkMochikomiUketsuke.Checked = false;
                        this.form.chkTeikiHaisha.Checked = false;
                    }
                    // 現場一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.M_GENBA_ICHIRAN.ToString()))
                    {
                        this.form.GYOUSHA_CD.Text = this.form.paramEntry.GYOUSHA_CD;
                        this.form.GYOUSHA_NAME.Text = this.form.paramEntry.GYOUSHA_NAME;
                        this.form.GENBA_CD.Text = this.form.paramEntry.GENBA_CD;
                        this.form.GENBA_NAME.Text = this.form.paramEntry.GENBA_NAME;
                        this.form.txtNum_HasseimotoSentaku.Text = "2";
                        this.form.radbtnHasseimotoShitei.Checked = true;
                        this.form.chkHasseimotoNashi.Checked = true;
                        this.form.chkShuushuuUketsuke.Checked = false;
                        this.form.chkShukkaUketsuke.Checked = false;
                        this.form.chkMochikomiUketsuke.Checked = false;
                        this.form.chkTeikiHaisha.Checked = false;
                    }
                    // 受付一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.T_UKETSUKE_ICHIRAN.ToString()))
                    {
                        this.form.txtNum_HasseimotoSentaku.Text = "2";
                        this.form.radbtnHasseimotoShitei.Checked = true;

                        String table = "";
                        if (this.form.paramEntry.HASSEIMOTO_CD.Equals("2"))
                        {
                            this.form.chkHasseimotoNashi.Checked = false;
                            this.form.chkShuushuuUketsuke.Checked = true;
                            this.form.chkShukkaUketsuke.Checked = false;
                            this.form.chkMochikomiUketsuke.Checked = false;
                            this.form.chkTeikiHaisha.Checked = false;

                            table = "T_UKETSUKE_SS_ENTRY";
                        }
                        else if (this.form.paramEntry.HASSEIMOTO_CD.Equals("3"))
                        {
                            this.form.chkHasseimotoNashi.Checked = false;
                            this.form.chkShuushuuUketsuke.Checked = false;
                            this.form.chkShukkaUketsuke.Checked = true;
                            this.form.chkMochikomiUketsuke.Checked = false;
                            this.form.chkTeikiHaisha.Checked = false;

                            table = "T_UKETSUKE_SK_ENTRY";
                        }
                        else if (this.form.paramEntry.HASSEIMOTO_CD.Equals("4"))
                        {
                            this.form.chkHasseimotoNashi.Checked = false;
                            this.form.chkShuushuuUketsuke.Checked = false;
                            this.form.chkShukkaUketsuke.Checked = false;
                            this.form.chkMochikomiUketsuke.Checked = true;
                            this.form.chkTeikiHaisha.Checked = false;

                            table = "T_UKETSUKE_MK_ENTRY";
                        }

                        // システムIDから受付番号を取得する。
                        string sql = string.Format("SELECT UKETSUKE_NUMBER "
                                           + "FROM " + table
                                           + " WHERE SYSTEM_ID = {0} "
                                           + "  AND DELETE_FLG = 0"
                                            , this.form.paramEntry.HASSEIMOTO_SYSTEM_ID.ToString());
                        // 検索
                        DataTable dt = this.mDetailDao.getdateforstringsql(sql);
                        if (dt.Rows.Count != 0)
                        {
                            this.form.HASSEIMOTO_NUMBER.Text = dt.Rows[0]["UKETSUKE_NUMBER"].ToString();
                        }
                    }
                    // 定期配車一覧の場合
                    else if (this.form.winId.Equals(WINDOW_ID.T_TEIKI_HAISHA_ICHIRAN.ToString()))
                    {
                        this.form.txtNum_HasseimotoSentaku.Text = "2";
                        this.form.radbtnHasseimotoShitei.Checked = true;
                        this.form.chkHasseimotoNashi.Checked = false;
                        this.form.chkShuushuuUketsuke.Checked = false;
                        this.form.chkShukkaUketsuke.Checked = false;
                        this.form.chkMochikomiUketsuke.Checked = false;
                        this.form.chkTeikiHaisha.Checked = true;

                        if (!this.form.paramEntry.HASSEIMOTO_DETAIL_SYSTEM_ID.IsNull)
                        {
                            // 明細システムIDから、定期配車番号と明細番号を取得する。
                            string sql = string.Format("SELECT DTL.TEIKI_HAISHA_NUMBER, DTL.ROW_NUMBER "
                                                + "FROM T_TEIKI_HAISHA_DETAIL DTL "
                                                + "INNER JOIN T_TEIKI_HAISHA_ENTRY ENT "
                                                + "        ON DTL.SYSTEM_ID = ENT.SYSTEM_ID "
                                                + "       AND DTL.SEQ = ENT.SEQ "
                                                + "       AND ENT.DELETE_FLG = 0 "
                                                + "WHERE DTL.DETAIL_SYSTEM_ID = {0} "
                                                 , this.form.paramEntry.HASSEIMOTO_DETAIL_SYSTEM_ID.ToString());
                            DataTable dt = this.mDetailDao.getdateforstringsql(sql);
                            if (dt.Rows.Count != 0)
                            {
                                this.form.HASSEIMOTO_NUMBER.Text = dt.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();
                                this.form.HASSEIMOTO_MEISAI_NUMBER.Text = dt.Rows[0]["ROW_NUMBER"].ToString();
                            }
                        }
                        else
                        {
                            // システムIDから、定期配車番号を取得する。
                            string sql = string.Format("SELECT DISTINCT TEIKI_HAISHA_NUMBER "
                                                + "FROM T_TEIKI_HAISHA_ENTRY "
                                                + "WHERE DELETE_FLG = 0 "
                                                + " AND SYSTEM_ID = {0} "
                                                 , this.form.paramEntry.HASSEIMOTO_SYSTEM_ID.ToString());
                            DataTable dt = this.mDetailDao.getdateforstringsql(sql);
                            if (dt.Rows.Count != 0)
                            {
                                this.form.HASSEIMOTO_NUMBER.Text = dt.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();
                            }
                        }
                    }
                }

                // 業者の前回値に保持する。
                this.form.testGYOUSHA_CD.Text = this.form.GYOUSHA_CD.Text;
                // 現場の前回値に保持する。
                this.form.testGENBA_CD.Text = this.form.GENBA_CD.Text;

                //フォーカス移動
                if (this.form.searchString.Visible == true)
                {
                    this.form.searchString.Focus();
                }
                else
                {
                    this.form.TORIHIKISAKI_CD.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Init_Heaher", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);
            string result = string.Empty;
            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }
            LogUtility.DebugMethodEnd(result);
            return result;
        }

        #endregion

        #region ボタンの初期化
        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //Functionボタンのイベント生成
                parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              // 新規
                parentForm.bt_func3.Click += new EventHandler(this.bt_func3_Click);              // 修正
                parentForm.bt_func4.Click += new EventHandler(this.bt_func4_Click);              // 削除
                parentForm.bt_func5.Click += new EventHandler(this.bt_func5_Click);              // 複写
                parentForm.bt_func6.Click += new EventHandler(this.bt_func6_Click);              // CSV出力
                parentForm.bt_func7.Click += new EventHandler(this.bt_func7_Click);              // 条件クリア
                parentForm.bt_func8.Click += new EventHandler(this.bt_func8_Click);              // 検索
                parentForm.bt_func10.Click += new EventHandler(this.bt_func10_Click);            // 並び替え
                parentForm.bt_func11.Click += new EventHandler(this.bt_func11_Click);            // フィルタ
                parentForm.bt_func12.Click += new EventHandler(this.bt_func12_Click);            //閉じる
                parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             // パターン一覧画面へ遷移
                //明細画面上でダブルクリック時のイベント生成
                this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick);
                this.form.customDataGridView1.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(customDataGridView1_ColumnHeaderMouseClick);
                // 20141127 teikyou ダブルクリックを追加する　start
                //this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
                // 20141127 teikyou ダブルクリックを追加する　end
                //受入出荷画面サイズ選択取得
                HearerSysInfoInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            // システム情報を取得し、初期値をセットする
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
            }
        }

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var buttonSetting = new ButtonSetting();
                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // 日付チェック
                if (CheckDate())
                {
                    return 0;
                }
                //検索用SQLの作成
                this.MakeSearchCondition();
                //検索実行
                this.searchResult = new DataTable();
                if (!string.IsNullOrEmpty(this.searchSql))
                {
                    this.searchResult = mDetailDao.getdateforstringsql(this.searchSql);
                    int count = searchResult.Rows.Count;
                    //検索結果が存在しませんの場合、メッセージを表示する
                    if (count == 0)
                    {
                        //検索結果を表示する
                        this.form.ShowData(searchResult);
                        //DataGridViewのプロパティ再設定
                        setDataGridView();
                        //読込データ件数を0にする
                        this.headForm.readDataNumber.Text = count.ToString();
                        MessageBoxUtility.MessageBoxShow("C001");
                        if (this.form.searchString.Visible)
                        {
                            this.form.searchString.Focus();
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_CD.Focus();
                        }
                        return 0;
                    }
                    else
                    {
                        //読込データ件数：０ [件]
                        this.headForm.readDataNumber.Text = "0";
                        //検索結果を表示する
                        this.form.ShowData(searchResult);
                        //読込データ件数の設定
                        this.headForm.readDataNumber.Text = count.ToString();
                        //DataGridViewのプロパティ再設定
                        setDataGridView();
                        //thongh 2015/09/14 #13032 start
                        //読込データ件数の設定
                        if (this.form.customDataGridView1 != null)
                        {
                            this.headForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                        }
                        else
                        {
                            this.headForm.readDataNumber.Text = "0";
                        }
                        //thongh 2015/09/14 #13032 end
                        return searchResult.Rows.Count;
                    }
                }
                else
                {
                    this.form.customDataGridView1.DataSource = null;
                    this.form.customDataGridView1.Columns.Clear();
                    //読込データ件数：０ [件]
                    this.headForm.readDataNumber.Text = "0";
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索用SQLの作成
        /// </summary>
        private void MakeSearchCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //SQL文格納StringBuilder
                var sql = new StringBuilder();
                //SELECT句未取得なら検索できない
                if (string.IsNullOrEmpty(this.form.SelectQuery))
                {
                    this.headForm.readDataNumber.Text = "0";
                    this.searchSql = string.Empty;
                    return;
                }

                #region SELECT句
                var isDetail = this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI;
                sql.Append(" SELECT DISTINCT ");
                //出力パターンよりのSQL
                sql.Append(this.form.SelectQuery);
                //システムID
                sql.AppendFormat(" ,T_GENBAMEMO_ENTRY.SYSTEM_ID AS {0} ", ConstCls.HIDDEN_SYSTEM_ID);
                //枝番
                sql.AppendFormat(" ,T_GENBAMEMO_ENTRY.SEQ AS {0} ", ConstCls.HIDDEN_SEQ);
                #endregion

                #region FROM句
                sql.Append(" FROM T_GENBAMEMO_ENTRY ");
                if (isDetail)
                {
                    sql.Append(" LEFT JOIN T_GENBAMEMO_DETAIL ");
                    sql.Append("        ON T_GENBAMEMO_ENTRY.SYSTEM_ID = T_GENBAMEMO_DETAIL.SYSTEM_ID ");
                    sql.Append("       AND T_GENBAMEMO_ENTRY.SEQ = T_GENBAMEMO_DETAIL.SEQ ");
                }
                sql.Append(" LEFT JOIN M_SHAIN ");
                sql.Append("        ON T_GENBAMEMO_ENTRY.SHOKAI_TOUROKUSHA_CD = M_SHAIN.SHAIN_CD ");
                sql.Append("       AND M_SHAIN.DELETE_FLG = 0 ");

                sql.Append(" LEFT JOIN T_UKETSUKE_SS_ENTRY ");
                sql.Append("        ON T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID = T_UKETSUKE_SS_ENTRY.SYSTEM_ID ");
                sql.Append("       AND T_UKETSUKE_SS_ENTRY.DELETE_FLG = 0 ");

                sql.Append(" LEFT JOIN T_UKETSUKE_SK_ENTRY ");
                sql.Append("        ON T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID = T_UKETSUKE_SK_ENTRY.SYSTEM_ID ");
                sql.Append("       AND T_UKETSUKE_SK_ENTRY.DELETE_FLG = 0 ");

                sql.Append(" LEFT JOIN T_UKETSUKE_MK_ENTRY ");
                sql.Append("        ON T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID = T_UKETSUKE_MK_ENTRY.SYSTEM_ID ");
                sql.Append("       AND T_UKETSUKE_MK_ENTRY.DELETE_FLG = 0 ");

                sql.Append(" LEFT JOIN (SELECT DTL.SYSTEM_ID, DTL.DETAIL_SYSTEM_ID, DTL.TEIKI_HAISHA_NUMBER, DTL.ROW_NUMBER FROM T_TEIKI_HAISHA_DETAIL DTL ");
                sql.Append("             INNER JOIN T_TEIKI_HAISHA_ENTRY ENT ");
                sql.Append("                     ON ENT.SYSTEM_ID = DTL.SYSTEM_ID AND ENT.SEQ = DTL.SEQ ");
                sql.Append("                    AND ENT.DELETE_FLG = 0 ");
                sql.Append("           ) as TEIKI_DATA ");
                sql.Append("        ON T_GENBAMEMO_ENTRY.HASSEIMOTO_SYSTEM_ID = TEIKI_DATA.SYSTEM_ID ");
                sql.Append("       AND T_GENBAMEMO_ENTRY.HASSEIMOTO_DETAIL_SYSTEM_ID = TEIKI_DATA.DETAIL_SYSTEM_ID ");

                sql.Append(" LEFT JOIN (SELECT DTL.SYSTEM_ID, DTL.DETAIL_SYSTEM_ID, DTL.TEIKI_HAISHA_NUMBER, DTL.ROW_NUMBER, DTL.UKETSUKE_NUMBER FROM T_TEIKI_HAISHA_DETAIL DTL ");
                sql.Append("             INNER JOIN T_TEIKI_HAISHA_ENTRY ENT ");
                sql.Append("                     ON ENT.SYSTEM_ID = DTL.SYSTEM_ID AND ENT.SEQ = DTL.SEQ ");
                sql.Append("                    AND ENT.DELETE_FLG = 0 ");
                sql.Append("           ) as TEIKI_DATA_UKETSUKE ");
                sql.Append("        ON T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER = TEIKI_DATA_UKETSUKE.UKETSUKE_NUMBER ");
                
                if (AppConfig.AppOptions.IsFileUpload() && AppConfig.AppOptions.IsFileUploadGenbaMemo())
                {
                    sql.Append(" LEFT JOIN M_FILE_LINK_GENBAMEMO_ENTRY AS FLGenbamemo ");
                    sql.Append("        ON T_GENBAMEMO_ENTRY.SYSTEM_ID = FLGenbamemo.SYSTEM_ID ");
                }
                else
                {
                    sql.Append(" LEFT JOIN ( ");
                    sql.Append(" select distinct '' AS SYSTEM_ID, '' AS FILE_ID ");
                    sql.Append(" from M_SYS_INFO WHERE SYS_ID = 0) as FLGenbamemo ");
                    sql.Append(" ON T_GENBAMEMO_ENTRY.SYSTEM_ID = FLGenbamemo.SYSTEM_ID ");
                }

                // パターンから作成されたJOIN句
                sql.Append(this.form.JoinQuery);
                #endregion

                #region WHERE句
                sql.Append(" WHERE ");
                //削除フラグ
                sql.Append(" T_GENBAMEMO_ENTRY.DELETE_FLG = 0 ");
                
                //画面条件
                // 表示区分
                if (!string.IsNullOrEmpty(this.form.txtNum_HyoujiKubunSentaku.Text))
                {
                    if (this.form.txtNum_HyoujiKubunSentaku.Text.Equals("2"))
                    {
                        sql.Append(" AND T_GENBAMEMO_ENTRY.HIHYOUJI_FLG = 0 ");
                    }
                    else if (this.form.txtNum_HyoujiKubunSentaku.Text.Equals("3"))
                    {
                        sql.Append(" AND T_GENBAMEMO_ENTRY.HIHYOUJI_FLG = 1 ");
                    }
                }
                // 現場メモ分類
                if (!string.IsNullOrEmpty(this.form.GENBAMEMO_BUNRUI_CD.Text))
                {
                    sql.Append(" AND T_GENBAMEMO_ENTRY.GENBAMEMO_BUNRUI_CD = '" + this.form.GENBAMEMO_BUNRUI_CD.Text + "' ");
                }
                //画面で取引先CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    sql.Append(" AND T_GENBAMEMO_ENTRY.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
                }
                //画面で業者CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    sql.Append(" AND T_GENBAMEMO_ENTRY.GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "' ");
                }
                //画面で現場CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    sql.Append(" AND T_GENBAMEMO_ENTRY.GENBA_CD = '" + this.form.GENBA_CD.Text + "' ");
                }
                // 発生元
                if (this.form.txtNum_HasseimotoSentaku.Text.Equals("2") || this.form.txtNum_HasseimotoSentaku.Text.Equals("3"))
                {
                    List<string> hasseimotoChkList = new List<string>();
                    if (this.form.chkHasseimotoNashi.Checked)
                    {
                        hasseimotoChkList.Add("1");
                    }
                    if (this.form.chkShuushuuUketsuke.Checked)
                    {
                        hasseimotoChkList.Add("2");
                    }
                    if (this.form.chkShukkaUketsuke.Checked)
                    {
                        hasseimotoChkList.Add("3");
                    }
                    if (this.form.chkMochikomiUketsuke.Checked)
                    {
                        hasseimotoChkList.Add("4");
                    }
                    if (this.form.chkTeikiHaisha.Checked)
                    {
                        hasseimotoChkList.Add("5");
                    }

                    if (hasseimotoChkList.Count > 0)
                    {

                        sql.Append(" AND ");

                        //※収集受付チェックOFF、定期配車チェックON
                        //現場メモが作成されている受付を、コース依頼入力で定期明細に移動したデータを、
                        //現場メモ一覧で定期配車抽出条件の時に、元の受付の現場メモを引っ張ってくる
                        if (!(this.form.chkShuushuuUketsuke.Checked) && (this.form.chkTeikiHaisha.Checked))
                        {
                            sql.Append("(");
                        }

                        sql.Append("T_GENBAMEMO_ENTRY.HASSEIMOTO_CD in ( ");

                        for (int i = 0; i < hasseimotoChkList.Count; i++)
                        {
                            sql.Append("'" + hasseimotoChkList[i] + "'");
                            if (i != hasseimotoChkList.Count - 1)
                            {
                                sql.Append(" , ");
                            }
                        }

                        sql.Append(" ) ");

                        //※収集受付チェックOFF、定期配車チェックON
                        if (!(this.form.chkShuushuuUketsuke.Checked) && (this.form.chkTeikiHaisha.Checked))
                        {
                            sql.Append(" OR ");
                            sql.Append(" ( ");
                            sql.Append(" T_GENBAMEMO_ENTRY.HASSEIMOTO_CD in ('2') AND TEIKI_DATA_UKETSUKE.SYSTEM_ID IS NOT NULL");
                            sql.Append(" )) ");
                        }
                    }
                }
                // 発生元番号
                if (!string.IsNullOrEmpty(this.form.HASSEIMOTO_NUMBER.Text))
                {
                    //※収集受付チェックOFF、定期配車チェックON
                    if (!(this.form.chkShuushuuUketsuke.Checked) && (this.form.chkTeikiHaisha.Checked))
                    {
                        sql.Append(" AND (TEIKI_DATA.TEIKI_HAISHA_NUMBER = " + this.form.HASSEIMOTO_NUMBER.Text);
                        sql.Append(" OR TEIKI_DATA_UKETSUKE.TEIKI_HAISHA_NUMBER = " + this.form.HASSEIMOTO_NUMBER.Text + ")");
                    }
                    else
                    {
                        if (this.form.chkShuushuuUketsuke.Checked)
                        {
                            sql.Append(" AND T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER = " + this.form.HASSEIMOTO_NUMBER.Text);
                        }
                        if (this.form.chkShukkaUketsuke.Checked)
                        {
                            sql.Append(" AND T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER = " + this.form.HASSEIMOTO_NUMBER.Text);
                        }
                        if (this.form.chkMochikomiUketsuke.Checked)
                        {
                            sql.Append(" AND T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER = " + this.form.HASSEIMOTO_NUMBER.Text);
                        }
                        if (this.form.chkTeikiHaisha.Checked)
                        {
                            sql.Append(" AND TEIKI_DATA.TEIKI_HAISHA_NUMBER = " + this.form.HASSEIMOTO_NUMBER.Text);
                        }
                    }
                }
                // 発生元明細No
                if (!string.IsNullOrEmpty(this.form.HASSEIMOTO_MEISAI_NUMBER.Text))
                {
                    //※収集受付チェックOFF、定期配車チェックON
                    if (!(this.form.chkShuushuuUketsuke.Checked) && (this.form.chkTeikiHaisha.Checked))
                    {
                        sql.Append(" AND (TEIKI_DATA.ROW_NUMBER = " + this.form.HASSEIMOTO_MEISAI_NUMBER.Text);
                        sql.Append(" OR TEIKI_DATA_UKETSUKE.ROW_NUMBER = " + this.form.HASSEIMOTO_MEISAI_NUMBER.Text + ")");
                    }
                    else
                    {
                        sql.Append(" AND TEIKI_DATA.ROW_NUMBER = " + this.form.HASSEIMOTO_MEISAI_NUMBER.Text);
                    }
                }
                // 初回登録者
                if (!string.IsNullOrEmpty(this.form.txtNum_ShokaiTourokushaSentaku.Text))
                {
                    if (this.form.txtNum_ShokaiTourokushaSentaku.Text.Equals("2"))
                    {
                        sql.Append(" AND M_SHAIN.NYUURYOKU_TANTOU_KBN = 1 ");
                    }
                    else if (this.form.txtNum_ShokaiTourokushaSentaku.Text.Equals("3"))
                    {
                        sql.Append(" AND M_SHAIN.UNTEN_KBN = 1 ");
                    }
                    else if (this.form.txtNum_ShokaiTourokushaSentaku.Text.Equals("4"))
                    {
                        sql.Append(" AND M_SHAIN.EIGYOU_TANTOU_KBN = 1 ");
                    }
                }
                if (!string.IsNullOrEmpty(this.form.SHAIN_CD.Text))
                {
                    sql.Append(" AND T_GENBAMEMO_ENTRY.SHOKAI_TOUROKUSHA_CD = " + this.form.SHAIN_CD.Text);
                }
                // 初回登録日
                if (!string.IsNullOrEmpty(this.form.TourokuDate_From.Text))
                {
                    sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, T_GENBAMEMO_ENTRY.CREATE_DATE, 111), 120) >= '" + DateTime.Parse(this.form.TourokuDate_From.Value.ToString()).ToShortDateString() + "' ");
                }
                if (!string.IsNullOrEmpty(this.form.TourokuDate_To.Text))
                {
                    sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, T_GENBAMEMO_ENTRY.CREATE_DATE, 111), 120) <= '" + DateTime.Parse(this.form.TourokuDate_To.Value.ToString()).ToShortDateString() + "' ");
                }

                #endregion

                #region ORDERBY句
                if (!string.IsNullOrEmpty(this.form.OrderByQuery))
                {
                    sql.Append(" ORDER BY ");
                    sql.Append(this.form.OrderByQuery);
                }
                #endregion

                this.searchSql = sql.ToString();
                sql.Append("");
            }
            catch (Exception ex)
            {
                LogUtility.Error("MakeSearchCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// DataGridViewのプロパティ再設定
        /// </summary>
        private void setDataGridView()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.AllowUserToResizeColumns = true;
                setDataGridViewColumn(this.form.customDataGridView1);
                this.form.customDataGridView1.AllowUserToResizeRows = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridView", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Column非表示設定
        /// </summary>
        /// <param name="dtgv"></param>
        private void setDataGridViewColumn(CustomDataGridView dtgv)
        {
            try
            {
                LogUtility.DebugMethodStart(dtgv);
                //入力画面へ遷移用Column（システムID）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_SYSTEM_ID))
                {
                    dtgv.Columns[ConstCls.HIDDEN_SYSTEM_ID].Visible = false;
                }
                //入力画面へ遷移用Column（枝番）を非表示にする
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_SEQ))
                {
                    dtgv.Columns[ConstCls.HIDDEN_SEQ].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridViewColumn", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dtgv);
            }
        }
        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //入力画面へ遷移する（新規モード）
                FormManager.OpenFormWithAuth("G741", WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, null, null, null, null, "OFF");
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func2_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //入力画面へ遷移する（修正モード）
                    forwardNyuuryoku(WINDOW_TYPE.UPDATE_WINDOW_FLAG, "OFF");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func3_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //入力画面へ遷移する（削除モード）
                    forwardNyuuryoku(WINDOW_TYPE.DELETE_WINDOW_FLAG, "OFF");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func4_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //入力画面へ遷移する（新規モード）
                    forwardNyuuryoku(WINDOW_TYPE.NEW_WINDOW_FLAG, "ON");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func5_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    MessageBoxUtility.MessageBoxShow("E044");
                }
                else
                {
                    //CSV出力確認メッセージを表示する
                    if (MessageBoxUtility.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, ConstCls.CSV_NAME, this.form);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                this.Init_Heaher(false);
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func7_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // パターン未登録の場合検索処理を行わない
                if (this.form.PatternNo == 0)
                {
                    MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }
                //検索処理を行う
                this.Search();
                //Start Sontt 20150710
                if (this.form.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
                {
                    DataGridViewCheckBoxHeaderCell header = this.form.customDataGridView1.Columns[ConstCls.CELL_CHECKBOX].HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        header._checked = false;
                    }
                }
                //End Sontt 20150710
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F10並び替え
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //一覧に明細行がない場合
                if (this.form.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、画面遷移しない
                    MessageBoxUtility.MessageBoxShow("E076");
                }
                else
                {
                    //ソート設定ダイアログを呼び出す
                    this.form.customSortHeader1.ShowCustomSortSettingDialog();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func10_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //フィルタ設定ダイアログを呼び出す
                this.form.customSearchHeader1.ShowCustomSearchSettingDialog();
                //読込データ件数           #13032
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.readDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.readDataNumber.Text = "0";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func11_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.form.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func12_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        #endregion

        #region 明細データダブルクリックイベント
        private void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.RowIndex < 0)
                {
                    // ヘッダダブルクリックの場合は標準の動きをさせる為にここでは何もしない
                    return;
                }
                CustomDataGridView customDataGridView = (CustomDataGridView)sender;
                if (customDataGridView.RowCount != 0)
                {
                    //入力画面へ遷移する（修正モード）
                    forwardNyuuryoku(WINDOW_TYPE.UPDATE_WINDOW_FLAG, "OFF");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("customDataGridView1_MouseDoubleClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        /// <summary>
        /// ヘッダーのチェックボックスクリック時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.form.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                DataGridViewCheckBoxHeaderCell header = col.HeaderCell as DataGridViewCheckBoxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
            }
        }

        #region 画面遷移処理
        /// <summary>
        /// 入力画面へ遷移する
        /// </summary>
        private void forwardNyuuryoku(WINDOW_TYPE windowType, string hukushaFlg)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, hukushaFlg);

                String systemId = String.Empty;
                String SEQ = String.Empty;

                //選択されたレコードを取得する
                DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
                systemId = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString();

                // システムIDをもとに最大のSEQを取得する。
                T_GENBAMEMO_ENTRY entry = this.mDetailDao.GetDataBySystemId(systemId);
                if (entry != null)
                {
                    SEQ = entry.SEQ.ToString();
                }

                // 権限チェック
                if (windowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG &&
                    !r_framework.Authority.Manager.CheckAuthority("G741", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (!r_framework.Authority.Manager.CheckAuthority("G741", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        MessageBoxUtility.MessageBoxShow("E158", "修正");
                        return;
                    }
                    windowType = WINDOW_TYPE.REFERENCE_WINDOW_FLAG;
                }
                FormManager.OpenFormWithAuth("G741", windowType, windowType, systemId, SEQ, null, null, hukushaFlg);
            }
            catch (Exception ex)
            {
                LogUtility.Error("forwardNyuuryoku", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(windowType, hukushaFlg);
            }
        }

        #region プロセスボタン押下処理
        /// <summary>
        /// サブファンクションボタン[1]をクリックされたときに処理されます
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                var sysID = this.form.OpenPatternIchiran();
                // 適用ボタンが押された場合
                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData(this.form.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #endregion

        #region 関連チェック
        /// <summary>
        /// 取引先情報取得
        /// </summary>
        /// <param name="toriCd">取引先CD</param>
        /// <returns>取引先マスタエンティティのリスト（実質一つ）</returns>
        public M_TORIHIKISAKI[] GetTorihikisakiInfo(string toriCd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(toriCd);
            var toriEntitys = new M_TORIHIKISAKI[0];
            catchErr = true;
            try
            {
                var tDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
                var toriEntity = new M_TORIHIKISAKI();
                toriEntity.TORIHIKISAKI_CD = toriCd;
                toriEntity.ISNOT_NEED_DELETE_FLG = true;
                // エンティティから取引先情報を絞り込んで取得
                toriEntitys = tDao.GetAllValidData(toriEntity);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisakiInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisakiInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(toriEntitys, catchErr);
            }
            return toriEntitys;
        }

        /// <summary>
        /// 業者情報取得
        /// </summary>
        /// <param name="gosyaCd">業者CD</param>
        public M_GYOUSHA[] GetGyousyaInfo(string gosyaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gosyaCd);
                IM_GYOUSHADao gDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                M_GYOUSHA gEntity = new M_GYOUSHA();
                gEntity.GYOUSHA_CD = gosyaCd;
                gEntity.ISNOT_NEED_DELETE_FLG = true;
                //業者情報取得
                var returnEntitys = gDao.GetAllValidData(gEntity);
                LogUtility.DebugMethodEnd(returnEntitys, catchErr);
                return returnEntitys;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousyaInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousyaInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }


        /// <summary>
        /// 現場情報取得
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="gosyaCd">業者CD</param>
        /// <returns></returns>
        public M_GENBA[] GetGenbaInfo(string genbaCd, string gosyaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(genbaCd, gosyaCd);
                IM_GENBADao gDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                M_GENBA gEntity = new M_GENBA();
                //現場CD
                gEntity.GENBA_CD = genbaCd;
                //業者CD
                if (gosyaCd != "")
                {
                    gEntity.GYOUSHA_CD = gosyaCd;
                }
                gEntity.ISNOT_NEED_DELETE_FLG = true;
                //現場情報取得
                //現場マスタ（M_GENBA）を[業者CD]、[現場CD]で検索する
                M_GENBA[] returnEntitys = gDao.GetAllValidData(gEntity);
                LogUtility.DebugMethodEnd(returnEntitys, catchErr);
                return returnEntitys;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenbaInfo", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbaInfo", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
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
                IM_GENBAMEMO_BUNRUIDao gDao = DaoInitUtility.GetComponent<IM_GENBAMEMO_BUNRUIDao>();
                var genbamemoBunrui = gDao.GetAllValidData(keyEntity).FirstOrDefault();

                LogUtility.DebugMethodEnd(genbamemoBunrui, catchErr);

                return genbamemoBunrui;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenbamemoBunrui", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbamemoBunrui", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        #region 使わない

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        // No.3123-->
        /// <summary>
        /// 次のタブストップのコントロールにフォーカス移動
        /// </summary>
        /// <param name="foward"></param>
        public void GotoNextControl(bool foward)
        {
            Control control = NextFormControl(foward);
            if (control != null)
            {
                control.Focus();
            }
        }

        public void HeaderFocus()
        {
            //this.headForm.HIDUKE_TO.Focus();
        }

        /// <summary>
        /// 現在のコントロールの次のタブストップコントールを探す
        /// </summary>
        /// <param name="foward"></param>
        /// <returns></returns>
        public Control NextFormControl(bool foward)
        {
            try
            {
                Control control = null;
                bool startflg = false;
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(tabUiFormControlNames);
                if (foward == false)
                {
                    formControlNameList.Reverse();
                }
                foreach (var controlName in formControlNameList)
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    if (control != null)
                    {
                        if (startflg)
                        {
                            // 次のコントロール
                            if (control.TabStop == true && control.Visible == true && control.Enabled == true)
                            {
                                return control;
                            }
                        }
                        else if (this.form.ActiveControl != null && this.form.ActiveControl.Equals(control))
                        {   // 現在のactiveコントロ－ル
                            startflg = true;
                        }
                    }
                    else
                    {
                        control = controlUtil.FindControl(this.headForm, controlName);
                        if (control != null)
                        {
                            if (startflg)
                            {
                                // 次のコントロール
                                if (control.TabStop == true && control.Visible == true && control.Enabled == true)
                                {
                                    return control;
                                }
                            }
                            else if (this.headForm.ActiveControl != null && this.headForm.ActiveControl.Equals(control))
                            {   // 現在のactiveコントロ－ル
                                startflg = true;
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                // 最後までみつからない場合、最初から探す
                foreach (var controlName in formControlNameList)
                {
                    control = controlUtil.FindControl(this.form, controlName);
                    if (control == null)
                    {
                        control = controlUtil.FindControl(this.headForm, controlName);
                    }
                    if (control != null)
                    {
                        if (control.TabStop == true && control.Visible == true && control.Enabled == true)
                        {
                            break;
                        }
                    }
                }
                return control;
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);
                throw;
            }
        }
        // No.3123<--

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.form.TourokuDate_From.BackColor = Constans.NOMAL_COLOR;
            this.form.TourokuDate_To.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.form.TourokuDate_From.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.TourokuDate_To.Text))
            {
                return false;
            }
            DateTime date_from = DateTime.Parse(this.form.TourokuDate_From.Text);
            DateTime date_to = DateTime.Parse(this.form.TourokuDate_To.Text);
            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.TourokuDate_From.IsInputErrorOccured = true;
                this.form.TourokuDate_To.IsInputErrorOccured = true;
                this.form.TourokuDate_From.BackColor = Constans.ERROR_COLOR;
                this.form.TourokuDate_To.BackColor = Constans.ERROR_COLOR;
                string errorMsg = "日付範囲の設定を見直してください。";
                MessageBoxUtility.MessageBoxShowError(errorMsg);
                this.form.TourokuDate_From.Focus();
                return true;
            }
            return false;
        }
        #endregion
    }
}