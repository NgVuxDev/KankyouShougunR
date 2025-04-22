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
using Shougun.Core.Common.BusinessCommon.Accessor;
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.SalesPayment.Tairyuichiran.Const;
using Seasar.Framework.Exceptions;
using r_framework.CustomControl;

namespace Shougun.Core.SalesPayment.Tairyuichiran
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        #region フィールド

        /// <summary>
        /// DTO
        /// </summary>
        private DTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// HeaderForm
        /// </summary>
        private UIHeader headForm;

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao m_kyotendao;

        /// <summary>
        /// 車輌マスタ
        /// </summary>
        private IM_SHARYOUDao m_sharyoudao;

        /// <summary>
        /// 業者マスタ
        /// </summary>
        private IM_GYOUSHADao m_gyoushadao;

        /// <summary>
        /// メッセージクラス
        /// </summary>
        private MessageBoxShowLogic msgcls;

        /// <summary>
        /// ローカルのDao
        /// </summary>
        private DAOClass tairyu_dao;

        /// <summary>
        /// 画面ID：受入入力
        /// </summary>
        private string UKEIRE_NYUURYOKU = "G051";

        /// <summary>
        /// 画面ID：Big受入入力
        /// </summary>
        private string BIG_UKEIRE_NYUURYOKU = "G721";

        /// <summary>
        /// 画面ID：出荷入力
        /// </summary>
        private string SYUKKA_NYUURYOKU = "G053";

        /// <summary>
        /// 画面ID：Big出荷入力
        /// </summary>
        private string BIG_SYUKKA_NYUURYOKU = "G722";

        /// <summary>
        /// 伝票種類：受入
        /// </summary>
        private string DENPYOU_SYURUI_UKEIRE = "受入";

        /// <summary>
        /// 伝票種類：出荷
        /// </summary>
        private string DENPYOU_SYURUI_SYUKKA = "出荷";

        private IM_SYS_INFODao sysInfoDao;
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// 画面独自のベースフォーム
        /// </summary>
        //private ExBaseForm exBaseForm;

        #endregion フィールド

        #region プロパティ

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
        /// 検索結果
        /// </summary>
        public DataTable searchResult { get; set; }

        /// <summary>
        /// 作成したSQL
        /// </summary>
        public string createSql { get; set; }


        #endregion プロパティ

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">フォームオブジェクト</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dto = new DTOClass();
            this.tairyu_dao = DaoInitUtility.GetComponent<DAOClass>();
            this.m_kyotendao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.m_sharyoudao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.m_gyoushadao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();

            // メッセージ出力用
            this.msgcls = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion コンストラクタ

        #region 初期処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (BusinessBaseForm)this.form.Parent;
                this.headForm = (UIHeader)parentForm.headerForm;
                parentForm.bt_process3.Enabled = false;

                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();
                // グリッドスタイル設定
                this.SetStyleDtGridView();
                // 初期値設定
                this.SetHeaderInit();
                this.SetFormInit();
                this.SetKensu();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            string ButtonInfoXmlPath = this.GetType().Namespace;
            ButtonInfoXmlPath = ButtonInfoXmlPath + ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath));
            return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;

            //Functionボタンイベント生成
            parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);
            parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);
            parentForm.bt_func5.Click += new EventHandler(bt_func5_Click);
            parentForm.bt_func6.Click += new EventHandler(bt_func6_Click);
            parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);       // No.2292
            parentForm.bt_func8.Click += new EventHandler(bt_func8_Click);
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);
            //プロセスボタンイベント生成
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);

            parentForm.FormClosing += new FormClosingEventHandler(SetPrevStatus);   // No.2002

            this.form.UNPAN_GYOUSHA_CD.Validated += new EventHandler(this.form.UNPAN_GYOUSHA_CDValidated);

            //ダブルクリック時
            this.form.customDataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(customDataGridView1_CellDoubleClick);

            //受入出荷画面サイズ選択取得
            HearerSysInfoInit();

            LogUtility.DebugMethodEnd();
        }

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

        /// <summary>
        /// ヘッダ初期値設定
        /// </summary>
        private void SetHeaderInit()
        {
            //前回保存値がない場合はシステム設定ファイルから拠点CDを設定する
            //拠点CDを取得
            XMLAccessor fileAccess = new XMLAccessor();
            CurrentUserCustomConfigProfile configProfile = fileAccess.XMLReader_CurrentUserCustomConfigProfile();
            this.headForm.txtKyotenCD.Text = String.Format("{0:D2}", int.Parse(configProfile.ItemSetVal1));

            // ユーザ拠点名称の取得
            if (this.headForm.txtKyotenCD.Text != null)
            {
                M_KYOTEN mKyoten = new M_KYOTEN();
                mKyoten = (M_KYOTEN)m_kyotendao.GetDataByCd(this.headForm.txtKyotenCD.Text);
                if (mKyoten == null || this.headForm.txtKyotenCD.Text == "")
                {
                    this.headForm.txtKyotenNameRyaku.Text = "";
                }
                else
                {
                    this.headForm.txtKyotenNameRyaku.Text = mKyoten.KYOTEN_NAME_RYAKU;
                }
            }
        }

        /// <summary>
        /// メインフォーム初期値設定
        /// </summary>
        private void SetFormInit()
        {
            //前回入力値
            this.form.txtDenpyouKind.Text = Properties.Settings.Default.SET_DENPYOU_KIND;
            this.form.UNPAN_GYOUSHA_CD.Text = Properties.Settings.Default.SET_GYOUSHA_CD;
            this.form.SHARYOU_CD.Text = Properties.Settings.Default.SET_SHARYOU_CD;
            //初期値
            this.form.txtJikaiKeizokuKeiryou.Text = "2";
            this.form.txtUkeireNumber.Text = "";
            this.form.txtSyukkaNumber.Text = "";
            this.form.txtDenpyouKind.Text = "3";

            // 車輌名称の取得
            this.form.SHARYOU_NAME_RYAKU.Text = "";
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text) && !string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                M_SHARYOU mSharyou = new M_SHARYOU();
                mSharyou.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                mSharyou.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                mSharyou = (M_SHARYOU)m_sharyoudao.GetDataByCd(mSharyou);
                if (mSharyou == null)
                {
                    this.form.SHARYOU_CD.Text = string.Empty;
                    this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                }
                else
                {
                    this.form.SHARYOU_NAME_RYAKU.Text = mSharyou.SHARYOU_NAME_RYAKU;
                }
            }
            else
            {
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
            }

            // 運搬業者名称の取得
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                M_GYOUSHA entity = new M_GYOUSHA();
                entity.ISNOT_NEED_DELETE_FLG = true;
                var unpanGyousha = this.m_gyoushadao.GetAllValidData(entity).FirstOrDefault(s => s.GYOUSHA_CD == this.form.UNPAN_GYOUSHA_CD.Text);
                if (unpanGyousha == null || !unpanGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                    this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                }
                else
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
            }
            else
            {
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 画面でDataGridViewのスタイル設定
        /// </summary>
        private void SetStyleDtGridView()
        {
            //行の追加オプション(false)
            this.form.customDataGridView1.AllowUserToAddRows = false;
        }

        #endregion 初期処理

        #region Functionボタン 押下処理

        /// <summary>
        /// F1 受入入力
        /// </summary>
        public void bt_func1_Click(object sender, EventArgs e)
        {
            if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
            {
                // 新規モードで起動
                FormManager.OpenFormWithAuth(UKEIRE_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            else
            {
                // 新規モードで起動
                FormManager.OpenFormWithAuth(BIG_UKEIRE_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// F2 出荷入力
        /// </summary>
        public void bt_func2_Click(object sender, EventArgs e)
        {
            if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
            {
                // 新規モードで起動
                FormManager.OpenFormWithAuth(SYUKKA_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
            else
            {
                // 新規モードで起動
                FormManager.OpenFormWithAuth(BIG_SYUKKA_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG);
            }
        }

        /// <summary>
        /// F5 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;

            if (datagridviewcell != null)
            {
                // 選択行の伝票番号を取得
                string DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells["HIDDEN_DENPYOU_NUMBER"].Value.ToString();
                
                // 選択行の伝票を削除モードで表示
                EditDetail(WINDOW_TYPE.DELETE_WINDOW_FLAG, DenpyouNum);
            }
            else
            {
                //アラートを表示し、画面遷移しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "対象データ");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        public void bt_func6_Click(object sender, EventArgs e)
        {
            //            OutputCSV();
            // No.2122
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
            catch (Exception ex)
            {
                throw ex;
            }

            LogUtility.DebugMethodEnd();
        }

        // No.2292 F7追加
        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        public void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.form.searchString.Clear();

            // 一覧クリア
            DataTable cre = (DataTable)this.form.customDataGridView1.DataSource;
            if (cre == null)
            {
                return;
            }
            cre.Clear();
            this.form.customDataGridView1.DataSource = cre;

            // ヘッダ部クリア
            this.form.customSortHeader1.ClearCustomSortSetting();
            if (this.form.Visible == true)
            {
                this.form.SHARYOU_CD.Clear();
                this.form.SHARYOU_NAME_RYAKU.Clear();
                this.form.UNPAN_GYOUSHA_CD.Clear();
                this.form.UNPAN_GYOUSHA_NAME.Clear();
                this.form.txtUkeireNumber.Clear();
                this.form.txtSyukkaNumber.Clear();
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            this.bt_func8_Click(true);
        }

        /// <summary>
        /// F8 検索処理
        /// 別画面からFormMangerのUpdateで検索実行された場合に0件時のアラートを表示させないためにオーバーロードしている。
        /// </summary>
        /// <param name="isShowMsg">True：検索結果が0件時のアラートを表示する　False：表示しない</param>
        public bool bt_func8_Click(bool isShowMsg)
        {
            try
            {
                //滞留数更新
                this.SetKensu();

                // 検索が呼ばれたのでフラグをクリア
                this.form.isSearch = false;

                // 必須チェック（伝票種類のみ） ※ボタンによってチェック対象が異なるためautocheckは使用していない
                if (string.IsNullOrEmpty(this.form.txtDenpyouKind.Text))
                {
                    msgcls.MessageBoxShow("E001", "伝票種類");
                    this.form.txtDenpyouKind.IsInputErrorOccured = true;
                    this.form.txtDenpyouKind.Focus();
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // パターンチェック
                if (this.form.PatternNo == 0)
                {
                    msgcls.MessageBoxShow("E057", "パターンが登録", "検索");
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                // 検索
                if (Search() == 0 && isShowMsg)
                {
                    // データが0件の場合メッセージ表示
                    msgcls.MessageBoxShow("E076");
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("bt_func8_Click", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                this.msgcls.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// F10 並び替え
        /// </summary>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            this.form.customSortHeader1.ShowCustomSortSettingDialog();

            //// 滞留番号採番
            //DataTable dataGridView = this.form.customDataGridView1.DataSource as DataTable;
            //var dataView = dataGridView.DefaultView;
            //DataTable dtEdit = dataView.ToTable();
            //dtEdit.Clear();

            //// コピーに結果をセット
            //for (int i = 0; i < dataView.ToTable().Rows.Count; i++)
            //{
            //    DataRow rowNew = dtEdit.NewRow();

            //    for (int j = 0; j < dtEdit.Columns.Count; j++)
            //    {
            //        if (dtEdit.Columns[j].ColumnName.Contains("滞留番号"))
            //        {
            //            // 滞留番号を振る
            //            rowNew[j] = i + 1;
            //        }
            //        else
            //        {
            //            rowNew[j] = dataView[i][j];
            //        }
            //    }
            //    dtEdit.Rows.Add(rowNew);
            //}

            //// セット
            //this.searchResult = dtEdit;

            //// 画面表示
            //this.form.ShowData();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            // 以下の項目をセッティングファイルに保存する
            Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.txtKyotenCD.Text;     //拠点CD
            Properties.Settings.Default.SET_SHARYOU_CD = this.form.SHARYOU_CD.Text;         //車輌コード
            Properties.Settings.Default.SET_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;   //運搬業者コード
            Properties.Settings.Default.SET_DENPYOU_KIND = this.form.txtDenpyouKind.Text;

            Properties.Settings.Default.Save();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.Close();
        }

        #endregion Functionボタン 押下処理

        #region プロセスボタン押下処理

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

        #endregion プロセスボタン押下処理

        #region メソッド
        /// <summary>
        /// 滞留数などのセット
        /// </summary>
        private void SetKensu()
        {
            // 拠点CD条件作成
            string kyotenCd = null;
            if (!string.IsNullOrEmpty(this.headForm.txtKyotenCD.Text)
                && !this.headForm.txtKyotenCD.Text.Equals(CommonConst.KYOTEN_CD_ZENSHA.ToString()))
            {
                kyotenCd = this.headForm.txtKyotenCD.Text;
            }

            // 受入滞留数
            long cntUTairyu = 0;
            // 「滞留登録区分=1」
            cntUTairyu = tairyu_dao.GetEntryCount((int)UIConstans.targetTable.UKEIRE, 1, 0, kyotenCd);
            this.form.txtUkeireTairyuNumber.Text = cntUTairyu.ToString();

            // 出荷滞留数
            long cntSTairyu = 0;
            // 「滞留登録区分=1」
            cntSTairyu = tairyu_dao.GetEntryCount((int)UIConstans.targetTable.SHUKKA, 1, 0, kyotenCd);
            this.form.txtSyukkaTairyuNumber.Text = cntSTairyu.ToString();

            // 本日受入台数
            long cntUDaisuu = 0;
            // 「滞留登録区分=0」かつ「作成日付=[SQLサーバーGetDate]」
            cntUDaisuu = tairyu_dao.GetEntryCount((int)UIConstans.targetTable.UKEIRE, 0, 1, kyotenCd);
            this.form.txtHonzituUkeireNumber.Text = cntUDaisuu.ToString();

            // 本日出荷台数
            long cntSDaisuu = 0;
            // 「滞留登録区分=0」かつ「作成日付=[SQLサーバーGetDate]」
            cntSDaisuu = tairyu_dao.GetEntryCount((int)UIConstans.targetTable.SHUKKA, 0, 1, kyotenCd);
            this.form.txtHonzituSyukkaNumber.Text = cntSDaisuu.ToString();

            // 本日受入数量
            decimal cntUMount = 0;
            // 「滞留登録区分=0」かつ「作成日付=[SQLサーバーGetDate]」
            cntUMount = tairyu_dao.GetNetTotal((int)UIConstans.targetTable.UKEIRE, kyotenCd);
            this.form.txtHonzituUkeireMount.Text = cntUMount.ToString();

            // 本日出荷数量
            decimal cntSMount = 0;
            // 「滞留登録区分=0」かつ「作成日付=[SQLサーバーGetDate]」
            cntSMount = tairyu_dao.GetNetTotal((int)UIConstans.targetTable.SHUKKA, kyotenCd);
            this.form.txtHonzituSyukkaMount.Text = cntSMount.ToString();
        }


        /// <summary>
        /// 明細ダブルクリック時処理
        /// </summary>
        public void customDataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            // 必須チェック（次回継続計量のみ） ※ボタンによってチェック対象が異なるためautocheckは使用していない
            if (string.IsNullOrEmpty(this.form.txtJikaiKeizokuKeiryou.Text))
            {
                msgcls.MessageBoxShow("E001", "次回継続計量");
                this.form.txtJikaiKeizokuKeiryou.IsInputErrorOccured = true;
                this.form.txtJikaiKeizokuKeiryou.Focus();
                return;
            }

            // カレント行の伝票番号と伝票種類によって画面を起動
            string strFormId = "";
            string strDenpyouNo = "";

            if (this.form.customDataGridView1.CurrentRow == null)
            {
                return;
            }
            strDenpyouNo = this.form.customDataGridView1.CurrentRow.Cells[UIConstans.HIDDEN_DENPYOU_NUMBER].Value.ToString();

            UIConstans.targetTable searchTarget = 0;
            if (this.form.customDataGridView1.CurrentRow.Cells[UIConstans.DENPYOU_SHURUI].Value.ToString().Equals(DENPYOU_SYURUI_UKEIRE))
            {
                // 受入入力へ遷移
                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    strFormId = UKEIRE_NYUURYOKU;
                }
                else
                {
                    strFormId = BIG_UKEIRE_NYUURYOKU;
                }
                searchTarget = UIConstans.targetTable.UKEIRE;
            }
            else if (this.form.customDataGridView1.CurrentRow.Cells[UIConstans.DENPYOU_SHURUI].Value.ToString().Equals(DENPYOU_SYURUI_SYUKKA))
            {
                // 出荷入力へ遷移
                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    strFormId = SYUKKA_NYUURYOKU;
                }
                else
                {
                    strFormId = BIG_SYUKKA_NYUURYOKU;
                }
                searchTarget = UIConstans.targetTable.SHUKKA;
            }

            // データチェック
            var ret = tairyu_dao.GetEntryExists((int)searchTarget, strDenpyouNo);
            if (ret.Rows.Count == 0)
            {
                // データがない場合はエラー
                msgcls.MessageBoxShow("E045");
                return;
            }
            else if (!(bool)ret.Rows[0]["TAIRYUU_KBN"])
            {
                msgcls.MessageBoxShow("E272");
                return;
            }

            if (!string.IsNullOrEmpty(strFormId))
            {
                bool keizokuKeiryou = false;
                if (this.form.txtJikaiKeizokuKeiryou.Text.ToString().Equals("1"))
                {
                    keizokuKeiryou = true;
                }

                //修正モードで起動
                //FormManager.OpenForm(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, long.Parse(strDenpyouNo), null, -1L, -1L, keizokuKeiryou);
                FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, long.Parse(strDenpyouNo), null, -1L, -1L, keizokuKeiryou, true);    // No.2334→元に戻す
            }
        }

        /// <summary>
        /// 受入番号入力時処理
        /// </summary>
        public bool EnterUkeireNo()
        {
            try
            {
                // 必須チェック（次回継続計量のみ） ※ボタンによってチェック対象が異なるためautocheckは使用していない
                if (string.IsNullOrEmpty(this.form.txtJikaiKeizokuKeiryou.Text))
                {
                    msgcls.MessageBoxShow("E001", "次回継続計量");
                    this.form.txtJikaiKeizokuKeiryou.IsInputErrorOccured = true;
                    this.form.txtJikaiKeizokuKeiryou.Focus();
                    return false;
                }

                // 受入入力テーブルを検索
                Cursor.Current = Cursors.WaitCursor;
                var ret = tairyu_dao.GetEntryExists((int)UIConstans.targetTable.UKEIRE, this.form.txtUkeireNumber.Text);
                Cursor.Current = Cursors.Default;
                if (ret.Rows.Count == 0)
                {
                    // データがない場合はエラー
                    msgcls.MessageBoxShow("E045");
                    this.form.txtUkeireNumber.Focus();
                    return false;
                }
                else if (!(bool)ret.Rows[0]["TAIRYUU_KBN"])
                {
                    msgcls.MessageBoxShow("E272");
                    return false;
                }
                else
                {
                    // 受入入力画面を修正モードで起動
                    if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        // 既存モードで起動
                        FormManager.OpenFormWithAuth(UKEIRE_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, long.Parse(this.form.txtUkeireNumber.Text), null, -1L, -1L, false, true);  // No.2334→元に戻す
                    }
                    else
                    {
                        // 新規モードで起動
                        FormManager.OpenFormWithAuth(BIG_UKEIRE_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, long.Parse(this.form.txtUkeireNumber.Text), null, -1L, -1L, false, true);  // No.2334→元に戻す
                    }
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("EnterUkeireNo", ex2);
                this.msgcls.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EnterUkeireNo", ex);
                this.msgcls.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 出荷番号入力時処理
        /// </summary>
        public bool EnterShukkaNo()
        {
            try
            {
                // 必須チェック（次回継続計量のみ） ※ボタンによってチェック対象が異なるためautocheckは使用していない
                if (string.IsNullOrEmpty(this.form.txtJikaiKeizokuKeiryou.Text))
                {
                    msgcls.MessageBoxShow("E001", "次回継続計量");
                    this.form.txtJikaiKeizokuKeiryou.IsInputErrorOccured = true;
                    this.form.txtJikaiKeizokuKeiryou.Focus();
                    return false;
                }

                // 出荷入力テーブルを検索
                Cursor.Current = Cursors.WaitCursor;
                var ret = tairyu_dao.GetEntryExists((int)UIConstans.targetTable.SHUKKA, this.form.txtSyukkaNumber.Text);
                Cursor.Current = Cursors.Default;
                if (ret.Rows.Count == 0)
                {
                    // データがない場合はエラー
                    msgcls.MessageBoxShow("E045");
                    this.form.txtSyukkaNumber.Focus();
                    return false;
                }
                else if (!(bool)ret.Rows[0]["TAIRYUU_KBN"])
                {
                    msgcls.MessageBoxShow("E272");
                    return false;
                }
                else
                {
                    // 出荷入力画面を修正モードで起動
                    if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                    {
                        // 既存モードで起動
                        FormManager.OpenFormWithAuth(SYUKKA_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, long.Parse(this.form.txtSyukkaNumber.Text), null, -1L, -1L, false, true);  // No.2334→元に戻す
                    }
                    else
                    {
                        // 新規モードで起動
                        FormManager.OpenFormWithAuth(BIG_SYUKKA_NYUURYOKU, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, long.Parse(this.form.txtSyukkaNumber.Text), null, -1L, -1L, false, true);  // No.2334→元に戻す
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EnterShukkaNo", ex1);
                this.msgcls.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EnterShukkaNo", ex);
                this.msgcls.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }

        /// <summary>
        /// CSV出力処理
        /// </summary>
        internal void OutputCSV()
        {
            //次期開発のため未実装

            //LogUtility.DebugMethodStart();

            //// 検索実行が行われている？
            //if (this.form.customDataGridView1.Rows.Count > 0)
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)			// CSV出力しますか？
            //    {
            //        // CSVLogicにGrid情報を格納
            //        CSVFileLogic CSVLogic = new CSVFileLogic();
            //        CSVLogic.DataGridVew = this.form.customDataGridView1;

            //        /**********************************************************************/
            //        /**		TODO: rowHeaderCell1のValueに何らかの値が入ってないと		**/
            //        /**		Errorが発生するため対策が必要							**/
            //        /**********************************************************************/

            //        // 出力ファイル名セット
            //        WINDOW_ID id = this.form.WindowId;
            //        CSVLogic.FileName = id.ToTitleString();

            //        // ヘッダー情報出力ON
            //        CSVLogic.headerOutputFlag = true;

            //        // CSV出力
            //        CSVLogic.CreateCSVFileForDataGrid();

            //        // CSV出力完了しました
            //        msgLogic.MessageBoxShow("I000");
            //    }
            //}

            //LogUtility.DebugMethodEnd();
        }

        #region 検索処理関連

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            this.searchResult = new DataTable();
            DataTable dtEdit = new DataTable();
            DataTable dtResult = new DataTable();

            Cursor.Current = Cursors.WaitCursor;

            // パターン未取得時は検索不可
            if (!string.IsNullOrEmpty(this.form.SelectQuery))
            {
                // 検索用SQL作成
                MakeSearchSql();

                // 検索実行
                dtResult = tairyu_dao.GetDateForStringSql(this.createSql);
            }
            // 結果を編集するために構造コピー
            dtEdit = dtResult.Clone();

            // コピーに結果をセット
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                DataRow rowNew = dtEdit.NewRow();

                for (int j = 0; j < dtEdit.Columns.Count; j++)
                {
                    if (dtEdit.Columns[j].ColumnName.Contains("滞留番号"))
                    {
                        // 滞留番号を振る
                        rowNew[j] = i + 1;
                    }
                    else
                    {
                        rowNew[j] = dtResult.Rows[i][j];
                    }
                }
                dtEdit.Rows.Add(rowNew);
            }
            // セット
            this.searchResult = dtEdit;

            // 画面表示
            this.form.ShowData();

            Cursor.Current = Cursors.Default;

            LogUtility.DebugMethodEnd();

            return this.searchResult.Rows.Count;

        }

        /// <summary>
        /// SQL作成
        /// </summary>
        public void MakeSearchSql()
        {
            //SQL文格納StringBuilder
            var sql = new StringBuilder();
            //SQL文格納StringBuilder
            sql.Append(" SELECT DISTINCT ");
            sql.AppendFormat(" 0 AS {0}, ", UIConstans.TAIRYU_NUMBER);
            sql.AppendFormat(UIConstans.ENTRY_TABLE + ".{0}, ", UIConstans.DENPYOU_SHURUI);
            // SELECT内容はベースクラスでパターンから取得される
            sql.Append(this.form.SelectQuery);
            sql.AppendFormat(" , {0}.DENPYOU_NUMBER AS {1} ", UIConstans.ENTRY_TABLE, UIConstans.HIDDEN_DENPYOU_NUMBER);
            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
            {
                sql.AppendFormat(" , {0}.DETAIL_SYSTEM_ID AS {1} ", UIConstans.DETAIL_TABLE, UIConstans.HIDDEN_DETAIL_SYSTEM_ID);
            }

            sql.Append(" FROM (");

            // FROM句内
            switch (this.form.txtDenpyouKind.Text)
            {
                case "1":
                    this.MakeSearchUkeireEntry(sql);
                    break;
                case "2":
                    this.MakeSearchShukkaEntry(sql);
                    break;
                case "3":
                    this.MakeSearchUkeireEntry(sql);
                    sql.Append(" UNION ALL ");
                    this.MakeSearchShukkaEntry(sql);
                    break;
            }

            sql.AppendFormat(" ) AS {0} ", UIConstans.ENTRY_TABLE);

            if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
            {
                sql.Append(" LEFT JOIN (");

                // FROM句内
                switch (this.form.txtDenpyouKind.Text)
                {
                    case "1":
                        this.MakeSearchUkeireDetail(sql);
                        break;
                    case "2":
                        this.MakeSearchShukkaDetail(sql);
                        break;
                    case "3":
                        this.MakeSearchUkeireDetail(sql);
                        sql.Append(" UNION ALL ");
                        this.MakeSearchShukkaDetail(sql);
                        break;
                }

                sql.AppendFormat(" ) AS {0} ", UIConstans.DETAIL_TABLE);
                sql.AppendFormat(" ON {0}.SYSTEM_ID = {1}.SYSTEM_ID ", UIConstans.ENTRY_TABLE, UIConstans.DETAIL_TABLE);
                sql.AppendFormat(" AND {0}.SEQ = {1}.SEQ ", UIConstans.ENTRY_TABLE, UIConstans.DETAIL_TABLE);
                sql.AppendFormat(" AND {0}.DENPYOU_NUMBER = {1}.DENPYOU_NUMBER ", UIConstans.ENTRY_TABLE, UIConstans.DETAIL_TABLE);
                //AND SUMMARY_ENTRY.伝票種類 = SUMMARY_DETAIL.伝票種類
                sql.AppendFormat(" AND {0}.{2} = {1}.{2} ", UIConstans.ENTRY_TABLE, UIConstans.DETAIL_TABLE, UIConstans.DENPYOU_SHURUI);

            }

            sql.Append(this.form.JoinQuery);

            // ORDERBYはベースクラスでパターンから取得される
            if (!string.IsNullOrEmpty(this.form.OrderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.form.OrderByQuery);
                sql.AppendFormat(" , {0} ASC ", UIConstans.TAIRYU_NUMBER);
                sql.AppendFormat(" , {0} ASC ", UIConstans.DENPYOU_SHURUI);
                sql.AppendFormat(" , {0} ASC ", UIConstans.HIDDEN_DENPYOU_NUMBER);
                if (this.form.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == (int)OUTPUT_KBN.MEISAI)
                {
                    sql.AppendFormat(" , {0} ASC ", UIConstans.HIDDEN_DETAIL_SYSTEM_ID);
                }
            }

            this.createSql = sql.ToString();
        }

        /// <summary>
        /// 受入伝票検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchUkeireEntry(StringBuilder sql)
        {
            sql.Append(" SELECT ");
            sql.Append(" M1.SYSTEM_ID, ");
            sql.Append(" M1.SEQ, ");
            sql.Append(" M1.TAIRYUU_KBN, ");
            sql.Append(" M1.KYOTEN_CD, ");
            sql.Append(" M1.UKEIRE_NUMBER AS DENPYOU_NUMBER, ");
            sql.Append(" M1.DATE_NUMBER, ");
            sql.Append(" M1.YEAR_NUMBER, ");
            sql.Append(" M1.KAKUTEI_KBN, ");
            sql.Append(" M1.DENPYOU_DATE, ");
            sql.Append(" M1.URIAGE_DATE, ");
            sql.Append(" M1.SHIHARAI_DATE, ");
            sql.Append(" M1.TORIHIKISAKI_CD, ");
            sql.Append(" M1.TORIHIKISAKI_NAME, ");
            sql.Append(" M1.GYOUSHA_CD, ");
            sql.Append(" M1.GYOUSHA_NAME, ");
            sql.Append(" M1.GENBA_CD, ");
            sql.Append(" M1.GENBA_NAME, ");
            sql.Append(" NULL AS NIZUMI_GYOUSHA_CD, ");
            sql.Append(" NULL AS NIZUMI_GYOUSHA_NAME, ");
            sql.Append(" NULL AS NIZUMI_GENBA_CD, ");
            sql.Append(" NULL AS NIZUMI_GENBA_NAME, ");
            sql.Append(" M1.NIOROSHI_GYOUSHA_CD, ");
            sql.Append(" M1.NIOROSHI_GYOUSHA_NAME, ");
            sql.Append(" M1.NIOROSHI_GENBA_CD, ");
            sql.Append(" M1.NIOROSHI_GENBA_NAME, ");
            sql.Append(" M1.EIGYOU_TANTOUSHA_CD, ");
            sql.Append(" M1.EIGYOU_TANTOUSHA_NAME, ");
            sql.Append(" M1.NYUURYOKU_TANTOUSHA_CD, ");
            sql.Append(" M1.NYUURYOKU_TANTOUSHA_NAME, ");
            sql.Append(" M1.SHARYOU_CD, ");
            sql.Append(" M1.SHARYOU_NAME, ");
            sql.Append(" M1.SHASHU_CD, ");
            sql.Append(" M1.SHASHU_NAME, ");
            sql.Append(" M1.UNPAN_GYOUSHA_CD, ");
            sql.Append(" M1.UNPAN_GYOUSHA_NAME, ");
            sql.Append(" M1.UNTENSHA_CD, ");
            sql.Append(" M1.UNTENSHA_NAME, ");
            sql.Append(" M1.NINZUU_CNT, ");
            sql.Append(" M1.KEITAI_KBN_CD, ");
            sql.Append(" M1.DAIKAN_KBN, ");
            sql.Append(" M1.CONTENA_SOUSA_CD, ");
            sql.Append(" M1.MANIFEST_SHURUI_CD, ");
            sql.Append(" M1.MANIFEST_TEHAI_CD, ");
            sql.Append(" M1.DENPYOU_BIKOU, ");
            sql.Append(" M1.TAIRYUU_BIKOU, ");
            sql.Append(" M1.UKETSUKE_NUMBER, ");
            sql.Append(" M1.KEIRYOU_NUMBER, ");
            sql.Append(" M1.RECEIPT_NUMBER, ");
            sql.Append(" M1.NET_TOTAL, ");
            sql.Append(" M1.URIAGE_SHOUHIZEI_RATE, ");
            sql.Append(" M1.URIAGE_KINGAKU_TOTAL, ");
            sql.Append(" M1.URIAGE_TAX_SOTO, ");
            sql.Append(" M1.URIAGE_TAX_UCHI, ");
            sql.Append(" M1.URIAGE_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.URIAGE_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.HINMEI_URIAGE_KINGAKU_TOTAL, ");
            sql.Append(" M1.HINMEI_URIAGE_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.HINMEI_URIAGE_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.SHIHARAI_SHOUHIZEI_RATE, ");
            sql.Append(" M1.SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append(" M1.SHIHARAI_TAX_SOTO, ");
            sql.Append(" M1.SHIHARAI_TAX_UCHI, ");
            sql.Append(" M1.SHIHARAI_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.SHIHARAI_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.HINMEI_SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append(" M1.HINMEI_SHIHARAI_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.HINMEI_SHIHARAI_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.URIAGE_ZEI_KEISAN_KBN_CD, ");
            sql.Append(" M1.URIAGE_ZEI_KBN_CD, ");
            sql.Append(" M1.URIAGE_TORIHIKI_KBN_CD, ");
            sql.Append(" M1.SHIHARAI_ZEI_KEISAN_KBN_CD, ");
            sql.Append(" M1.SHIHARAI_ZEI_KBN_CD, ");
            sql.Append(" M1.SHIHARAI_TORIHIKI_KBN_CD, ");
            sql.Append(" NULL AS KENSHU_DATE, ");
            sql.Append(" NULL AS SHUKKA_NET_TOTAL, ");
            sql.Append(" NULL AS KENSHU_NET_TOTAL, ");
            sql.Append(" NULL AS SABUN, ");
            sql.Append(" NULL AS SHUKKA_KINGAKU_TOTAL, ");
            sql.Append(" NULL AS KENSHU_KINGAKU_TOTAL, ");
            sql.Append(" NULL AS SAGAKU, ");
            sql.Append(" M1.CREATE_USER, ");
            sql.Append(" M1.CREATE_DATE, ");
            sql.Append(" M1.CREATE_PC, ");
            sql.Append(" M1.UPDATE_USER, ");
            sql.Append(" M1.UPDATE_DATE, ");
            sql.Append(" M1.UPDATE_PC, ");
            sql.Append(" M1.DELETE_FLG, ");
            sql.Append(" M1.TIME_STAMP, ");
            sql.AppendFormat(" '受入' AS {0} ", UIConstans.DENPYOU_SHURUI);
            sql.Append(" FROM T_UKEIRE_ENTRY M1 ");
            MakeSearchWhere(sql);
        }
        /// <summary>
        /// 出荷伝票検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchShukkaEntry(StringBuilder sql)
        {
            sql.Append(" SELECT ");
            sql.Append(" M1.SYSTEM_ID, ");
            sql.Append(" M1.SEQ, ");
            sql.Append(" M1.TAIRYUU_KBN, ");
            sql.Append(" M1.KYOTEN_CD, ");
            sql.Append(" M1.SHUKKA_NUMBER AS DENPYOU_NUMBER, ");
            sql.Append(" M1.DATE_NUMBER, ");
            sql.Append(" M1.YEAR_NUMBER, ");
            sql.Append(" M1.KAKUTEI_KBN, ");
            sql.Append(" M1.DENPYOU_DATE, ");
            sql.Append(" M1.URIAGE_DATE, ");
            sql.Append(" M1.SHIHARAI_DATE, ");
            sql.Append(" M1.TORIHIKISAKI_CD, ");
            sql.Append(" M1.TORIHIKISAKI_NAME, ");
            sql.Append(" M1.GYOUSHA_CD, ");
            sql.Append(" M1.GYOUSHA_NAME, ");
            sql.Append(" M1.GENBA_CD, ");
            sql.Append(" M1.GENBA_NAME, ");
            sql.Append(" M1.NIZUMI_GYOUSHA_CD, ");
            sql.Append(" M1.NIZUMI_GYOUSHA_NAME, ");
            sql.Append(" M1.NIZUMI_GENBA_CD, ");
            sql.Append(" M1.NIZUMI_GENBA_NAME, ");
            sql.Append(" NULL AS NIOROSHI_GYOUSHA_CD, ");
            sql.Append(" NULL AS NIOROSHI_GYOUSHA_NAME, ");
            sql.Append(" NULL AS NIOROSHI_GENBA_CD, ");
            sql.Append(" NULL AS NIOROSHI_GENBA_NAME, ");
            sql.Append(" M1.EIGYOU_TANTOUSHA_CD, ");
            sql.Append(" M1.EIGYOU_TANTOUSHA_NAME, ");
            sql.Append(" M1.NYUURYOKU_TANTOUSHA_CD, ");
            sql.Append(" M1.NYUURYOKU_TANTOUSHA_NAME, ");
            sql.Append(" M1.SHARYOU_CD, ");
            sql.Append(" M1.SHARYOU_NAME, ");
            sql.Append(" M1.SHASHU_CD, ");
            sql.Append(" M1.SHASHU_NAME, ");
            sql.Append(" M1.UNPAN_GYOUSHA_CD, ");
            sql.Append(" M1.UNPAN_GYOUSHA_NAME, ");
            sql.Append(" M1.UNTENSHA_CD, ");
            sql.Append(" M1.UNTENSHA_NAME, ");
            sql.Append(" M1.NINZUU_CNT, ");
            sql.Append(" M1.KEITAI_KBN_CD, ");
            sql.Append(" M1.DAIKAN_KBN, ");
            sql.Append(" M1.CONTENA_SOUSA_CD, ");
            sql.Append(" M1.MANIFEST_SHURUI_CD, ");
            sql.Append(" M1.MANIFEST_TEHAI_CD, ");
            sql.Append(" M1.DENPYOU_BIKOU, ");
            sql.Append(" M1.TAIRYUU_BIKOU, ");
            sql.Append(" M1.UKETSUKE_NUMBER, ");
            sql.Append(" M1.KEIRYOU_NUMBER, ");
            sql.Append(" M1.RECEIPT_NUMBER, ");
            sql.Append(" M1.NET_TOTAL, ");
            sql.Append(" M1.URIAGE_SHOUHIZEI_RATE, ");
            sql.Append(" M1.URIAGE_AMOUNT_TOTAL AS URIAGE_KINGAKU_TOTAL, ");
            sql.Append(" M1.URIAGE_TAX_SOTO, ");
            sql.Append(" M1.URIAGE_TAX_UCHI, ");
            sql.Append(" M1.URIAGE_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.URIAGE_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.HINMEI_URIAGE_KINGAKU_TOTAL, ");
            sql.Append(" M1.HINMEI_URIAGE_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.HINMEI_URIAGE_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.SHIHARAI_SHOUHIZEI_RATE, ");
            sql.Append(" M1.SHIHARAI_AMOUNT_TOTAL AS SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append(" M1.SHIHARAI_TAX_SOTO, ");
            sql.Append(" M1.SHIHARAI_TAX_UCHI, ");
            sql.Append(" M1.SHIHARAI_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.SHIHARAI_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.HINMEI_SHIHARAI_KINGAKU_TOTAL, ");
            sql.Append(" M1.HINMEI_SHIHARAI_TAX_SOTO_TOTAL, ");
            sql.Append(" M1.HINMEI_SHIHARAI_TAX_UCHI_TOTAL, ");
            sql.Append(" M1.URIAGE_ZEI_KEISAN_KBN_CD, ");
            sql.Append(" M1.URIAGE_ZEI_KBN_CD, ");
            sql.Append(" M1.URIAGE_TORIHIKI_KBN_CD, ");
            sql.Append(" M1.SHIHARAI_ZEI_KEISAN_KBN_CD, ");
            sql.Append(" M1.SHIHARAI_ZEI_KBN_CD, ");
            sql.Append(" M1.SHIHARAI_TORIHIKI_KBN_CD, ");
            sql.Append(" M1.KENSHU_DATE, ");
            sql.Append(" M1.SHUKKA_NET_TOTAL, ");
            sql.Append(" M1.KENSHU_NET_TOTAL, ");
            sql.Append(" M1.SABUN, ");
            sql.Append(" M1.SHUKKA_KINGAKU_TOTAL, ");
            sql.Append(" M1.KENSHU_KINGAKU_TOTAL, ");
            sql.Append(" M1.SAGAKU, ");
            sql.Append(" M1.CREATE_USER, ");
            sql.Append(" M1.CREATE_DATE, ");
            sql.Append(" M1.CREATE_PC, ");
            sql.Append(" M1.UPDATE_USER, ");
            sql.Append(" M1.UPDATE_DATE, ");
            sql.Append(" M1.UPDATE_PC, ");
            sql.Append(" M1.DELETE_FLG, ");
            sql.Append(" M1.TIME_STAMP, ");
            sql.AppendFormat(" '出荷' AS {0} ", UIConstans.DENPYOU_SHURUI);
            sql.Append(" FROM T_SHUKKA_ENTRY M1 ");
            MakeSearchWhere(sql);
        }

        /// <summary>
        /// 受入明細検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchUkeireDetail(StringBuilder sql)
        {
            sql.Append(" SELECT ");
            sql.Append(" M2.SYSTEM_ID, ");
            sql.Append(" M2.SEQ, ");
            sql.Append(" M2.UKEIRE_NUMBER AS DENPYOU_NUMBER, ");
            sql.Append(" M2.DETAIL_SYSTEM_ID, ");
            sql.Append(" M2.ROW_NO, ");
            sql.Append(" M2.KAKUTEI_KBN, ");
            sql.Append(" M2.URIAGESHIHARAI_DATE, ");
            sql.Append(" M2.STACK_JYUURYOU, ");
            sql.Append(" M2.EMPTY_JYUURYOU, ");
            sql.Append(" M2.WARIFURI_JYUURYOU, ");
            sql.Append(" M2.WARIFURI_PERCENT, ");
            sql.Append(" M2.CHOUSEI_JYUURYOU, ");
            sql.Append(" M2.CHOUSEI_PERCENT, ");
            sql.Append(" M2.YOUKI_CD, ");
            sql.Append(" M2.YOUKI_SUURYOU, ");
            sql.Append(" M2.YOUKI_JYUURYOU, ");
            sql.Append(" M2.DENPYOU_KBN_CD, ");
            sql.Append(" M2.HINMEI_CD, ");
            sql.Append(" M2.HINMEI_NAME, ");
            sql.Append(" M2.NET_JYUURYOU, ");
            sql.Append(" M2.SUURYOU, ");
            sql.Append(" M2.UNIT_CD, ");
            sql.Append(" M2.TANKA, ");
            sql.Append(" M2.KINGAKU, ");
            sql.Append(" M2.TAX_SOTO, ");
            sql.Append(" M2.TAX_UCHI, ");
            sql.Append(" M2.HINMEI_ZEI_KBN_CD, ");
            sql.Append(" M2.HINMEI_KINGAKU, ");
            sql.Append(" M2.HINMEI_TAX_SOTO, ");
            sql.Append(" M2.HINMEI_TAX_UCHI, ");
            sql.Append(" M2.MEISAI_BIKOU, ");
            sql.Append(" M2.NISUGATA_SUURYOU, ");
            sql.Append(" M2.NISUGATA_UNIT_CD, ");
            sql.Append(" M2.TIME_STAMP, ");
            sql.AppendFormat(" '受入' AS {0} ", UIConstans.DENPYOU_SHURUI);
            sql.Append(" FROM T_UKEIRE_DETAIL M2 ");
        }
        /// <summary>
        /// 出荷明細検索
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchShukkaDetail(StringBuilder sql)
        {
            sql.Append(" SELECT ");
            sql.Append(" M2.SYSTEM_ID, ");
            sql.Append(" M2.SEQ, ");
            sql.Append(" M2.SHUKKA_NUMBER AS DENPYOU_NUMBER, ");
            sql.Append(" M2.DETAIL_SYSTEM_ID, ");
            sql.Append(" M2.ROW_NO, ");
            sql.Append(" M2.KAKUTEI_KBN, ");
            sql.Append(" M2.URIAGESHIHARAI_DATE, ");
            sql.Append(" M2.STACK_JYUURYOU, ");
            sql.Append(" M2.EMPTY_JYUURYOU, ");
            sql.Append(" M2.WARIFURI_JYUURYOU, ");
            sql.Append(" M2.WARIFURI_PERCENT, ");
            sql.Append(" M2.CHOUSEI_JYUURYOU, ");
            sql.Append(" M2.CHOUSEI_PERCENT, ");
            sql.Append(" M2.YOUKI_CD, ");
            sql.Append(" M2.YOUKI_SUURYOU, ");
            sql.Append(" M2.YOUKI_JYUURYOU, ");
            sql.Append(" M2.DENPYOU_KBN_CD, ");
            sql.Append(" M2.HINMEI_CD, ");
            sql.Append(" M2.HINMEI_NAME, ");
            sql.Append(" M2.NET_JYUURYOU, ");
            sql.Append(" M2.SUURYOU, ");
            sql.Append(" M2.UNIT_CD, ");
            sql.Append(" M2.TANKA, ");
            sql.Append(" M2.KINGAKU, ");
            sql.Append(" M2.TAX_SOTO, ");
            sql.Append(" M2.TAX_UCHI, ");
            sql.Append(" M2.HINMEI_ZEI_KBN_CD, ");
            sql.Append(" M2.HINMEI_KINGAKU, ");
            sql.Append(" M2.HINMEI_TAX_SOTO, ");
            sql.Append(" M2.HINMEI_TAX_UCHI, ");
            sql.Append(" M2.MEISAI_BIKOU, ");
            sql.Append(" M2.NISUGATA_SUURYOU, ");
            sql.Append(" M2.NISUGATA_UNIT_CD, ");
            sql.Append(" M2.TIME_STAMP, ");
            sql.AppendFormat(" '出荷' AS {0} ", UIConstans.DENPYOU_SHURUI);
            sql.Append(" FROM T_SHUKKA_DETAIL M2 ");
        }

        /// <summary>
        /// Where句
        /// <param name="sql">sql</param>
        /// </summary>
        private void MakeSearchWhere(StringBuilder sql)
        {
            //SQL文格納StringBuilder
            sql.Append(" WHERE ");
            sql.Append(" M1.DELETE_FLG = 0 ");
            sql.Append(" AND M1.TAIRYUU_KBN = 1 ");

            //画面で拠点CDが入力されている場合
            if (!string.IsNullOrEmpty(this.headForm.txtKyotenCD.Text) && this.headForm.txtKyotenCD.Text != "99")
            {
                sql.Append(" AND M1.KYOTEN_CD = " + int.Parse(this.headForm.txtKyotenCD.Text) + " ");
            }

            //画面で車輌番号が入力されている場合
            if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                sql.Append(" AND M1.SHARYOU_CD = '" + this.form.SHARYOU_CD.Text + "' ");
            }
            else if (!string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                string sharyouNumber = SqlCreateUtility.CounterplanEscapeSequence2(this.form.SHARYOU_CD.Text);
                sql.Append(" AND M1.SHARYOU_CD = '" + sharyouNumber + "' ");
            }

            // 画面で運搬業者番号が入力されている場合
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                sql.Append(" AND M1.UNPAN_GYOUSHA_CD = '" + this.form.UNPAN_GYOUSHA_CD.Text + "' ");
            }
        }
        #endregion 検索処理関連

        #endregion メソッド

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

        //public int Search()
        //{
        //    throw new NotImplementedException();
        //}

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        // No.2002
        /// <summary>
        /// Windowクローズ処理。
        /// </summary>
        internal void SetPrevStatus(object sender, EventArgs e)
        {
            //拠点CD
            if (this.headForm.txtKyotenCD.Text != "")
            {
                Properties.Settings.Default.SET_KYOTEN_CD = this.headForm.txtKyotenCD.Text;
            }
            else
            {
                Properties.Settings.Default.SET_KYOTEN_CD = null;
            }
            //車輌CD
            Properties.Settings.Default.SET_SHARYOU_CD = this.form.SHARYOU_CD.Text;
            // 運搬業者
            Properties.Settings.Default.SET_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            //伝票種類CD
            Properties.Settings.Default.SET_DENPYOU_KIND = this.form.txtDenpyouKind.Text;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 画面表示
        /// </summary>
        /// <param name="wintype">ウィンドウタイプ</param>
        /// <param name="denpyuouNum">伝票番号</param>
        private void EditDetail(WINDOW_TYPE wintype, string denpyouNum)
        {
            long denpyou = -1;
            string strFormId = "";
            string DenpyouNum = "";


            if (!string.IsNullOrEmpty(denpyouNum))
            {
                denpyou = long.Parse(denpyouNum);
            }

            // 選択行の伝票種類を取得
            DataGridViewCell datagridviewcell = this.form.customDataGridView1.CurrentCell;
            if (datagridviewcell != null)
            {
                DenpyouNum = this.form.customDataGridView1.Rows[datagridviewcell.RowIndex].Cells[UIConstans.DENPYOU_SHURUI].Value.ToString();
            }

            UIConstans.targetTable searchTarget = 0;

            // 選択行の伝票種類が「受入」の場合
            if (DenpyouNum.Equals(DENPYOU_SYURUI_UKEIRE))
            {
                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    // 既存受入入力へ遷移
                    strFormId = UKEIRE_NYUURYOKU;
                }
                else
                {
                    // 新受入入力へ遷移
                    strFormId = BIG_UKEIRE_NYUURYOKU;
                }
                searchTarget = UIConstans.targetTable.UKEIRE;
            }
            // 選択行の伝票種類が「出荷」の場合
            else if (DenpyouNum.Equals(DENPYOU_SYURUI_SYUKKA))
            {
                if (this.sysInfoEntity.UKEIRESHUKA_GAMEN_SIZE == 2)
                {
                    // 既存出荷入力へ遷移
                    strFormId = SYUKKA_NYUURYOKU;
                }
                else
                {
                    // 新出荷入力へ遷移
                    strFormId = BIG_SYUKKA_NYUURYOKU;
                }
                searchTarget = UIConstans.targetTable.SHUKKA;
            }

            // データチェック
            var ret = tairyu_dao.GetEntryExists((int)searchTarget, denpyou.ToString());
            if (ret.Rows.Count == 0)
            {
                // データがない場合はエラー
                msgcls.MessageBoxShow("E045");
                return;
            }
            else if (!(bool)ret.Rows[0]["TAIRYUU_KBN"])
            {
                msgcls.MessageBoxShow("E272");
                return;
            }

            switch (wintype)
            {
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    //新規モードで起動
                    FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG);
                    break;
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    //修正モードで起動
                    FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.UPDATE_WINDOW_FLAG, WINDOW_TYPE.UPDATE_WINDOW_FLAG, denpyou);
                    break;
                case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                    //削除モードで起動
                    FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.DELETE_WINDOW_FLAG, WINDOW_TYPE.DELETE_WINDOW_FLAG, denpyou);
                    break;
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    //複写モードで起動（新規モード）
                    FormManager.OpenFormWithAuth(strFormId, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_TYPE.NEW_WINDOW_FLAG, denpyou);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 運搬業者CDバリデート
        /// </summary>
        public void UNPAN_GYOUSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = "";

                M_GYOUSHA entity = new M_GYOUSHA();
                entity.ISNOT_NEED_DELETE_FLG = true;
                var unpanGyousha = this.m_gyoushadao.GetAllValidData(entity).FirstOrDefault(s => s.GYOUSHA_CD == this.form.UNPAN_GYOUSHA_CD.Text);
                if (unpanGyousha == null || !unpanGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // エラーメッセージ
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    return;
                }

                // 名称セット
                this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CDValidated", ex);
                if (ex is SQLRuntimeException)
                {
                    msgcls.MessageBoxShow("E093", "");
                }
                else
                {
                    msgcls.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 車輌CDEnter処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void sharyouCdEnter(object sender, EventArgs e)
        {
            try
            {
                // 前回値を保持する
                var ctrl = (CustomAlphaNumTextBox)sender;
                //this.oldSharyouCD = ctrl.Text;
            }
            catch (Exception ex)
            {
                LogUtility.Error("sharyouCdEnter", ex);
                msgcls.MessageBoxShow("E245", "");
            }
        }

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChechSharyouCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 車輌名をクリア
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }

                // 車輌情報取得
                var sharyou = this.GetSharyou(this.form.SHARYOU_CD.Text);
                if (sharyou == null)
                {
                    // メッセージ表示
                    msgcls.MessageBoxShow("E020", "車輌");
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                // 車輌名設定
                this.form.SHARYOU_NAME_RYAKU.Text = sharyou.SHARYOU_NAME_RYAKU;

                // 運搬業者が入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 業者情報取得
                    var gyousha = this.GetGyousha(sharyou.GYOUSHA_CD);
                    if (gyousha != null)
                    {
                        // 業者情報設定
                        this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 処理終了
                returnVal = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechSharyouCd", ex);
                if (ex is SQLRuntimeException)
                {
                    msgcls.MessageBoxShow("E093", "");
                }
                else
                {
                    msgcls.MessageBoxShow("E245", "");
                }
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// 車輌情報取得
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        public M_SHARYOU GetSharyou(string sharyouCd)
        {
            M_SHARYOU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(sharyouCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHARYOU keyEntity = new M_SHARYOU();
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                keyEntity.SHARYOU_CD = sharyouCd;
                keyEntity.ISNOT_NEED_DELETE_FLG = true;
                // [運搬業者CD,車輌CD]でM_SHARYOUを検索する
                var returnEntitys = this.m_sharyoudao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    if (returnEntitys.Length == 1)
                    {
                        // 1件
                        returnVal = returnEntitys[0];
                    }
                    else
                    {
                        // ヒット数が複数件の場合、ポップアップ表示
                        this.form.SHARYOU_CD.Focus();
                        SendKeys.Send(" ");

                        // 返却値は空白をセット
                        returnVal = new M_SHARYOU();
                        returnVal.SHARYOU_NAME_RYAKU = "";
                        returnVal.SHASYU_CD = "";
                        returnVal.SHAIN_CD = "";
                        returnVal.GYOUSHA_CD = "";
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharyou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            M_GYOUSHA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.m_gyoushadao.GetAllValidData(keyEntity);

                if (gyousha != null && gyousha.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = gyousha[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion
    }
}
