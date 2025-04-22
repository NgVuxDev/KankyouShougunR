using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Message;
using Shougun.Core.ReceiptPayManagement.NyukinKeshikomi.DAO;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomi
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ReceiptPayManagement.NyukinKeshikomi.Setting.ButtonSetting.xml";

        /// <summary>
        /// システム上の必須項目
        /// </summary>
        internal readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// システム上の必須項目
        /// </summary>
        internal readonly string HIDDEN_NYUUKIN_NUMBER = "HIDDEN_NYUUKIN_NUMBER";

        /// <summary>
        /// システム上の必須項目
        /// </summary>
        internal readonly string HIDDEN_KESHIKOMI_SEQ = "HIDDEN_KESHIKOMI_SEQ";    

        /// <summary>
        /// 汎用、簡易検索フラグ
        /// </summary>
        private string Hanyou_Kenyi;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private UIHeader headForm;
        private BusinessBaseForm footer;

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;

        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;
        
        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

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
        /// SELECT句
        /// </summary>
        public string selectQuery { get; set; }

        /// <summary>
        /// ORDERBY句
        /// </summary>
        public string orderByQuery { get; set; }

        /// <summary>
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        private Control[] allControl;

        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        //private RibbonMainMenu ribbonForm;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();

            LogUtility.DebugMethodEnd();
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

                //2013.12.23 naitou upd start
                // ヘッダー（フッター）を初期化
                this.HeaderInit();

                //　ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                //this.popuApu();
                //this.dtGridView();
                //Hanyou_Kenyi = "汎用";
                //this.form.searchString.Visible = false;
                //this.RegistRibbonMenu("123456", "abcd1234");
                this.allControl = this.form.allControl;
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.Location = new System.Drawing.Point(3, 141);
                this.form.customDataGridView1.Size = new System.Drawing.Size(997, 278);
                //2013.12.23 naitou upd end

                //var parentForm = (BusinessBaseForm)this.form.Parent;
                //parentForm.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_NYUKIN_KESHIKOMI_RIREKI);

                //2013.12.23 naitou upd start
                //アラート件数
                M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
                this.headForm.InitialNumberAlert = int.Parse(mSysInfo.ICHIRAN_ALERT_KENSUU.ToString());
                this.headForm.NumberAlert = this.headForm.InitialNumberAlert;
                //2013.12.23 naitou upd end
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

        //2013.12.23 naitou upd start
        /// <summary>
        /// ヘッダー初期化処理
        /// </summary>
        private void HeaderInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //ヘッダーの初期化
            UIHeader targetHeader = (UIHeader)parentForm.headerForm;
            this.headForm = targetHeader;

            //フッターの初期化
            BusinessBaseForm targetFooter = (BusinessBaseForm)parentForm;
            this.footer = targetFooter;

            LogUtility.DebugMethodEnd();
        }
        //2013.12.23 naitou upd end

        //2013.12.23 naitou upd start
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
                    case "ClsSearchCondition"://検索条件をクリア

                        //タイトル
                        this.headForm.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_NYUKIN_KESHIKOMI_RIREKI);

                        //アラート件数
                        this.headForm.NumberAlert = this.headForm.InitialNumberAlert;

                        //拠点CD
                        // No.777(2014/01/23)
                        ////2013-12-24 touti R_E3 upd start No.777
                        ////this.headForm.KYOTEN_CD.Text = Properties.Settings.Default.SET_KYOTEN_CD;
                        //if (string.IsNullOrWhiteSpace(Properties.Settings.Default.SET_KYOTEN_CD))
                        //{
                        //    XMLAccessor fileAccess = new XMLAccessor();
                        //    CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                        //    //ヘッダに表示予定の値
                        //    if (configProfile.ItemSetVal1.Length == 1)
                        //    {
                        //        this.headForm.KYOTEN_CD.Text = "0" + configProfile.ItemSetVal1;
                        //    }
                        //    else
                        //    {
                        //        this.headForm.KYOTEN_CD.Text = configProfile.ItemSetVal1;
                        //    }
                        //}
                        ////2013-12-24 touti R_E3 upd end No.777

                        //常にシステム設定ファイルから拠点CDを設定する
                        XMLAccessor fileAccess = new XMLAccessor();
                        CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
                        this.headForm.KYOTEN_CD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

                        // 拠点名称
                        if (this.headForm.KYOTEN_CD.Text != "")
                        {
                            M_KYOTEN mKyoten = new M_KYOTEN();
                            mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                            if (mKyoten == null)
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
                        //if (Properties.Settings.Default.SET_HIDUKESENTAKU != "")
                        //{
                        //    this.headForm.txtNum_HidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKESENTAKU;
                        //}
                        //else
                        //{
                        //    this.headForm.txtNum_HidukeSentaku.Text = "1";
                        //}
                        //2013.12.15 naitou upd end

                        //伝票日付（From）
                        //2013.12.15 naitou upd start
                        //if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
                        //{
                        //    this.headForm.HIDUKE_FROM.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_FROM);
                        //}
                        //else
                        //{
                        //    this.headForm.HIDUKE_FROM.Value = DateTime.Now.Date;
                        //}
                        //2013.12.15 naitou upd end

                        //伝票日付（To）
                        //2013.12.15 naitou upd start
                        //if (Properties.Settings.Default.SET_HIDUKE_TO != "")
                        //{
                        //    this.headForm.HIDUKE_TO.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_TO);
                        //}
                        //else
                        //{
                        //    this.headForm.HIDUKE_TO.Value = DateTime.Now.Date;
                        //}
                        //2013.12.15 naitou upd end

                        //検索条件
                        this.form.searchString.Clear();

                        //取引先CD
                        //this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TORIHIKISAKI_CD;

                        // 取引先名称
                        //M_TORIHIKISAKI mTorihikisaki = new M_TORIHIKISAKI();
                        //mTorihikisaki = (M_TORIHIKISAKI)torihikisakiDao.GetDataByCd(this.form.TORIHIKISAKI_CD.Text);
                        //if (mTorihikisaki == null)
                        //{
                        //    this.form.TORIHIKISAKI_NAME_RYAKU.Text = "";
                        //}
                        //else
                        //{
                        //    this.form.TORIHIKISAKI_NAME_RYAKU.Text = mTorihikisaki.TORIHIKISAKI_NAME_RYAKU;
                        //}

                        break;

                }

                //読込データ件数
                this.headForm.ReadDataNumber.Text = "0";

                //アラート件数
                this.headForm.alertNumber.Text = this.headForm.NumberAlert.ToString();

                //一覧明細をクリア
                this.form.customDataGridView1.DataSource = null;
                this.form.ShowHeader();

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
        //2013.12.23 naitou upd end

        /// <summary>
        /// 起動元画面で条件が指定されている場合に、検索条件をセットする
        /// </summary>
        internal bool SetStartUpCondition()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 入金入力画面から遷移した場合
                if (!string.IsNullOrEmpty(this.form.kyotenCdForStartUpPoint))
                {
                    this.headForm.KYOTEN_CD.Text = this.form.kyotenCdForStartUpPoint;
                    M_KYOTEN mKyoten = new M_KYOTEN();
                    mKyoten = (M_KYOTEN)mkyotenDao.GetDataByCd(this.headForm.KYOTEN_CD.Text);
                    if (mKyoten == null)
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        this.headForm.KYOTEN_NAME_RYAKU.Text = mKyoten.KYOTEN_NAME_RYAKU;
                    }
                }

                // 今のところ、起動元画面から日付が範囲で指定されることは無いので、From、Toには同じ日付を設定する
                //DateTime tempDenpyouHidukeForm = DateTime.Now;
                //if (!string.IsNullOrEmpty(this.form.denpyouHidukeForStartUpPoint)
                //    && DateTime.TryParse(this.form.denpyouHidukeForStartUpPoint, out tempDenpyouHidukeForm))
                //{
                //    this.headForm.txtNum_HidukeSentaku.Text = "1";
                //    this.headForm.HIDUKE_FROM.Text = tempDenpyouHidukeForm.Date.ToString();
                //    this.headForm.HIDUKE_TO.Text = tempDenpyouHidukeForm.Date.ToString();
                //}
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SetStartUpCondition", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetStartUpCondition", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion

        //#region 「拠点」ポップアップ設定

        //2013.12.23 naitou upd start
        ///// <summary>
        ///// 「拠点」ポップアップ設定
        ///// </summary>
        //private void popuApu()
        //{
        //    LogUtility.DebugMethodStart();
        //    // headform【拠点】
        //    this.headForm.KYOTEN_CD.Text = Properties.Settings.Default.SET_KYOTEN_CD;
        //    if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
        //    {
        //        this.headForm.HIDUKE_FROM.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_FROM);
        //    }
        //    if (Properties.Settings.Default.SET_HIDUKE_FROM != "")
        //    {
        //        this.headForm.HIDUKE_TO.Value = DateTime.Parse(Properties.Settings.Default.SET_HIDUKE_TO);
        //    }
        //    this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TORIHIKISAKI_CD;
        //    // ユーザ拠点名称の取得
        //    if(this.headForm.KYOTEN_CD.Text != "")
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

        //    LogUtility.DebugMethodEnd();
        //}

        //#endregion

        //#region DataGridViewのスタイル設定

        ///// <summary>
        ///// 画面でDataGridViewのスタイル設定
        ///// </summary>
        //private void dtGridView()
        //{
        //    LogUtility.DebugMethodStart();
        //    //行の追加オプション(false)
        //    this.form.customDataGridView1.AllowUserToAddRows = false;
        //    ////ヘッダの背景色
        //    //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
        //    ////ヘッダの文字色
        //    //this.form.customDataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        //    LogUtility.DebugMethodEnd();
        //}
        //2013.12.23 naitou upd end

        //#endregion

        #region イベント処理の初期化

        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        public void EventInit()
        {
            LogUtility.DebugMethodStart();

            //customTextBoxでのエンターキー押下イベント生成
            //this.form.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);       //汎用検索(SearchString)
            //footer.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);         //処理No(ESC)

            //Functionボタンのイベント生成
            footer.bt_func1.Click += new System.EventHandler(this.bt_func1_Click);          //汎用検索
            //footer.bt_func1.Text = "[F1]　　汎用検索";
            footer.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);          //CSV出力
            footer.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);          //検索条件クリア
            footer.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);          //検索
            footer.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);        //閉じる
            footer.bt_func10.Click += new EventHandler(this.bt_func10_Click);               //並替移動ボタン(F10)イベント生成
            footer.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
            footer.bt_process1.Click += new EventHandler(bt_process1_Click);                //パターン一覧画面へ遷移
            //footer.bt_process2.Click += new EventHandler(bt_process2_Click);                //検索条件設定画面へ遷移

            //画面上でESCキー押下時のイベント生成 TODO　二次開発 
            //this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動

            /// 20141201 Houkakou 「入金消込履歴」のダブルクリックを追加する　start
            // 「To」のイベント生成
            //this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);
            /// 20141201 Houkakou 「入金消込履歴」のダブルクリックを追加する　end

            /// 20141203 Houkakou 「入金消込履歴」の日付チェックを追加する　start
            //this.headForm.HIDUKE_FROM.Leave += new System.EventHandler(HIDUKE_FROM_Leave);
            //this.headForm.HIDUKE_TO.Leave += new System.EventHandler(HIDUKE_TO_Leave);
            /// 20141203 Houkakou 「入金消込履歴」の日付チェックを追加する　end

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

            //2013.12.23 naitou upd start
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
            //2013.12.23 naitou upd end

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
                    exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(DENSHU_KBN.NYUUKIN_KESHIKOMI_RIEKI_ICHIRAN), this.form);
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

            //2013.12.23 naitou upd start
            ////[汎用検索][簡易検索]をクリア
            //this.form.searchString.Clear();
            ////一覧明細をクリア
            //this.form.customDataGridView1.DataSource = null;
            //並び順をクリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            //フィルタをクリア
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            this.ClearScreen("ClsSearchCondition");
            //2013.12.23 naitou upd end

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
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            /// 20141203 Houkakou 「入金消込履歴」の日付チェックを追加する　start
            //if (this.headForm.txtNum_HidukeSentaku.Text != "3" && this.DateCheck())
            //{
            //    return;
            //}
            /// 20141203 Houkakou 「入金消込履歴」の日付チェックを追加する　end

            if (string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text) &&
                string.IsNullOrEmpty(this.form.Nyuukin_CD.Text) &&
                string.IsNullOrEmpty(this.form.SEIKYUU_NUMBER.Text))
            {
                MessageBoxUtility.MessageBoxShow("E001", "検索時には、取引先、入金番号、請求番号のいずれか");
                return;
            }

            this.Search();

            LogUtility.DebugMethodEnd(sender, e);
        }

        /// <summary>
        /// F10 並替移動ボタン
        /// </summary>      
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.form.customDataGridView1.Rows.Count < 1)
            {
                return;
            }
            this.form.customSortHeader1.ShowCustomSortSettingDialog();

            LogUtility.DebugMethodEnd();
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
            //2013.12.23 naitou upd start
            //Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;                                                 //拠点CD
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
            //Properties.Settings.Default.SET_HIDUKESENTAKU = this.headForm.txtNum_HidukeSentaku.Text;
            //Properties.Settings.Default.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
            //Properties.Settings.Default.Save();
            this.setSetting();
            //2013.12.23 naitou upd end

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Settingsの値の保存

        //2013.12.23 naitou upd start
        /// <summary>
        /// Settingsの値の保存
        /// </summary>
        public void setSetting()
        {
            LogUtility.DebugMethodStart();

            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;                  //拠点CD

            //Properties.Settings.Default.SET_HIDUKESENTAKU = this.headForm.txtNum_HidukeSentaku.Text;   //日付選択

            //Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Text;              //伝票日付From
            //DateTime resultDt;
            //if (this.headForm.HIDUKE_FROM.Value == null)
            //if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text) && DateTime.TryParse(this.headForm.HIDUKE_FROM.Text, out resultDt))
            //{
            //    Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Value.ToString().Substring(0, 10);
            //}
            //else
            //{
            //    Properties.Settings.Default.SET_HIDUKE_FROM = string.Empty;
            //    // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
            //    this.headForm.HIDUKE_FROM.Text = string.Empty;
            //}

            //Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Text;                  //伝票日付To
            //if (this.headForm.HIDUKE_TO.Value == null)
            //if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text) && DateTime.TryParse(this.headForm.HIDUKE_TO.Text, out resultDt))
            //{
            //    Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Value.ToString().Substring(0, 10);
            //}
            //else
            //{
            //    Properties.Settings.Default.SET_HIDUKE_TO = string.Empty;
            //    // CustomDateTimePicker Valueのget return DateTime.ParseExactでエラーになる為、Emptyにしておく
            //    this.headForm.HIDUKE_TO.Text = string.Empty;
            //}

            Properties.Settings.Default.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;              //取引先

            Properties.Settings.Default.Save();

            LogUtility.DebugMethodEnd();
        }
        //2013.12.23 naitou upd end

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
        /// 検索条件設定画面へ遷移(二次開発)
        /// </summary>
        private void bt_process2_Click(object sender, System.EventArgs e)
        {

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
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            LogUtility.DebugMethodEnd();

        }

        #endregion

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

        #region HeaderForm

        /// <summary>
        /// HeaderForm.cs設定
        /// </summary>
        /// /// <returns>hs</returns>
        public void SetHeader(UIHeader hs)
        {
            LogUtility.DebugMethodStart(hs);
            this.headForm = hs;
            LogUtility.DebugMethodEnd(hs);
        }

        #endregion

        #region 入金消込一覧

        /// <summary>
        /// 入金消込一覧
        /// </summary>
        public void MakeSearchCondition()
        {
            LogUtility.DebugMethodStart();
            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            //2013.12.23 naitou upd start
            //sql.Append(" SELECT ");
            sql.Append(" SELECT DISTINCT ");
            //2013.12.23 naitou upd end

            //branches Rev.6314
            //sql.Append(this.selectQuery);
            sql.Append(this.selectQuery);
            sql.AppendFormat(" , T_NYUUKIN_KESHIKOMI.NYUUKIN_NUMBER AS {0} ", this.HIDDEN_NYUUKIN_NUMBER);
            sql.AppendFormat(" , T_NYUUKIN_KESHIKOMI.SYSTEM_ID AS {0} ", this.HIDDEN_SYSTEM_ID);

            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
            {
                sql.AppendFormat(" , T_NYUUKIN_KESHIKOMI.KESHIKOMI_SEQ AS {0} ", this.HIDDEN_KESHIKOMI_SEQ);
            }


            #region FROM句

            //FROM句作成
            sql.Append(" FROM ");
            // 入金消込テーブル
            sql.Append(" T_NYUUKIN_KESHIKOMI ");

            // 入金テーブル
            sql.Append(" LEFT JOIN T_NYUUKIN_ENTRY ");
            sql.Append(" ON T_NYUUKIN_ENTRY.SYSTEM_ID = T_NYUUKIN_KESHIKOMI.SYSTEM_ID ");
            // 取引先CD
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                sql.Append(" AND T_NYUUKIN_ENTRY.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
            }

            // 請求伝票テーブル
            sql.Append(" LEFT JOIN T_SEIKYUU_DENPYOU ");
            sql.Append(" ON T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER ");
            sql.Append(" LEFT JOIN T_SEIKYUU_DENPYOU_KAGAMI ");
            sql.Append(" ON T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = T_SEIKYUU_DENPYOU_KAGAMI.SEIKYUU_NUMBER ");
            sql.Append(" AND T_NYUUKIN_KESHIKOMI.KAGAMI_NUMBER = T_SEIKYUU_DENPYOU_KAGAMI.KAGAMI_NUMBER ");

            // 取引先請求マスタ
            sql.Append(" LEFT JOIN M_TORIHIKISAKI_SEIKYUU ");
            sql.Append(" ON T_NYUUKIN_KESHIKOMI.TORIHIKISAKI_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD ");

            // 同一の請求についての入金消込の合計金額を取得
            sql.Append(" LEFT JOIN ");
            sql.Append(" (SELECT SEIKYUU_NUMBER,SUM(KESHIKOMI_GAKU) AS KESHIKOMI_GAKU_TOTAL FROM T_NYUUKIN_KESHIKOMI WHERE DELETE_FLG = 0 GROUP BY SEIKYUU_NUMBER) T_NYUUKIN_KESHIKOMI2 ");
            sql.Append(" ON T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = T_NYUUKIN_KESHIKOMI2.SEIKYUU_NUMBER ");

            // 取引先開始残高についての入金消込の合計金額を取得
            sql.Append(" LEFT JOIN ");
            sql.Append(" (SELECT TORIHIKISAKI_CD,SEIKYUU_NUMBER,SUM(KESHIKOMI_GAKU) AS KESHIKOMI_GAKU_TOTAL FROM T_NYUUKIN_KESHIKOMI WHERE DELETE_FLG = 0 GROUP BY TORIHIKISAKI_CD,SEIKYUU_NUMBER) T_NYUUKIN_KESHIKOMI3 ");
            sql.Append(" ON T_NYUUKIN_KESHIKOMI.TORIHIKISAKI_CD = T_NYUUKIN_KESHIKOMI3.TORIHIKISAKI_CD AND T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER = T_NYUUKIN_KESHIKOMI3.SEIKYUU_NUMBER ");

            sql.Append(this.joinQuery);

            #endregion

            #region WHERE句

            sql.Append(" WHERE ");

            //画面で日付選択が伝票日付の場合
            //if ("1".Equals(this.headForm.txtNum_HidukeSentaku.Text))
            //{
            //    if (this.headForm.HIDUKE_FROM.Value != null)
            //    {
            //        sql.Append(" AND T_NYUUKIN_ENTRY.DENPYOU_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
            //    }
            //    if (this.headForm.HIDUKE_TO.Value != null)
            //    {
            //        sql.Append(" AND T_NYUUKIN_ENTRY.DENPYOU_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
            //    }
            //}
            //画面で日付選択が入力日付の場合
            //if ("2".Equals(this.headForm.txtNum_HidukeSentaku.Text))
            //{
            //    if (this.headForm.HIDUKE_FROM.Value != null)
            //    {
            //        sql.Append(" AND T_NYUUKIN_ENTRY.UPDATE_DATE >= '" + DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + " 00:00:00" + "' ");
            //    }
            //    if (this.headForm.HIDUKE_TO.Value != null)
            //    {
            //        sql.Append(" AND T_NYUUKIN_ENTRY.UPDATE_DATE <= '" + DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + " 23:59:59" + "' ");
            //    }
            //}
            
            sql.Append(" T_NYUUKIN_KESHIKOMI.KESHIKOMI_GAKU IS NOT NULL ");
            if ("".Equals(this.form.Nyuukin_CD.Text))
            {
                sql.Append(" AND T_NYUUKIN_KESHIKOMI.DELETE_FLG = 0 ");
            }
            else
            {
                sql.Append(" AND T_NYUUKIN_KESHIKOMI.NYUUKIN_NUMBER = '" + long.Parse(this.form.Nyuukin_CD.Text) + "' ");
                sql.Append(" AND T_NYUUKIN_KESHIKOMI.DELETE_FLG = 0 ");
            }
            // 拠点CD
            if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && !"99".Equals(this.headForm.KYOTEN_CD.Text))
            {
                sql.Append(" AND T_NYUUKIN_ENTRY.KYOTEN_CD = " + int.Parse(this.headForm.KYOTEN_CD.Text) + " ");
            }
            // 取引先CD
            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                sql.Append(" AND T_NYUUKIN_KESHIKOMI.TORIHIKISAKI_CD = '" + this.form.TORIHIKISAKI_CD.Text + "' ");
            }
            // 請求番号
            if (!string.IsNullOrEmpty(this.form.SEIKYUU_NUMBER.Text))
            {
                sql.Append(" AND T_SEIKYUU_DENPYOU.SEIKYUU_NUMBER = '" + this.form.SEIKYUU_NUMBER.Text + "' ");
                sql.Append(" AND T_NYUUKIN_KESHIKOMI.SEIKYUU_NUMBER = '" + this.form.SEIKYUU_NUMBER.Text + "' ");
            }
            sql.Append(" AND (T_SEIKYUU_DENPYOU.DELETE_FLG = 0 OR T_SEIKYUU_DENPYOU.DELETE_FLG IS NULL) ");

            sql.Append(" AND ISNULL(T_NYUUKIN_ENTRY.DELETE_FLG,0) = 0 ");

            #region ORDERBY句

            if (!string.IsNullOrEmpty(orderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
            }

            #endregion

            #endregion

            this.createSql = sql.ToString();
            sql.Append("");
            LogUtility.DebugMethodEnd();
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

                string errorStr = ""; //2013.12.23 naitou upd

                //SQLの作成 入金消込一覧
                this.form.setLogicSelect();
                this.MakeSearchCondition();

                //検索実行
                this.SearchResult = new DataTable();

                //if (string.IsNullOrEmpty(this.headForm.txtNum_HidukeSentaku.Text))
                //{
                //    //msgLogic.MessageBoxShow("E042", "1～2");
                //    //this.headForm.txtNum_HidukeSentaku.Focus();
                //    //return 0;
                //    this.headForm.txtNum_HidukeSentaku.IsInputErrorOccured = true;
                //    errorStr += String.Format(Message.MessageUtility.GetMessage("E001").Text, "日付選択") + Environment.NewLine;
                //}

                //必須チェックエラーメッセージ表示
                if (!String.IsNullOrEmpty(errorStr))
                {
                    MessageBoxUtility.MessageBoxShowError(errorStr);
                    LogUtility.DebugMethodEnd(0);
                    return 0;
                }

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
                        MessageBoxUtility.MessageBoxShow("C001");
                    }
                    LogUtility.DebugMethodEnd(SearchResult.Rows.Count);
                    if (this.form.customDataGridView1.Rows.Count == 0)
                    {
                        this.form.customDataGridView1.TabStop = false;
                    }
                    else
                    {
                        this.form.customDataGridView1.TabStop = true;
                    }
                    return SearchResult.Rows.Count;
                }

                this.form.SetSearch(); //2013.12.23 naitou upd
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

        /// 20141201 Houkakou 「入金消込履歴」のダブルクリックを追加する　start
        #region ダブルクリック時にFrom項目の入力内容をコピーする
        ///// <summary>
        ///// ダブルクリック時にFrom項目の入力内容をコピーする
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void HIDUKE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    LogUtility.DebugMethodStart(sender, e);

        //    var FromTextBox = this.headForm.HIDUKE_FROM;
        //    var ToTextBox = this.headForm.HIDUKE_TO;

        //    ToTextBox.Text = FromTextBox.Text;

        //    LogUtility.DebugMethodEnd();
        //}
        #endregion
        /// 20141201 Houkakou 「入金消込履歴」のダブルクリックを追加する　end

        /// 20141203 Houkakou 「入金消込履歴」の日付チェックを追加する　start
        #region 日付チェック
        ///// <summary>
        ///// 日付チェック
        ///// </summary>
        ///// <returns></returns>
        //internal bool DateCheck()
        //{
        //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        //    this.headForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        //    this.headForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;

        //    //nullチェック
        //    if (string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
        //    {
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
        //    {
        //        return false;
        //    }

        //    DateTime date_from = Convert.ToDateTime(this.headForm.HIDUKE_FROM.Value);
        //    DateTime date_to = Convert.ToDateTime(this.headForm.HIDUKE_TO.Value);

        //    // 日付FROM > 日付TO 場合
        //    if (date_to.CompareTo(date_from) < 0)
        //    {
        //        this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
        //        this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
        //        this.headForm.HIDUKE_FROM.BackColor = Constans.ERROR_COLOR;
        //        this.headForm.HIDUKE_TO.BackColor = Constans.ERROR_COLOR;
        //        string strFrom = this.headForm.lbl_DenpyoDate.Text + "From";
        //        string strTo = this.headForm.lbl_DenpyoDate.Text + "To";
        //        string[] errorMsg = { strFrom, strTo };
        //        msgLogic.MessageBoxShow("E030", errorMsg);
        //        this.headForm.HIDUKE_FROM.Focus();
        //        return true;
        //    }

        //    return false;
        //}
        #endregion

        #region HIDUKE_FROM_Leaveイベント
        ///// <summary>
        ///// HIDUKE_FROM_Leaveイベント
        ///// </summary>
        ///// <returns></returns>
        //private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text))
        //    {
        //        this.headForm.HIDUKE_TO.IsInputErrorOccured = false;
        //        this.headForm.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        //    }
        //}
        #endregion

        #region HIDUKE_TO_Leaveイベント
        ///// <summary>
        ///// HIDUKE_TO_Leaveイベント
        ///// </summary>
        ///// <returns></returns>
        //private void HIDUKE_TO_Leave(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text))
        //    {
        //        this.headForm.HIDUKE_FROM.IsInputErrorOccured = false;
        //        this.headForm.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        //    }
        //}
        #endregion
        /// 20141203 Houkakou 「入金消込履歴」の日付チェックを追加する　end
    }
}
