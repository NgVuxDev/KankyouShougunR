using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;

namespace Shougun.Core.Stock.ZaikoKanriHyo
{
    #region - Class -

    /// <summary> R634(在庫管理表)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR634 : ReportInfoBase
    {
        #region - Fields -

        // 帳票用データテーブル
        private DataTable chouhyouDataTable = new DataTable();
        private int outPutKbn = 1;

        // ヘッダー部
        private string corpRyakuName = string.Empty;       // 会社略称
        private string dateFrom = string.Empty;            // 伝票日付開始
        private string dateTo = string.Empty;              // 伝票日付終了
        private string gyoushaCdFrom = string.Empty;       // 業者CD開始
        private string gyoushaNameFrom = string.Empty;     // 業者名称開始
        private string gyoushaCdTo = string.Empty;         // 業者CD終了
        private string gyoushaNameTo = string.Empty;       // 業者名称終了
        private string genbaCdFrom = string.Empty;         // 現場CD開始
        private string genbaNameFrom = string.Empty;       // 現場名称開始
        private string genbaCdTo = string.Empty;           // 現場CD終了
        private string genbaNameTo = string.Empty;         // 現場名称終了
        private string zaikoHinmeiCdFrom = string.Empty;   // 在庫品名CD開始
        private string zaikoHinmeiNameFrom = string.Empty; // 在庫品名名称開始
        private string zaikoHinmeiCdTo = string.Empty;     // 在庫品名CD終了
        private string zaikoHinmeiNameTo = string.Empty;   // 在庫品名名称終了
        private string hyoukaHouhou = string.Empty;        // 評価方法

        // 明細部データ
        private List<Detail> detail = new List<Detail>();      // 明細部データ

        // 合計部
        private string preZaikoRyou = string.Empty;           // 総合計：繰越在庫量
        private string preZaikoKingaku = string.Empty;        // 総合計：繰越在庫金額
        private string ukeireRyou = string.Empty;             // 総合計：受入量
        private string shukkaRyou = string.Empty;             // 総合計：出荷量
        private string idouChouseiRyou = string.Empty;        // 総合計：移動/調整量
        private string nowZaikoRyou = string.Empty;           // 総合計：当月在庫量
        private string nowZaikoKingaku = string.Empty;        // 総合計：当月在庫金額
        private string toTalZaikoRyou = string.Empty;         // 総合計：合計在庫量
        private string toTalZaikoKingaku = string.Empty;      // 総合計：合計在庫金額

        #endregion

        #region - Constructors -

        /// <summary> Initializes a new instance of the <see cref="ReportInfoR634" /> class. </summary>
        public ReportInfoR634()
        {
            // パスとレイアウトのデフォルト値を設定しておく

            // 帳票ファイルフルパス
            this.OutputFormFullPathName1 = "./Template/R634-Form_01.xml";
            this.OutputFormFullPathName2 = "./Template/R634-Form_02.xml";

            // 帳票出力フォームレイアウト名
            this.OutputFormLayout = "LAYOUT1";
        }

        #endregion

        #region - properties -
        /// <summary>帳票出力フルパスフォーム名を保持するフィールド</summary>
        public string OutputFormFullPathName1 { get; set; }
        public string OutputFormFullPathName2 { get; set; }

        /// <summary>帳票出力フォームレイアウト名を保持するフィールド</summary>
        public string OutputFormLayout { get; set; }

        /// <summary>帳票用データテーブルを保持するプロパティ</summary>
        public DataTable ChouhyouDataTable { get; set; }
        #endregion

        #region - Methods -

        /// <summary> C1Reportの帳票データの作成を実行する </summary>
        /// <param name="chouhyouData">chouhyouData</param>
        public void R634_Reprt(DataTable chouhyouData, int outPutKbn)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            // 明細データ格納用テーブル            
            this.ChouhyouDataTable = new DataTable();

            this.outPutKbn = outPutKbn;
            // 明細データTABLEカラムセット
            if (this.outPutKbn == 1)
            {
                this.ChouhyouDataTable.Columns.Add("DTL_GYOUSHA_CD_CTL");
                this.ChouhyouDataTable.Columns.Add("DTL_GYOUSHA_NAME_CTL");
                this.ChouhyouDataTable.Columns.Add("DTL_GENBA_CD_CTL");
                this.ChouhyouDataTable.Columns.Add("DTL_GENBA_NAME_CTL");
            }
            this.ChouhyouDataTable.Columns.Add("DTL_ZAIKO_HINMEI_CD_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_ZAIKO_HINMEI_NAME_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_ZAIKO_RYOU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_ZAIKO_KINGAKU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_UKEIRE_RYOU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_SHUKKA_RYOU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_IDOU_CHOUSEI_RYOU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_TOUGETU_ZAIKO_RYOU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_TOUGETU_ZAIKO_KINGAKU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_GOUKEI_ZAIKO_RYOU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_GOUKEI_ZAIKO_KINGAKU_CTL");
            this.ChouhyouDataTable.Columns.Add("DTL_ZAIKO_TANKA_CTL");

            DataRow row;

            // 明細データ件数LOOP処理
            for (int i = 0; i < this.detail.Count; i++)
            {
                // 明細データTABLE行
                row = this.ChouhyouDataTable.NewRow();

                // 明細データ値セット
                if (this.outPutKbn == 1)
                {
                    row["DTL_GYOUSHA_CD_CTL"] = this.detail[i].GYOUSHA_CD;
                    row["DTL_GYOUSHA_NAME_CTL"] = this.detail[i].GYOUSHA_NAME;
                    row["DTL_GENBA_CD_CTL"] = this.detail[i].GENBA_CD;
                    row["DTL_GENBA_NAME_CTL"] = this.detail[i].GENBA_NAME;
                }
                row["DTL_ZAIKO_HINMEI_CD_CTL"] = this.detail[i].ZAIKO_HINMEI_CD;
                row["DTL_ZAIKO_HINMEI_NAME_CTL"] = this.detail[i].ZAIKO_HINMEI_NAME;
                row["DTL_ZAIKO_RYOU_CTL"] = this.detail[i].PRE_ZAIKO_RYOU;
                row["DTL_ZAIKO_KINGAKU_CTL"] = this.detail[i].PRE_ZAIKO_KINGAKU;
                row["DTL_UKEIRE_RYOU_CTL"] = this.detail[i].UKEIRE_RYOU;
                row["DTL_SHUKKA_RYOU_CTL"] = this.detail[i].SHUKKA_RYOU;
                row["DTL_IDOU_CHOUSEI_RYOU_CTL"] = this.detail[i].IDOU_CHOUSEI_RYOU;
                row["DTL_TOUGETU_ZAIKO_RYOU_CTL"] = this.detail[i].NOW_ZAIKO_RYOU;
                row["DTL_TOUGETU_ZAIKO_KINGAKU_CTL"] = this.detail[i].NOW_ZAIKO_KINGAKU;
                row["DTL_GOUKEI_ZAIKO_RYOU_CTL"] = this.detail[i].TOTAL_ZAIKO_RYOU;
                row["DTL_GOUKEI_ZAIKO_KINGAKU_CTL"] = this.detail[i].TOTAL_ZAIKO_KINGAKU;
                row["DTL_ZAIKO_TANKA_CTL"] = this.detail[i].ZAIKO_TANKA;

                // 明細データTABLEに行追加
                this.ChouhyouDataTable.Rows.Add(row);
            }

            // 明細データTABLEをセット
            this.SetRecord(this.ChouhyouDataTable);

            // データテーブル情報から帳票情報作成処理を実行する
            if (this.outPutKbn == 1)
            {
                this.Create(this.OutputFormFullPathName1, this.OutputFormLayout, ChouhyouDataTable);
            }
            else
            {
                this.Create(this.OutputFormFullPathName2, this.OutputFormLayout, ChouhyouDataTable);
            }
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            // ヘッダ部
            // 会社略称
            this.SetFieldName("FH_CORP_NAME_VLB", this.corpRyakuName);
            // 条件-伝票日付
            this.SetFieldName("FH_DENPYOU_DATE_CTL", this.dateFrom + " ～ " + this.dateTo);
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime().ToString("yyyy/MM/dd H:mm:ss"));
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            if (this.outPutKbn == 1)
            {
                // 条件-業者
                if (!string.IsNullOrWhiteSpace(this.gyoushaCdFrom) || !string.IsNullOrWhiteSpace(this.gyoushaCdTo))
                {
                    this.SetFieldName("FH_GYOUSHA_CTL", this.gyoushaCdFrom + " " + this.gyoushaNameFrom + " ～ " + this.gyoushaCdTo + " " + this.gyoushaNameTo);
                }
                else
                {
                    this.SetFieldName("FH_GYOUSHA_CTL", "");
                }
                // 条件-現場
                if (!string.IsNullOrWhiteSpace(this.genbaCdFrom) || !string.IsNullOrWhiteSpace(this.genbaCdTo))
                {
                    this.SetFieldName("FH_GENBA_CTL", this.genbaCdFrom + " " + this.genbaNameFrom + " ～ " + this.genbaCdTo + " " + this.genbaNameTo);
                }
                else
                {
                    this.SetFieldName("FH_GENBA_CTL", "");
                }
            }
            // 条件-在庫品名
            if (!string.IsNullOrWhiteSpace(this.zaikoHinmeiCdFrom) || !string.IsNullOrWhiteSpace(this.zaikoHinmeiCdTo))
            {
                this.SetFieldName("FH_ZAIKO_HINMEI_CTL", this.zaikoHinmeiCdFrom + " " + this.zaikoHinmeiNameFrom + " ～ " + this.zaikoHinmeiCdTo + " " + this.zaikoHinmeiNameTo);
            }
            else
            {
                this.SetFieldName("FH_ZAIKO_HINMEI_CTL", "");
            }
            // 条件-評価方法
            if (this.hyoukaHouhou == "1")
            {
                this.SetFieldName("FH_HYOUKA_HOUHOU_CTL", "在庫品名入力の在庫単価で計算");
            }
            else
            {
                this.SetFieldName("FH_HYOUKA_HOUHOU_CTL", "総平均法");
            }

            // フッタ部
            // 総合計-繰越在庫量
            this.SetFieldName("DTL_ZAIKO_RYOU_TOTAL_CTL", this.preZaikoRyou);
            // 総合計-繰越在庫金額
            this.SetFieldName("DTL_ZAIKO_KINGAKU_TOTAL_CTL", this.preZaikoKingaku);
            // 総合計-受入量
            this.SetFieldName("DTL_UKEIRE_RYOU_TOTAL_CTL", this.ukeireRyou);
            // 総合計-出荷量
            this.SetFieldName("DTL_SHUKKA_RYOU_TOTAL_CTL", this.shukkaRyou);
            // 総合計-移動/調整量
            this.SetFieldName("DTL_IDOU_CHOUSEI_RYOU_TOTAL_CTL", this.idouChouseiRyou);
            // 総合計-当月在庫量
            this.SetFieldName("DTL_TOUGETU_ZAIKO_RYOU_TOTAL_CTL", this.nowZaikoRyou);
            // 総合計-当月在庫金額
            this.SetFieldName("DTL_TOUGETU_ZAIKO_KINGAKU_TOTAL_CTL", this.nowZaikoKingaku);
            // 総合計-合計在庫量
            this.SetFieldName("DTL_GOUKEI_ZAIKO_RYOU_TOTAL_CTL", this.toTalZaikoRyou);
            // 総合計-合計在庫金額
            this.SetFieldName("DTL_GOUKEI_ZAIKO_KINGAKU_TOTAL_CTL", this.toTalZaikoKingaku);
        }

        /// <summary> 帳票データより、C1Reportに渡すデータを作成する </summary>
        /// <param name="dataTable">帳票データ</param>
        private void InputDataToMem(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                // row[0]を文字列に変換しtempに格納
                string tmp = row[0].ToString();

                // ","(ダブルコーテーション カンマ ダブルコーテーション)で区切って配列に格納
                string[] list = this.ReportSplit(tmp);

                // データ種類(list[0])により分岐
                if (list[0] == "1-1")
                {
                    // ヘッダ部
                    this.dateFrom = list[1];             // 伝票日付FROM
                    this.dateTo = list[2];               // 伝票日付TO
                    this.gyoushaCdFrom = list[3];        // 業者CDFROM
                    this.gyoushaCdTo = list[4];          // 業者CDTO
                    this.gyoushaNameFrom = list[5];      // 業者名称FROM
                    this.gyoushaNameTo = list[6];        // 業者名称TO
                    this.genbaCdFrom = list[7];          // 現場CDFROM
                    this.genbaCdTo = list[8];            // 現場CDTO
                    this.genbaNameFrom = list[9];        // 現場名称FROM
                    this.genbaNameTo = list[10];         // 現場名称TO
                    this.zaikoHinmeiCdFrom = list[11];   // 在庫品名CDFROM
                    this.zaikoHinmeiCdTo = list[12];     // 在庫品名CDTO
                    this.zaikoHinmeiNameFrom = list[13]; // 在庫品名名称FROM
                    this.zaikoHinmeiNameTo = list[14];   // 在庫品名名称TO
                    this.hyoukaHouhou = list[15];        // 評価方法
                    this.corpRyakuName = list[16];       // 会社略称
                }
                else if (list[0] == "2-1")
                {
                    // 明細部
                    this.detail.Add(new Detail(list));
                }
                else if (list[0] == "2-2")
                {
                    // 集計部
                    this.preZaikoRyou = list[1];        // 繰越在庫量
                    this.preZaikoKingaku = list[2];     // 繰越在庫金額
                    this.ukeireRyou = list[3];          // 受入量
                    this.shukkaRyou = list[4];          // 出荷量
                    this.idouChouseiRyou = list[5];     // 移動/調整量
                    this.nowZaikoRyou = list[6];        // 当月在庫量
                    this.nowZaikoKingaku = list[7];     // 当月在庫金額
                    this.toTalZaikoRyou = list[8];      // 合計在庫量
                    this.toTalZaikoKingaku = list[9];   // 合計在庫金額
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

        #endregion

        #region - Inner Class -

        /// <summary>帳票明細データを表すクラス・コントロール </summary>
        private class Detail
        {
            public Detail(string[] list)
            {
                this.GYOUSHA_CD = list[1];
                this.GYOUSHA_NAME = list[2];
                this.GENBA_CD = list[3];
                this.GENBA_NAME = list[4];
                this.ZAIKO_HINMEI_CD = list[5];
                this.ZAIKO_HINMEI_NAME = list[6];
                this.PRE_ZAIKO_RYOU = list[7];
                this.PRE_ZAIKO_KINGAKU = list[8];
                this.UKEIRE_RYOU = list[9];
                this.SHUKKA_RYOU = list[10];
                this.IDOU_CHOUSEI_RYOU = list[11];
                this.NOW_ZAIKO_RYOU = list[12];
                this.NOW_ZAIKO_KINGAKU = list[13];
                this.TOTAL_ZAIKO_RYOU = list[14];
                this.TOTAL_ZAIKO_KINGAKU = list[15];
                this.ZAIKO_TANKA = list[16];
            }

            /// <summary> 業者コードを保持するプロパティ </summary>
            public string GYOUSHA_CD { get; private set; }

            /// <summary> 業者名称を保持するプロパティ </summary>
            public string GYOUSHA_NAME { get; private set; }

            /// <summary> 現場コードを保持するプロパティ </summary>
            public string GENBA_CD { get; private set; }

            /// <summary> 現場名称を保持するプロパティ </summary>
            public string GENBA_NAME { get; private set; }

            /// <summary> 在庫品名コードを保持するプロパティ </summary>
            public string ZAIKO_HINMEI_CD { get; private set; }

            /// <summary> 在庫品名名称を保持するプロパティ </summary>
            public string ZAIKO_HINMEI_NAME { get; private set; }

            /// <summary> 繰越在庫量を保持するプロパティ </summary>            
            public string PRE_ZAIKO_RYOU { get; private set; }

            /// <summary> 繰越在庫金額を保持するプロパティ </summary>
            public string PRE_ZAIKO_KINGAKU { get; private set; }

            /// <summary> 受入量を保持するプロパティ </summary>
            public string UKEIRE_RYOU { get; private set; }

            /// <summary> 出荷量を保持するプロパティ </summary>
            public string SHUKKA_RYOU { get; private set; }

            /// <summary> 移動/調整量を保持するプロパティ </summary>
            public string IDOU_CHOUSEI_RYOU { get; private set; }

            /// <summary> 当月在庫量を保持するプロパティ </summary>
            public string NOW_ZAIKO_RYOU { get; private set; }

            /// <summary> 当月在庫金額を保持するプロパティ </summary>
            public string NOW_ZAIKO_KINGAKU { get; private set; }

            /// <summary> 合計在庫量を保持するプロパティ </summary>
            public string TOTAL_ZAIKO_RYOU { get; private set; }

            /// <summary> 合計在庫金額を保持するプロパティ </summary>
            public string TOTAL_ZAIKO_KINGAKU { get; private set; }

            /// <summary> 在庫単価を保持するプロパティ </summary>
            public string ZAIKO_TANKA { get; private set; }
        }

        #endregion

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

    #endregion
}
