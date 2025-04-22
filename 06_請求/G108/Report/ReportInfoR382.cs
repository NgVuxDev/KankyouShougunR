using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;

namespace Shougun.Core.Billing.Seikyucheckhyo
{
    /// <summary> R382(請求チェック表)を表すクラス・コントロール </summary>
    public class ReportInfoR382 : ReportInfoBase
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();
        // <summary>List<T>クラスの（Detail型）のリストとしてインスタンス</summary>
        private List<Detail> detail = new List<Detail>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfoR382"/> class.
        /// </summary>
        public ReportInfoR382()
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "./Template/R382-Form.xml";

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

        //////////////// <summary>帳票用データテーブルを保持するプロパティ</summary>
        //////////////private DataTable chouhyouDataTable { get; set; }
             
        /// <summary>
        /// C1Reportの帳票データの作成ならびに明細部分の列定義を実行する
        /// </summary>
        public void R382_Report(DataTable chouhyouData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // フォームへ設定する情報処理
            // データテーブル作成処理のみ
            this.chouhyouDataTable = new DataTable();

            // 明細部分の列定義
            this.chouhyouDataTable.Columns.Add("PHY_HANTEI_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_KUBUN_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_KYOTEN_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_DENPYO_DATE_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_DENPYO_NO_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_TORIHIKISAKI_CD_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_TORIHIKI_SHIHARAI_SAKI_VLB");
            this.chouhyouDataTable.Columns.Add("PHY_MEISAI_NO_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_ERROR_NAIYO_FLB");
            this.chouhyouDataTable.Columns.Add("PHY_RIYU_FLB");
            this.chouhyouDataTable.Columns.Add("FH_CORP_RYAKU_NAME_VLB");

            DataRow row;

            for (int i = 0; i < this.detail.Count; i++)
            {
                // データテーブルと同じの構造をもったレコードを作成
                row = this.chouhyouDataTable.NewRow();

                row["PHY_HANTEI_FLB"] = this.detail[i].Hantei;
                row["PHY_KUBUN_FLB"] = this.detail[i].Kubun;                               // 区分
                row["PHY_KYOTEN_FLB"] = this.detail[i].Kyoten;                             // 拠点
                DateTime tempDate = DateTime.Now;
                if (DateTime.TryParse(this.detail[i].Denpyoudate, out tempDate))
                {
                    row["PHY_DENPYO_DATE_FLB"] = tempDate.Date.ToShortDateString();        // 伝票日付
                }
                row["PHY_DENPYO_NO_FLB"] = this.detail[i].Denpyouno;                       // 伝票番号
                row["PHY_TORIHIKISAKI_CD_FLB"] = this.detail[i].Torihikisakicd;            // 取引先CD
                row["PHY_TORIHIKI_SHIHARAI_SAKI_VLB"] = this.detail[i].Torihikisakiname;   // 取引先
                row["PHY_MEISAI_NO_FLB"] = this.detail[i].Meisaino;                        // 明細番号
                row["PHY_ERROR_NAIYO_FLB"] = this.detail[i].Errdetail;                     // エラー内容
                row["PHY_RIYU_FLB"] = this.detail[i].Riyuureason;                          // 理由
                row["FH_CORP_RYAKU_NAME_VLB"] = this.detail[i].CorpRyakuName;              // 会社略称

                this.chouhyouDataTable.Rows.Add(row);
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
            // ヘッダ部
            this.SetFieldName("FH_TITLE_VLB", "請求チェック表");                  // タイトル
            this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", this.detail[0].CorpRyakuName);              // 会社名
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime().ToString("yyyy/MM/dd H:mm:ss"));
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            // カラムヘッダ部
            this.SetFieldName("PHY_KUBUN_FLB", "区分");                           // 区分
            this.SetFieldName("PHY_KYOTEN_FLB", "拠点");                          // 拠点
            this.SetFieldName("PHY_DENPYO_DATE_FLB", "売上日付");                 // 売上日付
            this.SetFieldName("PHY_DENPYO_NO_FLB", "伝票番号");                   // 伝票番号
            this.SetFieldName("PHY_TORIHIKISAKI_CD_FLB", "取引先CD");             // 取引先CD
            this.SetFieldName("PHY_TORIHIKI_SHIHARAI_SAKI_VLB", "取引先");        // 取引先
            this.SetFieldName("PHY_MEISAI_NO_FLB", "明細番号");                   // 明細番号
            this.SetFieldName("PHY_ERROR_NAIYO_FLB", "エラー内容");               // エラー内容
            this.SetFieldName("PHY_RIYU_FLB", "理由");                            // 理由 
        }

        /// <summary>
        /// 帳票データより、C1Reportに渡すデータを作成する
        /// </summary>
        /// <param name="dataTable">dataTable</param>
        private void InputDataToMem(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                // row[0]を文字列に変換しtempに格納
                string tmp = row[0].ToString();
                //// ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                //    //string[] splt = { "\",\"" };
                string[] list = this.ReportSplit(tmp);
                // Detail部          
                this.detail.Add(new Detail(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10]));
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
        private class Detail
        {
            public Detail(string hantei, string kubun, string kyoten, string denpyoudate, string denpyouno, string torihikisakicd, string torihikisakiname, string meisaino, string errdetail, string riyuureason, string corpRyakuName)
            {
                this.Hantei = hantei;
                this.Kubun = kubun;
                this.Kyoten = kyoten;
                this.Denpyoudate = denpyoudate;
                this.Denpyouno = denpyouno;
                this.Torihikisakicd = torihikisakicd;
                this.Torihikisakiname = torihikisakiname;
                this.Meisaino = meisaino;
                this.Errdetail = errdetail;
                this.Riyuureason = riyuureason;
                this.CorpRyakuName = corpRyakuName;
            }

            public string Hantei { get; private set; }
            public string Kubun { get; private set; }
            public string Kyoten { get; private set; }
            public string Denpyoudate { get; private set; }
            public string Denpyouno { get; private set; }
            public string Torihikisakicd { get; private set; }
            public string Torihikisakiname { get; private set; }
            public string Meisaino { get; private set; }
            public string Errdetail { get; private set; }
            public string Riyuureason { get; private set; }
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
