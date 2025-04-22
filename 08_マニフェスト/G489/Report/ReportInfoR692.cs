using System;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Utility;
using Shougun.Core.PaperManifest.InsatsuBusuSettei;

namespace Report
{
    public class ReportInfoR692 : Shougun.Core.PaperManifest.InsatsuBusuSettei.Report.ReportInfoBaseG489
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();

        // 交付年月日
        private ReportDate koufuDate;
        // 整理番号
        private string seirino = string.Empty;
        // 交付担当者
        private string koufutantousha = string.Empty;
        // 事前協議番号
        private string jizennumber = string.Empty;
        // 事前協議年月日
        private ReportDate jizenDate;
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
        private string lastsbnyoteigenbaname = string.Empty;        // 最終処分の場所現場名称
        //private string lastsbnyoteigenbatel = string.Empty;         // 最終処分の場所電話番号
        //private string lastsbnyoteigenbapost = string.Empty;        // 最終処分の場所郵便番号
        private string lastsbnyoteigenbaadress = string.Empty;      // 最終処分の場所住所
        private string sbngyoushaname = string.Empty;               // 処分受託者名
        private string sbngyoushapost = string.Empty;               // 処分受託者郵便番号
        private string sbngyoushatel = string.Empty;                // 処分受託者電話番号
        private string sbngyoushaadress = string.Empty;             // 処分受託者住所
        //private string tmhgenbaname = string.Empty;                 // 積換保管名
        private string tmhgenbapost = string.Empty;                 // 積換保管郵便番号
        private string tmhgenbatel = string.Empty;                  // 積換保管電話番号
        private string tmhgenbaadress = string.Empty;               // 積換保管住所
        private string yuukakbn1 = string.Empty;                    // 有価物拾集有無1
        private string yuukakbn2 = string.Empty;                    // 有価物拾集有無2

        /// <summary>実績</summary>
        private string jissekiNumber = string.Empty;
        /// <summary>実績数量 単位 0:t、1:㎥</summary>
        private string[] jissekiUnit = new string[] { "", "" };

        private string bikou = string.Empty;                        // 追加記事事項

        private ReportDate checkB1;                      // 照合確認B1票
        private ReportDate checkB2;                      // 照合確認B2票
        private ReportDate checkD2;                      // 照合確認D票
        private ReportDate checkE2;                      // 照合確認E票
        // 印字単位CD(t)
        private string prtunitcd1 = string.Empty;                   // 印字単位CD
        // 印字単位CD(kg)
        private string prtunitcd2 = string.Empty;                   // 印字単位CD
        // 印字単位CD(㎡)
        private string prtunitcd3 = string.Empty;                   // 印字単位CD
        // 印字単位CD(ℓ)
        private string prtunitcd4 = string.Empty;                   // 印字単位CD
        // 総重量又は総容量
        private string prtsuu = string.Empty;                       // 総重量又は総容量
        // 印刷部数（受渡し項目）
        private string printsets = string.Empty;                    // 印刷部数（受渡し項目）
        /// <summary>交付担当者所属</summary>
        private string KOUFU_TANTOUSHA_SHOZOKU = string.Empty;      // 交付担当者所属
        private string kamimainh = string.Empty;                    // マニフェスト用紙へ印字
        private string kamimainu1 = string.Empty;                    // マニフェスト用紙へ印字
        private string kamimainu2 = string.Empty;                    // マニフェスト用紙へ印字
        private string kamimaint = string.Empty;                    // マニフェスト用紙へ印字
        private string kamimains = string.Empty;                    // マニフェスト用紙へ印字
        private string sbngyoushacd = string.Empty;                    // 処分受託者CD（業者CD）
        private string kamimainkbn = string.Empty;                    // ﾏﾆ用紙へ印字

        // 明細部分の列定義（2-1）
        /// <summary>運搬の1区間あたりの項目数</summary>
        private const int UpnFieldCount = 16;
        // 運搬先の事業場CD（現場CD）
        private string upnsakigenbacd = string.Empty;

        /// <summary>運搬受託者 区間1-2</summary>
        private ManifestRouteElement[] upnRoute = new ManifestRouteElement[2];

        /// <summary>運搬先の事業場 区間1-2</summary>
        private ManifestRouteElement[] upnRoutePlace = new ManifestRouteElement[2];

        /// <summary>積替保管有無 区間1-2、0:なし　1:あり)</summary>
        private string[][] tmhKbn = new string[][] { new string[] { "", "" }, new string[] { "", "" } };

        /// <summary>車輛/車種 区間1-2、0:車輛　1:車種)</summary>
        private string[][] upnCar = new string[][] { new string[] { "", "" }, new string[] { "", "" } };

        /// <summary>運搬の受託 区間1-2</summary>
        private UpnJyutakuElement[] upnJyutakuRoute = new UpnJyutakuElement[2];

        /// <summary>処分の受託 0:受領 1:処分</summary>
        private UpnJyutakuElement[] sbnJyutakuRoute = new UpnJyutakuElement[2];

        /// <summary>最終処分終了年月日</summary>
        private ReportDate lastSbnDate;

        /// <summary>最終処分確認者</summary>
        private string lastSbnCheckName = String.Empty;

        /// <summary>最終処分を行った場所</summary>
        private ManifestRouteElement lastSbnResult;

        /// <summary>最終処分No(利用時はNo.を先頭につけること)</summary>
        private string LAST_SBN_GENBA_NUMBER = string.Empty;

        // 明細部分の列定義（3-1）
        private string haikishuruirecno = string.Empty;             // コンクリートがらチェック
        private string haikisuuryou1 = string.Empty;                // コンクリートがら
        private string haikishuruirecno2 = string.Empty;            // アスコンがらチェック
        private string haikisuuryou2 = string.Empty;                // アスコンがら
        private string haikishuruirecno3 = string.Empty;            // その他がれき類チェック
        private string haikisuuryou = string.Empty;                 // その他がれき類
        private string haikishuruirecno4 = string.Empty;            // ガラス・陶磁器くずチェック
        private string haikisuuryou4 = string.Empty;                // ガラス・陶磁器くず
        private string haikishuruirecno5 = string.Empty;            // 廃プラスチック類チェック
        private string haikisuuryou5 = string.Empty;                // 廃プラスチック類
        private string haikishuruirecno6 = string.Empty;            // 金属くずチェック
        private string haikisuuryou6 = string.Empty;                // 金属くず
        private string haikishuruirecno7 = string.Empty;            // 混合（安定型のみ）チェック
        private string haikisuuryou7 = string.Empty;                // 混合（安定型のみ）
        private string haikishuruirecno8 = string.Empty;            // 石綿含有産業廃棄物チェック
        private string haikisuuryou8 = string.Empty;                // 石綿含有産業廃棄物
        private string haikishuruirecno9 = string.Empty;            // フリー項目(安定)-1チェック
        private string haikishuruiname1 = string.Empty;             // フリー項目(安定)名-1
        private string haikisuuryou9 = string.Empty;                // フリー項目(安定)-1
        private string haikishuruirecno10 = string.Empty;           // フリー項目(安定)-2チェック
        private string haikishuruiname2 = string.Empty;             // フリー項目(安定)名-2
        private string haikisuuryou10 = string.Empty;               // フリー項目(安定)-2
        private string haikishuruirecno11 = string.Empty;           // フリー項目(安定)-3チェック
        private string haikishuruiname3 = string.Empty;             // フリー項目(安定)名-3
        private string haikisuuryou11 = string.Empty;               // フリー項目(安定)-3
        private string haikishuruirecno12 = string.Empty;           // フリー項目(安定)-4チェック
        private string haikishuruiname4 = string.Empty;             // フリー項目(安定)名-4
        private string haikisuuryou12 = string.Empty;               // フリー項目(安定)-4
        private string haikishuruirecno13 = string.Empty;           // 建設汚泥チェック
        private string haikisuuryou13 = string.Empty;               // 建設汚泥
        private string haikishuruirecno14 = string.Empty;           // 紙くずチェック
        private string haikisuuryou14 = string.Empty;               // 紙くず
        private string haikishuruirecno15 = string.Empty;           // 木くずチェック
        private string haikisuuryou15 = string.Empty;               // 木くず
        private string haikishuruirecno16 = string.Empty;           // 繊維くずチェック
        private string haikisuuryou16 = string.Empty;               // 繊維くず
        private string haikishuruirecno17 = string.Empty;           // 廃石膏ボードチェック
        private string haikisuuryou17 = string.Empty;               // 廃石膏ボード
        private string haikishuruirecno18 = string.Empty;           // 混合（管理型含む）チェック
        private string haikisuuryou18 = string.Empty;               // 混合（管理型含む）
        private string haikishuruirecno19 = string.Empty;           // 石綿含有産業廃棄物チェック
        private string haikisuuryou19 = string.Empty;               // 石綿含有産業廃棄物
        private string haikishuruirecno20 = string.Empty;           // フリー項目(管理)-1チェック
        private string haikishuruiname5 = string.Empty;             // フリー項目(管理)名-1
        private string haikisuuryou20 = string.Empty;               // フリー項目(管理)-1
        private string haikishuruirecno21 = string.Empty;           // フリー項目(管理)-2チェック
        private string haikishuruiname6 = string.Empty;             // フリー項目(管理)名-2
        private string haikisuuryou21 = string.Empty;               // フリー項目(管理)-2
        private string haikishuruirecno22 = string.Empty;           // フリー項目(管理)-3チェック
        private string haikishuruiname7 = string.Empty;             // フリー項目(管理)名-3
        private string haikisuuryou22 = string.Empty;               // フリー項目(管理)-3チェック
        private string haikishuruirecno23 = string.Empty;           // 廃石綿等チェック
        private string haikisuuryou23 = string.Empty;               // 廃石綿等
        private string haikishuruirecno24 = string.Empty;           // フリー項目(特別)-1チェック
        private string haikishuruiname8 = string.Empty;             // フリー項目(特別)名-1
        private string haikisuuryou24 = string.Empty;               // フリー項目(特別)-1
        private string haikishuruirecno25 = string.Empty;           // フリー項目(特別)-2チェック
        private string haikishuruiname9 = string.Empty;             // フリー項目(特別)名-2
        private string haikisuuryou25 = string.Empty;               // フリー項目(特別)-2チェック
        private string haikishuruirecno26 = string.Empty;           // フリー項目(特別)-3チェック
        private string haikishuruiname10 = string.Empty;            // フリー項目(特別)名-3
        private string haikisuuryou26 = string.Empty;               // フリー項目(特別)-3チェック
        private string haikishuruirecno27 = string.Empty;           // フリー項目(特別)-4チェック
        private string haikishuruiname11 = string.Empty;            // フリー項目(特別)名-4
        private string haikisuuryou27 = string.Empty;               // フリー項目(特別)-4チェック
        private string haikishuruirecno28 = string.Empty;           // フリー項目(特別)-5チェック
        private string haikishuruiname12 = string.Empty;            // フリー項目(特別)名-5
        private string haikisuuryou28 = string.Empty;               // フリー項目(特別)-5チェック

        // 明細部分の列定義（4-1）
        private string keijourecno1 = string.Empty;                 // 固形状チェック
        private string keijourecno2 = string.Empty;                 // 泥状チェック
        private string keijourecno3 = string.Empty;                 // 液状チェック
        private string keijourecno4 = string.Empty;                 // フリー項目(形状)-1チェック
        private string keijouname1 = string.Empty;                  // フリー項目(形状)名-1
        private string keijourecno5 = string.Empty;                 // フリー項目(形状)-2チェック
        private string keijouname2 = string.Empty;                  // フリー項目(形状)名-2
        private string keijourecno6 = string.Empty;                 // フリー項目(形状)-3チェック
        private string keijouname3 = string.Empty;                  // フリー項目(形状)名-3
        private string keijourecno7 = string.Empty;                 // フリー項目(形状)-4チェック
        private string keijouname4 = string.Empty;                  // フリー項目(形状)名-4

        // 明細部分の列定義（5-1）
        private string nisugatarecno1 = string.Empty;               // バラチェック
        private string nisugatarecno2 = string.Empty;               // コンテナチェック
        private string nisugatarecno3 = string.Empty;               // ドラム缶チェック
        private string nisugatarecno4 = string.Empty;               // 袋チェック
        private string nisugatarecno5 = string.Empty;               // フリー項目(荷姿)-1チェック
        private string nisugataname1 = string.Empty;                // フリー項目(荷姿)名-1
        private string nisugatarecno6 = string.Empty;               // フリー項目(荷姿)-2チェック
        private string nisugataname2 = string.Empty;                // フリー項目(荷姿)名-2
        private string nisugatarecno7 = string.Empty;               // フリー項目(荷姿)-3チェック
        private string nisugataname3 = string.Empty;                // フリー項目(荷姿)名-3

        // 明細部分の列定義（6-1）
        private string shobunhouhourecno1 = string.Empty;           // 処分方法脱水
        private string shobunhouhourecno2 = string.Empty;           // 処分方法焼却
        private string shobunhouhourecno3 = string.Empty;           // 処分方法破砕
        private string shobunhouhourecno4 = string.Empty;           // 処分方法フリー1
        private string shobunhouhouname1 = string.Empty;            // 処分方法フリー1名
        private string shobunhouhourecno5 = string.Empty;           // 積処分方法フリー2
        private string shobunhouhouname2 = string.Empty;            // 処分方法フリー2名
        private string shobunhouhourecno6 = string.Empty;           // 処分方法フリー3
        private string shobunhouhouname3 = string.Empty;            // 処分方法フリー3名
        private string shobunhouhourecno7 = string.Empty;           // 処分方法フリー4
        private string shobunhouhouname4 = string.Empty;            // 処分方法フリー4名
        private string shobunhouhourecno8 = string.Empty;           // 処分方法フリー5
        private string shobunhouhouname5 = string.Empty;            // 処分方法フリー5名
        private string shobunhouhourecno11 = string.Empty;          // 最終処分安定型
        private string shobunhouhourecno12 = string.Empty;          // 最終処分管理型
        private string shobunhouhourecno13 = string.Empty;          // 最終処分遮断型


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
        public ReportInfoR692()
        {
            // 帳票出力フルパスフォーム名
            //this.OutputFormFullPathName = TEMPLATE_PATH + "R692-Form.xml";
            this.OutputFormFullPathName = "Template/R692-Form.xml";
            //this.OutputFormFullPathName = "R692-Form.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";

            this.ReportID = "R692";
        }

        /// <summary>帳票出力フルパスフォームを保持するプロパティ</summary>
        public string OutputFormFullPathName { get; set; }

        /// <summary>
        /// 帳票出力フォームレイアウトを保持するプロパティ
        /// </summary>
        public string OutputFormLayout { get; set; }

        /// <summary>C1Reportの帳票データの作成ならびに明細部分の列定義を実行する</summary>
        /// <param name="chouhyouData">chouhyouData</param>
        public void R692_Report(DataTable chouhyouData)
        {
            LogUtility.DebugMethodStart(chouhyouData);
            try
            {
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

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (!(ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException))
                {
                    throw;
                }
            }

            LogUtility.DebugMethodEnd(chouhyouData);
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            LogUtility.DebugMethodStart();

            // ヘッダ部
            // 交付年月日
            this.koufuDate.SetReportField(new[] { "FH_KOUFU_DATE_1_CTL", "FH_KOUFU_DATE_2_CTL", "FH_KOUFU_DATE_3_CTL" });
            // 整理番号
            this.SetFieldName("FH_SEIRI_ID_CTL", this.seirino);
            // 交付担当者
            this.SetFieldName("FH_KOUFU_TANTOUSHA_CTL", this.koufutantousha);
            // 事前協議番号
            this.SetFieldName("FH_JIZEN_NUMBER_CTL", this.jizennumber);
            // 事前協議年月日
            this.SetFieldName("FH_JIZEN_DATE_CTL", this.jizenDate.ToString("yyyy/MM/dd"));
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

            this.SetFieldName("FH_CHUUKAN_HAIKI_KBN1_CTL", this.chuukanhaikikbn1);             // 中間処理産業廃棄物区分・帳簿記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_CHUUKAN_HAIKI_KBN2_CTL", this.chuukanhaikikbn2);             // 中間処理産業廃棄物区分・当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_CHUUKAN_HAIKI_CTL", this.chuukanhaikibutsu);            // 中間処理産業廃棄物
            //this.SetFieldName("PHY_HANTEI_FLB", this.lastsbnyoteikbn);            // 最終処分の場所区分
            this.SetFieldName("FH_LAST_SBN_YOTEI_KBN1_CTL", this.lastsbnyoteikbn1);             // 委託契約書記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_LAST_SBN_YOTEI_KBN2_CTL", this.lastsbnyoteikbn2);             // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_NAME_CTL", this.lastsbnyoteigenbaname);        // 最終処分の場所現場名称
            //this.SetFieldName("PHY_HANTEI_FLB", this.lastsbnyoteigenbatel);         // 最終処分の場所電話番号
            //this.SetFieldName("PHY_HANTEI_FLB", this.lastsbnyoteigenbapost);        // 最終処分の場所郵便番号
            this.SetFieldName("FH_LAST_SBN_YOTEI_GENBA_ADDRESS_CTL", this.lastsbnyoteigenbaadress);      // 最終処分の場所住所
            this.SetFieldName("FH_SBN_GYOUSHA_NAME_CTL", this.sbngyoushaname);               // 処分受託者名
            this.SetFieldName("FH_SBN_GYOUSHA_POST_CTL", this.sbngyoushapost);               // 処分受託者郵便番号
            this.SetFieldName("FH_SBN_GYOUSHA_TEL_CTL", this.sbngyoushatel);                // 処分受託者電話番号
            this.SetFieldName("FH_SBN_GYOUSHA_ADDRESS_CTL", this.sbngyoushaadress);             // 処分受託者住所
            //this.SetFieldName("PHY_HANTEI_FLB", this.tmhgenbaname);                 // 積換保管名
            this.SetFieldName("FH_TMH_GENBA_POST_CTL", this.tmhgenbapost);                 // 積換保管郵便番号
            this.SetFieldName("FH_TMH_GENBA_TEL_CTL", this.tmhgenbatel);                  // 積換保管電話番号
            this.SetFieldName("FH_TMH_GENBA_ADDRESS_CTL", this.tmhgenbaadress);               // 積換保管住所
            this.SetFieldName("FH_YUUKA_KBN1_CTL", this.yuukakbn1);               // 有価物拾集有無1
            this.SetFieldName("FH_YUUKA_KBN2_CTL", this.yuukakbn2);               // 有価物拾集有無2
            this.SetFieldName("FH_JISSEKI_SUU", this.jissekiNumber);
            this.SetFieldName("FH_JISSEKI_UNIT1", this.jissekiUnit[0]);
            this.SetFieldName("FH_JISSEKI_UNIT2", this.jissekiUnit[1]);
            
            this.SetFieldName("FH_BIKOU_CTL", this.bikou);               // 追加記事事項
            // 照合確認B1票
            this.checkB1.SetReportField(new[] { "FH_CHECK_B1_1_CTL", "FH_CHECK_B1_2_CTL", "FH_CHECK_B1_3_CTL" });
            // 照合確認B2票
            this.checkB2.SetReportField(new[] { "FH_CHECK_B2_1_CTL", "FH_CHECK_B2_2_CTL", "FH_CHECK_B2_3_CTL" });
            // 照合確認D票
            this.checkD2.SetReportField(new[] { "FH_CHECK_D_1_CTL", "FH_CHECK_D_2_CTL", "FH_CHECK_D_3_CTL" });
            // 照合確認E票
            this.checkE2.SetReportField(new[] { "FH_CHECK_E_1_CTL", "FH_CHECK_E_1_CTL", "FH_CHECK_E_3_CTL" });
            // 印字単位CD "1"の場合は"○"、左記以外の場合は""
            this.SetFieldName("FH_PRT_UNIT_CD1_CTL", this.prtunitcd1);                 // 印字単位CD
            // 印字単位CD "2"の場合は"○"、左記以外の場合は""
            this.SetFieldName("FH_PRT_UNIT_CD2_CTL", this.prtunitcd2);                 // 印字単位CD
            // 印字単位CD "3"の場合は"○"、左記以外の場合は""
            this.SetFieldName("FH_PRT_UNIT_CD3_CTL", this.prtunitcd3);                 // 印字単位CD
            // 印字単位CD "4"の場合は"○"、左記以外の場合は""
            this.SetFieldName("FH_PRT_UNIT_CD4_CTL", this.prtunitcd4);                 // 印字単位CD
            // 総重量又は総容量
            this.SetFieldName("FH_PRT_SUU_CTL", this.prtsuu);                          // 総重量又は総容量
            //// 印刷部数（受渡し項目）
            //this.SetFieldName("PHY_HANTEI_FLB", this.printsets);                    // 印刷部数（受渡し項目）
            //交付担当者所属
            string syozoku = string.Empty; //10文字で改行する
            if(string.IsNullOrEmpty(this.KOUFU_TANTOUSHA_SHOZOKU) || this.KOUFU_TANTOUSHA_SHOZOKU.Length <= 10 )
            {
                syozoku = this.KOUFU_TANTOUSHA_SHOZOKU;
            }
            else
            {
                syozoku = this.KOUFU_TANTOUSHA_SHOZOKU.Insert(10, Environment.NewLine);
            }
            this.SetFieldName("FH_KOUFU_TANTOUSHA_SHOZOKU_CTL", syozoku);  // 交付担当者所属

            this.SetFieldName("FH_HST_KAMI_H_CTL", this.kamimainh);                      // マニフェスト用紙へ印字
            this.SetFieldName("FH_HST_KAMI_T_CTL", this.kamimaint);                      // マニフェスト用紙へ印字
            this.SetFieldName("FH_HST_KAMI_S_CTL", this.kamimains);                      // マニフェスト用紙へ印字

            // 明細部分の列定義（2-1）
            for (var i = 0; i < 2; i++)
            {
                // 運搬先区分
                this.SetFieldName(String.Format("FH_TMH_KBN{0}_1_CTL", i + 1), this.tmhKbn[i][1]);
                this.SetFieldName(String.Format("FH_TMH_KBN{0}_2_CTL", i + 1), this.tmhKbn[i][0]);

                // 運搬受託者
                this.upnRoute[i].SetReportField(new string[] { 
                                                        String.Format("FH_UPN_GYOUSHA_NAME{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_GYOUSHA_POST{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_GYOUSHA_TEL{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_GYOUSHA_ADDRESS{0}_CTL", i + 1)});
                // 運搬先 (現場は1つしかない　ルート1と2の両方に同じ値が入っているっぽい 1を使うか2を使うかはテンプレートの名前で調整　ロジックは両方セットするようにする)
                this.upnRoutePlace[i].SetReportField(new string[] { 
                                                        String.Format("FH_UPN_SAKI_GENBA_NAME{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_SAKI_GENBA_POST{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_SAKI_GENBA_TEL{0}_CTL", i + 1), 
                                                        String.Format("FH_UPN_SAKI_GENBA_ADDRESS{0}_CTL", i + 1)});
                // 運搬の受託
                this.upnJyutakuRoute[i].SetReportField(new string[] { 
                                                        String.Format("FH_UPN_JYUTAKUSHA_NAME{0}", i + 1), 
                                                        String.Format("FH_UPN_TANTOU_NAME{0}", i + 1), 
                                                        String.Format("FH_UNPAN_DATE{0}_Y", i + 1), 
                                                        String.Format("FH_UNPAN_DATE{0}_M", i + 1), 
                                                        String.Format("FH_UNPAN_DATE{0}_D", i + 1), 
                                                        "",""});
                // 処分の受託
                this.sbnJyutakuRoute[i].SetReportField(new string[] { 
                                                        String.Format("FH_SBN_JYUTAKUSHA_NAME{0}", i + 1), 
                                                        String.Format("FH_SBN_TANTOU_NAME{0}", i + 1), 
                                                        String.Format("FH_SBN_DATE{0}_Y", i + 1), 
                                                        String.Format("FH_SBN_DATE{0}_M", i + 1), 
                                                        String.Format("FH_SBN_DATE{0}_D", i + 1), 
                                                        "",""});
                if (i == 0)
                {
                    this.SetFieldName("FH_HST_KAMI_U1_CTL", this.kamimainu1);                      // マニフェスト用紙へ印字
                }
                else if (i == 1)
                {
                    this.SetFieldName("FH_HST_KAMI_U2_CTL", this.kamimainu2);                      // マニフェスト用紙へ印字
                }
                //車輌 FH_SHARYOU_NAME1_CTL
                this.SetFieldName(string.Format("FH_SHARYOU_NAME{0}_CTL", i + 1), this.upnCar[i][0]);
                //車種 FH_SHASHU_NAME1_CTL
                this.SetFieldName(string.Format("FH_SHASHU_NAME{0}_CTL", i + 1), this.upnCar[i][1]);
            }

            this.lastSbnDate.SetReportField(new[] { "FH_LAST_SBN_DATE_Y", "FH_LAST_SBN_DATE_M", "FH_LAST_SBN_DATE_D" });
            this.SetFieldName("FH_LAST_SBN_CHECK_NAME", this.lastSbnCheckName);

            this.lastSbnResult.SetReportField(new string[] { "FH_LAST_SBN_GENBA_NAME", "FH_LAST_SBN_GENBA_POST", "", "FH_LAST_SBN_GENBA_ADDRESS" });

            this.SetFieldName("FH_LAST_SBN_GENBA_NUMBER", this.LAST_SBN_GENBA_NUMBER);         // 処分先No.

            if (!this.kamimainkbn.Equals("1"))
            {
                this.SetFieldVisible("FH_HST_KAMI_H_CTL", false);
                this.SetFieldVisible("FH_HST_KAMI_U1_CTL", false);
                this.SetFieldVisible("FH_HST_KAMI_U2_CTL", false);
                this.SetFieldVisible("FH_HST_KAMI_T_CTL", false);
                this.SetFieldVisible("FH_HST_KAMI_S_CTL", false);
            }
            // 明細部分の列定義（3-1）
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO1_CTL", this.haikishuruirecno);           // コンクリートがらチェック
            this.SetFieldName("FH_HAIKI_SUURYOU1_CTL", this.haikisuuryou1);                    // コンクリートがら
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO2_CTL", this.haikishuruirecno2);          // アスコンがらチェック
            this.SetFieldName("FH_HAIKI_SUURYOU2_CTL", this.haikisuuryou2);                    // アスコンがら
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO3_CTL", this.haikishuruirecno3);          // その他がれき類チェック
            this.SetFieldName("FH_HAIKI_SUURYOU3_CTL", this.haikisuuryou);                     // その他がれき類
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO4_CTL", this.haikishuruirecno4);          // ガラス・陶磁器くずチェック
            this.SetFieldName("FH_HAIKI_SUURYOU4_CTL", this.haikisuuryou4);                    // ガラス・陶磁器くず
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO5_CTL", this.haikishuruirecno5);          // 廃プラスチック類チェック
            this.SetFieldName("FH_HAIKI_SUURYOU5_CTL", this.haikisuuryou5);                    // 廃プラスチック類
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO6_CTL", this.haikishuruirecno6);          // 金属くずチェック
            this.SetFieldName("FH_HAIKI_SUURYOU6_CTL", this.haikisuuryou6);                    // 金属くず
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO7_CTL", this.haikishuruirecno7);          // 混合（安定型のみ）チェック
            this.SetFieldName("FH_HAIKI_SUURYOU7_CTL", this.haikisuuryou7);                    // 混合（安定型のみ）
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO8_CTL", this.haikishuruirecno8);          // 石綿含有産業廃棄物チェック
            this.SetFieldName("FH_HAIKI_SUURYOU8_CTL", this.haikisuuryou8);                    // 石綿含有産業廃棄物
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO9_CTL", this.haikishuruirecno9);          // フリー項目(安定)-1チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME1_CTL", this.haikishuruiname1);             // フリー項目(安定)名-1
            this.SetFieldName("FH_HAIKI_SUURYOU9_CTL", this.haikisuuryou9);                    // フリー項目(安定)-1
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO10_CTL", this.haikishuruirecno10);        // フリー項目(安定)-2チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME2_CTL", this.haikishuruiname2);             // フリー項目(安定)名-2
            this.SetFieldName("FH_HAIKI_SUURYOU10_CTL", this.haikisuuryou10);                  // フリー項目(安定)-2
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO11_CTL", this.haikishuruirecno11);        // フリー項目(安定)-3チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME3_CTL", this.haikishuruiname3);             // フリー項目(安定)名-3
            this.SetFieldName("FH_HAIKI_SUURYOU11_CTL", this.haikisuuryou11);                  // フリー項目(安定)-3
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO12_CTL", this.haikishuruirecno12);        // フリー項目(安定)-4チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME4_CTL", this.haikishuruiname4);             // フリー項目(安定)名-4
            this.SetFieldName("FH_HAIKI_SUURYOU12_CTL", this.haikisuuryou12);                  // フリー項目(安定)-4
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO13_CTL", this.haikishuruirecno13);        // 建設汚泥チェック
            this.SetFieldName("FH_HAIKI_SUURYOU13_CTL", this.haikisuuryou13);                  // 建設汚泥
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO14_CTL", this.haikishuruirecno14);        // 紙くずチェック
            this.SetFieldName("FH_HAIKI_SUURYOU14_CTL", this.haikisuuryou14);                  // 紙くず
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO15_CTL", this.haikishuruirecno15);        // 木くずチェック
            this.SetFieldName("FH_HAIKI_SUURYOU15_CTL", this.haikisuuryou15);                  // 木くず
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO16_CTL", this.haikishuruirecno16);        // 繊維くずチェック
            this.SetFieldName("FH_HAIKI_SUURYOU16_CTL", this.haikisuuryou16);                  // 繊維くず
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO17_CTL", this.haikishuruirecno17);        // 廃石膏ボードチェック
            this.SetFieldName("FH_HAIKI_SUURYOU17_CTL", this.haikisuuryou17);                  // 廃石膏ボード
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO18_CTL", this.haikishuruirecno18);        // 混合（管理型含む）チェック
            this.SetFieldName("FH_HAIKI_SUURYOU18_CTL", this.haikisuuryou18);                  // 混合（管理型含む）
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO19_CTL", this.haikishuruirecno19);        // 石綿含有産業廃棄物チェック
            this.SetFieldName("FH_HAIKI_SUURYOU19_CTL", this.haikisuuryou19);                  // 石綿含有産業廃棄物
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO20_CTL", this.haikishuruirecno20);        // フリー項目(管理)-1チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME5_CTL", this.haikishuruiname5);             // フリー項目(管理)名-1
            this.SetFieldName("FH_HAIKI_SUURYOU20_CTL", this.haikisuuryou20);                  // フリー項目(管理)-1
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO21_CTL", this.haikishuruirecno21);        // フリー項目(管理)-2チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME6_CTL", this.haikishuruiname6);             // フリー項目(管理)名-2
            this.SetFieldName("FH_HAIKI_SUURYOU21_CTL", this.haikisuuryou21);                  // フリー項目(管理)-2
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO22_CTL", this.haikishuruirecno22);        // フリー項目(管理)-3チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME7_CTL", this.haikishuruiname7);             // フリー項目(管理)名-3
            this.SetFieldName("FH_HAIKI_SUURYOU22_CTL", this.haikisuuryou22);                  // フリー項目(管理)-3チェック
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO23_CTL", this.haikishuruirecno23);        // 廃石綿等チェック
            this.SetFieldName("FH_HAIKI_SUURYOU23_CTL", this.haikisuuryou23);                  // 廃石綿等
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO24_CTL", this.haikishuruirecno24);        // フリー項目(特別)-1チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME8_CTL", this.haikishuruiname8);             // フリー項目(特別)名-1
            this.SetFieldName("FH_HAIKI_SUURYOU24_CTL", this.haikisuuryou24);                  // フリー項目(特別)-1
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO25_CTL", this.haikishuruirecno25);        // フリー項目(特別)-2チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME9_CTL", this.haikishuruiname9);             // フリー項目(特別)名-2
            this.SetFieldName("FH_HAIKI_SUURYOU25_CTL", this.haikisuuryou25);                  // フリー項目(特別)-2チェック
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO26_CTL", this.haikishuruirecno26);        // フリー項目(特別)-3チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME10_CTL", this.haikishuruiname10);           // フリー項目(特別)名-3
            this.SetFieldName("FH_HAIKI_SUURYOU26_CTL", this.haikisuuryou26);                  // フリー項目(特別)-3チェック
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO27_CTL", this.haikishuruirecno27);        // フリー項目(特別)-4チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME11_CTL", this.haikishuruiname11);           // フリー項目(特別)名-4
            this.SetFieldName("FH_HAIKI_SUURYOU27_CTL", this.haikisuuryou27);                  // フリー項目(特別)-4チェック
            this.SetFieldName("FH_HAIKI_SHURUI_REC_NO28_CTL", this.haikishuruirecno28);        // フリー項目(特別)-5チェック
            this.SetFieldName("FH_HAIKI_SHURUI_NAME12_CTL", this.haikishuruiname12);           // フリー項目(特別)名-5
            this.SetFieldName("FH_HAIKI_SUURYOU28_CTL", this.haikisuuryou28);                  // フリー項目(特別)-5チェック

            // 明細部分の列定義（4-1）
            this.SetFieldName("FH_KEIJOU_REC_NO1_CTL", this.keijourecno1);                     // 固形状チェック
            this.SetFieldName("FH_KEIJOU_REC_NO2_CTL", this.keijourecno2);                     // 泥状チェック
            this.SetFieldName("FH_KEIJOU_REC_NO3_CTL", this.keijourecno3);                     // 液状チェック
            this.SetFieldName("FH_KEIJOU_REC_NO4_CTL", this.keijourecno4);                     // フリー項目(形状)-1チェック
            this.SetFieldName("FH_KEIJOU_NAME1_CTL", this.keijouname1);                        // フリー項目(形状)名-1
            this.SetFieldName("FH_KEIJOU_REC_NO5_CTL", this.keijourecno5);                     // フリー項目(形状)-2チェック
            this.SetFieldName("FH_KEIJOU_NAME2_CTL", this.keijouname2);                        // フリー項目(形状)名-2
            this.SetFieldName("FH_KEIJOU_REC_NO6_CTL", this.keijourecno6);                     // フリー項目(形状)-3チェック
            this.SetFieldName("FH_KEIJOU_NAME3_CTL", this.keijouname3);                        // フリー項目(形状)名-3
            this.SetFieldName("FH_KEIJOU_REC_NO7_CTL", this.keijourecno7);                     // フリー項目(形状)-4チェック
            this.SetFieldName("FH_KEIJOU_NAME4_CTL", this.keijouname4);                        // フリー項目(形状)名-4

            // 明細部分の列定義（5-1）
            this.SetFieldName("FH_NISUGATA_REC_NO1_CTL", this.nisugatarecno1);                 // バラチェック
            this.SetFieldName("FH_NISUGATA_REC_NO2_CTL", this.nisugatarecno2);                 // コンテナチェック
            this.SetFieldName("FH_NISUGATA_REC_NO3_CTL", this.nisugatarecno3);                 // ドラム缶チェック
            this.SetFieldName("FH_NISUGATA_REC_NO4_CTL", this.nisugatarecno4);                 // 袋チェック
            this.SetFieldName("FH_NISUGATA_REC_NO5_CTL", this.nisugatarecno5);                 // フリー項目(荷姿)-1チェック
            this.SetFieldName("FH_NISUGATA_NAME1_CTL", this.nisugataname1);                    // フリー項目(荷姿)名-1
            this.SetFieldName("FH_NISUGATA_REC_NO6_CTL", this.nisugatarecno6);                 // フリー項目(荷姿)-2チェック
            this.SetFieldName("FH_NISUGATA_NAME2_CTL", this.nisugataname2);                    // フリー項目(荷姿)名-2
            this.SetFieldName("FH_NISUGATA_REC_NO7_CTL", this.nisugatarecno7);                 // フリー項目(荷姿)-3チェック
            this.SetFieldName("FH_NISUGATA_NAME3_CTL", this.nisugataname3);                    // フリー項目(荷姿)名-3

            // 明細部分の列定義（6-1）
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO1_CTL", this.shobunhouhourecno1);        // 処分方法脱水
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO2_CTL", this.shobunhouhourecno2);        // 処分方法焼却
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO3_CTL", this.shobunhouhourecno3);        // 処分方法破砕
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO4_CTL", this.shobunhouhourecno4);        // 処分方法フリー1
            this.SetFieldName("FH_SHOBUN_HOUHOU_NAME1_CTL", this.shobunhouhouname1);           // 処分方法フリー1名
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO5_CTL", this.shobunhouhourecno5);        // 積処分方法フリー2
            this.SetFieldName("FH_SHOBUN_HOUHOU_NAME2_CTL", this.shobunhouhouname2);           // 処分方法フリー2名
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO6_CTL", this.shobunhouhourecno6);        // 処分方法フリー3

            this.SetFieldName("FH_SHOBUN_HOUHOU_NAME3_CTL", this.shobunhouhouname3);           // 処分方法フリー3名
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO7_CTL", this.shobunhouhourecno7);        // 処分方法フリー4
            this.SetFieldName("FH_SHOBUN_HOUHOU_NAME4_CTL", this.shobunhouhouname4);           // 処分方法フリー4名
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO8_CTL", this.shobunhouhourecno8);        // 処分方法フリー5
            this.SetFieldName("FH_SHOBUN_HOUHOU_NAME5_CTL", this.shobunhouhouname5);           // 処分方法フリー5名
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO11_CTL", this.shobunhouhourecno11);      // 最終処分安定型
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO12_CTL", this.shobunhouhourecno12);      // 最終処分管理型
            this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO13_CTL", this.shobunhouhourecno13);      // 最終処分遮断型

            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO1_CTL", this.shobunhouhourecno1);        // 処分方法脱水
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO2_CTL", this.shobunhouhourecno2);        // 処分方法焼却
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO3_CTL", this.shobunhouhourecno3);        // 処分方法破砕
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO4_CTL", this.shobunhouhourecno4);        // 処分方法フリー1
            //this.SetFieldName("FH_SHOBUN_HOUHOU_NAME1_CTL", this.shobunhouhouname1);           // 処分方法フリー1名
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO5_CTL", this.shobunhouhourecno5);        // 積処分方法フリー2
            //this.SetFieldName("FH_SHOBUN_HOUHOU_NAME2_CTL", this.shobunhouhouname2);           // 処分方法フリー2名
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO6_CTL", this.shobunhouhourecno6);        // 処分方法フリー3
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO7_CTL", this.shobunhouhourecno7);        // 処分方法フリー3名
            //this.SetFieldName("FH_SHOBUN_HOUHOU_NAME4_CTL", this.shobunhouhouname4);           // 処分方法フリー4
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO8_CTL", this.shobunhouhourecno8);        // 処分方法フリー4名
            //this.SetFieldName("FH_SHOBUN_HOUHOU_NAME5_CTL", this.shobunhouhouname5);           // 処分方法フリー5
            //this.SetFieldName("FH_SHOBUN_HOUHOU_NAME3_CTL", this.shobunhouhouname3);           // 処分方法フリー5名
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO11_CTL", this.shobunhouhourecno11);      // 最終処分安定型
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO12_CTL", this.shobunhouhourecno12);      // 最終処分管理型
            //this.SetFieldName("FH_SHOBUN_HOUHOU_REC_NO13_CTL", this.shobunhouhourecno13);      // 最終処分遮断型


            //斜線制御(5-1)
            bool slashVisivle = false;

            //積替
            slashVisivle = this.SLASH_TSUMIHO_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_TSUMIHO_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_TMH_GENBA_POST_CTL", !slashVisivle);
            this.SetFieldVisible("FH_TMH_GENBA_ADDRESS_CTL", !slashVisivle);
            this.SetFieldVisible("FH_TMH_GENBA_TEL_CTL", !slashVisivle);
            this.SetFieldVisible("FH_YUUKA_KBN1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_YUUKA_KBN2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_JISSEKI_SUU", !slashVisivle);
            this.SetFieldVisible("FH_JISSEKI_UNIT1", !slashVisivle);
            this.SetFieldVisible("FH_JISSEKI_UNIT2", !slashVisivle);


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
            this.SetFieldVisible("FH_UPN_GYOUSHA_POST2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_ADDRESS2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_NAME2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_UPN_GYOUSHA_TEL2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_TMH_KBN2_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_TMH_KBN2_2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_SHARYOU_NAME2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_SHASHU_NAME2_CTL", !slashVisivle);
            

            //運搬受託2
            slashVisivle = this.SLASH_UPN_JYUTAKUSHA2_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_UPN_JYUTAKUSHA2_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_UPN_JYUTAKUSHA_NAME2", !slashVisivle);
            this.SetFieldVisible("FH_UPN_TANTOU_NAME2", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE2_Y", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE2_M", !slashVisivle);
            this.SetFieldVisible("FH_UNPAN_DATE2_D", !slashVisivle);


            //事前協議番号
            slashVisivle = this.SLASH_JIZENKYOUGI_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_JIZENKYOUGI_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_JIZEN_DATE_CTL", !slashVisivle);
            this.SetFieldVisible("FH_JIZEN_NUMBER_CTL", !slashVisivle);
            






            //B1
            slashVisivle = this.SLASH_B1_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_B1_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_CHECK_B1_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B1_2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B1_3_CTL", !slashVisivle);

            //B2
            slashVisivle = this.SLASH_B2_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_B2_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_CHECK_B2_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B2_2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_B2_3_CTL", !slashVisivle);

            //D
            slashVisivle = this.SLASH_D_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_D_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_CHECK_D_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_D_2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_D_3_CTL", !slashVisivle);

            //E
            slashVisivle = this.SLASH_E_FLG;
            this.SetFieldVisible("FH_PRT_SLASH_E_FLG", slashVisivle);
            //関連項目は斜線と反対（！）の表示
            this.SetFieldVisible("FH_CHECK_E_1_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_E_2_CTL", !slashVisivle);
            this.SetFieldVisible("FH_CHECK_E_3_CTL", !slashVisivle);


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
                    // 事前協議年月日
                    this.jizenDate = new ReportDate(this.SetFieldName, list[5], false);
                    // 照合確認B1票
                    this.checkB1 = new ReportDate(this.SetFieldName, list[30], false);
                    // 照合確認B2票
                    this.checkB2 = new ReportDate(this.SetFieldName, list[31], false);
                    // 照合確認D票
                    this.checkD2 = new ReportDate(this.SetFieldName, list[32], false);
                    // 照合確認E票
                    this.checkE2 = new ReportDate(this.SetFieldName, list[33], false);

                    // 整理番号
                    this.seirino = list[2];
                    // 交付担当者
                    this.koufutantousha = list[3];

                    // 事前協議番号
                    this.jizennumber = list[4];
                    // 20140612 katen 不具合No.4469 start‏
                    //// 排出事業者名
                    //this.haishutujigyoushaname = list[6];
                    // 20140612 katen 不具合No.4469 end‏
                    // 排出事業者郵便番号
                    this.haishutujigyoushapost = list[7];
                    // 排出事業者電話番号
                    this.haishutujigyoushatel = list[8];
                    // 20140612 katen 不具合No.4469 start‏
                    //// 排出事業場住所
                    //this.haishutujigyoushaadress = list[9];
                    //// 排出事業場名称
                    //this.haishutujigyoubaname = list[10];
                    // 20140612 katen 不具合No.4469 end‏
                    // 排出事業場郵便番号
                    this.haishutujigyoubapost = list[11];
                    // 排出事業場電話番号
                    this.haishutujigyoubatel = list[12];
                    // 20140612 katen 不具合No.4469 start‏
                    //// 排出事業場住所
                    //this.haishutujigyoubaadress = list[13];
                    // 20140612 katen 不具合No.4469 end‏

                    // 20140612 katen 不具合No.4469 start‏
                    if (list[6].Length > 40)
                    {
                        // 排出事業者名1
                        this.haishutujigyoushaname = list[6].Substring(0, 40).TrimEnd(' ');
                        // 排出事業者名2
                        this.haishutujigyoushaname2 = list[6].Substring(40);
                    }
                    else
                    {
                        // 排出事業者名1
                        this.haishutujigyoushaname = list[6].TrimEnd(' ');
                    }
                    if (list[9].Length > 48)
                    {
                        // 排出事業場住所1
                        this.haishutujigyoushaadress = list[9].Substring(0, 48).TrimEnd(' ');
                        // 排出事業場住所2
                        this.haishutujigyoushaadress2 = list[9].Substring(48);
                    }
                    else
                    {
                        // 排出事業場住所1
                        this.haishutujigyoushaadress = list[9].TrimEnd(' ');
                    }
                    if (list[10].Length > 40)
                    {
                        // 排出事業場名称1
                        this.haishutujigyoubaname = list[10].Substring(0, 40).TrimEnd(' ');
                        // 排出事業場名称2
                        this.haishutujigyoubaname2 = list[10].Substring(40);
                    }
                    else
                    {
                        // 排出事業場名称1
                        this.haishutujigyoubaname = list[10].TrimEnd(' ');
                    }
                    if (list[13].Length > 48)
                    {
                        // 排出事業場住所1
                        this.haishutujigyoubaadress = list[13].Substring(0, 48).TrimEnd(' ');
                        // 排出事業場住所2
                        this.haishutujigyoubaadress2 = list[13].Substring(48);
                    }
                    else
                    {
                        // 排出事業場住所1
                        this.haishutujigyoubaadress = list[13].TrimEnd(' ');
                    }
                    // 20140612 katen 不具合No.4469 end‏

                    // 中間処理産業廃棄物区分("1"の場合は"ㇾ"、"0"の場合は"")
                    // 帳簿記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
                    // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
                    if (list[14] == "1")
                    {
                        // 中間処理産業廃棄物区分
                        // 帳簿記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")                      
                        this.chuukanhaikikbn1 = "○";
                    }
                    else if (list[14] == "2")
                    {
                        // 中間処理産業廃棄物区分
                        // 当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
                        this.chuukanhaikikbn2 = "○";
                    }

                    // 中間処理産業廃棄物
                    this.chuukanhaikibutsu = list[15];

                    // 最終処分の場所区分・委託契約書記載チェック("1"の場合は"ㇾ"、それ以外の場合は"")
                    if (list[16] == "1")
                    {
                        this.lastsbnyoteikbn1 = "○";
                    }
                    else if (list[16] == "2")
                    {
                        // 最終処分の場所区分・当欄記載チェック("2"の場合は"ㇾ"、それ以外の場合は"")
                        this.lastsbnyoteikbn2 = "○";
                    }
                    // 最終処分の場所現場名称
                    this.lastsbnyoteigenbaname = list[17];
                    //// 最終処分の場所電話番号
                    //this.lastsbnyoteigenbatel = list[18];
                    //// 最終処分の場所郵便番号
                    //this.lastsbnyoteigenbapost = list[19];
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
                    //// 積換保管名
                    //this.tmhgenbaname = list[23];
                    // 積換保管郵便番号
                    this.tmhgenbapost = list[23];
                    // 積換保管電話番号
                    this.tmhgenbatel = list[24];
                    // 積換保管住所
                    this.tmhgenbaadress = list[25];

                    // 有価物拾集有無
                    if (list[26] == "1")
                    {
                        this.yuukakbn1 = "○";
                    }
                    else if (list[26] == "2")
                    {
                        // 有価物拾集有無
                        this.yuukakbn2 = "○";
                    }

                    // 実績数量
                    this.jissekiNumber = this.FormatSuuryo(list[27]);
                    // 実績数量単位
                    if (!String.IsNullOrEmpty(list[28]))
                    {
                        this.jissekiUnit[0] = list[28] == "1" ? "○" : String.Empty; // t
                        this.jissekiUnit[1] = list[28] == "2" ? "○" : String.Empty; // ㎥
                    }

                    // 追加突起事項
                    this.bikou = list[29];
                    if (list[34] == "1")
                    {
                        // 印字単位CD(t)
                        this.prtunitcd1 = "○";
                    }
                    if (list[34] == "2")
                    {
                        // 20140625 ria EV005037 建廃マニフェストを印字した時「産業廃棄物の種類　単位」項目にて「kg」と「」が逆で印字される。 start
                        //// 印字単位CD(kg)
                        //this.prtunitcd2 = "○";
                        // 印字単位CD(㎡)
                        this.prtunitcd3 = "○";
                        // 20140625 ria EV005037 建廃マニフェストを印字した時「産業廃棄物の種類　単位」項目にて「kg」と「」が逆で印字される。 end
                    }
                    if (list[34] == "3")
                    {
                        // 20140625 ria EV005037 建廃マニフェストを印字した時「産業廃棄物の種類　単位」項目にて「kg」と「」が逆で印字される。 start
                        //// 印字単位CD(㎡)
                        //this.prtunitcd3 = "○";
                        // 印字単位CD(kg)
                        this.prtunitcd2 = "○";
                        // 20140625 ria EV005037 建廃マニフェストを印字した時「産業廃棄物の種類　単位」項目にて「kg」と「」が逆で印字される。 end
                    }
                    if (list[34] == "4")
                    {
                        // 印字単位CD(ℓ)
                        this.prtunitcd4 = "○";
                    }
                    // 総重量又は総容量
                    this.prtsuu = this.FormatSuuryo(list[35]);
                    // 印刷部数（受渡し項目）
                    this.printsets = list[36];                    // 印刷部数（受渡し項目）
                    //交付担当者所属
                    this.KOUFU_TANTOUSHA_SHOZOKU = list[37];

                    if (list[38] != null && !string.IsNullOrWhiteSpace(list[38]) && list[39] != null && !string.IsNullOrWhiteSpace(list[39]))
                    {
                        this.kamimainh = "H: " + list[38] + "-" + list[39];
                    }

                    if (list[40] != null && !string.IsNullOrWhiteSpace(list[40]) && list[41] != null && !string.IsNullOrWhiteSpace(list[41]))
                    {
                        this.kamimaint = "T: " + list[40] + "-" + list[41];
                    }
                    // 処分受託者CD（業者CD）
                    this.sbngyoushacd = list[42];
                    // ﾏﾆ用紙へ印字
                    this.kamimainkbn = list[43];

                }
                else if (list[0] == "2-1")
                {
                    // 明細部分の列定義（2-1）
                    for (var i = 0; i < 2; i++)
                    {
                        var idx = i * ReportInfoR692.UpnFieldCount;
                        // 運搬受託者
                        this.upnRoute[i] = new ManifestRouteElement(this.SetFieldName)
                        {
                            Name = list[idx + 1],
                            Post = list[idx + 2],
                            Tel = list[idx + 3],
                            Address = list[idx + 4]
                        };

                        // 積替保管有無
                        if (list[idx + 5] == "1")
                        {
                            this.tmhKbn[i][1] = "○";
                        }
                        else if (list[idx + 5] == "0")
                        {
                            this.tmhKbn[i][0] = "○";
                        }

                        // 車輛
                        this.upnCar[i][0] = list[idx + 6];
                        // 車種
                        this.upnCar[i][1] = list[idx + 7];

                        // 運搬先の事業場
                        this.upnRoutePlace[i] = new ManifestRouteElement(this.SetFieldName)
                        {
                            Name = list[idx + 8],
                            Post = list[idx + 9],
                            Tel = list[idx + 10],
                            Address = list[idx + 11]
                        };

                        // 運搬の受託
                        this.upnJyutakuRoute[i] = new UpnJyutakuElement(this.SetFieldName)
                        {
                            Name = list[idx + 12],
                            Person = list[idx + 13],
                            EndDate = new ReportDate(this.SetFieldName, list[idx + 14], false)
                        };
                        if (i == 0)
                        {
                            if (list[15] != null && !string.IsNullOrWhiteSpace(list[15]))
                            {
                                this.kamimainu1 = "U1:" + list[15];
                            }
                        }
                        else if (i == 1)
                        {
                            if (list[idx + 15] != null && !string.IsNullOrWhiteSpace(list[idx + 15]))
                            {
                                this.kamimainu2 = "U2:" + list[idx + 15];
                            }
                        }

                        if (i == 0)
                        {
                            this.upnsakigenbacd = list[idx + 16];
                            if (this.upnsakigenbacd != null && !string.IsNullOrWhiteSpace(this.upnsakigenbacd) && this.sbngyoushacd != null && !string.IsNullOrWhiteSpace(this.sbngyoushacd))
                            {
                                kamimains = "S: " + this.sbngyoushacd + "-" + this.upnsakigenbacd;
                            }
                        }

                    }

                    // 処分の受託 33(16*2)～
                    for (var i = 0; i < 2; i++)
                    {
                        var idx = (i * 3) + (ReportInfoR692.UpnFieldCount * 2);
                        this.sbnJyutakuRoute[i] = new UpnJyutakuElement(this.SetFieldName)
                        {
                            Name = list[idx + 1],
                            Person = list[idx + 2],
                            EndDate = new ReportDate(this.SetFieldName, list[idx + 3], false)
                        };
                    }

                    this.lastSbnDate = new ReportDate(this.SetFieldName, list[39], false);
                    this.lastSbnCheckName = list[40];

                    // 最終処分を行った場所
                    this.lastSbnResult = new ManifestRouteElement(this.SetFieldName)
                    {
                        Name = list[41],
                        Post = ReportCommonUtility.AddPostMark(list[42]), //空でなければ郵便記号を付ける
                        Address = list[43]
                    };

                    this.LAST_SBN_GENBA_NUMBER = ReportCommonUtility.AddNoMark(list[44]); //空でなければ"No."を付ける
                }
                else if (list[0] == "3-1")
                {
                    // 明細部分の列定義（3-1）
                    if (list[1] == "1")
                    {
                        // コンクリートがらチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno = "○";
                        }
                        this.haikisuuryou1 = this.FormatSuuryo(list[4]);                // コンクリートがら
                    }
                    if (list[1] == "2")
                    {
                        // アスコンがらチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno2 = "○";
                        }
                        this.haikisuuryou2 = this.FormatSuuryo(list[4]);                // アスコンがら
                    }
                    if (list[1] == "3")
                    {
                        // その他がれき類チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno3 = "○";
                        }
                        this.haikisuuryou = this.FormatSuuryo(list[4]);                // その他がれき類
                    }
                    if (list[1] == "4")
                    {
                        // ガラス・陶磁器くずチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno4 = "○";
                        }
                        this.haikisuuryou4 = this.FormatSuuryo(list[4]);                // ガラス・陶磁器くず
                    }
                    if (list[1] == "5")
                    {
                        // 廃プラスチック類チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno5 = "○";
                        }
                        this.haikisuuryou5 = this.FormatSuuryo(list[4]);                // 廃プラスチック類
                    }
                    if (list[1] == "6")
                    {
                        // 金属くずチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno6 = "○";
                        }
                        this.haikisuuryou6 = this.FormatSuuryo(list[4]);                // 金属くず
                    }
                    if (list[1] == "7")
                    {
                        // 混合（安定型のみ）チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno7 = "○";
                        }
                        this.haikisuuryou7 = this.FormatSuuryo(list[4]);                // 混合（安定型のみ）
                    }
                    if (list[1] == "8")
                    {
                        // 石綿含有産業廃棄物チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno8 = "○";
                        }
                        this.haikisuuryou8 = this.FormatSuuryo(list[4]);                // 石綿含有産業廃棄物
                    }
                    if (list[1] == "9")
                    {
                        // フリー項目(安定)-1チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno9 = "○";
                        }
                        // 3-1．カラム1(連番)="9"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname1 = list[2] + " " + list[3];
                        this.haikisuuryou9 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "10")
                    {
                        // フリー項目(安定)-2チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno10 = "○";
                        }
                        // 3-1．カラム1(連番)="10"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname2 = list[2] + " " + list[3];
                        this.haikisuuryou10 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "11")
                    {
                        // フリー項目(安定)-3チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno11 = "○";
                        }
                        // 3-1．カラム1(連番)="11"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname3 = list[2] + " " + list[3];
                        this.haikisuuryou11 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "12")
                    {
                        // フリー項目(安定)-4チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno12 = "○";
                        }
                        // 3-1．カラム1(連番)="12"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname4 = list[2] + " " + list[3];
                        this.haikisuuryou12 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "13")
                    {
                        // 建設汚泥チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno13 = "○";
                        }
                        this.haikisuuryou13 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "14")
                    {
                        // 紙くずチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno14 = "○";
                        }
                        this.haikisuuryou14 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "15")
                    {
                        // 木くずチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno15 = "○";
                        }
                        this.haikisuuryou15 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "16")
                    {
                        // 繊維くずチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno16 = "○";
                        }
                        this.haikisuuryou16 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "17")
                    {
                        // 廃石膏ボードチェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno17 = "○";
                        }
                        this.haikisuuryou17 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "18")
                    {
                        // 混合（管理型含む）チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno18 = "○";
                        }
                        this.haikisuuryou18 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "19")
                    {
                        // 石綿含有産業廃棄物チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno19 = "○";
                        }
                        this.haikisuuryou19 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "20")
                    {
                        // フリー項目(管理)-1チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno20 = "○";
                        }
                        // 3-1．カラム1(連番)="12"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname5 = list[2] + " " + list[3];
                        this.haikisuuryou20 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "21")
                    {
                        // フリー項目(管理)-2チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno21 = "○";
                        }
                        // 3-1．カラム1(連番)="12"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname6 = list[2] + " " + list[3];
                        this.haikisuuryou21 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "22")
                    {
                        // フリー項目(管理)-3チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno22 = "○";
                        }
                        // 3-1．カラム1(連番)="12"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname7 = list[2] + " " + list[3];
                        this.haikisuuryou22 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "23")
                    {
                        // 廃石綿等チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno23 = "○";
                        }
                        this.haikisuuryou23 = this.FormatSuuryo(list[4]);               // 廃石綿等
                    }
                    if (list[1] == "24")
                    {
                        // フリー項目(特別)-1チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno24 = "○";
                        }
                        // 3-1．カラム1(連番)="12"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname8 = list[2] + " " + list[3];
                        this.haikisuuryou24 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "25")
                    {
                        // フリー項目(特別)-2チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno25 = "○";
                        }
                        // 3-1．カラム1(連番)="12"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname9 = list[2] + " " + list[3];
                        this.haikisuuryou25 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "26")
                    {
                        // フリー項目(特別)-3チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno26 = "○";
                        }
                        // 3-1．カラム1(連番)="12"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname10 = list[2] + " " + list[3];
                        this.haikisuuryou26 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "27")
                    {
                        // フリー項目(特別)-4チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno27 = "○";
                        }
                        // 3-1．カラム1(連番)="27"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname11 = list[2] + " " + list[3];
                        this.haikisuuryou27 = this.FormatSuuryo(list[4]);
                    }
                    if (list[1] == "28")
                    {
                        // フリー項目(特別)-5チェック
                        if (!string.IsNullOrWhiteSpace(list[5]) && Convert.ToBoolean(list[5]))
                        {
                            this.haikishuruirecno28 = "○";
                        }
                        // 3-1．カラム1(連番)="28"の場合は印字する
                        // 3-1．カラム2 + " " + 3-1．カラム3
                        this.haikishuruiname12 = list[2] + " " + list[3];
                        this.haikisuuryou28 = this.FormatSuuryo(list[4]);
                    }
                }
                else if (list[0] == "4-1")
                {
                    // 明細部分の列定義（4-1）
                    if (list[1] == "1")
                    {
                        // 固形状チェック
                        this.keijourecno1 = "○";
                    }
                    if (list[1] == "2")
                    {
                        // 泥状チェック
                        this.keijourecno2 = "○";
                    }
                    if (list[1] == "3")
                    {
                        // 液状チェック
                        this.keijourecno3 = "○";
                    }
                    if (list[1] == "4")
                    {
                        // フリー項目(形状)-1チェック
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.keijourecno4 = "○";
                        }
                        // 4-1．カラム1(連番)="4"の場合は印字する
                        // 4-1．カラム2 + " " + 4-1．カラム3
                        this.keijouname1 = list[2] + " " + list[3];
                        //this.keijouname1 = string.Empty;                // フリー項目(形状)名-1
                    }

                    if (list[1] == "5")
                    {
                        // フリー項目(形状)-2チェック
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.keijourecno5 = "○";
                        }
                        // 4-1．カラム1(連番)="5"の場合は印字する
                        // 4-1．カラム2 + " " + 4-1．カラム3
                        this.keijouname2 = list[2] + " " + list[3];
                        //this.keijouname2 = string.Empty;                // フリー項目(形状)名-2
                    }

                    //this.keijourecno6 = string.Empty;                // フリー項目(形状)-3チェック
                    if (list[1] == "6")
                    {
                        // フリー項目(形状)-3チェック
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.keijourecno6 = "○";
                        }
                        // 4-1．カラム1(連番)="6"の場合は印字する
                        // 4-1．カラム2 + " " + 4-1．カラム3
                        this.keijouname3 = list[2] + " " + list[3];
                        //this.keijouname3 = string.Empty;             // フリー項目(形状)名-3
                    }

                    //this.keijourecno7 = string.Empty;                // フリー項目(形状)-4チェック
                    if (list[1] == "7")
                    {
                        // フリー項目(形状)-4チェック
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.keijourecno7 = "○";
                        }
                        // 4-1．カラム1(連番)="7"の場合は印字する
                        // 4-1．カラム2 + " " + 4-1．カラム3
                        this.keijouname4 = list[2] + " " + list[3];
                        //this.keijouname4 = string.Empty;                // フリー項目(形状)名-4
                    }
                }
                else if (list[0] == "5-1")
                {
                    // 明細部分の列定義（5-1）
                    if (list[1] == "1")
                    {
                        // バラチェック
                        this.nisugatarecno1 = "○";
                    }
                    if (list[1] == "2")
                    {
                        // コンテナチェック
                        this.nisugatarecno2 = "○";
                    }
                    if (list[1] == "3")
                    {
                        // ドラム缶チェック
                        this.nisugatarecno3 = "○";
                    }
                    if (list[1] == "4")
                    {
                        // 袋チェック
                        this.nisugatarecno4 = "○";
                    }

                    //this.nisugatarecno5 = string.Empty;                // フリー項目(荷姿)-1チェック
                    if (list[1] == "5")
                    {
                        // フリー項目(形状)-2チェック
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.nisugatarecno5 = "○";
                        }
                        // 5-1．カラム1(連番)="5"の場合は印字する
                        // 5-1．カラム2 + " " + 5-1．カラム3
                        this.nisugataname1 = list[2] + " " + list[3];
                        //this.nisugataname1 = string.Empty;             // フリー項目(荷姿)名-1
                    }

                    //this.nisugatarecno6 = string.Empty;                // フリー項目(荷姿)-2チェック
                    if (list[1] == "6")
                    {
                        // フリー項目(荷姿)-2チェック
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.nisugatarecno6 = "○";
                        }
                        // 5-1．カラム1(連番)="6"の場合は印字する
                        // 5-1．カラム2 + " " + 5-1．カラム3
                        this.nisugataname2 = list[2] + " " + list[3];
                        //this.nisugataname2 = string.Empty;                // フリー項目(荷姿)名-2
                    }

                    //this.nisugatarecno7 = string.Empty;             // フリー項目(荷姿)-3チェック
                    if (list[1] == "7")
                    {
                        // フリー項目(荷姿)-3チェック
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.nisugatarecno7 = "○";
                        }
                        // 5-1．カラム1(連番)="7"の場合は印字する
                        // 5-1．カラム2 + " " + 5-1．カラム3
                        this.nisugataname3 = list[2] + " " + list[3];
                        //this.nisugataname3 = string.Empty;                // フリー項目(荷姿)名-3
                    }
                }
                else if (list[0] == "6-1")
                {
                    // 明細部分の列定義（6-1）
                    if (list[1] == "1")
                    {
                        // 処分方法脱水
                        this.shobunhouhourecno1 = "○";
                    }
                    if (list[1] == "2")
                    {
                        // 処分方法焼却
                        this.shobunhouhourecno2 = "○";
                    }
                    if (list[1] == "3")
                    {
                        // 処分方法破砕
                        this.shobunhouhourecno3 = "○";
                    }
                    if (list[1] == "4")
                    {
                        // 処分方法フリー1
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.shobunhouhourecno4 = "○";
                        }
                        this.shobunhouhouname1 = list[3];             // 処分方法フリー1名
                    }
                    if (list[1] == "5")
                    {
                        // 積処分方法フリー2
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.shobunhouhourecno5 = "○";
                        }
                        this.shobunhouhouname2 = list[3];             // 処分方法フリー2名                      
                    }
                    if (list[1] == "6")
                    {
                        // 処分方法フリー3
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.shobunhouhourecno6 = "○";
                        }
                        this.shobunhouhouname3 = list[3];             // 処分方法フリー3名                      
                    }
                    if (list[1] == "7")
                    {
                        // 処分方法フリー4
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.shobunhouhourecno7 = "○";
                        }
                        this.shobunhouhouname4 = list[3];             // 処分方法フリー4名                      
                    }
                    if (list[1] == "8")
                    {
                        // 処分方法フリー5
                        if (!string.IsNullOrWhiteSpace(list[4]) && Convert.ToBoolean(list[4]))
                        {
                            this.shobunhouhourecno8 = "○";
                        }
                        this.shobunhouhouname5 = list[3];             // 処分方法フリー5名                      
                    }

                    if (list[1] == "11")
                    {
                        // 最終処分安定型
                        this.shobunhouhourecno11 = "○";             // 最終処分安定型
                    }
                    if (list[1] == "12")
                    {
                        // 最終処分管理型
                        this.shobunhouhourecno12 = "○";             // 最終処分管理型
                    }
                    if (list[1] == "13")
                    {
                        // 最終処分遮断型
                        this.shobunhouhourecno13 = "○";             // 最終処分遮断型          
                    }
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

            LogUtility.DebugMethodEnd(dataTable);
        }
    }
}
