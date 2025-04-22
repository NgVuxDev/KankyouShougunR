using System;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Utility;
using Shougun.Core.PaperManifest.InsatsuBusuSettei;

namespace Report
{
    public class ReportInfoR494 : Shougun.Core.PaperManifest.InsatsuBusuSettei.Report.ReportInfoBaseG489
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();

        // ヘッダー部分の列定義（1-1）
        // 交付年月日
        private ReportDate koufuDate;
        // 整理番号
        private string seirino = string.Empty;
        // 交付担当者
        private string koufutantousha = string.Empty;
        // 排出事業者名
        private string haishutujigyoushaname = string.Empty;
        // 排出事業者郵便番号
        private string haishutujigyoushapost = string.Empty;
        // 排出事業者電話番号
        private string haishutujigyoushatel = string.Empty;
        // 排出事業者住所
        private string haishutujigyoushaadress = string.Empty;
        // 排出事業場名
        private string haishutujigyoubaname = string.Empty;
        // 排出事業場郵便番号
        private string haishutujigyoubapost = string.Empty;
        // 排出事業場電話番号
        private string haishutujigyoubatel = string.Empty;
        // 排出事業場住所
        private string haishutujigyoubaadress = string.Empty;

        // 20140612 katen 不具合No.4469 start‏
        // 排出事業者名2
        private string haishutujigyoushaname2 = string.Empty;
        // 排出事業者住所2
        private string haishutujigyoushaadress2 = string.Empty;
        // 排出事業場名2
        private string haishutujigyoubaname2 = string.Empty;
        // 排出事業場住所2
        private string haishutujigyoubaadress2 = string.Empty;
        // 20140612 katen 不具合No.4469 end‏

        private string chuukanhaikikbn1 = string.Empty;             // 中間処理産業廃棄物区分・帳簿記載チェック(0.設定なし　1.帳簿記載のとおり　2.当欄記載のとおり)
        private string chuukanhaikikbn2 = string.Empty;             // 中間処理産業廃棄物区分・当欄記載チェック(0.設定なし　1.帳簿記載のとおり　2.当欄記載のとおり)
        private string chuukanhaikibutsu = string.Empty;            // 中間処理産業廃棄物
        //private string lastsbnyoteikbn = string.Empty;            // 最終処分の場所区分
        private string lastsbnyoteikbn1 = string.Empty;             // 委託契約書記載チェック(0.設定なし　1.委託契約書記載のとおり　2.当欄記載のとおり)
        private string lastsbnyoteikbn2 = string.Empty;             // 当欄記載チェック(0.設定なし　1.委託契約書記載のとおり　2.当欄記載のとおり)
        private string lastsbnyoteigenbaname = string.Empty;        // 最終処分の場所現場名称
        private string lastsbnyoteigenbatel = string.Empty;         // 最終処分の場所電話番号
        private string lastsbnyoteigenbapost = string.Empty;        // 最終処分の場所郵便番号
        private string lastsbnyoteigenbaadress = string.Empty;      // 最終処分の場所住所
        private string sbngyoushaname = string.Empty;               // 処分受託者名
        private string sbngyoushapost = string.Empty;               // 処分受託者郵便番号
        private string sbngyoushatel = string.Empty;                // 処分受託者電話番号
        private string sbngyoushaadress = string.Empty;             // 処分受託者住所
        private string bikoutsuusinn = string.Empty;                // 備考・通信欄
        private ReportDate checkB2;                      // 照合確認B2票
        private ReportDate checkB4;                      // 照合確認B4票
        private ReportDate checkB6;                      // 照合確認B6票
        private ReportDate checkD;                      // 照合確認D票
        private ReportDate checkE;                      // 照合確認E票
        // 印刷部数（受渡し項目）
        private string printsets = string.Empty;                    // 印刷部数（受渡し項目）

        // ヘッダー部分の列定義（2-1）
        /// <summary>運搬の1区間あたりの項目数</summary>
        private const int UpnFieldCount = 14;

        /// <summary>積替え又は保管</summary>
        private ManifestRouteElement sbnPlace;

        /// <summary>運搬受託者 区間1-3</summary>
        private ManifestRouteElement[] upnRoute = new ManifestRouteElement[3];

        /// <summary>運搬先区分 区間1-3、0:処分施設　1:積替保管)</summary>
        private string[][] upnSakiKbn = new string[][] { new string[] { "", "" }, new string[] { "", "" }, new string[] { "", "" } };

        /// <summary>運搬先の事業場 区間1-3</summary>
        private ManifestRouteElement[] upnRoutePlace = new ManifestRouteElement[3];

        /// <summary>運搬の受託 区間1-3</summary>
        private UpnJyutakuElement[] upnJyutakuRoute = new UpnJyutakuElement[3];

        /// <summary>処分終了年月日</summary>
        private ReportDate sbnDate;

        /// <summary>最終処分終了年月日</summary>
        private ReportDate lastSbnDate;

        /// <summary>最終処分を行った場所</summary>
        private ManifestRouteElement lastSbnResult;

        /// <summary>最終処分No(利用時はNo.を先頭につけること)</summary>
        private string LAST_SBN_GENBA_NUMBER = string.Empty;

        /// <summary>処分の受託名</summary>
        private string sbnJyutakushaName = string.Empty;

        /// <summary>処分の受託担当者名</summary>
        private string sbnJyutakuTantouName = string.Empty;

        // ヘッダー部分の列定義（3-1）
        private string haikishuruiname = string.Empty;               // 印字廃棄物種類名
        private string prtsuu = string.Empty;                        // 印字数量
        private string prtunitcd = string.Empty;                     // 印字単位名称
        private string prthaikiname = string.Empty;                  // 印字廃棄物名称
        private string prtnisugataname = string.Empty;               // 印字荷姿名称

        private string prtyuugainame = string.Empty;                 // 印字有害物質名
        private string sbnhouhouname = string.Empty;                 // 印字処分方法名

        //斜線(7-1)
        /// <summary>斜線項目有害物質</summary>
        private bool SLASH_YUUGAI_FLG = false;                      //斜線項目有害物質
        /// <summary>斜線項目備考</summary>
        private bool SLASH_BIKOU_FLG = false;                       //斜線項目備考
        /// <summary>斜線項目中間処理産業廃棄物</summary>
        private bool SLASH_CHUUKAN_FLG = false;                     //斜線項目中間処理産業廃棄物
        /// <summary>斜線項目積替保管</summary>
        private bool SLASH_TSUMIHO_FLG = false;                     //斜線項目積替保管
        /// <summary>斜線項目事前協議</summary>
        private bool SLASH_JIZENKYOUGI_FLG = false;                 //斜線項目事前協議
        /// <summary>斜線項目運搬受託者2</summary>
        private bool SLASH_UPN_GYOUSHA2_FLG = false;                //斜線項目運搬受託者2
        /// <summary>斜線項目運搬受託者3</summary>
        private bool SLASH_UPN_GYOUSHA3_FLG = false;                //斜線項目運搬受託者3
        /// <summary>斜線項目運搬の受託者2</summary>
        private bool SLASH_UPN_JYUTAKUSHA2_FLG = false;             //斜線項目運搬の受託者2
        /// <summary>斜線項目運搬の受託者3</summary>
        private bool SLASH_UPN_JYUTAKUSHA3_FLG = false;             //斜線項目運搬の受託者3
        /// <summary>斜線項目運搬先事業場2</summary>
        private bool SLASH_UPN_SAKI_GENBA2_FLG = false;             //斜線項目運搬先事業場2
        /// <summary>斜線項目運搬先事業場3</summary>
        private bool SLASH_UPN_SAKI_GENBA3_FLG = false;             //斜線項目運搬先事業場3
        /// <summary>斜線項目B1票</summary>
        private bool SLASH_B1_FLG = false;                          //斜線項目B1票
        /// <summary>斜線項目B2票</summary>
        private bool SLASH_B2_FLG = false;                          //斜線項目B2票
        /// <summary>斜線項目B4票</summary>
        private bool SLASH_B4_FLG = false;                          //斜線項目B4票
        /// <summary>斜線項目B6票</summary>
        private bool SLASH_B6_FLG = false;                          //斜線項目B6票
        /// <summary>斜線項目D票</summary>
        private bool SLASH_D_FLG = false;                           //斜線項目D票
        /// <summary>斜線項目E票</summary>
        private bool SLASH_E_FLG = false;                           //斜線項目E票

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfoR382"/> class.
        /// </summary>
        public ReportInfoR494()
        {
            // 帳票出力フルパスフォーム名
            //this.OutputFormFullPathName = TEMPLATE_PATH + "R494-Form.xml";
            this.OutputFormFullPathName = "Template/R494-Form.xml";
            //this.OutputFormFullPathName = "R494-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            this.ReportID = "R494";
        }

        /// <summary>
        /// 帳票出力フルパスフォームを保持するプロパティ
        /// </summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>
        /// 帳票出力フォームレイアウトを保持するプロパティ
        /// </summary>
        public string OutputFormLayout { get; set; }

        /// <summary>
        /// C1Reportの帳票データの作成ならびに明細部分の列定義を実行する
        /// </summary>
        public void R494_Report(DataTable chouhyouData)
        {
            LogUtility.DebugMethodStart(chouhyouData);

            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // フォームへ設定する情報処理
            // データテーブル作成処理のみ
            this.chouhyouDataTable = new DataTable();

            //// データテーブル作成処理のみ
            this.SetRecord(this.chouhyouDataTable);
            /// <summary>データテーブル情報から帳票情報作成処理を実行する</summary>
            /// 引数１：XMLレポート定義ファイルの完全名
            /// 引数２：string reportName
            /// 引数３：画面から受け継いだdataTable
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dataTableChouhyouForm);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            LogUtility.DebugMethodStart();

            // ヘッダー部分の列定義（1-1）
            // 交付年月日・年(yy(和暦変更後の年))
            this.koufuDate.SetReportField(new[] { "FH_KOUFU_DATE_1_CTL", "FH_KOUFU_DATE_2_CTL", "FH_KOUFU_DATE_3_CTL" });
            // 整理番号
            this.SetFieldName("FH_SEIRI_ID_CTL", this.seirino);
            // 交付担当者
            this.SetFieldName("FH_KOUFU_TANTOUSHA_CTL", this.koufutantousha);
            // 排出事業者名
            this.SetFieldName("FH_HST_GYOUSHA_NAME_CTL", this.haishutujigyoushaname);
            // 排出事業者郵便番号
            this.SetFieldName("FH_HST_GYOUSHA_POST_CTL", this.haishutujigyoushapost);
            // 排出事業者電話番号
            this.SetFieldName("FH_HST_GYOUSHA_TEL_CTL", this.haishutujigyoushatel);
            // 排出事業者住所
            this.SetFieldName("FH_HST_GYOUSHA_ADDRESS_CTL", this.haishutujigyoushaadress);
            // 排出事業場名
            this.SetFieldName("FH_HST_GENBA_NAME_CTL", this.haishutujigyoubaname);
            // 排出事業場郵便番号
            this.SetFieldName("FH_HST_GENBA_POST_CTL", this.haishutujigyoubapost);
            // 排出事業場電話番号
            this.SetFieldName("FH_HST_GENBA_TEL_CTL", this.haishutujigyoubatel);
            // 排出事業場住所
            this.SetFieldName("FH_HST_GENBA_ADDRESS_CTL", this.haishutujigyoubaadress);

            // 20140612 katen 不具合No.4469 start‏
            // 排出事業者名
            this.SetFieldName("FH_HST_GYOUSHA_NAME2_CTL", this.haishutujigyoushaname2);
            // 排出事業者住所
            this.SetFieldName("FH_HST_GYOUSHA_ADDRESS2_CTL", this.haishutujigyoushaadress2);
            // 排出事業場名
            this.SetFieldName("FH_HST_GENBA_NAME2_CTL", this.haishutujigyoubaname2);
            // 排出事業場住所
            this.SetFieldName("FH_HST_GENBA_ADDRESS2_CTL", this.haishutujigyoubaadress2);
            // 20140612 katen 不具合No.4469 end

            this.SetFieldName("FH_CHUUKAN_HAIKI_KBN1_CTL", this.chuukanhaikikbn1);             // 中間処理産業廃棄物区分・帳簿記載チェック(0.設定なし　1.帳簿記載のとおり　2.当欄記載のとおり)
            this.SetFieldName("FH_CHUUKAN_HAIKI_KBN2_CTL", this.chuukanhaikikbn2);             // 中間処理産業廃棄物区分・当欄記載チェック(0.設定なし　1.帳簿記載のとおり　2.当欄記載のとおり)
            this.SetFieldName("FH_CHUUKAN_HAIKI_CTL", this.chuukanhaikibutsu);                 // 中間処理産業廃棄物
            //this.SetFieldName("FH_KOUFU_DATE_2_CTL",  lastsbnyoteikbn);                      // 最終処分の場所区分
            this.SetFieldName("FH_LAST_SBN_YOTEI_KBN1_CTL", this.lastsbnyoteikbn1);            // 委託契約書記載チェック(0.設定なし　1.委託契約書記載のとおり　2.当欄記載のとおり)
            this.SetFieldName("FH_LAST_SBN_YOTEI_KBN2_CTL", this.lastsbnyoteikbn2);            // 当欄記載チェック(0.設定なし　1.委託契約書記載のとおり　2.当欄記載のとおり)
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_NAME_CTL", this.lastsbnyoteigenbaname); // 最終処分の場所現場名称
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_POST_CTL", this.lastsbnyoteigenbapost); // 最終処分の場所郵便番号
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_TEL_CTL", this.lastsbnyoteigenbatel);   // 最終処分の場所電話番号         
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_ADDRESS_CTL", this.lastsbnyoteigenbaadress);      // 最終処分の場所住所
            this.SetFieldName("FH_SBN_GYOUSHA_NAME_CTL", this.sbngyoushaname);                 // 処分受託者名
            this.SetFieldName("FH_SBN_GYOUSHA_POST_CTL", this.sbngyoushapost);                 // 処分受託者郵便番号
            this.SetFieldName("FH_SBN_GYOUSHA_TEL_CTL", this.sbngyoushatel);                   // 処分受託者電話番号
            this.SetFieldName("FH_SBN_GYOUSHA_ADDRESS_CTL", this.sbngyoushaadress);            // 処分受託者住所
            this.SetFieldName("FH_BIKOU_CTL", this.bikoutsuusinn);                             // 備考・通信欄

            // 照合確認B2票
            this.koufuDate.SetReportField(new[] { "FH_CHECK_B2_1_CTL", "FH_CHECK_B2_2_CTL", "FH_CHECK_B2_3_CTL" });
            // 照合確認B4票
            this.checkB4.SetReportField(new[] { "FH_CHECK_B4_1_CTL", "FH_CHECK_B4_2_CTL", "FH_CHECK_B4_3_CTL" });
            // 照合確認B6票
            this.checkB6.SetReportField(new[] { "FH_CHECK_B6_1_CTL", "FH_CHECK_B6_2_CTL", "FH_CHECK_B6_3_CTL" });
            // 照合確認D票
            this.checkD.SetReportField(new[] { "FH_CHECK_D_1_CTL", "FH_CHECK_D_2_CTL", "FH_CHECK_D_3_CTL" });
            // 照合確認E票
            this.checkE.SetReportField(new[] { "FH_CHECK_E_1_CTL", "FH_CHECK_E_2_CTL", "FH_CHECK_E_3_CTL" });

            // ヘッダー部分の列定義（2-1）
            this.sbnPlace.SetReportField(new string[] { "FH_UPN_SAKI_GENBA_NAME4_CTL", "FH_UPN_SAKI_GENBA_POST4_CTL", "FH_UPN_SAKI_GENBA_TEL4_CTL", "FH_UPN_SAKI_GENBA_ADDRESS4_CTL" });

            for (var i = 0; i < 3; i++)
            {
                // 運搬先区分
                this.SetFieldName(String.Format("FH_UPN_SAKI_KBN{0}_1_CTL", i + 1), this.upnSakiKbn[i][0]);
                this.SetFieldName(String.Format("FH_UPN_SAKI_KBN{0}_2_CTL", i + 1), this.upnSakiKbn[i][1]);

                // 運搬受託者
                this.upnRoute[i].SetReportField(new string[] { 
                                                        String.Format("FH_UPN_GYOUSHA_NAME{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_GYOUSHA_POST{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_GYOUSHA_TEL{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_GYOUSHA_ADDRESS{0}_CTL", i + 1)});
                // 運搬先
                this.upnRoutePlace[i].SetReportField(new string[] { 
                                                        String.Format("FH_UPN_SAKI_GENBA_NAME{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_SAKI_GENBA_POST{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_SAKI_GENBA_TEL{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_SAKI_GENBA_ADDRESS{0}_CTL", i + 1)});
                // 運搬の受託
                this.upnJyutakuRoute[i].SetReportField(new string[] { 
                                                        String.Format("FH_UPN_JYUTAKUSHA_NAME{0}_CTL", i + 1), 
                                                        String.Format("FH_UNTENSHA_NAME{0}_CTL", i + 1), 
                                                        String.Format("FH_UNPAN_DATE{0}_Y", i + 1), 
                                                        String.Format("FH_UNPAN_DATE{0}_M", i + 1), 
                                                        String.Format("FH_UNPAN_DATE{0}_D", i + 1), 
                                                        String.Format("FH_YUUKA_SUU{0}", i + 1), 
                                                        String.Format("FH_YUUKA_UNIT{0}_NAME", i + 1)});
            }

            this.sbnDate.SetReportField(new[] { "FH_SBN_DATE_Y", "FH_SBN_DATE_M", "FH_SBN_DATE_D" });

            this.lastSbnDate.SetReportField(new[] { "FH_LAST_DATE_Y", "FH_LAST_DATE_M", "FH_LAST_DATE_D" });

            this.SetFieldName("FH_SBN_JYUTAKUSHA_NAME", this.sbnJyutakushaName);
            this.SetFieldName("FH_SBN_TANTOU_NAME", this.sbnJyutakuTantouName);

            this.lastSbnResult.SetReportField(new string[] { "FH_LAST_GYOUSHA_NAME", "FH_LAST_GYOUSHA_POST", "FH_LAST_GYOUSHA_TEL", "FH_LAST_GYOUSHA_ADDRESS" });

            this.SetFieldName("FH_LAST_SBN_GENBA_NUMBER", this.LAST_SBN_GENBA_NUMBER);         // 処分先No.

            // ヘッダー部分の列定義（3-1）
            this.SetFieldName("FH_HAIKI_SHURUI_NAME_CTL", this.haikishuruiname);               // 印字廃棄物種類名
            this.SetFieldName("FH_PRT_SUU_CTL", this.prtsuu);                                  // 印字数量
            this.SetFieldName("FH_UNIT_NAME_CTL", this.prtunitcd);                             // 印字単位名称
            this.SetFieldName("FH_PRT_HAIKI_NAME_CTL", this.prthaikiname);                     // 印字廃棄物名称
            this.SetFieldName("FH_PRT_NISUGATA_NAME_CTL", this.prtnisugataname);               // 印字荷姿名称
            this.SetFieldName("FH_PRT_YUUGAI_NAME_CTL", this.prtyuugainame);                   // 印字有害物質名
            this.SetFieldName("FH_PRT_SBN_HOUHOU_NAME_CTL", this.sbnhouhouname);               // 印字処分方法名     



            //斜線制御(5-1)
            bool slashVisivle = false;

            //積替
            slashVisivle = this.SLASH_TSUMIHO_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_TSUMIHO_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_NAME4_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_POST4_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_TEL4_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_ADDRESS4_CTL", !slashVisivle);

            //有害
            slashVisivle = this.SLASH_YUUGAI_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_YUUGAI_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_PRT_YUUGAI_NAME_CTL", !slashVisivle);

            //備考
            slashVisivle = this.SLASH_BIKOU_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_BIKOU_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_BIKOU_CTL", !slashVisivle);

            //中間
            slashVisivle = this.SLASH_CHUUKAN_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_CHUUKAN_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_CHUUKAN_HAIKI_KBN1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHUUKAN_HAIKI_KBN2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHUUKAN_HAIKI_CTL", !slashVisivle);



            //運搬業者2
            slashVisivle = this.SLASH_UPN_GYOUSHA2_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_UPN_GYOUSHA2_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_GYOUSHA_NAME2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_POST2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_TEL2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_ADDRESS2_CTL", !slashVisivle);

            //運搬業者3
            slashVisivle = this.SLASH_UPN_GYOUSHA3_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_UPN_GYOUSHA3_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_GYOUSHA_NAME3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_POST3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_TEL3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_ADDRESS3_CTL", !slashVisivle);

            //運搬先現場2
            slashVisivle = this.SLASH_UPN_SAKI_GENBA2_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_UPN_SAKI_GENBA2_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_NAME2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_POST2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_TEL2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_ADDRESS2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_KBN2_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_KBN2_2_CTL", !slashVisivle);

            

            //運搬先現場3
            slashVisivle = this.SLASH_UPN_SAKI_GENBA3_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_UPN_SAKI_GENBA3_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_NAME3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_POST3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_TEL3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_GENBA_ADDRESS3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_KBN3_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_SAKI_KBN3_2_CTL", !slashVisivle);


            //運搬受託2
            slashVisivle = this.SLASH_UPN_JYUTAKUSHA2_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_UPN_JYUTAKUSHA2_FLG1", slashVisivle);
            this.SetFieldVisible("FH_PRT_SLASH_UPN_JYUTAKUSHA2_FLG2", slashVisivle);
            this.SetFieldVisible("FH_PRT_SLASH_UPN_JYUTAKUSHA2_FLG3", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_JYUTAKUSHA_NAME2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UNTENSHA_NAME2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE2_Y", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE2_M", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE2_D", !slashVisivle);
            this.SetFieldVisible("FH_YUUKA_SUU2", !slashVisivle);
            this.SetFieldVisible("FH_YUUKA_UNIT2_NAME", !slashVisivle);

            //運搬受託3
            slashVisivle = this.SLASH_UPN_JYUTAKUSHA3_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_UPN_JYUTAKUSHA3_FLG1", slashVisivle);
            this.SetFieldVisible("FH_PRT_SLASH_UPN_JYUTAKUSHA3_FLG2", slashVisivle);
            this.SetFieldVisible("FH_PRT_SLASH_UPN_JYUTAKUSHA3_FLG3", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_JYUTAKUSHA_NAME3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UNTENSHA_NAME3_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE3_Y", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE3_M", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE3_D", !slashVisivle);
            this.SetFieldVisible("FH_YUUKA_SUU3", !slashVisivle);
            this.SetFieldVisible("FH_YUUKA_UNIT3_NAME", !slashVisivle);


            //B4
            slashVisivle = this.SLASH_B4_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_B4_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_CHECK_B4_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B4_2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B4_3_CTL", !slashVisivle);

            //B6
            slashVisivle = this.SLASH_B6_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_B6_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_CHECK_B6_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B6_2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B6_3_CTL", !slashVisivle);



            //照合確認関係は一旦すべて非表示とする
            this.SetFieldVisible("FH_CHECK_B1_1_CTL", false);
            this.SetFieldVisible("FH_CHECK_B1_2_CTL", false);
            this.SetFieldVisible("FH_CHECK_B1_3_CTL", false);
            this.SetFieldVisible("FH_PRT_SLASH_B1_FLG", false);

            this.SetFieldVisible("FH_CHECK_B2_1_CTL", false);
            this.SetFieldVisible("FH_CHECK_B2_2_CTL", false);
            this.SetFieldVisible("FH_CHECK_B2_3_CTL", false);
            this.SetFieldVisible("FH_PRT_SLASH_B2_FLG", false);

            this.SetFieldVisible("FH_CHECK_B4_1_CTL", false);
            this.SetFieldVisible("FH_CHECK_B4_2_CTL", false);
            this.SetFieldVisible("FH_CHECK_B4_3_CTL", false);
            this.SetFieldVisible("FH_PRT_SLASH_B4_FLG", false);

            this.SetFieldVisible("FH_CHECK_B6_1_CTL", false);
            this.SetFieldVisible("FH_CHECK_B6_2_CTL", false);
            this.SetFieldVisible("FH_CHECK_B6_3_CTL", false);
            this.SetFieldVisible("FH_PRT_SLASH_B6_FLG", false);

            this.SetFieldVisible("FH_CHECK_D_1_CTL", false);
            this.SetFieldVisible("FH_CHECK_D_2_CTL", false);
            this.SetFieldVisible("FH_CHECK_D_3_CTL", false);
            this.SetFieldVisible("FH_PRT_SLASH_D_FLG", false);

            this.SetFieldVisible("FH_CHECK_E_1_CTL", false);
            this.SetFieldVisible("FH_CHECK_E_2_CTL", false);
            this.SetFieldVisible("FH_CHECK_E_3_CTL", false);
            this.SetFieldVisible("FH_PRT_SLASH_E_FLG", false);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票データより、C1Reportに渡すデータを作成する
        /// </summary>
        /// <param name="dataTable">dataTable</param>
        private void InputDataToMem(DataTable dataTable)
        {
            LogUtility.DebugMethodStart(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                // row[0]を文字列に変換しtempに格納
                string tmp = row[0].ToString();
                //// ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                //    //string[] splt = { "\",\"" };
                string[] list = ReportCommonUtility.ReportSplit(tmp);

                // ヘッダ部・データ種類(list[0])により分岐   
                if (list[0] == "1-1")
                {
                    // 交付年月日
                    this.koufuDate = new ReportDate(this.SetFieldName, list[1], false, "yyyy");
                    // 照合確認B2票
                    this.checkB2 = new ReportDate(this.SetFieldName, list[24]);
                    // 照合確認B4票
                    this.checkB4 = new ReportDate(this.SetFieldName, list[25]);
                    // 照合確認B6票
                    this.checkB6 = new ReportDate(this.SetFieldName, list[26]);
                    // 照合確認D票
                    this.checkD = new ReportDate(this.SetFieldName, list[27]);
                    // 照合確認E票
                    this.checkE = new ReportDate(this.SetFieldName, list[28]);

                    // 整理番号
                    this.seirino = list[2];
                    // 交付担当者
                    this.koufutantousha = list[3];
                    // 排出事業者名
                    this.haishutujigyoushaname = list[4];
                    // 排出事業者郵便番号
                    this.haishutujigyoushapost = list[5];
                    // 排出事業者電話番号
                    this.haishutujigyoushatel = list[6];
                    // 排出事業場住所
                    this.haishutujigyoushaadress = list[7];
                    // 排出事業場名称
                    this.haishutujigyoubaname = list[8];
                    // 排出事業場郵便番号
                    this.haishutujigyoubapost = list[9];
                    // 排出事業場電話番号
                    this.haishutujigyoubatel = list[10];
                    // 排出事業場住所
                    this.haishutujigyoubaadress = list[11];

                    // 20140612 katen 不具合No.4469 start‏
                    if (list[4].Length > 40)
                    {
                        // 排出事業者名1
                        this.haishutujigyoushaname = list[4].Substring(0, 40).TrimEnd(' ');
                        // 排出事業者名2
                        this.haishutujigyoushaname2 = list[4].Substring(40);
                    }
                    else
                    {
                        // 排出事業者名1
                        this.haishutujigyoushaname = list[4].TrimEnd(' ');
                    }
                    if (list[7].Length > 48)
                    {
                        // 排出事業場住所1
                        this.haishutujigyoushaadress = list[7].Substring(0, 48).TrimEnd(' ');
                        // 排出事業場住所2
                        this.haishutujigyoushaadress2 = list[7].Substring(48);
                    }
                    else
                    {
                        // 排出事業場住所1
                        this.haishutujigyoushaadress = list[7].TrimEnd(' ');
                    }
                    if (list[8].Length > 40)
                    {
                        // 排出事業場名称1
                        this.haishutujigyoubaname = list[8].Substring(0, 40).TrimEnd(' ');
                        // 排出事業場名称2
                        this.haishutujigyoubaname2 = list[8].Substring(40);
                    }
                    else
                    {
                        // 排出事業場名称1
                        this.haishutujigyoubaname = list[8].TrimEnd(' ');
                    }
                    if (list[11].Length > 48)
                    {
                        // 排出事業場住所1
                        this.haishutujigyoubaadress = list[11].Substring(0, 48).TrimEnd(' ');
                        // 排出事業場住所2
                        this.haishutujigyoubaadress2 = list[11].Substring(48);
                    }
                    else
                    {
                        // 排出事業場住所1
                        this.haishutujigyoubaadress = list[11].TrimEnd(' ');
                    }
                    // 20140612 katen 不具合No.4469 end‏

                    // 中間処理産業廃棄物区分("1"の場合は"ㇾ"、"0"の場合は"")
                    // 帳簿記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
                    // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
                    if (list[12] == "1")
                    {
                        // 中間処理産業廃棄物区分
                        // 帳簿記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")                      
                        this.chuukanhaikikbn1 = "✓";
                    }
                    else if (list[12] == "2")
                    {
                        // 中間処理産業廃棄物区分
                        // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
                        this.chuukanhaikikbn2 = "✓";
                    }

                    // 中間処理産業廃棄物
                    this.chuukanhaikibutsu = list[13];

                    // 最終処分の場所区分・委託契約書記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
                    if (list[14] == "1")
                    {
                        this.lastsbnyoteikbn1 = "✓";
                    }
                    else if (list[14] == "2")
                    {
                        // 最終処分の場所区分・当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
                        this.lastsbnyoteikbn2 = "✓";
                    }
                    // 最終処分の場所現場名称
                    this.lastsbnyoteigenbaname = list[15];
                    // 最終処分の場所郵便番号
                    this.lastsbnyoteigenbapost = ReportCommonUtility.AddPostMark(list[16]);
                    // 最終処分の場所電話番号
                    this.lastsbnyoteigenbatel = list[17];
                    // 最終処分の場所住所
                    this.lastsbnyoteigenbaadress = list[18];
                    // 処分受託者名
                    this.sbngyoushaname = list[19];
                    // 処分受託者郵便番号
                    this.sbngyoushapost = list[20];
                    // 処分受託者電話番号
                    this.sbngyoushatel = list[21];
                    // 処分受託者住所
                    this.sbngyoushaadress = list[22];
                    // 備考・通信欄
                    this.bikoutsuusinn = list[23];

                    // 印刷部数（受渡し項目）
                    this.printsets = list[29];
                }
                else if (list[0] == "2-1")
                {
                    // 明細部分の列定義（2-1）
                    for (var i = 0; i < this.upnRoute.Length; i++)
                    {
                        var idx = i * ReportInfoR494.UpnFieldCount;
                        // 運搬先区分 (1.処分施設　2.積替保管)
                        if (list[idx + 1] == "1")
                        {
                            this.upnSakiKbn[i][0] = "✓";
                        }
                        else if (list[idx + 1] == "2")
                        {
                            this.upnSakiKbn[i][1] = "✓";
                        }

                        // 運搬受託者
                        this.upnRoute[i] = new ManifestRouteElement(this.SetFieldName)
                        {
                            Name = list[idx + 2],
                            Post = list[idx + 3],
                            Tel = list[idx + 4],
                            Address = list[idx + 5]
                        };

                        // 運搬先の事業場
                        this.upnRoutePlace[i] = new ManifestRouteElement(this.SetFieldName)
                        {
                            Name = list[idx + 6],
                            Post = list[idx + 7],
                            Tel = list[idx + 8],
                            Address = list[idx + 9]
                        };

                        // 運搬の受託
                        this.upnJyutakuRoute[i] = new UpnJyutakuElement(this.SetFieldName)
                        {
                            Name = list[idx + 10],
                            Person = list[idx + 11],
                            EndDate = new ReportDate(this.SetFieldName,list[idx + 12], false, "yyyy"),
                            Number = this.FormatSuuryo(list[idx + 13]),
                            Unit = list[idx + 14]
                        };
                    }

                    // 処分の受託 43(14*3)～
                    this.sbnJyutakushaName = list[43];
                    this.sbnJyutakuTantouName = list[44];

                    this.sbnDate = new ReportDate(this.SetFieldName, list[45], false, "yyyy");

                    this.lastSbnDate = new ReportDate(this.SetFieldName, list[46], false, "yyyy");

                    // 最終処分を行った場所
                    this.lastSbnResult = new ManifestRouteElement(this.SetFieldName)
                    {
                        Name = list[47],
                        Post = ReportCommonUtility.AddPostMark(list[48]), //空でなければ郵便記号を付ける
                        Tel = list[49],
                        Address = list[50]
                    };

                    // 積替え又は保管
                    this.sbnPlace = new ManifestRouteElement(this.SetFieldName)
                    {
                        Name = list[51],
                        Post = list[52],
                        Tel = list[53],
                        Address = list[54]
                    };

                    //最終処分を行った場所
                    this.LAST_SBN_GENBA_NUMBER = ReportCommonUtility.AddNoMark(list[55]); //空でなければ"No."を付ける


                }
                else if (list[0] == "3-1")
                {
                    this.haikishuruiname = list[1];              // 印字廃棄物種類名
                    this.prtsuu = this.FormatSuuryo(list[2]);    // 印字数量
                    this.prtunitcd = list[3];                    // 印字単位名称
                    this.prthaikiname = list[4];                 // 印字廃棄物名称
                    this.prtnisugataname = list[5];              // 印字荷姿名称
                    this.prtyuugainame = list[6];                // 印字有害物質名
                    this.sbnhouhouname = list[7];                // 印字処分方法名
                }
                else if (list[0] == "7-1")
                {
                    // 斜線（7-1）
                    this.SLASH_YUUGAI_FLG = bool.Parse(list[1]); //斜線項目有害物質
                    this.SLASH_BIKOU_FLG = bool.Parse(list[2]); //斜線項目備考
                    this.SLASH_CHUUKAN_FLG = bool.Parse(list[3]); //斜線項目中間処理産業廃棄物
                    this.SLASH_TSUMIHO_FLG = bool.Parse(list[4]); //斜線項目積替保管
                    this.SLASH_JIZENKYOUGI_FLG = bool.Parse(list[5]); //斜線項目事前協議
                    this.SLASH_UPN_GYOUSHA2_FLG = bool.Parse(list[6]); //斜線項目運搬受託者2
                    this.SLASH_UPN_GYOUSHA3_FLG = bool.Parse(list[7]); //斜線項目運搬受託者3
                    this.SLASH_UPN_JYUTAKUSHA2_FLG = bool.Parse(list[8]); //斜線項目運搬の受託者2
                    this.SLASH_UPN_JYUTAKUSHA3_FLG = bool.Parse(list[9]); //斜線項目運搬の受託者3
                    this.SLASH_UPN_SAKI_GENBA2_FLG = bool.Parse(list[10]); //斜線項目運搬先事業場2
                    this.SLASH_UPN_SAKI_GENBA3_FLG = bool.Parse(list[11]); //斜線項目運搬先事業場3
                    this.SLASH_B1_FLG = bool.Parse(list[12]); //斜線項目B1票
                    this.SLASH_B2_FLG = bool.Parse(list[13]); //斜線項目B2票
                    this.SLASH_B4_FLG = bool.Parse(list[14]); //斜線項目B4票
                    this.SLASH_B6_FLG = bool.Parse(list[15]); //斜線項目B6票
                    this.SLASH_D_FLG = bool.Parse(list[16]); //斜線項目D票
                    this.SLASH_E_FLG = bool.Parse(list[17]); //斜線項目E票
                }
            }

            LogUtility.DebugMethodEnd();
        }
    }
}
