using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Intercepter;
using r_framework.Logic;

namespace FixChouhyou
{
    public partial class FormMain : SuperForm
    {
        #region - Fields -

        /// <summary>画面ロジック</summary>
        private r_framework.Logic.IBuisinessLogic logic;

        /// <summary>ウィンドウＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>S2Daoインターフェースを保持するフィールド</summary>
        private IS2Dao dao = null;

        /// <summary>レポート情報オブジェクトを保持するフィールド</summary>
        private ReportInfoBase reportInfo = null;

        /// <summary>帳票(ComponentOne)用データーテーブルを保持するフィールド</summary>
        private DataTable dataTableForForm = new DataTable();

        #endregion - Fields -
        
        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="FormMain"/> class.</summary>
        public FormMain()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            // 会社別
            this.CorpType = CorpTypeDef.Normal;
        }

        /// <summary>Initializes a new instance of the <see cref="FormMain"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public FormMain(WINDOW_ID windowID)
        {
            this.InitializeComponent();

            this.windowID = windowID;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.dao = ((LogicClass)this.logic).Dao;

            // 会社別
            this.CorpType = CorpTypeDef.Normal;
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>出力タイプに関する列挙型</summary>
        public enum OutputTypeDef
        {
            /// <summary>A4 縦三つ切り</summary>
            OUTPUT_TYPE1 = 1,

            /// <summary>三つ切り 複数品目</summary>
            OUTPUT_TYPE2,
            
            /// <summary>三つ切り 単品目</summary>
            OUTPUT_TYPE3,

            /// <summary>金額見積り縦</summary>
            OUTPUT_TYPE6,
            
            /// <summary>金額見積り横</summary>
            OUTPUT_TYPE7,
            
            /// <summary>単価見積り縦</summary>
            OUTPUT_TYPE8,
            
            /// <summary>単価見積り横</summary>
            OUTPUT_TYPE9,
        }

        /// <summary>会社別に関する列挙型</summary>
        public enum CorpTypeDef
        {
            /// <summary>通常</summary>
            Normal,

            /// <summary>旭星</summary>
            Kyokusei,
        }

        #endregion - Enums -

        #region - Properties -

        /// <summary>会社別を保持するプロパティ</summary>
        public CorpTypeDef CorpType { get; set; }

        /// <summary>出力フォームのフルパス名を保持するプロパティ</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>帳票出力フォームレイアウト名を保持するプロパティ</summary>
        public string OutputFormLayout { get; set; }

        /// <summary>出力タイプを保持するプロパティ</summary>
        public OutputTypeDef OutputType { get; set; }

        /// <summary>エクセル出力する際のエクセル出力をするかＰＤＦ出力するかの状態を保持するプロパティ</summary>
        /// <remarks>真の場合：ＰＤＦで出力、偽の場合：Ｅｘｃｅｌで出力</remarks>
        public bool IsOutputPDF { get; set; }

        /// <summary>親フォーム</summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>フォームがロードされる場合処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.labelChouhyoumei.Text = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.windowID);

            this.panelParam.Visible = false;

            switch (this.windowID)
            {
                case WINDOW_ID.R_HAISYA_IRAISYO:                    // R345 配車依頼書
                case WINDOW_ID.R_SAGYOU_SIJISYO:                    // R350 作業依頼書

                    this.panelParam.Visible = true;

                    // パラメータ１
                    this.labelParam1.Visible = true;
                    this.labelParam1.Text = "控え印刷(0:正のみ / 1:正・控え２部)";
                    this.textBoxParam1.Visible = true;
                    this.textBoxParam1.Text = "0";

                    // パラメータ２
                    this.labelParam2.Visible = true;
                    this.labelParam2.Text = "受付番号";
                    this.textBoxParam2.Visible = true;
                    this.textBoxParam2.Text = "11";

                    // パラメータ３
                    this.labelParam3.Visible = true;
                    this.labelParam3.Text = "受付種類(1:収集 / 2:出荷)";
                    this.textBoxParam3.Visible = true;
                    this.textBoxParam3.Text = "1";
                    
                    break;
                case WINDOW_ID.R_UNTEN_NIPPOU:                      // R450 運転日報
                    this.panelParam.Visible = true;

                    // サンプルデータ
                    this.checkBoxSampleData.Visible = true;

                    // パラメータ１
                    this.labelParam1.Visible = true;
                    this.labelParam1.Text = "定期タイプ(1:配車 / 2:実績 / 3:DB)";
                    this.textBoxParam1.Visible = true;
                    this.textBoxParam1.Text = "3";

                    // パラメータ２
                    this.labelParam2.Visible = true;
                    this.labelParam2.Text = "出力タイプ(1:縦 / 2:横 / 3:DB)";
                    this.textBoxParam2.Visible = true;
                    this.textBoxParam2.Text = "3";

                    // パラメータ３
                    this.labelParam3.Visible = true;
                    this.labelParam3.Text = "定期配車番号";
                    this.textBoxParam3.Visible = true;
                    this.textBoxParam3.Text = "180";

                    break;
                default:

                    break;
            }
        }

        /// <summary>表示ボタンが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void ButtonDisplay_Click(object sender, EventArgs e)
        {
            switch (this.windowID)
            {
                case WINDOW_ID.R_SHIKIRISYO:                        // R338 仕切書
                    this.reportInfo = new ReportInfoR338(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR338)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_HAISYA_IRAISYO:                    // R345 配車依頼書
                case WINDOW_ID.R_SAGYOU_SIJISYO:                    // R350 作業指示書
                    this.reportInfo = new ReportInfoR345_R350(this.windowID, this.dao);

                    // 控え印刷(0：正のみ　1:正・控え２部)
                    ((ReportInfoR345_R350)this.reportInfo).ParameterList["HikaeType"] = this.textBoxParam1.Text;

                    // 伝票番号
                    ((ReportInfoR345_R350)this.reportInfo).ParameterList["DenpyouNumber"] = this.textBoxParam2.Text;

                    // 受付種類(1：収集　2:出荷)
                    ((ReportInfoR345_R350)this.reportInfo).ParameterList["UketukeType"] = this.textBoxParam3.Text;

                    // サンプルデータテーブル作成
                    //((ReportInfoR345_R350)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_HAISYA_MEISAISYO:                  // R346 配車明細表
                    this.reportInfo = new ReportInfoR346(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR346)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_KEIRYOU_HYOU:                      // R354 計量票
                    this.reportInfo = new ReportInfoR354_R549_R550(this.windowID);

                    switch (this.OutputType)
                    {
                        case OutputTypeDef.OUTPUT_TYPE1:    // A4 縦三つ切り

                            // 出力タイプ
                            ((ReportInfoR354_R549_R550)this.reportInfo).OutputType = ReportInfoR354_R549_R550.OutputTypeDef.Normal;

                            break;
                        case OutputTypeDef.OUTPUT_TYPE2:    // A4 複数品目

                            // 出力タイプ
                            ((ReportInfoR354_R549_R550)this.reportInfo).OutputType = ReportInfoR354_R549_R550.OutputTypeDef.MultiH;

                            break;
                        case OutputTypeDef.OUTPUT_TYPE3:    // A4 単品目

                            // 出力タイプ
                            ((ReportInfoR354_R549_R550)this.reportInfo).OutputType = ReportInfoR354_R549_R550.OutputTypeDef.SingleH;

                            break;
                    }

                    // サンプルデータテーブル作成
                    ((ReportInfoR354_R549_R550)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_KENNSYUU_ICHIRANHYOU:              // R400 検収一覧表
                    this.reportInfo = new ReportInfoR400(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR400)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_DAINOU_ICHIRANHYOU:                // R403 代納明細表
                    this.reportInfo = new ReportInfoR403(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR403)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_ZAIKO_KANNRIHYOU:                  // R405 在庫管理表
                    this.reportInfo = new ReportInfoR405(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR405)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_TANKA_HENKOU_TAISYOU_ICHIRANHYOU:  // R424 単価変更対象一覧表
                    this.reportInfo = new ReportInfoR424(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR424)this.reportInfo).CreateSampleData();
                    
                    break;
                case WINDOW_ID.R_MITSUMORISYO:                      // R425 見積書
                    switch (this.OutputType)
                    {
                        case OutputTypeDef.OUTPUT_TYPE6:    // 金額見積り縦

                            if (this.CorpType == CorpTypeDef.Kyokusei)
                            {   // 旭星向け
                                this.reportInfo = new ReportInfoR425_R508_R547_R548_Kyokusei(this.windowID);

                                // 出力タイプ
                                ((ReportInfoR425_R508_R547_R548_Kyokusei)this.reportInfo).OutputType = ReportInfoR425_R508_R547_R548_Kyokusei.OutputTypeDef.KingakuMitsumoriV;

                                // サンプルデータテーブル作成
                                ((ReportInfoR425_R508_R547_R548_Kyokusei)this.reportInfo).CreateSampleData();
                            }
                            else
                            {   // 通常
                                this.reportInfo = new ReportInfoR425_R508_R547_R548(this.windowID);

                                // 出力タイプ
                                ((ReportInfoR425_R508_R547_R548)this.reportInfo).OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.KingakuMitsumoriV;

                                // サンプルデータテーブル作成
                                ((ReportInfoR425_R508_R547_R548)this.reportInfo).CreateSampleData();
                            }
                            
                            break;
                        case OutputTypeDef.OUTPUT_TYPE7:    // 金額見積り横
                            this.reportInfo = new ReportInfoR425_R508_R547_R548(this.windowID);

                            // 出力タイプ
                            ((ReportInfoR425_R508_R547_R548)this.reportInfo).OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.KingakuMitsumoriH;

                            // サンプルデータテーブル作成
                            ((ReportInfoR425_R508_R547_R548)this.reportInfo).CreateSampleData();

                            break;
                        case OutputTypeDef.OUTPUT_TYPE8:    // 単価見積り縦
                            this.reportInfo = new ReportInfoR425_R508_R547_R548(this.windowID);

                            // 出力タイプ
                            ((ReportInfoR425_R508_R547_R548)this.reportInfo).OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.TankaMitsumoriV;

                            // サンプルデータテーブル作成
                            ((ReportInfoR425_R508_R547_R548)this.reportInfo).CreateSampleData();

                            break;
                        case OutputTypeDef.OUTPUT_TYPE9:    // 単価見積り横
                            this.reportInfo = new ReportInfoR425_R508_R547_R548(this.windowID);

                            // 出力タイプ
                            ((ReportInfoR425_R508_R547_R548)this.reportInfo).OutputType = ReportInfoR425_R508_R547_R548.OutputTypeDef.TankaMitsumoriH;

                            // サンプルデータテーブル作成
                            ((ReportInfoR425_R508_R547_R548)this.reportInfo).CreateSampleData();

                            break;
                    }

                    break;
                case WINDOW_ID.R_TEIKI_HAISYAHYOU_TSUKI:            // R429 定期配車表(月)
                    this.reportInfo = new ReportInfoR429(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR429)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_TEIKI_HAISYAHYOU_NEN:              // R430 定期配車表(年)
                    this.reportInfo = new ReportInfoR430(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR430)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_UNTEN_NIPPOU:                      // R450 運転日報
                    this.reportInfo = new ReportInfoR450_R551(this.windowID, this.dao);

                    switch (this.textBoxParam1.Text)
                    {
                        case "1":   // 定期配車
                            ((ReportInfoR450_R551)this.reportInfo).TeikiType = ReportInfoR450_R551.TeikiTypeDef.Haisha;

                            break;
                        case "2":   // 定期実績
                            ((ReportInfoR450_R551)this.reportInfo).TeikiType = ReportInfoR450_R551.TeikiTypeDef.Jitsuseki;

                            break;
                        case "3":   // DB
                            ((ReportInfoR450_R551)this.reportInfo).TeikiType = ReportInfoR450_R551.TeikiTypeDef.Normal;

                            break;
                    }

                    switch (this.textBoxParam2.Text)
                    {
                        case "1":   // 縦
                            ((ReportInfoR450_R551)this.reportInfo).OutputType = ReportInfoR450_R551.OutputTypeDef.Vertical;
                            
                            this.OutputFormLayout = "LAYOUT1";

                            break;
                        case "2":   // 横
                            ((ReportInfoR450_R551)this.reportInfo).OutputType = ReportInfoR450_R551.OutputTypeDef.Holizontal;

                            this.OutputFormLayout = "LAYOUT2";

                            break;
                        case "3":   // DB
                            ((ReportInfoR450_R551)this.reportInfo).OutputType = ReportInfoR450_R551.OutputTypeDef.Normal;

                            // レイアウトが縦か否か取得
                            if (((ReportInfoR450_R551)this.reportInfo).IsLayoutV())
                            {   // 縦
                                this.OutputFormLayout = "LAYOUT1";
                            }
                            else
                            {   // 横
                                this.OutputFormLayout = "LAYOUT2";
                            }
                            
                            break;
                    }

                    // サンプルデータ使用有無
                    ((ReportInfoR450_R551)this.reportInfo).IsSampleDataUse = this.checkBoxSampleData.Checked;

                    // 定期配車番号
                    ((ReportInfoR450_R551)this.reportInfo).TeikiHaishaNo = this.textBoxParam3.Text;

                    break;
                case WINDOW_ID.R_UNNCHIN_SYUUKEIHYOU:               // R483 運賃集計表
                    this.reportInfo = new ReportInfoR483(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR483)this.reportInfo).CreateSampleData();

                    break;
                case WINDOW_ID.R_DAINOU_SYUUKEIHYOU:                // R488 代納集計表
                    this.reportInfo = new ReportInfoR488(this.windowID);

                    // サンプルデータテーブル作成
                    ((ReportInfoR488)this.reportInfo).CreateSampleData();

                    break;
            }

            if (this.reportInfo != null)
            {
                // フォーム情報取得
                this.reportInfo.Create(this.OutputFormFullPathName, this.OutputFormLayout, new DataTable());

                // 印刷ポップアップ画面表示
                using (FormReportPrintPopup popup = new FormReportPrintPopup(this.reportInfo, this.windowID))
                {
                    popup.IsOutputPDF = this.IsOutputPDF;
                    popup.ReportCaption = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.windowID);
                    popup.ShowDialog();
                    popup.Dispose();
                }
            }
        }

        /// <summary>閉じるボタンが押された場合の処理を実行する</summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">イベント</param>
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion - Methods -
    }
}
