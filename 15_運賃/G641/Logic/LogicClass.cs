using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.APP.Base;
using r_framework.Authority;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Carriage.Unchinichiran.APP;
using Shougun.Core.Carriage.Unchinichiran.DAO;
using Shougun.Core.Carriage.Unchinichiran.DBAccesser;
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Utility;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Carriage.Unchinichiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class LogicClass : IBuisinessLogic
    {
        #region フィールド

        // 日付CD
        const int HIDUKE_SENTAKU_DENPYOU = 1;
        const int HIDUKE_SENTAKU_NYUURYOKU = 2;

        // 伝種区分
        const int DENPYOU_TYPE_UKEIRE = 1;
        const int DENPYOU_TYPE_SHUKKA = 2;
        const int DENPYOU_TYPE_URIAGESHIHARAI = 3;
        const int DENPYOU_TYPE_DAINOU = 4;
        const int DENPYOU_TYPE_UNCHIN = 5;
        const int DENPYOU_TYPE_ALL = 6;

        #region 非表示にする必須項目

        /// <summary>
        /// 伝票番号
        /// </summary>
        private readonly string HIDDEN_DENPYOU_NUMBER = "HIDDEN_DENPYOU_NUMBER";

        /// <summary>
        /// システムID
        /// </summary>
        private readonly string HIDDEN_SYSTEM_ID = "HIDDEN_SYSTEM_ID";

        /// <summary>
        /// 明細システムID
        /// </summary>
        private readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        #endregion

        //入力チェックメッセージタイトル
        private const String DIALOGTITLE = "インフォメーション";

        //初期表示フラグ
        private static bool InitialFlg = false;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Carriage.Unchinichiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        private DBAccessor accessor;

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>	
        /// 拠点マスタ	
        /// </summary>	
        private IM_KYOTENDao mkyotenDao;


        /// <summary>
        /// 取引先マスタ
        /// </summary>
        private IM_TORIHIKISAKIDao mtorihikisakiDao;

        /// <summary>
        /// 業者マスタ
        /// </summary>
        private IM_GYOUSHADao mgyoushaDao;

        /// <summary>
        /// 現場マスタ
        /// </summary>
        private IM_GENBADao mgenbaDao;

        /// <summary>
        /// 運搬業者マスタ
        /// </summary>
        private IM_GYOUSHADao munbangyousyaDao;

        /// <summary>
        /// 荷卸業者マスタ
        /// </summary>
        private IM_GYOUSHADao mniotosugyousyaDao;

        /// <summary>
        /// 荷卸場マスタ
        /// </summary>
        private IM_GENBADao mniotosubaDao;

        /// <summary>
        /// UnchinichiranForm form
        /// </summary>
        private UnchinichiranForm form;

        /// <summary>
        /// HeaderForm headForm
        /// </summary>
        private HeaderForm headForm;

        /// <summary>
        /// 社員コード
        /// </summary>
        public string syainCode { get; set; }

        /// <summary>
        /// 伝種区分
        /// </summary>
        public DENSHU_KBN denShu_Kbn { get; set; }

        /// <summary>
        /// 伝票種類フラグ
        /// </summary>
        public int disp_Flg { get; set; }

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string mcreateSql { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// コントロール
        /// </summary>
        private KensakuControl control;

        /// <summary>
        /// 検索結果一覧のDao
        /// </summary>
        private DAOClass daoIchiran;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        private KensakuControl kensakuControl;

        /// <summary>
        /// フッター
        /// </summary>
        private BusinessBaseForm parentForm;
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
        /// JOIN句
        /// </summary>
        public string joinQuery { get; set; }

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UnchinichiranForm targetForm)
        {

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.form = targetForm;
            this.dto = new DTOClass();
            this.daoIchiran = DaoInitUtility.GetComponent<DAOClass>();
            this.mkyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.mtorihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.mgyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mgenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.munbangyousyaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mniotosugyousyaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.mniotosubaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            // Accessor
            this.accessor = new DBAccessor();

            //検索方法によって、簡易検索／汎用検索を初期化する。
            KensakuControl hannyousearch = new KensakuControl(this);
            hannyousearch.Top = 3;
            hannyousearch.Left = 3;
            this.form.searchString.Visible = false;
            this.control = hannyousearch;
            this.form.Controls.Add(hannyousearch.label1);
            this.form.Controls.Add(hannyousearch.UNNBANGYOUSYA_CD);
            this.form.Controls.Add(hannyousearch.UNNBANGYOUSYA_NAME_RYAKU);
            this.form.Controls.Add(hannyousearch.btn_Gyousha);
        }

        public LogicClass(KensakuControl kensakuControl)
        {
            // TODO: Complete member initialization
            this.kensakuControl = kensakuControl;
        }

        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                //LogUtility.DebugMethodStart();
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode(this.form.SystemId.ToString());
                if (sysInfo != null)
                {
                    // システム情報からアラート件数を取得
                    this.alertCount = (int)sysInfo.ICHIRAN_ALERT_KENSUU;
                }
                var parentForm = (BusinessBaseForm)this.form.Parent;

                //headerFormにSettingsの値
                this.SetDenpyouHidukeInit();

                #region ポップアップ設定

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

                #endregion

                this.allControl = this.form.allControl;
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;

                ////画面の初期表示時日付CDを設定する
                if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKESENTAKU) || InitialFlg == false)
                {
                    this.headForm.txtNum_HidukeSentaku.Text = HIDUKE_SENTAKU_DENPYOU.ToString();
                }
                else
                {
                    this.headForm.txtNum_HidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKESENTAKU;
                }
                //InitialFlg = true; // No.2320

                //伝票種類、伝票区分の初期化
                SetInitialDenpyoShurui();

                ////汎用検索の取得
                control.UNNBANGYOUSYA_CD.Text = Properties.Settings.Default.SET_UNBANGYOUSYA;

                // ユーザ運搬業者名称の取得
                if (control.UNNBANGYOUSYA_CD.Text != string.Empty)
                {
                    M_GYOUSHA mUnbangyousya = new M_GYOUSHA();
                    mUnbangyousya = (M_GYOUSHA)munbangyousyaDao.GetDataByCd(control.UNNBANGYOUSYA_CD.Text);
                    if (mUnbangyousya == null)
                    {
                        control.UNNBANGYOUSYA_NAME_RYAKU.Text = "";
                    }
                    else
                    {
                        control.UNNBANGYOUSYA_NAME_RYAKU.Text = mUnbangyousya.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 上で子フォームへのデータバインドを行っている為、以下の初期化処理はそれより下に配置（二度呼ばれることを避ける）
                // ボタンのテキストを初期化
                this.ButtonInit();

                // 伝票種類のテキストボックスを初期化しておく
                this.form.txtNum_DenpyoKind.Text = string.Empty;

                // イベントの初期化処理
                this.EventInit();

                //伝票種類、伝票区分の初期化
                SetInitialDenpyoShurui();

                //検索ボタンの初期表示

                parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle("運賃一覧");

                Control[] copy = new Control[this.form.allControl.GetLength(0) + 1];

                this.form.allControl.CopyTo(copy, 1);
                copy[0] = this.headForm.txtNum_HidukeSentaku;
                this.allControl = copy;
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("WindowInit", sqlEx);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
        }

        #endregion

        #region ボタンの初期化

        /// <summary>
        /// ボタンの初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }

        #endregion

        #region イベント処理の初期化
        /// <summary>
        /// イベント処理の初期化を行う
        /// </summary>
        private void EventInit()
        {
            //LogUtility.DebugMethodStart();

            parentForm = (BusinessBaseForm)this.form.Parent;
            //customTextBoxでのエンターキー押下イベント生成
            this.form.searchString.KeyDown += new KeyEventHandler(SearchStringKeyDown);       //汎用検索(SearchString)
            parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);      //処理No(ESC)

            //伝票種類切り替えイベント
            this.form.txtNum_DenpyoKind.TextChanged += new EventHandler(this.txtNum_DenpyouSyurui_TextChanged);

            //Functionボタンのイベント生成
            parentForm.bt_func2.Click += new EventHandler(this.bt_func2_Click);              //F2 新規
            parentForm.bt_func3.Click += new System.EventHandler(this.bt_func3_Click);       //F3 修正
            parentForm.bt_func4.Click += new System.EventHandler(this.bt_func4_Click);       //F4 削除
            parentForm.bt_func5.Click += new System.EventHandler(this.bt_func5_Click);       //F5 複写
            parentForm.bt_func6.Click += new System.EventHandler(this.bt_func6_Click);       //F6 CSV出力
            parentForm.bt_func7.Click += new System.EventHandler(this.bt_func7_Click);       //F7 検索条件クリア
            parentForm.bt_func8.Click += new System.EventHandler(this.bt_func8_Click);       //F8 検索
            parentForm.bt_func10.Click += new System.EventHandler(this.bt_func10_Click);     //F10 並び替え
            parentForm.bt_func11.Click += new System.EventHandler(this.bt_func11_Click);     //F11 フィルタ
            parentForm.bt_func12.Click += new System.EventHandler(this.bt_func12_Click);     //閉じる
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);             //パターン一覧画面へ遷移
            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);

            //画面上でESCキー押下時のイベント生成
            this.form.PreviewKeyDown += new PreviewKeyDownEventHandler(form_PreviewKeyDown); //form上でのESCキー押下でFocus移動
            //  明細画面上でダブルクリック時のイベント生成
            this.form.customDataGridView1.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(customDataGridView1_MouseDoubleClick);

            // 「To」のイベント生成
            this.headForm.HIDUKE_TO.MouseDoubleClick += new MouseEventHandler(HIDUKE_TO_MouseDoubleClick);

            //LogUtility.DebugMethodEnd();
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
            if (disp_Flg != 0)
            {
                this.EditDetail(WINDOW_TYPE.NEW_WINDOW_FLAG, "");
            }
        }

        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                this.EditDetail(WINDOW_TYPE.UPDATE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F4 削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                this.EditDetail(WINDOW_TYPE.DELETE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F5 複写
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                this.EditDetail(WINDOW_TYPE.REFERENCE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();

            LogUtility.DebugMethodStart(sender, e);
            try
            {
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
                        exp.ConvertCustomDataGridViewToCsv(this.form.customDataGridView1, true, true, DENSHU_KBNExt.ToTitleString(this.form.DenshuKbn), this.form);
                    }
                }
            }
            catch
            {
                throw;
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
            //LogUtility.DebugMethodStart();

            this.form.searchString.Clear();

            // 一覧クリア
            DataTable cre = (DataTable)this.form.customDataGridView1.DataSource;
            if (cre == null)
            {
                return;
            }
            cre.Clear();
            this.form.customDataGridView1.DataSource = cre;

            //伝票日付のクリア
            this.SetDenpyouHidukeInit();

            this.headForm.ReadDataNumber.Text = "0";
            this.headForm.alertNumber.Clear();

            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();
            if (this.control.Visible == true)
            {
                this.control.UNNBANGYOUSYA_CD.Clear();
                this.control.UNNBANGYOUSYA_NAME_RYAKU.Clear();
            }
            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8検索
        /// </summary>                  
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            //LogUtility.DebugMethodStart();

            if (this.form.PatternNo == 0)
            {
                var msgLogic = new r_framework.Logic.MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            bool searchErrorFlag = false;
            this.headForm.HIDUKE_FROM.IsInputErrorOccured = false;
            this.headForm.HIDUKE_TO.IsInputErrorOccured = false;
            var allControlAndHeaderControls = allControl.ToList();
            allControlAndHeaderControls.AddRange(this.form.controlUtil.GetAllControls(this.headForm));
            var autoCheckLogic = new AutoRegistCheckLogic(allControlAndHeaderControls.ToArray(), allControlAndHeaderControls.ToArray());
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();

            if (!this.form.RegistErrorFlag)
            {
                if (!string.IsNullOrEmpty(this.headForm.HIDUKE_FROM.GetResultText())
                    && !string.IsNullOrEmpty(this.headForm.HIDUKE_TO.GetResultText()))
                {
                    DateTime dtpFrom = DateTime.Parse(this.headForm.HIDUKE_FROM.GetResultText());
                    DateTime dtpTo = DateTime.Parse(this.headForm.HIDUKE_TO.GetResultText());
                    DateTime dtpFromWithoutTime = DateTime.Parse(dtpFrom.ToShortDateString());
                    DateTime dtpToWithoutTime = DateTime.Parse(dtpTo.ToShortDateString());

                    int diff = dtpFromWithoutTime.CompareTo(dtpToWithoutTime);

                    if (0 < diff)
                    {
                        //対象期間内でないならエラーメッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        this.headForm.HIDUKE_FROM.IsInputErrorOccured = true;
                        this.headForm.HIDUKE_TO.IsInputErrorOccured = true;
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
                        else
                        {
                            string[] errorMsg = { "検収伝票日付From", "検収伝票日付To" };
                            msglogic.MessageBoxShow("E030", errorMsg);
                        }
                        this.headForm.HIDUKE_FROM.Select();
                        this.headForm.HIDUKE_FROM.Focus();
                        searchErrorFlag = true;
                    }
                }
            }

            // Ditailの行数チェックはFWでできないので自前でチェック
            if (!this.form.RegistErrorFlag && !searchErrorFlag)
            {
                //読込データ件数を取得
                this.headForm.ReadDataNumber.Text = this.Search().ToString();
                if (this.form.customDataGridView1 != null)
                {
                    this.headForm.ReadDataNumber.Text = this.form.customDataGridView1.Rows.Count.ToString();
                }
                else
                {
                    this.headForm.ReadDataNumber.Text = "0";
                }
                if (this.headForm.ReadDataNumber.Text == "0")
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("C001");
                }
            }
            //必須チェックエラーフォーカス処理
            this.SetErrorFocus();

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();
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
        private void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        #endregion

        #region プロセスボタン押下処理（※処理未実装）
        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        private void bt_process1_Click(object sender, System.EventArgs e)
        {
            var sysID = this.form.OpenPatternIchiran();

            if (!string.IsNullOrEmpty(sysID))
            {
                this.form.SetPatternBySysId(sysID);
                this.searchResult = this.form.Table;
                this.form.ShowData();
                this.selectQuery = this.form.SelectQuery;
                this.orderByQuery = this.form.OrderByQuery;
                this.joinQuery = this.form.JoinQuery;
            }
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
            //LogUtility.DebugMethodStart();

            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 start
                if (CheckDate())
                {
                    return;
                }
                // koukouei 20141021 「From　>　To」のアラート表示タイミング変更 end

                if (!string.IsNullOrEmpty(this.form.searchString.Text))
                {
                    string getSearchString = this.form.searchString.Text.Replace("\r", "").Replace("\n", "");
                    this.searchString = getSearchString;
                    this.Search();

                }
                else
                {
                    this.form.searchString.Text = "";
                    this.form.searchString.SelectionLength = this.form.searchString.Text.Length;
                    this.form.searchString.Focus();
                }

            }

            //LogUtility.DebugMethodEnd();
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
            //LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if ("1".Equals(parentForm.txb_process.Text))
            {
                //パターン一覧画面へ遷移


            }
            else if ("2".Equals(parentForm.txb_process.Text))
            {
                //検索条件設定画面へ遷移
            }

            //LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ESCキー押下イベント
        void form_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            if (e.KeyCode == Keys.Escape)
            {
                //処理No(ESC)へカーソル移動
                parentForm.txb_process.Focus();

            }

            //LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 伝種区分初期値設定
        /// <summary>
        /// 画面の伝種区分を初期化する
        /// </summary>
        private void SetInitialDenpyoShurui()
        {
            //画面の伝種区分初期値設定
            this.form.txtNum_DenpyoKind.Text = DENPYOU_TYPE_ALL.ToString();

            if (String.IsNullOrEmpty(this.form.txtNum_DenpyoKind.Text))
            {
                disp_Flg = DENPYOU_TYPE_ALL;
            }
            else
            {
                switch (int.Parse(this.form.txtNum_DenpyoKind.Text))
                {
                    //受入の場合
                    case DENPYOU_TYPE_UKEIRE:
                        this.disp_Flg = (int)DENSHU_KBN.UKEIRE;
                        break;
                    //出荷の場合
                    case DENPYOU_TYPE_SHUKKA:
                        this.disp_Flg = (int)DENSHU_KBN.SHUKKA;
                        break;
                    //売上支払の場合
                    case DENPYOU_TYPE_URIAGESHIHARAI:
                        this.disp_Flg = (int)DENSHU_KBN.URIAGE_SHIHARAI;
                        break;
                    // 運賃の場合
                    case DENPYOU_TYPE_UNCHIN:
                        this.disp_Flg = (int)DENSHU_KBN.UNCHIN;
                        break;
                    //代納の場合
                    case DENPYOU_TYPE_DAINOU:
                        this.disp_Flg = (int)DENSHU_KBN.DAINOU;
                        break;
                }
            }
        }
        #endregion

        #region 明細データダブルクリックイベント

        private void customDataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DataGridViewCellMouseEventArgs datagridviewcell = (DataGridViewCellMouseEventArgs)e;
            if (datagridviewcell.RowIndex >= 0)
            {
                string denpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[this.HIDDEN_DENPYOU_NUMBER].Value.ToString();
                this.EditDetail(WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyouNum);
            }
        }

        #endregion

        #region 検索売上確定入力一覧

        /// <summary>
        /// Where　条件
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeWhereSql(StringBuilder sql)
        {
            string tblName1 = string.Empty;
            string tblName2 = string.Empty;

            tblName1 = "T_UNCHIN_ENTRY";
            tblName2 = "T_UNCHIN_DETAIL";

            sql.Append(" WHERE ");

            sql.AppendFormat(" {0}.DELETE_FLG = 0 ", tblName1);

            sql.AppendFormat(" AND {0}.SEQ = (SELECT MAX(TMP.SEQ) FROM {0} TMP WHERE TMP.SYSTEM_ID = {0}.SYSTEM_ID) ", tblName1);

            if (this.headForm.radbtnDenpyouHiduke.Checked)
            {
                if (this.headForm.HIDUKE_FROM.Value != null || this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }

                // 日時は日付のみにしてから変換
                if (this.headForm.HIDUKE_FROM.Value != null)
                {
                    sql.AppendFormat(" CONVERT(DATETIME, CONVERT(nvarchar, {0}.DENPYOU_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }

                if (this.headForm.HIDUKE_FROM.Value != null && this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }

                if (this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.AppendFormat(" CONVERT(DATETIME, CONVERT(nvarchar, {0}.DENPYOU_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }
            else if (this.headForm.radbtnNyuuryokuHiduke.Checked)
            {
                if (this.headForm.HIDUKE_FROM.Value != null || this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }

                if (this.headForm.HIDUKE_FROM.Value != null)
                {
                    sql.AppendFormat(" CONVERT(DATETIME, CONVERT(nvarchar, {0}.UPDATE_DATE, 111), 120) >= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_FROM.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }

                if (this.headForm.HIDUKE_FROM.Value != null && this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.Append(" AND ");
                }

                if (this.headForm.HIDUKE_TO.Value != null)
                {
                    sql.AppendFormat(" CONVERT(DATETIME, CONVERT(nvarchar, {0}.UPDATE_DATE, 111), 120) <= CONVERT(DATETIME, CONVERT(nvarchar, '", tblName1);
                    sql.Append(DateTime.Parse(this.headForm.HIDUKE_TO.Value.ToString()).ToShortDateString() + "', 111), 120) ");
                }
            }

            if (!String.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text) && this.headForm.KYOTEN_CD.Text != "99")
            {
                sql.AppendFormat(" AND {0}.KYOTEN_CD = '{1}' ", tblName1, this.headForm.KYOTEN_CD.Text);
            }

            if (control.Visible == true)
            {
                if ((!String.IsNullOrEmpty(this.control.UNNBANGYOUSYA_CD.Text)))
                {
                    sql.AppendFormat(" AND {0}.UNPAN_GYOUSHA_CD = '{1}' ", tblName1, this.control.UNNBANGYOUSYA_CD.Text);
                }
            }
            if (disp_Flg != DENPYOU_TYPE_ALL)
            {
                sql.AppendFormat(" AND {0}.DENSHU_KBN_CD = '{1}' ", tblName1, disp_Flg);
            }
        }

        /// <summary>
        /// 運賃検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchUnchin(StringBuilder sql)
        {
            sql.Append(" T_UNCHIN_ENTRY ");

            // パターンの区分が明細の場合、明細テーブルを結合する
            sql.Append(" LEFT JOIN T_UNCHIN_DETAIL ");
            sql.Append(" ON T_UNCHIN_ENTRY.SYSTEM_ID = T_UNCHIN_DETAIL.SYSTEM_ID AND T_UNCHIN_ENTRY.SEQ = T_UNCHIN_DETAIL.SEQ ");
        }

        #endregion

        #region ボタン情報の設定
        /// <summary>
        /// ボタン情報の設定
        /// </summary>
        public ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

        }
        #endregion

        #region 全角の英数字の文字列を半角に変換

        /// <summary>
        /// 全角スペースを半角スペースに変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankakuSpace(string param)
        {
            Regex re = new Regex("[　]+");
            string output = re.Replace(param, MyReplacer);
            return output;
        }

        /// <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="param">半角へ変換する文字列</param>
        /// <returns></returns>
        public string ToHankaku(string param)
        {
            Regex re = new Regex("[０-９Ａ-Ｚａ-ｚ　]+");
            string output = re.Replace(param, MyReplacer);
            return output;
        }

        static string MyReplacer(Match m)
        {
            return Strings.StrConv(m.Value, VbStrConv.Narrow, 0);
        }

        #endregion

        #region 検索処理

        /// <summary>
        /// 検索処理を行う
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int ret_cnt = 0;

            //SELECT句未取得なら検索できない
            if (!string.IsNullOrEmpty(this.form.SelectQuery))
            {
                // ベースロジッククラスで作成したクエリをセット
                this.selectQuery = this.form.SelectQuery;
                this.orderByQuery = this.form.OrderByQuery;
                this.joinQuery = this.form.JoinQuery;

                string tblName1 = string.Empty;
                string tblName2 = string.Empty;
                string denpyouNum = string.Empty;

                tblName1 = "T_UNCHIN_ENTRY";
                tblName2 = "T_UNCHIN_DETAIL";
                denpyouNum = "DENPYOU_NUMBER";

                var order = new StringBuilder();
                var sql = new StringBuilder();
                sql.Append(" SELECT DISTINCT ");
                sql.Append(this.selectQuery);

                #region システム上必須な項目をSELECT句に追加する（後で非表示にする）
                sql.AppendFormat(" , {0}.DENPYOU_NUMBER AS {1} ", tblName1, this.HIDDEN_DENPYOU_NUMBER);
                order.AppendFormat(" , {0} ASC ", this.HIDDEN_DENPYOU_NUMBER);
                sql.AppendFormat(" , {0}.SYSTEM_ID AS {1} ", tblName1, this.HIDDEN_SYSTEM_ID);
                order.AppendFormat(" , {0} ASC ", this.HIDDEN_SYSTEM_ID);

                // 出力区分が明細の場合
                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 2)
                {
                    sql.AppendFormat(" , {0}.DETAIL_SYSTEM_ID AS {1} ", tblName2, this.HIDDEN_DETAIL_SYSTEM_ID);
                    order.AppendFormat(" , {0} ASC ", this.HIDDEN_DETAIL_SYSTEM_ID);
                }

                #endregion

                sql.Append(" FROM ");

                this.MakeSearchUnchin(sql);

                sql.Append(this.joinQuery);
                this.MakeWhereSql(sql);
                sql.Append(" ORDER BY ");
                sql.Append(this.orderByQuery);
                sql.Append(order);
                this.mcreateSql = sql.ToString();

                //検索実行
                this.searchResult = new DataTable();
                if (!string.IsNullOrEmpty(this.mcreateSql))
                {
                    this.searchResult = this.daoIchiran.getdateforstringsql(this.mcreateSql);
                }
                ret_cnt = searchResult.Rows.Count;

                //検索結果表示
                this.form.ShowData();
            }
            return ret_cnt;
        }
        #endregion

        #region 検索条件コントロール関連処理
        /// <summary>
        /// 伝種区分が変更されたときの処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        private void txtNum_DenpyouSyurui_TextChanged(object sender, EventArgs e)
        {
            //伝種区分に応じて状態を変更
            ChangeNyuukinsakiEnabled();

            //パターンを再表示する
            this.form.PatternUpdate();

            // パターン変更時にグリッドの内容は初期化しない
            //if (this.form.customDataGridView1 != null && this.form.customDataGridView1.Rows.Count > 0)
            //{
            //    DataTable dt = new DataTable();
            //    dt = (DataTable)this.form.customDataGridView1.DataSource;
            //    dt.Clear();
            //    this.form.customDataGridView1.DataSource = dt;
            //}
        }

        /// <summary>
        /// 伝種区分に応じて入金先条件のコントロールの状態を変更します。
        /// </summary>
        private void ChangeNyuukinsakiEnabled()
        {
            //伝種区分によって制御を変える
            if (!string.IsNullOrEmpty(this.form.txtNum_DenpyoKind.Text))
            {
                switch (int.Parse(this.form.txtNum_DenpyoKind.Text))
                {
                    //受入の場合
                    case DENPYOU_TYPE_UKEIRE:
                        this.disp_Flg = (int)DENSHU_KBN.UKEIRE;
                        break;
                    //出荷の場合
                    case DENPYOU_TYPE_SHUKKA:
                        this.disp_Flg = (int)DENSHU_KBN.SHUKKA;
                        break;
                    //売上支払の場合
                    case DENPYOU_TYPE_URIAGESHIHARAI:
                        this.disp_Flg = (int)DENSHU_KBN.URIAGE_SHIHARAI;
                        break;
                    case DENPYOU_TYPE_UNCHIN:
                        this.disp_Flg = (int)DENSHU_KBN.UNCHIN;
                        break;
                    //代納の場合
                    case DENPYOU_TYPE_DAINOU:
                        this.disp_Flg = (int)DENSHU_KBN.DAINOU;
                        break;
                    case DENPYOU_TYPE_ALL:
                        this.disp_Flg = DENPYOU_TYPE_ALL;
                        break;
                }
                this.form.DenshuKbn = DENSHU_KBN.UNCHIN_ICHIRAN;
                this.form.PatternUpdate();
            }
        }
        #endregion

        /// <summary>
        /// システム上必須な項目を非表示にします
        /// </summary>
        internal void HideColumnHeader()
        {
            foreach (DataGridViewColumn column in this.form.customDataGridView1.Columns)
            {
                if (column.Name.Equals(this.HIDDEN_DENPYOU_NUMBER) ||
                    column.Name.Equals(this.HIDDEN_SYSTEM_ID) ||
                    column.Name.Equals(this.HIDDEN_DETAIL_SYSTEM_ID)
                    )
                {
                    column.Visible = false;
                }
                if (column.Name.Equals("正味合計"))
                {
                    column.DefaultCellStyle.Format = "#,##0.####";
                }
            }
        }

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal void CheckUnpanGyousha()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                LogUtility.DebugMethodStart();
                // 初期化
                this.control.UNNBANGYOUSYA_NAME_RYAKU.Text = string.Empty;
                this.control.UNNBANGYOUSYA_NAME_RYAKU.ReadOnly = true;

                if (string.IsNullOrEmpty(this.control.UNNBANGYOUSYA_CD.Text))
                {
                    return;
                }

                var gyousha = this.accessor.GetGyousha(this.control.UNNBANGYOUSYA_CD.Text);
                if (gyousha == null)
                {
                    // エラーメッセージ
                    msgLogic.MessageBoxShow("E020", "運搬業者");
                    this.control.UNNBANGYOUSYA_CD.Focus();
                    return;
                }


                // 事業場区分、荷卸現場区分チェック
                // 20151023 BUNN #12040 STR
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                // 20151023 BUNN #12040 END
                {
                    this.control.UNNBANGYOUSYA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 一致するデータがないのでエラー
                    msgLogic.MessageBoxShow("E020", "運搬業者");
                    this.control.UNNBANGYOUSYA_CD.Focus();
                }
            }
            catch (SQLRuntimeException sqlEx)
            {
                LogUtility.Error("CheckUnpanGyousha", sqlEx);
                msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUnpanGyousha", ex);
                msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

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
        /// header設定
        /// </summary>
        /// /// <returns></returns>
        public void SetHeader(HeaderForm hs)
        {
            this.headForm = hs;
        }
        /// <summary>
        /// 入力画面表示
        /// </summary>
        /// <param name="wintype"></param>
        /// <param name="DenpyouNum"></param>
        private void EditDetail(WINDOW_TYPE wintype, string denpyouNum)
        {
            long denpyouNumLong = -1;
            string strFormId = "G153";

            if (!string.IsNullOrEmpty(denpyouNum))
            {
                denpyouNumLong = long.Parse(denpyouNum);
            }

            switch (wintype)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    // 新規モードで起動
                    FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG);
                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    // 修正モードの権限チェック
                    if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyouNumLong);
                    }
                    // 参照モードの権限チェック
                    else if (Manager.CheckAuthority(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                    {
                        FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNumLong);
                    }
                    else
                    {
                        // 修正モードの権限なしのアラームを上げる
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E158", "修正");
                    }
                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    // 削除モードで起動
                    FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.DELETE_WINDOW_FLAG, WINDOW_TYPE.DELETE_WINDOW_FLAG, denpyouNumLong);
                    break;
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    // 複写モードで起動（新規モード）
                    FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, denpyouNumLong);
                    break;
                default:
                    break;
            }

        }

        internal void SetPrevStatus(object sender, EventArgs e)
        {
            InitialFlg = true; // No.2320
            //拠点、伝票日付From、伝票日付To、確定区分、伝票種類の項目をセッティングファイルに保存する。

            if (this.headForm.KYOTEN_CD.Text != "")
            {
                Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.KYOTEN_CD.Text;
            }
            else
            {
                Properties.Settings.Default.SET_KYOTEN_CD = null;
            }

            Properties.Settings.Default.SET_HIDUKESENTAKU = this.headForm.txtNum_HidukeSentaku.Text;

            Properties.Settings.Default.SET_UNBANGYOUSYA = this.control.UNNBANGYOUSYA_CD.Text;

            DateTime resultDt;

            if (!String.IsNullOrEmpty(this.headForm.HIDUKE_FROM.Text.Trim()) && DateTime.TryParse(this.headForm.HIDUKE_FROM.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_FROM = this.headForm.HIDUKE_FROM.Text;
            }

            if (!String.IsNullOrEmpty(this.headForm.HIDUKE_TO.Text.Trim()) && DateTime.TryParse(this.headForm.HIDUKE_TO.Text.Trim(), out resultDt))
            {
                Properties.Settings.Default.SET_HIDUKE_TO = this.headForm.HIDUKE_TO.Text;
            }

            Properties.Settings.Default.SET_DENPYO_KIND_CD = this.form.txtNum_DenpyoKind.Text;

            // 保存
            Properties.Settings.Default.Save();
        }

        #region 必須チェックエラーフォーカス処理
        /// <summary>
        /// 必須チェックエラーフォーカス処理
        /// </summary>
        /// <returns></returns>
        private void SetErrorFocus()
        {
            Control target = null;
            foreach (Control control in this.form.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        if (target != null)
                        {
                            if (target.TabIndex > control.TabIndex)
                            {
                                target = control;
                            }
                        }
                        else
                        {
                            target = control;
                        }
                    }
                }
            }
            //ヘッダーチェック
            foreach (Control control in this.headForm.allControl)
            {
                if (control is ICustomTextBox)
                {
                    if (((ICustomTextBox)control).IsInputErrorOccured)
                    {
                        target = control;
                    }
                }
            }
            if (target != null)
            {
                target.Focus();
            }
        }

        #endregion 必須チェックエラーフォーカス処理

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
                else
                {
                    string[] errorMsg = { "検収伝票日付From", "検収伝票日付To" };
                    msglogic.MessageBoxShow("E030", errorMsg);
                }
                this.headForm.HIDUKE_FROM.Focus();
                return true;
            }
            return false;
        }
        #endregion

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

        //thongh 2015/09/14 #13030 start
        /// <summary>
        /// 伝票日付初期値設定
        /// </summary>
        private void SetDenpyouHidukeInit()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_TO) || InitialFlg == false)
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.headForm.HIDUKE_TO.Value = System.DateTime.Now;
                this.headForm.HIDUKE_TO.Value = this.parentForm.sysDate;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
            else
            {
                this.headForm.HIDUKE_TO.Value = Convert.ToDateTime(Properties.Settings.Default.SET_HIDUKE_TO.ToString());
            }

            if (string.IsNullOrEmpty(Properties.Settings.Default.SET_HIDUKE_FROM) || InitialFlg == false)
            {
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.headForm.HIDUKE_FROM.Value = System.DateTime.Now;
                this.headForm.HIDUKE_FROM.Value = this.parentForm.sysDate;
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            }
            else
            {
                this.headForm.HIDUKE_FROM.Value = Convert.ToDateTime(Properties.Settings.Default.SET_HIDUKE_FROM.ToString());
            }
        }
        //thongh 2015/09/14 #13030 end
    }
}