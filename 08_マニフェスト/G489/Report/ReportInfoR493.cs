using System;
using System.Data;
using System.Linq;
using CommonChouhyouPopup.App;
using r_framework.Utility;
using Shougun.Core.PaperManifest.InsatsuBusuSettei;
using r_framework.Entity;
using Shougun.Core.Common.BusinessCommon;

namespace Report
{
    /// <summary>
    /// を表すクラス・コントロール
    /// </summary>
    public class ReportInfoR493 : Shougun.Core.PaperManifest.InsatsuBusuSettei.Report.ReportInfoBaseG489
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();
        // <summary>List<T>クラスの（Detail型）のリストとしてインスタンス</summary>
        //private List<Detail> detail = new List<Detail>();

        // 交付年月日・年(yy(和暦変更後の年))
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

        private string chuukanhaikikbn1 = string.Empty;             // 中間処理産業廃棄物区分・帳簿記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
        private string chuukanhaikikbn2 = string.Empty;             // 中間処理産業廃棄物区分・当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
        private string chuukanhaikibutsu = string.Empty;            // 中間処理産業廃棄物
        //private string lastsbnyoteikbn = string.Empty;            // 最終処分の場所区分
        private string lastsbnyoteikbn1 = string.Empty;             // 委託契約書記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
        private string lastsbnyoteikbn2 = string.Empty;             // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
        private string lastsbnyoteigenbaname = string.Empty;        // 最終処分の場所名
        private string lastsbnyoteigenbatel = string.Empty;         // 最終処分の場所電話番号
        private string lastsbnyoteigenbapost = string.Empty;        // 最終処分の場所郵便番号
        private string lastsbnyoteigenbaadress = string.Empty;      // 最終処分の場所住所
        private string sbngyoushaname = string.Empty;               // 処分受託者名
        private string sbngyoushapost = string.Empty;               // 処分受託者郵便番号
        private string sbngyoushatel = string.Empty;                // 処分受託者電話番号
        private string sbngyoushaadress = string.Empty;             // 処分受託者住所
        private string tmhgenbaname = string.Empty;                 // 積換保管名
        private string tmhgenbapost = string.Empty;                 // 積換保管郵便番号
        private string tmhgenbatel = string.Empty;                  // 積換保管電話番号
        private string tmhgenbaadress = string.Empty;               // 積換保管住所
        private ReportDate checkB2Date;              // 照合確認B2票
        private ReportDate checkDDate;               // 照合確認D票
        private ReportDate checkEDate;               // 照合確認E票
        // 保留中・印字種類（普通）
        private string ingiv1 = string.Empty;                       // 印字種類（普通）
        // 保留中・印字種類（特管）
        private string ingiv2 = string.Empty;                       // 印字種類（特管）
        // 印刷部数（受渡し項目）
        private string printsets = string.Empty;                    // 印刷部数（受渡し項目）
        private string kamimainh = string.Empty;                    // マニフェスト用紙へ印字
        private string kamimainu = string.Empty;                    // マニフェスト用紙へ印字
        private string kamimaint = string.Empty;                    // マニフェスト用紙へ印字
        private string kamimains = string.Empty;                    // マニフェスト用紙へ印字
        private string sbngyoushacd = string.Empty;                    // 処分受託者CD（業者CD）
        private string kamimainkbn = string.Empty;                    // ﾏﾆ用紙へ印字

        // 明細部分の列定義（2-1）
        private string upngyoushaname = string.Empty;               // 運搬受託者名
        private string upngyoushapost = string.Empty;               // 運搬受託者郵便番号
        private string upngyoushatel = string.Empty;                // 運搬受託者電話番号
        private string upngyoushaadress = string.Empty;             // 運搬受託者住所
        private string upnsakigenbaname = string.Empty;             // 運搬先の事業場名
        private string upnsakigenbapost = string.Empty;             // 運搬先の事業場郵便番号
        private string upnsakigenbatel = string.Empty;              // 運搬先の事業場電話番号
        private string upnsakigenbaadress = string.Empty;           // 運搬先の事業場住所
        private string upnsakigenbacd = string.Empty;                    // 運搬先の事業場CD（現場CD）

        /// <summary>運搬の受託</summary>
        private UpnJyutakuElement upnJyutakuRoute;

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

        /// <summary>明細部分の列定義（3-1）</summary>
        private string[] recno = new string[44];
        #region Index説明
        //0  燃えがらチェック
        //1  汚泥チェック
        //2  廃油チェック
        //3  廃酸チェック
        //4  廃アルカリチェック
        //5  廃ﾌﾟﾗｽﾁｯｸ類チェック
        //6  紙くずチェック
        //7  木くずチェック
        //8  繊維くずチェック
        //9  動植物性残さチェック
        //10 ゴムくずチェック
        //11 金属くずチェック
        //12 ガラス・陶磁器くずチェック
        //13 鉱さいチェック
        //14 がれき類チェック
        //15 家畜の糞尿チェック
        //16 家畜の死体チェック
        //17 ばいじんチェック
        //18 13号廃棄物チェック
        //19 動物系固形不要物チェック
        //20 廃棄物種類-1チェック
        //21 引火性廃油チェック
        //22 引火性廃油(有害)チェック
        //23 強酸チェック
        //24 強酸(有害)チェック
        //25 強アルカリチェック
        //26 強アルカリ(有害)チェック
        //27 感染性廃棄物チェック
        //28 ＰＣＢ等チェック
        //29 廃石綿等チェック
        //30 指定汚水汚泥チェック
        //31 鉱さい(有害)チェック
        //32 燃えがら(有害)チェック
        //33 廃油(有害)チェック
        //34 汚泥(有害)チェック
        //35 廃酸(有害)チェック
        //36 廃アルカリ(有害)チェック
        //37 はいじん(有害)チェック
        //38 13号廃棄物(有害)チェック
        //39 廃棄物種類-3チェック
        //40 廃棄物種類-4チェック
        //41 廃棄物種類-5チェック
        //42 廃棄物種類-6チェック

        #endregion

        private string haikishuruicd21 = string.Empty;              // 廃棄物種類コード-1
        private string haikishuruiname21 = string.Empty;            // 廃棄物種類名-1
        private string haikishuruicd22 = string.Empty;              // 廃棄物種類コード-1
        private string haikishuruiname22 = string.Empty;            // 廃棄物種類名-2
        private string haikishuruicd41 = string.Empty;              // 廃棄物種類コード-3
        private string haikishuruiname41 = string.Empty;            // 廃棄物種類名-3
        private string haikishuruicd42 = string.Empty;              // 廃棄物種類コード-4
        private string haikishuruiname42 = string.Empty;            // 廃棄物種類名-4
        private string haikishuruicd43 = string.Empty;              // 廃棄物種類コード-5
        private string haikishuruiname43 = string.Empty;            // 廃棄物種類名-5
        private string haikishuruicd44 = string.Empty;              // 廃棄物種類コード-6
        private string haikishuruiname44 = string.Empty;            // 廃棄物種類名-6

        // 明細部分の列定義（4-1）
        private string prtsuu = string.Empty;                       // 数量
        private string unitname = string.Empty;                     // 単位名
        private string prtnisugataname = string.Empty;              // 荷姿名
        private string prthaikiname = string.Empty;                 // 産業廃棄物の名称名
        private string prtyuugainame = string.Empty;                // 有害物質等名
        private string prtsbnhouhouname = string.Empty;             // 処分方法名
        private string bikoutsuusinn = string.Empty;                // 備考・通信欄


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



        
        
        /// <summary>Initializes a new instance of the <see cref="ReportInfoR382"/> class.</summary>
        public ReportInfoR493()
        {
            // 帳票出力フルパスフォーム名
            //this.OutputFormFullPathName = TEMPLATE_PATH + "R493-Form.xml";
            this.OutputFormFullPathName = "Template/R493-Form.xml";
            //this.OutputFormFullPathName = "R493-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            this.ReportID = "R493";
        }


        /// <summary>帳票出力フルパスフォームを保持するプロパティ</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>
        /// 帳票出力フォームレイアウトを保持するプロパティ
        /// </summary>
        public string OutputFormLayout { get; set; }

        /// <summary>C1Reportの帳票データの作成ならびに明細部分の列定義を実行する</summary>
        /// <param name="chouhyouData">chouhyouData</param>
        public void R493_Report(DataTable chouhyouData)
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
                //TODO:交付担当者所属の追加

            // ヘッダ部
            // 交付年月日
            this.koufuDate.SetReportField(new[] { "FH_KOUFU_DATE_1_CTL", "FH_KOUFU_DATE_2_CTL", "FH_KOUFU_DATE_3_CTL" });
            this.SetFieldName("FH_SEIRI_ID_CTL", this.seirino);                                     // 整理番号
            this.SetFieldName("FH_KOUFU_TANTOUSHA_CTL", this.koufutantousha);                       // 交付担当者

            this.SetFieldName("FH_HST_GYOUSHA_NAME_CTL", this.haishutujigyoushaname);               // 排出事業者名
            this.SetFieldName("FH_HST_GYOUSHA_POST_CTL", this.haishutujigyoushapost);               // 排出事業者郵便番号
            this.SetFieldName("FH_HST_GYOUSHA_TEL_CTL", this.haishutujigyoushatel);                 // 排出事業者電話番号
            this.SetFieldName("FH_HST_GYOUSHA_ADDRESS_CTL", this.haishutujigyoushaadress);          // 排出事業者住所
            this.SetFieldName("FH_HST_GENBA_NAME_CTL", this.haishutujigyoubaname);                  // 排出事業場名
            this.SetFieldName("FH_HST_GENBA_POST_CTL", this.haishutujigyoubapost);                  // 排出事業場郵便番号
            this.SetFieldName("FH_HST_GENBA_TEL_CTL", this.haishutujigyoubatel);                    // 排出事業場電話番号
            this.SetFieldName("FH_HST_GENBA_ADDRESS_CTL", this.haishutujigyoubaadress);             // 排出事業場住所

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

            this.SetFieldName("FH_CHUUKAN_HAIKI_KBN1_CTL", this.chuukanhaikikbn1);                  // 帳簿記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_CHUUKAN_HAIKI_KBN2_CTL", this.chuukanhaikikbn2);                  // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_CHUUKAN_HAIKI_CTL", this.chuukanhaikibutsu);                      // 中間処理産業廃棄物
            this.SetFieldName("FH_LAST_SBN_YOTEI_KBN1_CTL", this.lastsbnyoteikbn1);                 // 委託契約書記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_LAST_SBN_YOTEI_KBN2_CTL", this.lastsbnyoteikbn2);                 // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_NAME_CTL", this.lastsbnyoteigenbaname);      // 最終処分の場所名
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_TEL_CTL", this.lastsbnyoteigenbatel);        // 最終処分の場所電話番号
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_POST_CTL", this.lastsbnyoteigenbapost);      // 最終処分の場所郵便番号
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_ADDRESS_CTL", this.lastsbnyoteigenbaadress); // 最終処分の場所住所
            this.SetFieldName("FH_SBN_GYOUSHA_NAME_CTL", this.sbngyoushaname);                      // 処分受託者名
            this.SetFieldName("FH_SBN_GYOUSHA_POST_CTL", this.sbngyoushapost);                      // 処分受託者郵便番号
            this.SetFieldName("FH_SBN_GYOUSHA_TEL_CTL", this.sbngyoushatel);                        // 処分受託者電話番号
            this.SetFieldName("FH_SBN_GYOUSHA_ADDRESS_CTL", this.sbngyoushaadress);                 // 処分受託者住所
            this.SetFieldName("FH_TMH_GENBA_NAME_CTL", this.tmhgenbaname);                          // 積換保管名
            this.SetFieldName("FH_TMH_GENBA_POST_CTL", this.tmhgenbapost);                          // 積換保管郵便番号
            this.SetFieldName("FH_TMH_GENBA_TEL_CTL", this.tmhgenbatel);                            // 積換保管電話番号
            this.SetFieldName("FH_TMH_GENBA_ADDRESS_CTL", this.tmhgenbaadress);                     // 積換保管住所
            this.checkB2Date.SetReportField(new[] { "FH_CHECK_B2_1_CTL", "FH_CHECK_B2_2_CTL", "FH_CHECK_B2_3_CTL" });   // 照合確認B2票
            this.checkDDate.SetReportField(new[] { "FH_CHECK_D_1_CTL", "FH_CHECK_D_2_CTL", "FH_CHECK_D_3_CTL" });   // 照合確認D票
            this.checkEDate.SetReportField(new[] { "FH_CHECK_E_1_CTL", "FH_CHECK_E_2_CTL", "FH_CHECK_E_3_CTL" });   // 照合確認E票
            // 印字種類（普通）
            this.SetFieldName("FH_PRT_FUTSUU_HAIKIBUTSU_CTL", this.ingiv1);                         // 印字種類（普通の産業廃棄物("1"の場合は"ㇾ"、"0"の場合は"")）
            // 印字種類（特管）
            this.SetFieldName("FH_PRT_TOKUBETSU_HAIKIBUTSU_CTL", this.ingiv2);                      // 印字種類（特別管理産業廃棄物("1"の場合は"ㇾ"、"0"の場合は"")）
            this.SetFieldName("FH_HST_KAMI_H_CTL", this.kamimainh);                      // マニフェスト用紙へ印字
            this.SetFieldName("FH_HST_KAMI_U_CTL", this.kamimainu);                      // マニフェスト用紙へ印字
            this.SetFieldName("FH_HST_KAMI_T_CTL", this.kamimaint);                      // マニフェスト用紙へ印字
            this.SetFieldName("FH_HST_KAMI_S_CTL", this.kamimains);                      // マニフェスト用紙へ印字
            if (!this.kamimainkbn.Equals("1"))
            {
                this.SetFieldVisible("FH_HST_KAMI_H_CTL", false);
                this.SetFieldVisible("FH_HST_KAMI_U_CTL", false);
                this.SetFieldVisible("FH_HST_KAMI_T_CTL", false);
                this.SetFieldVisible("FH_HST_KAMI_S_CTL", false);
            }

            // 明細部分の列定義（2-1）
            this.SetFieldName("FH_UPN_GYOUSHA_NAME_CTL", this.upngyoushaname);                      // 運搬受託者名
            this.SetFieldName("FH_UPN_GYOUSHA_POST_CTL", this.upngyoushapost);                      // 運搬受託者郵便番号
            this.SetFieldName("FH_UPN_GYOUSHA_TEL_CTL", this.upngyoushatel);                        // 運搬受託者電話番号
            this.SetFieldName("FH_UPN_GYOUSHA_ADDRESS_CTL", this.upngyoushaadress);                 // 運搬受託者住所
            this.SetFieldName("FH_UPN_SAKI_GENBA_NAME_CTL", this.upnsakigenbaname);                 // 運搬先の事業場名
            this.SetFieldName("FH_UPN_SAKI_GENBA_POST_CTL", this.upnsakigenbapost);                 // 運搬先の事業場郵便番号
            this.SetFieldName("FH_UPN_SAKI_GENBA_TEL_CTL", this.upnsakigenbatel);                   // 運搬先の事業場電話番号
            this.SetFieldName("FH_UPN_SAKI_GENBA_ADDRESS_CTL", this.upnsakigenbaadress);            // 運搬先の事業場住所

            // 運搬の受託
            this.upnJyutakuRoute.SetReportField(new[] { "FH_UPN_JYUTAKUSHA_NAME_CTL", "FH_UNTENSHA_NAME_CTL", "FH_UNPAN_DATE_Y", "FH_UNPAN_DATE_M", "FH_UNPAN_DATE_D", "FH_YUUKA_SUU", "FH_YUUKA_UNIT_NAME" });

            this.sbnDate.SetReportField(new[] { "FH_SBN_DATE_Y", "FH_SBN_DATE_M", "FH_SBN_DATE_D" });

            this.lastSbnDate.SetReportField(new[] { "FH_LAST_DATE_Y", "FH_LAST_DATE_M", "FH_LAST_DATE_D" });

            this.SetFieldName("FH_SBN_JYUTAKUSHA_NAME", this.sbnJyutakushaName);
            this.SetFieldName("FH_SBN_TANTOU_NAME", this.sbnJyutakuTantouName);

            this.SetFieldName("FH_LAST_GYOUSHA_NAME", this.lastSbnResult.Name);
            this.SetFieldName("FH_LAST_GYOUSHA_POST", this.lastSbnResult.Post);
            this.SetFieldName("FH_LAST_GYOUSHA_TEL", this.lastSbnResult.Tel);
            this.SetFieldName("FH_LAST_GYOUSHA_ADDRESS", this.lastSbnResult.Address);

            this.SetFieldName("FH_LAST_SBN_GENBA_NUMBER", this.LAST_SBN_GENBA_NUMBER);         // 処分先No.


            // 明細部分の列定義（3-1）
            for (var i = 0; i < this.recno.Length; i++)
            {
                this.SetFieldName(String.Format("FH_REC_NO{0}_CTL", i + 1), this.recno[i]);
            }

            this.SetFieldName("FH_HAIKI_SHURUI_NAME1_CTL", this.haikishuruicd21 + " " + this.haikishuruiname21);                  // 廃棄物種類名-1
            this.SetFieldName("FH_HAIKI_SHURUI_NAME2_CTL", this.haikishuruicd22 + " " + this.haikishuruiname22);                 // 廃棄物種類名-2
            this.SetFieldName("FH_HAIKI_SHURUI_NAME3_CTL", "");                // 廃棄物種類名-3
            this.SetFieldName("FH_HAIKI_SHURUI_NAME4_CTL", this.haikishuruicd42 + " " + this.haikishuruiname42);                // 廃棄物種類名-4
            this.SetFieldName("FH_HAIKI_SHURUI_NAME5_CTL", this.haikishuruicd43 + " " + this.haikishuruiname43);                // 廃棄物種類名-5
            this.SetFieldName("FH_HAIKI_SHURUI_NAME6_CTL", this.haikishuruicd44 + " " + this.haikishuruiname44);                // 廃棄物種類名-6

            // 明細部分の列定義（4-1）
            this.SetFieldName("FH_PRT_SUU_CTL", this.prtsuu);                                       // 数量
            this.SetFieldName("FH_UNIT_NAME_CTL", this.unitname);                                   // 単位名
            this.SetFieldName("FH_PRT_NISUGATA_NAME_CTL", this.prtnisugataname);                    // 荷姿名
            this.SetFieldName("FH_PRT_HAIKI_NAME_CTL", this.prthaikiname);                          // 産業廃棄物の名称名
            this.SetFieldName("FH_PRT_YUUGAI_NAME_CTL", this.prtyuugainame);                        // 有害物質等名
            this.SetFieldName("FH_PRT_SBN_HOUHOU_NAME_CTL", this.prtsbnhouhouname);                 // 処分方法名
            this.SetFieldName("FH_BIKOU_CTL", this.bikoutsuusinn);                                  // 備考・通信欄        




            //斜線制御(5-1)
            bool slashVisivle = false;

            //積替
            slashVisivle = this.SLASH_TSUMIHO_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_TSUMIHO_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_TMH_GENBA_POST_CTL", !slashVisivle);
            this.SetFieldVisible("FH_TMH_GENBA_TEL_CTL", !slashVisivle);
            this.SetFieldVisible("FH_TMH_GENBA_ADDRESS_CTL", !slashVisivle);
            this.SetFieldVisible("FH_TMH_GENBA_NAME_CTL", !slashVisivle);
            
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

                    // 照合確認B2票（和暦変換）
                    this.checkB2Date = new ReportDate(this.SetFieldName, list[27]);

                    // 照合確認D票（和暦変換）
                    this.checkDDate = new ReportDate(this.SetFieldName, list[28]);

                    // 照合確認E票（和暦変換）
                    this.checkEDate = new ReportDate(this.SetFieldName, list[29]);

                    // 整理番号
                    this.seirino = list[2];
                    // 交付担当者
                    this.koufutantousha = list[3];
                    // 20140612 katen 不具合No.4469 start‏
                    //// 排出事業者名
                    //this.haishutujigyoushaname = list[4];
                    // 20140612 katen 不具合No.4469 end‏
                    // 排出事業者郵便番号
                    this.haishutujigyoushapost = list[5];
                    // 排出事業者電話番号
                    this.haishutujigyoushatel = list[6];
                    // 20140612 katen 不具合No.4469 start‏
                    //// 排出事業場住所
                    //this.haishutujigyoushaadress = list[7];
                    //// 排出事業場名称
                    //this.haishutujigyoubaname = list[8];
                    // 20140612 katen 不具合No.4469 end‏
                    // 排出事業場郵便番号
                    this.haishutujigyoubapost = list[9];
                    // 排出事業場電話番号
                    this.haishutujigyoubatel = list[10];
                    // 20140612 katen 不具合No.4469 start‏
                    //// 排出事業場住所
                    //this.haishutujigyoubaadress = list[11];
                    // 20140612 katen 不具合No.4469 end‏

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
                    // 積換保管名
                    this.tmhgenbaname = list[23];
                    // 積換保管郵便番号
                    this.tmhgenbapost = list[24];
                    // 積換保管電話番号
                    this.tmhgenbatel = list[25];
                    // 積換保管住所
                    this.tmhgenbaadress = list[26];
                    if (list[30] == "1")
                    {
                        // 印字種類（普通）
                        this.ingiv1 = "✓";
                    }
                    if (list[31] == "1")
                    {
                        // 印字種類（特管）
                        this.ingiv2 = "✓";
                    }
                    // 印刷部数（受渡し項目）
                    this.printsets = list[32];                    // 印刷部数（受渡し項目）

                    if (list[33] != null && !string.IsNullOrWhiteSpace(list[33].ToString()) && list[34] != null && !string.IsNullOrWhiteSpace(list[34].ToString()))
                    {
                        this.kamimainh = "H: " + list[33] + "-" + list[34];
                    }

                    if (list[35] != null && !string.IsNullOrWhiteSpace(list[35].ToString()) && list[36] != null && !string.IsNullOrWhiteSpace(list[36].ToString()))
                    {
                        this.kamimaint = "T: " + list[35] + "-" + list[36];
                    }
                    // 処分受託者CD（業者CD）
                    this.sbngyoushacd = list[37];
                    // ﾏﾆ用紙へ印字
                    this.kamimainkbn = list[38];
                }
                else if (list[0] == "2-1")
                {
                    // 明細部分の列定義（2-1）
                    this.upngyoushaname = list[1];                // 運搬受託者名
                    this.upngyoushapost = list[2];                // 運搬受託者郵便番号
                    this.upngyoushatel = list[3];                 // 運搬受託者電話番号
                    this.upngyoushaadress = list[4];              // 運搬受託者住所
                    this.upnsakigenbaname = list[5];              // 運搬先の事業場名
                    this.upnsakigenbapost = list[6];              // 運搬先の事業場郵便番号
                    this.upnsakigenbatel = list[7];               // 運搬先の事業場電話番号
                    this.upnsakigenbaadress = list[8];            // 運搬先の事業場住所

                    // 運搬の受託
                    this.upnJyutakuRoute = new UpnJyutakuElement(this.SetFieldName)
                    {
                        Name = list[9],
                        Person = list[10],
                        EndDate = new ReportDate(this.SetFieldName, list[11], false, "yyyy"),
                        Number = this.FormatSuuryo(list[12]),
                        Unit = list[13]
                    };

                    if (list[14] != null && !string.IsNullOrWhiteSpace(list[14].ToString()))
                    {
                        this.kamimainu = "U: " + list[14];
                    }

                    this.upnsakigenbacd = list[15];
                    if (this.upnsakigenbacd != null && !string.IsNullOrWhiteSpace(this.upnsakigenbacd) && this.sbngyoushacd != null && !string.IsNullOrWhiteSpace(this.sbngyoushacd))
                    {
                        kamimains = "S: " + this.sbngyoushacd + "-" + this.upnsakigenbacd;
                    }

                    // 処分の受託
                    this.sbnJyutakushaName = list[16];
                    this.sbnJyutakuTantouName = list[17];

                    this.sbnDate = new ReportDate(this.SetFieldName, list[18], false, "yyyy");

                    this.lastSbnDate = new ReportDate(this.SetFieldName, list[19], false, "yyyy");

                    // 最終処分を行った場所
                    this.lastSbnResult = new ManifestRouteElement(this.SetFieldName)
                    {
                        Name = list[20],
                        Post = ReportCommonUtility.AddPostMark(list[21]), //空でなければ郵便記号を付ける
                        Tel = list[22],
                        Address = list[23]
                    };

                    this.LAST_SBN_GENBA_NUMBER = ReportCommonUtility.AddNoMark(list[24]); //空でなければ"No."を付ける
                }
                else if (list[0] == "3-1")
                {
                    if (list[4]!= null && Convert.ToBoolean(list[4]))
                    {
                        this.recno[Convert.ToInt16(list[1]) - 1] = "✓";
                    }
                    switch (list[1])
                    {
                        case "21":
                            //[0]"3-1",[1]"21",[2]"9011",[3]"フリー１１"
                            this.haikishuruicd21 = list[2];
                            this.haikishuruiname21 = list[3];
                            break;
                        case "22":
                            //[0]"3-1",[1]"21",[2]"9012",[3]"フリー１２"
                            this.haikishuruicd22 = list[2];
                            this.haikishuruiname22 = list[3];
                            break;
                        case "41":
                            this.haikishuruicd41 = list[2];
                            this.haikishuruiname41 = list[3];
                            break;
                        case "42":
                            this.haikishuruicd42 = list[2];
                            this.haikishuruiname42 = list[3];
                            break;
                        case "43":
                            this.haikishuruicd43 = list[2];
                            this.haikishuruiname43 = list[3];
                            break;
                        case "44":
                            this.haikishuruicd44 = list[2];
                            this.haikishuruiname44 = list[3];
                            break;
                        default:

                            break;
                    }
                }
                else if (list[0] == "4-1")
                {
                    // 明細部分の列定義（4-1）
                    this.prtsuu = this.FormatSuuryo(list[1]);    // 数量
                    this.unitname = list[2];                     // 単位名
                    this.prtnisugataname = list[3];              // 荷姿名
                    this.prthaikiname = list[4];                 // 産業廃棄物の名称名
                    this.prtyuugainame = list[5];                // 有害物質等名
                    this.prtsbnhouhouname = list[6];             // 処分方法名
                    this.bikoutsuusinn = list[7];                // 備考・通信欄
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
