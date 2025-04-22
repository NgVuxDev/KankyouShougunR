using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;

namespace Report
{
    /// <summary> R389(マニュフェストチェック表)を表すクラス・コントロール </summary>
    public class ReportInfoR389 : ReportInfoBase
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();
        // <summary>List<T>クラスの（Detail型）のリストとしてインスタンス</summary>
        private List<Detail> detail = new List<Detail>();
        // <summary>List<T>クラスの（Detail型）のリストとしてインスタンス</summary>
        private List<Detailmaster> detailmas = new List<Detailmaster>();
        // <summary>List<T>クラスの（Header型）のリストとしてインスタンス</summary>
        private List<Header> header = new List<Header>();
        // <summary>List<T>クラスの（Pageheader型）のリストとしてインスタンス</summary>
        private List<Pageheader> pageheader = new List<Pageheader>();

        // ヘッダー部分の列定義（0-1）
        // レイアウトNo
        private string layoutno = string.Empty;
        // 会社名(略称)
        private string corp = string.Empty;
        // ページヘッダー部分の列定義（1-1）
        // 一次マニュフェスト
        private string manifest = string.Empty;
        // 詳細部分の列定義（1-2）
        // 交付番号
        private string koufuno = string.Empty;
        // チェック項目
        private string checkitem = string.Empty;
        // 対象フォーム判別
        private string taishouform = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfoR382"/> class.
        /// </summary>
        public ReportInfoR389()
        {
            // 帳票出力フルパスフォーム名
            //this.OutputFormFullPathName = TEMPLATE_PATH + "R382_R387-Form.xml";
            this.OutputFormFullPathName = "Template/R389-Form.xml";
    
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
        public void R389_Report(DataTable chouhyouData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // フォームへ設定する情報処理
            // データテーブル作成処理のみ
            this.chouhyouDataTable = new DataTable();

            // 明細部分の列定義
            if (this.taishouform == "1")
            {
                // マニフェスト用の列定義
                this.OutputFormLayout = "LAYOUT1";

                this.chouhyouDataTable.Columns.Add("PHY_ICHIJI_MANIFEST_FLB");
                this.chouhyouDataTable.Columns.Add("PHY_HAIKI_KBN_FLB");
                this.chouhyouDataTable.Columns.Add("PHY_KOUFU_NO_FLB");
                this.chouhyouDataTable.Columns.Add("PHY_CHECK_ITEM_FLB");

                DataRow row;

                for (int i = 0; i < this.detail.Count; i++)
                {
                    // データテーブルと同じの構造をもったレコードを作成
                    row = this.chouhyouDataTable.NewRow();
                    // 帳票フォームに設定
                    row["PHY_ICHIJI_MANIFEST_FLB"] = this.detail[i].Manifest;               // 一次マニュフェスト
                    row["PHY_HAIKI_KBN_FLB"] = this.detail[i].ManiKubun;                    // 廃棄物区分
                    row["PHY_KOUFU_NO_FLB"] = this.detail[i].Koufuno;                       // 交付番号
                    row["PHY_CHECK_ITEM_FLB"] = this.detail[i].Checkitem;                   // チェック項目

                    this.chouhyouDataTable.Rows.Add(row);
                }
            }
            else
            {
                // マスタ用の列定義
                this.OutputFormLayout = "LAYOUT2";

                this.chouhyouDataTable.Columns.Add("PHY_HANTEI_FLB");
                this.chouhyouDataTable.Columns.Add("PHY_MASTER_SHURUI_FLB");
                this.chouhyouDataTable.Columns.Add("PHY_MASTER_CD_FLB");
                this.chouhyouDataTable.Columns.Add("PHY_MASTER_NAME_FLB");
                this.chouhyouDataTable.Columns.Add("PHY_CHECK_ITEM_FLB");

                DataRow row;

                for (int i = 0; i < this.detailmas.Count; i++)
                {
                    // データテーブルと同じの構造をもったレコードを作成
                    row = this.chouhyouDataTable.NewRow();
                    // 帳票フォームに設定
                    row["PHY_HANTEI_FLB"] = this.detailmas[i].Hanteimas;
                    row["PHY_MASTER_SHURUI_FLB"] = this.detailmas[i].Manifestmas;           // マスタ種類
                    row["PHY_MASTER_CD_FLB"] = this.detailmas[i].Codeno;                    // コード
                    row["PHY_MASTER_NAME_FLB"] = this.detailmas[i].Meishou;                 // 名称
                    row["PHY_CHECK_ITEM_FLB"] = this.detailmas[i].Checkitemmas;             // チェック項目

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
            if (this.taishouform == "1")
            {
                // ヘッダ部
                this.SetFieldName("FH_TITLE_VLB", "マニフェストチェック表");              // タイトル
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", this.header[0].CorpRyakuName);  // 会社名
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");        // 発行日時
                this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime() + " " + "発行");        // 発行日時
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                // ページヘッダー部
                //this.SetFieldName("PHY_ICHIJI_MANIFEST_FLB", "一次マニフェスト");
                // 詳細部
                //this.SetFieldName("PHY_ICHIJI_MANIFEST_FLB", this.manifest);
                this.SetFieldName("PHY_KOUFU_NO_FLB", "交付番号");                          // 交付番号
                this.SetFieldName("PHY_CHECK_ITEM_FLB", "チェック項目");                    // チェック項目
            }
            else
            {
                // ヘッダ部
                this.SetFieldName("FH_TITLE_VLB", "マスタチェック表");                      // タイトル
                this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", this.header[0].CorpRyakuName);  // 会社名
                this.SetFieldName("FH_PRINT_DATE_VLB", DateTime.Now + " " + "発行");        // 発行日時
                // ページヘッダー部
                this.SetFieldName("PHY_MASTER_SHURUI_FLB", "マスタ種類");
                // 詳細部
                //this.SetFieldName("PHY_ICHIJI_MANIFEST_FLB", this.manifest);
                this.SetFieldName("PHY_MASTER_CD_FLB", "コード");                           // コード
                this.SetFieldName("PHY_MASTER_NAME_FLB", "名称");                           // 名称
                this.SetFieldName("PHY_CHECK_ITEM_FLB", "チェック項目");                    // チェック項目
            }
        }

        /// <summary>
        /// 帳票データより、C1Reportに渡すデータを作成する
        /// </summary>
        /// <param name="dataTable">dataTable</param>
        private void InputDataToMem(DataTable dataTable)
        {
            string pagemanifest = string.Empty;
            string maniKubun = string.Empty;
            // 判別フラグ
            //string taishou = string.Empty;
            //int count = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                // row[0]を文字列に変換しtempに格納
                string tmp = row[0].ToString();
                // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                //    //string[] splt = { "\",\"" };
                string[] list = this.ReportSplit(tmp);
                // レイアウトNo判別処理
                if (this.taishouform == "1")
                {
                    // マニフェスト時

                    if (list[0] == "0-1")
                    {
                        // レイアウトNoを格納
                        this.taishouform = list[1];
                        //if (dataTable.Rows.Count == count)
                        //    || ()
                        // ヘッダー部          
                        this.header.Add(new Header(list[0], list[1], list[2]));
                    }
                    else if (list[0] == "1-1")
                    {
                        pagemanifest = list[1];
                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(list[0], list[1]));
                    }
                    else if (list[0] == "1-2")
                    {
                        // マニフェスト区分     
                        maniKubun = list[1];
                    }
                    else if (list[0] == "1-3")
                    {
                        // Detail部・マニュフェスト用         
                        this.detail.Add(new Detail(list[1], list[2], pagemanifest, maniKubun));
                    }
                }
                else
                {
                    // マスタ時

                    if (list[0] == "0-1")
                    {
                        // レイアウトNoを格納
                        this.taishouform = list[1];
                        //if (dataTable.Rows.Count == count)
                        //    || ()
                        // ヘッダー部          
                        this.header.Add(new Header(list[0], list[1], list[2]));
                    }
                    else if (list[0] == "1-1")
                    {
                        pagemanifest = list[1];
                        // ページヘッダー部          
                        //this.pageheader.Add(new Pageheader(list[0], list[1]));
                    }
                    else if (list[0] == "1-2")
                    {                       
                        // Detail部・マスタ用         
                        this.detailmas.Add(new Detailmaster(list[0], list[1], list[2], list[3], pagemanifest));                       
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

            return ret;
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Header
        {
            public Header(string hantei, string layoutno, string corpRyakuName)
            {
                this.Hantei = hantei;
                this.Layoutno = layoutno;
                this.CorpRyakuName = corpRyakuName;
            }
            
            public string Hantei { get; private set; }
            public string Layoutno { get; private set; }
            //public string Manifest { get; private set; }
            //public string Koufuno { get; private set; }
            //public string Checkitem { get; private set; }
            public string CorpRyakuName { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Pageheader
        {    
            public Pageheader(string hantei, string manifest)
            {
                this.Hantei = hantei;
                this.Manifest = manifest;    
            }
            
            public string Hantei { get; private set; }
            //public string Layoutno { get; private set; }
            public string Manifest { get; private set; }
            //public string Koufuno { get; private set; }
            //public string Checkitem { get; private set; }
            public string CorpRyakuName { get; private set; }
        }

        /// <summary> 帳票明細データを表すクラス・コントロール </summary>
        private class Detail
        {
            public Detail(string koufuno, string checkitem, string manifest, string maniKubun)
            {
                this.Koufuno = koufuno;
                this.Checkitem = checkitem;
                this.Manifest = manifest;
                this.ManiKubun = maniKubun;
            }

            public string Koufuno { get; private set; }
            public string Checkitem { get; private set; }
            public string Manifest { get; private set; }
            public string ManiKubun { get; private set; }
        }

        /// <summary>
        /// 帳票明細データを表すクラス・コントロール
        /// </summary>
        private class Detailmaster
        {
            public Detailmaster(string hantei, string codeno, string meishou, string checkitem, string manifest)
            {
                this.Hanteimas = hantei;
                this.Manifestmas = manifest;
                this.Codeno = codeno;
                this.Meishou = meishou;
                this.Checkitemmas = checkitem;
            }

            public string Hanteimas { get; private set; }
            //public string Layoutnomas { get; private set; }
            public string Manifestmas { get; private set; }
            public string Codeno { get; private set; }
            public string Checkitemmas { get; private set; }
            public string Meishou { get; private set; }

            public string CorpRyakuName { get; private set; }
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            r_framework.Dao.GET_SYSDATEDao dao = r_framework.Utility.DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
