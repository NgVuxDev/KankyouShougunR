using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;

namespace Shougun.Core.PaymentManagement.KaikakekinItiranHyo
{
    #region - Class -

    /// <summary> R365(買掛金一覧表)帳票を表すクラス・コントロール </summary>
    public class ReportInfoR365 : ReportInfoBase
    {
        #region - Fields -

        // 帳票用データテーブル
        private DataTable chouhyouDataTable = new DataTable();

        // ヘッダー部
        private string corpRyakuName = string.Empty;                // 会社略称
        private string denpyouHidukeFrom = string.Empty;            // 伝票日付開始
        private string denpyouHidukeTo = string.Empty;              // 伝票日付終了
        private string torihikiSakiFrom = string.Empty;             // 取引先開始
        private string torihikiSakiFromName = string.Empty;         // 取引先名称開始
        private string torihikiSakiTo = string.Empty;               // 取引先終了
        private string torihikiSakiToName = string.Empty;           // 取引先名称終了
        private string taitoru = "買掛金一覧表";                    // タイトル

        // 明細部カラムヘッダ
        private string torisakiCD = "取引先CD";                     // 取引先CD
        private string torisakiNameRyaku = "取引先名";              // 取引先名
        private string kurikoshiZandaka = "繰越残高";               // 繰越残高
        private string shukkinGaku = "出金額";                      // 出金額
        private string zeiNukiShiharai = "税抜支払金額";                // 税抜支払金額
        private string shouhiZei = "消費税";                        // 消費税
        private string zeiKomiShiharai = "税込支払金額";                // 税込支払金額
        private string sashihikiShiharaiZandaka = "差引残高";   // 差引残高
        private string shimeBi = "締日";                            // 締日
        // 明細部データ
        private List<Detail> detail = new List<Detail>();           // 明細部データ

        // 合計部
        private string kurikosiGoukei = string.Empty;               // 総合計：繰越残高
        private string shukkinGoukei = string.Empty;                // 総合計：出金額
        private string zeinukiharaiGoukei = string.Empty;           // 総合計：税抜支払
        private string shouhizeiGoukei = string.Empty;              // 総合計：消費税
        private string zeikomiharaiGoukei = string.Empty;           // 総合計：税込支払
        private string sasihizanGoukei = string.Empty;              // 総合計：差引支払残高

        #endregion

        #region - Constructors -

        /// <summary> Initializes a new instance of the <see cref="ReportInfoR365" /> class. </summary>
        public ReportInfoR365()
        {
            // パスとレイアウトのデフォルト値を設定しておく

            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "./Template/R365-Form.xml";

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
        public void R365_Reprt(DataTable chouhyouData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // 明細データ格納用テーブル            
            this.ChouhyouDataTable = new DataTable();

            // 明細データTABLEカラムセット
            this.ChouhyouDataTable.Columns.Add("PHY_TORIHIKISAKI_CD_VLB");                  // 取引先CD
            this.ChouhyouDataTable.Columns.Add("PHY_TORIHIKISAKI_NAME_RYAKU_VLB");          // 取引先名
            this.ChouhyouDataTable.Columns.Add("PHY_KURIKOSI_ZANDAKA_VLB");                 // 繰越残高
            this.ChouhyouDataTable.Columns.Add("PHY_NYUUKIN_GAKU_VLB");                     // 出金額
            this.ChouhyouDataTable.Columns.Add("PHY_ZEINUKI_URIAGE_VLB");                   // 税抜支払
            this.ChouhyouDataTable.Columns.Add("PHY_SHOUHIZEI_VLB");                        // 消費税
            this.ChouhyouDataTable.Columns.Add("PHY_ZEIKOMI_URIAGE_VLB");                   // 税込支払
            this.ChouhyouDataTable.Columns.Add("PHY_SASHIHIKI_URIAGE_ZANDAKA_VLB");         // 差引支払残高
            this.ChouhyouDataTable.Columns.Add("PHN_SHIMEBI1_VLB");                         // 締日1
            this.ChouhyouDataTable.Columns.Add("PHN_SHIMEBI2_VLB");                         // 締日2
            this.ChouhyouDataTable.Columns.Add("PHN_SHIMEBI3_VLB");                         // 締日3

            DataRow row;

            // 明細データ件数LOOP処理
            for (int i = 0; i < this.detail.Count; i++)
            {
                // 明細データTABLE行
                row = this.ChouhyouDataTable.NewRow();

                // 明細データ値セット
                row["PHY_TORIHIKISAKI_CD_VLB"] = this.detail[i].TorihikisakCD;                       // 取引先CD
                row["PHY_TORIHIKISAKI_NAME_RYAKU_VLB"] = this.detail[i].TorihikisakMei;              // 取引先名
                row["PHY_KURIKOSI_ZANDAKA_VLB"] = this.detail[i].KurikosiZndaka;                     // 繰越残高
                row["PHY_NYUUKIN_GAKU_VLB"] = this.detail[i].ShukkinGaku;                            // 出金額
                row["PHY_ZEINUKI_URIAGE_VLB"] = this.detail[i].ZeinukiShiharai;                      // 税抜支払
                row["PHY_SHOUHIZEI_VLB"] = this.detail[i].ShouhiZei;                                 // 消費税                
                row["PHY_ZEIKOMI_URIAGE_VLB"] = this.detail[i].ZeikomiShiharai;                      // 税込支払
                row["PHY_SASHIHIKI_URIAGE_ZANDAKA_VLB"] = this.detail[i].SashihikiShiharaiZandaka;   // 差引支払残高
                row["PHN_SHIMEBI1_VLB"] = this.detail[i].Shimebi1;                                   // 締日1
                row["PHN_SHIMEBI2_VLB"] = this.detail[i].Shimebi2;                                   // 締日2
                row["PHN_SHIMEBI3_VLB"] = this.detail[i].Shimebi3;                                   // 締日3

                // 明細データTABLEに行追加
                this.ChouhyouDataTable.Rows.Add(row);
            }

            // 明細データTABLEをセット
            this.SetRecord(this.ChouhyouDataTable);

            // データテーブル情報から帳票情報作成処理を実行する
            this.Create(this.OutputFormFullPathName, this.OutputFormLayout, dataTableChouhyouForm);
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        protected override void UpdateFieldsStatus()
        {
            // ヘッダ部
            this.SetFieldName("FH_CORP_RYAKU_NAME_VLB", this.corpRyakuName);                                    // 会社略称
            this.SetFieldName("FH_TITLE_VLB", this.taitoru);                                                    // タイトル
            this.SetFieldName("FH_DENPYOU_DATE_CTL", this.denpyouHidukeFrom + " ～ " + this.denpyouHidukeTo);   // 条件-伝票日付
            this.SetFieldName("FH_TORIHIKISAKI_CTL", this.torihikiSakiFrom + " " + this.torihikiSakiFromName + " ～ " + this.torihikiSakiTo + " " + this.torihikiSakiToName);     // 条件-取引
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.SetFieldName("FH_PRINT_DATE_VLB", this.getDBDateTime().ToString("yyyy/MM/dd H:mm:ss"));
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end


            // 明細部
            // カラムヘッダ
            this.SetFieldName("PHY_TORIHIKISAKI_CD_VLB", this.torisakiCD);                          // 取引先CD
            this.SetFieldName("PHY_TORIHIKISAKI_NAME_RYAKU_VLB", this.torisakiNameRyaku);           // 取引先名
            this.SetFieldName("PHY_KURIKOSI_ZANDAKA_VLB", this.kurikoshiZandaka);                   // 繰越残高
            this.SetFieldName("PHY_NYUUKIN_GAKU_VLB", this.shukkinGaku);                            // 出金額
            this.SetFieldName("PHY_ZEINUKI_URIAGE_VLB", this.zeiNukiShiharai);                      // 税抜支払
            this.SetFieldName("PHY_SHOUHIZEI_VLB", this.shouhiZei);                                 // 消費税
            this.SetFieldName("PHY_ZEIKOMI_URIAGE_VLB", this.zeiKomiShiharai);                      // 税込支払
            this.SetFieldName("PHY_SASHIHIKI_URIAGE_ZANDAKA_VLB", this.sashihikiShiharaiZandaka);   // 差引支払残高
            this.SetFieldName("PHY_SHIMEBI_VLB", this.shimeBi);                                     // 締日

            // 合計行部
            this.SetFieldName("G1F_KURIKOSI_ZANDAKA_TOTAL_CTL", this.kurikosiGoukei);            // 総合計：繰越残高
            this.SetFieldName("G1F_NYUUKIN_GAKU_TOTAL_CTL", this.shukkinGoukei);                 // 総合計：出金額
            this.SetFieldName("G1F_ZEINUKI_URIAGE_TOTAL_CTL", this.zeinukiharaiGoukei);          // 総合計：税抜支払
            this.SetFieldName("G1F_SHOUHIZEI_TOTAL_CTL", this.shouhizeiGoukei);                  // 総合計：消費税
            this.SetFieldName("G1F_ZEIKOMI_URIAGE_TOTAL_CTL", this.zeikomiharaiGoukei);          // 総合計：税込支払
            this.SetFieldName("G1F_SASHIHIKI_URIAGE_ZANDAKA_TOTAL_CTL", this.sasihizanGoukei);   // 総合計：差引支払残高
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
                    this.denpyouHidukeFrom = list[1];       // 伝票日付FROM
                    this.denpyouHidukeTo = list[2];         // 伝票日付TO
                    this.torihikiSakiFrom = list[3];        // 取引先FROM
                    this.torihikiSakiTo = list[4];          // 取引先TO
                    this.torihikiSakiFromName = list[5];    // 取引先名称FROM
                    this.torihikiSakiToName = list[6];      // 取引先名称TO
                    this.corpRyakuName = list[7];           // 会社略称
                }
                else if (list[0] == "2-1")
                {
                    // 明細部
                    this.detail.Add(new Detail(list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10], list[11]));
                }
                else if (list[0] == "2-2")
                {
                    // 集計部
                    this.kurikosiGoukei = list[1];       // 繰越残高
                    this.shukkinGoukei = list[2];        // 出金額
                    this.zeinukiharaiGoukei = list[3];   // 税抜支払
                    this.shouhizeiGoukei = list[4];      // 消費税
                    this.zeikomiharaiGoukei = list[5];   // 税込支払
                    this.sasihizanGoukei = list[6];      // 差引支払残高
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
            /// <summary> Initializes a new instance of the <see cref="Detail" /> class. </summary>
            /// <param name="torihikisakCD">取引先コードを表す文字列</param>
            /// <param name="torihikisakMei">取引先名称を表す文字列</param>
            /// <param name="kurikosiZndaka">繰越残高を表す文字列</param>
            /// <param name="shukkinGaku">出金額を表す文字列</param>
            /// <param name="zeinukiShiharai">税抜支払を表す文字列</param>
            /// <param name="shouhiZei">消費税を表す文字列</param>
            /// <param name="zeikomiShiharai">税込支払を表す文字列</param>
            /// <param name="sashihikiShiharaiZandaka">差引支払残高を表す文字列</param>
            /// <param name="shimebi1">締日1を表す文字列</param>
            /// <param name="shimebi2">締日2を表す文字列</param>
            /// <param name="shimebi3">締日3を表す文字列</param>
            public Detail(string torihikisakCD, string torihikisakMei, string kurikosiZndaka, string shukkinGaku, string zeinukiShiharai, string shouhiZei, string zeikomiShiharai, string sashihikiShiharaiZandaka, string shimebi1, string shimebi2, string shimebi3)
            {
                this.TorihikisakCD = torihikisakCD;                         // 取引先コード
                this.TorihikisakMei = torihikisakMei;                       // 取引先名称
                this.KurikosiZndaka = kurikosiZndaka;                       // 繰越残高
                this.ShukkinGaku = shukkinGaku;                             // 出金額
                this.ZeinukiShiharai = zeinukiShiharai;                     // 税抜支払
                this.ShouhiZei = shouhiZei;                                 // 消費税
                this.ZeikomiShiharai = zeikomiShiharai;                     // 税込支払
                this.SashihikiShiharaiZandaka = sashihikiShiharaiZandaka;   // 差引支払残高
                this.Shimebi1 = shimebi1;                                   // 締日1
                this.Shimebi2 = shimebi2;                                   // 締日2
                this.Shimebi3 = shimebi3;                                   // 締日3
            }

            /// <summary> 取引先コードを保持するプロパティ </summary>
            public string TorihikisakCD { get; private set; }

            /// <summary> 取引先名称を保持するプロパティ </summary>
            public string TorihikisakMei { get; private set; }

            /// <summary> 繰越残高を保持するプロパティ </summary>            
            public string KurikosiZndaka { get; private set; }

            /// <summary> 出金額を保持するプロパティ </summary>
            public string ShukkinGaku { get; private set; }

            /// <summary> 税抜支払を保持するプロパティ </summary>
            public string ZeinukiShiharai { get; private set; }

            /// <summary> 消費税を保持するプロパティ </summary>
            public string ShouhiZei { get; private set; }

            /// <summary> 税込支払を保持するプロパティ </summary>
            public string ZeikomiShiharai { get; private set; }

            /// <summary> 差引支払残高を保持するプロパティ </summary>
            public string SashihikiShiharaiZandaka { get; private set; }

            /// <summary> 締日1を保持するプロパティ </summary>
            public string Shimebi1 { get; private set; }

            /// <summary> 締日2を保持するプロパティ </summary>
            public string Shimebi2 { get; private set; }

            /// <summary> 締日3を保持するプロパティ </summary>
            public string Shimebi3 { get; private set; }
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
