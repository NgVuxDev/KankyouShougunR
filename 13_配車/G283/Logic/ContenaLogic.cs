using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;
using System.Data;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlTypes;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Allocation.MobileShougunTorikomi.APP;
using Shougun.Core.Allocation.MobileShougunTorikomi.DAO;
using Shougun.Core.Allocation.MobileShougunTorikomi.DTO;

namespace Shougun.Core.Allocation.MobileShougunTorikomi.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class ContenaLogic : IBuisinessLogic
    {
        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// システムID＆明細システムID採番用のアクセサー
        /// </summary>
        private DBAccessor CommonDBAccessor;

        /// <summary>
        /// DAO
        /// </summary>
        private MobileShougunTorikomiDAOClass dao;

        /// <summary>
        /// SetMobileSyogunDataInsertDao
        /// </summary>
        private SetMobileSyogunDataInsertDao setMobileSyogunDataInsertDao;

        /// <summary>
        /// SetTeikiJissekiEntryDao
        /// </summary>
        private SetTeikiJissekiEntryDao setTeikiJissekiEntryDao;

        /// <summary>
        /// SetTeikiJissekiDetailDao
        /// </summary>
        private SetTeikiJissekiDetailDao setTeikiJissekiDetailDao;

        /// <summary>
        /// SetTeikiJissekiNioroshiDao
        /// </summary>
        private SetTeikiJissekiNioroshiDao setTeikiJissekiNioroshiDao;

        /// <summary>
        /// SetUrShEntryDao
        /// </summary>
        private SetUrShEntryDao setUrShEntryDao;

        /// <summary>
        /// SetUrShDetailDao
        /// </summary>
        private SetUrShDetailDao setUrShDetailDao;

        /// <summary>
        /// 定期配車入力データ
        /// </summary>
        private DataTable teikiHaishaEntryResult { get; set; }

        /// <summary>
        /// コース名称マスタデータ
        /// </summary>
        private DataTable courseNameResult { get; set; }

        /// <summary>
        /// 受付(収集)入力データ
        /// </summary>
        private DataTable uketsukeSsEntryResult { get; set; }

        /// <summary>
        /// 受付(収集)明細データ
        /// </summary>
        private DataTable uketsukeSsDetailResult { get; set; }

        /// <summary>
        /// コース_明細内訳データ
        /// </summary>
        private DataTable courseDetailItemsResult { get; set; }

        /// <summary>
        /// 取引先_請求情報マスタデータ
        /// </summary>
        private DataTable mtorihikisakiSeikyuuResult { get; set; }

        /// <summary>
        /// コンテナ種類マスタデータ
        /// </summary>
        private DataTable contenaShuruiResult { get; set; }

        /// <summary>
        /// コンテナマスタデータ
        /// </summary>
        private DataTable contenaResult { get; set; }

        /// <summary>
        /// コンテナ稼動予定テーブルデータ
        /// </summary>
        private DataTable contenaReserveResult { get; set; }

        /// <summary>
        /// 親データ取得用
        /// </summary>
        private DataTable dataResult;
        private DataGridViewRow dr;

        /// <summary>
        /// 現在表示中の一覧
        /// </summary>
        public DataRow[] selectedRowsSettiIchiran;
        public DataRow[] selectedRowsHikiageIchiran;

        /// <summary>
        /// 検索結果件数（有効データ(DELETE_FLG=false)）
        /// </summary>
        public int YuukouData_count;

        /// <summary>
        /// 検索結果件数（定期データ）
        /// </summary>
        public int teikiData_count;

        /// <summary>
        /// 検索結果件数（スポットデータ）
        /// </summary>
        public int spotData_count;

        /// <summary>
        /// シーケンシャルナンバーのMAX値
        /// </summary>
        public Int64 Max_Seq_No;

        /// <summary>
        /// 枝番のMAX値
        /// </summary>
        public Int64 Max_Edaban;

        /// <summary>
        /// ノード枝番
        /// </summary>
        public Int64 NODE_EDABAN_HAISHA = 1;            // 配車ヘッダレコード
        public Int64 NODE_EDABAN_SHUKKO = 2;            // 出庫実績レコード
        public Int64 NODE_EDABAN_KIKO = 3;              // 帰庫実績レコード
        public Int64 NODE_EDABAN_GENBAJISSEKI = 4;      // 現場実績レコード
        public Int64 NODE_EDABAN_DETAIL = 5;            // 現場明細レコード
        public Int64 NODE_EDABAN_HANNYUUJISSEKI = 6;    // 搬入実績レコード

        /// <summary>
        /// コンテナ種類マスタ取得後の保存領域
        /// </summary>
        private string strContenaShuruiName;            // コンテナ種類名

        /// <summary>
        /// コンテナマスタ取得後の保存領域
        /// </summary>
        private string strContenaName;                  // コンテナ名

        /// <summary>
        /// コンテナ稼動予定テーブル取得後の保存領域
        /// </summary>
        private int intDaisuuCnt;                       // 台数

        /// <summary>
        /// 設置引揚区分
        /// </summary>
        public Int16 Contena_Set_Kbn_Setti = 1;         // 設置
        public Int16 Contena_Set_Kbn_Hikiage = 2;       // 引揚
        
        /// <summary>
        /// 受付(収集)入力テーブル取得後の保存領域
        /// </summary>
        private Int16 int16KyotenCd;                // 拠点CD
        private string strShashuCd;                 // 車種CD
        private string strShashuName;               // 車種名
        private string strSharyouCd;                // 車輌CD
        private string strSharyouName;              // 車輌名
        private string strUntenshaName;             // 運転者名
        private string strHojoinCd;                 // 補助員CD
        private string strTorihikisakiCd;           // 取引先CD
        private string strTorihikisakiName;         // 取引先名
        private string strGyoushaName;              // 業者名
        private string strGenbaName;                // 現場名
        private string strNioroshiGyoushaCd;        // 荷卸業者CD
        private string strNioroshiGyoushaName;      // 荷卸業者名
        private string strNioroshiGenbaCd;          // 荷卸現場CD
        private string strNioroshiGenbaName;        // 荷卸現場名
        private string strEigyouTantoushaCd;        // 営業担当者CD
        private string strEigyouTantoushaName;      // 営業担当者名
        private string strUnpanGyoushaCd;           // 運搬業者CD
        private string strUnpanGyoushaName;         // 運搬業者名
        private Int16 int16ContenaSousaCd;          // コンテナ操作CD
        private Int16 int16ManifestShuruiCd;        // マニフェスト種類CD
        private Int16 int16ManifestTehaiCd;         // マニフェスト手配CD
        private Decimal decShouhizeiRate;           // 売上消費税率
        private Decimal moneyKingakuTotal;          // 売上金額合計
        private Decimal moneyTaxSoto;               // 売上伝票毎消費税外税
        private Decimal moneyTaxUchi;               // 売上伝票毎消費税内税
        private Decimal moneyTaxSotoToal;           // 売上明細毎消費税外税合計
        private Decimal moneyTaxUchiToal;           // 売上明細毎消費税内税合計

        /// <summary>
        /// 受付(収集)明細テーブル取得後の保存領域
        /// </summary>
        private Int16 int16DenpyouKbnCd;            // 伝票区分CD
        private string strHinmeiCd;                 // 品名CD
        private string strHinmeiName;               // 品名
        private Double dblSuuryou;                   // 数量
        private Int16 int16UnitCd;                  // 単位CD
        private Decimal moneyTanka;                 // 単価
        private Decimal moneyKingaku;               // 金額
        private Decimal moneyTaxSotoDetail;         // 消費税外税
        private Decimal moneyTaxUchiDetail;         // 消費税内税
        private Int16 int16HinmeiZeiKbnCd;          // 品名別税区分CD
        private Decimal moneyHinmeiKingaku;         // 品名別金額
        private Decimal moneyHinmeiTaxSoto;         // 品名別消費税外税
        private Decimal moneyHinmeiTaxUchi;         // 品名別消費税内税
        private string strMeisaiBikou;              // 明細備考

        /// <summary>
        /// 取引先_請求情報マスタ取得後の保存領域
        /// </summary>
        private Int16 int16ZeiKeisanKbnCd;          // 税計算区分CD
        private Int16 int16ZeiKbnCd;                // 税区分CD
        private Int16 int16TorihikiKbnCd;           // 取引区分CD

        /// <summary>
        /// ダミー情報の保存領域
        /// </summary>
        private Int16 int16Kakutei_Kbn = 2;         // 確定区分

        /// <summary>
        /// xmlファイル名
        /// </summary>
        private string fn;

        /// <summary>
        /// 月極区分
        /// </summary>
        private Int16 int16teikiTsukiKbn;

        /// <summary>
        /// 伝票日付
        /// </summary>
        private DateTime dtDenpyouDate;

        /// <summary>
        /// システムID
        /// </summary>
        private SqlInt64 int64SystemId;

        /// <summary>
        /// 定期実績番号
        /// </summary>
        private SqlInt64 int64TeikiJissekiNumber;

        /// <summary>
        /// 売上／支払番号
        /// </summary>
        private SqlInt64 int64UrShNumber;

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private static readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.MobileShougunTorikomi.Setting.ButtonSetting.xml";

        /// <summary>
        /// DTO
        /// </summary>
        private MobileShougunTorikomiDTOClass dto;

        /// <summary>
        /// Form
        /// </summary>
        private ContenaForm form;

        private MessageBoxShowLogic MsgBox = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ContenaLogic(ContenaForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            //this.dto = new MobileShougunTorikomiDTOClass();
            this.dao = DaoInitUtility.GetComponent<MobileShougunTorikomiDAOClass>();
            this.CommonDBAccessor = new DBAccessor();

            LogUtility.DebugMethodEnd();
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

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowInit(DataTable oyaDataResult, DataGridViewRow oyaDr)
        {
            try
            {
                // 親情報の格納
                this.dataResult = oyaDataResult;
                this.dr = oyaDr;

                //// 取り込み済みデータ取得処理
                //this.getTorikomizumiData("YUUKOU");

                //// 画面初期表示設定
                //this.initializeScreen();

                //// ボタンのテキストを初期化
                //this.ButtonInit();

                //// イベントの初期化処理
                //this.EventInit();

                // 設置一覧の表示
                this.SetIchiran("SETTI");

                // 引揚一覧の表示
                this.SetIchiran("HIKIAGE");

                //// 未登録数の表示
                //this.SetMitourokusu("TEIKI");
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 画面情報の初期化を行う
        /// </summary>
        internal void WindowFinal()
        {
            //ContenaForm frm = new ContenaForm(this.dataResult, dr);
            //frm.Close;
            this.form.Close();
        }

    //    /// <summary>
    //    /// 画面情報の再表示を行う
    //    /// </summary>
    //    internal void ReWindow()
    //    {
    //        // 取り込み済みデータ取得処理
    //        this.getTorikomizumiData("YUUKOU");

    //        // 画面初期表示設定
    //        this.initializeScreen();

    //        // 一覧の表示
    //        this.SetIchiran("TEIKI");

    //        // 未登録数の表示
    //        this.SetMitourokusu("TEIKI");

    //    }

    //    /// <summary>
    //    /// 画面初期表示設定
    //    /// </summary>
    //    private void initializeScreen()
    //    {
    //        //「作業開始日」
    //        this.form.dtp_SagyouDateFrom.Value = null;
    //        this.form.dtp_SagyouDateFrom.Text = string.Empty;

    //        //「作業終了日」
    //        this.form.dtp_SagyouDateTo.Value = null;
    //        this.form.dtp_SagyouDateTo.Text = string.Empty;

    //        // 「拠点CD」
    //        this.form.ntxt_KyotenCd.Text = "";

    //        // 「拠点名」
    //        this.form.txt_KyotenName.Text = "";
    //        this.form.txt_KyotenName.Enabled = false;

    //        // 「車種CD」
    //        this.form.ntxt_ShashuCd.Text = "";

    //        // 「車種名」
    //        this.form.txt_ShashuName.Text = "";
    //        this.form.txt_ShashuName.Enabled = false;

    //        // 「車輌CD」
    //        this.form.ntxt_SharyouCd.Text = "";

    //        // 「車輌名」
    //        this.form.txt_SharyouName.Text = "";
    //        this.form.txt_SharyouName.Enabled = false;

    //        // 「運転者CD」
    //        this.form.ntxt_UntenshaCd.Text = "";

    //        // 「運転者名」
    //        this.form.txt_UntenshaName.Text = "";
    //        this.form.txt_UntenshaName.Enabled = false;

    //        // 「コースCD」
    //        this.form.ntxt_CourseCd.Text = "";

    //        // 「コース名」
    //        this.form.txt_CourseName.Text = "";
    //        this.form.txt_CourseName.Enabled = false;

    //        // 表示・非表示判定
    //        setVisible("TEIKI");

    //    }

    //    /// <summary>
    //    /// 表示・非表示判定
    //    /// </summary>
    //    public void setVisible(string haishaKbun)
    //    {
    //        // TODO
    //        // ※ntxt_CourseCdのvisileをfalseにすると落ちる（「コースラベル」「コース名」は問題なし）

    //        // 定期データ表示の場合　　　→　検索条件の「コースラベル」「コースCD」「コース名」を表示する
    //        // スポットデータ表示の場合　→　検索条件の「コースラベル」「コースCD」「コース名」を非表示にする
    //        if (haishaKbun == "TEIKI")
    //        { // 定期の場合

    //            // 「コースラベル」
    //            this.form.lbl_Course.Visible = true;

    //            // 「コースCD」
    //            this.form.ntxt_CourseCd.Visible = true;

    //            // 「コース名」
    //            this.form.txt_CourseName.Visible = true;

    //        }
    //        else
    //        { // スポットの場合

    //            // 「コースラベル」
    //            this.form.lbl_Course.Visible = false;

    //            // 「コースCD」
    //            this.form.ntxt_CourseCd.Visible = false;

    //            // 「コース名」
    //            this.form.txt_CourseName.Visible = false;

    //        }
    //    }

    //    /// <summary>
    //    /// ボタンの初期化処理
    //    /// </summary>
    //    public void ButtonInit()
    //    {
    //        var buttonSetting = this.CreateButtonInfo();
    //        var parentForm = (BusinessBaseForm)this.form.Parent;
    //        ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
    //    }

    //    /// <summary>
    //    /// ボタン情報の設定を行う
    //    /// </summary>
    //    private ButtonSetting[] CreateButtonInfo()
    //    {
    //        var buttonSetting = new ButtonSetting();

    //        var thisAssembly = Assembly.GetExecutingAssembly();
    //        return buttonSetting.LoadButtonSetting(thisAssembly, ButtonInfoXmlPath);
    //    }

    //    /// <summary>
    //    /// イベント処理の初期化を行う
    //    /// </summary>
    //    private void EventInit()
    //    {
    //        var parentForm = (BusinessBaseForm)this.form.Parent;

    //        // 「Functionボタン」のイベント生成
    //        parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);
    //        parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);
    //        parentForm.bt_func7.Click += new EventHandler(bt_func7_Click);
    //        parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);
    //        parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);
    //        parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);
    //        parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);

    //    }

    //    /// <summary>
    //    /// 「Ｆ１ ﾃﾞｰﾀ取込ボタン」イベント
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void bt_func1_Click(object sender, EventArgs e)
    //    {

    //        //OpenFileDialogクラスのインスタンスを作成
    //        OpenFileDialog ofd = new OpenFileDialog();

    //        //ダイアログに表示されるフォルダを指定する
    //        ofd.InitialDirectory = GetInPutPath("mobileInPutPath");

    //        //複数のファイルを選択できるようにする
    //        ofd.Multiselect = true;

    //        //[ファイルの種類]に表示される選択肢を指定する
    //        ofd.Filter = "xmlファイル|*.xml";

    //        //タイトルを設定する
    //        ofd.Title = "開くファイルを選択してください";

    //        //ダイアログを表示する
    //        if (ofd.ShowDialog() == DialogResult.OK)
    //        {
    //            // xmlファイル数分の処理をする
    //            for (int i = 0; i < ofd.FileNames.Length; ++i)
    //            {
    //                fn = ofd.FileNames[i];
    //                if (File.Exists(fn))
    //                {
    //                    XmlDocument xmldoc = new XmlDocument();

    //                    // XMLファイルを読み込む
    //                    try
    //                    {
    //                        xmldoc.Load(fn);
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        // TODO タグが対になっていない時のエラーメッセージの定義
    //                        MessageBox.Show(ex.Message);
    //                    }

    //                    // ノード指定
    //                    XmlNodeList haishaNodes = xmldoc.SelectNodes("denpyoJisseki/haisha");                   // 配車ヘッダレコード
    //                    XmlNodeList shukkoNodes = xmldoc.SelectNodes("denpyoJisseki/shukkokiko/shukko");        // 出庫実績レコード
    //                    XmlNodeList kikoNodes = xmldoc.SelectNodes("denpyoJisseki/shukkokiko/kiko");            // 帰庫実績レコード
    //                    XmlNodeList genbaJissekiNodes = xmldoc.SelectNodes("denpyoJisseki/genbaJisseki");       // 現場実績レコード
    //                    XmlNodeList detailNodes = xmldoc.SelectNodes("denpyoJisseki/genbaJisseki/detail");      // 現場明細レコード
    //                    XmlNodeList hannyuuJissekiNodes = xmldoc.SelectNodes("denpyoJisseki/hannyuuJisseki");   // 搬入実績レコード

    //                    // 枝番のMAX値を取得する　※同一枝番が同一xmlファイルのノード群となる
    //                    getTorikomizumiData("MAX_EDABAN");
    //                    this.Max_Edaban = this.Max_Edaban + 1;

    //                    // モバイル将軍用データ取込画面専用テーブル登録処理
    //                    setMobileSyogunData(haishaNodes, "haisha");                       // 配車ヘッダレコード
    //                    setMobileSyogunData(shukkoNodes, "shukko");                       // 出庫実績レコード
    //                    setMobileSyogunData(kikoNodes, "kiko");                           // 帰庫実績レコード
    //                    setMobileSyogunData(genbaJissekiNodes, "genbaJisseki");           // 現場実績レコード
    //                    setMobileSyogunData(detailNodes, "detail");                       // 現場明細レコード
    //                    setMobileSyogunData(hannyuuJissekiNodes, "hannyuuJisseki");       // 搬入実績レコード
    //                }
    //            }

    //            // 取込完了メッセージの表示
    //            MessageBox.Show(MobileShougunTorikomiConst.Msg1);

    //            // 画面の再表示
    //            ReWindow();
    //        }
    //    }

    //    /// <summary>
    //    /// モバイル将軍用データ取込画面専用テーブル登録処理
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void setMobileSyogunData(XmlNodeList Nodes, string kbn)
    //    {
    //        // DAO編集
    //        this.setMobileSyogunDataInsertDao = DaoInitUtility.GetComponent<SetMobileSyogunDataInsertDao>();

    //        // モバイル将軍用データ取込画面専用テーブルエンティティの定義
    //        T_MOBILE_SYOGUN_DATA_INSERT mobileSyogunDataInsertEntity;

    //        // 子ノードの編集
    //        switch (kbn)
    //        {
    //            // 配車ヘッダレコード   
    //            case "haisha":
    //                foreach (XmlNode childNode in Nodes)
    //                {
    //                    // モバイル将軍用データ取込画面専用テーブルエンティティの初期化
    //                    mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

    //                    // 枝番のMAX値を取得する　※同一枝番が同一xmlファイルのノード群となる
    //                    getTorikomizumiData("MAX_EDABAN");
    //                    mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;

    //                    // ノード枝番の編集
    //                    mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_HAISHA;

    //                    // 伝票番号より、拠点CD・車種CD・車種名を取得して編集する（伝票番号も編集）
    //                    if (childNode.SelectSingleNode("no").InnerText != "")
    //                    { //  配車ヘッダレコードの伝票番号が存在する場合　※タグ有り、値が空で取り込む場合もあるための処理

    //                        // 伝票番号
    //                        mobileSyogunDataInsertEntity.HAISHA_DENPYOU_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);

    //                        if (getUketsukeSsEntry(int.Parse(childNode.SelectSingleNode("no").InnerText)) != 0)
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルのデータが存在する場合
    //                            mobileSyogunDataInsertEntity.KYOTEN_CD = this.int16KyotenCd;
    //                            mobileSyogunDataInsertEntity.SHASHU_CD = this.strShashuCd;
    //                            mobileSyogunDataInsertEntity.SHASHU_NAME = this.strShashuName;
    //                            mobileSyogunDataInsertEntity.SHARYOU_CD = this.strSharyouCd;
    //                            mobileSyogunDataInsertEntity.SHARYOU_NAME = this.strSharyouName;
    //                            mobileSyogunDataInsertEntity.UNTENSHA_NAME = this.strUntenshaName;
    //                        }
    //                    }

    //                    mobileSyogunDataInsertEntity.HAISHA_SAGYOU_DATE = Convert.ToDateTime(childNode.SelectSingleNode("sagyouDate").InnerText);
    //                    mobileSyogunDataInsertEntity.HAISHA_UNTENSHA_CD = childNode.SelectSingleNode("untenshaCd").InnerText;
    //                    mobileSyogunDataInsertEntity.HAISHA_COURSE_NAME_CD = childNode.SelectSingleNode("courseCd").InnerText;
    //                    mobileSyogunDataInsertEntity.HAISHA_KBN = Int16.Parse(childNode.SelectSingleNode("haishaKbn").InnerText);
    //                    mobileSyogunDataInsertEntity.HAISHA_TORIKOMI_DATE = DateTime.Now;
    //                    mobileSyogunDataInsertEntity.HAISHA_TORIKOMI_FILENAME = Path.GetFileName(fn);

    //                    // モバイル将軍用データ取込画面専用テーブル登録
    //                    setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
    //                }
    //                break;

    //            // 出庫実績レコード
    //            case "shukko":
    //                foreach (XmlNode childNode in Nodes)
    //                {
    //                    // モバイル将軍用データ取込画面専用テーブルエンティティの初期化
    //                    mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

    //                    // ノード枝番の編集
    //                    mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_SHUKKO;

    //                    mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
    //                    mobileSyogunDataInsertEntity.SHUKKO_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
    //                    mobileSyogunDataInsertEntity.SHUKKO_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
    //                    mobileSyogunDataInsertEntity.SHUKKO_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
    //                    mobileSyogunDataInsertEntity.SHUKKO_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
    //                    mobileSyogunDataInsertEntity.SHUKKO_SHUKKODATE = DateTime.Parse(childNode.SelectSingleNode("shukkoDate").InnerText);
    //                    mobileSyogunDataInsertEntity.SHUKKO_SHARYOUCD = childNode.SelectSingleNode("sharyouCd").InnerText;
    //                    mobileSyogunDataInsertEntity.SHUKKO_TENKI = childNode.SelectSingleNode("tenki").InnerText;

    //                    if (childNode.SelectSingleNode("gyoushaCd").InnerText != "")
    //                    { // 出庫実績レコードの業者CDが存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
    //                        mobileSyogunDataInsertEntity.SHUKKO_GYOUSHACD = childNode.SelectSingleNode("gyoushaCd").InnerText;
    //                    }

    //                    if (childNode.SelectSingleNode("meter").InnerText != "")
    //                    { // 出庫実績レコードの出庫メーターが存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
    //                        mobileSyogunDataInsertEntity.SHUKKO_METER = Double.Parse(childNode.SelectSingleNode("meter").InnerText);
    //                    }

    //                    // モバイル将軍用データ取込画面専用テーブル登録
    //                    setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
    //                }
    //                break;

    //            // 帰庫実績レコード
    //            case "kiko":
    //                foreach (XmlNode childNode in Nodes)
    //                {
    //                    // モバイル将軍用データ取込画面専用テーブルエンティティの初期化
    //                    mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

    //                    // ノード枝番の編集
    //                    mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_KIKO;

    //                    mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
    //                    mobileSyogunDataInsertEntity.KIKO_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
    //                    mobileSyogunDataInsertEntity.KIKO_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
    //                    mobileSyogunDataInsertEntity.KIKO_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
    //                    mobileSyogunDataInsertEntity.KIKO_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
    //                    mobileSyogunDataInsertEntity.KIKO_KIKODATE = DateTime.Parse(childNode.SelectSingleNode("kikoDate").InnerText);

    //                    if (childNode.SelectSingleNode("meter").InnerText != "")
    //                    { // 帰庫実績レコードの帰庫メーターが存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
    //                        mobileSyogunDataInsertEntity.KIKO_METER = Double.Parse(childNode.SelectSingleNode("meter").InnerText);
    //                    }

    //                    // モバイル将軍用データ取込画面専用テーブル登録
    //                    setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
    //                }
    //                break;

    //            // 現場実績レコード
    //            case "genbaJisseki":
    //                foreach (XmlNode childNode in Nodes)
    //                {
    //                    // モバイル将軍用データ取込画面専用テーブルエンティティの初期化
    //                    mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

    //                    // ノード枝番の編集
    //                    mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_GENBAJISSEKI;

    //                    mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_SHUUSHUUTIME = DateTime.Parse(childNode.SelectSingleNode("shuushuuTime").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_GYOUSHACD = childNode.SelectSingleNode("gyoushaCd").InnerText;
    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_GENBACD = childNode.SelectSingleNode("genbaCd").InnerText;

    //                    if (childNode.SelectSingleNode("addGenbaFlg").InnerText == "1")
    //                    {
    //                        mobileSyogunDataInsertEntity.GENBA_JISSEKI_ADDGENBAFLG = true;
    //                    }
    //                    else
    //                    {
    //                        mobileSyogunDataInsertEntity.GENBA_JISSEKI_ADDGENBAFLG = false;
    //                    }

    //                    mobileSyogunDataInsertEntity.GENBA_JISSEKI_SHUKKONO = int.Parse(childNode.SelectSingleNode("shukkoNo").InnerText);

    //                    if (childNode.SelectSingleNode("jyogaiFlg").InnerText == "1")
    //                    {
    //                        mobileSyogunDataInsertEntity.GENBA_JISSEKI_JYOGAIFLG = true;
    //                    }
    //                    else
    //                    { // 0or出力されていない場合
    //                        mobileSyogunDataInsertEntity.GENBA_JISSEKI_JYOGAIFLG = false;
    //                    }

    //                    // モバイル将軍用データ取込画面専用テーブル登録
    //                    setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
    //                }
    //                break;

    //            // 現場明細レコード
    //            case "detail":
    //                foreach (XmlNode childNode in Nodes)
    //                {
    //                    // モバイル将軍用データ取込画面専用テーブルエンティティの初期化
    //                    mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

    //                    // ノード枝番の編集
    //                    mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_DETAIL;

    //                    mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
    //                    mobileSyogunDataInsertEntity.GENBA_DETAIL_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_DETAIL_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_DETAIL_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_DETAIL_NO = int.Parse(childNode.SelectSingleNode("No").InnerText);
    //                    mobileSyogunDataInsertEntity.GENBA_DETAIL_HINMEICD = childNode.SelectSingleNode("hinmeiCd").InnerText;

    //                    if (childNode.SelectSingleNode("suuryo1").InnerText != "")
    //                    {
    //                        mobileSyogunDataInsertEntity.GENBA_DETAIL_SUURYO1 = Double.Parse(childNode.SelectSingleNode("suuryo1").InnerText);
    //                    }

    //                    if ((childNode.SelectSingleNode("suuryo1").InnerText != "") &&
    //                        (childNode.SelectSingleNode("unitCd1").InnerText != ""))
    //                    { // 数量１が空なら無視
    //                        mobileSyogunDataInsertEntity.GENBA_DETAIL_SUURYO1 = Double.Parse(childNode.SelectSingleNode("unitCd1").InnerText);
    //                    }

    //                    if (childNode.SelectSingleNode("suuryo2") != null)
    //                    { // 現場明細レコードの数量２のタグが存在する場合　※タグ無しで取り込む場合もあるための処理
    //                        if ((childNode.SelectSingleNode("suuryo2").InnerText != "") &&
    //                            (childNode.SelectSingleNode("unitCd2").InnerText != ""))
    //                        { // 数量２、単位２は、どちらかが空ならもう一方も無視
    //                            mobileSyogunDataInsertEntity.GENBA_DETAIL_SUURYO2 = Double.Parse(childNode.SelectSingleNode("suuryo2").InnerText);
    //                            mobileSyogunDataInsertEntity.GENBA_DETAIL_UNIT_CD2 = Int16.Parse(childNode.SelectSingleNode("unitCd2").InnerText);
    //                        }
    //                    }

    //                    if (childNode.SelectSingleNode("addHinmeiFlg").InnerText == "1")
    //                    {
    //                        mobileSyogunDataInsertEntity.GENBA_DETAIL_ADDHINMEIFLG = true;
    //                    }
    //                    else
    //                    {
    //                        mobileSyogunDataInsertEntity.GENBA_DETAIL_ADDHINMEIFLG = false;
    //                    }

    //                    if (childNode.SelectSingleNode("maniNo") != null)
    //                    { // 現場明細レコードのマニフェスト番号のタグが存在する場合　※タグ無しで取り込む場合もあるための処理
    //                        mobileSyogunDataInsertEntity.GENBA_DETAIL_MANINO = Int64.Parse(childNode.SelectSingleNode("maniNo").InnerText);
    //                    }

    //                    mobileSyogunDataInsertEntity.GENBA_DETAIL_MANIKBN = Int16.Parse(childNode.SelectSingleNode("maniKbn").InnerText);

    //                    if (childNode.SelectSingleNode("hannyuuNo").InnerText != "")
    //                    { // 現場明細レコードの搬入番号が存在する場合　※タグ有り、値が空で取り込む場合もあるための処理
    //                        mobileSyogunDataInsertEntity.GENBA_DETAIL_HANNYUUNO = int.Parse(childNode.SelectSingleNode("hannyuuNo").InnerText);
    //                    }

    //                    mobileSyogunDataInsertEntity.GENBA_DETAIL_GENBA_NO = int.Parse(childNode.SelectSingleNode("genbaNo").InnerText);

    //                    // モバイル将軍用データ取込画面専用テーブル登録
    //                    setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
    //                }
    //                break;

    //            // 搬入実績レコード
    //            case "hannyuuJisseki":
    //                foreach (XmlNode childNode in Nodes)
    //                {
    //                    // モバイル将軍用データ取込画面専用テーブルエンティティの初期化
    //                    mobileSyogunDataInsertEntity = new T_MOBILE_SYOGUN_DATA_INSERT();

    //                    // ノード枝番の編集
    //                    mobileSyogunDataInsertEntity.NODE_EDABAN = NODE_EDABAN_HANNYUUJISSEKI;

    //                    mobileSyogunDataInsertEntity.EDABAN = this.Max_Edaban + 1;
    //                    mobileSyogunDataInsertEntity.HANNYUU_NO = int.Parse(childNode.SelectSingleNode("no").InnerText);
    //                    mobileSyogunDataInsertEntity.HANNYUU_CREATEDATE = DateTime.Parse(childNode.SelectSingleNode("createDate").InnerText);
    //                    mobileSyogunDataInsertEntity.HANNYUU_UPDATEDATE = DateTime.Parse(childNode.SelectSingleNode("updateDate").InnerText);
    //                    mobileSyogunDataInsertEntity.HANNYUU_UPDATECNT = int.Parse(childNode.SelectSingleNode("updateCnt").InnerText);
    //                    mobileSyogunDataInsertEntity.HANNYUU_HANNYUUDATE = DateTime.Parse(childNode.SelectSingleNode("hannyuuDate").InnerText);
    //                    mobileSyogunDataInsertEntity.HANNYUU_GYOUSHACD = childNode.SelectSingleNode("gyoushaCd").InnerText;
    //                    mobileSyogunDataInsertEntity.HANNYUU_GENBACD = childNode.SelectSingleNode("genbaCd").InnerText;
    //                    mobileSyogunDataInsertEntity.HANNYUU_RYO = Double.Parse(childNode.SelectSingleNode("ryo").InnerText);

    //                    // モバイル将軍用データ取込画面専用テーブル登録
    //                    setMobileSyogunDataInsert(mobileSyogunDataInsertEntity);
    //                }
    //                break;
    //        }
    //    }

    //    /// <summary>
    //    /// 受付(収集)入力テーブル取得処理
    //    /// </summary>
    //    public int getUketsukeSsEntry(int denpyouNo)
    //    {
    //        // 初期化
    //        this.int16KyotenCd = 0;
    //        this.int16ContenaSousaCd = 0;
    //        this.int16ManifestShuruiCd = 0;
    //        this.int16ManifestTehaiCd = 0;
    //        this.decShouhizeiRate = 0;
    //        this.moneyKingakuTotal = 0;
    //        this.moneyTaxSoto = 0;
    //        this.moneyTaxUchi = 0;
    //        this.moneyTaxSotoToal = 0;
    //        this.moneyTaxUchiToal = 0;

    //        // 以下の項目の取得
    //        // 拠点CD、車種CD、車種名、車輌CD、車輌名、補助員CD
    //        // 取引先CD、取引先名、業者名、現場名、荷卸業者CD、荷卸業者名、荷卸現場CD、荷卸現場名、営業担当者CD、営業担当者名
    //        // 運搬業者CD、運搬業者名、コンテナ操作CD、マニフェスト種類CD、マニフェスト手配CD、売上消費税率、売上金額合計、
    //        // 売上伝票毎消費税外税、売上伝票毎消費税内税、売上明細毎消費税外税合計、売上明細毎消費税内税合計
    //        MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
    //        entity.HAISHA_DENPYOU_NO = denpyouNo;
    //        this.uketsukeSsEntryResult = this.dao.GetUketsukeSsEntryForEntity(entity);

    //        DataRow[] selectedRows = uketsukeSsEntryResult.Select();
    //        foreach (DataRow row in selectedRows)
    //        {
    //            // 拠点CD
    //            if (row["KYOTEN_CD"] != DBNull.Value)
    //            {
    //                this.int16KyotenCd = (Int16)row["KYOTEN_CD"];
    //            }

    //            // 車種CD
    //            if (row["SHASHU_CD"] == DBNull.Value)
    //            {
    //                this.strShashuCd = null;
    //            }
    //            else
    //            {
    //                this.strShashuCd = (string)row["SHASHU_CD"];
    //            }

    //            // 車種名
    //            if (row["SHASHU_NAME"] == DBNull.Value)
    //            {
    //                this.strShashuName = null;
    //            }
    //            else
    //            {
    //                this.strShashuName = (string)row["SHASHU_NAME"];
    //            }

    //            // 車輌CD
    //            if (row["SHARYOU_CD"] == DBNull.Value)
    //            {
    //                this.strSharyouCd = null;
    //            }
    //            else
    //            {
    //                this.strSharyouCd = (string)row["SHARYOU_CD"];
    //            }

    //            // 車輌名
    //            if (row["SHARYOU_NAME"] == DBNull.Value)
    //            {
    //                this.strSharyouName = null;
    //            }
    //            else
    //            {
    //                this.strSharyouName = (string)row["SHARYOU_NAME"];
    //            }

    //            // 運転者名
    //            if (row["UNTENSHA_NAME"] == DBNull.Value)
    //            {
    //                this.strUntenshaName = null;
    //            }
    //            else
    //            {
    //                this.strUntenshaName = (string)row["UNTENSHA_NAME"];
    //            }

    //            // 補助員CD
    //            if (row["HOJOIN_CD"] == DBNull.Value)
    //            {
    //                this.strHojoinCd = null;
    //            }
    //            else
    //            {
    //                this.strHojoinCd = (string)row["HOJOIN_CD"];
    //            }

    //            // 取引先CD
    //            if (row["TORIHIKISAKI_CD"] == DBNull.Value)
    //            {
    //                this.strTorihikisakiCd = null;
    //            }
    //            else
    //            {
    //                this.strTorihikisakiCd = (string)row["TORIHIKISAKI_CD"];
    //            }

    //            // 取引先名
    //            if (row["TORIHIKISAKI_NAME"] == DBNull.Value)
    //            {
    //                this.strTorihikisakiName = null;
    //            }
    //            else
    //            {
    //                this.strTorihikisakiName = (string)row["TORIHIKISAKI_NAME"];
    //            }

    //            // 業者名
    //            if (row["GYOUSHA_NAME"] == DBNull.Value)
    //            {
    //                this.strGyoushaName = null;
    //            }
    //            else
    //            {
    //                this.strGyoushaName = (string)row["GYOUSHA_NAME"];
    //            }

    //            // 現場名
    //            if (row["GENBA_NAME"] == DBNull.Value)
    //            {
    //                this.strGenbaName = null;
    //            }
    //            else
    //            {
    //                this.strGenbaName = (string)row["GENBA_NAME"];
    //            }

    //            // 荷卸業者CD
    //            if (row["NIOROSHI_GYOUSHA_CD"] == DBNull.Value)
    //            {
    //                this.strNioroshiGyoushaCd = null;
    //            }
    //            else
    //            {
    //                this.strNioroshiGyoushaCd = (string)row["NIOROSHI_GYOUSHA_CD"];
    //            }

    //            // 荷卸業者名
    //            if (row["NIOROSHI_GYOUSHA_NAME"] == DBNull.Value)
    //            {
    //                this.strNioroshiGyoushaName = null;
    //            }
    //            else
    //            {
    //                this.strNioroshiGyoushaName = (string)row["NIOROSHI_GYOUSHA_NAME"];
    //            }

    //            // 荷卸現場CD
    //            if (row["NIOROSHI_GENBA_CD"] == DBNull.Value)
    //            {
    //                this.strNioroshiGenbaCd = null;
    //            }
    //            else
    //            {
    //                this.strNioroshiGenbaCd = (string)row["NIOROSHI_GENBA_CD"];
    //            }

    //            // 荷卸現場名
    //            if (row["NIOROSHI_GENBA_NAME"] == DBNull.Value)
    //            {
    //                this.strNioroshiGenbaName = null;
    //            }
    //            else
    //            {
    //                this.strNioroshiGenbaName = (string)row["NIOROSHI_GENBA_NAME"];
    //            }

    //            // 営業担当者CD
    //            if (row["EIGYOU_TANTOUSHA_CD"] == DBNull.Value)
    //            {
    //                this.strEigyouTantoushaCd = null;
    //            }
    //            else
    //            {
    //                this.strEigyouTantoushaCd = (string)row["EIGYOU_TANTOUSHA_CD"];
    //            }

    //            // 営業担当者名
    //            if (row["EIGYOU_TANTOUSHA_NAME"] == DBNull.Value)
    //            {
    //                this.strEigyouTantoushaName = null;
    //            }
    //            else
    //            {
    //                this.strEigyouTantoushaName = (string)row["EIGYOU_TANTOUSHA_NAME"];
    //            }

    //            // 運搬業者CD
    //            if (row["UNPAN_GYOUSHA_CD"] == DBNull.Value)
    //            {
    //                this.strUnpanGyoushaCd = null;
    //            }
    //            else
    //            {
    //                this.strUnpanGyoushaCd = (string)row["UNPAN_GYOUSHA_CD"];
    //            }

    //            // 運搬業者名
    //            if (row["UNPAN_GYOUSHA_NAME"] == DBNull.Value)
    //            {
    //                this.strUnpanGyoushaName = null;
    //            }
    //            else
    //            {
    //                this.strUnpanGyoushaName = (string)row["UNPAN_GYOUSHA_NAME"];
    //            }

    //            // コンテナ操作CD
    //            if (row["CONTENA_SOUSA_CD"] != DBNull.Value)
    //            {
    //                this.int16ContenaSousaCd = (Int16)row["CONTENA_SOUSA_CD"];
    //            }

    //            // マニフェスト種類CD
    //            if (row["MANIFEST_SHURUI_CD"] != DBNull.Value)
    //            {
    //                this.int16ManifestShuruiCd = (Int16)row["MANIFEST_SHURUI_CD"];
    //            }

    //            // マニフェスト手配CD
    //            if (row["MANIFEST_TEHAI_CD"] != DBNull.Value)
    //            {
    //                this.int16ManifestTehaiCd = (Int16)row["MANIFEST_TEHAI_CD"];
    //            }

    //            // 売上消費税率
    //            if (row["SHOUHIZEI_RATE"] != DBNull.Value)
    //            {
    //                this.decShouhizeiRate = (Decimal)row["SHOUHIZEI_RATE"];
    //            }

    //            // 売上金額合計
    //            if (row["KINGAKU_TOTAL"] != DBNull.Value)
    //            {
    //                this.moneyKingakuTotal = (Decimal)row["KINGAKU_TOTAL"];
    //            }

    //            // 売上伝票毎消費税外税
    //            if (row["TAX_SOTO"] != DBNull.Value)
    //            {
    //                this.moneyTaxSoto = (Decimal)row["TAX_SOTO"];
    //            }

    //            // 売上伝票毎消費税内税
    //            if (row["TAX_UCHI"] != DBNull.Value)
    //            {
    //                this.moneyTaxUchi = (Decimal)row["TAX_UCHI"];
    //            }

    //            // 売上明細毎消費税外税合計
    //            if (row["TAX_SOTO_TOTAL"] != DBNull.Value)
    //            {
    //                this.moneyTaxSotoToal = (Decimal)row["TAX_SOTO_TOTAL"];
    //            }

    //            // 売上明細毎消費税内税合計
    //            if (row["TAX_UCHI_TOTAL"] != DBNull.Value)
    //            {
    //                this.moneyTaxUchiToal = (Decimal)row["TAX_UCHI_TOTAL"];
    //            }
    //        }

    //        return selectedRows.Length;
    //    }

    //    /// <summary>
    //    /// 受付(収集)明細テーブル取得処理
    //    /// </summary>
    //    public int getUketsukeSsDetail(int denpyouNo, Int16 detailNo)
    //    {
    //        // 初期化
    //        this.int16DenpyouKbnCd = 0;
    //        this.dblSuuryou = 0;
    //        this.int16UnitCd = 0;
    //        this.moneyTanka = 0;
    //        this.moneyTaxSotoDetail = 0;
    //        this.moneyTaxUchiDetail = 0;
    //        this.int16HinmeiZeiKbnCd = 0;
    //        this.moneyHinmeiKingaku = 0;
    //        this.moneyHinmeiTaxSoto = 0;
    //        this.moneyHinmeiTaxUchi = 0;

    //        // 以下の項目の取得
    //        // 伝票区分CD、品名CD、品名、数量、単位CD、単価、金額、消費税外税、消費税内税、品名別税区分CD、
    //        // 品名別金額、品名別消費税外税、品名別消費税内税、明細備考
    //        UketsukeSsDetailDTOClass entity = new UketsukeSsDetailDTOClass();
    //        entity.UKETUKE_NUMBER = denpyouNo;
    //        entity.ROW_NO = detailNo;
    //        this.uketsukeSsDetailResult = this.dao.GetUketsukeSsDetailForEntity(entity);

    //        DataRow[] selectedRows = uketsukeSsDetailResult.Select();
    //        foreach (DataRow row in selectedRows)
    //        {
    //            // 伝票区分CD
    //            if (row["DENPYOU_KBN_CD"] != DBNull.Value)
    //            {
    //                this.int16DenpyouKbnCd = (Int16)row["DENPYOU_KBN_CD"];
    //            }

    //            // 品名CD
    //            if (row["HINMEI_CD"] == DBNull.Value)
    //            {
    //                this.strHinmeiCd = null;
    //            }
    //            else
    //            {
    //                this.strHinmeiCd = (string)row["HINMEI_CD"];
    //            }

    //            // 品名
    //            if (row["HINMEI_NAME"] == DBNull.Value)
    //            {
    //                this.strHinmeiName = null;
    //            }
    //            else
    //            {
    //                this.strHinmeiName = (string)row["HINMEI_NAME"];
    //            }

    //            // 数量
    //            if (row["SUURYOU"] != DBNull.Value)
    //            {
    //                this.dblSuuryou = (Double)row["SUURYOU"];
    //            }

    //            // 単位CD
    //            if (row["UNIT_CD"] != DBNull.Value)
    //            {
    //                this.int16UnitCd = (Int16)row["UNIT_CD"];
    //            }

    //            // 単価
    //            if (row["TANKA"] != DBNull.Value)
    //            {
    //                this.moneyTanka = (Decimal)row["TANKA"];
    //            }

    //            // 金額
    //            if (row["KINGAKU"] != DBNull.Value)
    //            {
    //                this.moneyKingaku = (Decimal)row["KINGAKU"];
    //            }

    //            // 消費税外税
    //            if (row["TAX_SOTO"] != DBNull.Value)
    //            {
    //                this.moneyTaxSotoDetail = (Decimal)row["TAX_SOTO"];
    //            }

    //            // 消費税内税
    //            if (row["TAX_UCHI"] != DBNull.Value)
    //            {
    //                this.moneyTaxUchiDetail = (Decimal)row["TAX_UCHI"];
    //            }

    //            // 品名別税区分CD
    //            if (row["HINMEI_ZEI_KBN_CD"] != DBNull.Value)
    //            {
    //                this.int16HinmeiZeiKbnCd = (Int16)row["HINMEI_ZEI_KBN_CD"];
    //            }

    //            // 品名別金額
    //            if (row["HINMEI_KINGAKU"] != DBNull.Value)
    //            {
    //                this.moneyHinmeiKingaku = (Decimal)row["HINMEI_KINGAKU"];
    //            }

    //            // 品名別消費税外税
    //            if (row["HINMEI_TAX_SOTO"] != DBNull.Value)
    //            {
    //                this.moneyHinmeiTaxSoto = (Decimal)row["HINMEI_TAX_SOTO"];
    //            }

    //            // 品名別消費税内税
    //            if (row["HINMEI_TAX_UCHI"] != DBNull.Value)
    //            {
    //                this.moneyHinmeiTaxUchi = (Decimal)row["HINMEI_TAX_UCHI"];
    //            }

    //            // 明細備考
    //            if (row["MEISAI_BIKOU"] == DBNull.Value)
    //            {
    //                this.strMeisaiBikou = null;
    //            }
    //            else
    //            {
    //                this.strMeisaiBikou = (string)row["MEISAI_BIKOU"];
    //            }
    //        }

    //        return selectedRows.Length;
    //    }

    //    /// <summary>
    //    /// コース_明細内訳マスタ取得処理
    //    /// </summary>
    //    public int getCourseDetailItems(string courseNameCd)
    //    {
    //        this.int16teikiTsukiKbn = 0;

    //        // 月極区分の取得
    //        MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
    //        entity.COURSE_NAME_CD = courseNameCd;
    //        this.courseDetailItemsResult = this.dao.GetCourseDetailItemsForEntity(entity);

    //        DataRow[] selectedRows = courseDetailItemsResult.Select();
    //        foreach (DataRow row in selectedRows)
    //        {
    //            // 月極区分
    //            if (row["TEIKI_TSUKI_KBN"] != DBNull.Value)
    //            {
    //                this.int16teikiTsukiKbn = (Int16)row["TEIKI_TSUKI_KBN"];
    //            }
    //        }

    //        return selectedRows.Length;
    //    }

    //    /// <summary>
    //    /// 取引先_請求情報マスタ取得処理
    //    /// </summary>
    //    public int getMtorihikisakiSeikyuu(string torihikisakiCd)
    //    {
    //        this.int16ZeiKeisanKbnCd = 0;
    //        this.int16ZeiKbnCd = 0;
    //        this.int16TorihikiKbnCd = 0;

    //        // 税計算区分CD、税区分CD、取引区分CDの取得
    //        TorihikisakiSeikyuuDTOClass entity = new TorihikisakiSeikyuuDTOClass();
    //        entity.TORIHIKISAKI_CD = torihikisakiCd;
    //        this.mtorihikisakiSeikyuuResult = this.dao.GetMtorihikisakiSeikyuuForEntity(entity);

    //        DataRow[] selectedRows = mtorihikisakiSeikyuuResult.Select();
    //        foreach (DataRow row in selectedRows)
    //        {
    //            // 税計算区分CD
    //            if (row["ZEI_KEISAN_KBN_CD"] != DBNull.Value)
    //            {
    //                this.int16ZeiKeisanKbnCd = (Int16)row["ZEI_KEISAN_KBN_CD"];
    //            }

    //            // 税区分CD
    //            if (row["ZEI_KBN_CD"] != DBNull.Value)
    //            {
    //                this.int16ZeiKbnCd = (Int16)row["ZEI_KBN_CD"];
    //            }

    //            // 取引区分CD
    //            if (row["TORIHIKI_KBN_CD"] != DBNull.Value)
    //            {
    //                this.int16TorihikiKbnCd = (Int16)row["TORIHIKI_KBN_CD"];
    //            }
    //        }

    //        return selectedRows.Length;
    //    }

    //    /// <summary>
    //    /// モバイル将軍用データ取込画面専用テーブル登録
    //    /// </summary>
    //    void setMobileSyogunDataInsert(T_MOBILE_SYOGUN_DATA_INSERT Entity)
    //    {
    //        // シーケンシャルナンバーのMAX値を取得する
    //        getTorikomizumiData("MAX_SEQ_NO");

    //        // キー項目の編集
    //        Entity.SEQ_NO = this.Max_Seq_No + 1;

    //        // 共通項目の編集
    //        Entity.JISSEKI_REGIST_FLG = false;
    //        Entity.DELETE_FLG = false;

    //        // TODO 以下はシステムで自動的に編集される項目なので、後に削除が必要
    //        Entity.CREATE_USER = "TEST_USER";
    //        Entity.CREATE_DATE = DateTime.Now;
    //        Entity.SEARCH_CREATE_DATE = "TEST_DATE";
    //        Entity.CREATE_PC = "TEST_PC";
    //        Entity.UPDATE_USER = "TEST_USER";
    //        Entity.UPDATE_DATE = DateTime.Now;
    //        Entity.SEARCH_UPDATE_DATE = "TEST_DATE";
    //        Entity.UPDATE_PC = "TEST_PC";

    //        // モバイル将軍用データ取込画面専用テーブルに登録する
    //        setMobileSyogunDataInsertDao.Insert(Entity);
    //    }

    //    /// <summary>
    //    /// 定期実績入力テーブル登録
    //    /// </summary>
    //    void setTeikiJissekiEntry(DataRow[] selectedRowsEdaban)
    //    {
    //        // 編集領域
    //        // 編集フラグと編集項目を含んだ配列を作成する
    //        // [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
    //        // [1]＝編集有無フラグが編集ありの場合に編集される項目値
    //        // （エンティティを初期化することによる、int項目のnull値許容対応）
    //        string[] KYOTEN_CD = new string[2];
    //        string[] WEATHER_CD = new string[2];
    //        string[] DENPYOU_DATE = new string[2];
    //        string[] SEARCH_DENPYOU_DATE = new string[2];
    //        string[] SAGYOU_DATE = new string[2];
    //        string[] SEARCH_SAGYOU_DATE = new string[2];
    //        string[] COURSE_NAME_CD = new string[2];
    //        string[] SHARYOU_CD = new string[2];
    //        string[] SHASHU_CD = new string[2];
    //        string[] UNTENSHA_CD = new string[2];
    //        string[] HOJOIN_CD = new string[2];
    //        string[] SHUKKO_METER = new string[2];
    //        string[] SHUKKO_HOUR = new string[2];
    //        string[] SHUKKO_MINUTE = new string[2];
    //        string[] KIKO_METER = new string[2];
    //        string[] KIKO_HOUR = new string[2];
    //        string[] KIKO_MINUTE = new string[2];
    //        string[] TEIKI_HAISHA_NUMBER = new string[2];
    //        string[] MOBILE_SHOGUN_FILE_NAME = new string[2];
    //        string[] DELETE_FLG = new string[2];

    //        // 編集ありフラグ
    //        string HensyuFlg_Ari = "1";
    //        string HensyuFlg_Nashi = "0";

    //        // 定期実績入力テーブルテーブルエンティティの初期化
    //        T_TEIKI_JISSEKI_ENTRY Entity = new T_TEIKI_JISSEKI_ENTRY();

    //        // 各項目の編集
    //        // 配車ヘッダレコードの情報を取得する
    //        for (int i = 0; i < selectedRowsEdaban.Length; i++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[i]["NODE_EDABAN"].ToString()) == NODE_EDABAN_HAISHA)
    //            { // 配車ヘッダレコードの場合

    //                // 拠点CD：配車ヘッダレコードの拠点CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["KYOTEN_CD"].ToString())))
    //                {
    //                    KYOTEN_CD[0] = HensyuFlg_Ari;
    //                    KYOTEN_CD[1] = selectedRowsEdaban[i]["KYOTEN_CD"].ToString();
    //                }
    //                else
    //                {
    //                    KYOTEN_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 伝票日付：配車ヘッダレコードの伝票番号をキーに定期配車入力テーブルから取得
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
    //                { // 配車ヘッダレコードの伝票番号が存在する場合
    //                    if (getTeikiHaishaEntry(int.Parse(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())) != 0)
    //                    { // 配車ヘッダレコードの伝票番号に紐付く定期配車入力テーブルの伝票日付が存在する場合
    //                        DENPYOU_DATE[0] = HensyuFlg_Ari;
    //                        DENPYOU_DATE[1] = this.dtDenpyouDate.ToString();
    //                    }
    //                    else
    //                    {
    //                        DENPYOU_DATE[0] = HensyuFlg_Nashi;
    //                    }
    //                }

    //                // 作業日：配車ヘッダレコードの作業日を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString())))
    //                {
    //                    SAGYOU_DATE[0] = HensyuFlg_Ari;
    //                    SAGYOU_DATE[1] = selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString();
    //                }
    //                else
    //                {
    //                    SAGYOU_DATE[0] = HensyuFlg_Nashi;
    //                }

    //                // コース名称CD：配車ヘッダレコードのコース名称CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_COURSE_NAME_CD"].ToString())))
    //                {
    //                    COURSE_NAME_CD[0] = HensyuFlg_Ari;
    //                    COURSE_NAME_CD[1] = selectedRowsEdaban[i]["HAISHA_COURSE_NAME_CD"].ToString();
    //                }
    //                else
    //                {
    //                    COURSE_NAME_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 車種CD：配車ヘッダレコードの車種CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["SHASHU_CD"].ToString())))
    //                {
    //                    SHASHU_CD[0] = HensyuFlg_Ari;
    //                    SHASHU_CD[1] = selectedRowsEdaban[i]["SHASHU_CD"].ToString();
    //                }
    //                else
    //                {
    //                    SHASHU_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 運転者CD：配車ヘッダレコードの運転者CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_UNTENSHA_CD"].ToString())))
    //                {
    //                    UNTENSHA_CD[0] = HensyuFlg_Ari;
    //                    UNTENSHA_CD[1] = selectedRowsEdaban[i]["HAISHA_UNTENSHA_CD"].ToString();
    //                }
    //                else
    //                {
    //                    UNTENSHA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 補助員CD：配車ヘッダレコードの伝票番号をキーに受付(収集)入力テーブルから取得
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
    //                { // 配車ヘッダレコードの伝票番号が存在する場合
    //                    if ((getUketsukeSsEntry(int.Parse(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())) != 0) &&
    //                         !(string.IsNullOrEmpty(this.strHojoinCd)))
    //                    { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの補助員CDが存在する場合
    //                        HOJOIN_CD[0] = HensyuFlg_Ari;
    //                        HOJOIN_CD[1] = this.strHojoinCd.ToString();
    //                    }
    //                    else
    //                    {
    //                        HOJOIN_CD[0] = HensyuFlg_Nashi;
    //                    }
    //                }

    //                // 定期配車番号：配車ヘッダレコードの伝票番号を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
    //                {
    //                    TEIKI_HAISHA_NUMBER[0] = HensyuFlg_Ari;
    //                    TEIKI_HAISHA_NUMBER[1] = selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString();
    //                }
    //                else
    //                {
    //                    TEIKI_HAISHA_NUMBER[0] = HensyuFlg_Nashi;
    //                }

    //                // モバイル将軍ファイル名：配車ヘッダレコードの取込ファイル名を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_TORIKOMI_FILENAME"].ToString())))
    //                {
    //                    MOBILE_SHOGUN_FILE_NAME[0] = HensyuFlg_Ari;
    //                    MOBILE_SHOGUN_FILE_NAME[1] = selectedRowsEdaban[i]["HAISHA_TORIKOMI_FILENAME"].ToString();
    //                }
    //                else
    //                {
    //                    MOBILE_SHOGUN_FILE_NAME[0] = HensyuFlg_Nashi;
    //                }
    //            }
    //        }

    //        // 現場実績レコードの情報を取得する
    //        for (int iGJ = 0; iGJ < selectedRowsEdaban.Length; iGJ++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[iGJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_GENBAJISSEKI)
    //            { // 現場実績レコードの場合

    //                // 現場実績レコードの出庫Noに紐付く出庫実績レコードの各項目を編集する
    //                // 出庫実績レコードの情報を取得する
    //                for (int iSJ = 0; iSJ < selectedRowsEdaban.Length; iSJ++)
    //                { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //                    if (Int64.Parse(selectedRowsEdaban[iSJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_SHUKKO)
    //                    { // 出庫実績レコードの場合
    //                        if (int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_SHUKKONO"].ToString()) == int.Parse(selectedRowsEdaban[iSJ]["SHUKKO_NO"].ToString()))
    //                        { // 現場実績レコードの出庫Noと出庫実績レコードのレコード番号が紐付いた場合（実質紐付くのは１レコードのみ）

    //                            // 車輌CD：出庫実績レコードの車輌コードを編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_SHARYOUCD"].ToString())))
    //                            {
    //                                SHARYOU_CD[0] = HensyuFlg_Ari;
    //                                SHARYOU_CD[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHARYOUCD"].ToString();
    //                            }
    //                            else
    //                            {
    //                                SHARYOU_CD[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 出庫メーター：出庫実績レコードの出庫メーターを編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_METER"].ToString())))
    //                            {
    //                                SHUKKO_METER[0] = HensyuFlg_Ari;
    //                                SHUKKO_METER[1] = selectedRowsEdaban[iSJ]["SHUKKO_METER"].ToString();
    //                            }
    //                            else
    //                            {
    //                                SHUKKO_METER[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 出庫時間_時
    //                            // 出庫時間_分
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString())))
    //                            {
    //                                SHUKKO_HOUR[0] = HensyuFlg_Ari;
    //                                SHUKKO_MINUTE[0] = HensyuFlg_Ari;

    //                                if (selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(13, 1) == ":")
    //                                { // 時間表記が2文字の場合（例＝2013/10/02 15:41:05）
    //                                    // 出庫時間_時：出庫実績レコードの出庫日時の時を編集する
    //                                    SHUKKO_HOUR[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(11, 2);

    //                                    // 出庫時間_分：出庫実績レコードの出庫日時の分を編集する
    //                                    SHUKKO_MINUTE[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(14, 2);
    //                                }
    //                                else
    //                                { // 時間表記が1文字の場合（例＝2013/09/30 9:13:00）
    //                                    // 出庫時間_時：出庫実績レコードの出庫日時の時を編集する
    //                                    SHUKKO_HOUR[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(11, 1);

    //                                    // 出庫時間_分：出庫実績レコードの出庫日時の分を編集する
    //                                    SHUKKO_MINUTE[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHUKKODATE"].ToString().Substring(13, 2);
    //                                }
    //                            }
    //                            else
    //                            {
    //                                SHUKKO_HOUR[0] = HensyuFlg_Nashi;
    //                                SHUKKO_MINUTE[0] = HensyuFlg_Nashi;
    //                            }
    //                        }
    //                    }
    //                }

    //                // 現場実績レコードの出庫Noに紐付く帰庫実績レコードの各項目を編集する
    //                // 帰庫実績レコードの情報を取得する
    //                for (int iKJ = 0; iKJ < selectedRowsEdaban.Length; iKJ++)
    //                { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //                    if (Int64.Parse(selectedRowsEdaban[iKJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_KIKO)
    //                    { // 帰庫実績レコードの場合
    //                        if (int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_SHUKKONO"].ToString()) == int.Parse(selectedRowsEdaban[iKJ]["KIKO_NO"].ToString()))
    //                        { // 現場実績レコードの出庫Noと帰庫実績レコードのレコード番号が紐付いた場合（実質紐付くのは１レコードのみ）

    //                            // 帰庫メーター：帰庫実績レコードの帰庫メーターを編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iKJ]["KIKO_METER"].ToString())))
    //                            {
    //                                KIKO_METER[0] = HensyuFlg_Ari;
    //                                KIKO_METER[1] = selectedRowsEdaban[iKJ]["KIKO_METER"].ToString();
    //                            }
    //                            else
    //                            {
    //                                KIKO_METER[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 帰庫時間_時
    //                            // 帰庫時間_分
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString())))
    //                            {
    //                                KIKO_HOUR[0] = HensyuFlg_Ari;
    //                                KIKO_MINUTE[0] = HensyuFlg_Ari;

    //                                if (selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(13, 1) == ":")
    //                                { // 時間表記が2文字の場合（例＝2013/10/02 15:41:05）
    //                                    // 帰庫時間_時：帰庫実績レコードの帰庫日時の時を編集する
    //                                    KIKO_HOUR[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(11, 2);

    //                                    // 帰庫時間_分：帰庫実績レコードの帰庫日時の分を編集する
    //                                    KIKO_MINUTE[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(14, 2);
    //                                }
    //                                else
    //                                { // 時間表記が1文字の場合（例＝2013/09/30 9:13:00）
    //                                    // 帰庫時間_時：帰庫実績レコードの帰庫日時の時を編集する
    //                                    KIKO_HOUR[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(11, 1);

    //                                    // 帰庫時間_分：帰庫実績レコードの帰庫日時の分を編集する
    //                                    KIKO_MINUTE[1] = selectedRowsEdaban[iKJ]["KIKO_KIKODATE"].ToString().Substring(13, 2);
    //                                }
    //                            }
    //                            else
    //                            {
    //                                KIKO_HOUR[0] = HensyuFlg_Nashi;
    //                                KIKO_MINUTE[0] = HensyuFlg_Nashi;
    //                            }
    //                        }
    //                    }
    //                }

    //                // 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
    //                // 定期実績明細、定期実績荷卸の編集用の退避
    //                this.int64SystemId = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));
    //                this.int64TeikiJissekiNumber = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));

    //                // システムID
    //                Entity.SYSTEM_ID = this.int64SystemId;

    //                // 枝番
    //                Entity.SEQ = 1;

    //                // 拠点CD
    //                if (KYOTEN_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.KYOTEN_CD = Int16.Parse(KYOTEN_CD[1]);
    //                }

    //                // 定期実績番号
    //                Entity.TEIKI_JISSEKI_NUMBER = this.int64TeikiJissekiNumber;

    //                // 天気CD　TODO ダミーで設定
    //                Entity.WEATHER_CD = 0;

    //                // 伝票日付
    //                if (DENPYOU_DATE[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.DENPYOU_DATE = DateTime.Parse(DENPYOU_DATE[1]);
    //                }

    //                // 作業日
    //                if (SAGYOU_DATE[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SAGYOU_DATE = DateTime.Parse(SAGYOU_DATE[1]);
    //                }

    //                // コース名称CD
    //                if (COURSE_NAME_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.COURSE_NAME_CD = COURSE_NAME_CD[1];
    //                }

    //                // 車輌CD
    //                if (SHARYOU_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHARYOU_CD = SHARYOU_CD[1];
    //                }

    //                // 車種CD
    //                if (SHASHU_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHASHU_CD = SHASHU_CD[1];
    //                }

    //                // 運転者CD
    //                if (UNTENSHA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.UNTENSHA_CD = UNTENSHA_CD[1];
    //                }

    //                // 補助員CD
    //                if (HOJOIN_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.HOJOIN_CD = HOJOIN_CD[1];
    //                }

    //                // 出庫メーター
    //                if (SHUKKO_METER[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHUKKO_METER = Double.Parse(SHUKKO_METER[1]);
    //                }

    //                // 出庫時間_時
    //                if (SHUKKO_HOUR[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHUKKO_HOUR = Int16.Parse(SHUKKO_HOUR[1]);
    //                }

    //                // 出庫時間_分
    //                if (SHUKKO_MINUTE[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHUKKO_MINUTE = Int16.Parse(SHUKKO_MINUTE[1]);
    //                }

    //                // 帰庫メーター
    //                if (KIKO_METER[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.KIKO_METER = Double.Parse(KIKO_METER[1]);
    //                }

    //                // 帰庫時間_時
    //                if (KIKO_HOUR[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.KIKO_HOUR = Int16.Parse(KIKO_HOUR[1]);
    //                }

    //                // 帰庫時間_分
    //                if (KIKO_MINUTE[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.KIKO_MINUTE = Int16.Parse(KIKO_MINUTE[1]);
    //                }

    //                // 定期配車番号
    //                if (TEIKI_HAISHA_NUMBER[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.TEIKI_HAISHA_NUMBER = Int64.Parse(TEIKI_HAISHA_NUMBER[1]);
    //                }

    //                // モバイル将軍ファイル名
    //                if (MOBILE_SHOGUN_FILE_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.MOBILE_SHOGUN_FILE_NAME = MOBILE_SHOGUN_FILE_NAME[1];
    //                }

    //                Entity.DELETE_FLG = false;      // 削除フラグ

    //                // TODO 以下はシステムで自動的に編集される項目なので、後に削除が必要
    //                Entity.CREATE_USER = "TEST_USER";
    //                Entity.CREATE_DATE = DateTime.Now;
    //                Entity.SEARCH_CREATE_DATE = "TEST_DATE";
    //                Entity.CREATE_PC = "TEST_PC";
    //                Entity.UPDATE_USER = "TEST_USER";
    //                Entity.UPDATE_DATE = DateTime.Now;
    //                Entity.SEARCH_UPDATE_DATE = "TEST_DATE";
    //                Entity.UPDATE_PC = "TEST_PC";
    //                // TODO ここまで

    //                // 定期実績入力テーブルに登録する
    //                setTeikiJissekiEntryDao.Insert(Entity);

    //                // 定期実績入力テーブルテーブルエンティティの初期化
    //                Entity = new T_TEIKI_JISSEKI_ENTRY();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 定期配車入力テーブル取得処理
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    public int getTeikiHaishaEntry(int HaishaDenpyouNo)
    //    {
    //        this.dtDenpyouDate = DateTime.Now;

    //        // 定期配車入力テーブル取得
    //        MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
    //        entity.HAISHA_DENPYOU_NO = HaishaDenpyouNo;
    //        this.teikiHaishaEntryResult = this.dao.GetTeikiHaishaEntryDataForEntity(entity);

    //        DataRow[] selectedRows = teikiHaishaEntryResult.Select();
    //        foreach (DataRow row in selectedRows)
    //        {
    //            this.dtDenpyouDate = (DateTime)row["DENPYOU_DATE"];
    //        }

    //        return selectedRows.Length;
    //    }

    //    /// <summary>
    //    /// 定期実績明細テーブル登録
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void setTeikiJissekiDetail(DataRow[] selectedRowsEdaban)
    //    {
    //        // 編集領域
    //        // 編集フラグと編集項目を含んだ配列を作成する
    //        // [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
    //        // [1]＝編集有無フラグが編集ありの場合に編集される項目値
    //        // （エンティティを初期化することによる、int項目のnull値許容対応）
    //        string[] ROW_NUMBER = new string[2];
    //        string[] GYOUSHA_CD = new string[2];
    //        string[] GENBA_CD = new string[2];
    //        string[] HINMEI_CD = new string[2];
    //        string[] SUURYOU = new string[2];
    //        string[] UNIT_CD = new string[2];
    //        string[] ANBUN_SUURYOU = new string[2];
    //        string[] NIOROSHI_NUMBER = new string[2];
    //        string[] TSUKIGIME_KBN = new string[2];

    //        // 編集ありフラグ
    //        string HensyuFlg_Ari = "1";
    //        string HensyuFlg_Nashi = "0";

    //        // 定期実績明細テーブルエンティティの初期化
    //        T_TEIKI_JISSEKI_DETAIL Entity = new T_TEIKI_JISSEKI_DETAIL();

    //        // 各項目の編集
    //        // 配車ヘッダレコードの情報を取得する
    //        for (int i = 0; i < selectedRowsEdaban.Length; i++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[i]["NODE_EDABAN"].ToString()) == NODE_EDABAN_HAISHA)
    //            { // 配車ヘッダレコードの場合

    //                // 月極区分：配車ヘッダレコードのコース名称CDをキーにコース_明細内訳マスタから取得
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_COURSE_NAME_CD"].ToString())))
    //                { // 配車ヘッダレコードのコース名称CDが存在する場合
    //                    if (getCourseDetailItems(selectedRowsEdaban[i]["HAISHA_COURSE_NAME_CD"].ToString()) != 0)
    //                    { // 配車ヘッダレコードのコース名称CDに紐付くコース_明細内訳マスタの月極区分が存在する場合
    //                        TSUKIGIME_KBN[0] = HensyuFlg_Ari;
    //                        TSUKIGIME_KBN[1] = this.int16teikiTsukiKbn.ToString();
    //                    }
    //                    else
    //                    {
    //                        TSUKIGIME_KBN[0] = HensyuFlg_Nashi;
    //                    }
    //                }
    //            }
    //        }

    //        // 現場実績レコードの情報を取得する
    //        for (int iGJ = 0; iGJ < selectedRowsEdaban.Length; iGJ++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[iGJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_GENBAJISSEKI)
    //            { // 現場実績レコードの場合

    //                // 業者CD：現場実績レコードの業者CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GYOUSHACD"].ToString())))
    //                {
    //                    GYOUSHA_CD[0] = HensyuFlg_Ari;
    //                    GYOUSHA_CD[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GYOUSHACD"].ToString();
    //                }
    //                else
    //                {
    //                    GYOUSHA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 現場CD：現場実績レコードの現場CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString())))
    //                {
    //                    GENBA_CD[0] = HensyuFlg_Ari;
    //                    GENBA_CD[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString();
    //                }
    //                else
    //                {
    //                    GENBA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 現場実績レコードのレコード番号に紐付く現場明細レコードの各項目を編集する
    //                // 現場明細レコードの情報を取得する
    //                for (int iGM = 0; iGM < selectedRowsEdaban.Length; iGM++)
    //                { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //                    if (Int64.Parse(selectedRowsEdaban[iGM]["NODE_EDABAN"].ToString()) == NODE_EDABAN_DETAIL)
    //                    { // 現場明細レコードの場合
    //                        if (int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_NO"].ToString()) == int.Parse(selectedRowsEdaban[iGM]["GENBA_DETAIL_GENBA_NO"].ToString()))
    //                        { // 現場実績レコードのレコード番号と現場明細レコードの現場Noが紐付いた場合(現場実績：現場明細＝１：Ｎで紐付く)

    //                            // 行番号：現場明細レコードのレコード番号を編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString())))
    //                            {
    //                                ROW_NUMBER[0] = HensyuFlg_Ari;
    //                                ROW_NUMBER[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString();
    //                            }
    //                            else
    //                            {
    //                                ROW_NUMBER[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 品名CD：現場明細レコードの品名CDを編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_HINMEICD"].ToString())))
    //                            {
    //                                HINMEI_CD[0] = HensyuFlg_Ari;
    //                                HINMEI_CD[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_HINMEICD"].ToString();
    //                            }
    //                            else
    //                            {
    //                                HINMEI_CD[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 数量：現場明細レコードの数量１を編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO1"].ToString())))
    //                            {
    //                                SUURYOU[0] = HensyuFlg_Ari;
    //                                SUURYOU[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO1"].ToString();
    //                            }
    //                            else
    //                            {
    //                                SUURYOU[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 単位CD：現場明細レコードの単位１を編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_UNIT_CD1"].ToString())))
    //                            {
    //                                UNIT_CD[0] = HensyuFlg_Ari;
    //                                UNIT_CD[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_UNIT_CD1"].ToString();
    //                            }
    //                            else
    //                            {
    //                                UNIT_CD[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 按分数量：現場明細レコードの数量２を編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO2"].ToString())))
    //                            {
    //                                ANBUN_SUURYOU[0] = HensyuFlg_Ari;
    //                                ANBUN_SUURYOU[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_SUURYO2"].ToString();
    //                            }
    //                            else
    //                            {
    //                                ANBUN_SUURYOU[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 荷卸No：現場明細レコードの搬入番号を編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_HANNYUUNO"].ToString())))
    //                            {
    //                                NIOROSHI_NUMBER[0] = HensyuFlg_Ari;
    //                                NIOROSHI_NUMBER[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_HANNYUUNO"].ToString();
    //                            }
    //                            else
    //                            {
    //                                NIOROSHI_NUMBER[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
    //                            // システムID
    //                            Entity.SYSTEM_ID = this.int64SystemId;

    //                            // 枝番
    //                            Entity.SEQ = 1;

    //                            // 明細システムID
    //                            Entity.DETAIL_SYSTEM_ID = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));

    //                            // 定期実績番号
    //                            Entity.TEIKI_JISSEKI_NUMBER = this.int64TeikiJissekiNumber;

    //                            // 行番号
    //                            if (ROW_NUMBER[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.ROW_NUMBER = Int16.Parse(ROW_NUMBER[1]);
    //                            }

    //                            // 業者CD
    //                            if (GYOUSHA_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.GYOUSHA_CD = GYOUSHA_CD[1];
    //                            }

    //                            // 現場CD
    //                            if (GENBA_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.GENBA_CD = GENBA_CD[1];
    //                            }

    //                            // 品名CD
    //                            if (HINMEI_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.HINMEI_CD = HINMEI_CD[1];
    //                            }

    //                            // 数量
    //                            if (SUURYOU[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.SUURYOU = Double.Parse(SUURYOU[1]);
    //                            }

    //                            // 単位CD
    //                            if (UNIT_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.UNIT_CD = Int16.Parse(UNIT_CD[1]);
    //                            }

    //                            // 按分数量
    //                            if (ANBUN_SUURYOU[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.ANBUN_SUURYOU = Double.Parse(ANBUN_SUURYOU[1]);
    //                            }

    //                            // 荷卸No
    //                            if (NIOROSHI_NUMBER[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.NIOROSHI_NUMBER = int.Parse(NIOROSHI_NUMBER[1]);
    //                            }

    //                            // 月極区分
    //                            if (TSUKIGIME_KBN[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.TSUKIGIME_KBN = Int16.Parse(TSUKIGIME_KBN[1]);
    //                            }

    //                            // 定期実績明細テーブル登録
    //                            setTeikiJissekiDetailDao.Insert(Entity);

    //                            // 定期実績明細テーブルエンティティの初期化
    //                            Entity = new T_TEIKI_JISSEKI_DETAIL();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 定期実績荷卸テーブル登録
    //    /// </summary>
    //    void setTeikiJissekiNioroshi(DataRow[] selectedRowsEdaban)
    //    {
    //        // 編集領域
    //        // 編集フラグと編集項目を含んだ配列を作成する
    //        // [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
    //        // [1]＝編集有無フラグが編集ありの場合に編集される項目値
    //        // （エンティティを初期化することによる、int項目のnull値許容対応）
    //        string[] NIOROSHI_NUMBER = new string[2];
    //        string[] NIOROSHI_GYOUSHA_CD = new string[2];
    //        string[] NIOROSHI_GENBA_CD = new string[2];
    //        string[] NIOROSHI_RYOU = new string[2];

    //        // 編集ありフラグ
    //        string HensyuFlg_Ari = "1";
    //        string HensyuFlg_Nashi = "0";

    //        // 定期実績荷卸テーブルエンティティの初期化
    //        T_TEIKI_JISSEKI_NIOROSHI Entity = new T_TEIKI_JISSEKI_NIOROSHI();

    //        // 各項目の編集
    //        // 搬入実績レコードの情報を取得する
    //        for (int iHJ = 0; iHJ < selectedRowsEdaban.Length; iHJ++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[iHJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_HANNYUUJISSEKI)
    //            { // 搬入実績レコードの場合

    //                // 荷卸No：搬入実績レコードの搬入番号を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_NO"].ToString())))
    //                {
    //                    NIOROSHI_NUMBER[0] = HensyuFlg_Ari;
    //                    NIOROSHI_NUMBER[1] = selectedRowsEdaban[iHJ]["HANNYUU_NO"].ToString();
    //                }
    //                else
    //                {
    //                    NIOROSHI_NUMBER[0] = HensyuFlg_Nashi;
    //                }

    //                // 荷卸業者CD：搬入実績レコードの業者CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_GYOUSHACD"].ToString())))
    //                {
    //                    NIOROSHI_GYOUSHA_CD[0] = HensyuFlg_Ari;
    //                    NIOROSHI_GYOUSHA_CD[1] = selectedRowsEdaban[iHJ]["HANNYUU_GYOUSHACD"].ToString();
    //                }
    //                else
    //                {
    //                    NIOROSHI_GYOUSHA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 荷卸現場CD：搬入実績レコードの現場CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_GENBACD"].ToString())))
    //                {
    //                    NIOROSHI_GENBA_CD[0] = HensyuFlg_Ari;
    //                    NIOROSHI_GENBA_CD[1] = selectedRowsEdaban[iHJ]["HANNYUU_GENBACD"].ToString();
    //                }
    //                else
    //                {
    //                    NIOROSHI_GENBA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 荷卸量：搬入実績レコードの搬入量を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iHJ]["HANNYUU_RYO"].ToString())))
    //                {
    //                    NIOROSHI_RYOU[0] = HensyuFlg_Ari;
    //                    NIOROSHI_RYOU[1] = selectedRowsEdaban[iHJ]["HANNYUU_RYO"].ToString();
    //                }
    //                else
    //                {
    //                    NIOROSHI_RYOU[0] = HensyuFlg_Nashi;
    //                }

    //                // 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
    //                // システムID
    //                Entity.SYSTEM_ID = this.int64SystemId;

    //                // 枝番
    //                Entity.SEQ = 1;

    //                // 荷卸No
    //                if (NIOROSHI_NUMBER[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_NUMBER = int.Parse(NIOROSHI_NUMBER[1]);
    //                }

    //                // 定期実績番号
    //                Entity.TEIKI_JISSEKI_NUMBER = this.int64TeikiJissekiNumber;

    //                // 行番号
    //                // TODO ダミー登録なので、考慮の必要あり
    //                Entity.ROW_NUMBER = 1;

    //                // 荷卸業者CD
    //                if (NIOROSHI_GYOUSHA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_GYOUSHA_CD = NIOROSHI_GYOUSHA_CD[1];
    //                }

    //                // 荷卸現場CD
    //                if (NIOROSHI_GENBA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_GENBA_CD = NIOROSHI_GENBA_CD[1];
    //                }

    //                // 荷卸量
    //                if (NIOROSHI_RYOU[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_RYOU = Double.Parse(NIOROSHI_RYOU[1]);
    //                }

    //                // 定期実績荷卸テーブル登録
    //                setTeikiJissekiNioroshiDao.Insert(Entity);

    //                // 定期実績荷卸テーブルエンティティの初期化
    //                Entity = new T_TEIKI_JISSEKI_NIOROSHI();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 売上_支払入力テーブル登録
    //    /// </summary>
    //    void setUrShEntry(DataRow[] selectedRowsEdaban)
    //    {
    //        // 編集領域
    //        // 編集フラグと編集項目を含んだ配列を作成する
    //        // [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
    //        // [1]＝編集有無フラグが編集ありの場合に編集される項目値
    //        // （エンティティを初期化することによる、int項目のnull値許容対応）
    //        string[] KYOTEN_CD = new string[2];
    //        string[] DENPYOU_DATE = new string[2];
    //        string[] URIAGE_DATE = new string[2];
    //        string[] TORIHIKISAKI_CD = new string[2];
    //        string[] TORIHIKISAKI_NAME = new string[2];
    //        string[] GYOUSHA_CD = new string[2];
    //        string[] GYOUSHA_NAME = new string[2];
    //        string[] GENBA_CD = new string[2];
    //        string[] GENBA_NAME = new string[2];
    //        string[] NIOROSHI_GYOUSHA_CD = new string[2];
    //        string[] NIOROSHI_GYOUSHA_NAME = new string[2];
    //        string[] NIOROSHI_GENBA_CD = new string[2];
    //        string[] NIOROSHI_GENBA_NAME = new string[2];
    //        string[] EIGYOU_TANTOUSHA_CD = new string[2];
    //        string[] EIGYOU_TANTOUSHA_NAME = new string[2];
    //        string[] NYUURYOKU_TANTOUSHA_NAME = new string[2];
    //        string[] SHARYOU_CD = new string[2];
    //        string[] SHARYOU_NAME = new string[2];
    //        string[] SHASHU_CD = new string[2];
    //        string[] SHASHU_NAME = new string[2];
    //        string[] UNPAN_GYOUSHA_CD = new string[2];
    //        string[] UNPAN_GYOUSHA_NAME = new string[2];
    //        string[] UNTENSHA_CD = new string[2];
    //        string[] UNTENSHA_NAME = new string[2];
    //        string[] CONTENA_SOUSA_CD = new string[2];
    //        string[] MANIFEST_SHURUI_CD = new string[2];
    //        string[] MANIFEST_TEHAI_CD = new string[2];
    //        string[] URIAGE_SHOUHIZEI_RATE = new string[2];
    //        string[] URIAGE_AMOUNT_TOTAL = new string[2];
    //        string[] URIAGE_TAX_SOTO = new string[2];
    //        string[] URIAGE_TAX_UCHI = new string[2];
    //        string[] URIAGE_TAX_SOTO_TOTAL = new string[2];
    //        string[] URIAGE_TAX_UCHI_TOTAL = new string[2];
    //        string[] URIAGE_ZEI_KEISAN_KBN_CD = new string[2];
    //        string[] URIAGE_ZEI_KBN_CD = new string[2];
    //        string[] URIAGE_TORIHIKI_KBN_CD = new string[2];

    //        // 編集ありフラグ
    //        string HensyuFlg_Ari = "1";
    //        string HensyuFlg_Nashi = "0";

    //        // 売上_支払入力テーブルエンティティの初期化
    //        T_UR_SH_ENTRY Entity = new T_UR_SH_ENTRY();

    //        // 各項目の編集
    //        // 配車ヘッダレコードの情報を取得する
    //        for (int i = 0; i < selectedRowsEdaban.Length; i++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[i]["NODE_EDABAN"].ToString()) == NODE_EDABAN_HAISHA)
    //            { // 配車ヘッダレコードの場合

    //                // 拠点CD：配車ヘッダレコードの拠点CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["KYOTEN_CD"].ToString())))
    //                {
    //                    KYOTEN_CD[0] = HensyuFlg_Ari;
    //                    KYOTEN_CD[1] = selectedRowsEdaban[i]["KYOTEN_CD"].ToString();
    //                }
    //                else
    //                {
    //                    KYOTEN_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 伝票日付：配車ヘッダレコードの伝票番号をキーに定期配車入力テーブルから取得
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
    //                { // 配車ヘッダレコードの伝票番号が存在する場合
    //                    if (getTeikiHaishaEntry(int.Parse(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())) != 0)
    //                    { // 配車ヘッダレコードの伝票番号に紐付く定期配車入力テーブルの伝票日付が存在する場合
    //                        DENPYOU_DATE[0] = HensyuFlg_Ari;
    //                        DENPYOU_DATE[1] = this.dtDenpyouDate.ToString();
    //                    }
    //                    else
    //                    {
    //                        DENPYOU_DATE[0] = HensyuFlg_Nashi;
    //                    }
    //                }

    //                // 売上日付：配車ヘッダレコードの作業日を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString())))
    //                {
    //                    URIAGE_DATE[0] = HensyuFlg_Ari;
    //                    URIAGE_DATE[1] = selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString();
    //                }
    //                else
    //                {
    //                    URIAGE_DATE[0] = HensyuFlg_Nashi;
    //                }

    //                // 入力担当者名：配車ヘッダレコードの作成者を編集する
    //                // 配車ヘッダレコード～搬入実績レコードまで、xml単位では全て同じ作成者が登録されているので、
    //                // 代表して配車ヘッダレコードから編集している。
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["CREATE_USER"].ToString())))
    //                {
    //                    NYUURYOKU_TANTOUSHA_NAME[0] = HensyuFlg_Ari;
    //                    NYUURYOKU_TANTOUSHA_NAME[1] = selectedRowsEdaban[i]["CREATE_USER"].ToString();
    //                }
    //                else
    //                {
    //                    NYUURYOKU_TANTOUSHA_NAME[0] = HensyuFlg_Nashi;
    //                }

    //                // 車輌名：配車ヘッダレコードの車輌名を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["SHARYOU_NAME"].ToString())))
    //                {
    //                    SHARYOU_NAME[0] = HensyuFlg_Ari;
    //                    SHARYOU_NAME[1] = selectedRowsEdaban[i]["SHARYOU_NAME"].ToString();
    //                }
    //                else
    //                {
    //                    SHARYOU_NAME[0] = HensyuFlg_Nashi;
    //                }

    //                // 車種CD：配車ヘッダレコードの車種CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["SHASHU_CD"].ToString())))
    //                {
    //                    SHASHU_CD[0] = HensyuFlg_Ari;
    //                    SHASHU_CD[1] = selectedRowsEdaban[i]["SHASHU_CD"].ToString();
    //                }
    //                else
    //                {
    //                    SHASHU_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 車種名：配車ヘッダレコードの車種名を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["SHASHU_NAME"].ToString())))
    //                {
    //                    SHASHU_NAME[0] = HensyuFlg_Ari;
    //                    SHASHU_NAME[1] = selectedRowsEdaban[i]["SHASHU_NAME"].ToString();
    //                }
    //                else
    //                {
    //                    SHASHU_NAME[0] = HensyuFlg_Nashi;
    //                }

    //                // 運転者CD：配車ヘッダレコードの運転者CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_UNTENSHA_CD"].ToString())))
    //                {
    //                    UNTENSHA_CD[0] = HensyuFlg_Ari;
    //                    UNTENSHA_CD[1] = selectedRowsEdaban[i]["HAISHA_UNTENSHA_CD"].ToString();
    //                }
    //                else
    //                {
    //                    UNTENSHA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 運転者名：配車ヘッダレコードの運転者名を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["UNTENSHA_NAME"].ToString())))
    //                {
    //                    UNTENSHA_NAME[0] = HensyuFlg_Ari;
    //                    UNTENSHA_NAME[1] = selectedRowsEdaban[i]["UNTENSHA_NAME"].ToString();
    //                }
    //                else
    //                {
    //                    UNTENSHA_NAME[0] = HensyuFlg_Nashi;
    //                }

    //                // 配車ヘッダレコードの伝票番号をキーに受付(収集)入力テーブルから取得
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
    //                { // 配車ヘッダレコードの伝票番号が存在する場合
    //                    if (getUketsukeSsEntry(int.Parse(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())) != 0)
    //                    { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルのデータが存在した場合

    //                        // 取引先CD
    //                        if (!(string.IsNullOrEmpty(this.strTorihikisakiCd)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの取引先CDが存在する場合
    //                            TORIHIKISAKI_CD[0] = HensyuFlg_Ari;
    //                            TORIHIKISAKI_CD[1] = this.strTorihikisakiCd.ToString();
    //                        }
    //                        else
    //                        {
    //                            TORIHIKISAKI_CD[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 取引先名
    //                        if (!(string.IsNullOrEmpty(this.strTorihikisakiName)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの取引先名が存在する場合
    //                            TORIHIKISAKI_NAME[0] = HensyuFlg_Ari;
    //                            TORIHIKISAKI_NAME[1] = this.strTorihikisakiName.ToString();
    //                        }
    //                        else
    //                        {
    //                            TORIHIKISAKI_NAME[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 業者名
    //                        if (!(string.IsNullOrEmpty(this.strGyoushaName)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの業者名が存在する場合
    //                            GYOUSHA_NAME[0] = HensyuFlg_Ari;
    //                            GYOUSHA_NAME[1] = this.strGyoushaName.ToString();
    //                        }
    //                        else
    //                        {
    //                            GYOUSHA_NAME[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 現場名
    //                        if (!(string.IsNullOrEmpty(this.strGenbaName)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの現場名が存在する場合
    //                            GENBA_NAME[0] = HensyuFlg_Ari;
    //                            GENBA_NAME[1] = this.strGenbaName.ToString();
    //                        }
    //                        else
    //                        {
    //                            GENBA_NAME[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 荷卸業者CD
    //                        if (!(string.IsNullOrEmpty(this.strNioroshiGyoushaCd)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの現場名が存在する場合
    //                            NIOROSHI_GYOUSHA_CD[0] = HensyuFlg_Ari;
    //                            NIOROSHI_GYOUSHA_CD[1] = this.strNioroshiGyoushaCd.ToString();
    //                        }
    //                        else
    //                        {
    //                            NIOROSHI_GYOUSHA_CD[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 荷卸業者名
    //                        if (!(string.IsNullOrEmpty(this.strNioroshiGyoushaName)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの荷卸業者名が存在する場合
    //                            NIOROSHI_GYOUSHA_NAME[0] = HensyuFlg_Ari;
    //                            NIOROSHI_GYOUSHA_NAME[1] = this.strNioroshiGyoushaName.ToString();
    //                        }
    //                        else
    //                        {
    //                            NIOROSHI_GYOUSHA_NAME[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 荷卸現場CD
    //                        if (!(string.IsNullOrEmpty(this.strNioroshiGenbaCd)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの荷卸現場CDが存在する場合
    //                            NIOROSHI_GENBA_CD[0] = HensyuFlg_Ari;
    //                            NIOROSHI_GENBA_CD[1] = this.strNioroshiGenbaCd.ToString();
    //                        }
    //                        else
    //                        {
    //                            NIOROSHI_GENBA_CD[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 荷卸現場名
    //                        if (!(string.IsNullOrEmpty(this.strNioroshiGenbaName)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの荷卸現場名が存在する場合
    //                            NIOROSHI_GENBA_NAME[0] = HensyuFlg_Ari;
    //                            NIOROSHI_GENBA_NAME[1] = this.strNioroshiGenbaName.ToString();
    //                        }
    //                        else
    //                        {
    //                            NIOROSHI_GENBA_NAME[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 営業担当者CD
    //                        if (!(string.IsNullOrEmpty(this.strEigyouTantoushaCd)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの営業担当者CDが存在する場合
    //                            EIGYOU_TANTOUSHA_CD[0] = HensyuFlg_Ari;
    //                            EIGYOU_TANTOUSHA_CD[1] = this.strEigyouTantoushaCd.ToString();
    //                        }
    //                        else
    //                        {
    //                            EIGYOU_TANTOUSHA_CD[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 営業担当者名
    //                        if (!(string.IsNullOrEmpty(this.strEigyouTantoushaName)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの営業担当者名が存在する場合
    //                            EIGYOU_TANTOUSHA_NAME[0] = HensyuFlg_Ari;
    //                            EIGYOU_TANTOUSHA_NAME[1] = this.strEigyouTantoushaName.ToString();
    //                        }
    //                        else
    //                        {
    //                            EIGYOU_TANTOUSHA_NAME[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 運搬業者CD
    //                        if (!(string.IsNullOrEmpty(this.strUnpanGyoushaCd)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの運搬業者CDが存在する場合
    //                            UNPAN_GYOUSHA_CD[0] = HensyuFlg_Ari;
    //                            UNPAN_GYOUSHA_CD[1] = this.strUnpanGyoushaCd.ToString();
    //                        }
    //                        else
    //                        {
    //                            UNPAN_GYOUSHA_CD[0] = HensyuFlg_Nashi;
    //                        }

    //                        // 運搬業者名
    //                        if (!(string.IsNullOrEmpty(this.strUnpanGyoushaName)))
    //                        { // 配車ヘッダレコードの伝票番号に紐付く受付(収集)入力テーブルの運搬業者名が存在する場合
    //                            UNPAN_GYOUSHA_NAME[0] = HensyuFlg_Ari;
    //                            UNPAN_GYOUSHA_NAME[1] = this.strUnpanGyoushaName.ToString();
    //                        }
    //                        else
    //                        {
    //                            UNPAN_GYOUSHA_NAME[0] = HensyuFlg_Nashi;
    //                        }

    //                        // コンテナ操作CD
    //                        // 数値項目について、getUketsukeSsEntry(受付(収集)入力テーブル取得処理)からデータが取得できれば
    //                        // 初期化で「0」が入るのでNullOrEmpty判定は実施不要。（以下、数値項目も同様）
    //                        CONTENA_SOUSA_CD[0] = HensyuFlg_Ari;
    //                        CONTENA_SOUSA_CD[1] = this.int16ContenaSousaCd.ToString();

    //                        // マニフェスト種類CD
    //                        MANIFEST_SHURUI_CD[0] = HensyuFlg_Ari;
    //                        MANIFEST_SHURUI_CD[1] = this.int16ManifestShuruiCd.ToString();

    //                        // マニフェスト手配CD
    //                        MANIFEST_TEHAI_CD[0] = HensyuFlg_Ari;
    //                        MANIFEST_TEHAI_CD[1] = this.int16ManifestTehaiCd.ToString();

    //                        // 売上消費税率   
    //                        URIAGE_SHOUHIZEI_RATE[0] = HensyuFlg_Ari;
    //                        URIAGE_SHOUHIZEI_RATE[1] = this.decShouhizeiRate.ToString();

    //                        // 売上金額合計   
    //                        URIAGE_AMOUNT_TOTAL[0] = HensyuFlg_Ari;
    //                        URIAGE_AMOUNT_TOTAL[1] = this.moneyKingakuTotal.ToString();

    //                        // 売上伝票毎消費税外税   
    //                        URIAGE_TAX_SOTO[0] = HensyuFlg_Ari;
    //                        URIAGE_TAX_SOTO[1] = this.moneyTaxSoto.ToString();

    //                        // 売上伝票毎消費税内税   
    //                        URIAGE_TAX_UCHI[0] = HensyuFlg_Ari;
    //                        URIAGE_TAX_UCHI[1] = this.moneyTaxUchi.ToString();

    //                        // 売上明細毎消費税外税合計   
    //                        URIAGE_TAX_SOTO_TOTAL[0] = HensyuFlg_Ari;
    //                        URIAGE_TAX_SOTO_TOTAL[1] = this.moneyTaxSotoToal.ToString();

    //                        // 売上明細毎消費税内税合計   
    //                        URIAGE_TAX_UCHI_TOTAL[0] = HensyuFlg_Ari;
    //                        URIAGE_TAX_UCHI_TOTAL[1] = this.moneyTaxUchiToal.ToString();

    //                        // 受付(収集)入力テーブルの取引先CDをキーに取引先_請求情報マスタから取得
    //                        if (!(string.IsNullOrEmpty(this.strTorihikisakiCd)))
    //                        { // 受付(収集)入力テーブルの取引先CDが存在する場合
    //                            if (getMtorihikisakiSeikyuu(this.strTorihikisakiCd) != 0)
    //                            { // 取得した取引先CDをキーに取引先_請求情報マスタから取得できた場合

    //                                // 売上税計算区分CD
    //                                if (!(string.IsNullOrEmpty(this.int16ZeiKeisanKbnCd.ToString())))
    //                                { // 取引先_請求情報マスタの売上税計算区分CDが存在する場合
    //                                    URIAGE_ZEI_KEISAN_KBN_CD[0] = HensyuFlg_Ari;
    //                                    URIAGE_ZEI_KEISAN_KBN_CD[1] = int16ZeiKeisanKbnCd.ToString();
    //                                }
    //                                else
    //                                {
    //                                    URIAGE_ZEI_KEISAN_KBN_CD[0] = HensyuFlg_Nashi;
    //                                }

    //                                // 売上税区分CD
    //                                if (!(string.IsNullOrEmpty(this.int16ZeiKbnCd.ToString())))
    //                                { // 取引先_請求情報マスタの売上税区分CDが存在する場合
    //                                    URIAGE_ZEI_KBN_CD[0] = HensyuFlg_Ari;
    //                                    URIAGE_ZEI_KBN_CD[1] = int16ZeiKbnCd.ToString();
    //                                }
    //                                else
    //                                {
    //                                    URIAGE_ZEI_KBN_CD[0] = HensyuFlg_Nashi;
    //                                }

    //                                // 売上取引区分CD
    //                                if (!(string.IsNullOrEmpty(this.int16TorihikiKbnCd.ToString())))
    //                                { // 取引先_請求情報マスタの売上取引区分CDが存在する場合
    //                                    URIAGE_TORIHIKI_KBN_CD[0] = HensyuFlg_Ari;
    //                                    URIAGE_TORIHIKI_KBN_CD[1] = int16TorihikiKbnCd.ToString();
    //                                }
    //                                else
    //                                {
    //                                    URIAGE_TORIHIKI_KBN_CD[0] = HensyuFlg_Nashi;
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        // 現場実績レコードの情報を取得する
    //        for (int iGJ = 0; iGJ < selectedRowsEdaban.Length; iGJ++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[iGJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_GENBAJISSEKI)
    //            { // 現場実績レコードの場合

    //                // 業者CD：現場実績レコードの業者CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GYOUSHACD"].ToString())))
    //                {
    //                    GYOUSHA_CD[0] = HensyuFlg_Ari;
    //                    GYOUSHA_CD[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GYOUSHACD"].ToString();
    //                }
    //                else
    //                {
    //                    GYOUSHA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 現場CD：現場実績レコードの現場CDを編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString())))
    //                {
    //                    GENBA_CD[0] = HensyuFlg_Ari;
    //                    GENBA_CD[1] = selectedRowsEdaban[iGJ]["GENBA_JISSEKI_GENBACD"].ToString();
    //                }
    //                else
    //                {
    //                    GENBA_CD[0] = HensyuFlg_Nashi;
    //                }

    //                // 現場実績レコードの出庫Noに紐付く出庫実績レコードの各項目を編集する
    //                // 出庫実績レコードの情報を取得する
    //                for (int iSJ = 0; iSJ < selectedRowsEdaban.Length; iSJ++)
    //                { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //                    if (Int64.Parse(selectedRowsEdaban[iSJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_SHUKKO)
    //                    { // 出庫実績レコードの場合
    //                        if (int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_SHUKKONO"].ToString()) == int.Parse(selectedRowsEdaban[iSJ]["SHUKKO_NO"].ToString()))
    //                        { // 現場実績レコードの出庫Noと出庫実績レコードのレコード番号が紐付いた場合（実質紐付くのは１レコードのみ）

    //                            // 車輌CD：出庫実績レコードの車輌コードを編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iSJ]["SHUKKO_SHARYOUCD"].ToString())))
    //                            {
    //                                SHARYOU_CD[0] = HensyuFlg_Ari;
    //                                SHARYOU_CD[1] = selectedRowsEdaban[iSJ]["SHUKKO_SHARYOUCD"].ToString();
    //                            }
    //                            else
    //                            {
    //                                SHARYOU_CD[0] = HensyuFlg_Nashi;
    //                            }
    //                        }
    //                    }
    //                }

    //                // 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
    //                // 売上_支払明細の編集用の退避
    //                this.int64SystemId = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));
    //                this.int64UrShNumber = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));

    //                // システムID
    //                Entity.SYSTEM_ID = this.int64SystemId;

    //                // 枝番
    //                Entity.SEQ = 1;

    //                // 拠点CD
    //                if (KYOTEN_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.KYOTEN_CD = Int16.Parse(KYOTEN_CD[1]);
    //                }

    //                // 売上／支払番号
    //                Entity.UR_SH_NUMBER = this.int64UrShNumber;

    //                // 日連番　TODO ダミーで設定
    //                Entity.DATE_NUMBER = 0;

    //                // 年連番　TODO ダミーで設定
    //                Entity.YEAR_NUMBER = 0;

    //                // 確定区分　TODO ダミーで設定
    //                Entity.KAKUTEI_KBN = this.int16Kakutei_Kbn;

    //                // 伝票日付
    //                if (DENPYOU_DATE[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.DENPYOU_DATE = DateTime.Parse(DENPYOU_DATE[1]);
    //                }

    //                // 売上日付
    //                if (URIAGE_DATE[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_DATE = DateTime.Parse(URIAGE_DATE[1]);
    //                }

    //                // 取引先CD
    //                if (TORIHIKISAKI_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.TORIHIKISAKI_CD = TORIHIKISAKI_CD[1];
    //                }

    //                // 取引先名
    //                if (TORIHIKISAKI_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.TORIHIKISAKI_NAME = TORIHIKISAKI_NAME[1];
    //                }

    //                // 業者CD
    //                if (GYOUSHA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.GYOUSHA_CD = GYOUSHA_CD[1];
    //                }

    //                // 業者名
    //                if (GYOUSHA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.GYOUSHA_NAME = GYOUSHA_NAME[1];
    //                }

    //                // 現場CD
    //                if (GENBA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.GENBA_CD = GENBA_CD[1];
    //                }

    //                // 現場名
    //                if (GENBA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.GENBA_NAME = GENBA_NAME[1];
    //                }

    //                // 荷卸業者CD
    //                if (NIOROSHI_GYOUSHA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_GYOUSHA_CD = NIOROSHI_GYOUSHA_CD[1];
    //                }

    //                // 荷卸業者名
    //                if (NIOROSHI_GYOUSHA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_GYOUSHA_NAME = NIOROSHI_GYOUSHA_NAME[1];
    //                }

    //                // 荷卸現場CD
    //                if (NIOROSHI_GENBA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_GENBA_CD = NIOROSHI_GENBA_CD[1];
    //                }

    //                // 荷卸現場名
    //                if (NIOROSHI_GENBA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NIOROSHI_GENBA_NAME = NIOROSHI_GENBA_NAME[1];
    //                }

    //                // 営業担当者CD
    //                if (EIGYOU_TANTOUSHA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.EIGYOU_TANTOUSHA_CD = EIGYOU_TANTOUSHA_CD[1];
    //                }

    //                // 営業担当者名
    //                if (EIGYOU_TANTOUSHA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.EIGYOU_TANTOUSHA_NAME = EIGYOU_TANTOUSHA_NAME[1];
    //                }

    //                // 入力担当者名
    //                if (NYUURYOKU_TANTOUSHA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.NYUURYOKU_TANTOUSHA_NAME = NYUURYOKU_TANTOUSHA_NAME[1];
    //                }

    //                // 車輌CD
    //                if (SHARYOU_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHARYOU_CD = SHARYOU_CD[1];
    //                }

    //                // 車輌名
    //                if (SHARYOU_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHARYOU_NAME = SHARYOU_NAME[1];
    //                }

    //                // 車種CD
    //                if (SHASHU_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHASHU_CD = SHASHU_CD[1];
    //                }

    //                // 車種名
    //                if (SHASHU_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.SHASHU_NAME = SHASHU_NAME[1];
    //                }

    //                // 運搬業者CD
    //                if (UNPAN_GYOUSHA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.UNPAN_GYOUSHA_CD = UNPAN_GYOUSHA_CD[1];
    //                }

    //                // 運搬業者名
    //                if (UNPAN_GYOUSHA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.UNPAN_GYOUSHA_NAME = UNPAN_GYOUSHA_NAME[1];
    //                }

    //                // 運転者CD
    //                if (UNTENSHA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.UNTENSHA_CD = UNTENSHA_CD[1];
    //                }

    //                // 運転者名
    //                if (UNTENSHA_NAME[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.UNTENSHA_NAME = UNTENSHA_NAME[1];
    //                }

    //                // コンテナ操作CD
    //                if (CONTENA_SOUSA_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.CONTENA_SOUSA_CD = Int16.Parse(CONTENA_SOUSA_CD[1]);
    //                }

    //                // マニフェスト種類CD
    //                if (MANIFEST_SHURUI_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.MANIFEST_SHURUI_CD = Int16.Parse(MANIFEST_SHURUI_CD[1]);
    //                }

    //                // マニフェスト手配CD
    //                if (MANIFEST_TEHAI_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.MANIFEST_TEHAI_CD = Int16.Parse(MANIFEST_TEHAI_CD[1]);
    //                }

    //                // 売上消費税率
    //                if (URIAGE_SHOUHIZEI_RATE[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_SHOUHIZEI_RATE = Decimal.Parse(URIAGE_SHOUHIZEI_RATE[1]);
    //                }

    //                // 売上金額合計
    //                if (URIAGE_AMOUNT_TOTAL[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_AMOUNT_TOTAL = Decimal.Parse(URIAGE_AMOUNT_TOTAL[1]);
    //                }

    //                // 売上伝票毎消費税外税
    //                if (URIAGE_TAX_SOTO[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_TAX_SOTO = Decimal.Parse(URIAGE_TAX_SOTO[1]);
    //                }

    //                // 売上伝票毎消費税内税
    //                if (URIAGE_TAX_UCHI[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_TAX_UCHI = Decimal.Parse(URIAGE_TAX_UCHI[1]);
    //                }

    //                // 売上明細毎消費税外税合計
    //                if (URIAGE_TAX_SOTO_TOTAL[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_TAX_SOTO_TOTAL = Decimal.Parse(URIAGE_TAX_SOTO_TOTAL[1]);
    //                }

    //                // 売上明細毎消費税内税合計
    //                if (URIAGE_TAX_UCHI_TOTAL[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_TAX_UCHI_TOTAL = Decimal.Parse(URIAGE_TAX_UCHI_TOTAL[1]);
    //                }

    //                // 売上税計算区分CD
    //                if (URIAGE_ZEI_KEISAN_KBN_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_ZEI_KEISAN_KBN_CD = Int16.Parse(URIAGE_ZEI_KEISAN_KBN_CD[1]);
    //                }

    //                // 売上税区分CD
    //                if (URIAGE_ZEI_KBN_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_ZEI_KBN_CD = Int16.Parse(URIAGE_ZEI_KBN_CD[1]);
    //                }

    //                // 売上取引区分CD
    //                if (URIAGE_TORIHIKI_KBN_CD[0] == HensyuFlg_Ari)
    //                { // 編集ありの場合
    //                    Entity.URIAGE_TORIHIKI_KBN_CD = Int16.Parse(URIAGE_TORIHIKI_KBN_CD[1]);
    //                }

    //                // TODO 以下はシステムで自動的に編集される項目なので、後に削除が必要
    //                Entity.CREATE_USER = "TEST_USER";
    //                Entity.CREATE_DATE = DateTime.Now;
    //                Entity.SEARCH_CREATE_DATE = "TEST_DATE";
    //                Entity.CREATE_PC = "TEST_PC";
    //                Entity.UPDATE_USER = "TEST_USER";
    //                Entity.UPDATE_DATE = DateTime.Now;
    //                Entity.SEARCH_UPDATE_DATE = "TEST_DATE";
    //                Entity.UPDATE_PC = "TEST_PC";
    //                // TODO ここまで

    //                // 売上_支払入力テーブル登録
    //                setUrShEntryDao.Insert(Entity);

    //                // 売上_支払入力テーブルエンティティの初期化
    //                Entity = new T_UR_SH_ENTRY();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 売上_支払明細テーブル登録
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void setUrShDetail(DataRow[] selectedRowsEdaban)
    //    {
    //        // 編集領域
    //        // 編集フラグと編集項目を含んだ配列を作成する
    //        // [0]＝編集有無フラグ(1＝編集あり、1≠NullOrEmptyなので編集なし)
    //        // [1]＝編集有無フラグが編集ありの場合に編集される項目値
    //        // （エンティティを初期化することによる、int項目のnull値許容対応）
    //        string[] ROW_NUMBER = new string[2];
    //        string[] URIAGE_DATE = new string[2];
    //        string[] DENPYOU_KBN_CD = new string[2];
    //        string[] HINMEI_CD = new string[2];
    //        string[] HINMEI_NAME = new string[2];
    //        string[] SUURYOU = new string[2];
    //        string[] UNIT_CD = new string[2];
    //        string[] TANKA = new string[2];
    //        string[] KINGAKU = new string[2];
    //        string[] TAX_SOTO = new string[2];
    //        string[] TAX_UCHI = new string[2];
    //        string[] HINMEI_ZEI_KBN_CD = new string[2];
    //        string[] HINMEI_KINGAKU = new string[2];
    //        string[] HINMEI_TAX_SOTO = new string[2];
    //        string[] HINMEI_TAX_UCHI = new string[2];
    //        string[] MEISAI_BIKOU = new string[2];

    //        // 編集ありフラグ
    //        string HensyuFlg_Ari = "1";
    //        string HensyuFlg_Nashi = "0";

    //        // 売上_支払明細テーブルエンティティの初期化
    //        T_UR_SH_DETAIL Entity = new T_UR_SH_DETAIL();

    //        // 伝票番号の保存領域
    //        int intHaishaDenpyouNo = 0;

    //        // 各項目の編集
    //        // 配車ヘッダレコードの情報を取得する
    //        for (int i = 0; i < selectedRowsEdaban.Length; i++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[i]["NODE_EDABAN"].ToString()) == NODE_EDABAN_HAISHA)
    //            { // 配車ヘッダレコードの場合

    //                // 売上支払日付：配車ヘッダレコードの作業日を編集する
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString())))
    //                {
    //                    URIAGE_DATE[0] = HensyuFlg_Ari;
    //                    URIAGE_DATE[1] = selectedRowsEdaban[i]["HAISHA_SAGYOU_DATE"].ToString();
    //                }
    //                else
    //                {
    //                    URIAGE_DATE[0] = HensyuFlg_Nashi;
    //                }

    //                // 後続の受付(収集)明細テーブルから項目を取得する際のキー(伝票番号)を保存
    //                if (!(string.IsNullOrEmpty(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString())))
    //                { // 配車ヘッダレコードの伝票番号が存在する場合
    //                    intHaishaDenpyouNo = int.Parse(selectedRowsEdaban[i]["HAISHA_DENPYOU_NO"].ToString());
    //                }
    //            }
    //        }

    //        // 現場実績レコードの情報を取得する
    //        for (int iGJ = 0; iGJ < selectedRowsEdaban.Length; iGJ++)
    //        { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //            if (Int64.Parse(selectedRowsEdaban[iGJ]["NODE_EDABAN"].ToString()) == NODE_EDABAN_GENBAJISSEKI)
    //            { // 現場実績レコードの場合

    //                // 現場実績レコードのレコード番号に紐付く現場明細レコードの各項目を編集する
    //                // 現場明細レコードの情報を取得する
    //                for (int iGM = 0; iGM < selectedRowsEdaban.Length; iGM++)
    //                { // 枝番(登録対象)で抽出したモバイル将軍用データ取込画面専用テーブル(selectedRowsEdaban)内のレコードが処理対象
    //                    if (Int64.Parse(selectedRowsEdaban[iGM]["NODE_EDABAN"].ToString()) == NODE_EDABAN_DETAIL)
    //                    { // 現場明細レコードの場合
    //                        if (int.Parse(selectedRowsEdaban[iGJ]["GENBA_JISSEKI_NO"].ToString()) == int.Parse(selectedRowsEdaban[iGM]["GENBA_DETAIL_GENBA_NO"].ToString()))
    //                        { // 現場実績レコードのレコード番号と現場明細レコードの現場Noが紐付いた場合(現場実績：現場明細＝１：Ｎで紐付く)

    //                            // 行番号：現場明細レコードのレコード番号を編集する
    //                            if (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString())))
    //                            {
    //                                ROW_NUMBER[0] = HensyuFlg_Ari;
    //                                ROW_NUMBER[1] = selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString();
    //                            }
    //                            else
    //                            {
    //                                ROW_NUMBER[0] = HensyuFlg_Nashi;
    //                            }

    //                            // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号をキーに受付(収集)明細テーブルから取得
    //                            if ((intHaishaDenpyouNo != 0) && (!(string.IsNullOrEmpty(selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString()))))
    //                            { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号が存在する場合　
    //                                if (getUketsukeSsDetail(intHaishaDenpyouNo, Int16.Parse(selectedRowsEdaban[iGM]["GENBA_DETAIL_NO"].ToString())) != 0)
    //                                { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルのデータが存在した場合

    //                                    // 伝票区分CD
    //                                    if (!(string.IsNullOrEmpty(this.int16DenpyouKbnCd.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの伝票区分CDが存在する場合
    //                                        DENPYOU_KBN_CD[0] = HensyuFlg_Ari;
    //                                        DENPYOU_KBN_CD[1] = this.int16DenpyouKbnCd.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        DENPYOU_KBN_CD[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 品名CD
    //                                    if (!(string.IsNullOrEmpty(this.strHinmeiCd)))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの品名CDが存在する場合
    //                                        HINMEI_CD[0] = HensyuFlg_Ari;
    //                                        HINMEI_CD[1] = this.strHinmeiCd;
    //                                    }
    //                                    else
    //                                    {
    //                                        HINMEI_CD[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 品名
    //                                    if (!(string.IsNullOrEmpty(this.strHinmeiName)))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの品名が存在する場合
    //                                        HINMEI_NAME[0] = HensyuFlg_Ari;
    //                                        HINMEI_NAME[1] = this.strHinmeiName;
    //                                    }
    //                                    else
    //                                    {
    //                                        HINMEI_NAME[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 数量
    //                                    if (!(string.IsNullOrEmpty(this.dblSuuryou.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの数量が存在する場合
    //                                        SUURYOU[0] = HensyuFlg_Ari;
    //                                        SUURYOU[1] = this.dblSuuryou.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        SUURYOU[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 単位CD
    //                                    if (!(string.IsNullOrEmpty(this.int16UnitCd.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの単位CDが存在する場合
    //                                        UNIT_CD[0] = HensyuFlg_Ari;
    //                                        UNIT_CD[1] = this.int16UnitCd.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        UNIT_CD[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 単価
    //                                    if (!(string.IsNullOrEmpty(this.moneyTanka.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの単価が存在する場合
    //                                        TANKA[0] = HensyuFlg_Ari;
    //                                        TANKA[1] = this.moneyTanka.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        TANKA[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 金額
    //                                    if (!(string.IsNullOrEmpty(this.moneyKingaku.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの金額が存在する場合
    //                                        KINGAKU[0] = HensyuFlg_Ari;
    //                                        KINGAKU[1] = this.moneyKingaku.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        KINGAKU[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 消費税外税
    //                                    if (!(string.IsNullOrEmpty(this.moneyTaxSotoDetail.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの消費税外税が存在する場合
    //                                        TAX_SOTO[0] = HensyuFlg_Ari;
    //                                        TAX_SOTO[1] = this.moneyTaxSotoDetail.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        TAX_SOTO[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 消費税内税
    //                                    if (!(string.IsNullOrEmpty(this.moneyTaxUchiDetail.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの消費税内税が存在する場合
    //                                        TAX_UCHI[0] = HensyuFlg_Ari;
    //                                        TAX_UCHI[1] = this.moneyTaxUchiDetail.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        TAX_UCHI[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 品名別税区分CD
    //                                    if (!(string.IsNullOrEmpty(this.int16HinmeiZeiKbnCd.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの品名別税区分CDが存在する場合
    //                                        HINMEI_ZEI_KBN_CD[0] = HensyuFlg_Ari;
    //                                        HINMEI_ZEI_KBN_CD[1] = this.int16HinmeiZeiKbnCd.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        HINMEI_ZEI_KBN_CD[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 品名別金額    
    //                                    if (!(string.IsNullOrEmpty(this.moneyHinmeiKingaku.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの品名別金額が存在する場合
    //                                        HINMEI_KINGAKU[0] = HensyuFlg_Ari;
    //                                        HINMEI_KINGAKU[1] = this.moneyHinmeiKingaku.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        HINMEI_KINGAKU[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 品名別消費税外税    
    //                                    if (!(string.IsNullOrEmpty(this.moneyHinmeiTaxSoto.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの品名別消費税外税が存在する場合
    //                                        HINMEI_TAX_SOTO[0] = HensyuFlg_Ari;
    //                                        HINMEI_TAX_SOTO[1] = this.moneyHinmeiTaxSoto.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        HINMEI_TAX_SOTO[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 品名別消費税内税    
    //                                    if (!(string.IsNullOrEmpty(this.moneyHinmeiTaxUchi.ToString())))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの品名別消費税内税が存在する場合
    //                                        HINMEI_TAX_UCHI[0] = HensyuFlg_Ari;
    //                                        HINMEI_TAX_UCHI[1] = this.moneyHinmeiTaxUchi.ToString();
    //                                    }
    //                                    else
    //                                    {
    //                                        HINMEI_TAX_UCHI[0] = HensyuFlg_Nashi;
    //                                    }

    //                                    // 明細備考
    //                                    if (!(string.IsNullOrEmpty(this.strMeisaiBikou)))
    //                                    { // 配車ヘッダレコードの伝票番号と現場明細レコードのレコード番号に紐付く受付(収集)明細テーブルの明細備考が存在する場合
    //                                        MEISAI_BIKOU[0] = HensyuFlg_Ari;
    //                                        MEISAI_BIKOU[1] = this.strMeisaiBikou;
    //                                    }
    //                                    else
    //                                    {
    //                                        MEISAI_BIKOU[0] = HensyuFlg_Nashi;
    //                                    }
    //                                }
    //                            }

    //                            // 各項目の登録前の編集(編集なしの場合はエンティティが初期化されているのでnullのまま)
    //                            // システムID
    //                            Entity.SYSTEM_ID = this.int64SystemId;

    //                            // 枝番
    //                            Entity.SEQ = 1;

    //                            // 明細システムID
    //                            Entity.DETAIL_SYSTEM_ID = this.CommonDBAccessor.createSystemId(Int16.Parse(DENSHU_KBN.TEIKI_JISSEKI.GetHashCode().ToString()));

    //                            // 売上／支払番号
    //                            Entity.UR_SH_NUMBER = this.int64UrShNumber;

    //                            // 行番号
    //                            if (ROW_NUMBER[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.ROW_NO = Int16.Parse(ROW_NUMBER[1]);
    //                            }

    //                            // 確定区分　TODO ダミーで設定
    //                            Entity.KAKUTEI_KBN = this.int16Kakutei_Kbn;

    //                            // 売上支払日付
    //                            if (URIAGE_DATE[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.URIAGESHIHARAI_DATE = DateTime.Parse(URIAGE_DATE[1]);
    //                            }

    //                            // 伝票区分CD
    //                            if (DENPYOU_KBN_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.DENPYOU_KBN_CD = Int16.Parse(DENPYOU_KBN_CD[1]);
    //                            }

    //                            // 品名CD
    //                            if (HINMEI_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.HINMEI_CD = HINMEI_CD[1];
    //                            }

    //                            // 品名
    //                            if (HINMEI_NAME[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.HINMEI_NAME = HINMEI_NAME[1];
    //                            }

    //                            // 数量
    //                            if (SUURYOU[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.SUURYOU = Double.Parse(SUURYOU[1]);
    //                            }

    //                            // 単位CD
    //                            if (UNIT_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.UNIT_CD = Int16.Parse(UNIT_CD[1]);
    //                            }

    //                            // 単価
    //                            if (TANKA[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.TANKA = Decimal.Parse(TANKA[1]);
    //                            }

    //                            // 金額
    //                            if (KINGAKU[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.KINGAKU = Decimal.Parse(KINGAKU[1]);
    //                            }

    //                            // 消費税外税
    //                            if (TAX_SOTO[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.TAX_SOTO = Decimal.Parse(TAX_SOTO[1]);
    //                            }

    //                            // 消費税内税
    //                            if (TAX_UCHI[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.TAX_UCHI = Decimal.Parse(TAX_UCHI[1]);
    //                            }

    //                            // 品名別税区分CD
    //                            if (HINMEI_ZEI_KBN_CD[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.HINMEI_ZEI_KBN_CD = Int16.Parse(HINMEI_ZEI_KBN_CD[1]);
    //                            }

    //                            // 品名別金額
    //                            if (HINMEI_KINGAKU[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.HINMEI_KINGAKU = Decimal.Parse(HINMEI_KINGAKU[1]);
    //                            }

    //                            // 品名別消費税外税
    //                            if (HINMEI_TAX_SOTO[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.HINMEI_TAX_SOTO = Decimal.Parse(HINMEI_TAX_SOTO[1]);
    //                            }

    //                            // 品名別消費税内税 
    //                            if (HINMEI_TAX_UCHI[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.HINMEI_TAX_UCHI = Decimal.Parse(HINMEI_TAX_UCHI[1]);
    //                            }

    //                            // 明細備考 
    //                            if (MEISAI_BIKOU[0] == HensyuFlg_Ari)
    //                            { // 編集ありの場合
    //                                Entity.MEISAI_BIKOU = MEISAI_BIKOU[1];
    //                            }

    //                            // 売上_支払明細テーブル登録
    //                            setUrShDetailDao.Insert(Entity);

    //                            // 売上_支払明細テーブルエンティティの初期化
    //                            Entity = new T_UR_SH_DETAIL();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// XML入力先ファイルパスの取得
    //    /// </summary>
    //    /// <param name="path">XML出力先ディレクトリ</param>
    //    private string GetInPutPath(string path)
    //    {
    //        string retPath = "";

    //        // XML出力先ディレクトリ読み込み
    //        string[] lines = File.ReadAllLines("mobileXMLPath.ini",
    //          System.Text.Encoding.GetEncoding("Shift_JIS"));

    //        // ディレクトリ取得
    //        int maxCount = lines.Length;
    //        for (int i = 0; i < maxCount; i++)
    //        {
    //            if (lines[i].IndexOf(path) >= 0)
    //            {
    //                // 「=」以降を取得
    //                int num = lines[i].IndexOf("=");
    //                retPath = lines[i].Substring(num + 1);
    //            }

    //        }
    //        return retPath;
    //    }

    //    /// <summary>
    //    /// 「Ｆ２ 切替ボタン」イベント
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void bt_func2_Click(object sender, EventArgs e)
    //    {
    //        if (string.IsNullOrEmpty(this.form.ntxt_TeikiMitouroku.Text))
    //        { // 定期未登録欄が空欄（スポットデータ表示時）の場合　→　定期データの表示

    //            // 一覧の表示
    //            this.SetIchiran("TEIKI");

    //            // 表示・非表示判定
    //            setVisible("TEIKI");

    //            // 未登録数の表示
    //            this.SetMitourokusu("TEIKI");

    //            // タイトル表示の切り替え
    //            var parentForm = (BusinessBaseForm)this.form.Parent;
    //            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
    //            titleControl.Text = MobileShougunTorikomiConst.Title1;
    //        }
    //        else
    //        { // スポット未登録欄が空欄（定期データ表示時）の場合　→　スポットデータの表示

    //            // 一覧の表示
    //            this.SetIchiran("SPOT");

    //            // 表示・非表示判定
    //            setVisible("SPOT");

    //            // 未登録数の表示
    //            this.SetMitourokusu("SPOT");

    //            // タイトル表示の切り替え
    //            var parentForm = (BusinessBaseForm)this.form.Parent;
    //            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
    //            titleControl.Text = MobileShougunTorikomiConst.Title2;
    //        }
    //    }

    //    /// <summary>
    //    /// 「Ｆ７ 検索ボタン」イベント
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void bt_func7_Click(object sender, EventArgs e)
    //    {
    //        // 一覧の表示
    //        this.SetIchiran("SEARCH");
    //    }

    //    /// <summary>
    //    /// 「Ｆ９ 登録ボタン」イベント
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void bt_func9_Click(object sender, EventArgs e)
    //    {
    //        // 登録実行フラグの初期化
    //        Boolean chkFlg = false;

    //        for (int i = 0; i < selectedRowsIchiran.Length; i++)
    //        {
    //            if ((bool)this.form.Ichiran.Rows[i].Cells["DELETE_CHECK"].Value)
    //            {
    //                // モバイル将軍用データ取込画面専用テーブル削除処理
    //                deleteMobileShougunData(Int64.Parse(this.form.Ichiran.Rows[i].Cells["EDABAN"].Value.ToString()));

    //                // 登録実行フラグにチェックをする
    //                chkFlg = true;
    //            }

    //            if ((bool)this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value)
    //            {
    //                // 各テーブル登録
    //                insertJissekiData(Int64.Parse(this.form.Ichiran.Rows[i].Cells["EDABAN"].Value.ToString()));

    //                // 登録実行フラグにチェックをする
    //                chkFlg = true;
    //            }
    //        }

    //        if (chkFlg)
    //        { // 削除処理or登録が実行された場合
    //            // 登録完了メッセージの表示
    //            MessageBox.Show(MobileShougunTorikomiConst.Msg2);

    //            // 画面の再表示
    //            ReWindow();
    //        }
    //        else
    //        {
    //            // 登録NGメッセージの表示
    //            MessageBox.Show(MobileShougunTorikomiConst.Msg3);           
    //        }
    //    }

    //    /// <summary>
    //    /// モバイル将軍用データ取込画面専用テーブル削除処理
    //    /// </summary>
    //    void deleteMobileShougunData(Int64 EDABAN)
    //    {
    //        MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
    //        entity.EDABAN = EDABAN;

    //        this.dao.GetDeleteMobileShougunDataForEntity(entity);
    //    }

    //    /// <summary>
    //    /// 各テーブル登録
    //    /// </summary>
    //    void insertJissekiData(Int64 EDABAN)
    //    {
    //        // DAO編集
    //        this.setTeikiJissekiEntryDao = DaoInitUtility.GetComponent<SetTeikiJissekiEntryDao>();
    //        this.setTeikiJissekiDetailDao = DaoInitUtility.GetComponent<SetTeikiJissekiDetailDao>();
    //        this.setTeikiJissekiNioroshiDao = DaoInitUtility.GetComponent<SetTeikiJissekiNioroshiDao>();
    //        this.setUrShEntryDao = DaoInitUtility.GetComponent<SetUrShEntryDao>();
    //        this.setUrShDetailDao = DaoInitUtility.GetComponent<SetUrShDetailDao>();

    //        // 初期化
    //        this.int64SystemId = 0;
    //        this.int64TeikiJissekiNumber = 0;

    //        // 対象枝番での絞り込み
    //        DataRow[] selectedRowsEdaban;
    //        selectedRowsEdaban = this.dataResult.Select("EDABAN = " + EDABAN);

    //        if (string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text))
    //        { // スポット未登録欄が空欄（定期データ表示時）の場合

    //            // 定期実績入力テーブル登録
    //            setTeikiJissekiEntry(selectedRowsEdaban);

    //            // 定期実績明細テーブル登録
    //            setTeikiJissekiDetail(selectedRowsEdaban);

    //            // 定期実績荷卸テーブル登録   
    //            setTeikiJissekiNioroshi(selectedRowsEdaban);
    //        }
    //        else
    //        { // 定期未登録欄が空欄（スポットデータ表示時）の場合

    //            // 売上_支払入力テーブル登録   
    //            setUrShEntry(selectedRowsEdaban);

    //            // 売上_支払明細テーブル登録   
    //            setUrShDetail(selectedRowsEdaban);
    //        }
    //    }

    //    /// <summary>
    //    /// 「Ｆ12 閉じるボタン」イベント
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void bt_func12_Click(object sender, EventArgs e)
    //    {
    //        var parentForm = (BusinessBaseForm)this.form.Parent;
    //        parentForm.Close();
    //    }

    //    /// <summary>
    //    /// 「[1] 全て選択(登録)ボタン」イベント
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void bt_process1_Click(object sender, EventArgs e)
    //    {
    //        for (int i = 0; i < selectedRowsIchiran.Length; i++)
    //        {
    //            this.form.Ichiran.Rows[i].Cells["DELETE_CHECK"].Value = false;
    //            this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value = true;
    //        }
    //    }

    //    /// <summary>
    //    /// 「[2] 全て解除(登録)ボタン」イベント
    //    /// </summary>
    //    /// <param name="sender">イベント呼び出し元オブジェクト</param>
    //    /// <param name="e">e</param>
    //    void bt_process2_Click(object sender, EventArgs e)
    //    {
    //        for (int i = 0; i < selectedRowsIchiran.Length; i++)
    //        {
    //            this.form.Ichiran.Rows[i].Cells["DELETE_CHECK"].Value = true;
    //            this.form.Ichiran.Rows[i].Cells["REGIST_CHECK"].Value = false;
    //        }
    //    }

    //    // <summary>
    //    // チェックボックス変更時のイベント
    //    // </summary>
    //    // <param name="sender">イベント呼び出し元オブジェクト</param>
    //    // <param name="e">e</param>
    //    public void delete_Check_Change(object sender, DataGridViewCellEventArgs e)
    //    {
    //        var dr = this.form.Ichiran.CurrentRow;

    //        if (dr.Cells[e.ColumnIndex].ColumnIndex == 0)
    //        { // 削除チェック(ColumnIndex = 0)がクリックされた場合
    //            if (!(bool)dr.Cells["DELETE_CHECK"].Value)
    //            { // クリック時、削除チェックにチェックが入っていない場合
    //                dr.Cells["DELETE_CHECK"].Value = true;
    //                dr.Cells["REGIST_CHECK"].Value = false;
    //            }

    //        }

    //        if (dr.Cells[e.ColumnIndex].ColumnIndex == 1)
    //        { // 登録チェック(ColumnIndex = 1)がクリックされた場合
    //            if (!(bool)dr.Cells["REGIST_CHECK"].Value)
    //            { // クリック時、登録チェックにチェックが入っていない場合
    //                dr.Cells["DELETE_CHECK"].Value = false;
    //                dr.Cells["REGIST_CHECK"].Value = true;
    //            }

    //        }

    //        if (dr.Cells[e.ColumnIndex].ColumnIndex == 2)
    //        { // 詳細ボタン(ColumnIndex = 2)がクリックされた場合
    //            ContenaForm frm = new ContenaForm(this.form);
    //            frm.Show();
    //        }
    //    }

    //    /// <summary>
    //    /// 取り込み済みデータ取得処理
    //    /// </summary>
    //    /// <param name="sender">配車区分/param>
    //    /// <param name="e">e</param>
    //    void getTorikomizumiData(string haishaKbun)
    //    {
    //        // モバイル将軍用データ取込画面専用テーブル取得
    //        MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();

    //        switch (haishaKbun)
    //        {
    //            case "YUUKOU":
    //                { // 有効データ(DELETE_FLG=false)の場合
    //                    this.dataResult = this.dao.GetYuukouDataForEntity(entity);

    //                    // 件数も取得
    //                    this.YuukouData_count = this.dataResult.Rows.Count;
    //                }
    //                break;

    //            case "MAX_SEQ_NO":
    //                { // シーケンシャルナンバーのMAX値を取得する場合
    //                    this.dataResult = this.dao.GetMaxSeqForEntity(entity);

    //                    DataRow[] selectedRows = dataResult.Select();
    //                    foreach (DataRow row in selectedRows)
    //                    {
    //                        this.Max_Seq_No = (Int64)row["MAX_SEQ_NO"];
    //                    }
    //                }
    //                break;

    //            case "MAX_EDABAN":
    //                { // 枝番のMAX値を取得する場合
    //                    this.dataResult = this.dao.GetMaxEdabanForEntity(entity);

    //                    DataRow[] selectedRows = dataResult.Select();
    //                    foreach (DataRow row in selectedRows)
    //                    {
    //                        this.Max_Edaban = (Int64)row["MAX_EDABAN"];
    //                    }
    //                }
    //                break;
    //        }
    //    }

    //    /// <summary>
    //    /// コース名称マスタ取得処理
    //    /// </summary>
    //    public string getCourseName(string courseCd)
    //    {

    //        string strCourseName = null;

    //        // コース名称マスタ取得取得
    //        MobileShougunTorikomiDTOClass entity = new MobileShougunTorikomiDTOClass();
    //        entity.COURSE_NAME_CD = courseCd;
    //        this.courseNameResult = this.dao.GetCourseNameDataForEntity(entity);

    //        DataRow[] selectedRows = courseNameResult.Select();
    //        foreach (DataRow row in selectedRows)
    //        {
    //            if (row["COURSE_NAME"] == DBNull.Value)
    //            {
    //                strCourseName = null;
    //            }
    //            else
    //            {
    //                strCourseName = (string)row["COURSE_NAME"];
    //            }
    //        }

    //        return strCourseName;
    //    }

        /// <summary>
        /// 一覧の表示
        /// </summary>
        internal void SetIchiran(string haishaKbun)
        {
            // 親情報をセット
            // 親のテーブル
            var table = this.dataResult;
            table.BeginLoadData();

            // 親の選択グリッド
            Int64 selectEdaban = (Int64)this.dr.Cells["EDABAN"].Value;     // 枝番
           
            // where句の初期化
            string selectWhere = null;

            // 一覧表示内容制御
            switch (haishaKbun)
            {
                case "SETTI":
                    { // 設置の場合

                        // 一覧の初期化
                        this.form.Grid_Setti.Rows.Clear();

                        // 一覧の抽出
                        selectWhere = "EDABAN = " + selectEdaban + " AND NODE_EDABAN = 7 AND CONTENA_IDOU_KBN = 1"; 
                        selectedRowsSettiIchiran = table.Select(selectWhere);
                    }
                    break;

                case "HIKIAGE":
                    { // 引揚の場合

                        // 一覧の初期化
                        this.form.Grid_Hikiage.Rows.Clear();

                        selectWhere = "EDABAN = " + selectEdaban + " AND NODE_EDABAN = 7 AND CONTENA_IDOU_KBN = 2";
                        selectedRowsHikiageIchiran = table.Select(selectWhere);
                    }
                    break;
            }

            // 検索結果設定
            switch (haishaKbun)
            {
                case "SETTI":
                    { // 設置の場合
                        for (int i = 0; i < selectedRowsSettiIchiran.Length; i++)
                        {
                            this.form.Grid_Setti.Rows.Add();

                            // コンテナ種類CD
                            if (string.IsNullOrEmpty(selectedRowsSettiIchiran[i]["CONTENA_SHURUI_CD"].ToString()))
                            {
                                this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_SHURUI_CD"].Value = null;
                            }
                            else
                            {
                                this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_SHURUI_CD"].Value = selectedRowsSettiIchiran[i]["CONTENA_SHURUI_CD"].ToString();
                            }

                            if (getContenaShurui(selectedRowsSettiIchiran[i]["CONTENA_SHURUI_CD"].ToString()) != 0)
                            {
                                // コンテナ種類名
                                this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_SYURUI_NAME"].Value = this.strContenaShuruiName;
                            }

                            // コンテナCD
                            if (string.IsNullOrEmpty(selectedRowsSettiIchiran[i]["CONTENA_CD"].ToString()))
                            {
                                this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_CD"].Value = null;
                            }
                            else
                            {
                                this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_CD"].Value = selectedRowsSettiIchiran[i]["CONTENA_CD"].ToString();
                            }

                            if (getContena(selectedRowsSettiIchiran[i]["CONTENA_SHURUI_CD"].ToString(), selectedRowsSettiIchiran[i]["CONTENA_CD"].ToString()) != 0)
                            {
                                // コンテナ名
                                this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_NAME"].Value = this.strContenaName;
                            }
                            
                            if (getContenaReserve(Contena_Set_Kbn_Setti, selectedRowsSettiIchiran[i]["CONTENA_SHURUI_CD"].ToString(), selectedRowsSettiIchiran[i]["CONTENA_CD"].ToString()) != 0)
                            {
                                // 台数
                                this.form.Grid_Setti.Rows[i].Cells["SETTI_DAISUU_CNT"].Value = this.intDaisuuCnt;                       
                            }

                            //// 登録用データの編集
                            //this.form.Grid_Setti.Rows[i].Cells["SEQ_NO"].Value = selectedRowsSettiIchiran[i]["SEQ_NO"];
                            //this.form.Grid_Setti.Rows[i].Cells["EDABAN"].Value = selectedRowsSettiIchiran[i]["EDABAN"];
                        }
                    }
                    break;

                case "HIKIAGE":
                    { // 引揚の場合
                        for (int i = 0; i < selectedRowsHikiageIchiran.Length; i++)
                        {
                            this.form.Grid_Hikiage.Rows.Add();

                            // コンテナ種類CD
                            if (string.IsNullOrEmpty(selectedRowsHikiageIchiran[i]["CONTENA_SHURUI_CD"].ToString()))
                            {
                                this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_SHURUI_CD"].Value = null;
                            }
                            else
                            {
                                this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_SHURUI_CD"].Value = selectedRowsHikiageIchiran[i]["CONTENA_SHURUI_CD"].ToString();
                            }

                            if (getContenaShurui(selectedRowsHikiageIchiran[i]["CONTENA_SHURUI_CD"].ToString()) != 0)
                            {
                                // コンテナ種類名
                                this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_SYURUI_NAME"].Value = this.strContenaShuruiName;
                            }

                            // コンテナCD
                            if (string.IsNullOrEmpty(selectedRowsHikiageIchiran[i]["CONTENA_CD"].ToString()))
                            {
                                this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_CD"].Value = null;
                            }
                            else
                            {
                                this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_CD"].Value = selectedRowsHikiageIchiran[i]["CONTENA_CD"].ToString();
                            }

                            if (getContena(selectedRowsHikiageIchiran[i]["CONTENA_SHURUI_CD"].ToString(), selectedRowsHikiageIchiran[i]["CONTENA_CD"].ToString()) != 0)
                            {
                                // コンテナ名
                                this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_NAME"].Value = this.strContenaName;
                            }

                            if (getContenaReserve(Contena_Set_Kbn_Hikiage, selectedRowsHikiageIchiran[i]["CONTENA_SHURUI_CD"].ToString(), selectedRowsHikiageIchiran[i]["CONTENA_CD"].ToString()) != 0)
                            {
                                // 台数
                                this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_DAISUU_CNT"].Value = this.intDaisuuCnt;
                            }

                            //// 登録用データの編集
                            //this.form.Grid_Hikiage.Rows[i].Cells["SEQ_NO"].Value = selectedRowsHikiageIchiran[i]["SEQ_NO"];
                            //this.form.Grid_Hikiage.Rows[i].Cells["EDABAN"].Value = selectedRowsHikiageIchiran[i]["EDABAN"];
                        }
                    }
                    break;
            }

            // グリッドセル色の設定
            switch (haishaKbun)
            {
                case "SETTI":
                    { // 設置の場合
                        for (int i = 0; i < selectedRowsSettiIchiran.Length; i++)
                        {
                            this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_SHURUI_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);       // コンテナ種類CD
                            this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_SYURUI_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);     // コンテナ種類名
                            this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);              // コンテナCD
                            this.form.Grid_Setti.Rows[i].Cells["SETTI_CONTENA_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);            // コンテナ名
                            this.form.Grid_Setti.Rows[i].Cells["SETTI_DAISUU_CNT"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);              // 台数
                        }
                    }
                    break;

                case "HIKIAGE":
                    { // 引揚の場合
                        for (int i = 0; i < selectedRowsHikiageIchiran.Length; i++)
                        {
                            this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_SHURUI_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);       // コンテナ種類CD
                            this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_SYURUI_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);     // コンテナ種類名
                            this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_CD"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);              // コンテナCD
                            this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_CONTENA_NAME"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);            // コンテナ名
                            this.form.Grid_Hikiage.Rows[i].Cells["HIKIAGE_DAISUU_CNT"].Style.BackColor = System.Drawing.Color.FromArgb(240, 250, 230);              // 台数
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// コンテナ種類マスタ取得処理
        /// </summary>
        public int getContenaShurui(string contenaShuruiCd)
        {
            this.strContenaShuruiName = null;

            ContenaShuruiDTOClass entity = new ContenaShuruiDTOClass();
            entity.CONTENA_SHURUI_CD = contenaShuruiCd;
            this.contenaShuruiResult = this.dao.GetContenaShuruiDataForEntity(entity);

            DataRow[] selectedRows = contenaShuruiResult.Select();
            foreach (DataRow row in selectedRows)
            {
                if (row["CONTENA_SHURUI_NAME_RYAKU"] == DBNull.Value)
                {
                    this.strContenaShuruiName = null;
                }
                else
                {
                    this.strContenaShuruiName = (string)row["CONTENA_SHURUI_NAME_RYAKU"];
                }
            }

            return selectedRows.Length;
        }

        /// <summary>
        /// コンテナマスタ取得処理
        /// </summary>
        public int getContena(string contenaShuruiCd, string contenaCd)
        {
            this.strContenaName = null;

            ContenaDTOClass entity = new ContenaDTOClass();
            entity.CONTENA_SHURUI_CD = contenaShuruiCd;
            entity.CONTENA_CD = contenaCd;
            this.contenaResult = this.dao.GetContenaDataForEntity(entity);

            DataRow[] selectedRows = contenaResult.Select();
            foreach (DataRow row in selectedRows)
            {
                if (row["CONTENA_NAME_RYAKU"] == DBNull.Value)
                {
                    this.strContenaName = null;
                }
                else
                {
                    this.strContenaName = (string)row["CONTENA_NAME_RYAKU"];
                }
            }

            return selectedRows.Length;
        }

        /// <summary>
        /// コンテナ稼動予定テーブル取得処理
        /// </summary>
        public int getContenaReserve(Int16 Contena_Set_Kbn, string contenaShuruiCd, string contenaCd)
        {
            this.intDaisuuCnt = 0;

            ContenaReserveDTOClass entity = new ContenaReserveDTOClass();
            entity.CONTENA_SET_KBN = Contena_Set_Kbn;
            entity.CONTENA_SHURUI_CD = contenaShuruiCd;
            entity.CONTENA_CD = contenaCd;
            this.contenaReserveResult = this.dao.GetContenaReserveDataForEntity(entity);

            DataRow[] selectedRows = contenaReserveResult.Select();
            foreach (DataRow row in selectedRows)
            {
                if (row["DAISUU_CNT"] != DBNull.Value)
                {
                    this.intDaisuuCnt = (int)row["DAISUU_CNT"];
                }
            }

            return selectedRows.Length;
        }

    //    /// <summary>
    //    /// 検索条件取得処理
    //    /// </summary>
    //    public string getSelectWhere()
    //    {
    //        string selectWhere = null;

    //        if (string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim()) &&
    //            string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim()) &&
    //            string.IsNullOrEmpty(this.form.ntxt_KyotenCd.Text) &&
    //            string.IsNullOrEmpty(this.form.ntxt_ShashuCd.Text) &&
    //            string.IsNullOrEmpty(this.form.ntxt_SharyouCd.Text) &&
    //            string.IsNullOrEmpty(this.form.ntxt_UntenshaCd.Text) &&
    //            string.IsNullOrEmpty(this.form.ntxt_CourseCd.Text))
    //        { // 指定条件がない場合

    //            // 定期orスポットの設定
    //            if (string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text))
    //            { // スポット未登録欄が空欄（定期データ表示時）の場合
    //                selectWhere = "HAISHA_KBN = 0";
    //            }
    //            else
    //            { // 定期未登録欄が空欄（スポットデータ表示時）の場合
    //                selectWhere = "HAISHA_KBN = 1";
    //            }

    //            return selectWhere;
    //        }
    //        else
    //        { // 指定条件がある場合

    //            DateTime dtSagyouDateFrom;
    //            DateTime dtSagyouDateTo;

    //            // 作業日Fromあり、作業日Toあり
    //            if ((!(string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim()))) &&
    //                (!(string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim()))))
    //            {
    //                dtSagyouDateFrom = DateTime.Parse(this.form.dtp_SagyouDateFrom.Text);
    //                dtSagyouDateTo = DateTime.Parse(this.form.dtp_SagyouDateTo.Text);
    //                selectWhere = selectWhere + "(" +
    //                                            string.Format("[{0}] >= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateFrom) +
    //                                            ") AND (" +
    //                                            string.Format("[{0}] <= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateTo) +
    //                                            ")";
    //            }

    //            // 作業日Fromあり、作業日Toなし
    //            if ((!(string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim()))) &&
    //                  (string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim())))
    //            {
    //                dtSagyouDateFrom = DateTime.Parse(this.form.dtp_SagyouDateFrom.Text);
    //                selectWhere = selectWhere + "(" +
    //                                            string.Format("[{0}] >= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateFrom) +
    //                                            ")";
    //            }

    //            // 作業日Fromなし、作業日Toあり
    //            if ((string.IsNullOrEmpty(this.form.dtp_SagyouDateFrom.Text.Trim())) &&
    //                (!(string.IsNullOrEmpty(this.form.dtp_SagyouDateTo.Text.Trim()))))
    //            {
    //                dtSagyouDateTo = DateTime.Parse(this.form.dtp_SagyouDateTo.Text);
    //                selectWhere = selectWhere + "(" +
    //                                            string.Format("[{0}] <= #{1}#", "HAISHA_SAGYOU_DATE", dtSagyouDateTo) +
    //                                            ")";
    //            }

    //            // 拠点あり
    //            if (!(string.IsNullOrEmpty(this.form.ntxt_KyotenCd.Text)))
    //            {
    //                selectWhere = selectWhere + " AND KYOTEN_CD = " + this.form.ntxt_KyotenCd.Text;
    //            }

    //            // 車種あり
    //            if (!(string.IsNullOrEmpty(this.form.ntxt_ShashuCd.Text)))
    //            {
    //                selectWhere = selectWhere + " AND SHASHU_CD = " + this.form.ntxt_ShashuCd.Text;
    //            }

    //            // 車輌あり
    //            if (!(string.IsNullOrEmpty(this.form.ntxt_SharyouCd.Text)))
    //            {
    //                selectWhere = selectWhere + " AND SHUKKO_SHARYOUCD = " + this.form.ntxt_SharyouCd.Text;
    //            }

    //            // 運転者あり
    //            if (!(string.IsNullOrEmpty(this.form.ntxt_UntenshaCd.Text)))
    //            {
    //                selectWhere = selectWhere + " AND HAISHA_UNTENSHA_CD = " + this.form.ntxt_UntenshaCd.Text;
    //            }

    //            // コースあり
    //            if (!(string.IsNullOrEmpty(this.form.ntxt_CourseCd.Text)))
    //            {
    //                selectWhere = selectWhere + " AND HAISHA_COURSE_NAME_CD = " + this.form.ntxt_CourseCd.Text;
    //            }

    //            // 定期orスポットの設定
    //            if (string.IsNullOrEmpty(this.form.ntxt_SpotMitouroku.Text))
    //            { // スポット未登録欄が空欄（定期データ表示時）の場合
    //                selectWhere = selectWhere + " AND HAISHA_KBN = 0";
    //            }
    //            else
    //            { // 定期未登録欄が空欄（スポットデータ表示時）の場合
    //                selectWhere = selectWhere + " AND HAISHA_KBN = 1";
    //            }

    //            // SQL文の整形
    //            if (selectWhere.Substring(0, 5) == " AND ")
    //            {
    //                StringBuilder sb = new System.Text.StringBuilder(selectWhere);
    //                sb.Replace(" AND ", "", 0, 5);
    //                selectWhere = sb.ToString();
    //            }
    //        }

    //        return selectWhere;
    //    }

    //    /// <summary>
    //    /// 未登録数の表示
    //    /// </summary>
    //    void SetMitourokusu(string haishaKbun)
    //    {
    //        DataRow[] selectedRows;

    //        // 取得したモバイル将軍用データ取込画面専用テーブルをセット
    //        var table = this.dataResult;
    //        table.BeginLoadData();

    //        if (haishaKbun == "TEIKI")
    //        { // 定期の場合
    //            selectedRows = table.Select("HAISHA_KBN = 0");
    //            this.teikiData_count = selectedRows.Length;
    //            this.form.ntxt_TeikiMitouroku.Text = this.teikiData_count.ToString();
    //            this.form.ntxt_SpotMitouroku.Text = string.Empty;
    //        }
    //        else
    //        { // スポットの場合
    //            selectedRows = table.Select("HAISHA_KBN = 1");
    //            this.spotData_count = selectedRows.Length;
    //            this.form.ntxt_TeikiMitouroku.Text = string.Empty;
    //            this.form.ntxt_SpotMitouroku.Text = this.spotData_count.ToString();
    //        }
    //    }
    }
}