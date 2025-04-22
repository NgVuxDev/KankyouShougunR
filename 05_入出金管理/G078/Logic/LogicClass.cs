using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.Const;
using Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.DAO;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.Const;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// 汎用、簡易検索フラグ
        /// </summary>
        private string Hanyou_Kenyi;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// DAO
        /// </summary>
        public DAOClass dao;

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// NyuuSyutuKinIchiranForm form
        /// </summary>
        private HeaderForm headForm;
        private NyuuSyutuKinIchiranForm form;
        private BusinessBaseForm footer;

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public DENSHU_KBN DenshuKbn { get; set; }
      
        /// <summary>
        /// 検索結果(header部分)
        /// </summary>
        public DataTable searchResult { get; set; }
        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// テーブル「M_OUTPUT_PATTERN_KOBETSU」のディフォール区分
        /// </summary>
        public DataTable setDefaultKbn { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public string SearchString { get; set; }

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

        public Control[] allControl;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        ///// <summary>
        ///// システム情報のDao
        ///// </summary>
        //private IM_SYS_INFODao sysInfoDao;

        //private RibbonMainMenu ribbonForm;

        //2013.12.23 naitou upd start
        /// <summary>
        /// メッセージユーティリティ
        /// </summary>
        private MessageUtility MessageUtil { get; set; }
        //2013.12.23 naitou upd end

        internal MessageBoxShowLogic errmessage { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(NyuuSyutuKinIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.MessageUtil = new MessageUtility(); //2013.12.23 naitou upd
            this.errmessage = new MessageBoxShowLogic();

            //DTO
            this.dto = new DTOClass();

            //DAO
            //this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();

            this.dao = DaoInitUtility.GetComponent<DAOClass>();

            //this.form.customDataGridView1.TabStop = false; //2013.12.23 naitou upd

            LogUtility.DebugMethodEnd();
        }

        #endregion

        //#region アラート件数を取得

        ////システム情報からアラート件数を取得(二次開発)
        //private void getAlartKensuu()
        //{
        //    LogUtility.DebugMethodStart();

        //    M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
        //    if (sysInfo != null)
        //    {
        //        this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
        //    }
        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

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

                //#region アラート件数を取得(二次開発)
                //this.getAlartKensuu();
                //#endregion

                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                // 20150917 katen #12048 「システム日付」の基準作成、適用 start
                //開始日付
                if ("".Equals(Properties.Settings.Default.SET_HIDUKE_FROM))
                {
                    this.form.header_new.HIDUKE_FROM.Text = this.footer.sysDate.ToString();
                }
                else
                {
                    this.form.header_new.HIDUKE_FROM.Text = Properties.Settings.Default.SET_HIDUKE_FROM;
                }

                //終了日付
                if ("".Equals(Properties.Settings.Default.SET_HIDUKE_TO))
                {
                    this.form.header_new.HIDUKE_TO.Text = this.footer.sysDate.ToString();
                }
                else
                {
                    this.form.header_new.HIDUKE_TO.Text = Properties.Settings.Default.SET_HIDUKE_TO;
                }
                // 20150917 katen #12048 「システム日付」の基準作成、適用 end

                // ボタンのテキストを初期化
                this.ButtonInit();

                // 伝票種類のテキストボックスを初期化しておく
                this.form.txtNum_DenpyouSyurui.Text = string.Empty;

                // イベントの初期化処理
                this.EventInit();

                //this.poPuApuName();
                //this.dtGridView();

                //Hanyou_Kenyi = "汎用";
                //this.form.searchString.Visible = false;
                //this.RegistRibbonMenu("123456", "abcd1234");
                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customSortHeader1.Location = new System.Drawing.Point(3, 79);
                this.form.customDataGridView1.Location = new System.Drawing.Point(3, 110);
                this.form.customDataGridView1.Size = new System.Drawing.Size(997, 320);

                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.headForm.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.headForm.NumberAlert = this.headForm.InitialNumberAlert;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        // リボン情報登録用メソッド
        //private void RegistRibbonMenu(string loginID, string passWord)
        //{
        //    LogUtility.DebugMethodStart(loginID, passWord);

        //    M_SHAIN[] ShainAll = DaoInitUtility.GetComponent<IM_SHAINDao>().GetAllValidData(new M_SHAIN());
        //    M_SYS_INFO SystemInformation = DaoInitUtility.GetComponent<IM_SYS_INFODao>().GetAllDataForCode("0");
        //    M_CORP_INFO CorpInformation = DaoInitUtility.GetComponent<IM_CORP_INFODao>().GetAllData().FirstOrDefault();
        //    var currentUser = ShainAll.Where(s => s.LOGIN_ID == loginID && s.PASSWORD == passWord).FirstOrDefault();
        //    this.ribbonForm = new RibbonMainMenu(null, new CommonInformation(SystemInformation, CorpInformation, currentUser));
        //    SystemProperty.UserName = currentUser.SHAIN_NAME_RYAKU;

        //    LogUtility.DebugMethodEnd();
        //}

        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            HeaderForm targetHeader = (HeaderForm)parentForm.headerForm;
            this.headForm = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面クリア
        /// </summary>
        public bool ClearScreen(String Kbn)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                switch (Kbn)
                {
                    case "Initial"://初期表示
                        //case "ClsSearchCondition"://検索条件をクリア  // No.2292

                        //タイトル
                        this.headForm.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_NYUSHUTSUKIN_ICHIRAN);

                        //アラート件数
                        this.headForm.NumberAlert = this.headForm.InitialNumberAlert;

                        //前回保存値がない場合はシステム設定ファイルから拠点CDを設定する
                        //拠点CDを取得
                        XMLAccessor fileAccess = new XMLAccessor();
                        CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                        this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                        // ユーザ拠点名称の取得
                        if (this.headForm.KYOTEN_CD.Text != null)
                        {
                            M_KYOTEN mKyoten = new M_KYOTEN();
                            mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                            if (mKyoten == null || this.headForm.KYOTEN_CD.Text == "")
                            {
                                this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                            }
                            else
                            {
                                this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                            }
                        }

                        //日付選択
                        //2013.12.15 naitou upd start
                        if (Properties.Settings.Default.SET_HIDUKESENTAKU != "")
                        {
                            this.headForm.txtNum_HidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKESENTAKU;
                        }
                        else
                        {
                            this.headForm.txtNum_HidukeSentaku.Text = "1";
                        }
                        //2013.12.15 naitou upd end

                        //検索条件
                        this.form.searchString.Clear();

                        //取引先CD
                        this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TORIHIKISAKI_CD;

                        // 取引先名称
                        M_TORIHIKISAKI mTorihikisaki = new M_TORIHIKISAKI();
                        mTorihikisaki = (M_TORIHIKISAKI)torihikisakiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                        if (mTorihikisaki == null)
                        {
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = "";
                        }
                        else
                        {
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = mTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        }

                        //伝票種類
                        SetInitialDenpyoShurui();

                        if (ConstCls.DENPYO_SHURUI_NYUKIN == this.form.txtNum_DenpyouSyurui.Text)
                        {
                            // 入金先CD
                            var nyuukinsakiCd = Properties.Settings.Default.NYUUKINSAKI_CD;
                            this.form.NYUUKINSAKI_CD.Text = nyuukinsakiCd;

                            // 入金先名称
                            M_TORIHIKISAKI nyuukinsakiEntity = new M_TORIHIKISAKI();
                            nyuukinsakiEntity = (M_TORIHIKISAKI)torihikisakiDao.GetDataByCd(nyuukinsakiCd);
                            if (null == nyuukinsakiEntity)
                            {
                                this.form.NYUUKINSAKI_NAME_RYAKU.Text = String.Empty;
                            }
                            else
                            {
                                this.form.NYUUKINSAKI_NAME_RYAKU.Text = nyuukinsakiEntity.TORIHIKISAKI_NAME_RYAKU;
                            }
                        }

                        break;

                    // No.2292
                    case "ClsSearchCondition"://検索条件をクリア
                        //アラート件数
                        this.headForm.NumberAlert = this.headForm.InitialNumberAlert;

                        //検索条件
                        this.form.searchString.Clear();

                        //入出金番号
                        this.form.NYUUKIN_NUMBER.Text = "";

                        //取引先CD
                        this.form.TORIHIKISAKI_CD.Text = "";

                        // 取引先名称
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = "";

                        // 入金先CD
                        this.form.NYUUKINSAKI_CD.Text = "";

                        // 入金先名称
                        this.form.NYUUKINSAKI_NAME_RYAKU.Text = "";

                        break;
                }

                //thongh 2015/09/14 #13030 start
                //伝票日付（From）
                //2013.12.15 naitou upd start
                if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
                {
                    this.headForm.HIDUKE_FROM.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_FROM);
                    Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                }
                else
                {
                    // 20150917 katen #12048 「システム日付」の基準作成、適用 start
                    //this.headForm.HIDUKE_FROM.Value = DateTime.Now.Date;
                    this.headForm.HIDUKE_FROM.Value = this.footer.sysDate.Date;
                    // 20150917 katen #12048 「システム日付」の基準作成、適用 end
                }
                //2013.12.15 naitou upd end

                //伝票日付（To）
                //2013.12.15 naitou upd start
                if (Properties.Settings.Default.SET_HIDUKE_TO != "")
                {
                    this.headForm.HIDUKE_TO.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_TO);
                    Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                }
                else
                {
                    // 20150917 katen #12048 「システム日付」の基準作成、適用 start
                    //this.headForm.HIDUKE_TO.Value = DateTime.Now.Date;
                    this.headForm.HIDUKE_TO.Value = this.footer.sysDate.Date;
                    // 20150917 katen #12048 「システム日付」の基準作成、適用 end
                }
                //2013.12.15 naitou upd end
                //thongh 2015/09/14 #13030 end

                //読込データ件数
                this.headForm.ReadDataNumber.Text = "0";

                //アラート件数
                this.headForm.alertNumber.Text = this.headForm.NumberAlert.ToString();

                //一覧明細をクリア
                this.form.customDataGridView1.DataSource = null;
                this.form.ShowHeader(); //2013.12.23 naitou upd

                //並び順をクリア
                this.form.customSortHeader1.ClearCustomSortSetting();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("ClearScreen", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearScreen", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        //#region ポップアップ拠点名称取得設定

        ///// <summary>
        ///// ポップアップ拠点名称取得設定
        ///// </summary>
        //private void poPuApuName()
        //{
        //    // headform【拠点】
        //    this.headForm.KYOTEN_CD.Text = Properties.Settings.Default.SET_KYOTEN_CD;
        //    this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TORIHIKISAKI_CD;
        //    if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
        //    {
        //        this.headForm.HIDUKE_FROM.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_FROM);
        //    }
        //    if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
        //    {
        //        this.headForm.HIDUKE_TO.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_TO);
        //    }
        //    // ユーザ拠点名称の取得
        //    if (this.headForm.KYOTEN_CD.Text != "")
        //    {
        //        M_KYOTEN mKyoten = new M_KYOTEN();
        //        mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
        //        if (mKyoten == null)
        //        {
        //            this.headForm.KYOTEN_NAME_RYAKU.Text = "";
        //        }
        //        else
        //        {
        //            this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
        //        }
        //    }

        //}

        //#endregion

        //#region 画面でDataGridViewのスタイル設定

        ///// <summary>
        ///// 画面でDataGridViewのスタイル設定
        ///// </summary>
        //private void dtGridView()
        //{
        //    LogUtility.DebugMethodStart();
        //    //行の追加オプション(true)
        //    this.form.customDataGridView1.AllowUserToAddRows = true;
        //    //this.form.customDataGridView1.Size = new System.Drawing.Size(997, 225);
        //    //this.form.customDataGridView1.Location = new System.Drawing.Point(3, 198);
        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        public void ButtonInit()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        public void EventInit()
        {
            LogUtility.DebugMethodStart();

            //customTextBoxでのエンターキー押下イベント生成
            this.form.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);     //汎用検索(SearchString)
            footer.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);         //処理No(ESC)

            //Functionボタンのイベント生成
            footer.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);          //汎用検索
            //footer.bt_func1.Text = "[F1]　　汎用検索";
            footer.bt_func2.Click += new EventHandler(this.bt_func2_Click);                 //新規
            footer.bt_func3.Click += new EventHandler(this.bt_func3_Click);                 //修正
            footer.bt_func4.Click += new EventHandler(this.bt_func4_Click);                 //削除
            footer.bt_func6.Click += new EventHandler(this.bt_func6_Click);                 //CSV出力
            footer.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);          //検索条件クリア
            footer.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);          //検索
            footer.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);        //並替移動
            footer.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);        //F11 フィルタ
            footer.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);        //閉じる
            footer.bt_process1.Click += new EventHandler(bt_process1_Click);                //パターン一覧画面へ遷移
            //footer.bt_process2.Click += new EventHandler(bt_process2_Click);                //検索条件設定画面へ遷移

            footer.FormClosing += new FormClosingEventHandler(SetPrevStatus);   // No.2002

            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

            //ダブルクリック時の動作は「F3 修正」と同様の処理を行う
            this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);

            //【伝票種類】が[1][2]以外の場合、フォーカス移動をチェック
            //this.form.txtNum_DenpyouSyurui.LostFocus += new EventHandler(this.txtNum_DenpyouSyurui_LostFocus);

            // 伝票種類が変更された時に発生するイベント
            this.form.txtNum_DenpyouSyurui.TextChanged += new EventHandler(this.txtNum_DenpyouSyurui_TextChanged);

            /// 20141201 Houkakou 「入出金一覧」のダブルクリックを追加する　start
            // 「To」のイベント生成
            this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            /// 20141201 Houkakou 「入出金一覧」のダブルクリックを追加する　end

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 明細行ダブルクリック処理

        /// <summary>
        /// 明細行ダブルクリック処理
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void customDataGridView1_MouseDoubleClick(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            DataGridViewCellMouseEventArgs cell = (DataGridViewCellMouseEventArgs)e;
            String No = "";
            if (cell.RowIndex >= 0)
            {
                //2013.11.28 naitou del
                //if (this.form.customDataGridView1.CurrentRow.Index != this.form.customDataGridView1.Rows.Count - 1)
                //{
                //2013.11.28 naitou del
                String Kbn = this.form.txtNum_DenpyouSyurui.Text;
                bool inputKbn = true;
                if ("1".Equals(this.form.txtNum_DenpyouSyurui.Text))
                {
                    No = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_NYUUKIN_NUMBER].Value.ToString();
                    var value = Convert.ToString(this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_TOK_INPUT_KBN].Value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        inputKbn = Convert.ToBoolean(value);
                    }
                }
                else
                {
                    No = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_SHUKKIN_NUMBER].Value.ToString();
                }

                //入金入力画面へ遷移
                Popup_Nyushukin(Kbn, inputKbn, No, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                //} //2013.11.28 naitou del
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面遷移


        /// <summary>
        /// 画面遷移
        /// </summary>
        /// <param name="kbn">伝票区分</param>
        /// <param name="inputKbn">入金区分</param>
        /// <param name="No">伝票番号</param>
        /// <param name="type">画面区分</param>
        public bool Popup_Nyushukin(String kbn, bool inputKbn, String No, WINDOW_TYPE type)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(kbn, inputKbn, No, type);

                String formID = string.Empty;
                string nyuukinKbn = string.Empty;

                if (ConstCls.DENPYO_SHURUI_NYUKIN.Equals(kbn))
                {
                    if (inputKbn)
                    {
                        // 入金入力(入金先)
                        formID = "G459";
                        nyuukinKbn = ConstCls.NYUKIN_KBN_NYUKINSAKI;
                    }
                    else
                    {
                        // 入金入力(取引先)
                        formID = "G619";
                        nyuukinKbn = ConstCls.NYUKIN_KBN_TORIHIKISAKI;
                    }
                }
                else
                {
                    // 出金入力
                    formID = "G090";
                }

                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(type)
                    || WINDOW_TYPE.DELETE_WINDOW_FLAG.Equals(type))
                {
                    this.SearchData(kbn, No, nyuukinKbn);
                    if (this.searchResult.Rows.Count == 0)
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E045", "");
                        LogUtility.DebugMethodEnd(true);
                        return true;
                    }
                }

                if (WINDOW_TYPE.UPDATE_WINDOW_FLAG.Equals(type))
                {
                    // 修正権限が無い場合は、参照権限で表示
                    if (r_framework.Authority.Manager.CheckAuthority(formID, type, false))
                    {
                        FormManager.OpenFormWithAuth(formID, type, type, No);
                    }
                    else if (r_framework.Authority.Manager.CheckAuthority(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth(formID, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, No);
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", WINDOW_TYPEExt.ToTypeString(WINDOW_TYPE.UPDATE_WINDOW_FLAG));
                    }
                }
                else
                {
                    FormManager.OpenFormWithAuth(formID, type, type, No);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Popup_Nyushukin", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 出金番号によって検索を行う
        /// </summary>
        /// <param>出金番号
        private void SearchData(string dataKbn, string number, string nyuukinKbn)
        {
            LogUtility.DebugMethodStart(dataKbn, number, nyuukinKbn);
       
            searchResult = new DataTable();
            this.searchResult = dao.GetDataForEntity(dataKbn, number, nyuukinKbn);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Functionボタン 押下処理

        /// <summary>
        /// F1 汎用検索ボタン
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (Hanyou_Kenyi == "汎用")
            {
                Hanyou_Kenyi = "簡易";
                this.form.TORIHIKISAKI_LABEL.Visible = false;
                this.form.TORIHIKISAKI_CD.Visible = false;
                this.form.TORIHIKISAKI_NAME_RYAKU.Visible = false;
                this.form.TorihikiPopupButton.Visible = false;
                this.form.searchString.Visible = true;
                footer.bt_func1.Text = "[F1]\r\n簡易検索";
            }
            else
            {
                Hanyou_Kenyi = "汎用";
                this.form.TORIHIKISAKI_LABEL.Visible = true;
                this.form.TORIHIKISAKI_CD.Visible = true;
                this.form.TORIHIKISAKI_NAME_RYAKU.Visible = true;
                this.form.TorihikiPopupButton.Visible = true;
                this.form.searchString.Visible = false;
                footer.bt_func1.Text = "[F1]\r\n汎用検索";
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //this.setSetting();

            String Kbn = this.form.txtNum_DenpyouSyurui.Text;
            String No = "";
            bool inputKbn = false;
            //入金入力画面へ遷移
            Popup_Nyushukin(Kbn, inputKbn, No, WINDOW_TYPE.NEW_WINDOW_FLAG);

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //this.setSetting();

            //2013.12.23 naitou upd start
            //if (this.form.customDataGridView1.Rows.Count != 0 && this.form.customDataGridView1.SelectedRows.Count != this.form.customDataGridView1.Rows.Count - 1)
            if (this.form.customDataGridView1.Rows.Count != 0 && this.form.customDataGridView1.CurrentRow != null)
            //2013.12.23 naitou upd end
            {
                String Kbn = this.form.txtNum_DenpyouSyurui.Text;
                String No = "";
                bool inputKbn = true;
                if ("1".Equals(this.form.txtNum_DenpyouSyurui.Text))
                {
                    No = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_NYUUKIN_NUMBER].Value.ToString();
                    var value = Convert.ToString(this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_TOK_INPUT_KBN].Value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        inputKbn = Convert.ToBoolean(value);
                    }
                }
                else
                {
                    No = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_SHUKKIN_NUMBER].Value.ToString();
                }
                //入金入力画面へ遷移
                Popup_Nyushukin(Kbn, inputKbn, No, WINDOW_TYPE.UPDATE_WINDOW_FLAG);
            }
            else
            {
                string[] message = new string[2];
                message[0] = "修正する伝票";
                message[1] = "伝票一覧";
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E029", message);
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //this.setSetting();

            //2013.12.23 naitou upd start
            //if (this.form.customDataGridView1.Rows.Count != 0 && this.form.customDataGridView1.SelectedRows.Count != this.form.customDataGridView1.Rows.Count - 1)
            if (this.form.customDataGridView1.Rows.Count != 0 && this.form.customDataGridView1.CurrentRow != null)
            //2013.12.23 naitou upd end
            {
                String Kbn = this.form.txtNum_DenpyouSyurui.Text;
                String No = "";
                bool inputKbn = true;
                if ("1".Equals(this.form.txtNum_DenpyouSyurui.Text))
                {
                    No = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_NYUUKIN_NUMBER].Value.ToString();
                    var value = Convert.ToString(this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_TOK_INPUT_KBN].Value);
                    if (!string.IsNullOrEmpty(value))
                    {
                        inputKbn = Convert.ToBoolean(value);
                    }
                }
                else
                {
                    No = this.form.customDataGridView1.Rows[this.form.customDataGridView1.CurrentRow.Index].Cells[ConstCls.HIDDEN_SHUKKIN_NUMBER].Value.ToString();
                }
                //入金入力画面へ遷移
                Popup_Nyushukin(Kbn, inputKbn, No, WINDOW_TYPE.DELETE_WINDOW_FLAG);
            }
            else
            {
                string[] message = new string[2];
                message[0] = "削除する伝票";
                message[1] = "伝票一覧";
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E029", message);
            }

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            // 一覧にデータ行がない場合
            if (this.form.customDataGridView1.RowCount == 0)
            {
                // アラートを表示し、CSV出力処理はしない
                msgLogic.MessageBoxShow("E044");
            }
            else
            {
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport exp = new CSVExport();
                    exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(DENSHU_KBN.NYUUSHUKKIN_ICHIRAN), this.form);
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            ////[汎用検索][簡易検索]をクリア
            //this.form.searchString.Clear();
            ////一覧明細をクリア
            //this.form.customDataGridView1.DataSource = null;
            //並び順をクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            //フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            this.ClearScreen("ClsSearchCondition");

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // パターンチェック
            if (this.form.PatternNo == 0)
            {
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            this.Search();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F10 並替移動
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.customDataGridView1.Rows.Count < 1)
            {
                return;
            }
            this.form.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            this.form.customSearchHeader1.ShowCustomSearchSettingDialog();

            if (this.form.customDataGridView1 != null)
            {
                this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.headForm.ReadDataNumber.Text = "0";
            }
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //以下の項目をセッティングファイルに保存する
            //Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;                                               //拠点CD
            //Properties.Settings.Default.SET_HIDUKESENTAKU = this.headForm.txtNum_HidukeSentaku.Text;                                //日付選択
            //if (this.headForm.HIDUKE_FROM.Value == null)
            //{
            //    Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
            //}
            //else
            //{
            //    Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Value.ToString().Substring(0, 10);          //伝票日付From
            //}
            //if (this.headForm.HIDUKE_TO.Value == null)
            //{
            //    Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
            //}
            //else
            //{
            //    Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Value.ToString().Substring(0, 10);              //伝票日付To
            //}
            //Properties.Settings.Default.SET_DENPYOUSYURUI = this.form.txtNum_DenpyouSyurui.Text;
            //Properties.Settings.Default.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            //Properties.Settings.Default.Save();
            this.setSetting();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd(sender, e);
        }

        #endregion

        #region Settingsの値の保存

        /// <summary>
        /// Settingsの値の保存
        /// </summary>
        public bool setSetting()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.headForm.KYOTEN_CD.Text != "")
                {
                    Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;               //拠点CD
                }
                else
                {
                    Properties.Settings.Default.SET_KYOTEN_CD = null;                                       //拠点CD
                }

                Properties.Settings.Default.SET_HIDUKESENTAKU = this.headForm.txtNum_HidukeSentaku.Text;   //日付選択

                //Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Text;              //伝票日付From
                DateTime resultDt;
                //if (this.headForm.HIDUKE_FROM.Value == null)
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text) && DateTime.TryParse(this.headForm.HIDUKE_FROM.Text, out resultDt))
                {
                    Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Value.ToString().Substring(0, 10);
                }
                else
                {
                    Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
                    // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                    this.headForm.HIDUKE_FROM.Text = string.Empty;
                }

                //Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Text;                  //伝票日付To
                //if (this.headForm.HIDUKE_TO.Value == null)
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text) && DateTime.TryParse(this.headForm.HIDUKE_TO.Text, out resultDt))
                {
                    Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Value.ToString().Substring(0, 10);
                }
                else
                {
                    Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
                    // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
                    this.headForm.HIDUKE_TO.Text = string.Empty;
                }

                Properties.Settings.Default.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;              //取引先

                Properties.Settings.Default.SET_DENPYOUSYURUI = this.form.txtNum_DenpyouSyurui.Text; 　　　//伝票種類

                Properties.Settings.Default.NYUUKINSAKI_CD = this.form.NYUUKINSAKI_CD.Text;                // 入金先

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LogUtility.Error("setSetting", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region プロセスボタン押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.SearchResult = this.form.Table;
                this.form.ShowData();
            }

            LogUtility.DebugMethodEnd();
        }
        /// <summary>
        /// 検索条件設定画面へ遷移
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region 汎用検索(SearchString)内でのエンターキー押下イベント

        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void SearchStringKeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                    this.SearchString = getSearchString;
                    this.Search();

                }
                else
                {
                    this.form.searchString.Text = "";
                    this.form.searchString.SelectionLength = this.form.searchString.Text.Length;
                    this.form.searchString.Focus();
                }
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 処理No(ESC)でのエンターキー押下イベント(※遷移先画面が存在しない為、未実装)

        /// <summary>
        /// エンターキー押下イベント
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if ("1".Equals(parentForm.txb_process.Text))
            {
                //パターン一覧画面へ遷移
            }
            else if ("2".Equals(parentForm.txb_process.Text))
            {
                //検索条件設定画面へ遷移
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ESCキー押下イベント

        void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                parentForm.txb_process.Focus();
            }
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region HeaderForm

        /// <summary>
        /// HeaderForm.cs設定
        /// </summary>
        /// /// <returns>hs</returns>
        public void SetHeader(HeaderForm hs)
        {
            LogUtility.DebugMethodStart(hs);
            this.headForm = hs;
            LogUtility.DebugMethodEnd(hs);
        }

        #endregion

        #region 検索入出金一覧

        /// <summary>
        /// 入出金一覧
        /// </summary>
        public void MakeSearchCondition1()
        {
            LogUtility.DebugMethodStart();

            if (string.IsNullOrEmpty(this.form.GetSelectQuery()))
            {
                return;
            }

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            sql.Append(" SELECT DISTINCT ");

            sql.Append(this.form.GetSelectQuery());

            if (ConstCls.DENPYO_SHURUI_NYUKIN == this.form.txtNum_DenpyouSyurui.Text)
            {
                sql.AppendFormat(" ,T_NYUUKIN_SUM_ENTRY.SYSTEM_ID AS {0} ", ConstCls.HIDDEN_SYSTEM_ID);
                sql.AppendFormat(" ,T_NYUUKIN_SUM_ENTRY.NYUUKIN_NUMBER AS {0} ", ConstCls.HIDDEN_NYUUKIN_NUMBER);
                sql.AppendFormat(" ,T_NYUUKIN_ENTRY.TOK_INPUT_KBN  AS {0} ", ConstCls.HIDDEN_TOK_INPUT_KBN);

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    sql.AppendFormat(" ,T_NYUUKIN_SUM_DETAIL.DETAIL_SYSTEM_ID AS {0} ", ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                }
            }
            else if (ConstCls.DENPYO_SHURUI_SHUKKIN == this.form.txtNum_DenpyouSyurui.Text)
            {
                sql.AppendFormat(" ,T_SHUKKIN_ENTRY.SYSTEM_ID AS {0} ", ConstCls.HIDDEN_SYSTEM_ID);
                sql.AppendFormat(" ,T_SHUKKIN_ENTRY.SHUKKIN_NUMBER AS {0} ", ConstCls.HIDDEN_SHUKKIN_NUMBER);

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    sql.AppendFormat(" ,T_SHUKKIN_DETAIL.DETAIL_SYSTEM_ID AS {0} ", ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                }
            }

            // 伝票種類が 1.入金 の場合
            if (ConstCls.DENPYO_SHURUI_NYUKIN == this.form.txtNum_DenpyouSyurui.Text)
            {
                sql.Append(" FROM T_NYUUKIN_SUM_ENTRY ");
                sql.Append(" LEFT JOIN T_NYUUKIN_ENTRY ");
                sql.Append(" ON ");
                sql.Append("     T_NYUUKIN_SUM_ENTRY.NYUUKIN_NUMBER = T_NYUUKIN_ENTRY.NYUUKIN_NUMBER ");
                sql.Append(" AND T_NYUUKIN_ENTRY.DELETE_FLG = 0 ");

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    sql.Append(" LEFT JOIN T_NYUUKIN_SUM_DETAIL ");
                    sql.Append(" ON T_NYUUKIN_SUM_ENTRY.SYSTEM_ID = T_NYUUKIN_SUM_DETAIL.SYSTEM_ID ");
                    sql.Append(" AND T_NYUUKIN_SUM_ENTRY.SEQ = T_NYUUKIN_SUM_DETAIL.SEQ ");
                    sql.Append(" LEFT JOIN T_NYUUKIN_DETAIL ");
                    sql.Append(" ON T_NYUUKIN_ENTRY.SYSTEM_ID = T_NYUUKIN_DETAIL.SYSTEM_ID ");
                    sql.Append(" AND T_NYUUKIN_ENTRY.SEQ = T_NYUUKIN_DETAIL.SEQ ");
                }

                sql.Append(this.form.GetJoinQuery());

                sql.Append(" WHERE ");
                sql.Append(" T_NYUUKIN_SUM_ENTRY.DELETE_FLG = 0 ");

                //画面で日付選択が伝票日付の場合
                if ("1".Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_NYUUKIN_SUM_ENTRY.DENPYOU_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_NYUUKIN_SUM_ENTRY.DENPYOU_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }
                //画面で日付選択が入力日付の場合
                if ("2".Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_NYUUKIN_SUM_ENTRY.UPDATE_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_NYUUKIN_SUM_ENTRY.UPDATE_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }

                //画面で拠点CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && !"99".Equals(this.headForm.KYOTEN_CD.Text))
                {
                    sql.Append(" AND T_NYUUKIN_SUM_ENTRY.KYOTEN_CD = " + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
                }
                //取引先CDがnullでない場合
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    //汎用検索の場合
                    //if (Hanyou_Kenyi == "汎用" && !string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                    //{
                    sql.Append(" AND T_NYUUKIN_ENTRY.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
                    //}
                }
                // 入金番号
                if (!string.IsNullOrEmpty(this.form.NYUUKIN_NUMBER.Text))
                {
                    sql.Append(" AND T_NYUUKIN_SUM_ENTRY.NYUUKIN_NUMBER = '" + this.form.NYUUKIN_NUMBER.Text + "' ");
                }

                var nyuukinsakiCd = this.form.NYUUKINSAKI_CD.Text;
                if (!string.IsNullOrEmpty(nyuukinsakiCd))
                {
                    sql.AppendFormat(" AND T_NYUUKIN_SUM_ENTRY.NYUUKINSAKI_CD = '{0}' ", nyuukinsakiCd);
                }
            }
            else if (ConstCls.DENPYO_SHURUI_SHUKKIN == this.form.txtNum_DenpyouSyurui.Text)
            {
                sql.Append(" FROM T_SHUKKIN_ENTRY ");

                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    sql.Append(" LEFT JOIN T_SHUKKIN_DETAIL ");
                    sql.Append(" ON T_SHUKKIN_ENTRY.SYSTEM_ID = T_SHUKKIN_DETAIL.SYSTEM_ID ");
                    sql.Append(" AND T_SHUKKIN_ENTRY.SEQ = T_SHUKKIN_DETAIL.SEQ ");
                }

                sql.Append(this.form.GetJoinQuery());

                sql.Append(" WHERE ");
                sql.Append(" T_SHUKKIN_ENTRY.DELETE_FLG = 0 ");
                //sql.Append(" AND ");
                //画面で日付選択が伝票日付の場合
                if ("1".Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    //日付FROMの判定かどうかnull
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_SHUKKIN_ENTRY.DENPYOU_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    //日付TOの判定かどうかnull
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_SHUKKIN_ENTRY.DENPYOU_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }
                //画面で日付選択が入力日付の場合
                if ("2".Equals(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    //日付FROMの判定かどうかnull
                    if (this.headForm.HIDUKE_FROM.Value != null)
                    {
                        sql.Append(" AND T_SHUKKIN_ENTRY.UPDATE_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
                    }
                    //日付TOの判定かどうかnull
                    if (this.headForm.HIDUKE_TO.Value != null)
                    {
                        sql.Append(" AND T_SHUKKIN_ENTRY.UPDATE_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
                    }
                }

                //画面で拠点CDがnull無いの場合
                if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && !"99".Equals(this.headForm.KYOTEN_CD.Text))
                {
                    sql.Append(" AND T_SHUKKIN_ENTRY.KYOTEN_CD = " + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
                }
                //汎用検索の場合
                if (Hanyou_Kenyi == "汎用" && !string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    sql.Append(" AND T_SHUKKIN_ENTRY.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
                }
                // 取引先CDが入力されている場合
                if (false == String.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    sql.Append("AND T_SHUKKIN_ENTRY.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
                }
                // 出金番号
                if (!string.IsNullOrEmpty(this.form.NYUUKIN_NUMBER.Text))
                {
                    sql.Append(" AND T_SHUKKIN_ENTRY.SHUKKIN_NUMBER = '" + this.form.NYUUKIN_NUMBER.Text + "' ");
                }
            }

            if (!string.IsNullOrEmpty(this.form.GetOrderByQuery()))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.form.GetOrderByQuery());

                var template = " ,\"{0}\" ASC ";
                if (ConstCls.DENPYO_SHURUI_NYUKIN == this.form.txtNum_DenpyouSyurui.Text)
                {
                    sql.AppendFormat(template, ConstCls.HIDDEN_SYSTEM_ID);
                    sql.AppendFormat(template, ConstCls.HIDDEN_NYUUKIN_NUMBER);

                    if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                    {
                        sql.AppendFormat(template, ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                    }
                }
                else if (ConstCls.DENPYO_SHURUI_SHUKKIN == this.form.txtNum_DenpyouSyurui.Text)
                {
                    sql.AppendFormat(template, ConstCls.HIDDEN_SYSTEM_ID);
                    sql.AppendFormat(template, ConstCls.HIDDEN_SHUKKIN_NUMBER);

                    if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                    {
                        sql.AppendFormat(template, ConstCls.HIDDEN_DETAIL_SYSTEM_ID);
                    }
                }
            }

            System.Diagnostics.Debug.WriteLine(sql.ToString());
            this.createSql = sql.ToString();
            sql.Append("");
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ボタン情報の設定

        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

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

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string errorStr = ""; //2013.12.23 naitou upd

                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

                //SQLの作成 入出金一覧
                this.MakeSearchCondition1();

                //検索実行
                this.SearchResult = new DataTable();

                //2013.12.23 naitou upd start
                // 日付選択チェック
                if (string.IsNullOrEmpty(this.headForm.txtNum_HidukeSentaku.Text))
                {
                    //this.headForm.txtNum_HidukeSentaku.Focus();
                    //msgLogic.MessageBoxShow("E042", "1～2");
                    //return 0;
                    this.headForm.txtNum_HidukeSentaku.IsInputErrorOccured = true;
                    errorStr += String.Format(MessageUtil.GetMessage("E001").MESSAGE, "日付選択") + Environment.NewLine;

                }
                // 伝票種類チェック
                if (string.IsNullOrEmpty(this.form.txtNum_DenpyouSyurui.Text))
                {
                    //this.form.txtNum_DenpyouSyurui.Focus();
                    //msgLogic.MessageBoxShow("E042", "1～2");
                    //return 0;
                    this.form.txtNum_DenpyouSyurui.IsInputErrorOccured = true;
                    errorStr += String.Format(MessageUtil.GetMessage("E001").MESSAGE, "伝票種類") + Environment.NewLine;
                }

                //必須チェックエラーメッセージ表示
                if (!String.IsNullOrEmpty(errorStr))
                {
                    msgLogic.MessageBoxShowError(errorStr);
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }
                //2013.12.23 naitou upd end

                if (!string.IsNullOrEmpty(this.syainCode))
                {
                    this.SearchResult = daoIchiran.getdateforstringsql(this.createSql);
                    int count = SearchResult.Rows.Count;

                    //2013.12.15 naitou upd start
                    
                    this.form.ShowData();

                    //読込データ件数を取得
                    if (this.form.customDataGridView1 != null)
                    {
                        this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                    }
                    else
                    {
                        this.headForm.ReadDataNumber.Text = "0";
                    }

                    if (count == 0)
                    {
                        msgLogic.MessageBoxShow("C001");
                    }
                    LogUtility.DebugMethodEnd(SearchResult.Rows.Count);
                    return SearchResult.Rows.Count;
                }

                this.form.SetSearch();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }

            LogUtility.DebugMethodEnd(0);

            return 0;
        }

        #endregion

        #region alert件数(二次開発使用)

        private void alertKensuu()
        {
            //alert件数を超えた場合、メッセージの表示(二次開発使用)
            //alert件数を超えた場合、メッセージの表示
            //if (this.alertCount < count)
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("C025");
            //}

        }

        #endregion

        #region 検索条件コントロール関連処理
        /// <summary>
        /// 伝票種類が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtNum_DenpyouSyurui_TextChanged(object sender, EventArgs e)
        {
            ChangeNyuukinsakiEnabled();
            SetDenshuKbn();

            // 選択された伝票種類用のパターンに変更する
            if (!this.form.PatternUpdate())
            {
                return;
            }
            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　start
            if (this.form.customDataGridView1 != null && this.form.customDataGridView1.Rows.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)this.form.customDataGridView1.DataSource;
                dt.Clear();
                this.form.customDataGridView1.DataSource = dt;

            }
            // 20140624 syunrei EV004430_受付一覧で検索後、伝票種類を変更してからサブファンクションで伝票紐付をするとシステムエラー　end
        }

        /// <summary>
        /// 伝票種類に応じて入金先条件のコントロールの状態を変更します。
        /// </summary>
        private void ChangeNyuukinsakiEnabled()
        {
            if (this.form.txtNum_DenpyouSyurui.Text == ConstCls.DENPYO_SHURUI_NYUKIN)
            {
                this.form.NYUUKINSAKI_CD.Enabled = true;
                this.form.NYUUKINSAKI_NAME_RYAKU.Enabled = true;
                this.form.NyuukinPopupButton.Enabled = true;
            }
            else
            {
                this.form.NYUUKINSAKI_CD.Text = String.Empty;
                this.form.NYUUKINSAKI_NAME_RYAKU.Text = String.Empty;

                this.form.NYUUKINSAKI_CD.Enabled = false;
                this.form.NYUUKINSAKI_NAME_RYAKU.Enabled = false;
                this.form.NyuukinPopupButton.Enabled = false;
            }
        }

        /// <summary>
        /// パターン一覧用の伝種区分を設定します。
        /// </summary>
        private void SetDenshuKbn()
        {
            //伝種区分を取得すること
            if (ConstCls.DENPYO_SHURUI_NYUKIN == this.form.txtNum_DenpyouSyurui.Text)
            {
                this.DenshuKbn = DENSHU_KBN.NYUUKIN_ICHIRAN;
                this.form.lblBangou.Text = "入金番号";
            }
            else
            {
                this.DenshuKbn = DENSHU_KBN.SHUKKIN_ICHIRAN;
                this.form.lblBangou.Text = "出金番号";
            }
        }
        #endregion

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

        /// <summary>
        /// 伝票種類を初期化します。
        /// </summary>
        /// <remarks>
        /// 画面起動時の引数がある場合は、引数を設定
        /// 画面起動時の引数がなく、設定値がある場合は、設定値を設定
        /// どちらも無い場合は、「1.入金」を設定
        /// </remarks>
        internal void SetInitialDenpyoShurui()
        {
            if (false == String.IsNullOrEmpty(this.form.DenpyoShurui))
            {
                this.form.txtNum_DenpyouSyurui.Text = this.form.DenpyoShurui;
            }
            else if (false == "".Equals(Properties.Settings.Default.SET_DENPYOUSYURUI))
            {
                this.form.txtNum_DenpyouSyurui.Text = Properties.Settings.Default.SET_DENPYOUSYURUI;
            }
            else
            {
                this.form.txtNum_DenpyouSyurui.Text = ConstCls.DENPYO_SHURUI_NYUKIN;
            }
        }

        // No.2002
        /// <summary>
        /// Windowクローズ処理。
        /// </summary>
        internal void SetPrevStatus(object sender, EventArgs e)
        {
            this.setSetting();
        }

        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.headForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.headForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.headForm.HIDUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.headForm.HIDUKE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
                this.headForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.headForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                if (this.headForm.radbtnDenpyouHiduke.Checked)
                {
                    string[] errorMsg = { "伝票日付From", "伝票日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                else if (this.headForm.radbtnNyuuryokuHiduke.Checked)
                {
                    string[] errorMsg = { "入力日付From", "入力日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                this.headForm.HIDUKE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion
        // koukouei 20141022 「From　>　To」のアラート表示タイミング変更 end

        /// 20141201 Houkakou 「入出金一覧」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var FromTextBox = this.headForm.HIDUKE_FROM;
            var ToTextBox = this.headForm.HIDUKE_TO;

            ToTextBox.Text = FromTextBox.Text;


            LogUtility.DebugMethodEnd();
        }
        #endregion
        /// 20141201 Houkakou 「入出金一覧」のダブルクリックを追加する　end
    }
}