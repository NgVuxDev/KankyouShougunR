using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using System.Data;
using System.Windows.Forms;

namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
{
    /// <summary> R391(マニュフェスト推移表)を表すクラス・コントロール </summary>
    public class ReportInfoR391 : ReportInfoBase
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();
        // <summary>List<T>クラスの（Detail型）のリストとしてインスタンス</summary>
        private List<Detail> detail = new List<Detail>();
        // <summary>List<T>クラスの（Detail型）のリストとしてインスタンス</summary>
        private List<Detail2> detail2 = new List<Detail2>();
        // <summary>List<T>クラスの（Group1Footer型）のリストとしてインスタンス</summary>
        private List<Group1Footer> group1Footer = new List<Group1Footer>();
        // <summary>List<T>クラスの（UpnGroup1Footer型）のリストとしてインスタンス</summary>
        private List<UpnGroup1Footer> upngroup1Footer = new List<UpnGroup1Footer>();
        // <summary>List<T>クラスの（Group2Header型）のリストとしてインスタンス</summary>
        private List<Group2Hedder> group2Hedder = new List<Group2Hedder>();
        // <summary>List<T>クラスの（Group2Footer型）のリストとしてインスタンス</summary>
        private List<Group2Footer> group2Footer = new List<Group2Footer>();
        // <summary>List<T>クラスの（LastGroup2Hedder型）のリストとしてインスタンス</summary>
        private List<LastGroup2Hedder> lastgroup2Header = new List<LastGroup2Hedder>();
        // <summary>List<T>クラスの（LastGroup2Footer型）のリストとしてインスタンス</summary>
        private List<LastGroup2Footer> lastgroup2Footer = new List<LastGroup2Footer>();
        // <summary>List<T>クラスの（UpnGroup2Footer型）のリストとしてインスタンス</summary>
        private List<UpnGroup2Footer> upngroup2Footer = new List<UpnGroup2Footer>();
        // <summary>List<T>クラスの（Group3Footer型）のリストとしてインスタンス</summary>
        private List<Group3Footer> group3Footer = new List<Group3Footer>();
        // インデックスデータ識別
        int seq = -1;
        // インデックスデータ識別
        int index = 0;
        // Detillインデックスデータ識別
        int seqindex = 0;

        // ヘッダー部分の列定義（0-1）
        // レイアウトNo
        private string layoutno = string.Empty;
        // 判定用
        private string hantei = string.Empty;

        // ヘッダー部分の列定義（1-1）
        // 一次二次区分
        private string firstsecoundkbn = string.Empty;
        // 月カラム
        private string janfromdecgokeikbn = string.Empty;
        // 1月カラム
        private string janmonthkbn = string.Empty;
        // 2月カラム
        private string febmonthkbn = string.Empty;
        // 3月カラム
        private string marmonthkbn = string.Empty;
        // 4月カラム
        private string aprmonthkbn = string.Empty;
        // 5月カラム
        private string maymonthkbn = string.Empty;
        // 6月カラム
        private string junmonthkbn = string.Empty;
        // 7月カラム
        private string julymonthkbn = string.Empty;
        // 8月カラム
        private string augmonthkbn = string.Empty;
        // 9月カラム
        private string sepmonthkbn = string.Empty;
        // 10月カラム
        private string octmonthkbn = string.Empty;
        // 11月カラム
        private string novmonthkbn = string.Empty;
        // 12月カラム
        private string decmonthkbn = string.Empty;
        // 合計カラム
        private string janfromdecgokeitotalkbn = string.Empty;
        // グループ２ヘッダー部分の列定義（2-1）     
        // 排出事業者CD
        private string g2haishutujigyoushacd = string.Empty;
        // 排出事業者名
        private string g2haishutujigyoushaname = string.Empty;
        // 会社名(略称)
        private string corp = string.Empty;

        // 詳細部分の列定義（2-2）
        // 廃棄物区分ラベル(運搬受託者用)
        private string haikikbn = string.Empty;
        private string tumikaekbn = string.Empty; 
        // 排出事業場CD
        private string haishutujigyoujocd = string.Empty;
        // 排出事業者名
        private string haishutujigyoujoname = string.Empty;
        // 廃棄物種類
        private string haikishurui = string.Empty;
        // 単位
        private string tani = string.Empty;

        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
        // 最終処分業者CD
        private string lastsbngyoshacd = string.Empty;
        // 最終処分業者名
        private string lastsbngyoshaname = string.Empty;
        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end

        // 最終処分場CD
        private string lastsbngenbacd = string.Empty;
        // 最終処分場名
        private string lastsbngenbaname = string.Empty;

        //廃棄物種類-1～12月別
        private string shuruijanfromdec = string.Empty;

        //廃棄物種類-1月別
        private string shuruijanuary = string.Empty;
        //廃棄物種類-2月別
        private string shuruifebruary = string.Empty;
        //廃棄物種類-3月別
        private string shuruimarch = string.Empty;
        //廃棄物種類-4月別
        private string shuruiapril = string.Empty;
        //廃棄物種類-5月別
        private string shuruimay = string.Empty;
        //廃棄物種類-6月別
        private string shuruijune = string.Empty;
        //廃棄物種類-7月別
        private string shuruijuly = string.Empty;
        //廃棄物種類-8月別
        private string shuruiaugust = string.Empty;
        //廃棄物種類-9月別
        private string shuruiseptember = string.Empty;
        //廃棄物種類-10月別
        private string shuruioctober = string.Empty;
        //廃棄物種類-11月別
        private string shuruinovember = string.Empty;
        //廃棄物種類-12月別
        private string shuruidecember = string.Empty;
        //廃棄物種類-合計
        private string allshuruitotal = string.Empty;

        // グループ２フッターの列定義（2-3）
        // 合計ラベル(運搬受託者用)
        private string haikikbngoukei = string.Empty;
        // 排出事業者計-1～12月別
        private string g2fjanfromdecjigyoshatotal = string.Empty;
        // 合計-1月別
        private string g2fjanjigyoshatotal2 = string.Empty;
        // 合計-2月別
        private string g2ffebjigyoshatotal2 = string.Empty;
        // 合計-3月別
        private string g2fmarjigyoshatotal2 = string.Empty;
        // 合計-4月別
        private string g2faprjigyoshatotal2 = string.Empty;
        // 合計-5月別
        private string g2fmayjigyoshatotal2 = string.Empty;
        // 合計-6月別
        private string g2fjunjigyoshatotal2 = string.Empty;
        // 合計-7月別
        private string g2fjlyjigyoshatotal2 = string.Empty;
        // 合計-8月別
        private string g2faugjigyoshatotal2 = string.Empty;
        // 合計-9月別
        private string g2fsepjigyoshatotal2 = string.Empty;
        // 合計-10月別
        private string g2foctjigyoshatotal2 = string.Empty;
        // 合計-11月別
        private string g2fnovjigyoshatotal2 = string.Empty;
        // 合計-12月別
        private string g2fdecjigyoshatotal2 = string.Empty;
        // 合計-合計
        private string g2fhaishutujigyoshaalltotal2 = string.Empty;

        // 排出事業者計-1～12月別
        // 排出事業者計-1月別
        private string g2fjanjigyoshatotal = string.Empty;
        // 排出事業者計-2月別
        private string g2ffebjigyoshatotal = string.Empty;
        // 排出事業者計-3月別
        private string g2fmarjigyoshatotal = string.Empty;
        // 排出事業者計-4月別
        private string g2faprjigyoshatotal = string.Empty;
        // 排出事業者計-5月別
        private string g2fmayjigyoshatotal = string.Empty;
        // 排出事業者計-6月別
        private string g2fjunjigyoshatotal = string.Empty;
        // 排出事業者計-7月別
        private string g2fjlyjigyoshatotal = string.Empty;
        // 排出事業者計-8月別
        private string g2faugjigyoshatotal = string.Empty;
        // 排出事業者計-9月別
        private string g2fsepjigyoshatotal = string.Empty;
        // 排出事業者計-10月別
        private string g2foctjigyoshatotal = string.Empty;
        // 排出事業者計-11月別
        private string g2fnovjigyoshatotal = string.Empty;
        // 排出事業者計-12月別
        private string g2fdecjigyoshatotal = string.Empty;
        // 排出事業者計-合計
        private string g2fhaishutujigyoshaalltotal = string.Empty;

        // グループ２フッターの列定義（2-3）最終処分場計
        // 最終処分場計-1月別
        private string lastgenbamanth1total = string.Empty;
        // 最終処分場計-2月別
        private string lastgenbamanth2total = string.Empty;
        // 最終処分場計-3月別
        private string lastgenbamanth3total = string.Empty;
        // 最終処分場計-4月別
        private string lastgenbamanth4total = string.Empty;
        // 最終処分場計-5月別
        private string lastgenbamanth5total = string.Empty;
        // 最終処分場計-6月別
        private string lastgenbamanth6total = string.Empty;
        // 最終処分場計-7月別
        private string lastgenbamanth7total = string.Empty;
        // 最終処分場計-8月別
        private string lastgenbamanth8total = string.Empty;
        // 最終処分場計-9月別
        private string lastgenbamanth9total = string.Empty;
        // 最終処分場計-10月別
        private string lastgenbamanth10total = string.Empty;
        // 最終処分場計-11月別
        private string lastgenbamanth11total = string.Empty;
        // 最終処分場計-12月別
        private string lastgenbamanth12total = string.Empty;
        // 最終処分場計-合計
        private string lastgenbasbntotal = string.Empty;

        // グループ１フッターの列定義（2-4）総合計
        // 総合計ラベル(運搬受託者別)
        private string g1ftotalkbn = string.Empty;
        // 総合計-月別
        private string g2fjanalltotal = string.Empty;
        // 総合計-月別
        private string g2ffeballtotal = string.Empty;
        // 総合計-月別
        private string g2fmaralltotal = string.Empty;
        // 総合計-月別
        private string g2fapralltotal = string.Empty;
        // 総合計-月別
        private string g2fmayalltotal = string.Empty;
        // 総合計-月別
        private string g2fjunalltotal = string.Empty;
        // 総合計-月別
        private string g2fjlyalltotal = string.Empty;
        // 総合計-月別
        private string g2faugalltotal = string.Empty;
        // 総合計-月別
        private string g2fsepalltotal = string.Empty;
        // 総合計-月別
        private string g2foctalltotal = string.Empty;
        // 総合計-月別
        private string g2fnovalltotal = string.Empty;
        // 総合計-月別
        private string g2fdecalltotal = string.Empty;
        // 総合計-合計
        private string g2fentirealltotal = string.Empty;       

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfoR382"/> class.
        /// </summary>
        public ReportInfoR391()
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "Template/R391-Form.xml";
    
            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";
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
        public void R391_Report(DataTable chouhyouData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // フォームへ設定する情報処理
            // データテーブル作成処理のみ
            this.chouhyouDataTable = new DataTable();

            // 明細部分の列定義(排出事業者別)
            if (this.layoutno == "1")
            {
                // マニフェスト用の列定義
                this.OutputFormLayout = "LAYOUT1";

                DataRow row;

                this.chouhyouDataTable.Columns.Add("PHY_MANIFEST_VLB");                 // 一次二次区分
                
                this.chouhyouDataTable.Columns.Add("PHN_JIGYOSHA_CODE_VLB");            // 排出事業者CD
                this.chouhyouDataTable.Columns.Add("PHN_JIGYOSHA_NAME_VLB");            // 排出事業者名

                this.chouhyouDataTable.Columns.Add("PHY_JIGYOJO_CODE_FLB");             // 排出事業場CD
                this.chouhyouDataTable.Columns.Add("PHY_JIGYOJYO_NAME_FLB");            // 排出事業場名
                this.chouhyouDataTable.Columns.Add("PHY_HAIKI_KIND_FLB");               // 廃棄物種類
                this.chouhyouDataTable.Columns.Add("PHY_UNIT_FLB");                     // 単位
                
                this.chouhyouDataTable.Columns.Add("PHY_MONTH1_FLB");                   // 廃棄物種類-１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH2_FLB");                   // 廃棄物種類-２月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH3_FLB");                   // 廃棄物種類-３月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH4_FLB");                   // 廃棄物種類-４月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH5_FLB");                   // 廃棄物種類-５月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH6_FLB");                   // 廃棄物種類-６月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH7_FLB");                   // 廃棄物種類-７月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH8_FLB");                   // 廃棄物種類-８月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH9_FLB");                   // 廃棄物種類-９月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH10_FLB");                  // 廃棄物種類-１０月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH11_FLB");                  // 廃棄物種類-１１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH12_FLB");                  // 廃棄物種類-１２月別  
                this.chouhyouDataTable.Columns.Add("PHY_ALL_FLB");                      // 廃棄物種類-合計

                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_JIGYOTOTAL_VLB");        // 排出事業者計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_JIGYOTOTAL_VLB");        // 排出事業者計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_JIGYOTOTAL_VLB");        // 排出事業者計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_JIGYOTOTAL_VLB");        // 排出事業者計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_JIGYOTOTAL_VLB");        // 排出事業者計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_JIGYOTOTAL_VLB");        // 排出事業者計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_JIGYOTOTAL_VLB");        // 排出事業者計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_JIGYOTOTAL_VLB");        // 排出事業者計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_JIGYOTOTAL_VLB");        // 排出事業者計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_JIGYOTOTAL_VLB");       // 排出事業者計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_JIGYOTOTAL_VLB");       // 排出事業者計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_JIGYOTOTAL_VLB");       // 排出事業者計-１２月別        
                this.chouhyouDataTable.Columns.Add("PHN_JIGYOTOTAL_VLB");               // 排出事業者計-合計

                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_TOTAL_VLB");             // 総合計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_TOTAL_VLB");             // 総合計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_TOTAL_VLB");             // 総合計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_TOTAL_VLB");             // 総合計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_TOTAL_VLB");             // 総合計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_TOTAL_VLB");             // 総合計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_TOTAL_VLB");             // 総合計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_TOTAL_VLB");             // 総合計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_TOTAL_VLB");             // 総合計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_TOTAL_VLB");            // 総合計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_TOTAL_VLB");            // 総合計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_TOTAL_VLB");            // 総合計-１２月別
                this.chouhyouDataTable.Columns.Add("PHN_TOTAL_VLB");                    // 総合計-合計

                    // データテーブルと同じの構造をもったレコードを作成
                    row = this.chouhyouDataTable.NewRow();
                    // 帳票フォームに設定
                    row["PHY_MANIFEST_VLB"] = this.firstsecoundkbn;                     // 一次二次区分
                    row["PHY_MONTH1_FLB"] = this.janmonthkbn;                           // １月カラム
                    row["PHY_MONTH2_FLB"] = this.febmonthkbn;                           // ２月カラム
                    row["PHY_MONTH3_FLB"] = this.marmonthkbn;                           // ３月カラム
                    row["PHY_MONTH4_FLB"] = this.aprmonthkbn;                           // ４月カラム
                    row["PHY_MONTH5_FLB"] = this.maymonthkbn;                           // ５月カラム
                    row["PHY_MONTH6_FLB"] = this.junmonthkbn;                           // ６月カラム
                    row["PHY_MONTH7_FLB"] = this.julymonthkbn;                          // ７月カラム
                    row["PHY_MONTH8_FLB"] = this.augmonthkbn;                           // ８月カラム
                    row["PHY_MONTH9_FLB"] = this.sepmonthkbn;                           // ９月カラム
                    row["PHY_MONTH10_FLB"] = this.octmonthkbn;                          // １０月カラム
                    row["PHY_MONTH11_FLB"] = this.novmonthkbn;                          // １１月カラム
                    row["PHY_MONTH12_FLB"] = this.decmonthkbn;                          // １２月カラム
                    row["PHY_ALL_FLB"] = this.janfromdecgokeitotalkbn;                  // 合計カラム
       
                    for (int i = 0; i < this.detail.Count; i++)
                    {
                        // データテーブルと同じの構造をもったレコードを作成
                        row = this.chouhyouDataTable.NewRow();

                        row["PHN_JIGYOSHA_CODE_VLB"] = this.group2Hedder[detail[i].Seq].G2haishutujigyoushacd;      // 排出事業者CD
                        row["PHN_JIGYOSHA_NAME_VLB"] = this.group2Hedder[detail[i].Seq].G2haishutujigyoushaname;    // 排出事業者名

                        // 帳票フォームに設定
                        row["PHY_JIGYOJO_CODE_FLB"] = this.detail[i].Haishutujigyoujocd;                            // 排出事業場CD
                        row["PHY_JIGYOJYO_NAME_FLB"] = this.detail[i].Haishutujigyoujoname;                         // 排出事業場名
                        row["PHY_HAIKI_KIND_FLB"] = this.detail[i].Haikishurui;                                     // 廃棄物種類
                        row["PHY_UNIT_FLB"] = this.detail[i].Tani;                                                  // 単位

                        row["PHY_MONTH1_FLB"] = this.detail[i].Shuruijanuary;                                       // 廃棄物種類-１月別
                        row["PHY_MONTH2_FLB"] = this.detail[i].Shuruifebruary;                                      // 廃棄物種類-２月別
                        row["PHY_MONTH3_FLB"] = this.detail[i].Shuruimarch;                                         // 廃棄物種類-３月別
                        row["PHY_MONTH4_FLB"] = this.detail[i].Shuruiapril;                                         // 廃棄物種類-４月別
                        row["PHY_MONTH5_FLB"] = this.detail[i].Shuruimay;                                           // 廃棄物種類-５月別
                        row["PHY_MONTH6_FLB"] = this.detail[i].Shuruijune;                                          // 廃棄物種類-６月別
                        row["PHY_MONTH7_FLB"] = this.detail[i].Shuruijuly;                                          // 廃棄物種類-７月別
                        row["PHY_MONTH8_FLB"] = this.detail[i].Shuruiaugust;                                        // 廃棄物種類-８月別
                        row["PHY_MONTH9_FLB"] = this.detail[i].Shuruiseptember;                                     // 廃棄物種類-９月別
                        row["PHY_MONTH10_FLB"] = this.detail[i].Shuruioctober;                                      // 廃棄物種類-１０月別
                        row["PHY_MONTH11_FLB"] = this.detail[i].Shuruinovember;                                     // 廃棄物種類-１１月別
                        row["PHY_MONTH12_FLB"] = this.detail[i].Shuruidecember;                                     // 廃棄物種類-１２月別
                        row["PHY_ALL_FLB"] = this.detail[i].Allshuruitotal;                                         // 廃棄物種類-合計

                        // 帳票フォーム設定
                        row["PHN_MONTH1_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fjanjigyoshatotal;            // 排出事業者計-１月別
                        row["PHN_MONTH2_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2ffebjigyoshatotal;            // 排出事業者計-２月別
                        row["PHN_MONTH3_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fmarjigyoshatotal;            // 排出事業者計-３月別
                        row["PHN_MONTH4_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2faprjigyoshatotal;            // 排出事業者計-４月別
                        row["PHN_MONTH5_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fmayjigyoshatotal;            // 排出事業者計-５月別
                        row["PHN_MONTH6_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fjunjigyoshatotal;            // 排出事業者計-６月別
                        row["PHN_MONTH7_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fjlyjigyoshatotal;            // 排出事業者計-７月別
                        row["PHN_MONTH8_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2faugjigyoshatotal;            // 排出事業者計-８月別
                        row["PHN_MONTH9_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fsepjigyoshatotal;            // 排出事業者計-９月別
                        row["PHN_MONTH10_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2foctjigyoshatotal;           // 排出事業者計-１０月別
                        row["PHN_MONTH11_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fnovjigyoshatotal;           // 排出事業者計-１１月別
                        row["PHN_MONTH12_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fdecjigyoshatotal;           // 排出事業者計-１２月別        
                        row["PHN_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fhaishutujigyoshaalltotal;           // 排出事業者計-合計

                        // 帳票フォームに設定
                        row["PHN_MONTH1_TOTAL_VLB"] = this.group1Footer[0].G2fjanalltotal;                          // 総合計-１月別
                        row["PHN_MONTH2_TOTAL_VLB"] = this.group1Footer[0].G2ffeballtotal;                          // 総合計-２月別
                        row["PHN_MONTH3_TOTAL_VLB"] = this.group1Footer[0].G2fmaralltotal;                          // 総合計-３月別
                        row["PHN_MONTH4_TOTAL_VLB"] = this.group1Footer[0].G2fapralltotal;                          // 総合計-４月別
                        row["PHN_MONTH5_TOTAL_VLB"] = this.group1Footer[0].G2fmayalltotal;                          // 総合計-５月別
                        row["PHN_MONTH6_TOTAL_VLB"] = this.group1Footer[0].G2fjunalltotal;                          // 総合計-６月別
                        row["PHN_MONTH7_TOTAL_VLB"] = this.group1Footer[0].G2fjlyalltotal;                          // 総合計-７月別
                        row["PHN_MONTH8_TOTAL_VLB"] = this.group1Footer[0].G2faugalltotal;                          // 総合計-８月別
                        row["PHN_MONTH9_TOTAL_VLB"] = this.group1Footer[0].G2fsepalltotal;                          // 総合計-９月別
                        row["PHN_MONTH10_TOTAL_VLB"] = this.group1Footer[0].G2foctalltotal;                         // 総合計-１０月別
                        row["PHN_MONTH11_TOTAL_VLB"] = this.group1Footer[0].G2fnovalltotal;                         // 総合計-１１月別
                        row["PHN_MONTH12_TOTAL_VLB"] = this.group1Footer[0].G2fdecalltotal;                         // 総合計-１２月別
                        row["PHN_TOTAL_VLB"] = this.group1Footer[0].G2fentirealltotal;                              // 総合計-合計

                        this.chouhyouDataTable.Rows.Add(row);
                    }
            }
            // 明細部分の列定義(運搬受託者別)
            else if (this.layoutno == "2")
            {
                // 運搬受託者別用の列定義
                this.OutputFormLayout = "LAYOUT3";

                DataRow row;
                // 2-1 Start
                this.chouhyouDataTable.Columns.Add("PHY_MANIFEST_VLB");                         // 一次二次区分
                this.chouhyouDataTable.Columns.Add("G2H_HAIKI_KBN_VLB");                        // 廃棄物区分ラベル
                this.chouhyouDataTable.Columns.Add("PHY_UPN_GYOUSHA_CD_FLB");                   // 運搬受託者CD
                this.chouhyouDataTable.Columns.Add("PHY_UPN_GYOUSHA_NAME_FLB");                 // 運搬受託者名
                this.chouhyouDataTable.Columns.Add("PHY_HAIKI_KIND_FLB");                       // 廃棄物種類
                this.chouhyouDataTable.Columns.Add("PHY_UNIT_FLB");                             // 単位
                this.chouhyouDataTable.Columns.Add("PHY_MONTH1_FLB");                           // 廃棄物種類-１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH2_FLB");                           // 廃棄物種類-２月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH3_FLB");                           // 廃棄物種類-３月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH4_FLB");                           // 廃棄物種類-４月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH5_FLB");                           // 廃棄物種類-５月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH6_FLB");                           // 廃棄物種類-６月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH7_FLB");                           // 廃棄物種類-７月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH8_FLB");                           // 廃棄物種類-８月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH9_FLB");                           // 廃棄物種類-９月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH10_FLB");                          // 廃棄物種類-１０月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH11_FLB");                          // 廃棄物種類-１１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH12_FLB");                          // 廃棄物種類-１２月別  
                this.chouhyouDataTable.Columns.Add("PHY_ALL_FLB");                              // 廃棄物種類-合計
                // 2-1 End

                // 2-2 Start
                this.chouhyouDataTable.Columns.Add("G3F_UPN_GYOUSHA_TOTAL_VLB");                // 運搬受託者計ラベル
                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_UPN_GYOUSHA_TOTAL_VLB");         // 運搬受託者計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_UPN_GYOUSHA_TOTAL_VLB");        // 運搬受託者計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_UPN_GYOUSHA_TOTAL_VLB");        // 運搬受託者計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_UPN_GYOUSHA_TOTAL_VLB");        // 運搬受託者計-１２月別        
                this.chouhyouDataTable.Columns.Add("PHN_ALL_UPN_GYOUSHA_TOTAL_VLB");            // 運搬受託者計-合計
                // 2-2 End

                // 2-3 Start
                this.chouhyouDataTable.Columns.Add("G2F_HAIKI_KBN_TOTAL_VLB");                  // 積替用合計ラベル
                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_HAIKI_KBN_TOTAL_VLB");           // 積替用合計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_HAIKI_KBN_TOTAL_VLB");          // 積替用合計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_HAIKI_KBN_TOTAL_VLB");          // 積替用合計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_HAIKI_KBN_TOTAL_VLB");          // 積替用合計-１２月別
                this.chouhyouDataTable.Columns.Add("PHN_ALL_HAIKI_KBN_TOTAL_VLB");              // 積替用合計-合計
                // 2-3 

                // 2-4 
                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_TOTAL_VLB");                     // 総合計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_TOTAL_VLB");                     // 総合計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_TOTAL_VLB");                     // 総合計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_TOTAL_VLB");                     // 総合計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_TOTAL_VLB");                     // 総合計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_TOTAL_VLB");                     // 総合計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_TOTAL_VLB");                     // 総合計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_TOTAL_VLB");                     // 総合計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_TOTAL_VLB");                     // 総合計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_TOTAL_VLB");                    // 総合計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_TOTAL_VLB");                    // 総合計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_TOTAL_VLB");                    // 総合計-１２月別
                this.chouhyouDataTable.Columns.Add("PHN_TOTAL_VLB");                            // 総合計-合計
                // 2-4 

                // データテーブルと同じの構造をもったレコードを作成
                row = this.chouhyouDataTable.NewRow();
                // 1-1 
                // 帳票フォームに設定
                row["PHY_MANIFEST_VLB"] = this.firstsecoundkbn;                                 // 一次二次区分

                row["PHY_MONTH1_FLB"] = this.janmonthkbn;                                       // １月カラム
                row["PHY_MONTH2_FLB"] = this.febmonthkbn;                                       // ２月カラム
                row["PHY_MONTH3_FLB"] = this.marmonthkbn;                                       // ３月カラム
                row["PHY_MONTH4_FLB"] = this.aprmonthkbn;                                       // ４月カラム
                row["PHY_MONTH5_FLB"] = this.maymonthkbn;                                       // ５月カラム
                row["PHY_MONTH6_FLB"] = this.junmonthkbn;                                       // ６月カラム
                row["PHY_MONTH7_FLB"] = this.julymonthkbn;                                      // ７月カラム
                row["PHY_MONTH8_FLB"] = this.augmonthkbn;                                       // ８月カラム
                row["PHY_MONTH9_FLB"] = this.sepmonthkbn;                                       // ９月カラム
                row["PHY_MONTH10_FLB"] = this.octmonthkbn;                                      // １０月カラム
                row["PHY_MONTH11_FLB"] = this.novmonthkbn;                                      // １１月カラム
                row["PHY_MONTH12_FLB"] = this.decmonthkbn;                                      // １２月カラム
                row["PHY_ALL_FLB"] = this.janfromdecgokeitotalkbn;                              // 合計カラム
                // 1-1 

                for (int i = 0; i < this.detail2.Count; i++)
                {
                    // データテーブルと同じの構造をもったレコードを作成
                    row = this.chouhyouDataTable.NewRow();

                    // 2-1 
                    // 帳票フォームに設定
                    row["G2H_HAIKI_KBN_VLB"] = this.detail2[i].Haikikbn;                        // 廃棄物区分ラベル
                    row["PHY_UPN_GYOUSHA_CD_FLB"] = this.detail2[i].Haishutujigyoujocd;         // 運搬受託者CD
                    row["PHY_UPN_GYOUSHA_NAME_FLB"] = this.detail2[i].Haishutujigyoujoname;     // 運搬受託者名
                    row["PHY_HAIKI_KIND_FLB"] = this.detail2[i].Haikishurui;                    // 廃棄物種類
                    row["PHY_UNIT_FLB"] = this.detail2[i].Tani;                                 // 単位

                    row["PHY_MONTH1_FLB"] = this.detail2[i].Shuruijanuary;                      // 廃棄物種類-１月別
                    row["PHY_MONTH2_FLB"] = this.detail2[i].Shuruifebruary;                     // 廃棄物種類-２月別
                    row["PHY_MONTH3_FLB"] = this.detail2[i].Shuruimarch;                        // 廃棄物種類-３月別
                    row["PHY_MONTH4_FLB"] = this.detail2[i].Shuruiapril;                        // 廃棄物種類-４月別
                    row["PHY_MONTH5_FLB"] = this.detail2[i].Shuruimay;                          // 廃棄物種類-５月別
                    row["PHY_MONTH6_FLB"] = this.detail2[i].Shuruijune;                         // 廃棄物種類-６月別
                    row["PHY_MONTH7_FLB"] = this.detail2[i].Shuruijuly;                         // 廃棄物種類-７月別
                    row["PHY_MONTH8_FLB"] = this.detail2[i].Shuruiaugust;                       // 廃棄物種類-８月別
                    row["PHY_MONTH9_FLB"] = this.detail2[i].Shuruiseptember;                    // 廃棄物種類-９月別
                    row["PHY_MONTH10_FLB"] = this.detail2[i].Shuruioctober;                     // 廃棄物種類-１０月別
                    row["PHY_MONTH11_FLB"] = this.detail2[i].Shuruinovember;                    // 廃棄物種類-１１月別
                    row["PHY_MONTH12_FLB"] = this.detail2[i].Shuruidecember;                    // 廃棄物種類-１２月別
                    row["PHY_ALL_FLB"] = this.detail2[i].Allshuruitotal;                        // 廃棄物種類-合計
                    // 2-1 

                    // 2-2 
                    // 帳票フォーム設定
                    row["G3F_UPN_GYOUSHA_TOTAL_VLB"] = this.tumikaekbn;                // 運搬受託者計ラベル
                    row["PHN_MONTH1_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fjanjigyoshatotal;         // 運搬受託者計-１月別
                    row["PHN_MONTH2_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2ffebjigyoshatotal;         // 運搬受託者計-２月別
                    row["PHN_MONTH3_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fmarjigyoshatotal;         // 運搬受託者計-３月別
                    row["PHN_MONTH4_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2faprjigyoshatotal;         // 運搬受託者計-４月別
                    row["PHN_MONTH5_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fmayjigyoshatotal;         // 運搬受託者計-５月別
                    row["PHN_MONTH6_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fjunjigyoshatotal;         // 運搬受託者計-６月別
                    row["PHN_MONTH7_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fjlyjigyoshatotal;         // 運搬受託者計-７月別
                    row["PHN_MONTH8_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2faugjigyoshatotal;         // 運搬受託者計-８月別
                    row["PHN_MONTH9_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fsepjigyoshatotal;         // 運搬受託者計-９月別
                    row["PHN_MONTH10_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2foctjigyoshatotal;        // 運搬受託者計-１０月別
                    row["PHN_MONTH11_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fnovjigyoshatotal;        // 運搬受託者計-１１月別
                    row["PHN_MONTH12_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fdecjigyoshatotal;        // 運搬受託者計-１２月別        
                    row["PHN_ALL_UPN_GYOUSHA_TOTAL_VLB"] = this.group3Footer[detail2[i].Seq].G2fhaishutujigyoshaalltotal;    // 運搬受託者計-合計
                    // 2-2 

                    // 2-3 Start
                    // 帳票フォームに設定
                    row["G2F_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].Haikikbngoukei;                              // 積替用合計ラベル
                    row["PHN_MONTH1_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fjanjigyoshatotal2;                 // 積替用合計-１月別
                    row["PHN_MONTH2_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2ffebjigyoshatotal2;                 // 積替用合計-２月別
                    row["PHN_MONTH3_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fmarjigyoshatotal2;                 // 積替用合計-３月別
                    row["PHN_MONTH4_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2faprjigyoshatotal2;                 // 積替用合計-４月別
                    row["PHN_MONTH5_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fmayjigyoshatotal2;                 // 積替用合計-５月別
                    row["PHN_MONTH6_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fjunjigyoshatotal2;                 // 積替用合計-６月別
                    row["PHN_MONTH7_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fjlyjigyoshatotal2;                 // 積替用合計-７月別
                    row["PHN_MONTH8_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2faugjigyoshatotal2;                 // 積替用合計-８月別
                    row["PHN_MONTH9_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fsepjigyoshatotal2;                 // 積替用合計-９月別
                    row["PHN_MONTH10_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2foctjigyoshatotal2;                // 積替用合計-１０月別
                    row["PHN_MONTH11_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fnovjigyoshatotal2;                // 積替用合計-１１月別
                    row["PHN_MONTH12_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fdecjigyoshatotal2;                // 積替用合計-１２月別
                    row["PHN_ALL_HAIKI_KBN_TOTAL_VLB"] = this.upngroup2Footer[detail2[i].Seqindex].G2fhaishutujigyoshaalltotal2;            // 積替用合計-合計
                    // 2-3

                    // 2-4 
                    // 帳票フォームに設定
                    row["PHN_MONTH1_TOTAL_VLB"] = this.upngroup1Footer[0].G2fjanalltotal;                    // 総合計-１月別
                    row["PHN_MONTH2_TOTAL_VLB"] = this.upngroup1Footer[0].G2ffeballtotal;                    // 総合計-２月別
                    row["PHN_MONTH3_TOTAL_VLB"] = this.upngroup1Footer[0].G2fmaralltotal;                    // 総合計-３月別
                    row["PHN_MONTH4_TOTAL_VLB"] = this.upngroup1Footer[0].G2fapralltotal;                    // 総合計-４月別
                    row["PHN_MONTH5_TOTAL_VLB"] = this.upngroup1Footer[0].G2fmayalltotal;                    // 総合計-５月別
                    row["PHN_MONTH6_TOTAL_VLB"] = this.upngroup1Footer[0].G2fjunalltotal;                    // 総合計-６月別
                    row["PHN_MONTH7_TOTAL_VLB"] = this.upngroup1Footer[0].G2fjlyalltotal;                    // 総合計-７月別
                    row["PHN_MONTH8_TOTAL_VLB"] = this.upngroup1Footer[0].G2faugalltotal;                    // 総合計-８月別
                    row["PHN_MONTH9_TOTAL_VLB"] = this.upngroup1Footer[0].G2fsepalltotal;                    // 総合計-９月別
                    row["PHN_MONTH10_TOTAL_VLB"] = this.upngroup1Footer[0].G2foctalltotal;                   // 総合計-１０月別
                    row["PHN_MONTH11_TOTAL_VLB"] = this.upngroup1Footer[0].G2fnovalltotal;                   // 総合計-１１月別
                    row["PHN_MONTH12_TOTAL_VLB"] = this.upngroup1Footer[0].G2fdecalltotal;                   // 総合計-１２月別
                    row["PHN_TOTAL_VLB"] = this.upngroup1Footer[0].G2fentirealltotal;                        // 総合計-合計
                    // 2-4 
                    this.chouhyouDataTable.Rows.Add(row);
                }
            }
            // 明細部分の列定義(処分受託者別)
            else if (this.layoutno == "3")
            {
                // 処分受託者別用の列定義
                this.OutputFormLayout = "LAYOUT1";

                DataRow row;

                this.chouhyouDataTable.Columns.Add("PHY_MANIFEST_VLB");                     // 一次二次区分
                this.chouhyouDataTable.Columns.Add("PHN_JIGYOSHA_CODE_VLB");                // 排出事業者CD
                this.chouhyouDataTable.Columns.Add("PHN_JIGYOSHA_NAME_VLB");                // 排出事業者名

                this.chouhyouDataTable.Columns.Add("PHY_JIGYOJO_CODE_FLB");                 // 排出事業場CD
                this.chouhyouDataTable.Columns.Add("PHY_JIGYOJYO_NAME_FLB");                // 排出事業場名
                this.chouhyouDataTable.Columns.Add("PHY_HAIKI_KIND_FLB");                   // 廃棄物種類
                this.chouhyouDataTable.Columns.Add("PHY_UNIT_FLB");                         // 単位
                
                this.chouhyouDataTable.Columns.Add("PHY_MONTH1_FLB");                   // 廃棄物種類-１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH2_FLB");                   // 廃棄物種類-２月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH3_FLB");                   // 廃棄物種類-３月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH4_FLB");                   // 廃棄物種類-４月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH5_FLB");                   // 廃棄物種類-５月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH6_FLB");                   // 廃棄物種類-６月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH7_FLB");                   // 廃棄物種類-７月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH8_FLB");                   // 廃棄物種類-８月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH9_FLB");                   // 廃棄物種類-９月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH10_FLB");                  // 廃棄物種類-１０月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH11_FLB");                  // 廃棄物種類-１１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH12_FLB");                  // 廃棄物種類-１２月別  
                this.chouhyouDataTable.Columns.Add("PHY_ALL_FLB");                      // 廃棄物種類-合計

                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_JIGYOTOTAL_VLB");        // 排出事業者計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_JIGYOTOTAL_VLB");        // 排出事業者計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_JIGYOTOTAL_VLB");        // 排出事業者計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_JIGYOTOTAL_VLB");        // 排出事業者計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_JIGYOTOTAL_VLB");        // 排出事業者計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_JIGYOTOTAL_VLB");        // 排出事業者計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_JIGYOTOTAL_VLB");        // 排出事業者計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_JIGYOTOTAL_VLB");        // 排出事業者計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_JIGYOTOTAL_VLB");        // 排出事業者計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_JIGYOTOTAL_VLB");       // 排出事業者計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_JIGYOTOTAL_VLB");       // 排出事業者計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_JIGYOTOTAL_VLB");       // 排出事業者計-１２月別        
                this.chouhyouDataTable.Columns.Add("PHN_JIGYOTOTAL_VLB");               // 排出事業者計-合計

                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_TOTAL_VLB");             // 総合計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_TOTAL_VLB");             // 総合計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_TOTAL_VLB");             // 総合計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_TOTAL_VLB");             // 総合計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_TOTAL_VLB");             // 総合計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_TOTAL_VLB");             // 総合計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_TOTAL_VLB");             // 総合計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_TOTAL_VLB");             // 総合計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_TOTAL_VLB");             // 総合計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_TOTAL_VLB");            // 総合計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_TOTAL_VLB");            // 総合計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_TOTAL_VLB");            // 総合計-１２月別
                this.chouhyouDataTable.Columns.Add("PHN_TOTAL_VLB");                    // 総合計-合計

                // データテーブルと同じの構造をもったレコードを作成
                row = this.chouhyouDataTable.NewRow();
                // 帳票フォームに設定
                row["PHY_MANIFEST_VLB"] = this.firstsecoundkbn;                         // 一次二次区分

                row["PHY_MONTH1_FLB"] = this.janmonthkbn;                               // １月カラム
                row["PHY_MONTH2_FLB"] = this.febmonthkbn;                               // ２月カラム
                row["PHY_MONTH3_FLB"] = this.marmonthkbn;                               // ３月カラム
                row["PHY_MONTH4_FLB"] = this.aprmonthkbn;                               // ４月カラム
                row["PHY_MONTH5_FLB"] = this.maymonthkbn;                               // ５月カラム
                row["PHY_MONTH6_FLB"] = this.junmonthkbn;                               // ６月カラム
                row["PHY_MONTH7_FLB"] = this.julymonthkbn;                              // ７月カラム
                row["PHY_MONTH8_FLB"] = this.augmonthkbn;                               // ８月カラム
                row["PHY_MONTH9_FLB"] = this.sepmonthkbn;                               // ９月カラム
                row["PHY_MONTH10_FLB"] = this.octmonthkbn;                              // １０月カラム
                row["PHY_MONTH11_FLB"] = this.novmonthkbn;                              // １１月カラム
                row["PHY_MONTH12_FLB"] = this.decmonthkbn;                              // １２月カラム
                row["PHY_ALL_FLB"] = this.janfromdecgokeitotalkbn;                      // 合計カラム

                for (int i = 0; i < this.detail.Count; i++)
                {
                    // データテーブルと同じの構造をもったレコードを作成
                    row = this.chouhyouDataTable.NewRow();

                    row["PHN_JIGYOSHA_CODE_VLB"] = this.group2Hedder[detail[i].Seq].G2haishutujigyoushacd;              // 排出事業者CD
                    row["PHN_JIGYOSHA_NAME_VLB"] = this.group2Hedder[detail[i].Seq].G2haishutujigyoushaname;

                    // 帳票フォームに設定
                    row["PHY_JIGYOJO_CODE_FLB"] = this.detail[i].Haishutujigyoujocd;                                    // 排出事業場CD
                    row["PHY_JIGYOJYO_NAME_FLB"] = this.detail[i].Haishutujigyoujoname;                                 // 排出事業場名
                    row["PHY_HAIKI_KIND_FLB"] = this.detail[i].Haikishurui;                                             // 廃棄物種類
                    row["PHY_UNIT_FLB"] = this.detail[i].Tani;                                                          // 単位

                    row["PHY_MONTH1_FLB"] = this.detail[i].Shuruijanuary;                       // 廃棄物種類-１月別
                    row["PHY_MONTH2_FLB"] = this.detail[i].Shuruifebruary;                      // 廃棄物種類-２月別
                    row["PHY_MONTH3_FLB"] = this.detail[i].Shuruimarch;                         // 廃棄物種類-３月別
                    row["PHY_MONTH4_FLB"] = this.detail[i].Shuruiapril;                         // 廃棄物種類-４月別
                    row["PHY_MONTH5_FLB"] = this.detail[i].Shuruimay;                           // 廃棄物種類-５月別
                    row["PHY_MONTH6_FLB"] = this.detail[i].Shuruijune;                          // 廃棄物種類-６月別
                    row["PHY_MONTH7_FLB"] = this.detail[i].Shuruijuly;                          // 廃棄物種類-７月別
                    row["PHY_MONTH8_FLB"] = this.detail[i].Shuruiaugust;                        // 廃棄物種類-８月別
                    row["PHY_MONTH9_FLB"] = this.detail[i].Shuruiseptember;                     // 廃棄物種類-９月別
                    row["PHY_MONTH10_FLB"] = this.detail[i].Shuruioctober;                      // 廃棄物種類-１０月別
                    row["PHY_MONTH11_FLB"] = this.detail[i].Shuruinovember;                     // 廃棄物種類-１１月別
                    row["PHY_MONTH12_FLB"] = this.detail[i].Shuruidecember;                     // 廃棄物種類-１２月別
                    row["PHY_ALL_FLB"] = this.detail[i].Allshuruitotal;                         // 廃棄物種類-合計

                    // 帳票フォーム設定
                    row["PHN_MONTH1_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fjanjigyoshatotal;            // 排出事業者計-１月別
                    row["PHN_MONTH2_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2ffebjigyoshatotal;            // 排出事業者計-２月別
                    row["PHN_MONTH3_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fmarjigyoshatotal;            // 排出事業者計-３月別
                    row["PHN_MONTH4_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2faprjigyoshatotal;            // 排出事業者計-４月別
                    row["PHN_MONTH5_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fmayjigyoshatotal;            // 排出事業者計-５月別
                    row["PHN_MONTH6_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fjunjigyoshatotal;            // 排出事業者計-６月別
                    row["PHN_MONTH7_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fjlyjigyoshatotal;            // 排出事業者計-７月別
                    row["PHN_MONTH8_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2faugjigyoshatotal;            // 排出事業者計-８月別
                    row["PHN_MONTH9_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fsepjigyoshatotal;            // 排出事業者計-９月別
                    row["PHN_MONTH10_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2foctjigyoshatotal;           // 排出事業者計-１０月別
                    row["PHN_MONTH11_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fnovjigyoshatotal;           // 排出事業者計-１１月別
                    row["PHN_MONTH12_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fdecjigyoshatotal;           // 排出事業者計-１２月別        
                    row["PHN_JIGYOTOTAL_VLB"] = this.group2Footer[detail[i].Seq].G2fhaishutujigyoshaalltotal;           // 排出事業者計-合計

                    // 帳票フォームに設定
                    row["PHN_MONTH1_TOTAL_VLB"] = this.group1Footer[0].G2fjanalltotal;           // 総合計-１月別
                    row["PHN_MONTH2_TOTAL_VLB"] = this.group1Footer[0].G2ffeballtotal;           // 総合計-２月別
                    row["PHN_MONTH3_TOTAL_VLB"] = this.group1Footer[0].G2fmaralltotal;           // 総合計-３月別
                    row["PHN_MONTH4_TOTAL_VLB"] = this.group1Footer[0].G2fapralltotal;           // 総合計-４月別
                    row["PHN_MONTH5_TOTAL_VLB"] = this.group1Footer[0].G2fmayalltotal;           // 総合計-５月別
                    row["PHN_MONTH6_TOTAL_VLB"] = this.group1Footer[0].G2fjunalltotal;           // 総合計-６月別
                    row["PHN_MONTH7_TOTAL_VLB"] = this.group1Footer[0].G2fjlyalltotal;           // 総合計-７月別
                    row["PHN_MONTH8_TOTAL_VLB"] = this.group1Footer[0].G2faugalltotal;           // 総合計-８月別
                    row["PHN_MONTH9_TOTAL_VLB"] = this.group1Footer[0].G2fsepalltotal;           // 総合計-９月別
                    row["PHN_MONTH10_TOTAL_VLB"] = this.group1Footer[0].G2foctalltotal;          // 総合計-１０月別
                    row["PHN_MONTH11_TOTAL_VLB"] = this.group1Footer[0].G2fnovalltotal;          // 総合計-１１月別
                    row["PHN_MONTH12_TOTAL_VLB"] = this.group1Footer[0].G2fdecalltotal;          // 総合計-１２月別
                    row["PHN_TOTAL_VLB"] = this.group1Footer[0].G2fentirealltotal;               // 総合計-合計

                    this.chouhyouDataTable.Rows.Add(row);
                }
            }
            // 明細部分の列定義(最終処分場所別)
            if (this.layoutno == "4")
            {
                // マニフェスト用の列定義
                this.OutputFormLayout = "LAYOUT2";

                DataRow row;

                this.chouhyouDataTable.Columns.Add("PHY_ICHIJI_MANIFEST_FLB");          // 一次二次区分

                this.chouhyouDataTable.Columns.Add("PHY_CODE_FLB");                     // 最終処分場所CD
                this.chouhyouDataTable.Columns.Add("PHY_JIGYOJYO_NAME_FLB");            // 最終処分場所
                this.chouhyouDataTable.Columns.Add("PHY_HAIKI_KIND_FLB");               // 廃棄物種類
                this.chouhyouDataTable.Columns.Add("PHY_UNIT_FLB");                     // 単位

                // Group2_Header
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                this.chouhyouDataTable.Columns.Add("PHN_LAST_SBN_GYOSHA_CD_VLB");        // 最終処分業者CD
                this.chouhyouDataTable.Columns.Add("PHN_LAST_SBN_GYOSHA_NAME_VLB");      // 最終処分業者名
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                this.chouhyouDataTable.Columns.Add("PHN_LAST_SBN_GENBA_CD_VLB");        // 最終処分場CD
                this.chouhyouDataTable.Columns.Add("PHN_LAST_SBN_GENBA_NAME_VLB");      // 最終処分場名
               
                this.chouhyouDataTable.Columns.Add("PHY_MONTH1_FLB");                   // 廃棄物種類-１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH2_FLB");                   // 廃棄物種類-２月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH3_FLB");                   // 廃棄物種類-３月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH4_FLB");                   // 廃棄物種類-４月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH5_FLB");                   // 廃棄物種類-５月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH6_FLB");                   // 廃棄物種類-６月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH7_FLB");                   // 廃棄物種類-７月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH8_FLB");                   // 廃棄物種類-８月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH9_FLB");                   // 廃棄物種類-９月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH10_FLB");                  // 廃棄物種類-１０月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH11_FLB");                  // 廃棄物種類-１１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH12_FLB");                  // 廃棄物種類-１２月別  
                this.chouhyouDataTable.Columns.Add("PHY_ALL_FLB");                      // 廃棄物種類-合計

                // Group2_Footer
                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_LASTGENBATOTAL_VLB");    // 最終処分場計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_LASTGENBATOTAL_VLB");    // 最終処分場計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_LASTGENBATOTAL_VLB");    // 最終処分場計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_LASTGENBATOTAL_VLB");    // 最終処分場計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_LASTGENBATOTAL_VLB");    // 最終処分場計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_LASTGENBATOTAL_VLB");    // 最終処分場計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_LASTGENBATOTAL_VLB");    // 最終処分場計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_LASTGENBATOTAL_VLB");    // 最終処分場計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_LASTGENBATOTAL_VLB");    // 最終処分場計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_LASTGENBATOTAL_VLB");   // 最終処分場計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_LASTGENBATOTAL_VLB");   // 最終処分場計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_LASTGENBATOTAL_VLB");   // 最終処分場計-１２月別
                this.chouhyouDataTable.Columns.Add("PHN_LASTGENBATOTAL_VLB");           // 最終処分場計-合計

                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_TOTAL_VLB");             // 総合計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_TOTAL_VLB");             // 総合計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_TOTAL_VLB");             // 総合計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_TOTAL_VLB");             // 総合計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_TOTAL_VLB");             // 総合計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_TOTAL_VLB");             // 総合計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_TOTAL_VLB");             // 総合計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_TOTAL_VLB");             // 総合計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_TOTAL_VLB");             // 総合計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_TOTAL_VLB");            // 総合計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_TOTAL_VLB");            // 総合計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_TOTAL_VLB");            // 総合計-１２月別
                this.chouhyouDataTable.Columns.Add("PHN_TOTAL_VLB");                    // 総合計-合計

                // データテーブルと同じの構造をもったレコードを作成
                row = this.chouhyouDataTable.NewRow();
                // 帳票フォームに設定
                row["PHY_ICHIJI_MANIFEST_FLB"] = firstsecoundkbn;                       // 一次二次区分

                row["PHY_MONTH1_FLB"] = this.janmonthkbn;                               // １月カラム
                row["PHY_MONTH2_FLB"] = this.febmonthkbn;                               // ２月カラム
                row["PHY_MONTH3_FLB"] = this.marmonthkbn;                               // ３月カラム
                row["PHY_MONTH4_FLB"] = this.aprmonthkbn;                               // ４月カラム
                row["PHY_MONTH5_FLB"] = this.maymonthkbn;                               // ５月カラム
                row["PHY_MONTH6_FLB"] = this.junmonthkbn;                               // ６月カラム
                row["PHY_MONTH7_FLB"] = this.julymonthkbn;                              // ７月カラム
                row["PHY_MONTH8_FLB"] = this.augmonthkbn;                               // ８月カラム
                row["PHY_MONTH9_FLB"] = this.sepmonthkbn;                               // ９月カラム
                row["PHY_MONTH10_FLB"] = this.octmonthkbn;                              // １０月カラム
                row["PHY_MONTH11_FLB"] = this.novmonthkbn;                              // １１月カラム
                row["PHY_MONTH12_FLB"] = this.decmonthkbn;                              // １２月カラム
                row["PHY_ALL_FLB"] = this.janfromdecgokeitotalkbn;                      // 合計カラム

                for (int i = 0; i < this.detail.Count; i++)
                {
                        // データテーブルと同じの構造をもったレコードを作成
                        row = this.chouhyouDataTable.NewRow();

                        // 帳票フォームに設定
                        row["PHY_CODE_FLB"] = this.detail[i].Haishutujigyoujocd;                                        // 最終処分場所CD
                        row["PHY_JIGYOJYO_NAME_FLB"] = this.detail[i].Haishutujigyoujoname;                             // 最終処分場所名
                        row["PHY_HAIKI_KIND_FLB"] = this.detail[i].Haikishurui;                                         // 廃棄物種類
                        row["PHY_UNIT_FLB"] = this.detail[i].Tani;                                                      // 単位
                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                        row["PHN_LAST_SBN_GYOSHA_CD_VLB"] = this.lastgroup2Header[detail[i].Seq].Lastsbngyoshacd;         // 最終処分業者CD
                        row["PHN_LAST_SBN_GYOSHA_NAME_VLB"] = this.lastgroup2Header[detail[i].Seq].Lastsbngyoshaname;     // 最終処分業者名
                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                        row["PHN_LAST_SBN_GENBA_CD_VLB"] = this.lastgroup2Header[detail[i].Seq].Lastsbngenbacd;         // 最終処分場CD
                        row["PHN_LAST_SBN_GENBA_NAME_VLB"] = this.lastgroup2Header[detail[i].Seq].Lastsbngenbaname;     // 最終処分場名

                        row["PHY_MONTH1_FLB"] = this.detail[i].Shuruijanuary;                                           // 廃棄物種類-１月別
                        row["PHY_MONTH2_FLB"] = this.detail[i].Shuruifebruary;                                          // 廃棄物種類-２月別
                        row["PHY_MONTH3_FLB"] = this.detail[i].Shuruimarch;                                             // 廃棄物種類-３月別
                        row["PHY_MONTH4_FLB"] = this.detail[i].Shuruiapril;                                             // 廃棄物種類-４月別
                        row["PHY_MONTH5_FLB"] = this.detail[i].Shuruimay;                                               // 廃棄物種類-５月別
                        row["PHY_MONTH6_FLB"] = this.detail[i].Shuruijune;                                              // 廃棄物種類-６月別
                        row["PHY_MONTH7_FLB"] = this.detail[i].Shuruijuly;                                              // 廃棄物種類-７月別
                        row["PHY_MONTH8_FLB"] = this.detail[i].Shuruiaugust;                                            // 廃棄物種類-８月別
                        row["PHY_MONTH9_FLB"] = this.detail[i].Shuruiseptember;                                         // 廃棄物種類-９月別
                        row["PHY_MONTH10_FLB"] = this.detail[i].Shuruioctober;                                          // 廃棄物種類-１０月別
                        row["PHY_MONTH11_FLB"] = this.detail[i].Shuruinovember;                                         // 廃棄物種類-１１月別
                        row["PHY_MONTH12_FLB"] = this.detail[i].Shuruidecember;                                         // 廃棄物種類-１２月別
                        row["PHY_ALL_FLB"] = this.detail[i].Allshuruitotal;                                             // 廃棄物種類-合計

                        row["PHN_MONTH1_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth1total;                    // 最終処分場計-１月別
                        row["PHN_MONTH2_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth2total;                    // 最終処分場計-２月別
                        row["PHN_MONTH3_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth3total;                    // 最終処分場計-３月別
                        row["PHN_MONTH4_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth4total;                    // 最終処分場計-４月別
                        row["PHN_MONTH5_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth5total;                    // 最終処分場計-５月別
                        row["PHN_MONTH6_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth6total;                    // 最終処分場計-６月別
                        row["PHN_MONTH7_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth7total;                    // 最終処分場計-７月別
                        row["PHN_MONTH8_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth8total;                    // 最終処分場計-８月別
                        row["PHN_MONTH9_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth9total;                    // 最終処分場計-９月別
                        row["PHN_MONTH10_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth10total;                  // 最終処分場計-１０月別
                        row["PHN_MONTH11_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth11total;                  // 最終処分場計-１１月別
                        row["PHN_MONTH12_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbamanth12total;                  // 最終処分場計-１２月別
                        row["PHN_LASTGENBATOTAL_VLB"] = this.lastgroup2Footer[detail[i].Seq].Lastgenbasbntotal;                              // 最終処分場計-合計

                        // 帳票フォームに設定
                        row["PHN_MONTH1_TOTAL_VLB"] = this.group1Footer[0].G2fjanalltotal;                    // 総合計-１月別
                        row["PHN_MONTH2_TOTAL_VLB"] = this.group1Footer[0].G2ffeballtotal;                    // 総合計-２月別
                        row["PHN_MONTH3_TOTAL_VLB"] = this.group1Footer[0].G2fmaralltotal;                    // 総合計-３月別
                        row["PHN_MONTH4_TOTAL_VLB"] = this.group1Footer[0].G2fapralltotal;                    // 総合計-４月別
                        row["PHN_MONTH5_TOTAL_VLB"] = this.group1Footer[0].G2fmayalltotal;                    // 総合計-５月別
                        row["PHN_MONTH6_TOTAL_VLB"] = this.group1Footer[0].G2fjunalltotal;                    // 総合計-６月別
                        row["PHN_MONTH7_TOTAL_VLB"] = this.group1Footer[0].G2fjlyalltotal;                    // 総合計-７月別
                        row["PHN_MONTH8_TOTAL_VLB"] = this.group1Footer[0].G2faugalltotal;                    // 総合計-８月別
                        row["PHN_MONTH9_TOTAL_VLB"] = this.group1Footer[0].G2fsepalltotal;                    // 総合計-９月別
                        row["PHN_MONTH10_TOTAL_VLB"] = this.group1Footer[0].G2foctalltotal;                   // 総合計-１０月別
                        row["PHN_MONTH11_TOTAL_VLB"] = this.group1Footer[0].G2fnovalltotal;                   // 総合計-１１月別
                        row["PHN_MONTH12_TOTAL_VLB"] = this.group1Footer[0].G2fdecalltotal;                   // 総合計-１２月別
                        row["PHN_TOTAL_VLB"] = this.group1Footer[0].G2fentirealltotal;                        // 総合計-合計

                        this.chouhyouDataTable.Rows.Add(row);
                    }
            }
            // 明細部分の列定義(廃棄物種類別)
            else if (this.layoutno == "5")
            {
                // 処分受託者別用の列定義
                this.OutputFormLayout = "LAYOUT2";

                DataRow row;
                // Detail
                this.chouhyouDataTable.Columns.Add("PHY_ICHIJI_MANIFEST_FLB");          // 一次二次区分
                this.chouhyouDataTable.Columns.Add("PHY_CODE_FLB");                     // 廃棄物種類CD
                this.chouhyouDataTable.Columns.Add("PHY_HAIKI_KIND_FLB");               // 廃棄物種類
                this.chouhyouDataTable.Columns.Add("PHY_UNIT_FLB");                     // 単位
                this.chouhyouDataTable.Columns.Add("PHY_MONTH1_FLB");                   // 廃棄物種類-１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH2_FLB");                   // 廃棄物種類-２月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH3_FLB");                   // 廃棄物種類-３月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH4_FLB");                   // 廃棄物種類-４月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH5_FLB");                   // 廃棄物種類-５月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH6_FLB");                   // 廃棄物種類-６月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH7_FLB");                   // 廃棄物種類-７月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH8_FLB");                   // 廃棄物種類-８月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH9_FLB");                   // 廃棄物種類-９月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH10_FLB");                  // 廃棄物種類-１０月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH11_FLB");                  // 廃棄物種類-１１月別
                this.chouhyouDataTable.Columns.Add("PHY_MONTH12_FLB");                  // 廃棄物種類-１２月別  
                this.chouhyouDataTable.Columns.Add("PHY_ALL_FLB");                      // 廃棄物種類-合計
                // Detail


                // Group1_Footer
                this.chouhyouDataTable.Columns.Add("PHN_MONTH1_TOTAL_VLB");             // 総合計-１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH2_TOTAL_VLB");             // 総合計-２月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH3_TOTAL_VLB");             // 総合計-３月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH4_TOTAL_VLB");             // 総合計-４月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH5_TOTAL_VLB");             // 総合計-５月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH6_TOTAL_VLB");             // 総合計-６月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH7_TOTAL_VLB");             // 総合計-７月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH8_TOTAL_VLB");             // 総合計-８月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH9_TOTAL_VLB");             // 総合計-９月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH10_TOTAL_VLB");            // 総合計-１０月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH11_TOTAL_VLB");            // 総合計-１１月別
                this.chouhyouDataTable.Columns.Add("PHN_MONTH12_TOTAL_VLB");            // 総合計-１２月別
                this.chouhyouDataTable.Columns.Add("PHN_TOTAL_VLB");                    // 総合計-合計
                // Group1_Footer

                // データテーブルと同じの構造をもったレコードを作成
                row = this.chouhyouDataTable.NewRow();
                // 帳票フォームに設定
                row["PHY_ICHIJI_MANIFEST_FLB"] = this.firstsecoundkbn;                  // 一次二次区分
                row["PHY_MONTH1_FLB"] = this.janmonthkbn;                               // １月カラム
                row["PHY_MONTH2_FLB"] = this.febmonthkbn;                               // ２月カラム
                row["PHY_MONTH3_FLB"] = this.marmonthkbn;                               // ３月カラム
                row["PHY_MONTH4_FLB"] = this.aprmonthkbn;                               // ４月カラム
                row["PHY_MONTH5_FLB"] = this.maymonthkbn;                               // ５月カラム
                row["PHY_MONTH6_FLB"] = this.junmonthkbn;                               // ６月カラム
                row["PHY_MONTH7_FLB"] = this.julymonthkbn;                              // ７月カラム
                row["PHY_MONTH8_FLB"] = this.augmonthkbn;                               // ８月カラム
                row["PHY_MONTH9_FLB"] = this.sepmonthkbn;                               // ９月カラム
                row["PHY_MONTH10_FLB"] = this.octmonthkbn;                              // １０月カラム
                row["PHY_MONTH11_FLB"] = this.novmonthkbn;                              // １１月カラム
                row["PHY_MONTH12_FLB"] = this.decmonthkbn;                              // １２月カラム
                row["PHY_ALL_FLB"] = this.janfromdecgokeitotalkbn;                      // 合計カラム

                for (int i = 0; i < this.detail.Count; i++)
                {
                    // データテーブルと同じの構造をもったレコードを作成
                    row = this.chouhyouDataTable.NewRow();

                    // 帳票フォームに設定
                    row["PHY_CODE_FLB"] = this.detail[i].Haishutujigyoujocd;                // 廃棄物種類CD
                    row["PHY_HAIKI_KIND_FLB"] = this.detail[i].Haishutujigyoujoname;        // 廃棄物種類
                    row["PHY_UNIT_FLB"] = this.detail[i].Haikishurui;                       // 単位

                    row["PHY_MONTH1_FLB"] = this.detail[i].Shuruijanuary;                   // 廃棄物種類-１月別
                    row["PHY_MONTH2_FLB"] = this.detail[i].Shuruifebruary;                  // 廃棄物種類-２月別
                    row["PHY_MONTH3_FLB"] = this.detail[i].Shuruimarch;                     // 廃棄物種類-３月別
                    row["PHY_MONTH4_FLB"] = this.detail[i].Shuruiapril;                     // 廃棄物種類-４月別
                    row["PHY_MONTH5_FLB"] = this.detail[i].Shuruimay;                       // 廃棄物種類-５月別
                    row["PHY_MONTH6_FLB"] = this.detail[i].Shuruijune;                      // 廃棄物種類-６月別
                    row["PHY_MONTH7_FLB"] = this.detail[i].Shuruijuly;                      // 廃棄物種類-７月別
                    row["PHY_MONTH8_FLB"] = this.detail[i].Shuruiaugust;                    // 廃棄物種類-８月別
                    row["PHY_MONTH9_FLB"] = this.detail[i].Shuruiseptember;                 // 廃棄物種類-９月別
                    row["PHY_MONTH10_FLB"] = this.detail[i].Shuruioctober;                  // 廃棄物種類-１０月別
                    row["PHY_MONTH11_FLB"] = this.detail[i].Shuruinovember;                 // 廃棄物種類-１１月別
                    row["PHY_MONTH12_FLB"] = this.detail[i].Shuruidecember;                 // 廃棄物種類-１２月別
                    row["PHY_ALL_FLB"] = this.detail[i].Allshuruitotal;                     // 廃棄物種類-合計



                    // 帳票フォームに設定
                    row["PHN_MONTH1_TOTAL_VLB"] = this.group1Footer[0].G2fjanalltotal;      // 総合計-１月別
                    row["PHN_MONTH2_TOTAL_VLB"] = this.group1Footer[0].G2ffeballtotal;      // 総合計-２月別
                    row["PHN_MONTH3_TOTAL_VLB"] = this.group1Footer[0].G2fmaralltotal;      // 総合計-３月別
                    row["PHN_MONTH4_TOTAL_VLB"] = this.group1Footer[0].G2fapralltotal;      // 総合計-４月別
                    row["PHN_MONTH5_TOTAL_VLB"] = this.group1Footer[0].G2fmayalltotal;      // 総合計-５月別
                    row["PHN_MONTH6_TOTAL_VLB"] = this.group1Footer[0].G2fjunalltotal;      // 総合計-６月別
                    row["PHN_MONTH7_TOTAL_VLB"] = this.group1Footer[0].G2fjlyalltotal;      // 総合計-７月別
                    row["PHN_MONTH8_TOTAL_VLB"] = this.group1Footer[0].G2faugalltotal;      // 総合計-８月別
                    row["PHN_MONTH9_TOTAL_VLB"] = this.group1Footer[0].G2fsepalltotal;      // 総合計-９月別
                    row["PHN_MONTH10_TOTAL_VLB"] = this.group1Footer[0].G2foctalltotal;     // 総合計-１０月別
                    row["PHN_MONTH11_TOTAL_VLB"] = this.group1Footer[0].G2fnovalltotal;     // 総合計-１１月別
                    row["PHN_MONTH12_TOTAL_VLB"] = this.group1Footer[0].G2fdecalltotal;     // 総合計-１２月別
                    row["PHN_TOTAL_VLB"] = this.group1Footer[0].G2fentirealltotal;          // 総合計-合計

                    this.chouhyouDataTable.Rows.Add(row);
                }
            }

            // データテーブル作成処理のみ
            this.SetRecord(this.chouhyouDataTable);
            /// <summary>データテーブル情報から帳票情報作成処理を実行する</summary>
            /// 引数１：XMLレポート定義ファイルの完全名
            /// 引数２：string reportName
            /// 引数３：画面から受け継いだdataTable
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dataTableChouhyouForm);
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            // 固定項目の列定義(排出事業者別)
            if (this.layoutno == "1")
            {
                // ページヘッダー部
                //this.SetFieldName("FH_TITLE_FLB", "マニフェスト明細表");              // タイトル
                this.SetFieldName("PHY_MANIFEST_VLB", this.firstsecoundkbn);            // マニフェスト区分
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");    // 発行日時
                this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime() + " " + "発行");    // 発行日時
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", corp);                      // 会社名  

                this.SetFieldName("PHY_MONTH1_FLB", this.janmonthkbn);                  // １月カラム
                this.SetFieldName("PHY_MONTH2_FLB", this.febmonthkbn);                  // ２月カラム
                this.SetFieldName("PHY_MONTH3_FLB", this.marmonthkbn);                  // ３月カラム
                this.SetFieldName("PHY_MONTH4_FLB", this.aprmonthkbn);                  // ４月カラム
                this.SetFieldName("PHY_MONTH5_FLB", this.maymonthkbn);                  // ５月カラム
                this.SetFieldName("PHY_MONTH6_FLB", this.junmonthkbn);                  // ６月カラム
                this.SetFieldName("PHY_MONTH7_FLB", this.julymonthkbn);                 // ７月カラム
                this.SetFieldName("PHY_MONTH8_FLB", this.augmonthkbn);                  // ８月カラム
                this.SetFieldName("PHY_MONTH9_FLB", this.sepmonthkbn);                  // ９月カラム
                this.SetFieldName("PHY_MONTH10_FLB", this.octmonthkbn);                 // １０月カラム
                this.SetFieldName("PHY_MONTH11_FLB", this.novmonthkbn);                 // １１月カラム
                this.SetFieldName("PHY_MONTH12_FLB", this.decmonthkbn);                 // １２月カラム
                this.SetFieldName("PHY_ALL_FLB", "合計");                               // 合計カラム


                this.SetFieldName("PHN_MONTH1_TOTAL_VLB", this.g2fjanalltotal);         // 総合計-１月別
                this.SetFieldName("PHN_MONTH2_TOTAL_VLB", this.g2ffeballtotal);         // 総合計-２月別
                this.SetFieldName("PHN_MONTH3_TOTAL_VLB", this.g2fmaralltotal);         // 総合計-３月別
                this.SetFieldName("PHN_MONTH4_TOTAL_VLB", this.g2fapralltotal);         // 総合計-４月別
                this.SetFieldName("PHN_MONTH5_TOTAL_VLB", this.g2fmayalltotal);         // 総合計-５月別
                this.SetFieldName("PHN_MONTH6_TOTAL_VLB", this.g2fjunalltotal);         // 総合計-６月別
                this.SetFieldName("PHN_MONTH7_TOTAL_VLB", this.g2fjlyalltotal);         // 総合計-７月別
                this.SetFieldName("PHN_MONTH8_TOTAL_VLB", this.g2faugalltotal);         // 総合計-８月別
                this.SetFieldName("PHN_MONTH9_TOTAL_VLB", this.g2fsepalltotal);         // 総合計-９月別
                this.SetFieldName("PHN_MONTH10_TOTAL_VLB", this.g2foctalltotal);        // 総合計-１０月別
                this.SetFieldName("PHN_MONTH11_TOTAL_VLB", this.g2fnovalltotal);        // 総合計-１１月別
                this.SetFieldName("PHN_MONTH12_TOTAL_VLB", this.g2fdecalltotal);        // 総合計-１２月別
                this.SetFieldName("PHN_TOTAL_VLB", this.g2fentirealltotal);             // 総合計-合計


                this.SetFieldName("PHY_HAISYUTSU_JIGYOSHA_FLB", "排出事業者");
                this.SetFieldName("PHY_JIGYOJO_CODE_FLB", "CD");
                this.SetFieldName("PHY_JIGYOJYO_NAME_FLB", "排出事業場");
                this.SetFieldName("PHY_HAIKI_KIND_FLB", "廃棄物種類");
                this.SetFieldName("PHY_UNIT_FLB", "単位");
                this.SetFieldName("G2F_TOTAL_FLB", "排出事業者計");
                this.SetFieldName("G1F_TOTAL_FLB", "総合計");
            }
            // 固定項目の列定義(運搬受託者別)
            if (this.layoutno == "2")
            {
                // ページヘッダー部
                this.SetFieldName("FH_TITLE_VLB", "マニフェスト推移表(運搬受託者別)");  // タイトル
                this.SetFieldName("PHY_MANIFEST_VLB", this.firstsecoundkbn);            // マニフェスト区分
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");    // 発行日時
                this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime() + " " + "発行");    // 発行日時
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", corp);                      // 会社名  

                this.SetFieldName("PHY_MONTH1_FLB", this.janmonthkbn);                  // １月カラム
                this.SetFieldName("PHY_MONTH2_FLB", this.febmonthkbn);                  // ２月カラム
                this.SetFieldName("PHY_MONTH3_FLB", this.marmonthkbn);                  // ３月カラム
                this.SetFieldName("PHY_MONTH4_FLB", this.aprmonthkbn);                  // ４月カラム
                this.SetFieldName("PHY_MONTH5_FLB", this.maymonthkbn);                  // ５月カラム
                this.SetFieldName("PHY_MONTH6_FLB", this.junmonthkbn);                  // ６月カラム
                this.SetFieldName("PHY_MONTH7_FLB", this.julymonthkbn);                 // ７月カラム
                this.SetFieldName("PHY_MONTH8_FLB", this.augmonthkbn);                  // ８月カラム
                this.SetFieldName("PHY_MONTH9_FLB", this.sepmonthkbn);                  // ９月カラム
                this.SetFieldName("PHY_MONTH10_FLB", this.octmonthkbn);                 // １０月カラム
                this.SetFieldName("PHY_MONTH11_FLB", this.novmonthkbn);                 // １１月カラム
                this.SetFieldName("PHY_MONTH12_FLB", this.decmonthkbn);                 // １２月カラム
                this.SetFieldName("PHY_ALL_FLB", "合計");                               // 合計カラム


                this.SetFieldName("PHN_MONTH1_TOTAL_VLB", this.g2fjanalltotal);         // 総合計-１月別
                this.SetFieldName("PHN_MONTH2_TOTAL_VLB", this.g2ffeballtotal);         // 総合計-２月別
                this.SetFieldName("PHN_MONTH3_TOTAL_VLB", this.g2fmaralltotal);         // 総合計-３月別
                this.SetFieldName("PHN_MONTH4_TOTAL_VLB", this.g2fapralltotal);         // 総合計-４月別
                this.SetFieldName("PHN_MONTH5_TOTAL_VLB", this.g2fmayalltotal);         // 総合計-５月別
                this.SetFieldName("PHN_MONTH6_TOTAL_VLB", this.g2fjunalltotal);         // 総合計-６月別
                this.SetFieldName("PHN_MONTH7_TOTAL_VLB", this.g2fjlyalltotal);         // 総合計-７月別
                this.SetFieldName("PHN_MONTH8_TOTAL_VLB", this.g2faugalltotal);         // 総合計-８月別
                this.SetFieldName("PHN_MONTH9_TOTAL_VLB", this.g2fsepalltotal);         // 総合計-９月別
                this.SetFieldName("PHN_MONTH10_TOTAL_VLB", this.g2foctalltotal);        // 総合計-１０月別
                this.SetFieldName("PHN_MONTH11_TOTAL_VLB", this.g2fnovalltotal);        // 総合計-１１月別
                this.SetFieldName("PHN_MONTH12_TOTAL_VLB", this.g2fdecalltotal);        // 総合計-１２月別
                this.SetFieldName("PHN_TOTAL_VLB", this.g2fentirealltotal);             // 総合計-合計


                this.SetFieldName("PHY_UPN_GYOUSHA_CD_FLB", "CD");
                this.SetFieldName("PHY_UPN_GYOUSHA_NAME_FLB", "運搬受託者");
                this.SetFieldName("PHY_HAIKI_KIND_FLB", "廃棄物種類");
                this.SetFieldName("PHY_UNIT_FLB", "単位");
                this.SetFieldName("G1F_TOTAL_FLB", "総合計");
            }
            // 固定項目の列定義(処分受託者別)
            if (this.layoutno == "3")
            {
                // ページヘッダー部
                this.SetFieldName("FH_TITLE_FLB", "マニフェスト推移表（処分受託者別）");        // タイトル
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");            // 発行日時
                this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime() + " " + "発行");            // 発行日時
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", corp);                              // 会社名  
                this.SetFieldName("PHY_MANIFEST_VLB", this.firstsecoundkbn);                    // マニフェスト区分

                this.SetFieldName("PHY_MONTH1_FLB", this.janmonthkbn);          // １月カラム
                this.SetFieldName("PHY_MONTH2_FLB", this.febmonthkbn);          // ２月カラム
                this.SetFieldName("PHY_MONTH3_FLB", this.marmonthkbn);          // ３月カラム
                this.SetFieldName("PHY_MONTH4_FLB", this.aprmonthkbn);          // ４月カラム
                this.SetFieldName("PHY_MONTH5_FLB", this.maymonthkbn);          // ５月カラム
                this.SetFieldName("PHY_MONTH6_FLB", this.junmonthkbn);          // ６月カラム
                this.SetFieldName("PHY_MONTH7_FLB", this.julymonthkbn);         // ７月カラム
                this.SetFieldName("PHY_MONTH8_FLB", this.augmonthkbn);          // ８月カラム
                this.SetFieldName("PHY_MONTH9_FLB", this.sepmonthkbn);          // ９月カラム
                this.SetFieldName("PHY_MONTH10_FLB", this.octmonthkbn);         // １０月カラム
                this.SetFieldName("PHY_MONTH11_FLB", this.novmonthkbn);         // １１月カラム
                this.SetFieldName("PHY_MONTH12_FLB", this.decmonthkbn);         // １２月カラム
                this.SetFieldName("PHY_ALL_FLB", "合計");                       // 合計カラム


                this.SetFieldName("PHN_MONTH1_TOTAL_VLB", this.g2fjanalltotal);         // 総合計-１月別
                this.SetFieldName("PHN_MONTH2_TOTAL_VLB", this.g2ffeballtotal);         // 総合計-２月別
                this.SetFieldName("PHN_MONTH3_TOTAL_VLB", this.g2fmaralltotal);         // 総合計-３月別
                this.SetFieldName("PHN_MONTH4_TOTAL_VLB", this.g2fapralltotal);         // 総合計-４月別
                this.SetFieldName("PHN_MONTH5_TOTAL_VLB", this.g2fmayalltotal);         // 総合計-５月別
                this.SetFieldName("PHN_MONTH6_TOTAL_VLB", this.g2fjunalltotal);         // 総合計-６月別
                this.SetFieldName("PHN_MONTH7_TOTAL_VLB", this.g2fjlyalltotal);         // 総合計-７月別
                this.SetFieldName("PHN_MONTH8_TOTAL_VLB", this.g2faugalltotal);         // 総合計-８月別
                this.SetFieldName("PHN_MONTH9_TOTAL_VLB", this.g2fsepalltotal);         // 総合計-９月別
                this.SetFieldName("PHN_MONTH10_TOTAL_VLB", this.g2foctalltotal);        // 総合計-１０月別
                this.SetFieldName("PHN_MONTH11_TOTAL_VLB", this.g2fnovalltotal);        // 総合計-１１月別
                this.SetFieldName("PHN_MONTH12_TOTAL_VLB", this.g2fdecalltotal);        // 総合計-１２月別
                this.SetFieldName("PHN_TOTAL_VLB", this.g2fentirealltotal);             // 総合計-合計

                // ページヘッダー部固定項目
                this.SetFieldName("PHY_HAISYUTSU_JIGYOSHA_FLB", "処分受託者");
                this.SetFieldName("PHY_JIGYOJO_CODE_FLB", "CD");
                this.SetFieldName("PHY_JIGYOJYO_NAME_FLB", "処分事業場");
                this.SetFieldName("PHY_HAIKI_KIND_FLB", "廃棄物種類");
                this.SetFieldName("PHY_UNIT_FLB", "単位");
                this.SetFieldName("G2F_TOTAL_FLB", "処分受託者計");
                this.SetFieldName("G1F_TOTAL_FLB", "総合計");
            }
            // 固定項目の列定義(最終処分場所別)
            if (this.layoutno == "4")
            {
                // ページヘッダー部
                this.SetFieldName("FH_TITLE_VLB", "マニフェスト推移表（最終処分場所別）");  // タイトル            
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");        // 発行日時
                this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime() + " " + "発行");        // 発行日時
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", corp);                          // 会社名
                this.SetFieldName("PHY_ICHIJI_MANIFEST_FLB", this.firstsecoundkbn);         // マニフェスト区分

                this.SetFieldName("PHY_MONTH1_FLB", this.janmonthkbn);                      // １月カラム
                this.SetFieldName("PHY_MONTH2_FLB", this.febmonthkbn);                      // ２月カラム
                this.SetFieldName("PHY_MONTH3_FLB", this.marmonthkbn);                      // ３月カラム
                this.SetFieldName("PHY_MONTH4_FLB", this.aprmonthkbn);                      // ４月カラム
                this.SetFieldName("PHY_MONTH5_FLB", this.maymonthkbn);                      // ５月カラム
                this.SetFieldName("PHY_MONTH6_FLB", this.junmonthkbn);                      // ６月カラム
                this.SetFieldName("PHY_MONTH7_FLB", this.julymonthkbn);                     // ７月カラム
                this.SetFieldName("PHY_MONTH8_FLB", this.augmonthkbn);                      // ８月カラム
                this.SetFieldName("PHY_MONTH9_FLB", this.sepmonthkbn);                      // ９月カラム
                this.SetFieldName("PHY_MONTH10_FLB", this.octmonthkbn);                     // １０月カラム
                this.SetFieldName("PHY_MONTH11_FLB", this.novmonthkbn);                     // １１月カラム
                this.SetFieldName("PHY_MONTH12_FLB", this.decmonthkbn);                     // １２月カラム
                this.SetFieldName("PHY_ALL_FLB", "合計");                                   // 合計カラム

                this.SetFieldName("PHN_MONTH1_TOTAL_VLB", this.g2fjanalltotal);             // 総合計-１月別
                this.SetFieldName("PHN_MONTH2_TOTAL_VLB", this.g2ffeballtotal);             // 総合計-２月別
                this.SetFieldName("PHN_MONTH3_TOTAL_VLB", this.g2fmaralltotal);             // 総合計-３月別
                this.SetFieldName("PHN_MONTH4_TOTAL_VLB", this.g2fapralltotal);             // 総合計-４月別
                this.SetFieldName("PHN_MONTH5_TOTAL_VLB", this.g2fmayalltotal);             // 総合計-５月別
                this.SetFieldName("PHN_MONTH6_TOTAL_VLB", this.g2fjunalltotal);             // 総合計-６月別
                this.SetFieldName("PHN_MONTH7_TOTAL_VLB", this.g2fjlyalltotal);             // 総合計-７月別
                this.SetFieldName("PHN_MONTH8_TOTAL_VLB", this.g2faugalltotal);             // 総合計-８月別
                this.SetFieldName("PHN_MONTH9_TOTAL_VLB", this.g2fsepalltotal);             // 総合計-９月別
                this.SetFieldName("PHN_MONTH10_TOTAL_VLB", this.g2foctalltotal);            // 総合計-１０月別
                this.SetFieldName("PHN_MONTH11_TOTAL_VLB", this.g2fnovalltotal);            // 総合計-１１月別
                this.SetFieldName("PHN_MONTH12_TOTAL_VLB", this.g2fdecalltotal);            // 総合計-１２月別
                this.SetFieldName("PHN_TOTAL_VLB", this.g2fentirealltotal);                 // 総合計-合計

                this.SetFieldName("PHY_JIGYOJYO_NAME_FLB", "最終処分場所");
                this.SetFieldName("PHY_HAIKI_KIND_FLB", "廃棄物種類");
                this.SetFieldName("PHY_UNIT_FLB", "単位");
                this.SetFieldName("PHY_ALL_FLB", "合計");
                this.SetFieldName("G2F_TOTAL_LASTGENBA_FLB", "最終処分場所計");
                this.SetFieldName("G1F_TOTAL_FLB", "総合計");
            }
            // 固定項目の列定義(廃棄物種類別)
            if (this.layoutno == "5")
            {
                // ページヘッダー部
                this.SetFieldName("FH_TITLE_FLB", "マニフェスト推移表（廃棄物種類別）");    // タイトル
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");        // 発行日時
                this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime() + " " + "発行");        // 発行日時
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", corp);                          // 会社名  
                this.SetFieldName("PHY_ICHIJI_MANIFEST_FLB", this.firstsecoundkbn);         // マニフェスト区分

                this.SetFieldName("PHY_MONTH1_FLB", this.janmonthkbn);                  // １月カラム
                this.SetFieldName("PHY_MONTH2_FLB", this.febmonthkbn);                  // ２月カラム
                this.SetFieldName("PHY_MONTH3_FLB", this.marmonthkbn);                  // ３月カラム
                this.SetFieldName("PHY_MONTH4_FLB", this.aprmonthkbn);                  // ４月カラム
                this.SetFieldName("PHY_MONTH5_FLB", this.maymonthkbn);                  // ５月カラム
                this.SetFieldName("PHY_MONTH6_FLB", this.junmonthkbn);                  // ６月カラム
                this.SetFieldName("PHY_MONTH7_FLB", this.julymonthkbn);                 // ７月カラム
                this.SetFieldName("PHY_MONTH8_FLB", this.augmonthkbn);                  // ８月カラム
                this.SetFieldName("PHY_MONTH9_FLB", this.sepmonthkbn);                  // ９月カラム
                this.SetFieldName("PHY_MONTH10_FLB", this.octmonthkbn);                 // １０月カラム
                this.SetFieldName("PHY_MONTH11_FLB", this.novmonthkbn);                 // １１月カラム
                this.SetFieldName("PHY_MONTH12_FLB", this.decmonthkbn);                 // １２月カラム
                this.SetFieldName("PHY_ALL_FLB", "合計");                               // 合計カラム


                this.SetFieldName("PHN_MONTH1_TOTAL_VLB", this.g2fjanalltotal);         // 総合計-１月別
                this.SetFieldName("PHN_MONTH2_TOTAL_VLB", this.g2ffeballtotal);         // 総合計-２月別
                this.SetFieldName("PHN_MONTH3_TOTAL_VLB", this.g2fmaralltotal);         // 総合計-３月別
                this.SetFieldName("PHN_MONTH4_TOTAL_VLB", this.g2fapralltotal);         // 総合計-４月別
                this.SetFieldName("PHN_MONTH5_TOTAL_VLB", this.g2fmayalltotal);         // 総合計-５月別
                this.SetFieldName("PHN_MONTH6_TOTAL_VLB", this.g2fjunalltotal);         // 総合計-６月別
                this.SetFieldName("PHN_MONTH7_TOTAL_VLB", this.g2fjlyalltotal);         // 総合計-７月別
                this.SetFieldName("PHN_MONTH8_TOTAL_VLB", this.g2faugalltotal);         // 総合計-８月別
                this.SetFieldName("PHN_MONTH9_TOTAL_VLB", this.g2fsepalltotal);         // 総合計-９月別
                this.SetFieldName("PHN_MONTH10_TOTAL_VLB", this.g2foctalltotal);        // 総合計-１０月別
                this.SetFieldName("PHN_MONTH11_TOTAL_VLB", this.g2fnovalltotal);        // 総合計-１１月別
                this.SetFieldName("PHN_MONTH12_TOTAL_VLB", this.g2fdecalltotal);        // 総合計-１２月別
                this.SetFieldName("PHN_TOTAL_VLB", this.g2fentirealltotal);             // 総合計-合計

                this.SetFieldName("PHY_JIGYOJO_CODE_FLB", "CD");
                this.SetFieldName("PHY_HAIKI_KIND_FLB", "廃棄物種類");
                this.SetFieldName("PHY_UNIT_FLB", "単位");
                this.SetFieldName("PHY_ALL_FLB", "合計");
                this.SetFieldName("G1F_TOTAL_FLB", "総合計");

                // フィールドの表示・非表示
                this.SetFieldVisible("G2H_LAST_SBN_GENBA_CD_CTL", false);
                this.SetFieldVisible("G2H_LAST_SBN_GENBA_NAME_CTL", false);

                this.SetFieldVisible("G2F_TOTAL_LASTGENBA_FLB", false);
                this.SetFieldVisible("G2F_MONTH1_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH2_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH3_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH4_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH5_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH6_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH6_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH7_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH8_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH9_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH10_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH11_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_MONTH12_LASTGENBATOTAL_CTL", false);
                this.SetFieldVisible("G2F_ALL_LASTGENBATOTAL_CTL", false);

                this.SetGroupVisible("GROUP1", false, true);
                this.SetGroupVisible("GROUP2", false, false);
            }
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            r_framework.Dao.GET_SYSDATEDao dateDao = r_framework.Utility.DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();
            System.Data.DataTable dt = dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        /// <summary>
        /// 帳票データより、C1Reportに渡すデータを作成する
        /// </summary>
        /// <param name="dataTable">dataTable</param>
        private void InputDataToMem(DataTable dataTable)
        {
            string manikbn = string.Empty;

            foreach (DataRow row in dataTable.Rows)
            {
                // row[0]を文字列に変換しtempに格納
                string tmp = row[0].ToString();
                // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                //    //string[] splt = { "\",\"" };
                string[] list = this.ReportSplit(tmp);

                if (list[0] == "0-1")
                {
                    // レイアウトNo
                    this.layoutno = list[1];
                }
            }


            if (layoutno == "1")
            {

                foreach (DataRow row in dataTable.Rows)
                {
                    // row[0]を文字列に変換しtempに格納
                    string tmp = row[0].ToString();
                    // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                    //    //string[] splt = { "\",\"" };
                    string[] list = this.ReportSplit(tmp);

                    if (list[0] == "0-1")
                    {
                        // 判定用
                        this.hantei = list[0];              // 判定区分
                        // レイアウトNo
                        this.layoutno = list[1];

                        this.corp = list[2];                // 会社名

                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(list[0], list[1], list[2]));
                    }
                    if (list[0] == "1-1")
                    {
                        this.hantei = list[0];              // 判定区分
                        this.firstsecoundkbn = list[1];     // 一次二次区分
                        //this.janfromdecgokeikbn = list[2];            // 月カラム＋合計

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[2].Split(',');

                        this.janmonthkbn = stArrayData[0];
                        this.febmonthkbn = stArrayData[1];
                        this.marmonthkbn = stArrayData[2];
                        this.aprmonthkbn = stArrayData[3];
                        this.maymonthkbn = stArrayData[4];
                        this.junmonthkbn = stArrayData[5];
                        this.julymonthkbn = stArrayData[6];
                        this.augmonthkbn = stArrayData[7];
                        this.sepmonthkbn = stArrayData[8];
                        this.octmonthkbn = stArrayData[9];
                        this.novmonthkbn = stArrayData[10];
                        this.decmonthkbn = stArrayData[11];
                        //this.janfromdecgokeitotalkbn = stArrayData[12];


                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(this.hantei, this.firstsecoundkbn, this.janmonthkbn, this.febmonthkbn, this.marmonthkbn, list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14], list[15]));
                    }
                    else if (list[0] == "2-1")
                    {
                        seq = seq + 1;
                        // 排出事業者CD
                        g2haishutujigyoushacd = list[1];
                        // 排出事業者名
                        g2haishutujigyoushaname = list[2];

                        //Group2Hedder部・マニュフェスト集計表         
                        this.group2Hedder.Add(new Group2Hedder(seq, list[0], list[1], list[2]));
                    }
                    else if (list[0] == "2-2")
                    {
                        // 排出事業場CD
                        this.haishutujigyoujocd = list[1];
                        // 排出事業者名
                        this.haishutujigyoujoname = list[2];
                        // 廃棄物種類
                        this.haikishurui = list[3];
                        // 単位
                        this.tani = list[4];

                        this.allshuruitotal = list[6];// 廃棄物種類-合計

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[5].Split(',');

                        this.shuruijanuary = stArrayData[0];        // 廃棄物種類-1月別
                        this.shuruifebruary = stArrayData[1];       // 廃棄物種類-2月別
                        this.shuruimarch = stArrayData[2];          // 廃棄物種類-3月別
                        this.shuruiapril = stArrayData[3];          // 廃棄物種類-4月別
                        this.shuruimay = stArrayData[4];            // 廃棄物種類-5月別
                        this.shuruijune = stArrayData[5];           // 廃棄物種類-6月別
                        this.shuruijuly = stArrayData[6];           // 廃棄物種類-7月別
                        this.shuruiaugust = stArrayData[7];         // 廃棄物種類-8月別
                        this.shuruiseptember = stArrayData[8];      // 廃棄物種類-9月別
                        this.shuruioctober = stArrayData[9];        // 廃棄物種類-10月別
                        this.shuruinovember = stArrayData[10];      // 廃棄物種類-11月別
                        this.shuruidecember = stArrayData[11];      // 廃棄物種類-12月別
                        
                        // Detail部・マニュフェスト集計表         
                        this.detail.Add(new Detail(seq, list[0], list[1], list[2], list[3], list[4], this.shuruijanuary, this.shuruifebruary, this.shuruimarch, this.shuruiapril, this.shuruimay, this.shuruijune,
                                                   this.shuruijuly, this.shuruiaugust, this.shuruiseptember, this.shuruioctober, this.shuruinovember, this.shuruidecember, this.allshuruitotal));
                        //seq = seq + 1;
                    }
                    else if (list[0] == "2-3")
                    {
                        // 排出事業者計-月別
                        string haishutu = list[1];
                        // 排出事業者計-合計
                        this.allshuruitotal = list[2];

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');

                        this.g2fjanjigyoshatotal = stArrayData[0];  // 排出事業者計-1月別
                        this.g2ffebjigyoshatotal = stArrayData[1];  // 排出事業者計-2月別
                        this.g2fmarjigyoshatotal = stArrayData[2];  // 排出事業者計-3月別
                        this.g2faprjigyoshatotal = stArrayData[3];  // 排出事業者計-4月別
                        this.g2fmayjigyoshatotal = stArrayData[4];  // 排出事業者計-5月別
                        this.g2fjunjigyoshatotal = stArrayData[5];  // 排出事業者計-6月別
                        this.g2fjlyjigyoshatotal = stArrayData[6];  // 排出事業者計-7月別
                        this.g2faugjigyoshatotal = stArrayData[7];  // 排出事業者計-8月別
                        this.g2fsepjigyoshatotal = stArrayData[8];  // 排出事業者計-9月別
                        this.g2foctjigyoshatotal = stArrayData[9];  // 排出事業者計-10月別
                        this.g2fnovjigyoshatotal = stArrayData[10]; // 排出事業者計-11月別
                        this.g2fdecjigyoshatotal = stArrayData[11]; // 排出事業者計-12月別

                        // Detail部・マニュフェスト集計表        
                        this.group2Footer.Add(new Group2Footer(seq, list[0], stArrayData[0], stArrayData[1], stArrayData[2], stArrayData[3], stArrayData[4], stArrayData[5], stArrayData[6], stArrayData[7],
                                                   stArrayData[8], stArrayData[9], stArrayData[10], stArrayData[11], list[2]));
                        
                    }
                    else if (list[0] == "2-4")
                    {
                        this.g2fentirealltotal = list[2];// 総合計-合計
                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');

                        this.g2fjanalltotal = stArrayData[0];   // 総合計-1月別
                        this.g2ffeballtotal = stArrayData[1];   // 総合計-2月別
                        this.g2fmaralltotal = stArrayData[2];   // 総合計-3月別
                        this.g2fapralltotal = stArrayData[3];   // 総合計-4月別
                        this.g2fmayalltotal = stArrayData[4];   // 総合計-5月別
                        this.g2fjunalltotal = stArrayData[5];   // 総合計-6月別
                        this.g2fjlyalltotal = stArrayData[6];   // 総合計-7月別
                        this.g2faugalltotal = stArrayData[7];   // 総合計-8月別
                        this.g2fsepalltotal = stArrayData[8];   // 総合計-9月別
                        this.g2foctalltotal = stArrayData[9];   // 総合計-10月別
                        this.g2fnovalltotal = stArrayData[10];  // 総合計-11月別
                        this.g2fdecalltotal = stArrayData[11];  // 総合計-12月別

                        // Group1Footer部・マニュフェスト集計表         
                        this.group1Footer.Add(new Group1Footer(list[0], this.g2fjanalltotal, this.g2ffeballtotal, this.g2fmaralltotal, this.g2fapralltotal, this.g2fmayalltotal, this.g2fjunalltotal,
                            this.g2fjlyalltotal, this.g2faugalltotal, this.g2fsepalltotal, this.g2foctalltotal, this.g2fnovalltotal, this.g2fdecalltotal, this.g2fentirealltotal));
                    }
                }
            }
            else if (layoutno == "2")
            {
                // 運搬受託者別
                foreach (DataRow row in dataTable.Rows)
                {
                    // row[0]を文字列に変換しtempに格納
                    string tmp = row[0].ToString();
                    // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                    //    //string[] splt = { "\",\"" };
                    string[] list = this.ReportSplit(tmp);

                    if (list[0] == "0-1")
                    {
                        // 判定用
                        this.hantei = list[0];              // 判定区分
                        // レイアウトNo
                        this.layoutno = list[1];

                        this.corp = list[2];                // 会社名

                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(list[0], list[1], list[2]));
                    }
                    if (list[0] == "1-1")
                    {
                        this.hantei = list[0];              // 判定区分
                        this.firstsecoundkbn = list[1];     // 一次二次区分

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[2].Split(',');

                        this.janmonthkbn = stArrayData[0];
                        this.febmonthkbn = stArrayData[1];
                        this.marmonthkbn = stArrayData[2];
                        this.aprmonthkbn = stArrayData[3];
                        this.maymonthkbn = stArrayData[4];
                        this.junmonthkbn = stArrayData[5];
                        this.julymonthkbn = stArrayData[6];
                        this.augmonthkbn = stArrayData[7];
                        this.sepmonthkbn = stArrayData[8];
                        this.octmonthkbn = stArrayData[9];
                        this.novmonthkbn = stArrayData[10];
                        this.decmonthkbn = stArrayData[11];

                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(this.hantei, this.firstsecoundkbn, this.janmonthkbn, this.febmonthkbn, this.marmonthkbn, list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14], list[15]));
                    }
                    else if (list[0] == "2-1")
                    {
                        
                        // 廃棄物区分ラベル
                        this.haikikbn = list[1];
                        // 運搬受託者CD
                        this.haishutujigyoujocd = list[2];
                        // 運搬受託者名
                        this.haishutujigyoujoname = list[3];
                        // 廃棄物種類
                        this.haikishurui = list[4];
                        // 単位
                        this.tani = list[5];

                        this.allshuruitotal = list[7];// 廃棄物種類-合計

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[6].Split(',');

                        this.shuruijanuary = stArrayData[0];    // 廃棄物種類-1月別
                        this.shuruifebruary = stArrayData[1];   // 廃棄物種類-2月別
                        this.shuruimarch = stArrayData[2];      // 廃棄物種類-3月別
                        this.shuruiapril = stArrayData[3];      // 廃棄物種類-4月別
                        this.shuruimay = stArrayData[4];        // 廃棄物種類-5月別
                        this.shuruijune = stArrayData[5];       // 廃棄物種類-6月別
                        this.shuruijuly = stArrayData[6];       // 廃棄物種類-7月別
                        this.shuruiaugust = stArrayData[7];     // 廃棄物種類-8月別
                        this.shuruiseptember = stArrayData[8];  // 廃棄物種類-9月別
                        this.shuruioctober = stArrayData[9];    // 廃棄物種類-10月別
                        this.shuruinovember = stArrayData[10];  // 廃棄物種類-11月別
                        this.shuruidecember = stArrayData[11];  // 廃棄物種類-12月別
                       
                        // Detail部・マニュフェスト集計表         
                        this.detail2.Add(new Detail2(index, list[0], list[1], list[2], list[3], list[4], list[5], this.shuruijanuary, this.shuruifebruary, this.shuruimarch, this.shuruiapril, this.shuruimay, this.shuruijune,
                                                   this.shuruijuly, this.shuruiaugust, this.shuruiseptember, this.shuruioctober, this.shuruinovember, this.shuruidecember, this.allshuruitotal, seqindex));
                    }
                    else if (list[0] == "2-2")
                    {
                        index = index + 1;
                        seq = seq + 1;
                        // 運搬受託者計ラベル
                        this.tumikaekbn = list[1];
                        // 運搬受託者計-月別
                        string haishutu = list[2];
                        // 運搬受託者計-合計
                        this.allshuruitotal = list[3];

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[2].Split(',');

                        //this.g2fjanjigyoshatotal = stArrayData[0];//運搬受託者計-1月別
                        //this.g2ffebjigyoshatotal = stArrayData[1];//運搬受託者計-2月別
                        //this.g2fmarjigyoshatotal = stArrayData[2];//運搬受託者計-3月別
                        //this.g2faprjigyoshatotal = stArrayData[3];//運搬受託者計-4月別
                        //this.g2fmayjigyoshatotal = stArrayData[4];//運搬受託者計-5月別
                        //this.g2fjunjigyoshatotal = stArrayData[5];//運搬受託者計-6月別
                        //this.g2fjlyjigyoshatotal = stArrayData[6];// 運搬受託者計-7月別
                        //this.g2faugjigyoshatotal = stArrayData[7];// 運搬受託者計-8月別
                        //this.g2fsepjigyoshatotal = stArrayData[8];// 運搬受託者計-9月別
                        //this.g2foctjigyoshatotal = stArrayData[9];// 運搬受託者計-10月別
                        //this.g2fnovjigyoshatotal = stArrayData[10];// 運搬受託者計-11月別
                        //this.g2fdecjigyoshatotal = stArrayData[11];// 運搬受託者計-12月別
                        
                        //// Detail部・マニュフェスト集計表        
                        //this.group3Footer.Add(new Group3Footer(seq, list[0], list[2], this.shuruijanuary, this.shuruifebruary, this.shuruimarch, this.shuruiapril, this.shuruimay, this.shuruijune, this.shuruijuly, this.shuruiaugust,
                        //                           this.shuruiseptember, shuruioctober, this.shuruinovember, this.shuruidecember, list[3]));
                         //Detail部・マニュフェスト集計表        
                        this.group3Footer.Add(new Group3Footer(seq, list[0], list[1], stArrayData[0], stArrayData[1], stArrayData[2], stArrayData[3], stArrayData[4], stArrayData[5],
                            stArrayData[6], stArrayData[7], stArrayData[8], stArrayData[9], stArrayData[10], stArrayData[11], this.allshuruitotal));
                    }
                    else if (list[0] == "2-3")
                    {

                        // 積替用合計ラベル 
                        string haikikbngoukei = list[1];
                        // 積替用合計-月別
                        string haishutu = list[2];
                        //// 積替用合計CD
                        //this.haishutujigyoujocd = list[1];
                        // 積替用合計-合計
                        this.allshuruitotal = list[3];

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[2].Split(',');

                        this.g2fjanjigyoshatotal2 = stArrayData[0];//積替用合計-1月別
                        this.g2ffebjigyoshatotal2 = stArrayData[1];//積替用合計-2月別
                        this.g2fmarjigyoshatotal2 = stArrayData[2];//積替用合計-3月別
                        this.g2faprjigyoshatotal2 = stArrayData[3];//積替用合計-4月別
                        this.g2fmayjigyoshatotal2 = stArrayData[4];//積替用合計-5月別
                        this.g2fjunjigyoshatotal2 = stArrayData[5];//積替用合計-6月別
                        this.g2fjlyjigyoshatotal2 = stArrayData[6];// 積替用合計-7月別
                        this.g2faugjigyoshatotal2 = stArrayData[7];// 積替用合計-8月別
                        this.g2fsepjigyoshatotal2 = stArrayData[8];// 積替用合計-9月別
                        this.g2foctjigyoshatotal2 = stArrayData[9];// 積替用合計-10月別
                        this.g2fnovjigyoshatotal2 = stArrayData[10];// 積替用合計-11月別
                        this.g2fdecjigyoshatotal2 = stArrayData[11];// 積替用合計-12月別
                        
                        // Detail部・マニュフェスト集計表        
                        this.upngroup2Footer.Add(new UpnGroup2Footer(seqindex, list[0], list[1], this.g2fjanjigyoshatotal2, this.g2ffebjigyoshatotal2, this.g2fmarjigyoshatotal2, this.g2faprjigyoshatotal2, this.g2fmayjigyoshatotal2, this.g2fjunjigyoshatotal2, this.g2fjlyjigyoshatotal2, this.g2faugjigyoshatotal2,
                                                   this.g2fsepjigyoshatotal2, g2foctjigyoshatotal2, this.g2fnovjigyoshatotal2, this.g2fdecjigyoshatotal2, list[3]));
                        seqindex = seqindex + 1;
                    }
                    else if (list[0] == "2-4")
                    {
                        this.g2fentirealltotal = list[2];// 総合計-合計
                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');

                        this.g2fjanalltotal = stArrayData[0];//総合計-1月別
                        this.g2ffeballtotal = stArrayData[1];//総合計-2月別
                        this.g2fmaralltotal = stArrayData[2];//総合計-3月別
                        this.g2fapralltotal = stArrayData[3];//総合計-4月別
                        this.g2fmayalltotal = stArrayData[4];//総合計-5月別
                        this.g2fjunalltotal = stArrayData[5];//総合計-6月別
                        this.g2fjlyalltotal = stArrayData[6];//総合計-7月別
                        this.g2faugalltotal = stArrayData[7];// 総合計-8月別
                        this.g2fsepalltotal = stArrayData[8];// 総合計-9月別
                        this.g2foctalltotal = stArrayData[9];// 総合計-10月別
                        this.g2fnovalltotal = stArrayData[10];// 総合計-11月別
                        this.g2fdecalltotal = stArrayData[11];// 総合計-12月別
                        
                        // Group1Footer部・マニュフェスト集計表         
                        this.upngroup1Footer.Add(new UpnGroup1Footer(list[0], this.g2fjanalltotal, this.g2ffeballtotal, this.g2fmaralltotal, this.g2fapralltotal, this.g2fmayalltotal, this.g2fjunalltotal,
                            this.g2fjlyalltotal, this.g2faugalltotal, this.g2fsepalltotal, this.g2foctalltotal, this.g2fnovalltotal, this.g2fdecalltotal, this.g2fentirealltotal));
                    }
                }
            }
            else if (layoutno == "3")
            {
                // 処分受託者別
                foreach (DataRow row in dataTable.Rows)
                {
                    // row[0]を文字列に変換しtempに格納
                    string tmp = row[0].ToString();
                    // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                    //    //string[] splt = { "\",\"" };
                    string[] list = this.ReportSplit(tmp);

                    if (list[0] == "0-1")
                    {
                        // 判定用
                        this.hantei = list[0];              // 判定区分
                        // レイアウトNo
                        this.layoutno = list[1];

                        this.corp = list[2];                // 会社名

                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(list[0], list[1], list[2]));
                    }
                    if (list[0] == "1-1")
                    {
                        this.hantei = list[0];              // 判定区分
                        this.firstsecoundkbn = list[1];     // 一次二次区分
                        
                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[2].Split(',');

                        this.janmonthkbn = stArrayData[0];
                        this.febmonthkbn = stArrayData[1];
                        this.marmonthkbn = stArrayData[2];
                        this.aprmonthkbn = stArrayData[3];
                        this.maymonthkbn = stArrayData[4];
                        this.junmonthkbn = stArrayData[5];
                        this.julymonthkbn = stArrayData[6];
                        this.augmonthkbn = stArrayData[7];
                        this.sepmonthkbn = stArrayData[8];
                        this.octmonthkbn = stArrayData[9];
                        this.novmonthkbn = stArrayData[10];
                        this.decmonthkbn = stArrayData[11];
                       
                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(this.hantei, this.firstsecoundkbn, this.janmonthkbn, this.febmonthkbn, this.marmonthkbn, list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14], list[15]));
                    }
                    else if (list[0] == "2-1")
                    {
                        seq = seq + 1;
                        // 排出事業者CD
                        g2haishutujigyoushacd = list[1];
                        // 排出事業者名
                        g2haishutujigyoushaname = list[2];

                        //Group2Hedder部・マニュフェスト集計表         
                        this.group2Hedder.Add(new Group2Hedder(seq, list[0], list[1], list[2]));
                    }
                    else if (list[0] == "2-2")
                    {
                        // 排出事業場CD
                        this.haishutujigyoujocd = list[1];
                        // 排出事業者名
                        this.haishutujigyoujoname = list[2];
                        // 廃棄物種類
                        this.haikishurui = list[3];
                        // 単位
                        this.tani = list[4];

                        this.allshuruitotal = list[6];// 廃棄物種類-合計

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[5].Split(',');

                        this.shuruijanuary = stArrayData[0];//廃棄物種類-1月別
                        this.shuruifebruary = stArrayData[1];//廃棄物種類-2月別
                        this.shuruimarch = stArrayData[2];//廃棄物種類-3月別
                        this.shuruiapril = stArrayData[3];//廃棄物種類-4月別
                        this.shuruimay = stArrayData[4];//廃棄物種類-5月別
                        this.shuruijune = stArrayData[5];//廃棄物種類-6月別
                        this.shuruijuly = stArrayData[6];// 廃棄物種類-7月別
                        this.shuruiaugust = stArrayData[7];// 廃棄物種類-8月別
                        this.shuruiseptember = stArrayData[8];// 廃棄物種類-9月別
                        this.shuruioctober = stArrayData[9];// 廃棄物種類-10月別
                        this.shuruinovember = stArrayData[10];// 廃棄物種類-11月別
                        this.shuruidecember = stArrayData[11];// 廃棄物種類-12月別
                        
                        // Detail部・マニュフェスト集計表         
                        this.detail.Add(new Detail(seq, list[0], list[1], list[2], list[3], list[4], this.shuruijanuary, this.shuruifebruary, this.shuruimarch, this.shuruiapril, this.shuruimay, this.shuruijune,
                                                   this.shuruijuly, this.shuruiaugust, this.shuruiseptember, this.shuruioctober, this.shuruinovember, this.shuruidecember, this.allshuruitotal));
                    }
                    else if (list[0] == "2-3")
                    {
                        // 排出事業者計-月別
                        string haishutu = list[1];
                        // 排出事業者計-合計
                        this.allshuruitotal = list[2];

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');

                        this.g2fjanjigyoshatotal = stArrayData[0];//排出事業者計-1月別
                        this.g2ffebjigyoshatotal = stArrayData[1];//排出事業者計-2月別
                        this.g2fmarjigyoshatotal = stArrayData[2];//排出事業者計-3月別
                        this.g2faprjigyoshatotal = stArrayData[3];//排出事業者計-4月別
                        this.g2fmayjigyoshatotal = stArrayData[4];//排出事業者計-5月別
                        this.g2fjunjigyoshatotal = stArrayData[5];//排出事業者計-6月別
                        this.g2fjlyjigyoshatotal = stArrayData[6];// 排出事業者計-7月別
                        this.g2faugjigyoshatotal = stArrayData[7];// 排出事業者計-8月別
                        this.g2fsepjigyoshatotal = stArrayData[8];// 排出事業者計-9月別
                        this.g2foctjigyoshatotal = stArrayData[9];// 排出事業者計-10月別
                        this.g2fnovjigyoshatotal = stArrayData[10];// 排出事業者計-11月別
                        this.g2fdecjigyoshatotal = stArrayData[11];// 排出事業者計-12月別
                        
                        // Detail部・マニュフェスト集計表        
                        this.group2Footer.Add(new Group2Footer(seq, list[0], stArrayData[0], stArrayData[1], stArrayData[2], stArrayData[3], stArrayData[4], stArrayData[5], stArrayData[6], stArrayData[7],
                                                   stArrayData[8], stArrayData[9], stArrayData[10], stArrayData[11], list[2]));
                    }
                    else if (list[0] == "2-4")
                    {
                        this.g2fentirealltotal = list[2];// 総合計-合計
                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');


                        this.g2fjanalltotal = stArrayData[0];//総合計-1月別
                        this.g2ffeballtotal = stArrayData[1];//総合計-2月別
                        this.g2fmaralltotal = stArrayData[2];//総合計-3月別
                        this.g2fapralltotal = stArrayData[3];//総合計-4月別
                        this.g2fmayalltotal = stArrayData[4];//総合計-5月別
                        this.g2fjunalltotal = stArrayData[5];//総合計-6月別
                        this.g2fjlyalltotal = stArrayData[6];//総合計-7月別
                        this.g2faugalltotal = stArrayData[7];// 総合計-8月別
                        this.g2fsepalltotal = stArrayData[8];// 総合計-9月別
                        this.g2foctalltotal = stArrayData[9];// 総合計-10月別
                        this.g2fnovalltotal = stArrayData[10];// 総合計-11月別
                        this.g2fdecalltotal = stArrayData[11];// 総合計-12月別
                        
                        // Group1Footer部・マニュフェスト集計表         
                        this.group1Footer.Add(new Group1Footer(list[0], this.g2fjanalltotal, this.g2ffeballtotal, this.g2fmaralltotal, this.g2fapralltotal, this.g2fmayalltotal, this.g2fjunalltotal,
                            this.g2fjlyalltotal, this.g2faugalltotal, this.g2fsepalltotal, this.g2foctalltotal, this.g2fnovalltotal, this.g2fdecalltotal, this.g2fentirealltotal));
                    }
                }
            }
            else if (layoutno == "4")   // 最終処分場所別
            {
                // 最終処分場所別
                foreach (DataRow row in dataTable.Rows)
                {
                    // row[0]を文字列に変換しtempに格納
                    string tmp = row[0].ToString();
                    // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                    //    //string[] splt = { "\",\"" };
                    string[] list = this.ReportSplit(tmp);

                    if (list[0] == "0-1")
                    {
                        // 判定用
                        this.hantei = list[0];              // 判定区分
                        // レイアウトNo
                        this.layoutno = list[1];

                        this.corp = list[2];                // 会社名

                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(list[0], list[1], list[2]));
                    }
                    if (list[0] == "1-1")
                    {
                        this.hantei = list[0];              // 判定区分
                        this.firstsecoundkbn = list[1];     // 一次二次区分

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[2].Split(',');

                        this.janmonthkbn = stArrayData[0];
                        this.febmonthkbn = stArrayData[1];
                        this.marmonthkbn = stArrayData[2];
                        this.aprmonthkbn = stArrayData[3];
                        this.maymonthkbn = stArrayData[4];
                        this.junmonthkbn = stArrayData[5];
                        this.julymonthkbn = stArrayData[6];
                        this.augmonthkbn = stArrayData[7];
                        this.sepmonthkbn = stArrayData[8];
                        this.octmonthkbn = stArrayData[9];
                        this.novmonthkbn = stArrayData[10];
                        this.decmonthkbn = stArrayData[11];
                        
                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(this.hantei, this.firstsecoundkbn, this.janmonthkbn, this.febmonthkbn, this.marmonthkbn, list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14], list[15]));
                    }
                    else if (list[0] == "2-1")
                    {
                        seq = seq + 1;
                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                        //// 最終処分場CD
                        //this.lastsbngenbacd = list[1];
                        //// 最終処分場名
                        //this.lastsbngenbaname = list[2];
                        // 最終処分業者CD
                        this.lastsbngyoshacd = list[1];
                        // 最終処分業者名
                        this.lastsbngyoshaname = list[2];
                        // 最終処分場CD
                        this.lastsbngenbacd = list[3];
                        // 最終処分場名
                        this.lastsbngenbaname = list[4];

                        // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                        //Group2Hedder部・マニュフェスト集計表         
                        this.lastgroup2Header.Add(new LastGroup2Hedder(seq, list[0], list[1], list[2], list[3], list[4]));
                    }
                    else if (list[0] == "2-2")
                    {
                        // 廃棄物種類CD
                        this.haishutujigyoujocd = list[1];
                        // 廃棄物種類名
                        //this.haishutujigyoujoname = list[2];
                        this.haishutujigyoujoname = "";
                        // 廃棄物種類
                        this.haikishurui = list[2];
                        // 単位
                        this.tani = list[3];

                        this.allshuruitotal = list[5];// 廃棄物種類-合計

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[4].Split(',');

                        this.shuruijanuary = stArrayData[0];//廃棄物種類-1月別
                        this.shuruifebruary = stArrayData[1];//廃棄物種類-2月別
                        this.shuruimarch = stArrayData[2];//廃棄物種類-3月別
                        this.shuruiapril = stArrayData[3];//廃棄物種類-4月別
                        this.shuruimay = stArrayData[4];//廃棄物種類-5月別
                        this.shuruijune = stArrayData[5];//廃棄物種類-6月別
                        this.shuruijuly = stArrayData[6];// 廃棄物種類-7月別
                        this.shuruiaugust = stArrayData[7];// 廃棄物種類-8月別
                        this.shuruiseptember = stArrayData[8];// 廃棄物種類-9月別
                        this.shuruioctober = stArrayData[9];// 廃棄物種類-10月別
                        this.shuruinovember = stArrayData[10];// 廃棄物種類-11月別
                        this.shuruidecember = stArrayData[11];// 廃棄物種類-12月別
                        //this.allshuruitotal = stArrayData[12];// 廃棄物種類-合計

                        // Detail部・マニュフェスト集計表         
                        //this.detail.Add(new Detail(seq, list[0], list[1], list[2], list[3], list[4], this.shuruijanuary, this.shuruifebruary, this.shuruimarch, this.shuruiapril, this.shuruimay, this.shuruijune,
                        //                           this.shuruijuly, this.shuruiaugust, this.shuruiseptember, this.shuruioctober, this.shuruinovember, this.shuruidecember, this.allshuruitotal));
                        this.detail.Add(new Detail(seq, list[0], list[1], "", list[2], list[3], this.shuruijanuary, this.shuruifebruary, this.shuruimarch, this.shuruiapril, this.shuruimay, this.shuruijune,
                                                   this.shuruijuly, this.shuruiaugust, this.shuruiseptember, this.shuruioctober, this.shuruinovember, this.shuruidecember, this.allshuruitotal));
                    }
                    else if (list[0] == "2-3")
                    {
                        this.lastgenbasbntotal = list[2];// 最終処分場計-合計
                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');

                        this.lastgenbamanth1total = stArrayData[0];//最終処分場計-1月別
                        this.lastgenbamanth2total = stArrayData[1];//最終処分場計-2月別
                        this.lastgenbamanth3total = stArrayData[2];//最終処分場計-3月別
                        this.lastgenbamanth4total = stArrayData[3];//最終処分場計-4月別
                        this.lastgenbamanth5total = stArrayData[4];//最終処分場計-5月別
                        this.lastgenbamanth6total = stArrayData[5];//最終処分場計-6月別
                        this.lastgenbamanth7total = stArrayData[6];//最終処分場計-7月別
                        this.lastgenbamanth8total = stArrayData[7];// 最終処分場計-8月別
                        this.lastgenbamanth9total = stArrayData[8];// 最終処分場計-9月別
                        this.lastgenbamanth10total = stArrayData[9];// 最終処分場計-10月別
                        this.lastgenbamanth11total = stArrayData[10];// 最終処分場計-11月別
                        this.lastgenbamanth12total = stArrayData[11];// 最終処分場計-12月別

                        // LastGroup2Footer部・マニュフェスト集計表         
                        this.lastgroup2Footer.Add(new LastGroup2Footer(list[0], this.lastgenbamanth1total, this.lastgenbamanth2total, this.lastgenbamanth3total, this.lastgenbamanth4total, this.lastgenbamanth5total, this.lastgenbamanth6total,
                            this.lastgenbamanth7total, this.lastgenbamanth8total, this.lastgenbamanth9total, this.lastgenbamanth10total, this.lastgenbamanth11total, this.lastgenbamanth12total, this.lastgenbasbntotal));
                    }
                    
                    else if (list[0] == "2-4")
                    {
                        this.g2fentirealltotal = list[2];// 最終処分場計-合計
                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');


                        this.g2fjanalltotal = stArrayData[0];//総合計-1月別
                        this.g2ffeballtotal = stArrayData[1];//総合計-2月別
                        this.g2fmaralltotal = stArrayData[2];//総合計-3月別
                        this.g2fapralltotal = stArrayData[3];//総合計-4月別
                        this.g2fmayalltotal = stArrayData[4];//総合計-5月別
                        this.g2fjunalltotal = stArrayData[5];//総合計-6月別
                        this.g2fjlyalltotal = stArrayData[6];//総合計-7月別
                        this.g2faugalltotal = stArrayData[7];// 総合計-8月別
                        this.g2fsepalltotal = stArrayData[8];// 総合計-9月別
                        this.g2foctalltotal = stArrayData[9];// 総合計-10月別
                        this.g2fnovalltotal = stArrayData[10];// 総合計-11月別
                        this.g2fdecalltotal = stArrayData[11];// 総合計-12月別
                        
                        // Group1Footer部・マニュフェスト集計表         
                        this.group1Footer.Add(new Group1Footer(list[0], this.g2fjanalltotal, this.g2ffeballtotal, this.g2fmaralltotal, this.g2fapralltotal, this.g2fmayalltotal, this.g2fjunalltotal,
                            this.g2fjlyalltotal, this.g2faugalltotal, this.g2fsepalltotal, this.g2foctalltotal, this.g2fnovalltotal, this.g2fdecalltotal, this.g2fentirealltotal));
                    }
                }
            }
            else if (layoutno == "5")
            {

                foreach (DataRow row in dataTable.Rows)
                {
                    // row[0]を文字列に変換しtempに格納
                    string tmp = row[0].ToString();
                    // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                    //    //string[] splt = { "\",\"" };
                    string[] list = this.ReportSplit(tmp);

                    if (list[0] == "0-1")
                    {
                        // 判定用
                        this.hantei = list[0];              // 判定区分

                        this.layoutno = list[1];            // レイアウトNo

                        this.corp = list[2];                // 会社名

                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(list[0], list[1], list[2]));
                    }
                    if (list[0] == "1-1")
                    {
                        this.hantei = list[0];              // 判定区分
                        this.firstsecoundkbn = list[1];     // 一次二次区分
                        //this.janfromdecgokeikbn = list[2];            // 月カラム＋合計

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[2].Split(',');

                        this.janmonthkbn = stArrayData[0];
                        this.febmonthkbn = stArrayData[1];
                        this.marmonthkbn = stArrayData[2];
                        this.aprmonthkbn = stArrayData[3];
                        this.maymonthkbn = stArrayData[4];
                        this.junmonthkbn = stArrayData[5];
                        this.julymonthkbn = stArrayData[6];
                        this.augmonthkbn = stArrayData[7];
                        this.sepmonthkbn = stArrayData[8];
                        this.octmonthkbn = stArrayData[9];
                        this.novmonthkbn = stArrayData[10];
                        this.decmonthkbn = stArrayData[11];
                        
                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(this.hantei, this.firstsecoundkbn, this.janmonthkbn, this.febmonthkbn, this.marmonthkbn, list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14], list[15]));
                    }
                    
                    else if (list[0] == "2-1")
                    {
                        seq = seq + 1;
                        // 廃棄物種類CD
                        this.haishutujigyoujocd = list[1];
                        // 廃棄物種類
                        this.haikishurui = list[2];
                        // 単位
                        this.tani = list[3];

                        this.allshuruitotal = list[5];// 廃棄物種類-合計

                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[4].Split(',');

                        this.shuruijanuary = stArrayData[0];//廃棄物種類-1月別
                        this.shuruifebruary = stArrayData[1];//廃棄物種類-2月別
                        this.shuruimarch = stArrayData[2];//廃棄物種類-3月別
                        this.shuruiapril = stArrayData[3];//廃棄物種類-4月別
                        this.shuruimay = stArrayData[4];//廃棄物種類-5月別
                        this.shuruijune = stArrayData[5];//廃棄物種類-6月別
                        this.shuruijuly = stArrayData[6];// 廃棄物種類-7月別
                        this.shuruiaugust = stArrayData[7];// 廃棄物種類-8月別
                        this.shuruiseptember = stArrayData[8];// 廃棄物種類-9月別
                        this.shuruioctober = stArrayData[9];// 廃棄物種類-10月別
                        this.shuruinovember = stArrayData[10];// 廃棄物種類-11月別
                        this.shuruidecember = stArrayData[11];// 廃棄物種類-12月別
                        //this.allshuruitotal = stArrayData[12];// 廃棄物種類-合計

                        // Detail部・マニュフェスト集計表         
                        this.detail.Add(new Detail(seq, list[0], list[1], list[2], list[3], list[4], this.shuruijanuary, this.shuruifebruary, this.shuruimarch, this.shuruiapril, this.shuruimay, this.shuruijune,
                                                   this.shuruijuly, this.shuruiaugust, this.shuruiseptember, this.shuruioctober, this.shuruinovember, this.shuruidecember, this.allshuruitotal));
                    }
                    else if (list[0] == "2-2")
                    {
                        this.g2fentirealltotal = list[2];// 総合計-合計
                        // カンマ区切りで分割して配列に格納する
                        string[] stArrayData = list[1].Split(',');


                        this.g2fjanalltotal = stArrayData[0];//総合計-1月別
                        this.g2ffeballtotal = stArrayData[1];//総合計-2月別
                        this.g2fmaralltotal = stArrayData[2];//総合計-3月別
                        this.g2fapralltotal = stArrayData[3];//総合計-4月別
                        this.g2fmayalltotal = stArrayData[4];//総合計-5月別
                        this.g2fjunalltotal = stArrayData[5];//総合計-6月別
                        this.g2fjlyalltotal = stArrayData[6];//総合計-7月別
                        this.g2faugalltotal = stArrayData[7];// 総合計-8月別
                        this.g2fsepalltotal = stArrayData[8];// 総合計-9月別
                        this.g2foctalltotal = stArrayData[9];// 総合計-10月別
                        this.g2fnovalltotal = stArrayData[10];// 総合計-11月別
                        this.g2fdecalltotal = stArrayData[11];// 総合計-12月別
                        
                        // Group1Footer部・マニュフェスト集計表         
                        this.group1Footer.Add(new Group1Footer(list[0], this.g2fjanalltotal, this.g2ffeballtotal, this.g2fmaralltotal, this.g2fapralltotal, this.g2fmayalltotal, this.g2fjunalltotal,
                        this.g2fjlyalltotal, this.g2faugalltotal, this.g2fsepalltotal, this.g2foctalltotal, this.g2fnovalltotal, this.g2fdecalltotal, this.g2fentirealltotal));
                    }
                }
            }
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

                // カンマ区切りで分割して配列に格納する
                string[] stArrayData = ret[1].Split(',');

            return ret;
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Group2Hedder
        {
            public Group2Hedder(int seq, string hantei, string g2haishutujigyoushacd, string g2haishutujigyoushaname)
            {
                this.Seq = seq;
                this.Hantei = hantei;
                // 排出事業者CD
                this.G2haishutujigyoushacd = g2haishutujigyoushacd;
                // 排出事業者名
                this.G2haishutujigyoushaname = g2haishutujigyoushaname;
            }
            public int Seq { get; private set; }
            public string Hantei { get; private set; }
            public string G2haishutujigyoushacd { get; private set; }
            public string G2haishutujigyoushaname { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Detail
        {
            public Detail(int seq, string hantei, string haishutujigyoujocd,
               string haishutujigyoujoname, string haikishurui, string tani, string shuruijanuary, string shuruifebruary, string shuruimarch,
                string shuruiapril, string shuruimay, string shuruijune, string shuruijuly, string shuruiaugust, string shuruiseptember, string shuruioctober,
                string shuruinovember, string shuruidecember, string allshuruitotal)
            {
                this.Seq = seq;
                this.Hantei = hantei;
                
                // 会社名(略称)
                //this.Corp = corp;
  
                // 詳細部分の列定義（2-2）
                // 廃棄物種類CD
                this.Haishutujigyoujocd = haishutujigyoujocd;
                // 排出事業者名
                this.Haishutujigyoujoname = haishutujigyoujoname;
                // 廃棄物種類
                this.Haikishurui = haikishurui;
                // 単位
                this.Tani = tani;
                //廃棄物種類-月別
                //this.Shuruijanfromdecuary = shuruijanfromdec;

                //廃棄物種類-1月別
                this.Shuruijanuary = shuruijanuary;
                //廃棄物種類-2月別
                this.Shuruifebruary = shuruifebruary;
                //廃棄物種類-3月別
                this.Shuruimarch = shuruimarch;
                //廃棄物種類-4月別
                this.Shuruiapril = shuruiapril;
                //廃棄物種類-5月別
                this.Shuruimay = shuruimay;
                //廃棄物種類-6月別
                this.Shuruijune = shuruijune;
                //廃棄物種類-7月別
                this.Shuruijuly = shuruijuly;
                //廃棄物種類-8月別
                this.Shuruiaugust = shuruiaugust;
                //廃棄物種類-9月別
                this.Shuruiseptember = shuruiseptember;
                //廃棄物種類-10月別
                this.Shuruioctober = shuruioctober;
                //廃棄物種類-11月別
                this.Shuruinovember = shuruinovember;
                //廃棄物種類-12月別
                this.Shuruidecember = shuruidecember;
                //廃棄物種類-合計
                this.Allshuruitotal = allshuruitotal;
            }
            public int Seq { get; private set; }
            public string Hantei { get; private set; }
            public string Corp { get; private set; }
            public string Haishutujigyoujocd { get; private set; }
            public string Haishutujigyoujoname { get; private set; }
            public string Haikishurui { get; private set; }
            public string Tani { get; private set; }
            public string Shuruijanuary { get; private set; }
            public string Shuruifebruary { get; private set; }
            public string Shuruimarch { get; private set; }
            public string Shuruiapril { get; private set; }
            public string Shuruimay { get; private set; }
            public string Shuruijune { get; private set; }
            public string Shuruijuly { get; private set; }
            public string Shuruiaugust { get; private set; }
            public string Shuruiseptember { get; private set; }
            public string Shuruioctober { get; private set; }
            public string Shuruinovember { get; private set; }
            public string Shuruidecember { get; private set; }
            public string Allshuruitotal { get; private set; }
        }
        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Detail2
        {
            public Detail2(int seq, string hantei, string haikikbn, string haishutujigyoujocd,
               string haishutujigyoujoname, string haikishurui, string tani, string shuruijanuary, string shuruifebruary, string shuruimarch,
                string shuruiapril, string shuruimay, string shuruijune, string shuruijuly, string shuruiaugust, string shuruiseptember, string shuruioctober,
                string shuruinovember, string shuruidecember, string allshuruitotal, int seqindex)
            {
                this.Seq = seq;
                this.Hantei = hantei;

                // 廃棄物区分ラベル
                this.Haikikbn = haikikbn;

                // 詳細部分の列定義（2-2）
                // 排出事業場CD
                this.Haishutujigyoujocd = haishutujigyoujocd;
                // 排出事業者名
                this.Haishutujigyoujoname = haishutujigyoujoname;
                // 廃棄物種類
                this.Haikishurui = haikishurui;
                // 単位
                this.Tani = tani;

                //廃棄物種類-1月別
                this.Shuruijanuary = shuruijanuary;
                //廃棄物種類-2月別
                this.Shuruifebruary = shuruifebruary;
                //廃棄物種類-3月別
                this.Shuruimarch = shuruimarch;
                //廃棄物種類-4月別
                this.Shuruiapril = shuruiapril;
                //廃棄物種類-5月別
                this.Shuruimay = shuruimay;
                //廃棄物種類-6月別
                this.Shuruijune = shuruijune;
                //廃棄物種類-7月別
                this.Shuruijuly = shuruijuly;
                //廃棄物種類-8月別
                this.Shuruiaugust = shuruiaugust;
                //廃棄物種類-9月別
                this.Shuruiseptember = shuruiseptember;
                //廃棄物種類-10月別
                this.Shuruioctober = shuruioctober;
                //廃棄物種類-11月別
                this.Shuruinovember = shuruinovember;
                //廃棄物種類-12月別
                this.Shuruidecember = shuruidecember;
                //廃棄物種類-合計
                this.Allshuruitotal = allshuruitotal;

                this.Seqindex = seqindex;
            }
            public int Seq { get; private set; }
            public string Hantei { get; private set; }
            public string Haikikbn { get; private set; }
            public string Haishutujigyoujocd { get; private set; }
            public string Haishutujigyoujoname { get; private set; }
            public string Haikishurui { get; private set; }
            public string Tani { get; private set; }
            public string Shuruijanuary { get; private set; }
            public string Shuruifebruary { get; private set; }
            public string Shuruimarch { get; private set; }
            public string Shuruiapril { get; private set; }
            public string Shuruimay { get; private set; }
            public string Shuruijune { get; private set; }
            public string Shuruijuly { get; private set; }
            public string Shuruiaugust { get; private set; }
            public string Shuruiseptember { get; private set; }
            public string Shuruioctober { get; private set; }
            public string Shuruinovember { get; private set; }
            public string Shuruidecember { get; private set; }

            public string Allshuruitotal { get; private set; }
            public int Seqindex { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Group3Footer
        {
            //public Group3Footer(string hantei, string g2fjanfromdecjigyoshatotal, string g2fhaishutujigyoshaalltotal)
            public Group3Footer(int seq, string hantei, string tumikaekbn, string g2fjanjigyoshatotal, string g2ffebjigyoshatotal, string g2fmarjigyoshatotal, string g2faprjigyoshatotal,
                string g2fmayjigyoshatotal, string g2fjunjigyoshatotal, string g2fjlyjigyoshatotal, string g2faugjigyoshatotal, string g2fsepjigyoshatotal,
                string g2foctjigyoshatotal, string g2fnovjigyoshatotal, string g2fdecjigyoshatotal, string g2fhaishutujigyoshaalltotal)
            {
                this.Seq = seq;
                this.Hantei = hantei;

                // グループ３フッターの列定義（2-2）
                // 廃棄物種類毎合計ラベル
                this.Tumikaekbn = tumikaekbn;
                // 廃棄物種類毎合計-1月別
                this.G2fjanjigyoshatotal = g2fjanjigyoshatotal;
                // 廃棄物種類毎合計-2月別
                this.G2ffebjigyoshatotal = g2ffebjigyoshatotal;
                // 廃棄物種類毎合計-3月別
                this.G2fmarjigyoshatotal = g2fmarjigyoshatotal;
                // 廃棄物種類毎合計-4月別
                this.G2faprjigyoshatotal = g2faprjigyoshatotal;
                // 廃棄物種類毎合計-5月別
                this.G2fmayjigyoshatotal = g2fmayjigyoshatotal;
                // 廃棄物種類毎合計-6月別
                this.G2fjunjigyoshatotal = g2fjunjigyoshatotal;
                // 廃棄物種類毎合計-7月別
                this.G2fjlyjigyoshatotal = g2fjlyjigyoshatotal;
                // 廃棄物種類毎合計-8月別
                this.G2faugjigyoshatotal = g2faugjigyoshatotal;
                // 廃棄物種類毎合計-9月別
                this.G2fsepjigyoshatotal = g2fsepjigyoshatotal;
                // 廃棄物種類毎合計-10月別
                this.G2foctjigyoshatotal = g2foctjigyoshatotal;
                // 廃棄物種類毎合計-11月別
                this.G2fnovjigyoshatotal = g2fnovjigyoshatotal;
                // 廃棄物種類毎合計-12月別
                this.G2fdecjigyoshatotal = g2fdecjigyoshatotal;
                // 廃棄物種類毎合計-合計
                this.G2fhaishutujigyoshaalltotal = g2fhaishutujigyoshaalltotal;

            }
            public int Seq { get; private set; }
            public string Hantei { get; private set; }
            public string Tumikaekbn { get; private set; }
            public string G2fjanjigyoshatotal { get; private set; }
            public string G2ffebjigyoshatotal { get; private set; }
            public string G2fmarjigyoshatotal { get; private set; }
            public string G2faprjigyoshatotal { get; private set; }
            public string G2fmayjigyoshatotal { get; private set; }
            public string G2fjunjigyoshatotal { get; private set; }
            public string G2fjlyjigyoshatotal { get; private set; }
            public string G2faugjigyoshatotal { get; private set; }
            public string G2fsepjigyoshatotal { get; private set; }
            public string G2foctjigyoshatotal { get; private set; }
            public string G2fnovjigyoshatotal { get; private set; }
            public string G2fdecjigyoshatotal { get; private set; }
            public string G2fhaishutujigyoshaalltotal { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Group2Footer
        {
            public Group2Footer(int seq, string hantei, string g2fjanjigyoshatotal, string g2ffebjigyoshatotal, string g2fmarjigyoshatotal, string g2faprjigyoshatotal,
                string g2fmayjigyoshatotal, string g2fjunjigyoshatotal, string g2fjlyjigyoshatotal, string g2faugjigyoshatotal, string g2fsepjigyoshatotal,
                string g2foctjigyoshatotal, string g2fnovjigyoshatotal, string g2fdecjigyoshatotal, string g2fhaishutujigyoshaalltotal)
            {
                this.Seq = seq;
                this.Hantei = hantei;
                
                // グループ２フッターの列定義（2-3）
                // 排出事業者計-1月別
                this.G2fjanjigyoshatotal = g2fjanjigyoshatotal;
                // 排出事業者計-2月別
                this.G2ffebjigyoshatotal = g2ffebjigyoshatotal;
                // 排出事業者計-3月別
                this.G2fmarjigyoshatotal = g2fmarjigyoshatotal;
                // 排出事業者計-4月別
                this.G2faprjigyoshatotal = g2faprjigyoshatotal;
                // 排出事業者計-5月別
                this.G2fmayjigyoshatotal = g2fmayjigyoshatotal;
                // 排出事業者計-6月別
                this.G2fjunjigyoshatotal = g2fjunjigyoshatotal;
                // 排出事業者計-7月別
                this.G2fjlyjigyoshatotal = g2fjlyjigyoshatotal;
                // 排出事業者計-8月別
                this.G2faugjigyoshatotal = g2faugjigyoshatotal;
                // 排出事業者計-9月別
                this.G2fsepjigyoshatotal = g2fsepjigyoshatotal;
                // 排出事業者計-10月別
                this.G2foctjigyoshatotal = g2foctjigyoshatotal;
                // 排出事業者計-11月別
                this.G2fnovjigyoshatotal = g2fnovjigyoshatotal;
                // 排出事業者計-12月別
                this.G2fdecjigyoshatotal = g2fdecjigyoshatotal;
                // 排出事業者計-合計
                this.G2fhaishutujigyoshaalltotal = g2fhaishutujigyoshaalltotal;
                
            }
            public int Seq { get; private set; }
            public string Hantei { get; private set; }
            public string G2fjanjigyoshatotal { get; private set; }
            public string G2ffebjigyoshatotal { get; private set; }
            public string G2fmarjigyoshatotal { get; private set; }
            public string G2faprjigyoshatotal { get; private set; }
            public string G2fmayjigyoshatotal { get; private set; }
            public string G2fjunjigyoshatotal { get; private set; }
            public string G2fjlyjigyoshatotal { get; private set; }
            public string G2faugjigyoshatotal { get; private set; }
            public string G2fsepjigyoshatotal { get; private set; }
            public string G2foctjigyoshatotal { get; private set; }
            public string G2fnovjigyoshatotal { get; private set; }
            public string G2fdecjigyoshatotal { get; private set; }
            public string G2fhaishutujigyoshaalltotal { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール(運搬受託者用)
        /// </summary>
        private class UpnGroup2Footer
        {
            public UpnGroup2Footer(int seq, string hantei, string haikikbngoukei, string g2fjanjigyoshatotal2, string g2ffebjigyoshatotal2, string g2fmarjigyoshatotal2, string g2faprjigyoshatotal2,
                string g2fmayjigyoshatotal2, string g2fjunjigyoshatotal2, string g2fjlyjigyoshatotal2, string g2faugjigyoshatotal2, string g2fsepjigyoshatotal2,
                string g2foctjigyoshatotal2, string g2fnovjigyoshatotal2, string g2fdecjigyoshatotal2, string g2fhaishutujigyoshaalltotal2)
            {
                this.Seq = seq;
                this.Hantei = hantei;
                
                // グループ２フッターの列定義（2-3）
                // 合計ラベル 
                this.Haikikbngoukei = haikikbngoukei;
                // 合計-1月別
                this.G2fjanjigyoshatotal2 = g2fjanjigyoshatotal2;
                // 合計-2月別
                this.G2ffebjigyoshatotal2 = g2ffebjigyoshatotal2;
                // 合計-3月別
                this.G2fmarjigyoshatotal2 = g2fmarjigyoshatotal2;
                // 合計-4月別
                this.G2faprjigyoshatotal2 = g2faprjigyoshatotal2;
                // 合計-5月別
                this.G2fmayjigyoshatotal2 = g2fmayjigyoshatotal2;
                // 合計-6月別
                this.G2fjunjigyoshatotal2 = g2fjunjigyoshatotal2;
                // 合計-7月別
                this.G2fjlyjigyoshatotal2 = g2fjlyjigyoshatotal2;
                // 合計-8月別
                this.G2faugjigyoshatotal2 = g2faugjigyoshatotal2;
                // 合計-9月別
                this.G2fsepjigyoshatotal2 = g2fsepjigyoshatotal2;
                // 合計-10月別
                this.G2foctjigyoshatotal2 = g2foctjigyoshatotal2;
                // 合計-11月別
                this.G2fnovjigyoshatotal2 = g2fnovjigyoshatotal2;
                // 合計-12月別
                this.G2fdecjigyoshatotal2 = g2fdecjigyoshatotal2;
                // 合計-合計
                this.G2fhaishutujigyoshaalltotal2 = g2fhaishutujigyoshaalltotal2;
            }
            public int Seq { get; private set; }
            public string Hantei { get; private set; }
            public string Haikikbngoukei { get; private set; }
            public string G2fjanjigyoshatotal2 { get; private set; }
            public string G2ffebjigyoshatotal2 { get; private set; }
            public string G2fmarjigyoshatotal2 { get; private set; }
            public string G2faprjigyoshatotal2 { get; private set; }
            public string G2fmayjigyoshatotal2 { get; private set; }
            public string G2fjunjigyoshatotal2 { get; private set; }
            public string G2fjlyjigyoshatotal2 { get; private set; }
            public string G2faugjigyoshatotal2 { get; private set; }
            public string G2fsepjigyoshatotal2 { get; private set; }
            public string G2foctjigyoshatotal2 { get; private set; }
            public string G2fnovjigyoshatotal2 { get; private set; }
            public string G2fdecjigyoshatotal2 { get; private set; }
            public string G2fhaishutujigyoshaalltotal2 { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Group1Footer
        {
            public Group1Footer(string hantei, string g2fjanalltotal, string g2ffeballtotal,
            string g2fmaralltotal, string g2fapralltotal, string g2fmayalltotal, string g2fjunalltotal, string g2fjlyalltotal, string g2faugalltotal,
            string g2fsepalltotal, string g2foctalltotal, string g2fnovalltotal, string g2fdecalltotal,
            string g2fentirealltotal)
            {
                this.Hantei = hantei;
                
                // グループ１フッターの列定義（2-4）総合計
                // 総合計-1月別
                this.G2fjanalltotal = g2fjanalltotal;
                // 総合計-2月別
                this.G2ffeballtotal = g2ffeballtotal;
                // 総合計-3月別
                this.G2fmaralltotal = g2fmaralltotal;
                // 総合計-4月別
                this.G2fapralltotal = g2fapralltotal;
                // 総合計-5月別
                this.G2fmayalltotal = g2fmayalltotal;
                // 総合計-6月別
                this.G2fjunalltotal = g2fjunalltotal;
                // 総合計-7月別
                this.G2fjlyalltotal = g2fjlyalltotal;
                // 総合計-8月別
                this.G2faugalltotal = g2faugalltotal;
                // 総合計-9月別
                this.G2fsepalltotal = g2fsepalltotal;
                // 総合計-10月別
                this.G2foctalltotal = g2foctalltotal;
                // 総合計-11月別
                this.G2fnovalltotal = g2fnovalltotal;
                // 総合計-12月別
                this.G2fdecalltotal = g2fdecalltotal;
                // 総合計-合計
                this.G2fentirealltotal = g2fentirealltotal;
            }
            public string Hantei { get; private set; }
            // グループ１フッターの列定義（2-4）総合計
            // 総合計-1月別
            public string G2fjanalltotal { get; private set; }
            // 総合計-2月別
            public string G2ffeballtotal { get; private set; }
            // 総合計-3月別
            public string G2fmaralltotal { get; private set; }
            // 総合計-4月別
            public string G2fapralltotal { get; private set; }
            // 総合計-5月別
            public string G2fmayalltotal { get; private set; }
            // 総合計-6月別
            public string G2fjunalltotal { get; private set; }
            // 総合計-7月別
            public string G2fjlyalltotal { get; private set; }
            // 総合計-8月別
            public string G2faugalltotal { get; private set; }
            // 総合計-9月別
            public string G2fsepalltotal { get; private set; }
            // 総合計-10月別
            public string G2foctalltotal { get; private set; }
            // 総合計-11月別
            public string G2fnovalltotal { get; private set; }
            // 総合計-12月別
            public string G2fdecalltotal { get; private set; }
            // 総合計-合計
            public string G2fentirealltotal { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class UpnGroup1Footer
        {
            public UpnGroup1Footer(string hantei, string g2fjanalltotal, string g2ffeballtotal,
            string g2fmaralltotal, string g2fapralltotal, string g2fmayalltotal, string g2fjunalltotal, string g2fjlyalltotal, string g2faugalltotal,
            string g2fsepalltotal, string g2foctalltotal, string g2fnovalltotal, string g2fdecalltotal,
            string g2fentirealltotal)
            {
                this.Hantei = hantei;

                // グループ１フッターの列定義（2-4）総合計
                // 総合計-1月別
                this.G2fjanalltotal = g2fjanalltotal;
                // 総合計-2月別
                this.G2ffeballtotal = g2ffeballtotal;
                // 総合計-3月別
                this.G2fmaralltotal = g2fmaralltotal;
                // 総合計-4月別
                this.G2fapralltotal = g2fapralltotal;
                // 総合計-5月別
                this.G2fmayalltotal = g2fmayalltotal;
                // 総合計-6月別
                this.G2fjunalltotal = g2fjunalltotal;
                // 総合計-7月別
                this.G2fjlyalltotal = g2fjlyalltotal;
                // 総合計-8月別
                this.G2faugalltotal = g2faugalltotal;
                // 総合計-9月別
                this.G2fsepalltotal = g2fsepalltotal;
                // 総合計-10月別
                this.G2foctalltotal = g2foctalltotal;
                // 総合計-11月別
                this.G2fnovalltotal = g2fnovalltotal;
                // 総合計-12月別
                this.G2fdecalltotal = g2fdecalltotal;
                // 総合計-合計
                this.G2fentirealltotal = g2fentirealltotal;
            }

            public string Hantei { get; private set; }

            // グループ１フッターの列定義（2-4）総合計
            // 総合計-1月別
            public string G2fjanalltotal { get; private set; }
            // 総合計-2月別
            public string G2ffeballtotal { get; private set; }
            // 総合計-3月別
            public string G2fmaralltotal { get; private set; }
            // 総合計-4月別
            public string G2fapralltotal { get; private set; }
            // 総合計-5月別
            public string G2fmayalltotal { get; private set; }
            // 総合計-6月別
            public string G2fjunalltotal { get; private set; }
            // 総合計-7月別
            public string G2fjlyalltotal { get; private set; }
            // 総合計-8月別
            public string G2faugalltotal { get; private set; }
            // 総合計-9月別
            public string G2fsepalltotal { get; private set; }
            // 総合計-10月別
            public string G2foctalltotal { get; private set; }
            // 総合計-11月別
            public string G2fnovalltotal { get; private set; }
            // 総合計-12月別
            public string G2fdecalltotal { get; private set; }
            // 総合計-合計
            public string G2fentirealltotal { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール(最終処分場所別用)
        /// </summary>
        private class LastGroup2Hedder
        {
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
            //public LastGroup2Hedder(int seq, string hantei,string lastsbngenbacd, string lastsbngenbaname)
            //{
            public LastGroup2Hedder(int seq, string hantei, string lastsbngyoshacd, string lastsbngyoshaname, string lastsbngenbacd, string lastsbngenbaname)
            {
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                this.Seq = seq;
                this.Hantei = hantei;
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
                //最終処分業者CD
                this.Lastsbngyoshacd = lastsbngyoshacd;
                // 最終処分業者名
                this.Lastsbngyoshaname = lastsbngyoshaname;
                // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
                // 最終処分場CD
                this.Lastsbngenbacd = lastsbngenbacd;
                // 最終処分場名
                this.Lastsbngenbaname = lastsbngenbaname;
            }
            public int Seq { get; private set; }
            public string Hantei { get; private set; }
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　start
            public string Lastsbngyoshacd { get; private set; }
            public string Lastsbngyoshaname { get; private set; }
            // 20140621 syunrei EV004874_最終処分場ＣＤのみ表示される　end
            public string Lastsbngenbacd { get; private set; }
            public string Lastsbngenbaname { get; private set; }
        }
        
        /// <summary>
        /// 帳票明細データを表すクラス・コントロール(最終処分場所別用)
        /// </summary>
        private class LastGroup2Footer
        {
            public LastGroup2Footer(string hantei, string lastgenbamanth1total, string lastgenbamanth2total, string lastgenbamanth3total, string lastgenbamanth4total,
                string lastgenbamanth5total, string lastgenbamanth6total, string lastgenbamanth7total, string lastgenbamanth8total, string lastgenbamanth9total,
                string lastgenbamanth10total, string lastgenbamanth11total, string lastgenbamanth12total, string lastgenbasbntotal)
            {
                this.Hantei = hantei;

                // グループ２フッターの列定義（2-3）
                // 最終処分場計-1月別
                this.Lastgenbamanth1total = lastgenbamanth1total;
                // 最終処分場計-2月別
                this.Lastgenbamanth2total = lastgenbamanth2total;
                // 最終処分場計-3月別
                this.Lastgenbamanth3total = lastgenbamanth3total;
                // 最終処分場計-4月別
                this.Lastgenbamanth4total = lastgenbamanth4total;
                // 最終処分場計-5月別
                this.Lastgenbamanth5total = lastgenbamanth5total;
                // 最終処分場計-6月別
                this.Lastgenbamanth6total = lastgenbamanth6total;
                // 最終処分場計-7月別
                this.Lastgenbamanth7total = lastgenbamanth7total;
                // 最終処分場計-8月別
                this.Lastgenbamanth8total = lastgenbamanth8total;
                // 最終処分場計-9月別
                this.Lastgenbamanth9total = lastgenbamanth9total;
                // 最終処分場計-10月別
                this.Lastgenbamanth10total = lastgenbamanth10total;
                // 最終処分場計-11月別
                this.Lastgenbamanth11total = lastgenbamanth11total;
                // 最終処分場計-12月別
                this.Lastgenbamanth12total = lastgenbamanth12total;
                // 最終処分場計-合計
                this.Lastgenbasbntotal = lastgenbasbntotal;

            }
            public string Hantei { get; private set; }
            public string Lastgenbamanth1total { get; private set; }
            public string Lastgenbamanth2total { get; private set; }
            public string Lastgenbamanth3total { get; private set; }
            public string Lastgenbamanth4total { get; private set; }
            public string Lastgenbamanth5total { get; private set; }
            public string Lastgenbamanth6total { get; private set; }
            public string Lastgenbamanth7total { get; private set; }
            public string Lastgenbamanth8total { get; private set; }
            public string Lastgenbamanth9total { get; private set; }
            public string Lastgenbamanth10total { get; private set; }
            public string Lastgenbamanth11total { get; private set; }
            public string Lastgenbamanth12total { get; private set; }
            public string Lastgenbasbntotal { get; private set; }
        }
    }
}
