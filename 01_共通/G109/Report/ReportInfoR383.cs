using System;
using System.Collections.Generic;
using System.Data;
using CommonChouhyouPopup.App;

namespace Shougun.Core.Billing.AtenaLabel
{
    /// <summary> R383(宛名ラベル)を表すクラス・コントロール </summary>
    public class ReportInfoR383 : ReportInfoBase
    {
        // <summary>帳票用データテーブルを保持するプロパティ</summary>
        private DataTable chouhyouDataTable = new DataTable();
        // <summary>List<T>クラスの（Detail型）のリストとしてインスタンス</summary>
        private List<Detail> detail = new List<Detail>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportInfoR472"/> class.
        /// </summary>
        public ReportInfoR383()
        {
            // 帳票出力フルパスフォーム名
            this.OutputFormFullPathName = "./Template/R383-Form.xml";

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
        public void R383_Report(DataTable chouhyouData)
        {
            // 引数の帳票データより、C1Reportに渡すデータを作成する
            this.InputDataToMem(chouhyouData);

            DataTable dataTableChouhyouForm = new DataTable();

            // フォームへ設定する情報処理
            // データテーブル作成処理のみ
            this.chouhyouDataTable = new DataTable();

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
            // カラムヘッダ部
            this.SetFieldName("PHY_SOUFU_POST_1_CTL", this.detail[0].Post_1);          // 郵便番号
            this.SetFieldName("PHY_SOUFU_ADDRESS1_1_CTL", this.detail[0].Address1_1);  // 住所1
            this.SetFieldName("PHY_SOUFU_ADDRESS2_1_CTL", this.detail[0].Address2_1);  // 住所2
            this.SetFieldName("PHY_SOUFU_NAME1_1_CTL", this.detail[0].Name1_1);        // 名称1
            this.SetFieldName("PHY_SOUFU_KEISHOU1_1_CTL", this.detail[0].Keishou1_1);  // 敬称1
            this.SetFieldName("PHY_SOUFU_NAME2_1_CTL", this.detail[0].Name2_1);        // 名称2
            this.SetFieldName("PHY_SOUFU_KEISHOU2_1_CTL", this.detail[0].Keishou2_1);  // 敬称2
            this.SetFieldName("PHY_SOUFU_BUSHO_1_CTL", this.detail[0].Busho_1);        // 部署
            this.SetFieldName("PHY_SOUFU_TANTOU_1_CTL", this.detail[0].Tantou_1);      // 担当者
            this.SetFieldName("PHY_SOUFU_POST_2_CTL", this.detail[0].Post_2);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_2_CTL", this.detail[0].Address1_2);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_2_CTL", this.detail[0].Address2_2);
            this.SetFieldName("PHY_SOUFU_NAME1_2_CTL", this.detail[0].Name1_2);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_2_CTL", this.detail[0].Keishou1_2);
            this.SetFieldName("PHY_SOUFU_NAME2_2_CTL", this.detail[0].Name2_2);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_2_CTL", this.detail[0].Keishou2_2);
            this.SetFieldName("PHY_SOUFU_BUSHO_2_CTL", this.detail[0].Busho_2);
            this.SetFieldName("PHY_SOUFU_TANTOU_2_CTL", this.detail[0].Tantou_2);
            this.SetFieldName("PHY_SOUFU_POST_3_CTL", this.detail[0].Post_3);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_3_CTL", this.detail[0].Address1_3);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_3_CTL", this.detail[0].Address2_3);
            this.SetFieldName("PHY_SOUFU_NAME1_3_CTL", this.detail[0].Name1_3);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_3_CTL", this.detail[0].Keishou1_3);
            this.SetFieldName("PHY_SOUFU_NAME2_3_CTL", this.detail[0].Name2_3);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_3_CTL", this.detail[0].Keishou2_3);
            this.SetFieldName("PHY_SOUFU_BUSHO_3_CTL", this.detail[0].Busho_3);
            this.SetFieldName("PHY_SOUFU_TANTOU_3_CTL", this.detail[0].Tantou_3);
            this.SetFieldName("PHY_SOUFU_POST_4_CTL", this.detail[0].Post_4);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_4_CTL", this.detail[0].Address1_4);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_4_CTL", this.detail[0].Address2_4);
            this.SetFieldName("PHY_SOUFU_NAME1_4_CTL", this.detail[0].Name1_4);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_4_CTL", this.detail[0].Keishou1_4);
            this.SetFieldName("PHY_SOUFU_NAME2_4_CTL", this.detail[0].Name2_4);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_4_CTL", this.detail[0].Keishou2_4);
            this.SetFieldName("PHY_SOUFU_BUSHO_4_CTL", this.detail[0].Busho_4);
            this.SetFieldName("PHY_SOUFU_TANTOU_4_CTL", this.detail[0].Tantou_4);
            this.SetFieldName("PHY_SOUFU_POST_5_CTL", this.detail[0].Post_5);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_5_CTL", this.detail[0].Address1_5);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_5_CTL", this.detail[0].Address2_5);
            this.SetFieldName("PHY_SOUFU_NAME1_5_CTL", this.detail[0].Name1_5);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_5_CTL", this.detail[0].Keishou1_5);
            this.SetFieldName("PHY_SOUFU_NAME2_5_CTL", this.detail[0].Name2_5);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_5_CTL", this.detail[0].Keishou2_5);
            this.SetFieldName("PHY_SOUFU_BUSHO_5_CTL", this.detail[0].Busho_5);
            this.SetFieldName("PHY_SOUFU_TANTOU_5_CTL", this.detail[0].Tantou_5);
            this.SetFieldName("PHY_SOUFU_POST_6_CTL", this.detail[0].Post_6);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_6_CTL", this.detail[0].Address1_6);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_6_CTL", this.detail[0].Address2_6);
            this.SetFieldName("PHY_SOUFU_NAME1_6_CTL", this.detail[0].Name1_6);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_6_CTL", this.detail[0].Keishou1_6);
            this.SetFieldName("PHY_SOUFU_NAME2_6_CTL", this.detail[0].Name2_6);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_6_CTL", this.detail[0].Keishou2_6);
            this.SetFieldName("PHY_SOUFU_BUSHO_6_CTL", this.detail[0].Busho_6);
            this.SetFieldName("PHY_SOUFU_TANTOU_6_CTL", this.detail[0].Tantou_6);
            this.SetFieldName("PHY_SOUFU_POST_7_CTL", this.detail[0].Post_7);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_7_CTL", this.detail[0].Address1_7);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_7_CTL", this.detail[0].Address2_7);
            this.SetFieldName("PHY_SOUFU_NAME1_7_CTL", this.detail[0].Name1_7);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_7_CTL", this.detail[0].Keishou1_7);
            this.SetFieldName("PHY_SOUFU_NAME2_7_CTL", this.detail[0].Name2_7);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_7_CTL", this.detail[0].Keishou2_7);
            this.SetFieldName("PHY_SOUFU_BUSHO_7_CTL", this.detail[0].Busho_7);
            this.SetFieldName("PHY_SOUFU_TANTOU_7_CTL", this.detail[0].Tantou_7);
            this.SetFieldName("PHY_SOUFU_POST_8_CTL", this.detail[0].Post_8);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_8_CTL", this.detail[0].Address1_8);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_8_CTL", this.detail[0].Address2_8);
            this.SetFieldName("PHY_SOUFU_NAME1_8_CTL", this.detail[0].Name1_8);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_8_CTL", this.detail[0].Keishou1_8);
            this.SetFieldName("PHY_SOUFU_NAME2_8_CTL", this.detail[0].Name2_8);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_8_CTL", this.detail[0].Keishou2_8);
            this.SetFieldName("PHY_SOUFU_BUSHO_8_CTL", this.detail[0].Busho_8);
            this.SetFieldName("PHY_SOUFU_TANTOU_8_CTL", this.detail[0].Tantou_8);
            this.SetFieldName("PHY_SOUFU_POST_9_CTL", this.detail[0].Post_9);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_9_CTL", this.detail[0].Address1_9);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_9_CTL", this.detail[0].Address2_9);
            this.SetFieldName("PHY_SOUFU_NAME1_9_CTL", this.detail[0].Name1_9);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_9_CTL", this.detail[0].Keishou1_9);
            this.SetFieldName("PHY_SOUFU_NAME2_9_CTL", this.detail[0].Name2_9);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_9_CTL", this.detail[0].Keishou2_9);
            this.SetFieldName("PHY_SOUFU_BUSHO_9_CTL", this.detail[0].Busho_9);
            this.SetFieldName("PHY_SOUFU_TANTOU_9_CTL", this.detail[0].Tantou_9);
            this.SetFieldName("PHY_SOUFU_POST_10_CTL", this.detail[0].Post_10);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_10_CTL", this.detail[0].Address1_10);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_10_CTL", this.detail[0].Address2_10);
            this.SetFieldName("PHY_SOUFU_NAME1_10_CTL", this.detail[0].Name1_10);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_10_CTL", this.detail[0].Keishou1_10);
            this.SetFieldName("PHY_SOUFU_NAME2_10_CTL", this.detail[0].Name2_10);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_10_CTL", this.detail[0].Keishou2_10);
            this.SetFieldName("PHY_SOUFU_BUSHO_10_CTL", this.detail[0].Busho_10);
            this.SetFieldName("PHY_SOUFU_TANTOU_10_CTL", this.detail[0].Tantou_10);
            this.SetFieldName("PHY_SOUFU_POST_11_CTL", this.detail[0].Post_11);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_11_CTL", this.detail[0].Address1_11);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_11_CTL", this.detail[0].Address2_11);
            this.SetFieldName("PHY_SOUFU_NAME1_11_CTL", this.detail[0].Name1_11);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_11_CTL", this.detail[0].Keishou1_11);
            this.SetFieldName("PHY_SOUFU_NAME2_11_CTL", this.detail[0].Name2_11);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_11_CTL", this.detail[0].Keishou2_11);
            this.SetFieldName("PHY_SOUFU_BUSHO_11_CTL", this.detail[0].Busho_11);
            this.SetFieldName("PHY_SOUFU_TANTOU_11_CTL", this.detail[0].Tantou_11);
            this.SetFieldName("PHY_SOUFU_POST_12_CTL", this.detail[0].Post_12);
            this.SetFieldName("PHY_SOUFU_ADDRESS1_12_CTL", this.detail[0].Address1_12);
            this.SetFieldName("PHY_SOUFU_ADDRESS2_12_CTL", this.detail[0].Address2_12);
            this.SetFieldName("PHY_SOUFU_NAME1_12_CTL", this.detail[0].Name1_12);
            this.SetFieldName("PHY_SOUFU_KEISHOU1_12_CTL", this.detail[0].Keishou1_12);
            this.SetFieldName("PHY_SOUFU_NAME2_12_CTL", this.detail[0].Name2_12);
            this.SetFieldName("PHY_SOUFU_KEISHOU2_12_CTL", this.detail[0].Keishou2_12);
            this.SetFieldName("PHY_SOUFU_BUSHO_12_CTL", this.detail[0].Busho_12);
            this.SetFieldName("PHY_SOUFU_TANTOU_12_CTL", this.detail[0].Tantou_12);
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
            this.SetFieldName("PHY_SOUFU_HYO_1_CTL", this.detail[0].Hyo1);
            this.SetFieldName("PHY_SOUFU_HYO_2_CTL", this.detail[0].Hyo2);
            this.SetFieldName("PHY_SOUFU_HYO_3_CTL", this.detail[0].Hyo3);
            this.SetFieldName("PHY_SOUFU_HYO_4_CTL", this.detail[0].Hyo4);
            this.SetFieldName("PHY_SOUFU_HYO_5_CTL", this.detail[0].Hyo5);
            this.SetFieldName("PHY_SOUFU_HYO_6_CTL", this.detail[0].Hyo6);
            this.SetFieldName("PHY_SOUFU_HYO_7_CTL", this.detail[0].Hyo7);
            this.SetFieldName("PHY_SOUFU_HYO_8_CTL", this.detail[0].Hyo8);
            this.SetFieldName("PHY_SOUFU_HYO_9_CTL", this.detail[0].Hyo9);
            this.SetFieldName("PHY_SOUFU_HYO_10_CTL", this.detail[0].Hyo10);
            this.SetFieldName("PHY_SOUFU_HYO_11_CTL", this.detail[0].Hyo11);
            this.SetFieldName("PHY_SOUFU_HYO_12_CTL", this.detail[0].Hyo12);
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
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
                this.detail.Add(new Detail(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9],
                    list[10], list[11], list[12], list[13], list[14], list[15], list[16], list[17], list[18],
                    list[19], list[20], list[21], list[22], list[23], list[24], list[25], list[26], list[27],
                    list[28], list[29], list[30], list[31], list[32], list[33], list[34], list[35], list[36],
                    list[37], list[38], list[39], list[40], list[41], list[42], list[43], list[44], list[45],
                    list[46], list[47], list[48], list[49], list[50], list[51], list[52], list[53], list[54],
                    list[55], list[56], list[57], list[58], list[59], list[60], list[61], list[62], list[63],
                    list[64], list[65], list[66], list[67], list[68], list[69], list[70], list[71], list[72],
                    list[73], list[74], list[75], list[76], list[77], list[78], list[79], list[80], list[81],
                    list[82], list[83], list[84], list[85], list[86], list[87], list[88], list[89], list[90],
                    list[91], list[92], list[93], list[94], list[95], list[96], list[97], list[98], list[99],
                    list[100], list[101], list[102], list[103], list[104], list[105], list[106], list[107], list[108]
                    // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                    , list[109], list[110], list[111], list[112], list[113], list[114], list[115], list[116], list[117]
                    , list[118], list[119], list[120]
                    // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
                    ));
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
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
            //public Detail(string hantei, string post_1, string address1_1, string address2_1, string name1_1, string keishou1_1, string name2_1, string keishou2_1, string busho_1, string tantou_1,
            //    string post_2, string address1_2, string address2_2, string name1_2, string keishou1_2, string name2_2, string keishou2_2, string busho_2, string tantou_2,
            //    string post_3, string address1_3, string address2_3, string name1_3, string keishou1_3, string name2_3, string keishou2_3, string busho_3, string tantou_3,
            //    string post_4, string address1_4, string address2_4, string name1_4, string keishou1_4, string name2_4, string keishou2_4, string busho_4, string tantou_4,
            //    string post_5, string address1_5, string address2_5, string name1_5, string keishou1_5, string name2_5, string keishou2_5, string busho_5, string tantou_5,
            //    string post_6, string address1_6, string address2_6, string name1_6, string keishou1_6, string name2_6, string keishou2_6, string busho_6, string tantou_6,
            //    string post_7, string address1_7, string address2_7, string name1_7, string keishou1_7, string name2_7, string keishou2_7, string busho_7, string tantou_7,
            //    string post_8, string address1_8, string address2_8, string name1_8, string keishou1_8, string name2_8, string keishou2_8, string busho_8, string tantou_8,
            //    string post_9, string address1_9, string address2_9, string name1_9, string keishou1_9, string name2_9, string keishou2_9, string busho_9, string tantou_9,
            //    string post_10, string address1_10, string address2_10, string name1_10, string keishou1_10, string name2_10, string keishou2_10, string busho_10, string tantou_10,
            //    string post_11, string address1_11, string address2_11, string name1_11, string keishou1_11, string name2_11, string keishou2_11, string busho_11, string tantou_11,
            //    string post_12, string address1_12, string address2_12, string name1_12, string keishou1_12, string name2_12, string keishou2_12, string busho_12, string tantou_12)
            public Detail(string hantei, string post_1, string address1_1, string address2_1, string name1_1, string keishou1_1, string name2_1, string keishou2_1, string busho_1, string tantou_1,string hyo1,
    string post_2, string address1_2, string address2_2, string name1_2, string keishou1_2, string name2_2, string keishou2_2, string busho_2, string tantou_2, string hyo2,
    string post_3, string address1_3, string address2_3, string name1_3, string keishou1_3, string name2_3, string keishou2_3, string busho_3, string tantou_3, string hyo3,
    string post_4, string address1_4, string address2_4, string name1_4, string keishou1_4, string name2_4, string keishou2_4, string busho_4, string tantou_4, string hyo4,
    string post_5, string address1_5, string address2_5, string name1_5, string keishou1_5, string name2_5, string keishou2_5, string busho_5, string tantou_5, string hyo5,
    string post_6, string address1_6, string address2_6, string name1_6, string keishou1_6, string name2_6, string keishou2_6, string busho_6, string tantou_6, string hyo6,
    string post_7, string address1_7, string address2_7, string name1_7, string keishou1_7, string name2_7, string keishou2_7, string busho_7, string tantou_7, string hyo7,
    string post_8, string address1_8, string address2_8, string name1_8, string keishou1_8, string name2_8, string keishou2_8, string busho_8, string tantou_8, string hyo8,
    string post_9, string address1_9, string address2_9, string name1_9, string keishou1_9, string name2_9, string keishou2_9, string busho_9, string tantou_9, string hyo9,
    string post_10, string address1_10, string address2_10, string name1_10, string keishou1_10, string name2_10, string keishou2_10, string busho_10, string tantou_10, string hyo10,
    string post_11, string address1_11, string address2_11, string name1_11, string keishou1_11, string name2_11, string keishou2_11, string busho_11, string tantou_11, string hyo11,
    string post_12, string address1_12, string address2_12, string name1_12, string keishou1_12, string name2_12, string keishou2_12, string busho_12, string tantou_12, string hyo12)
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
            {
                this.Hantei = hantei;
                this.Post_1 = post_1;
                this.Address1_1 = address1_1;
                this.Address2_1 = address2_1;
                this.Name1_1 = name1_1;
                this.Keishou1_1 = keishou1_1;
                this.Name2_1 = name2_1;
                this.Keishou2_1 = keishou2_1;
                this.Busho_1 = busho_1;
                this.Tantou_1 = tantou_1;
                if (!tantou_1.Trim().Equals(""))
                {
                    this.Tantou_1 = tantou_1 + " 様";
                }
                this.Post_2 = post_2;
                this.Address1_2 = address1_2;
                this.Address2_2 = address2_2;
                this.Name1_2 = name1_2;
                this.Keishou1_2 = keishou1_2;
                this.Name2_2 = name2_2;
                this.Keishou2_2 = keishou2_2;
                this.Busho_2 = busho_2;
                this.Tantou_2 = tantou_2;
                if (!tantou_2.Trim().Equals(""))
                {
                    this.Tantou_2 = tantou_2 + " 様";
                }
                this.Post_3 = post_3;
                this.Address1_3 = address1_3;
                this.Address2_3 = address2_3;
                this.Name1_3 = name1_3;
                this.Keishou1_3 = keishou1_3;
                this.Name2_3 = name2_3;
                this.Keishou2_3 = keishou2_3;
                this.Busho_3 = busho_3;
                this.Tantou_3 = tantou_3;
                if (!tantou_3.Trim().Equals(""))
                {
                    this.Tantou_3 = tantou_3 + " 様";
                }
                this.Post_4 = post_4;
                this.Address1_4 = address1_4;
                this.Address2_4 = address2_4;
                this.Name1_4 = name1_4;
                this.Keishou1_4 = keishou1_4;
                this.Name2_4 = name2_4;
                this.Keishou2_4 = keishou2_4;
                this.Busho_4 = busho_4;
                this.Tantou_4 = tantou_4;
                if (!tantou_4.Trim().Equals(""))
                {
                    this.Tantou_4 = tantou_4 + " 様";
                }
                this.Post_5 = post_5;
                this.Address1_5 = address1_5;
                this.Address2_5 = address2_5;
                this.Name1_5 = name1_5;
                this.Keishou1_5 = keishou1_5;
                this.Name2_5 = name2_5;
                this.Keishou2_5 = keishou2_5;
                this.Busho_5 = busho_5;
                this.Tantou_5 = tantou_5;
                if (!tantou_5.Trim().Equals(""))
                {
                    this.Tantou_5 = tantou_5 + " 様";
                }
                this.Post_6 = post_6;
                this.Address1_6 = address1_6;
                this.Address2_6 = address2_6;
                this.Name1_6 = name1_6;
                this.Keishou1_6 = keishou1_6;
                this.Name2_6 = name2_6;
                this.Keishou2_6 = keishou2_6;
                this.Busho_6 = busho_6;
                this.Tantou_6 = tantou_6;
                if (!tantou_6.Trim().Equals(""))
                {
                    this.Tantou_6 = tantou_6 + " 様";
                }
                this.Post_7 = post_7;
                this.Address1_7 = address1_7;
                this.Address2_7 = address2_7;
                this.Name1_7 = name1_7;
                this.Keishou1_7 = keishou1_7;
                this.Name2_7 = name2_7;
                this.Keishou2_7 = keishou2_7;
                this.Busho_7 = busho_7;
                this.Tantou_7 = tantou_7;
                if (!tantou_7.Trim().Equals(""))
                {
                    this.Tantou_7 = tantou_7 + " 様";
                }
                this.Post_8 = post_8;
                this.Address1_8 = address1_8;
                this.Address2_8 = address2_8;
                this.Name1_8 = name1_8;
                this.Keishou1_8 = keishou1_8;
                this.Name2_8 = name2_8;
                this.Keishou2_8 = keishou2_8;
                this.Busho_8 = busho_8;
                this.Tantou_8 = tantou_8;
                if (!tantou_8.Trim().Equals(""))
                {
                    this.Tantou_8 = tantou_8 + " 様";
                }
                this.Post_9 = post_9;
                this.Address1_9 = address1_9;
                this.Address2_9 = address2_9;
                this.Name1_9 = name1_9;
                this.Keishou1_9 = keishou1_9;
                this.Name2_9 = name2_9;
                this.Keishou2_9 = keishou2_9;
                this.Busho_9 = busho_9;
                this.Tantou_9 = tantou_9;
                if (!tantou_9.Trim().Equals(""))
                {
                    this.Tantou_9 = tantou_9 + " 様";
                }
                this.Post_10 = post_10;
                this.Address1_10 = address1_10;
                this.Address2_10 = address2_10;
                this.Name1_10 = name1_10;
                this.Keishou1_10 = keishou1_10;
                this.Name2_10 = name2_10;
                this.Keishou2_10 = keishou2_10;
                this.Busho_10 = busho_10;
                this.Tantou_10 = tantou_10;
                if (!tantou_10.Trim().Equals(""))
                {
                    this.Tantou_10 = tantou_10 + " 様";
                }
                this.Post_11 = post_11;
                this.Address1_11 = address1_11;
                this.Address2_11 = address2_11;
                this.Name1_11 = name1_11;
                this.Keishou1_11 = keishou1_11;
                this.Name2_11 = name2_11;
                this.Keishou2_11 = keishou2_11;
                this.Busho_11 = busho_11;
                this.Tantou_11 = tantou_11;
                if (!tantou_11.Trim().Equals(""))
                {
                    this.Tantou_11 = tantou_11 + " 様";
                }
                this.Post_12 = post_12;
                this.Address1_12 = address1_12;
                this.Address2_12 = address2_12;
                this.Name1_12 = name1_12;
                this.Keishou1_12 = keishou1_12;
                this.Name2_12 = name2_12;
                this.Keishou2_12 = keishou2_12;
                this.Busho_12 = busho_12;
                this.Tantou_12 = tantou_12;
                if (!tantou_12.Trim().Equals(""))
                {
                    this.Tantou_12 = tantou_12 + " 様";
                }
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
                // 20140627 d-sato この部分取り消し
                //this.Hyo1 = hyo1;
                //this.Hyo2 = hyo2;
                //this.Hyo3 = hyo3;
                //this.Hyo4 = hyo4;
                //this.Hyo5 = hyo5;
                //this.Hyo6 = hyo6;
                //this.Hyo7 = hyo7;
                //this.Hyo8 = hyo8;
                //this.Hyo9 = hyo9;
                //this.Hyo10 = hyo10;
                //this.Hyo11 = hyo11;
                //this.Hyo12 = hyo12;
                this.Hyo1 = string.Empty;
                this.Hyo2 = string.Empty;
                this.Hyo3 = string.Empty;
                this.Hyo4 = string.Empty;
                this.Hyo5 = string.Empty;
                this.Hyo6 = string.Empty;
                this.Hyo7 = string.Empty;
                this.Hyo8 = string.Empty;
                this.Hyo9 = string.Empty;
                this.Hyo10 = string.Empty;
                this.Hyo11 = string.Empty;
                this.Hyo12 = string.Empty;
                // 20140627 d-sato この部分取り消し
                // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end
            }

            public string Hantei { get; private set; }
            public string Post_1 { get; private set; }
            public string Address1_1 { get; private set; }
            public string Address2_1 { get; private set; }
            public string Name1_1 { get; private set; }
            public string Keishou1_1 { get; private set; }
            public string Name2_1 { get; private set; }
            public string Keishou2_1 { get; private set; }
            public string Busho_1 { get; private set; }
            public string Tantou_1 { get; private set; }
            public string Post_2 { get; private set; }
            public string Address1_2 { get; private set; }
            public string Address2_2 { get; private set; }
            public string Name1_2 { get; private set; }
            public string Keishou1_2 { get; private set; }
            public string Name2_2 { get; private set; }
            public string Keishou2_2 { get; private set; }
            public string Busho_2 { get; private set; }
            public string Tantou_2 { get; private set; }
            public string Post_3 { get; private set; }
            public string Address1_3 { get; private set; }
            public string Address2_3 { get; private set; }
            public string Name1_3 { get; private set; }
            public string Keishou1_3 { get; private set; }
            public string Name2_3 { get; private set; }
            public string Keishou2_3 { get; private set; }
            public string Busho_3 { get; private set; }
            public string Tantou_3 { get; private set; }
            public string Post_4 { get; private set; }
            public string Address1_4 { get; private set; }
            public string Address2_4 { get; private set; }
            public string Name1_4 { get; private set; }
            public string Keishou1_4 { get; private set; }
            public string Name2_4 { get; private set; }
            public string Keishou2_4 { get; private set; }
            public string Busho_4 { get; private set; }
            public string Tantou_4 { get; private set; }
            public string Post_5 { get; private set; }
            public string Address1_5 { get; private set; }
            public string Address2_5 { get; private set; }
            public string Name1_5 { get; private set; }
            public string Keishou1_5 { get; private set; }
            public string Name2_5 { get; private set; }
            public string Keishou2_5 { get; private set; }
            public string Busho_5 { get; private set; }
            public string Tantou_5 { get; private set; }
            public string Post_6 { get; private set; }
            public string Address1_6 { get; private set; }
            public string Address2_6 { get; private set; }
            public string Name1_6 { get; private set; }
            public string Keishou1_6 { get; private set; }
            public string Name2_6 { get; private set; }
            public string Keishou2_6 { get; private set; }
            public string Busho_6 { get; private set; }
            public string Tantou_6 { get; private set; }
            public string Post_7 { get; private set; }
            public string Address1_7 { get; private set; }
            public string Address2_7 { get; private set; }
            public string Name1_7 { get; private set; }
            public string Keishou1_7 { get; private set; }
            public string Name2_7 { get; private set; }
            public string Keishou2_7 { get; private set; }
            public string Busho_7 { get; private set; }
            public string Tantou_7 { get; private set; }
            public string Post_8 { get; private set; }
            public string Address1_8 { get; private set; }
            public string Address2_8 { get; private set; }
            public string Name1_8 { get; private set; }
            public string Keishou1_8 { get; private set; }
            public string Name2_8 { get; private set; }
            public string Keishou2_8 { get; private set; }
            public string Busho_8 { get; private set; }
            public string Tantou_8 { get; private set; }
            public string Post_9 { get; private set; }
            public string Address1_9 { get; private set; }
            public string Address2_9 { get; private set; }
            public string Name1_9 { get; private set; }
            public string Keishou1_9 { get; private set; }
            public string Name2_9 { get; private set; }
            public string Keishou2_9 { get; private set; }
            public string Busho_9 { get; private set; }
            public string Tantou_9 { get; private set; }
            public string Post_10 { get; private set; }
            public string Address1_10 { get; private set; }
            public string Address2_10 { get; private set; }
            public string Name1_10 { get; private set; }
            public string Keishou1_10 { get; private set; }
            public string Name2_10 { get; private set; }
            public string Keishou2_10 { get; private set; }
            public string Busho_10 { get; private set; }
            public string Tantou_10 { get; private set; }
            public string Post_11 { get; private set; }
            public string Address1_11 { get; private set; }
            public string Address2_11 { get; private set; }
            public string Name1_11 { get; private set; }
            public string Keishou1_11 { get; private set; }
            public string Name2_11 { get; private set; }
            public string Keishou2_11 { get; private set; }
            public string Busho_11 { get; private set; }
            public string Tantou_11 { get; private set; }
            public string Post_12 { get; private set; }
            public string Address1_12 { get; private set; }
            public string Address2_12 { get; private set; }
            public string Name1_12 { get; private set; }
            public string Keishou1_12 { get; private set; }
            public string Name2_12 { get; private set; }
            public string Keishou2_12 { get; private set; }
            public string Busho_12 { get; private set; }
            public string Tantou_12 { get; private set; }
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する start
            public string Hyo1 { get; private set; }
            public string Hyo2 { get; private set; }
            public string Hyo3 { get; private set; }
            public string Hyo4 { get; private set; }
            public string Hyo5 { get; private set; }
            public string Hyo6 { get; private set; }
            public string Hyo7 { get; private set; }
            public string Hyo8 { get; private set; }
            public string Hyo9 { get; private set; }
            public string Hyo10 { get; private set; }
            public string Hyo11 { get; private set; }
            public string Hyo12 { get; private set; }
            // 20140625 katen EV005021 現場入力にマニフェスト返送先タブが追加になったためそれに合わせてマニフェスト宛名ラベル条件指定画面も修正する end

        }
    }
}
