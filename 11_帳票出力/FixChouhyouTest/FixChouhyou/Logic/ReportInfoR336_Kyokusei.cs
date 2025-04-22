using System;
using System.Data;
using CommonChouhyouPopup.App;

namespace Report
{
    #region - Class -

    /// <summary> R336(請求書)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR336 : ReportInfoBase
    {
        #region - Fields -

        // Detail部データテーブル
        private DataTable chouhyouDataTable = new DataTable();

        // Header部
        // 1-1
        private string torihikisakiCD = string.Empty;           // 取引先CD
        private string seikyuuSaki1H = string.Empty;            // 請求先1
        private string seikyuuSaki2H = string.Empty;            // 請求先2
        private string printDate = string.Empty;                // 発行日
        private string seikyuuNumber = string.Empty;            // 請求番号
        private string taitoru = "請　求　書";                  // タイトル【固定】

        // Detail部-Header部
        // 1-2  
        private string seikyuuSakiPostCD = string.Empty;        // 請求先郵便番号
        private string seikyuuSakiAddress1 = string.Empty;      // 請求先住所1
        private string seikyuuSakiAddress2 = string.Empty;      // 請求先住所2
        private string seikyuuSaki1DH = string.Empty;           // 請求先1
        private string seikyuuSaki2DH = string.Empty;           // 請求先2
        private string jishaName1 = string.Empty;               // 自社名1
        private string jishaName2 = string.Empty;               // 自社名2
        private string kyotenDaihyoushaName = string.Empty;     // 代表者名
        private string jishaPostCD = string.Empty;              // 自社郵便番号
        private string jishaAddress1 = string.Empty;            // 自社住所1
        private string jishaAddress2 = string.Empty;            // 自社住所2
        private string kyotenTEL = string.Empty;                // 電話
        private string kyotenFAX = string.Empty;                // FAX  
        private string seikyuuTantou = string.Empty;            // 請求担当者 
        private string kakinotori = "下記の通りご請求申し上げます。";  // 【固定】 

        // Detail部-Detail部
        // 1-3
        private string shiharaiHouhou = string.Empty;           // 支払い方法
        private string konkaiSeikyuuLabel = "今回御請求額";     // 今回御請求額label【固定】
        private string konkaiSeikyuu = string.Empty;            // 今回御請求額
        private string seikyuuDateLabel = "請求年月日";         // 請求年月日label【固定】
        private string seikyuuDate = string.Empty;              // 請求年月日
        private string codeLabel = "コード";                    // コードlabel【固定】
        private string code = string.Empty;                     // コード
        private string zenkaiSeikyuuLabel = "前回御請求額";     // 前回御請求額label【固定】
        private string zenkaiSeikyuu = string.Empty;            // 前回御請求額
        private string nyuukinGakuLabel = "入金額";             // 入金額label【固定】
        private string nyuukinGaku = string.Empty;              // 入金額
        private string sousaiHokaLabel = "相殺他";              // 相殺他label【固定】
        private string sousaiHoka = string.Empty;               // 相殺他
        private string sashihikiKurikosiLabel = "差引繰越額";   // 差引繰越額label【固定】
        private string sashihikiKurikosi = string.Empty;        // 差引繰越額
        private string konkaiUriageLabel = "今回売上額";        // 今回売上額label【固定】
        private string konkaiUriage = string.Empty;             // 今回売上額
        private string shouhizeiLabel = "消費税額";             // 消費税額label【固定】
        private string shouhizei = string.Empty;                // 消費税額
        private string goukeiSeikyuuLabel = "合計請求額";       // 合計請求額label【固定】
        private string goukeiSeikyuu = string.Empty;            // 合計請求額

        // Detail部-footer部
        // 1-4
        private string furikomiGinkouLabel = "振込銀行";        // 振込銀行label【固定】
        private string ginkouMei1 = string.Empty;               // 振込銀行1-銀行名
        private string shitenMei1 = string.Empty;               // 振込銀行1-支店名
        private string kouzaShurui1 = string.Empty;             // 振込銀行1-口座種類
        private string kouzaBangou1 = string.Empty;             // 振込銀行1-口座番号
        private string kouzaMeigi1 = string.Empty;              // 振込銀行1-口座名義
        private string ginkouMei2 = string.Empty;               // 振込銀行2-銀行名
        private string shitenMei2 = string.Empty;               // 振込銀行2-支店名
        private string kouzaShurui2 = string.Empty;             // 振込銀行2-口座種類
        private string kouzaBangou2 = string.Empty;             // 振込銀行2-口座番号
        private string kouzaMeigi2 = string.Empty;              // 振込銀行2-口座名義
        private string ginkouMei3 = string.Empty;               // 振込銀行3-銀行名
        private string shitenMei3 = string.Empty;               // 振込銀行3-支店名
        private string kouzaShurui3 = string.Empty;             // 振込銀行3-口座種類
        private string kouzaBangou3 = string.Empty;             // 振込銀行3-口座番号
        private string kouzaMeigi3 = string.Empty;              // 振込銀行3-口座名義

        // Detail部-footer部
        // 1-5
        private string bikouLabel = "備考：";                   // 備考label【固定】
        private string bikou = string.Empty;                    // 備考

        // グループヘッダ表示、非表示フラグ
        private bool gyousyaNameVisible = false;                // 業者グループヘッダ(グループ１)の表示、非表示FLAG
        private bool gyousyaKeiVisible = false;                 // 業者グループフッダ(グループ１)の表示、非表示FLAG
        private bool genbaNameVisible = false;                  // 現場グループヘッダ(グループ２)の表示、非表示FLAG
        private bool genbaKeiVisible = false;                   // 現場グループフッダ(グループ２)の表示、非表示FLAG

        #endregion

        #region - Constructors -

        /// <summary> Initializes a new instance of the <see cref="ReportInfoR361" /> class. </summary>
        public ReportInfoR336()
        {
            // パスとレイアウトのデフォルト値を設定しておく

            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "../../Template/R336-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";
        }

        #endregion

        #region - properties -
        /// <summary>帳票出力フルパスフォーム名を保持するフィールド</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>帳票出力フォームレイアウト名を保持するフィールド</summary>
        public string OutputFormLayout { get; set; }

        /// <summary>帳票用データテーブルを保持するプロパティ</summary>
        public DataTable ChouhyouDataTable { get; set; }
        #endregion

        #region - Methods -

        /// <summary> C1Reportの帳票データの作成を実行する </summary>
        /// <param name="chouhyouData">chouhyouData</param>
        public void R336_Reprt(DataTable chouhyouData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // 明細データTABLEをセット
            this.SetRecord(this.ChouhyouDataTable);

            // データテーブル情報から帳票情報作成処理を実行する
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dataTableChouhyouForm);
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            // 業者グループヘッダ(グループ１)の表示、非表示制御
            this.SetGroupVisible("GROUP1", this.gyousyaNameVisible, this.gyousyaKeiVisible);
            
            // 現場グループヘッダ(グループ２)の表示、非表示制御
            this.SetGroupVisible("GROUP2", this.genbaNameVisible, this.genbaKeiVisible);
            
            // Header部
            // 1-1
            this.SetFieldName("FH_TORIHIKISAKI_CD_1_CTL", this.torihikisakiCD);             // 取引先CD
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME1_1_CTL", this.seikyuuSaki1H);          // 請求先1
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME2_1_CTL", this.seikyuuSaki2H);          // 請求先2            
            this.SetFieldName("FH_PRINT_DATE_VLB", "発行日：" + this.printDate);            // 発行日
            this.SetFieldName("FH_SEIKYUU_NUMBER_CTL", "No." + this.seikyuuNumber);         // 請求番号
            this.SetFieldName("FH_TITLE_FLB", this.taitoru);                                // タイトル【固定】"請　求　書"

            // poage-Header部
            this.SetFieldName("PHY_TORIHIKISAKI_CD_CTL", this.torihikisakiCD);              // 取引先CD
            this.SetFieldName("PHY_SEIKYUU_SOUFU_NAME1_CTL", this.seikyuuSaki1H);           // 請求先1
            this.SetFieldName("PHY_SEIKYUU_SOUFU_NAME2_CTL", this.seikyuuSaki2H);           // 請求先2
            this.SetFieldName("PHY_PRINT_DATE_VLB", "発行日:" + this.printDate);            // 発行日
            this.SetFieldName("PHY_SEIKYUU_NUMBER_CTL", "No." + this.seikyuuNumber);        // 請求番号

            // Detail-Header部
            // 1-2
            this.SetFieldName("FH_SEIKYUU_SOUFU_POST_CTL", this.seikyuuSakiPostCD);         // 請求先郵便番号
            this.SetFieldName("FH_SEIKYUU_SOUFU_ADDRESS1_CTL", this.seikyuuSakiAddress1);   // 請求先住所1
            this.SetFieldName("FH_SEIKYUU_SOUFU_ADDRESS2_CTL", this.seikyuuSakiAddress2);   // 請求先住所2
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME1_2_CTL", this.seikyuuSaki1DH);         // 請求先1
            this.SetFieldName("FH_SEIKYUU_SOUFU_NAME2_2_CTL", this.seikyuuSaki2DH);         // 請求先2
            this.SetFieldName("FH_JISHA_NAME1_CTL", this.jishaName1);                       // 自社名1
            this.SetFieldName("FH_JISHA_NAME2_CTL", this.jishaName2);                       // 自社名2
            this.SetFieldName("FH_KYOTEN_DAIHYOU_CTL", this.kyotenDaihyoushaName);          // 代表者名
            this.SetFieldName("FH_KYOTEN_POST_CTL", this.jishaPostCD);                      // 自社郵便番号
            this.SetFieldName("FH_KYOTEN_ADDRESS1_CTL", this.jishaAddress1);                // 自社住所1
            this.SetFieldName("FH_KYOTEN_ADDRESS2_CTL", this.jishaAddress2);                // 自社住所2
            this.SetFieldName("FH_KYOTEN_TEL_CTL", this.kyotenTEL);                         // 電話
            this.SetFieldName("FH_KYOTEN_FAX_CTL", this.kyotenFAX);                         // FAX
            this.SetFieldName("FH_SEIKYUU_TANTOU_CTL", this.seikyuuTantou);                 // 請求担当者
            this.SetFieldName("FH_KAKINOTORI_FBL", this.kakinotori);                        // 【固定】"下記の通りご請求申し上げます。"

            // Detail-Header部
            // 1-3
            this.SetFieldName("FH_SIHARAIHOUHOU_VLB", this.shiharaiHouhou);                     // 支払い方法
            this.SetFieldName("FH_KONKAI_SEIKYU_GAKU_FLB", this.konkaiSeikyuuLabel);            // 今回御請求額label【固定】"今回御請求額"
            this.SetFieldName("FH_KONKAI_SEIKYU_GAKU_CTL", this.konkaiSeikyuu);                 // 今回御請求額
            this.SetFieldName("FH_SEIKYU_NENGATSUPI_FLB", this.seikyuuDateLabel);               // 請求年月日label【固定】"請求年月日"
            this.SetFieldName("FH_SEIKYU_NENGATSUPI_CTL", this.seikyuuDate);                    // 請求年月日
            this.SetFieldName("FH_TORIHIKISAKI_CD_2_FLB", this.codeLabel);                      // コードlabel【固定】"コード"
            this.SetFieldName("FH_TORIHIKISAKI_CD_2_CTL", this.code);                           // コード
            this.SetFieldName("FH_ZENKAI_KURIKOSI_GAKU_FLB", this.zenkaiSeikyuuLabel);          // 前回御請求額label【固定】"前回御請求額"
            this.SetFieldName("FH_ZENKAI_KURIKOSI_GAKU_CTL", this.zenkaiSeikyuu);               // 前回御請求額
            this.SetFieldName("FH_KONKAI_NYUUKIN_GAKU_FLB", this.nyuukinGakuLabel);             // 入金額label【固定】"入金額"
            this.SetFieldName("FH_KONKAI_NYUUKIN_GAKU_CTL", this.nyuukinGaku);                  // 入金額
            this.SetFieldName("FH_KONKAI_CHOUSEI_GAKU_FLB", this.sousaiHokaLabel);              // 相殺他label【固定】"相殺他"
            this.SetFieldName("FH_KONKAI_CHOUSEI_GAKU_CTL", this.sousaiHoka);                   // 相殺他
            this.SetFieldName("FH_SASHIHIKI_KURIKOSHI_GAKU_FLB", this.sashihikiKurikosiLabel);  // 差引繰越額label【固定】"差引繰越額"
            this.SetFieldName("FH_SASHIHIKI_KURIKOSHI_GAKU_CTL", this.sashihikiKurikosi);       // 差引繰越額
            this.SetFieldName("FH_KONKAI_URIAGE_GAKU_FLB", this.konkaiUriageLabel);             // 今回売上額label【固定】"今回売上額"
            this.SetFieldName("FH_KONKAI_URIAGE_GAKU_CTL", this.konkaiUriage);                  // 今回売上額
            this.SetFieldName("FH_SHOUHIZEI_GAKU_FLB", this.shouhizeiLabel);                    // 消費税額label【固定】"消費税額"
            this.SetFieldName("FH_SHOUHIZEI_GAKU_CTL", this.shouhizei);                         // 消費税額
            this.SetFieldName("FH_GOUKEI_GOSEIKYUU_GAKU_FLB", this.goukeiSeikyuuLabel);         // 合計請求額label【固定】"合計請求額"
            this.SetFieldName("FH_GOUKEI_GOSEIKYUU_GAKU_CTL", this.goukeiSeikyuu);              // 合計請求額

            // Page-Footer部
            // 1-4
            this.SetFieldName("FF_FURIKOMI_GINKO_FLB", this.furikomiGinkouLabel);                                                // 振込銀行label【固定】"振込銀行"
            this.SetFieldName("FF_FURIKOMI_GINKO1_CTL", this.ginkouMei1 + "　" + this.shitenMei1);                               // 振込銀行1-銀行(銀行名、支店名)
            this.SetFieldName("FF_FURIKOMI_KOZA1_CTL", this.kouzaShurui1 + "　" + this.kouzaBangou1 + "　" + this.kouzaMeigi1);  // 振込銀行1-口座((口座種類、口座番号、口座名義)
            this.SetFieldName("FF_FURIKOMI_GINKO2_CTL", this.ginkouMei2 + "　" + this.shitenMei2);                               // 振込銀行2-銀行(銀行名、支店名)
            this.SetFieldName("FF_FURIKOMI_KOZA2_CTL", this.kouzaShurui2 + "　" + this.kouzaBangou2 + "　" + this.kouzaMeigi2);  // 振込銀行2-口座((口座種類、口座番号、口座名義)
            this.SetFieldName("FF_FURIKOMI_GINKO3_CTL", this.ginkouMei1 + "　" + this.shitenMei1);                               // 振込銀行3-銀行(銀行名、支店名)
            this.SetFieldName("FF_FURIKOMI_KOZA3_CTL", this.kouzaShurui3 + "　" + this.kouzaBangou3 + "　" + this.kouzaMeigi3);  // 振込銀行3-口座((口座種類、口座番号、口座名義)

            // Page-Footer部
            // 1-5
            this.SetFieldName("FF_BIKOU_FLB", this.bikouLabel);     // 備考label【固定】"備考："
            //this.SetFieldName("FF_BIKOU_CTL", this.bikou);          // 備考　　　// 2013/10/10現在でこのフッタ備考は出力されない想定
        }

        /// <summary> 帳票データより、C1Reportに渡すデータを作成する </summary>
        /// <param name="dataTable">帳票データ</param>
        private void InputDataToMem(DataTable dataTable)
        {
            // 数値項目フォーマット指定文字列
            string formatSyousuuAri = "#,#.000";        // 少数ありフォーマット(3桁ごとにカンマ区切りで、小数点第3位までゼロパディング)
            string formatSyousuuNashi = "#,#";          // 少数なしフォーマット(3桁ごとにカンマ区切り)

            // 詳細部のブランク項目セット用
            string empty = string.Empty;

            string gyousyaName = string.Empty;              // 業者名
            string genbaName = string.Empty;                // 現場名
            int gyousyaCount = 0;                              // 業者ID
            int genbaCount = 0;                                // 現場ID

            this.ChouhyouDataTable = new DataTable();

            // 明細データTABLEカラムセット
            this.ChouhyouDataTable.Columns.Add("PHN_GYOUSHA_NAME_FBL");                     // 業者名
            this.ChouhyouDataTable.Columns.Add("PHN_GENBA_NAME_FBL");                       // 現場名
            this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_DATE_FLB");                     // 月日
            this.ChouhyouDataTable.Columns.Add("PHY_DENPYOU_NUMBER_FLB");                   // 売上No.
            this.ChouhyouDataTable.Columns.Add("PHY_HINMEI_NAME_FLB");                      // 品名
            this.ChouhyouDataTable.Columns.Add("PHY_SUURYOU_FLB");                          // 数量
            this.ChouhyouDataTable.Columns.Add("PHY_UNIT_NAME_FLB");                        // 単位
            this.ChouhyouDataTable.Columns.Add("PHY_TANKA_FLB");                            // 単価
            this.ChouhyouDataTable.Columns.Add("PHY_KINGAKU_FLB");                          // 金額
            this.ChouhyouDataTable.Columns.Add("PHY_SHOUHIZEI_FLB");                        // 消費税
            this.ChouhyouDataTable.Columns.Add("PHY_MEISAI_BIKOU_FLB");                     // 備考
            this.ChouhyouDataTable.Columns.Add("PHN_GENBA_KINGAKU_FLB");                    // 現場計：金額
            this.ChouhyouDataTable.Columns.Add("PHN_GENBA_SHOUHIZEI_FLB");                  // 現場計：消費税
            this.ChouhyouDataTable.Columns.Add("PHN_GYOUSHA_KINGAKU_FLB");                  // 業者計：金額
            this.ChouhyouDataTable.Columns.Add("PHN_GYOUSHA_SHOUHIZEI_FLB");                // 業者計：消費税

            foreach (DataRow row in dataTable.Rows)
            {
                // row[0]を文字列に変換しtempに格納
                string tmp = row[0].ToString();

                // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                string[] list = this.ReportSplit(tmp);

                switch (list[0])
                { 
                    case "1-1":
                        #region -Header部-
                        // Header部
                        this.torihikisakiCD = list[1];           // 取引先CD
                        this.seikyuuSaki1H = list[2];            // 請求先1
                        this.seikyuuSaki2H = list[3];            // 請求先2
                        this.printDate = list[4];                // 発行日
                        this.seikyuuNumber = list[5];            // 請求番号
                        break;
                        #endregion

                    case "1-2":
                        #region -Detail部-Header部-
                        // Detail部-Header部
                        this.seikyuuSakiPostCD = list[1];        // 請求先郵便番号
                        this.seikyuuSakiAddress1 = list[2];      // 請求先住所1
                        this.seikyuuSakiAddress2 = list[3];      // 請求先住所2
                        this.seikyuuSaki1DH = list[4];           // 請求先1
                        this.seikyuuSaki2DH = list[5];           // 請求先2
                        this.jishaName1 = list[6];               // 自社名1
                        this.jishaName2 = list[7];               // 自社名2
                        this.kyotenDaihyoushaName = list[8];     // 代表者名
                        this.jishaPostCD = list[9];              // 自社郵便番号
                        this.jishaAddress1 = list[10];           // 自社住所1
                        this.jishaAddress2 = list[11];           // 自社住所2
                        this.kyotenTEL = list[12];               // 電話
                        this.kyotenFAX = list[13];               // FAX  
                        this.seikyuuTantou = list[14];           // 請求担当者 
                        break;
                        #endregion

                    case "1-3":
                        #region -Detail部-Header部-
                        // Detail部-Header部
                        this.shiharaiHouhou = list[1];                                          // 支払い方法
                        this.konkaiSeikyuu = list[2];                                           // 今回御請求額
                        this.seikyuuDate = list[3];                                             // 請求年月日
                        this.code = list[4];                                                    // コード
                        this.zenkaiSeikyuu = this.SetFormt(list[5], formatSyousuuNashi);        // 前回御請求額
                        this.nyuukinGaku = this.SetFormt(list[6], formatSyousuuNashi);          // 入金額
                        this.sousaiHoka = this.SetFormt(list[7], formatSyousuuNashi);           // 相殺他
                        this.sashihikiKurikosi = this.SetFormt(list[8], formatSyousuuNashi);    // 差引繰越額
                        this.konkaiUriage = this.SetFormt(list[9], formatSyousuuNashi);         // 今回売上額
                        this.shouhizei = this.SetFormt(list[10], formatSyousuuNashi);            // 消費税額
                        this.goukeiSeikyuu = this.SetFormt(list[11], formatSyousuuNashi);       // 合計請求額
                        break;
                        #endregion

                    case "1-4":
                        #region -Detail部-footer部-
                        // Detail部-footer部
                        this.ginkouMei1 = list[1];              // 振込銀行1-銀行名
                        this.shitenMei1 = list[2];              // 振込銀行1-支店名
                        this.kouzaShurui1 = list[3];            // 振込銀行1-口座種類
                        this.kouzaBangou1 = list[4];            // 振込銀行1-口座番号
                        this.kouzaMeigi1 = list[5];             // 振込銀行1-口座名義
                        this.ginkouMei2 = list[6];              // 振込銀行2-銀行名
                        this.shitenMei2 = list[7];              // 振込銀行2-支店名
                        this.kouzaShurui2 = list[8];            // 振込銀行2-口座種類
                        this.kouzaBangou2 = list[9];            // 振込銀行2-口座番号
                        this.kouzaMeigi2 = list[10];            // 振込銀行2-口座名義
                        this.ginkouMei3 = list[11];             // 振込銀行3-銀行名
                        this.shitenMei3 = list[12];             // 振込銀行3-支店名
                        this.kouzaShurui3 = list[13];           // 振込銀行3-口座種類
                        this.kouzaBangou3 = list[14];           // 振込銀行3-口座番号
                        this.kouzaMeigi3 = list[15];            // 振込銀行3-口座名義
                        break;
                        #endregion

                    case "1-5":
                        #region -Detail部-footer部-
                        // Detail部-footer部
                        this.bikou = list[1];               // 備考
                        break;
                        #endregion

                    case "2-1":
                        #region -Detail部-Detail部-
                        // Detail部-Detail部

                        // 2-1があったら業者グループヘッタの表示フラグをONにする
                        if (!this.gyousyaNameVisible)
                        {
                            this.gyousyaNameVisible = true;
                        }

                        gyousyaName = list[1];              // 業者名
                        break;
                        #endregion

                    case "2-2":
                        #region -Detail部-Detail部-
                        // Detail部-Detail部

                        // 2-2があったら現場グループヘッタの表示フラグをONにする
                        if (!this.genbaNameVisible)
                        {
                            this.genbaNameVisible = true;
                        }

                        genbaName = list[1];                // 現場名
                        break;
                        #endregion
                    
                    case "2-3":
                        #region -Detail部-Detail部-
                        // Detail部-Detail部

                        // 業者名表示フラグがfalseの場合
                        if (!this.gyousyaNameVisible)
                        {
                            // 業者カウントを現場名に設定(業者グループのキーが業者名の為、設定の必要がある)
                            gyousyaName = gyousyaCount.ToString();
                        }

                        // 現場名表示フラグがfalseの場合
                        if (!this.genbaNameVisible)
                        {
                            // 現場カウントを現場名に設定(現場グループのキーが現場名の為、設定の必要がある)
                            genbaName = genbaCount.ToString();
                        }

                        // ChouhyouDataTableにデータを追加
                        DataRow dr = this.ChouhyouDataTable.NewRow();
                        dr["PHN_GYOUSHA_NAME_FBL"] = gyousyaName;                               // 業者名
                        dr["PHN_GENBA_NAME_FBL"] = genbaName;                                   // 現場名
                        dr["PHY_DENPYOU_DATE_FLB"] = list[1];                                   // 月日
                        dr["PHY_DENPYOU_NUMBER_FLB"] = list[2];                                 // 売上No.
                        dr["PHY_HINMEI_NAME_FLB"] = list[3];                                    // 品名
                        dr["PHY_SUURYOU_FLB"] = this.SetFormt(list[4], formatSyousuuAri);       // 数量 フォーマット(3桁ごとにカンマ区切りで、小数点第3位までゼロパディング)
                        dr["PHY_UNIT_NAME_FLB"] = list[5];                                      // 単位
                        dr["PHY_TANKA_FLB"] = this.SetFormt(list[6], formatSyousuuAri);         // 単価 フォーマット(3桁ごとにカンマ区切りで、小数点第3位までゼロパディング)
                        dr["PHY_KINGAKU_FLB"] = this.SetFormt(list[7], formatSyousuuNashi);     // 金額 フォーマット(3桁ごとにカンマ区切り)
                        dr["PHY_SHOUHIZEI_FLB"] = this.SetFormt(list[8], formatSyousuuNashi);   // 消費税 フォーマット(3桁ごとにカンマ区切り)
                        dr["PHY_MEISAI_BIKOU_FLB"] = list[9];                                   // 備考
                        this.ChouhyouDataTable.Rows.Add(dr);
                        break;
                        #endregion
                    
                    case "2-4":
                        #region -Detail部-Detail部-
                        // Detail部-Detail部

                        // 2-4があったら現場グループフッタの表示フラグをONにする
                        if (!this.genbaKeiVisible)
                        {
                            this.genbaKeiVisible = true;
                        }

                        // (業者+現場)単位の売上計、消費税計を設定
                        DataRow[] hitGenbaRow;
                        hitGenbaRow = this.ChouhyouDataTable.Select("PHN_GYOUSHA_NAME_FBL = '" + gyousyaName + "' AND PHN_GENBA_NAME_FBL = '" + genbaName + "'");
                        for (int i = 0; i < hitGenbaRow.Length; i++)
                        {
                            hitGenbaRow[i]["PHN_GENBA_KINGAKU_FLB"] = this.SetFormt(list[1], formatSyousuuNashi);        // 現場計：金額
                            hitGenbaRow[i]["PHN_GENBA_SHOUHIZEI_FLB"] = this.SetFormt(list[2], formatSyousuuNashi);      // 現場計：消費税
                        }
                        genbaCount++;       // 現場名が設定されない場合に現場名(Key項目)として使う
                        break;
                        #endregion

                    case "2-5":
                        #region -Detail部-Detail部-
                        // Detail部-Detail部

                        // 2-5があったら業者グループフッタの表示フラグをONにする
                        if (!this.gyousyaKeiVisible)
                        {
                            this.gyousyaKeiVisible = true;
                        }

                        // 業者単位の売上計、消費税計を設定
                        DataRow[] hitGyoushaRow;
                        hitGyoushaRow = this.ChouhyouDataTable.Select("PHN_GYOUSHA_NAME_FBL = '" + gyousyaName + "'");
                        for (int i = 0; i < hitGyoushaRow.Length; i++)
                        {
                            hitGyoushaRow[i]["PHN_GYOUSHA_KINGAKU_FLB"] = this.SetFormt(list[1], formatSyousuuNashi);        // 業者計：金額
                            hitGyoushaRow[i]["PHN_GYOUSHA_SHOUHIZEI_FLB"] = this.SetFormt(list[2], formatSyousuuNashi);      // 業者計：消費税
                        }
                        gyousyaCount++;        // 業者名が設定されない場合に業者名(Key項目)として使う
                        break;
                        #endregion
                    
                    default:
                        break;
                }
            }
        }

        /// <summary> 指定フォーマットに変換した文字列を取得する </summary>
        /// <param name="value">変換対象を表す文字列</param>
        /// <param name="format">指定フォーマットを表す文字列</param>
        /// <returns>指定フォーマットに変換後文字列</returns>
        private string SetFormt(string value, string format)
        {
            // フォーマット変換後文字列
            string ret = string.Empty;

            // 引数がブランクの場合はブランクを返す
            if (value.Trim() != string.Empty)
            {
                //※↓↓↓引数がブランク以外で数値変換できない場合に、引数をそのまま返す時はコメントから外してください↓↓↓
                // ret = value;
                //※↑↑↑引数がブランク以外で数値変換できない場合に、引数をそのまま返す時はコメントから外してください↑↑↑

                // 括弧付の場合に使う変数
                string kakkoStart = "（";
                string kakkoEnd = "）"; 

                // 数値変換時の一時変数
                double temp = 0;

                // 引数が括弧付(内税)の場合は括弧付で返す
                if (kakkoStart == value.Trim().Substring(0, 1))
                {
                    // ◆括弧付の場合

                    // 先頭と末尾の括弧を外す(例： (123456789) ⇒ 123456789 )
                    value = value.Substring(1, value.Length - 2);

                    // 引数の文字列が数値変換可能か
                    if (double.TryParse(value, out temp))
                    {
                        // 数値変換できた場合は指定フォーマットの文字列に変換する
                        ret = string.Format(temp.ToString(format));

                        // 括弧を付け直す(例： 123,456,789 ⇒ (123,456,789) )
                        ret = kakkoStart + ret + kakkoEnd;
                    }
                }
                else
                {
                    // ◆括弧なしの場合

                    // 引数の文字列が数値変換可能か
                    if (double.TryParse(value, out temp))
                    {
                        // 数値変換できた場合は指定フォーマットの文字列に変換する
                        ret = string.Format(temp.ToString(format));
                    }
                }
            }

            return ret;
        }

        /// <summary> 文字列の帳票データを文字列配列データへの変換を実行する </summary>
        /// <param name="tmp">帳票データを表す文字列</param>
        /// <returns>文字列配列の帳票データ</returns>
        private string[] ReportSplit(string tmp)
        {
            // 値が空の項目を半角スペースに置き換える(""⇒" ")
            tmp = tmp.Replace("\"\"", "\" \"");

            // 先頭と末尾の"(ダブルコーテーション)を削除する
            // 先頭と末尾の空白を削除
            tmp = tmp.Trim();

            // 先頭と末尾以外を抽出(先頭と、末尾は"(ダブルコーテーション))
            tmp = tmp.Substring(1, tmp.Length - 2);

            // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
            string[] splt = { "\",\"" };
            string[] ret = tmp.Split(splt, StringSplitOptions.RemoveEmptyEntries);

            return ret;
        }
        #endregion
    }

    #endregion
}
