using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace FixChouhyou
{
    #region - Class -

    /// <summary>(R425・R508・R547・R548)見積書を表すクラス・コントロール</summary>
    public class ReportInfoR425_R508_R547_R548_Kyokusei : ReportInfoBase
    {
        #region - Fields -

        private const int ConstMaxDispDetailHRowCount = 10;           // 金額見積もり横のDetail1の最大表示行数
        private const int ConstMaxDispDetailVRowCount = 16;           // 金額見積もり縦のDetail1の最大表示行数
        private const int ConstMaxDispTankaDetailHRowCount = 12;      // 単価見積もり横のDetail1の最大表示行数
        private const int ConstMaxDispTankaDetailVRowCount = 20;      // 単価見積もり縦のDetail1の最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR425_R508_R547_R548_Kyokusei"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR425_R508_R547_R548_Kyokusei(WINDOW_ID windowID)
        {
            this.windowID = windowID;

            this.OutputType = OutputTypeDef.KingakuMitsumoriH;      // 金額見積り（横、R508）

            this.SetRecord(this.dataTable);
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>出力タイプに関する列挙型</summary>
        public enum OutputTypeDef
        {
            /// <summary>金額見積り（縦）</summary>
            KingakuMitsumoriV,

            /// <summary>金額見積り（横）</summary>
            KingakuMitsumoriH,

            /// <summary>単価見積り（縦）</summary>
            TankaMitsumoriV,

            /// <summary>単価見積り（縦）</summary>
            TankaMitsumoriH,
        }

        #endregion - Enums -

        #region - Properties -

        /// <summary>出力タイプを保持するプロパティ</summary>
        /// <remarks>
        /// 金額見積り（縦）: KingakuMitsumoriV,
        /// 金額見積り（横）: KingakuMitsumoriH,
        /// 単価見積り（縦）: TankaMitsumoriV,
        /// 単価見積り（縦）: TankaMitsumoriH,
        /// </remarks>
        public OutputTypeDef OutputType { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>サンプルデータの作成処理を実行する</summary>
        public void CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;

            for (int pageNo = 1; pageNo <= 3; pageNo++)
            {
                this.DataTablePageList[pageNo.ToString()] = new Dictionary<string, DataTable>();

                switch (this.OutputType)
                {
                    case OutputTypeDef.KingakuMitsumoriV:   // 金額見積り（縦）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE_FLB");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");
                        // 付帯工事
                        dataTableTmp.Columns.Add("FUTAIKOUJI");
                        // 紹介料
                        dataTableTmp.Columns.Add("SYOUKAI");
                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");

                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");
                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");
                        // 合計金額
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU");

                        rowTmp = dataTableTmp.NewRow();

                        // タイトル
                        rowTmp["TITLE_FLB"] = "a";
                        // 見積書番号
                        rowTmp["MITSUMORI_NUMBER"] = "b";
                        // 付帯工事
                        rowTmp["FUTAIKOUJI"] = "B";
                        // 紹介料
                        rowTmp["SYOUKAI"] = "BB";
                        // 見積日付
                        rowTmp["MITSUMORI_DATE"] = "c";
                        // 取引先名1(＋取引先敬称1)
                        rowTmp["TORIHIKISAKI_NAME1"] = "d";
                        // 取引先名2(＋取引先敬称2)
                        rowTmp["TORIHIKISAKI_NAME2"] = "e";
                        // 件名
                        rowTmp["KENMEI"] = "j";
                        // 見積項目名称1
                        rowTmp["MITSUMORI_KOUMOKU1"] = "k";
                        // 見積項目1
                        rowTmp["MITSUMORI_1"] = "l";
                        // 見積項目名称2
                        rowTmp["MITSUMORI_KOUMOKU2"] = "m";
                        // 見積項目2
                        rowTmp["MITSUMORI_2"] = "n";
                        // 見積項目名称3
                        rowTmp["MITSUMORI_KOUMOKU3"] = "o";
                        // 見積項目3
                        rowTmp["MITSUMORI_3"] = "p";
                        // 見積項目名称4
                        rowTmp["MITSUMORI_KOUMOKU4"] = "q";
                        // 見積項目4
                        rowTmp["MITSUMORI_4"] = "r";
                        // 会社名
                        rowTmp["CORP_NAME"] = "s";
                        // 代表者
                        rowTmp["CORP_DAIHYOU"] = "t";
                        // 印字拠点名1
                        rowTmp["KYOTEN_NAME_1"] = "u";
                        // 印字拠点郵便番号1
                        rowTmp["KYOTEN_POST_1"] = "v";
                        // 印字拠点住所1_1
                        rowTmp["KYOTEN_ADDRESS1_1"] = "w";
                        // 印字拠点住所2_1
                        rowTmp["KYOTEN_ADDRESS2_1"] = "x";
                        // 印字拠点TEL1
                        rowTmp["KYOTEN_TEL_1"] = "y";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_1"] = "z";
                        // 印字拠点名2
                        rowTmp["KYOTEN_NAME_2"] = "aa";
                        // 印字拠点郵便番号2
                        rowTmp["KYOTEN_POST_2"] = "bb";
                        // 印字拠点住所1_2
                        rowTmp["KYOTEN_ADDRESS1_2"] = "cc";
                        // 印字拠点住所2_2
                        rowTmp["KYOTEN_ADDRESS2_2"] = "dd";
                        // 印字拠点TEL2
                        rowTmp["KYOTEN_TEL_2"] = "ee";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_2"] = "ff";
                        // 部署名
                        rowTmp["BUSHO_NAME"] = "gg";
                        // 営業担当者名
                        rowTmp["EIGYO_TANTOUSHA_NAME"] = "hh";
                        // 見積書文言
                        rowTmp["MITSUMORI_SENTENSE"] = "ii";
                        // 合計金額
                        rowTmp["GOUKEI_KINGAKU"] = "jj";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()].Add("Header", dataTableTmp);

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 金額
                        dataTableTmp.Columns.Add("KINGAKU");
                        // 品名別税区分
                        dataTableTmp.Columns.Add("HINMEI_ZEI_KBN_CD");
                        // 消費税
                        dataTableTmp.Columns.Add("TAX");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");
                        // 伝票区分
                        dataTableTmp.Columns.Add("DENPYOU_KBN");

                        for (int i = 0; i < 20; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // №
                            rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}", pageNo, i + 1);
                            // 品名
                            rowTmp["HINMEI_NAME"] = "kk";
                            // 数量
                            rowTmp["SUURYOU"] = "ll";
                            // 単位
                            rowTmp["UNIT_NAME"] = "mm";
                            // 単価
                            rowTmp["TANKA"] = "nn";
                            // 金額
                            rowTmp["KINGAKU"] = "oo";
                            // 品名別税区分
                            rowTmp["HINMEI_ZEI_KBN_CD"] = "pp";
                            // 消費税
                            rowTmp["TAX"] = "qq";
                            // 備考
                            rowTmp["MEISAI_BIKOU"] = "rr";
                            // 伝票区分
                            rowTmp["DENPYOU_KBN"] = "ss";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);

                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 合計(内税込)
                        dataTableTmp.Columns.Add("KINGAKU_TOTAL");
                        // 消費税(外税)
                        dataTableTmp.Columns.Add("TAX_SOTO");
                        // 課税対象額
                        dataTableTmp.Columns.Add("PRICE_PROPER");
                        // 総合計
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU_TOTAL");
                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        rowTmp = dataTableTmp.NewRow();

                        // 合計(内税込)
                        rowTmp["KINGAKU_TOTAL"] = "tt";
                        // 消費税(外税)
                        rowTmp["TAX_SOTO"] = "uu";
                        // 課税対象額
                        rowTmp["PRICE_PROPER"] = "vv";
                        // 総合計
                        rowTmp["GOUKEI_KINGAKU_TOTAL"] = "ww";
                        // 備考1
                        rowTmp["BIKOU_1"] = "xx";
                        // 備考2
                        rowTmp["BIKOU_2"] = "yy";
                        // 備考3
                        rowTmp["BIKOU_3"] = "zz";
                        // 備考4
                        rowTmp["BIKOU_4"] = "aaa";
                        // 備考5
                        rowTmp["BIKOU_5"] = "bbb";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                    case OutputTypeDef.KingakuMitsumoriH:   // 金額見積り（横）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");
                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");
                        // 業者名1(＋業者敬称1)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME1");
                        // 業者名2(＋業者敬称2)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME2");
                        // 現場名1(＋現場敬称1)
                        dataTableTmp.Columns.Add("GENBA_NAME1");
                        // 現場名2(＋現場敬称2)
                        dataTableTmp.Columns.Add("GENBA_NAME2");
                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");
                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");
                        // 合計金額
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU");

                        rowTmp = dataTableTmp.NewRow();

                        // タイトル
                        rowTmp["TITLE"] = "a";
                        // 見積書番号
                        rowTmp["MITSUMORI_NUMBER"] = "b";
                        // 見積日付
                        rowTmp["MITSUMORI_DATE"] = "c";
                        // 取引先名1(＋取引先敬称1)
                        rowTmp["TORIHIKISAKI_NAME1"] = "d";
                        // 取引先名2(＋取引先敬称2)
                        rowTmp["TORIHIKISAKI_NAME2"] = "e";
                        // 業者名1(＋業者敬称1)
                        rowTmp["GYOUSHA_NAME1"] = "f";
                        // 業者名2(＋業者敬称2)
                        rowTmp["GYOUSHA_NAME2"] = "g";
                        // 現場名1(＋現場敬称1)
                        rowTmp["GENBA_NAME1"] = "h";
                        // 現場名2(＋現場敬称2)
                        rowTmp["GENBA_NAME2"] = "i";
                        // 件名
                        rowTmp["KENMEI"] = "j";
                        // 見積項目名称1
                        rowTmp["MITSUMORI_KOUMOKU1"] = "k";
                        // 見積項目1
                        rowTmp["MITSUMORI_1"] = "l";
                        // 見積項目名称2
                        rowTmp["MITSUMORI_KOUMOKU2"] = "m";
                        // 見積項目2
                        rowTmp["MITSUMORI_2"] = "n";
                        // 見積項目名称3
                        rowTmp["MITSUMORI_KOUMOKU3"] = "o";
                        // 見積項目3
                        rowTmp["MITSUMORI_3"] = "p";
                        // 見積項目名称4
                        rowTmp["MITSUMORI_KOUMOKU4"] = "q";
                        // 見積項目4
                        rowTmp["MITSUMORI_4"] = "r";
                        // 会社名
                        rowTmp["CORP_NAME"] = "s";
                        // 代表者
                        rowTmp["CORP_DAIHYOU"] = "t";
                        // 印字拠点名1
                        rowTmp["KYOTEN_NAME_1"] = "u";
                        // 印字拠点郵便番号1
                        rowTmp["KYOTEN_POST_1"] = "v";
                        // 印字拠点住所1_1
                        rowTmp["KYOTEN_ADDRESS1_1"] = "w";
                        // 印字拠点住所2_1
                        rowTmp["KYOTEN_ADDRESS2_1"] = "x";
                        // 印字拠点TEL1
                        rowTmp["KYOTEN_TEL_1"] = "y";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_1"] = "z";
                        // 印字拠点名2
                        rowTmp["KYOTEN_NAME_2"] = "aa";
                        // 印字拠点郵便番号2
                        rowTmp["KYOTEN_POST_2"] = "bb";
                        // 印字拠点住所1_2
                        rowTmp["KYOTEN_ADDRESS1_2"] = "cc";
                        // 印字拠点住所2_2
                        rowTmp["KYOTEN_ADDRESS2_2"] = "dd";
                        // 印字拠点TEL2
                        rowTmp["KYOTEN_TEL_2"] = "ee";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_2"] = "ff";
                        // 部署名ラベル
                        rowTmp["BUSHO_NAME"] = "gg";
                        // 部署名
                        rowTmp["BUSHO_NAME"] = "hh";
                        // 営業担当者名
                        rowTmp["EIGYO_TANTOUSHA_NAME"] = "ii";
                        // 見積書文言
                        rowTmp["MITSUMORI_SENTENSE"] = "jj";
                        // 合計金額
                        rowTmp["GOUKEI_KINGAKU"] = "kk";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()]["Header"] = dataTableTmp;

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 金額
                        dataTableTmp.Columns.Add("KINGAKU");
                        // 品名別税区分
                        dataTableTmp.Columns.Add("HINMEI_ZEI_KBN_CD");
                        // 消費税
                        dataTableTmp.Columns.Add("TAX");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");
                        // 伝票区分
                        dataTableTmp.Columns.Add("DENPYOU_KBN");

                        for (int i = 0; i < 21; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // №
                            rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}" , pageNo, i + 1);
                            // 品名
                            rowTmp["HINMEI_NAME"] = "mm";
                            // 数量
                            rowTmp["SUURYOU"] = "nn";
                            // 単位
                            rowTmp["UNIT_NAME"] = "oo";
                            // 単価
                            rowTmp["TANKA"] = "pp";
                            // 金額
                            rowTmp["KINGAKU"] = "qq";
                            // 品名別税区分
                            rowTmp["HINMEI_ZEI_KBN_CD"] = "rr";
                            // 消費税
                            rowTmp["TAX"] = "ss";
                            // 備考
                            rowTmp["MEISAI_BIKOU"] = "tt";
                            // 伝票区分
                            rowTmp["DENPYOU_KBN"] = "uu";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);
                        
                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 合計(内税込)
                        dataTableTmp.Columns.Add("KINGAKU_TOTAL");
                        // 消費税(外税)
                        dataTableTmp.Columns.Add("TAX_SOTO");
                        // 課税対象額
                        dataTableTmp.Columns.Add("PRICE_PROPER");
                        // 総合計
                        dataTableTmp.Columns.Add("GOUKEI_KINGAKU_TOTAL");
                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        rowTmp = dataTableTmp.NewRow();

                        // 合計(内税込)
                        rowTmp["KINGAKU_TOTAL"] = "vv";
                        // 消費税(外税)
                        rowTmp["TAX_SOTO"] = "ww";
                        // 課税対象額
                        rowTmp["PRICE_PROPER"] = "xx";
                        // 総合計
                        rowTmp["GOUKEI_KINGAKU_TOTAL"] = "yy";
                        // 備考1
                        rowTmp["BIKOU_1"] = "zz";
                        // 備考2
                        rowTmp["BIKOU_2"] = "aaa";
                        // 備考3
                        rowTmp["BIKOU_3"] = "bbb";
                        // 備考4
                        rowTmp["BIKOU_4"] = "ccc";
                        // 備考5
                        rowTmp["BIKOU_5"] = "ddd";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                    case OutputTypeDef.TankaMitsumoriV:     // 単価見積り（縦）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");
                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");
                        // 業者名1(＋業者敬称1)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME1");
                        // 業者名2(＋業者敬称2)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME2");
                        // 現場名1(＋現場敬称1)
                        dataTableTmp.Columns.Add("GENBA_NAME1");
                        // 現場名2(＋現場敬称2)
                        dataTableTmp.Columns.Add("GENBA_NAME2");
                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");
                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");

                        rowTmp = dataTableTmp.NewRow();

                        // タイトル
                        rowTmp["TITLE"] = "a";
                        // 見積書番号
                        rowTmp["MITSUMORI_NUMBER"] = "b";
                        // 見積日付
                        rowTmp["MITSUMORI_DATE"] = "c";
                        // 取引先名1(＋取引先敬称1)
                        rowTmp["TORIHIKISAKI_NAME1"] = "d";
                        // 取引先名2(＋取引先敬称2)
                        rowTmp["TORIHIKISAKI_NAME2"] = "e";
                        // 業者名1(＋業者敬称1)
                        rowTmp["GYOUSHA_NAME1"] = "f";
                        // 業者名2(＋業者敬称2)
                        rowTmp["GYOUSHA_NAME2"] = "g";
                        // 現場名1(＋現場敬称1)
                        rowTmp["GENBA_NAME1"] = "h";
                        // 現場名2(＋現場敬称2)
                        rowTmp["GENBA_NAME2"] = "i";
                        // 件名
                        rowTmp["KENMEI"] = "j";
                        // 見積項目名称1
                        rowTmp["MITSUMORI_KOUMOKU1"] = "k";
                        // 見積項目1
                        rowTmp["MITSUMORI_1"] = "l";
                        // 見積項目名称2
                        rowTmp["MITSUMORI_KOUMOKU2"] = "m";
                        // 見積項目2
                        rowTmp["MITSUMORI_2"] = "n";
                        // 見積項目名称3
                        rowTmp["MITSUMORI_KOUMOKU3"] = "o";
                        // 見積項目3
                        rowTmp["MITSUMORI_3"] = "p";
                        // 見積項目名称4
                        rowTmp["MITSUMORI_KOUMOKU4"] = "q";
                        // 見積項目4
                        rowTmp["MITSUMORI_4"] = "r";
                        // 会社名
                        rowTmp["CORP_NAME"] = "s";
                        // 代表者
                        rowTmp["CORP_DAIHYOU"] = "t";
                        // 印字拠点名1
                        rowTmp["KYOTEN_NAME_1"] = "u";
                        // 印字拠点郵便番号1
                        rowTmp["KYOTEN_POST_1"] = "v";
                        // 印字拠点住所1_1
                        rowTmp["KYOTEN_ADDRESS1_1"] = "w";
                        // 印字拠点住所2_1
                        rowTmp["KYOTEN_ADDRESS2_1"] = "x";
                        // 印字拠点TEL1
                        rowTmp["KYOTEN_TEL_1"] = "y";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_1"] = "z";
                        // 印字拠点名2
                        rowTmp["KYOTEN_NAME_2"] = "aa";
                        // 印字拠点郵便番号2
                        rowTmp["KYOTEN_POST_2"] = "bb";
                        // 印字拠点住所1_2
                        rowTmp["KYOTEN_ADDRESS1_2"] = "cc";
                        // 印字拠点住所2_2
                        rowTmp["KYOTEN_ADDRESS2_2"] = "dd";
                        // 印字拠点TEL2
                        rowTmp["KYOTEN_TEL_2"] = "ee";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_2"] = "ff";
                        // 部署名
                        rowTmp["BUSHO_NAME"] = "gg";
                        // 営業担当者名
                        rowTmp["EIGYO_TANTOUSHA_NAME"] = "hh";
                        // 見積書文言
                        rowTmp["MITSUMORI_SENTENSE"] = "ii";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()].Add("Header", dataTableTmp);

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");
                        // 伝票区分
                        dataTableTmp.Columns.Add("DENPYOU_KBN_CD");

                        for (int i = 0; i < 25; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // №
                            rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}", pageNo, i + 1);
                            // 品名
                            rowTmp["HINMEI_NAME"] = "kk";
                            // 数量
                            rowTmp["SUURYOU"] = "ll";
                            // 単位
                            rowTmp["UNIT_NAME"] = "mm";
                            // 単価
                            rowTmp["TANKA"] = "nn";
                            // 備考
                            rowTmp["MEISAI_BIKOU"] = "oo";
                            // 伝票区分
                            rowTmp["DENPYOU_KBN_CD"] = "pp";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);

                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        rowTmp = dataTableTmp.NewRow();
                        // 備考1
                        rowTmp["BIKOU_1"] = "qq";
                        // 備考2
                        rowTmp["BIKOU_2"] = "rr";
                        // 備考3
                        rowTmp["BIKOU_3"] = "ss";
                        // 備考4
                        rowTmp["BIKOU_4"] = "tt";
                        // 備考5
                        rowTmp["BIKOU_5"] = "uu";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                    case OutputTypeDef.TankaMitsumoriH:     // 単価見積り（横）

                        #region - Header -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Header";

                        // タイトル
                        dataTableTmp.Columns.Add("TITLE");
                        // 見積書番号
                        dataTableTmp.Columns.Add("MITSUMORI_NUMBER");
                        // 見積日付
                        dataTableTmp.Columns.Add("MITSUMORI_DATE");
                        // 取引先名1(＋取引先敬称1)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME1");
                        // 取引先名2(＋取引先敬称2)
                        dataTableTmp.Columns.Add("TORIHIKISAKI_NAME2");
                        // 業者名1(＋業者敬称1)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME1");
                        // 業者名2(＋業者敬称2)
                        dataTableTmp.Columns.Add("GYOUSHA_NAME2");
                        // 現場名1(＋現場敬称1)
                        dataTableTmp.Columns.Add("GENBA_NAME1");
                        // 現場名2(＋現場敬称2)
                        dataTableTmp.Columns.Add("GENBA_NAME2");
                        // 件名
                        dataTableTmp.Columns.Add("KENMEI");
                        // 見積項目名称1
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU1");
                        // 見積項目1
                        dataTableTmp.Columns.Add("MITSUMORI_1");
                        // 見積項目名称2
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU2");
                        // 見積項目2
                        dataTableTmp.Columns.Add("MITSUMORI_2");
                        // 見積項目名称3
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU3");
                        // 見積項目3
                        dataTableTmp.Columns.Add("MITSUMORI_3");
                        // 見積項目名称4
                        dataTableTmp.Columns.Add("MITSUMORI_KOUMOKU4");
                        // 見積項目4
                        dataTableTmp.Columns.Add("MITSUMORI_4");
                        // 会社名
                        dataTableTmp.Columns.Add("CORP_NAME");
                        // 代表者
                        dataTableTmp.Columns.Add("CORP_DAIHYOU");
                        // 印字拠点名1
                        dataTableTmp.Columns.Add("KYOTEN_NAME_1");
                        // 印字拠点郵便番号1
                        dataTableTmp.Columns.Add("KYOTEN_POST_1");
                        // 印字拠点住所1_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_1");
                        // 印字拠点住所2_1
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_1");
                        // 印字拠点TEL1
                        dataTableTmp.Columns.Add("KYOTEN_TEL_1");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_1");
                        // 印字拠点名2
                        dataTableTmp.Columns.Add("KYOTEN_NAME_2");
                        // 印字拠点郵便番号2
                        dataTableTmp.Columns.Add("KYOTEN_POST_2");
                        // 印字拠点住所1_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS1_2");
                        // 印字拠点住所2_2
                        dataTableTmp.Columns.Add("KYOTEN_ADDRESS2_2");
                        // 印字拠点TEL2
                        dataTableTmp.Columns.Add("KYOTEN_TEL_2");
                        // 印字拠点FAX2
                        dataTableTmp.Columns.Add("KYOTEN_FAXL_2");
                        // 部署名
                        dataTableTmp.Columns.Add("BUSHO_NAME");
                        // 営業担当者名
                        dataTableTmp.Columns.Add("EIGYO_TANTOUSHA_NAME");
                        // 見積書文言
                        dataTableTmp.Columns.Add("MITSUMORI_SENTENSE");

                        rowTmp = dataTableTmp.NewRow();

                        // タイトル
                        rowTmp["TITLE"] = "a";
                        // 見積書番号
                        rowTmp["MITSUMORI_NUMBER"] = "b";
                        // 見積日付
                        rowTmp["MITSUMORI_DATE"] = "c";
                        // 取引先名1(＋取引先敬称1)
                        rowTmp["TORIHIKISAKI_NAME1"] = "d";
                        // 取引先名2(＋取引先敬称2)
                        rowTmp["TORIHIKISAKI_NAME2"] = "e";
                        // 業者名1(＋業者敬称1)
                        rowTmp["GYOUSHA_NAME1"] = "f";
                        // 業者名2(＋業者敬称2)
                        rowTmp["GYOUSHA_NAME2"] = "g";
                        // 現場名1(＋現場敬称1)
                        rowTmp["GENBA_NAME1"] = "h";
                        // 現場名2(＋現場敬称2)
                        rowTmp["GENBA_NAME2"] = "i";
                        // 件名
                        rowTmp["KENMEI"] = "j";
                        // 見積項目名称1
                        rowTmp["MITSUMORI_KOUMOKU1"] = "k";
                        // 見積項目1
                        rowTmp["MITSUMORI_1"] = "l";
                        // 見積項目名称2
                        rowTmp["MITSUMORI_KOUMOKU2"] = "m";
                        // 見積項目2
                        rowTmp["MITSUMORI_2"] = "n";
                        // 見積項目名称3
                        rowTmp["MITSUMORI_KOUMOKU3"] = "o";
                        // 見積項目3
                        rowTmp["MITSUMORI_3"] = "p";
                        // 見積項目名称4
                        rowTmp["MITSUMORI_KOUMOKU4"] = "q";
                        // 見積項目4
                        rowTmp["MITSUMORI_4"] = "r";
                        // 会社名
                        rowTmp["CORP_NAME"] = "s";
                        // 代表者
                        rowTmp["CORP_DAIHYOU"] = "t";
                        // 印字拠点名1
                        rowTmp["KYOTEN_NAME_1"] = "u";
                        // 印字拠点郵便番号1
                        rowTmp["KYOTEN_POST_1"] = "v";
                        // 印字拠点住所1_1
                        rowTmp["KYOTEN_ADDRESS1_1"] = "w";
                        // 印字拠点住所2_1
                        rowTmp["KYOTEN_ADDRESS2_1"] = "x";
                        // 印字拠点TEL1
                        rowTmp["KYOTEN_TEL_1"] = "y";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_1"] = "z";
                        // 印字拠点名2
                        rowTmp["KYOTEN_NAME_2"] = "aa";
                        // 印字拠点郵便番号2
                        rowTmp["KYOTEN_POST_2"] = "bb";
                        // 印字拠点住所1_2
                        rowTmp["KYOTEN_ADDRESS1_2"] = "cc";
                        // 印字拠点住所2_2
                        rowTmp["KYOTEN_ADDRESS2_2"] = "dd";
                        // 印字拠点TEL2
                        rowTmp["KYOTEN_TEL_2"] = "ee";
                        // 印字拠点FAX2
                        rowTmp["KYOTEN_FAXL_2"] = "ff";
                        // 部署名
                        rowTmp["BUSHO_NAME"] = "gg";
                        // 営業担当者名
                        rowTmp["EIGYO_TANTOUSHA_NAME"] = "hh";
                        // 見積書文言
                        rowTmp["MITSUMORI_SENTENSE"] = "ii";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()].Add("Header", dataTableTmp);

                        #endregion - Header -

                        #region - Detail -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Detail";

                        // №
                        dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                        // 品名
                        dataTableTmp.Columns.Add("HINMEI_NAME");
                        // 数量
                        dataTableTmp.Columns.Add("SUURYOU");
                        // 単位
                        dataTableTmp.Columns.Add("UNIT_NAME");
                        // 単価
                        dataTableTmp.Columns.Add("TANKA");
                        // 備考
                        dataTableTmp.Columns.Add("MEISAI_BIKOU");
                        // 伝票区分
                        dataTableTmp.Columns.Add("DENPYOU_KBN_CD");

                        for (int i = 0; i < 15; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // №
                            rowTmp["DENPYOU_NUMBER"] = string.Format("{0}-{1}", pageNo, i + 1);
                            // 品名
                            rowTmp["HINMEI_NAME"] = "kk";
                            // 数量
                            rowTmp["SUURYOU"] = "ll";
                            // 単位
                            rowTmp["UNIT_NAME"] = "mm";
                            // 単価
                            rowTmp["TANKA"] = "nn";
                            // 備考
                            rowTmp["MEISAI_BIKOU"] = "oo";
                            // 伝票区分
                            rowTmp["DENPYOU_KBN_CD"] = "pp";

                            dataTableTmp.Rows.Add(rowTmp);
                        }

                        this.DataTablePageList[pageNo.ToString()].Add("Detail", dataTableTmp);

                        #endregion - Detail -

                        #region - Footer -

                        dataTableTmp = new DataTable();
                        dataTableTmp.TableName = "Footer";

                        // 備考1
                        dataTableTmp.Columns.Add("BIKOU_1");
                        // 備考2
                        dataTableTmp.Columns.Add("BIKOU_2");
                        // 備考3
                        dataTableTmp.Columns.Add("BIKOU_3");
                        // 備考4
                        dataTableTmp.Columns.Add("BIKOU_4");
                        // 備考5
                        dataTableTmp.Columns.Add("BIKOU_5");

                        rowTmp = dataTableTmp.NewRow();

                        // 備考1
                        rowTmp["BIKOU_1"] = "qq";
                        // 備考2
                        rowTmp["BIKOU_2"] = "rr";
                        // 備考3
                        rowTmp["BIKOU_3"] = "ss";
                        // 備考4
                        rowTmp["BIKOU_4"] = "tt";
                        // 備考5
                        rowTmp["BIKOU_5"] = "uu";

                        dataTableTmp.Rows.Add(rowTmp);

                        this.DataTablePageList[pageNo.ToString()].Add("Footer", dataTableTmp);

                        #endregion - Footer -

                        break;
                }
            }
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int rowNo = 1;
            int i;
            DataRow row = null;
            string ctrlName = string.Empty;

            for (int pageNo = 1; pageNo <= this.DataTablePageList.Count; pageNo++)
            {
                int maxPage;
                bool detailComp = false;
                DataTable dataTableTmp = this.DataTablePageList[pageNo.ToString()]["Detail"];
                int detailMaxCount = dataTableTmp.Rows.Count;
                int detailStart = 0;

                int maxRow = 0;

                // 帳票出力用データの設定処理
                this.SetChouhyouInfo(pageNo);

                #region - Detail -

                switch (this.OutputType)
                {
                    case OutputTypeDef.KingakuMitsumoriV:       // 金額見積もり縦

                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 金額
                            ctrlName = "PHY_KINGAKU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 税区分
                            ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 消費税
                            ctrlName = "PHY_TAX_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 伝票区分
                            ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((double)detailMaxCount / ConstMaxDispDetailVRowCount);
                        maxRow = maxPage * ConstMaxDispDetailVRowCount;
                        rowNo = 1;
                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();

                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 金額
                                index = dataTableTmp.Columns.IndexOf("KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN_CD");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("TAX");
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }
                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 金額
                                index = dataTableTmp.Columns.IndexOf("KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = string.Empty;

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("TAX");
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = string.Empty;

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN_CD");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);

                            rowNo++;
                        }

                        break;
                    case OutputTypeDef.KingakuMitsumoriH:       // 金額見積もり横
                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 金額
                            ctrlName = "PHY_KINGAKU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 税区分
                            ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 消費税
                            ctrlName = "PHY_TAX_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 伝票区分
                            ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((double)detailMaxCount / ConstMaxDispDetailHRowCount);
                        maxRow = maxPage * ConstMaxDispDetailHRowCount;
                        rowNo = 1;

                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();

                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 金額
                                index = dataTableTmp.Columns.IndexOf("KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN_CD");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("TAX");
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }
                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 金額
                                index = dataTableTmp.Columns.IndexOf("KINGAKU");
                                ctrlName = "PHY_KINGAKU_FLB";
                                row[ctrlName] = string.Empty;

                                // 税区分
                                index = dataTableTmp.Columns.IndexOf("HINMEI_ZEI_KBN");
                                ctrlName = "PHN_HINMEI_ZEI_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;

                                // 消費税
                                index = dataTableTmp.Columns.IndexOf("TAX");
                                ctrlName = "PHY_TAX_FLB";
                                row[ctrlName] = string.Empty;

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN_CD");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);

                            rowNo++;
                        }

                        break;
                    case OutputTypeDef.TankaMitsumoriV:         // 単価見積もり縦

                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 伝票区分
                            ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((double)detailMaxCount / ConstMaxDispTankaDetailVRowCount);
                        maxRow = maxPage * ConstMaxDispTankaDetailVRowCount;
                        rowNo = 1;
                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();

                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN_CD");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }
                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN_CD");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);
                            rowNo++;
                        }

                        break;
                    case OutputTypeDef.TankaMitsumoriH:         // 単価見積もり横

                        #region Columns

                        if (pageNo == 1)
                        {
                            // №
                            ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 品名
                            ctrlName = "PHY_HINMEI_NAME_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 数量
                            ctrlName = "PHY_SUURYOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単位
                            ctrlName = "PHN_UNIT_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 単価
                            ctrlName = "PHY_TANKA_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 備考
                            ctrlName = "PHY_MEISAI_BIKOU_FLB";
                            this.dataTable.Columns.Add(ctrlName);

                            // 伝票区分
                            ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                            this.dataTable.Columns.Add(ctrlName);
                        }

                        #endregion

                        maxPage = (int)Math.Ceiling((double)detailMaxCount / ConstMaxDispTankaDetailHRowCount);
                        maxRow = maxPage * ConstMaxDispTankaDetailHRowCount;
                        rowNo = 1;
                        for (i = detailStart; i < maxRow; i++)
                        {
                            row = this.dataTable.NewRow();
                            if (!detailComp)
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN_CD");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = dataTableTmp.Rows[i].ItemArray[index];

                                if (rowNo == dataTableTmp.Rows.Count)
                                {
                                    detailComp = true;
                                }
                            }
                            else
                            {
                                // №
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_NUMBER");
                                ctrlName = "PHY_DENPYOU_NUMBER_FLB";
                                row[ctrlName] = string.Empty;

                                // 品名
                                index = dataTableTmp.Columns.IndexOf("HINMEI_NAME");
                                ctrlName = "PHY_HINMEI_NAME_FLB";
                                row[ctrlName] = string.Empty;

                                // 数量
                                index = dataTableTmp.Columns.IndexOf("SUURYOU");
                                ctrlName = "PHY_SUURYOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 単位
                                index = dataTableTmp.Columns.IndexOf("UNIT_NAME");
                                ctrlName = "PHN_UNIT_FLB";
                                row[ctrlName] = string.Empty;

                                // 単価
                                index = dataTableTmp.Columns.IndexOf("TANKA");
                                ctrlName = "PHY_TANKA_FLB";
                                row[ctrlName] = string.Empty;

                                // 備考
                                index = dataTableTmp.Columns.IndexOf("MEISAI_BIKOU");
                                ctrlName = "PHY_MEISAI_BIKOU_FLB";
                                row[ctrlName] = string.Empty;

                                // 伝票区分
                                index = dataTableTmp.Columns.IndexOf("DENPYOU_KBN_CD");
                                ctrlName = "PHY_DENPYOU_KBN_CD_FLB";
                                row[ctrlName] = string.Empty;
                            }

                            this.dataTable.Rows.Add(row);
                            rowNo++;
                        }

                        break;
                }

                this.SetRecord(this.dataTable);

                #endregion - Detail -
            }
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
        }

        /// <summary>帳票出力用データテーブル作成処理を実行する</summary>
        private void SetChouhyouInfo(int pageNo)
        {
            int index;
            DataTable dataTableHeaderTmp = this.DataTablePageList[pageNo.ToString()]["Header"];
            DataTable dataTableFooterTmp = this.DataTablePageList[pageNo.ToString()]["Footer"];
            string ctrlName = string.Empty;

            switch (this.OutputType)
            {
                case OutputTypeDef.KingakuMitsumoriV:       // 金額見積り（縦）
                    #region - Header -

                    //// タイトル
                    //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                    //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書番号
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                    this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積日付
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                    this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 付帯工事
                    index = dataTableHeaderTmp.Columns.IndexOf("FUTAIKOUJI");
                    this.SetFieldName("FH_FUTAIKOUJI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 紹介料
                    index = dataTableHeaderTmp.Columns.IndexOf("SYOUKAI");
                    this.SetFieldName("FH_SYOUKAI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名1(＋取引先敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名2(＋取引先敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 件名
                    index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                    this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                    this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                    this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                    this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                    this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 会社名
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                    this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 代表者
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                    this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                    this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                    this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                    this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                    this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                    this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                    this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                    this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                    this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書文言
                    //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                    //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                    // 合計金額
                    index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                    this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    #endregion - Header -

                    #region - Footer -

                    // 合計(内税込)
                    index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_TOTAL");
                    this.SetFieldName("PF_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 消費税(外税)
                    index = dataTableFooterTmp.Columns.IndexOf("TAX_SOTO");
                    this.SetFieldName("PF_TAX_SOTO_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 課税対象額
                    index = dataTableFooterTmp.Columns.IndexOf("PRICE_PROPER");
                    this.SetFieldName("PF_PRICE_PROPER_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 総合計
                    index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                    this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考1
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                    this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考2
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                    this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考3
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                    this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考4
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                    this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考5
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                    this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 部署名
                    index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                    this.SetFieldName("PF_BUSHO_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 営業担当者名
                    index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                    this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    #endregion - Footer -

                    break;
                case OutputTypeDef.KingakuMitsumoriH:       // 金額見積り（横）
                    #region - Header -

                    // タイトル
                    //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                    //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書番号
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                    this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積日付
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                    this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名1(＋取引先敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名2(＋取引先敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 業者名1(＋業者敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                    this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 業者名2(＋業者敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                    this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 現場名1(＋現場敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                    this.SetFieldName("PHY_GENBA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 現場名2(＋現場敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                    this.SetFieldName("PHY_GENBA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 件名
                    index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                    this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                    this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                    this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                    this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                    this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 会社名
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                    this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 代表者
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                    this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                    this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                    this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                    this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                    this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                    this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                    this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                    this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                    this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書文言
                    //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                    //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                    // 部署名
                    index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                    this.SetFieldName("PHY_BUSHO_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 営業担当者名
                    index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                    this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 合計金額
                    index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                    this.SetFieldName("PHY_GOUKEI_KINGAKU_TOTAL_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    #endregion - Header -

                    #region - Footer -

                    // 合計(内税込)
                    index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_TOTAL");
                    this.SetFieldName("PF_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 消費税(外税)
                    index = dataTableFooterTmp.Columns.IndexOf("TAX_SOTO");
                    this.SetFieldName("PF_TAX_SOTO_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 課税対象額
                    index = dataTableFooterTmp.Columns.IndexOf("PRICE_PROPER");
                    this.SetFieldName("PF_PRICE_PROPER_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 総合計
                    index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                    this.SetFieldName("PF_GOUKEI_KINGAKU_TOTAL_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考1
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                    this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考2
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                    this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考3
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                    this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考4
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                    this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考5
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                    this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    #endregion - Footer -

                    break;
                case OutputTypeDef.TankaMitsumoriV:         // 単価見積り（縦）
                    #region - Header -

                    //// タイトル
                    //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                    //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書番号
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                    this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積日付
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                    this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名1(＋取引先敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名2(＋取引先敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 業者名1(＋業者敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                    this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 業者名2(＋業者敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                    this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 現場名1(＋現場敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                    this.SetFieldName("PHY_GENBA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 現場名2(＋現場敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                    this.SetFieldName("PHY_GENBA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 件名
                    index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                    this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                    this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                    this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                    this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                    this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 会社名
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                    this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 代表者
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                    this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                    this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                    this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                    this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                    this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                    this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                    this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                    this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                    this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書文言
                    //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                    //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                    #endregion - Header -

                    #region - Footer -

                    // 備考1
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                    this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考2
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                    this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考3
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                    this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考4
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                    this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考5
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                    this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 部署名
                    index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                    this.SetFieldName("PF_BUSHO_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 営業担当者名
                    index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                    this.SetFieldName("PF_EIGYO_TANTOUSHA_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    #endregion - Footer -

                    break;
                case OutputTypeDef.TankaMitsumoriH:         // 単価見積り（横）
                    #region - Header -

                    // タイトル
                    //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                    //this.SetFieldName("PHY_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書番号
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_NUMBER");
                    this.SetFieldName("PHY_MITSUMORI_NUMBER_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積日付
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_DATE");
                    this.SetFieldName("PHY_MITSUMORI_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名1(＋取引先敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME1");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 取引先名2(＋取引先敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME2");
                    this.SetFieldName("PHY_TORIHIKISAKI_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 業者名1(＋業者敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                    this.SetFieldName("PHY_GYOUSHA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 業者名2(＋業者敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                    this.SetFieldName("PHY_GYOUSHA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 現場名1(＋現場敬称1)
                    index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME1");
                    this.SetFieldName("PHY_GENBA_NAME1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 現場名2(＋現場敬称2)
                    index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME2");
                    this.SetFieldName("PHY_GENBA_NAME2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 件名
                    index = dataTableHeaderTmp.Columns.IndexOf("KENMEI");
                    this.SetFieldName("PHY_KENMEI_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU1");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目1
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_1");
                    this.SetFieldName("PHY_MITSUMORI_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU2");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目2
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_2");
                    this.SetFieldName("PHY_MITSUMORI_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU3");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目3
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_3");
                    this.SetFieldName("PHY_MITSUMORI_3_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目名称4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_KOUMOKU4");
                    this.SetFieldName("PHY_MITSUMORI_KOUMOKU4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積項目4
                    index = dataTableHeaderTmp.Columns.IndexOf("MITSUMORI_4");
                    this.SetFieldName("PHY_MITSUMORI_4_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 会社名
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_NAME");
                    this.SetFieldName("PHY_CORP_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 代表者
                    index = dataTableHeaderTmp.Columns.IndexOf("CORP_DAIHYOU");
                    this.SetFieldName("PHY_CORP_DAIHYOU_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_1");
                    this.SetFieldName("PHY_KYOTEN_NAME_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_1");
                    this.SetFieldName("PHY_KYOTEN_POST_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_1");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL1
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_1");
                    this.SetFieldName("PHY_KYOTEN_TEL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_1");
                    this.SetFieldName("PHY_KYOTEN_FAXL_1_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点名2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME_2");
                    this.SetFieldName("PHY_KYOTEN_NAME_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点郵便番号2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST_2");
                    this.SetFieldName("PHY_KYOTEN_POST_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所1_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS1_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点住所2_2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2_2");
                    this.SetFieldName("PHY_KYOTEN_ADDRESS2_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点TEL2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL_2");
                    this.SetFieldName("PHY_KYOTEN_TEL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 印字拠点FAX2
                    index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAXL_2");
                    this.SetFieldName("PHY_KYOTEN_FAXL_2_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 見積書文言
                    //index = dataTableHeaderTmp.Columns.IndexOf("FH_MITSUMORI_SENTENSE_FLB");
                    //this.SetFieldName("FH_MITSUMORI_SENTENSE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    //this.SetFieldAlign("FH_MITSUMORI_SENTENSE_FLB", ALIGN_TYPE.Left);

                    // 部署名
                    index = dataTableHeaderTmp.Columns.IndexOf("BUSHO_NAME");
                    this.SetFieldName("PHY_BUSHO_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    // 営業担当者名
                    index = dataTableHeaderTmp.Columns.IndexOf("EIGYO_TANTOUSHA_NAME");
                    this.SetFieldName("PHY_EIGYO_TANTOUSHA_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                    #endregion - Header -

                    #region - Footer -

                    // 備考1
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_1");
                    this.SetFieldName("PF_BIKOU_1_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考2
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_2");
                    this.SetFieldName("PF_BIKOU_2_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考3
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_3");
                    this.SetFieldName("PF_BIKOU_3_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考4
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_4");
                    this.SetFieldName("PF_BIKOU_4_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    // 備考5
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU_5");
                    this.SetFieldName("PF_BIKOU_5_CTL", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);

                    #endregion - Footer -

                    break;
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
