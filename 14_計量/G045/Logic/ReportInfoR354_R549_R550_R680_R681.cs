using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Entity;

namespace Shougun.Core.Scale.Keiryou
{
    #region - Class -

    /// <summary>(R354・R549・R550・R680・R681)計量票を表すクラス・コントロール</summary>
    public class ReportInfoR354_R549_R550_R680_R681 : ReportInfoBase
    {
        #region - Fields -

        /// <summary>Detail部に表示するレコート最大数を保持するフィールド</summary>
        private const int ConstMaxDispDetailRowCount = 5;

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        /// <summary>
        /// DBアクセッサー
        /// </summary>
        private Shougun.Core.Scale.Keiryou.Accessor.DBAccessor accessor;

        /// <summary>
        /// 画面ロジック
        /// </summary>
        public Shougun.Core.Scale.Keiryou.LogicClass logic;

        public DateTime sysDate = DateTime.Now;

        public DateTime DenpyouDate = DateTime.Now;

        /// システム設定情報
        private M_SYS_INFO sysInfo;

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR354_R549_R550_R680_R681"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR354_R549_R550_R680_R681(WINDOW_ID windowID)
        {
            this.windowID = windowID;

            /// <summary>出力タイプを保持するプロパティ</summary>
            this.OutputType = OutputTypeDef.Normal;
            this.DispTypeForNormal = DispTypeForNormalDef.Torihikisaki;

            // Accessor
            this.accessor = new Shougun.Core.Scale.Keiryou.Accessor.DBAccessor();
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>出力タイプに関する列挙型</summary>
        public enum OutputTypeDef
        {
            /// <summary>A4 縦三つ切り</summary>
            Normal = 1,

            /// <summary>三つ切り 複数品目</summary>
            MultiH,

            /// <summary>三つ切り 単品目</summary>
            SingleH,

            /// <summary>三つ切り 複数品目 取引先なし</summary>
            MultiH_NoTorihikisaki,

            /// <summary>三つ切り 単品目 取引先なし</summary>
            SingleH_NoTorihikisaki,
        }

        /// <summary>A4 縦三つ切りの表示タイプに関する列挙型</summary>
        public enum DispTypeForNormalDef
        {
            /// <summary>取引先</summary>
            Torihikisaki = 0,
            /// <summary>業者</summary>
            Gyousha,
        }
        #endregion - Enums -

        #region - Properties -

        /// <summary>出力タイプを保持するプロパティ</summary>
        public OutputTypeDef OutputType { get; set; }

        /// <summary>
        /// A4 縦三つ切りの表示タイプ
        /// デザイナで取引先が表示されている部分に何を表示するか指定可能とする
        /// </summary>
        public DispTypeForNormalDef DispTypeForNormal { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>サンプルデータの作成処理を実行する</summary>
        public void CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;

            bool isPrint = true;
            bool isPrintH = true;
            switch (this.OutputType)
            {
                case OutputTypeDef.Normal:    // A4 縦三つ切り

                    #region - A4 縦三つ切り -

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    dataTableTmp.Columns.Add("KEIRYOU_HYOU_TITLE");
                    dataTableTmp.Columns.Add("TANTOU");
                    dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
                    dataTableTmp.Columns.Add("TORIHIKISAKI_NAME");
                    dataTableTmp.Columns.Add("TORIHIKISAKI_KEISYOU");
                    dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                    dataTableTmp.Columns.Add("JYOUIN");
                    dataTableTmp.Columns.Add("SHABAN");
                    dataTableTmp.Columns.Add("DENPYOU_DATE");
                    dataTableTmp.Columns.Add("PAGE");

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量証明書１２３４５６７８９０１２３４５,計量表１２３４５６７８９０１２３４５６７,計量表（控）１２３４５６７８９０１２３４";
                        rowTmp["TANTOU"] = "あいうえおかきく";

                        rowTmp["TORIHIKISAKI_CD"] = "123456";
                        rowTmp["TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        rowTmp["TORIHIKISAKI_KEISYOU"] = "あい";
                        rowTmp["DENPYOU_NUMBER"] = "1234567";
                        rowTmp["JYOUIN"] = "12";
                        rowTmp["SHABAN"] = "1234567890123456789012345";
                        rowTmp["DENPYOU_DATE"] = "2013/12/10";
                        rowTmp["PAGE"] = "j";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail";

                    dataTableTmp.Columns.Add("ROW_NO");
                    dataTableTmp.Columns.Add("STACK_JYUURYOU");
                    dataTableTmp.Columns.Add("EMPTY_JYUURYOU");
                    dataTableTmp.Columns.Add("NET_CHOUSEI");
                    dataTableTmp.Columns.Add("YOUKI_JYUURYOU");
                    dataTableTmp.Columns.Add("NET_JYUURYOU");
                    dataTableTmp.Columns.Add("HINMEI_CD");
                    dataTableTmp.Columns.Add("HINMEI_NAME");
                    dataTableTmp.Columns.Add("KEIRYOU_TIME");

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            rowTmp["ROW_NO"] = ((i + 1) * 10).ToString();
                            rowTmp["STACK_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["EMPTY_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["NET_CHOUSEI"] = "123,456,789,000,123,456";
                            rowTmp["YOUKI_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["NET_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["HINMEI_CD"] = "123456";
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            rowTmp["KEIRYOU_TIME"] = "23:59";

                            dataTableTmp.Rows.Add(rowTmp);
                        }
                    }

                    this.DataTableList.Add("Detail", dataTableTmp);

                    #endregion - Detail -

                    #region - Footer -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Footer";

                    dataTableTmp.Columns.Add("GENBA_CD");
                    dataTableTmp.Columns.Add("GENBA_NAME");
                    dataTableTmp.Columns.Add("NET_JYUURYOU_TOTAL");

                    dataTableTmp.Columns.Add("DENPYOU_BIKOU");
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU1");
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU2");
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU3");
                    dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                    dataTableTmp.Columns.Add("KYOTEN_NAME");
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
                    dataTableTmp.Columns.Add("KYOTEN_TEL");
                    dataTableTmp.Columns.Add("KYOTEN_FAX");

                    if (isPrint)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        rowTmp["GENBA_CD"] = "123456";
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよらり";
                        rowTmp["NET_JYUURYOU_TOTAL"] = "123,456,789,000,123,456";

                        rowTmp["DENPYOU_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KEIRYOU_JYOUHOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KEIRYOU_JYOUHOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KEIRYOU_JYOUHOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KYOTEN_TEL"] = "1234567890123";
                        rowTmp["KYOTEN_FAX"] = "1234567890123";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    #endregion - A4 縦三つ切り -

                    break;
                case OutputTypeDef.MultiH:   // 三つ切り 複数品目

                    #region - 三つ切り 複数品目 -

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    // 計量証明書タイトル
                    dataTableTmp.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmp.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                    // 取引先CD
                    dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
                    // 取引先名
                    dataTableTmp.Columns.Add("TORIHIKISAKI_NAME");
                    // 取引先敬称
                    dataTableTmp.Columns.Add("TORIHIKISAKI_KEISYOU");
                    // 業者CD
                    dataTableTmp.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmp.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmp.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmp.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmp.Columns.Add("GENBA_NAME");
                    // 車輌
                    dataTableTmp.Columns.Add("SHARYOU");
                    // 総重量
                    dataTableTmp.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmp.Columns.Add("EMPTY_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmp.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmp.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // バーコード
                    dataTableTmp.Columns.Add("BARCODE");

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 計量証明書タイトル
                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量票１,計量票２,計量票３";
                        // 伝票日付
                        rowTmp["DENPYOU_DATE"] = "2013/12/10";
                        // 伝票番号
                        rowTmp["DENPYOU_NUMBER"] = "1234567";
                        // 取引先CD
                        rowTmp["TORIHIKISAKI_CD"] = "123456";
                        // 取引先名
                        rowTmp["TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 取引先敬称
                        rowTmp["TORIHIKISAKI_KEISYOU"] = "あい";
                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "123456";
                        // 業者名
                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 業者敬称
                        rowTmp["GYOUSHA_KEISYOU"] = "あい";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "123456";
                        // 現場名
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 車輌
                        rowTmp["SHARYOU"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 総重量
                        rowTmp["STACK_JYUURYOU"] = "123,456,789,000,123,456";
                        // 空車重量
                        rowTmp["EMPTY_JYUURYOU"] = "123,456,789,000,123,456";
                        // 総重量計量時間
                        rowTmp["STACK_KEIRYOU_TIME"] = "23:59";
                        // 空車重量計量時間
                        rowTmp["EMPTY_KEIRYOU_TIME"] = "23:59";
                        // バーコード
                        rowTmp["BARCODE"] = "0123456789";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail";

                    // 品名CD
                    dataTableTmp.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmp.Columns.Add("HINMEI_NAME");
                    // 調整
                    dataTableTmp.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmp.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmp.Columns.Add("NET_JYUURYOU");
                    // 計量時間
                    dataTableTmp.Columns.Add("KEIRYOU_TIME");

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 品名CD
                            rowTmp["HINMEI_CD"] = "123456";
                            // 品名
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 調整
                            rowTmp["NET_CHOUSEI"] = "123,456,789,000,123,456";
                            // 容器引
                            rowTmp["YOUKI_JYUURYOU"] = "123,456,789,000,123,456";
                            // 正味
                            rowTmp["NET_JYUURYOU"] = "123,456,789,000,123,456";
                            // 計量時間
                            rowTmp["KEIRYOU_TIME"] = "23:59";

                            dataTableTmp.Rows.Add(rowTmp);
                        }
                    }

                    this.DataTableList.Add("Detail", dataTableTmp);

                    #endregion - Detail -

                    #region - Footer -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Footer";

                    // 調整合計
                    dataTableTmp.Columns.Add("NET_CHOSEI_TOTAL");
                    // 容器引合計
                    dataTableTmp.Columns.Add("YOUKI_JYUURYOU_TOTAL");
                    // 正味合計
                    dataTableTmp.Columns.Add("NET_JYUURYOU_TOTAL");

                    // 伝票備考
                    dataTableTmp.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmp.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmp.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmp.Columns.Add("KYOTEN_FAX");

                    if (isPrint)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 調整合計
                        rowTmp["NET_CHOSEI_TOTAL"] = "123,456,789,000,123,456";
                        // 容器引合計
                        rowTmp["YOUKI_JYUURYOU_TOTAL"] = "123,456,789,000,123,456";
                        // 正味合計
                        rowTmp["NET_JYUURYOU_TOTAL"] = "123,456,789,000,123,456";

                        // 伝票備考
                        rowTmp["DENPYOU_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目1
                        rowTmp["KEIRYOU_JYOUHOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目2
                        rowTmp["KEIRYOU_JYOUHOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目3
                        rowTmp["KEIRYOU_JYOUHOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所1
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所2
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点電話
                        rowTmp["KYOTEN_TEL"] = "1234567890123";
                        // 拠点FAX
                        rowTmp["KYOTEN_FAX"] = "1234567890123";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    #endregion - 三つ切り 複数品目 -

                    break;
                case OutputTypeDef.SingleH:   // 三つ切り 単品目

                    #region - 三つ切り 単品目 -

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    // 計量証明書タイトル
                    dataTableTmp.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmp.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                    // 取引先CD
                    dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
                    // 取引先名
                    dataTableTmp.Columns.Add("TORIHIKISAKI_NAME");
                    // 取引先敬称
                    dataTableTmp.Columns.Add("TORIHIKISAKI_KEISYOU");
                    // 業者CD
                    dataTableTmp.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmp.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmp.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmp.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmp.Columns.Add("GENBA_NAME");
                    // 業者敬称
                    dataTableTmp.Columns.Add("GENBA_KEISYOU");
                    // 車輌
                    dataTableTmp.Columns.Add("SHARYOU");
                    // バーコード
                    dataTableTmp.Columns.Add("BARCODE");

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 計量証明書タイトル
                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量票１,計量票２,計量票３";
                        // 伝票日付
                        rowTmp["DENPYOU_DATE"] = "2013/12/10";
                        // 伝票番号
                        rowTmp["DENPYOU_NUMBER"] = "1234567";
                        // 取引先CD
                        rowTmp["TORIHIKISAKI_CD"] = "123456";
                        // 取引先名
                        rowTmp["TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 取引先敬称
                        rowTmp["TORIHIKISAKI_KEISYOU"] = "あい";
                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "123456";
                        // 業者名
                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 業者敬称
                        rowTmp["GYOUSHA_KEISYOU"] = "あい";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "123456";
                        // 現場名
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 現場敬称
                        rowTmp["GENBA_KEISYOU"] = "あい";
                        // 車輌
                        rowTmp["SHARYOU"] = "あいうえおかきくけこさしすせそたちつてと";
                        // バーコード
                        rowTmp["BARCODE"] = "0123456789";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail";

                    // 品名CD
                    dataTableTmp.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmp.Columns.Add("HINMEI_NAME");
                    // 総重量
                    dataTableTmp.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmp.Columns.Add("EMPTY_JYUURYOU");
                    // 調整
                    dataTableTmp.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmp.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmp.Columns.Add("NET_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmp.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmp.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // 正味重量計量時間
                    dataTableTmp.Columns.Add("NET_JYUURYOU_TIME");

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 品名CD
                            rowTmp["HINMEI_CD"] = "123456";
                            // 品名
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 総重量
                            rowTmp["STACK_JYUURYOU"] = "123,456,789,000,123,456";
                            // 空車重量
                            rowTmp["EMPTY_JYUURYOU"] = "123,456,789,000,123,456";
                            // 調整
                            rowTmp["NET_CHOUSEI"] = "123,456,789,000,123,456";
                            // 容器引
                            rowTmp["YOUKI_JYUURYOU"] = "123,456,789,000,123,456";
                            // 正味
                            rowTmp["NET_JYUURYOU"] = "123,456,789,000,123,456";
                            // 総重量計量時間
                            rowTmp["STACK_KEIRYOU_TIME"] = "23:59";
                            // 空車重量計量時間
                            rowTmp["EMPTY_KEIRYOU_TIME"] = "23:59";
                            // 正味重量計量時間
                            rowTmp["NET_JYUURYOU_TIME"] = "23:59";

                            dataTableTmp.Rows.Add(rowTmp);
                        }
                    }

                    this.DataTableList.Add("Detail", dataTableTmp);

                    #endregion - Detail -

                    #region - Footer -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Footer";

                    // 伝票備考
                    dataTableTmp.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmp.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmp.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmp.Columns.Add("KYOTEN_FAX");

                    if (isPrint)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 伝票備考
                        rowTmp["DENPYOU_BIKOU"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 計量情報計量証明項目1
                        rowTmp["KEIRYOU_JYOUHOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目2
                        rowTmp["KEIRYOU_JYOUHOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目3
                        rowTmp["KEIRYOU_JYOUHOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所1
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所2
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点電話
                        rowTmp["KYOTEN_TEL"] = "1234567890123";
                        // 拠点FAX
                        rowTmp["KYOTEN_FAX"] = "1234567890123";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    #endregion - 三つ切り 単品目 -

                    break;
                case OutputTypeDef.MultiH_NoTorihikisaki:   // 三つ切り 複数品目 取引先なし

                    #region - 三つ切り 複数品目 取引先なし -

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    // 計量証明書タイトル
                    dataTableTmp.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmp.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                    // 業者CD
                    dataTableTmp.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmp.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmp.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmp.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmp.Columns.Add("GENBA_NAME");
                    // 車輌
                    dataTableTmp.Columns.Add("SHARYOU");
                    // 総重量
                    dataTableTmp.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmp.Columns.Add("EMPTY_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmp.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmp.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // バーコード
                    dataTableTmp.Columns.Add("BARCODE");

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 計量証明書タイトル
                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量票１,計量票２,計量票３";
                        // 伝票日付
                        rowTmp["DENPYOU_DATE"] = "2013/12/10";
                        // 伝票番号
                        rowTmp["DENPYOU_NUMBER"] = "1234567";
                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "123456";
                        // 業者名
                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 業者敬称
                        rowTmp["GYOUSHA_KEISYOU"] = "あい";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "123456";
                        // 現場名
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 車輌
                        rowTmp["SHARYOU"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 総重量
                        rowTmp["STACK_JYUURYOU"] = "123,456,789,000,123,456";
                        // 空車重量
                        rowTmp["EMPTY_JYUURYOU"] = "123,456,789,000,123,456";
                        // 総重量計量時間
                        rowTmp["STACK_KEIRYOU_TIME"] = "23:59";
                        // 空車重量計量時間
                        rowTmp["EMPTY_KEIRYOU_TIME"] = "23:59";
                        // バーコード
                        rowTmp["BARCODE"] = "0123456789";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail";

                    // 品名CD
                    dataTableTmp.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmp.Columns.Add("HINMEI_NAME");
                    // 調整
                    dataTableTmp.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmp.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmp.Columns.Add("NET_JYUURYOU");
                    // 計量時間
                    dataTableTmp.Columns.Add("KEIRYOU_TIME");

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 品名CD
                            rowTmp["HINMEI_CD"] = "123456";
                            // 品名
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 調整
                            rowTmp["NET_CHOUSEI"] = "123,456,789,000,123,456";
                            // 容器引
                            rowTmp["YOUKI_JYUURYOU"] = "123,456,789,000,123,456";
                            // 正味
                            rowTmp["NET_JYUURYOU"] = "123,456,789,000,123,456";
                            // 計量時間
                            rowTmp["KEIRYOU_TIME"] = "23:59";

                            dataTableTmp.Rows.Add(rowTmp);
                        }
                    }

                    this.DataTableList.Add("Detail", dataTableTmp);

                    #endregion - Detail -

                    #region - Footer -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Footer";

                    // 調整合計
                    dataTableTmp.Columns.Add("NET_CHOSEI_TOTAL");
                    // 容器引合計
                    dataTableTmp.Columns.Add("YOUKI_JYUURYOU_TOTAL");
                    // 正味合計
                    dataTableTmp.Columns.Add("NET_JYUURYOU_TOTAL");

                    // 伝票備考
                    dataTableTmp.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmp.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmp.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmp.Columns.Add("KYOTEN_FAX");

                    if (isPrint)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 調整合計
                        rowTmp["NET_CHOSEI_TOTAL"] = "123,456,789,000,123,456";
                        // 容器引合計
                        rowTmp["YOUKI_JYUURYOU_TOTAL"] = "123,456,789,000,123,456";
                        // 正味合計
                        rowTmp["NET_JYUURYOU_TOTAL"] = "123,456,789,000,123,456";

                        // 伝票備考
                        rowTmp["DENPYOU_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目1
                        rowTmp["KEIRYOU_JYOUHOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目2
                        rowTmp["KEIRYOU_JYOUHOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目3
                        rowTmp["KEIRYOU_JYOUHOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所1
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所2
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点電話
                        rowTmp["KYOTEN_TEL"] = "1234567890123";
                        // 拠点FAX
                        rowTmp["KYOTEN_FAX"] = "1234567890123";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    #endregion - 三つ切り 複数品目 -

                    break;
                case OutputTypeDef.SingleH_NoTorihikisaki:   // 三つ切り 単品目 取引先なし

                    #region - 三つ切り 単品目 取引先なし -

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    // 計量証明書タイトル
                    dataTableTmp.Columns.Add("KEIRYOU_HYOU_TITLE");
                    // 伝票日付
                    dataTableTmp.Columns.Add("DENPYOU_DATE");
                    // 伝票番号
                    dataTableTmp.Columns.Add("DENPYOU_NUMBER");
                    // 業者CD
                    dataTableTmp.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmp.Columns.Add("GYOUSHA_NAME");
                    // 業者敬称
                    dataTableTmp.Columns.Add("GYOUSHA_KEISYOU");
                    // 現場CD
                    dataTableTmp.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmp.Columns.Add("GENBA_NAME");
                    // 業者敬称
                    dataTableTmp.Columns.Add("GENBA_KEISYOU");
                    // 車輌
                    dataTableTmp.Columns.Add("SHARYOU");
                    // バーコード
                    dataTableTmp.Columns.Add("BARCODE");

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 計量証明書タイトル
                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量票１,計量票２,計量票３";
                        // 伝票日付
                        rowTmp["DENPYOU_DATE"] = "2013/12/10";
                        // 伝票番号
                        rowTmp["DENPYOU_NUMBER"] = "1234567";
                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "123456";
                        // 業者名
                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 業者敬称
                        rowTmp["GYOUSHA_KEISYOU"] = "あい";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "123456";
                        // 現場名
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 現場敬称
                        rowTmp["GENBA_KEISYOU"] = "あい";
                        // 車輌
                        rowTmp["SHARYOU"] = "あいうえおかきくけこさしすせそたちつてと";
                        // バーコード
                        rowTmp["BARCODE"] = "0123456789";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail";

                    // 品名CD
                    dataTableTmp.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmp.Columns.Add("HINMEI_NAME");
                    // 総重量
                    dataTableTmp.Columns.Add("STACK_JYUURYOU");
                    // 空車重量
                    dataTableTmp.Columns.Add("EMPTY_JYUURYOU");
                    // 調整
                    dataTableTmp.Columns.Add("NET_CHOUSEI");
                    // 容器引
                    dataTableTmp.Columns.Add("YOUKI_JYUURYOU");
                    // 正味
                    dataTableTmp.Columns.Add("NET_JYUURYOU");
                    // 総重量計量時間
                    dataTableTmp.Columns.Add("STACK_KEIRYOU_TIME");
                    // 空車重量計量時間
                    dataTableTmp.Columns.Add("EMPTY_KEIRYOU_TIME");
                    // 正味重量計量時間
                    dataTableTmp.Columns.Add("NET_JYUURYOU_TIME");

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 品名CD
                            rowTmp["HINMEI_CD"] = "123456";
                            // 品名
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                            // 総重量
                            rowTmp["STACK_JYUURYOU"] = "123,456,789,000,123,456";
                            // 空車重量
                            rowTmp["EMPTY_JYUURYOU"] = "123,456,789,000,123,456";
                            // 調整
                            rowTmp["NET_CHOUSEI"] = "123,456,789,000,123,456";
                            // 容器引
                            rowTmp["YOUKI_JYUURYOU"] = "123,456,789,000,123,456";
                            // 正味
                            rowTmp["NET_JYUURYOU"] = "123,456,789,000,123,456";
                            // 総重量計量時間
                            rowTmp["STACK_KEIRYOU_TIME"] = "23:59";
                            // 空車重量計量時間
                            rowTmp["EMPTY_KEIRYOU_TIME"] = "23:59";
                            // 正味重量計量時間
                            rowTmp["NET_JYUURYOU_TIME"] = "23:59";

                            dataTableTmp.Rows.Add(rowTmp);
                        }
                    }

                    this.DataTableList.Add("Detail", dataTableTmp);

                    #endregion - Detail -

                    #region - Footer -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Footer";

                    // 伝票備考
                    dataTableTmp.Columns.Add("DENPYOU_BIKOU");
                    // 計量情報計量証明項目1
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU1");
                    // 計量情報計量証明項目2
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU2");
                    // 計量情報計量証明項目3
                    dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU3");
                    // 会社名
                    dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                    // 拠点
                    dataTableTmp.Columns.Add("KYOTEN_NAME");
                    // 拠点住所1
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
                    // 拠点住所2
                    dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
                    // 拠点電話
                    dataTableTmp.Columns.Add("KYOTEN_TEL");
                    // 拠点FAX
                    dataTableTmp.Columns.Add("KYOTEN_FAX");

                    if (isPrint)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 伝票備考
                        rowTmp["DENPYOU_BIKOU"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 計量情報計量証明項目1
                        rowTmp["KEIRYOU_JYOUHOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目2
                        rowTmp["KEIRYOU_JYOUHOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量情報計量証明項目3
                        rowTmp["KEIRYOU_JYOUHOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所1
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点住所2
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 拠点電話
                        rowTmp["KYOTEN_TEL"] = "1234567890123";
                        // 拠点FAX
                        rowTmp["KYOTEN_FAX"] = "1234567890123";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    #endregion - 三つ切り 単品目 取引先なし -

                    break;
            }
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int rowNo = 1;
            DataRow row;
            DataTable dataTableDetailTmp;
            string ctrlName = string.Empty;
            bool detailComp = false;

            int detailMaxCount = 0;
            int maxPage = 0;

            int maxRow = 0;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // システム設定情報取得
            sysInfo = this.accessor.GetSysInfo();

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region - Detail -

            switch (this.OutputType)
            {
                case OutputTypeDef.Normal:    // A4 縦三つ切り

                    #region - A4 縦三つ切り -

                    for (int i = 1; i <= ConstMaxDispDetailRowCount; i++)
                    {
                        // No.
                        ctrlName = string.Format("PHN_ROW_NO_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 総重量
                        ctrlName = string.Format("PHN_STACK_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 空車重量
                        ctrlName = string.Format("PHN_EMPTY_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 調整
                        ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 容器引
                        ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 正味
                        ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名CD
                        ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名
                        ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 計量時間
                        ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    dataTableDetailTmp = this.DataTableList["Detail"];
                    detailMaxCount = dataTableDetailTmp.Rows.Count;
                    maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispDetailRowCount);

                    if (maxPage == 0)
                    {
                        maxPage = 1;
                        detailMaxCount = 1;
                        detailComp = true;
                    }

                    maxRow = maxPage * ConstMaxDispDetailRowCount;

                    rowNo = 1;
                    row = this.dataTable.NewRow();
                    for (int i = 0; i < detailMaxCount; i++)
                    {
                        if (!detailComp)
                        {
                            // No.
                            index = dataTableDetailTmp.Columns.IndexOf("ROW_NO");
                            ctrlName = string.Format("PHN_ROW_NO_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 総重量
                            index = dataTableDetailTmp.Columns.IndexOf("STACK_JYUURYOU");
                            ctrlName = string.Format("PHN_STACK_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 空車重量
                            index = dataTableDetailTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                            ctrlName = string.Format("PHN_EMPTY_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 調整
                            index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                            ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 容器引
                            index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                            ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 正味
                            index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                            ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 品名CD
                            string hinmei_cd = "";
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                hinmei_cd = dataTableDetailTmp.Rows[i].ItemArray[index].ToString();
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            //if (hinmei_cd != "")
                            //{
                            //    var hinmeiEntity = this.accessor.GetHinmeiDataByCd(hinmei_cd);
                            //    if (hinmeiEntity != null)
                            //    {
                            //        byteArray = encoding.GetBytes(hinmeiEntity.HINMEI_NAME);//印刷は正式名称を出力する
                            //        if (byteArray.Length > 40)
                            //        {
                            //            row[ctrlName] = encoding.GetString(byteArray, 0, 40);    // No.3278
                            //        }
                            //        else
                            //        {
                            //            row[ctrlName] = hinmeiEntity.HINMEI_NAME;
                            //        }
                            //    }
                            //}
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            // 計量時間
                            index = dataTableDetailTmp.Columns.IndexOf("KEIRYOU_TIME");
                            ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                        }
                        else
                        {
                            // No.
                            index = dataTableDetailTmp.Columns.IndexOf("ROW_NO");
                            ctrlName = string.Format("PHN_ROW_NO_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 総重量
                            index = dataTableDetailTmp.Columns.IndexOf("STACK_JYUURYOU");
                            ctrlName = string.Format("PHN_STACK_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 空車重量
                            index = dataTableDetailTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                            ctrlName = string.Format("PHN_EMPTY_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 調整
                            index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                            ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 容器引
                            index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                            ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 正味
                            index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                            ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 品名CD
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 計量時間
                            index = dataTableDetailTmp.Columns.IndexOf("KEIRYOU_TIME");
                            ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                        }

                        if (rowNo == ConstMaxDispDetailRowCount)
                        {
                            this.dataTable.Rows.Add(row);

                            rowNo = 1;

                            row = this.dataTable.NewRow();
                        }
                        else
                        {
                            rowNo++;
                        }
                    }

                    if (rowNo > 1)
                    {
                        this.dataTable.Rows.Add(row);
                    }
                    #endregion - A4 縦三つ切り -

                    break;
                case OutputTypeDef.MultiH:   // 三つ切り 複数品目

                    #region - 三つ切り 複数品目 -

                    for (int i = 1; i <= ConstMaxDispDetailRowCount; i++)
                    {
                        // 品名CD
                        ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名
                        ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 調整
                        ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 容器引
                        ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 正味
                        ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 計量時間
                        ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    dataTableDetailTmp = this.DataTableList["Detail"];

                    detailMaxCount = dataTableDetailTmp.Rows.Count;
                    maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispDetailRowCount);

                    if (maxPage == 0)
                    {
                        maxPage = 1;
                        detailMaxCount = 1;
                        detailComp = true;
                    }

                    maxRow = maxPage * ConstMaxDispDetailRowCount;
                    rowNo = 1;
                    row = this.dataTable.NewRow();
                    for (int i = 0; i < detailMaxCount; i++)
                    {
                        if (!detailComp)
                        {
                            // 品名CD
                            string hinmei_cd = "";
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                hinmei_cd = dataTableDetailTmp.Rows[i].ItemArray[index].ToString();
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 20151021 katen #13337 品名手入力に関する機能修正 start
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            //if (hinmei_cd != "")
                            //{
                            //    var hinmeiEntity = this.accessor.GetHinmeiDataByCd(hinmei_cd);
                            //    if (hinmeiEntity != null)
                            //    {
                            //        byteArray = encoding.GetBytes(hinmeiEntity.HINMEI_NAME);//印刷は正式名称を出力する
                            //        if (byteArray.Length > 40)
                            //        {
                            //            row[ctrlName] = encoding.GetString(byteArray, 0, 40);    // No.3278
                            //        }
                            //        else
                            //        {
                            //            row[ctrlName] = hinmeiEntity.HINMEI_NAME;
                            //        }
                            //    }
                            //}
                            // 20151021 katen #13337 品名手入力に関する機能修正 end
                            // 調整
                            index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                            ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 容器引
                            index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                            ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 正味
                            index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                            ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 計量時間
                            index = dataTableDetailTmp.Columns.IndexOf("KEIRYOU_TIME");
                            ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                        }
                        else
                        {
                            // 品名CD
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 調整
                            index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                            ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 容器引
                            index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                            ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 正味
                            index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                            ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 計量時間
                            index = dataTableDetailTmp.Columns.IndexOf("KEIRYOU_TIME");
                            ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                        }

                        if (rowNo == ConstMaxDispDetailRowCount)
                        {
                            this.dataTable.Rows.Add(row);

                            rowNo = 1;

                            row = this.dataTable.NewRow();
                        }
                        else
                        {
                            rowNo++;
                        }
                    }

                    if (rowNo > 1)
                    {
                        this.dataTable.Rows.Add(row);
                    }

                    #endregion - 三つ切り 複数品目 -

                    break;

                case OutputTypeDef.SingleH:   // 三つ切り 単品目

                    #region - 三つ切り 単品目 -

                    // 品名CD
                    this.dataTable.Columns.Add("PHN_HINMEI_CD_FLB");
                    // 品名
                    this.dataTable.Columns.Add("PHN_HINMEI_NAME_FLB");
                    // 総重量
                    this.dataTable.Columns.Add("PHN_STACK_JYUURYOU_FLB");
                    // 空車重量
                    this.dataTable.Columns.Add("PHN_EMPTY_JYUURYOU_FLB");
                    // 調整
                    this.dataTable.Columns.Add("PHN_NET_CHOUSEI_FLB");
                    // 容器引
                    this.dataTable.Columns.Add("PHN_YOUKI_JYUURYOU_FLB");
                    // 正味
                    this.dataTable.Columns.Add("PHN_NET_JYUURYOU_FLB");
                    //// 伝票備考
                    //this.dataTable.Columns.Add("PHN_DENPYOU_BIKOU_FLB");
                    // 総重量計量時間
                    this.dataTable.Columns.Add("PHN_STACK_KEIRYOU_TIME_FLB");
                    // 空車重量計量時間
                    this.dataTable.Columns.Add("PHN_EMPTY_KEIRYOU_TIME_FLB");
                    // 正味重量計量時間
                    this.dataTable.Columns.Add("PHN_NET_JYUURYOU_TIME_FLB");

                    dataTableDetailTmp = this.DataTableList["Detail"];

                    rowNo = 1;
                    row = this.dataTable.NewRow();

                    if (dataTableDetailTmp.Rows.Count > 0)
                    {
                        // 品名コード
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_HINMEI_CD_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_HINMEI_CD_FLB"] = string.Empty;
                        }
                        // 品名
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                row["PHN_HINMEI_NAME_FLB"] = encoding.GetString(byteArray, 0, 40);
                            }
                            else
                            {
                                row["PHN_HINMEI_NAME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {   // Null
                            row["PHN_HINMEI_NAME_FLB"] = string.Empty;
                        }
                        // 総重量
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_STACK_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_STACK_JYUURYOU_FLB"] = string.Empty;
                        }
                        // 空車重量
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_EMPTY_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_EMPTY_JYUURYOU_FLB"] = string.Empty;
                        }
                        // 調整
                        index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_NET_CHOUSEI_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_NET_CHOUSEI_FLB"] = string.Empty;
                        }
                        // 容器引
                        index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_YOUKI_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_YOUKI_JYUURYOU_FLB"] = string.Empty;
                        }
                        // 正味
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_NET_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_NET_JYUURYOU_FLB"] = string.Empty;
                        }
                        //// 伝票備考
                        //index = dataTableDetailTmp.Columns.IndexOf("DENPYOU_BIKOU");
                        //row["PHN_DENPYOU_BIKOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        // 総重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_STACK_KEIRYOU_TIME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_STACK_KEIRYOU_TIME_FLB"] = string.Empty;
                        }
                        // 空車重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_EMPTY_KEIRYOU_TIME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_EMPTY_KEIRYOU_TIME_FLB"] = string.Empty;
                        }
                        // 正味重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU_TIME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_NET_JYUURYOU_TIME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_NET_JYUURYOU_TIME_FLB"] = string.Empty;
                        }
                    }
                    else
                    {
                        // 品名コード
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                        row["PHN_HINMEI_CD_FLB"] = string.Empty;
                        // 品名
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                        row["PHN_HINMEI_NAME_FLB"] = string.Empty;
                        // 総重量
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_JYUURYOU");
                        row["PHN_STACK_JYUURYOU_FLB"] = string.Empty;
                        // 空車重量
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                        row["PHN_EMPTY_JYUURYOU_FLB"] = string.Empty;
                        // 調整
                        index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                        row["PHN_NET_CHOUSEI_FLB"] = string.Empty;
                        // 容器引
                        index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                        row["PHN_YOUKI_JYUURYOU_FLB"] = string.Empty;
                        // 正味
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                        row["PHN_NET_JYUURYOU_FLB"] = string.Empty;
                        //// 伝票備考
                        //index = dataTableDetailTmp.Columns.IndexOf("DENPYOU_BIKOU");
                        //row["PHN_DENPYOU_BIKOU_FLB"] = string.Empty;
                        // 総重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                        row["PHN_STACK_KEIRYOU_TIME_FLB"] = string.Empty;
                        // 空車重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                        row["PHN_EMPTY_KEIRYOU_TIME_FLB"] = string.Empty;
                        // 正味重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU_TIME");
                        row["PHN_NET_JYUURYOU_TIME_FLB"] = string.Empty;
                    }

                    this.dataTable.Rows.Add(row);

                    break;

                    #endregion - 三つ切り 単品目 -

                case OutputTypeDef.MultiH_NoTorihikisaki:   // 三つ切り 複数品目 取引先なし

                    #region - 三つ切り 複数品目 取引先なし -

                    for (int i = 1; i <= ConstMaxDispDetailRowCount; i++)
                    {
                        // 品名CD
                        ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名
                        ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 調整
                        ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 容器引
                        ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 正味
                        ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 計量時間
                        ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    dataTableDetailTmp = this.DataTableList["Detail"];

                    detailMaxCount = dataTableDetailTmp.Rows.Count;
                    maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispDetailRowCount);

                    if (maxPage == 0)
                    {
                        maxPage = 1;
                        detailMaxCount = 1;
                        detailComp = true;
                    }

                    maxRow = maxPage * ConstMaxDispDetailRowCount;
                    rowNo = 1;
                    row = this.dataTable.NewRow();
                    for (int i = 0; i < detailMaxCount; i++)
                    {
                        if (!detailComp)
                        {
                            // 品名CD
                            string hinmei_cd = "";
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                hinmei_cd = dataTableDetailTmp.Rows[i].ItemArray[index].ToString();
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            // 調整
                            index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                            ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 容器引
                            index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                            ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 正味
                            index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                            ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 計量時間
                            index = dataTableDetailTmp.Columns.IndexOf("KEIRYOU_TIME");
                            ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                        }
                        else
                        {
                            // 品名CD
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 調整
                            index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                            ctrlName = string.Format("PHN_NET_CHOUSEI_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 容器引
                            index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                            ctrlName = string.Format("PHN_YOUKI_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 正味
                            index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                            ctrlName = string.Format("PHN_NET_JYUURYOU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 計量時間
                            index = dataTableDetailTmp.Columns.IndexOf("KEIRYOU_TIME");
                            ctrlName = string.Format("PHN_KEIRYOU_TIME_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                        }

                        if (rowNo == ConstMaxDispDetailRowCount)
                        {
                            this.dataTable.Rows.Add(row);

                            rowNo = 1;

                            row = this.dataTable.NewRow();
                        }
                        else
                        {
                            rowNo++;
                        }
                    }

                    if (rowNo > 1)
                    {
                        this.dataTable.Rows.Add(row);
                    }

                    #endregion - 三つ切り 複数品目 取引先なし -

                    break;
                case OutputTypeDef.SingleH_NoTorihikisaki:   // 三つ切り 単品目 取引先なし

                    #region - 三つ切り 単品目 取引先なし -

                    // 品名CD
                    this.dataTable.Columns.Add("PHN_HINMEI_CD_FLB");
                    // 品名
                    this.dataTable.Columns.Add("PHN_HINMEI_NAME_FLB");
                    // 総重量
                    this.dataTable.Columns.Add("PHN_STACK_JYUURYOU_FLB");
                    // 空車重量
                    this.dataTable.Columns.Add("PHN_EMPTY_JYUURYOU_FLB");
                    // 調整
                    this.dataTable.Columns.Add("PHN_NET_CHOUSEI_FLB");
                    // 容器引
                    this.dataTable.Columns.Add("PHN_YOUKI_JYUURYOU_FLB");
                    // 正味
                    this.dataTable.Columns.Add("PHN_NET_JYUURYOU_FLB");
                    // 総重量計量時間
                    this.dataTable.Columns.Add("PHN_STACK_KEIRYOU_TIME_FLB");
                    // 空車重量計量時間
                    this.dataTable.Columns.Add("PHN_EMPTY_KEIRYOU_TIME_FLB");
                    // 正味重量計量時間
                    this.dataTable.Columns.Add("PHN_NET_JYUURYOU_TIME_FLB");

                    dataTableDetailTmp = this.DataTableList["Detail"];

                    rowNo = 1;
                    row = this.dataTable.NewRow();

                    if (dataTableDetailTmp.Rows.Count > 0)
                    {
                        // 品名コード
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_HINMEI_CD_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_HINMEI_CD_FLB"] = string.Empty;
                        }
                        // 品名
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                row["PHN_HINMEI_NAME_FLB"] = encoding.GetString(byteArray, 0, 40);
                            }
                            else
                            {
                                row["PHN_HINMEI_NAME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {   // Null
                            row["PHN_HINMEI_NAME_FLB"] = string.Empty;
                        }
                        // 総重量
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_STACK_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_STACK_JYUURYOU_FLB"] = string.Empty;
                        }
                        // 空車重量
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_EMPTY_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_EMPTY_JYUURYOU_FLB"] = string.Empty;
                        }
                        // 調整
                        index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_NET_CHOUSEI_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_NET_CHOUSEI_FLB"] = string.Empty;
                        }
                        // 容器引
                        index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_YOUKI_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_YOUKI_JYUURYOU_FLB"] = string.Empty;
                        }
                        // 正味
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_NET_JYUURYOU_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_NET_JYUURYOU_FLB"] = string.Empty;
                        }
                        // 総重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_STACK_KEIRYOU_TIME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_STACK_KEIRYOU_TIME_FLB"] = string.Empty;
                        }
                        // 空車重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_EMPTY_KEIRYOU_TIME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_EMPTY_KEIRYOU_TIME_FLB"] = string.Empty;
                        }
                        // 正味重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU_TIME");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[0].ItemArray[index]))
                        {
                            row["PHN_NET_JYUURYOU_TIME_FLB"] = dataTableDetailTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {   // Null
                            row["PHN_NET_JYUURYOU_TIME_FLB"] = string.Empty;
                        }
                    }
                    else
                    {
                        // 品名コード
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                        row["PHN_HINMEI_CD_FLB"] = string.Empty;
                        // 品名
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                        row["PHN_HINMEI_NAME_FLB"] = string.Empty;
                        // 総重量
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_JYUURYOU");
                        row["PHN_STACK_JYUURYOU_FLB"] = string.Empty;
                        // 空車重量
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                        row["PHN_EMPTY_JYUURYOU_FLB"] = string.Empty;
                        // 調整
                        index = dataTableDetailTmp.Columns.IndexOf("NET_CHOUSEI");
                        row["PHN_NET_CHOUSEI_FLB"] = string.Empty;
                        // 容器引
                        index = dataTableDetailTmp.Columns.IndexOf("YOUKI_JYUURYOU");
                        row["PHN_YOUKI_JYUURYOU_FLB"] = string.Empty;
                        // 正味
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU");
                        row["PHN_NET_TIME_FLB"] = string.Empty;
                        // 総重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                        row["PHN_STACK_KEIRYOU_TIME_FLB"] = string.Empty;
                        // 空車重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                        row["PHN_EMPTY_KEIRYOU_TIME_FLB"] = string.Empty;
                        // 正味重量計量時間
                        index = dataTableDetailTmp.Columns.IndexOf("NET_JYUURYOU_TIME");
                        row["PHN_NET_JYUURYOU_TIME_FLB"] = string.Empty;
                    }

                    this.dataTable.Rows.Add(row);

                    #endregion - 三つ切り 単品目 取引先なし -

                    break;
            }

            #endregion - Detail -

            this.SetRecord(this.dataTable);
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
        }

        /// <summary>帳票出力用データの設定処理を実行する</summary>
        private void SetChouhyouInfo()
        {
            int index;
            DataTable dataTableHeaderTmp = this.DataTableList["Header"];
            DataTable dataTableFooterTmp = this.DataTableList["Footer"];
            string[] titleTmp = null;
            string ctrlName = string.Empty;
            string tmp;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            switch (this.OutputType)
            {
                case OutputTypeDef.Normal:    // A4 縦三つ切り

                    #region - A4 縦三つ切り -

                    // 計量証明書タイトル(カンマ区切りの３個)
                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        index = dataTableHeaderTmp.Columns.IndexOf("KEIRYOU_HYOU_TITLE");
                        tmp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];

                        titleTmp = tmp.Split(',');
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    for (int pos = 1; pos <= 3; pos++)
                    {
                        #region - Header -

                        if (dataTableHeaderTmp.Rows.Count > 0)
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, titleTmp[pos - 1]);

                            // 担当者名
                            index = dataTableHeaderTmp.Columns.IndexOf("TANTOU");
                            ctrlName = string.Format("DH_TANTOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }

                            #region 取引先関連
                            // 受入、出荷入力から出力する場合は取引先欄に業者情報を出力する。
                            switch (this.DispTypeForNormal)
                            {
                                case DispTypeForNormalDef.Torihikisaki:
                                    #region 表示タイプ：取引先
                                    // 取引先CD
                                    string torihikisaki_cd = "";
                                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                                    {
                                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                        torihikisaki_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                    }
                                    else
                                    {   // Null
                                        this.SetFieldName(ctrlName, string.Empty);
                                    }
                                    // 取引先名
                                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                                    this.SetFieldName(ctrlName, string.Empty);
                                    if (torihikisaki_cd != "")
                                    {
                                        var torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisaki_cd);
                                        if (torihikisakiEntity != null)
                                        {
                                            if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU))
                                            {
                                                byteArray = encoding.GetBytes(torihikisakiEntity.TORIHIKISAKI_NAME1);//印刷は正式名称を出力する
                                                if (byteArray.Length > 82)
                                                {
                                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));
                                                }
                                                else
                                                {
                                                    this.SetFieldName(ctrlName, torihikisakiEntity.TORIHIKISAKI_NAME1);
                                                }
                                            }
                                            else
                                            {
                                                // 取引先マスタの取引先名略称と入力された取引先名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                                this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                            }
                                        }
                                    }

                                    // 取引先敬称
                                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                                    {
                                        var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                        this.SetFieldName(ctrlName, keisyou);
                                    }
                                    else
                                    {   // Null
                                        this.SetFieldName(ctrlName, string.Empty);
                                    }
                                    #endregion
                                    break;

                                case DispTypeForNormalDef.Gyousha:
                                    #region 表示タイプ：業者
                                    // 取引先ラベルを業者へ変更
                                    ctrlName = string.Format("DH_TORIHIKISAKI_NAME_FLB{0}", pos);
                                    this.SetFieldName(ctrlName, "業者");

                                    // 業者CD
                                    string gyousha_cd = "";
                                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                                    if (index >= 0)
                                    {
                                        ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                                        {
                                            this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                            gyousha_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                                        }
                                        else
                                        {   // Null
                                            this.SetFieldName(ctrlName, string.Empty);
                                        }
                                    }
                                    // 業者名
                                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                                    if (index >= 0)
                                    {
                                        ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                                        this.SetFieldName(ctrlName, string.Empty);
                                        if (gyousha_cd != "")
                                        {
                                            var gyoushaEntity = this.accessor.GetGyousha(gyousha_cd);
                                            if (gyoushaEntity != null)
                                            {
                                                if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(gyoushaEntity.GYOUSHA_NAME_RYAKU))
                                                {
                                                    byteArray = encoding.GetBytes(gyoushaEntity.GYOUSHA_NAME1);//印刷は正式名称を出力する
                                                    if (byteArray.Length > 82)
                                                    {
                                                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));
                                                    }
                                                    else
                                                    {
                                                        this.SetFieldName(ctrlName, gyoushaEntity.GYOUSHA_NAME1);
                                                    }
                                                }
                                                else
                                                {
                                                    // 取引先マスタの取引先名略称と入力された取引先名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                                    this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                                }
                                            }
                                        }
                                    }

                                    // 業者敬称
                                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                                    if (index >= 0)
                                    {
                                        ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                                        {
                                            var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                            this.SetFieldName(ctrlName, keisyou);
                                        }
                                        else
                                        {   // Null
                                            this.SetFieldName(ctrlName, string.Empty);
                                        }
                                    }
                                    #endregion
                                    break;
                            }
                            #endregion 取引先関連

                            // 伝票No.
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 10));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 乗員
                            index = dataTableHeaderTmp.Columns.IndexOf("JYOUIN");
                            ctrlName = string.Format("DH_JYOUIN_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 車番
                            index = dataTableHeaderTmp.Columns.IndexOf("SHABAN");
                            ctrlName = string.Format("DH_SHABAN_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                        }
                        else
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);

                            // 担当者名
                            index = dataTableHeaderTmp.Columns.IndexOf("TANTOU");
                            ctrlName = string.Format("DH_TANTOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);

                            switch (this.DispTypeForNormal)
                            {
                                case DispTypeForNormalDef.Torihikisaki:
                                    // 取引先CD
                                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                                    this.SetFieldName(ctrlName, string.Empty);
                                    // 取引先名
                                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                                    this.SetFieldName(ctrlName, string.Empty);
                                    // 取引先敬称
                                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                                    this.SetFieldName(ctrlName, string.Empty);
                                    break;

                                case DispTypeForNormalDef.Gyousha:
                                    // 取引先CD
                                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                                    this.SetFieldName(ctrlName, string.Empty);
                                    // 取引先名
                                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                                    this.SetFieldName(ctrlName, string.Empty);
                                    // 取引先敬称
                                    index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                                    ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                                    this.SetFieldName(ctrlName, string.Empty);
                                    break;
                            }
                            // 伝票No.
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 乗員
                            index = dataTableHeaderTmp.Columns.IndexOf("JYOUIN");
                            ctrlName = string.Format("DH_JYOUIN_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 車番
                            index = dataTableHeaderTmp.Columns.IndexOf("SHABAN");
                            ctrlName = string.Format("DH_SHABAN_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Header -

                        #region - Footer -
                        if (dataTableFooterTmp.Rows.Count > 0)
                        {


                            // 現場CD
                            string genba_cd = "";
                            index = dataTableFooterTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("DF_GENBA_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                genba_cd = (string)dataTableFooterTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場名
                            index = dataTableFooterTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = string.Format("DF_GENBA_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                //if (byteArray.Length > 40)    // No.3279
                                if (byteArray.Length > 80)    // No.3279
                                {
                                    //this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));    // No.3279
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 80));    // No.3279
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }


                            // 正味合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_NET_JYUURYOU_TOTAL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }

                            // 備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("DF_DENPYOU_BIKOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                        }
                        else
                        {
                            // 現場名
                            index = dataTableFooterTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = string.Format("DF_GENBA_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 正味合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_NET_JYUURYOU_TOTAL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);

                            // 備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("DF_DENPYOU_BIKOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Footer -
                    }

                    #endregion - A4 縦三つ切り -

                    break;
                case OutputTypeDef.MultiH:   // 三つ切り 複数品目

                    #region - 三つ切り 複数品目 -

                    // 計量証明書タイトル(カンマ区切りの３個)
                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        index = dataTableHeaderTmp.Columns.IndexOf("KEIRYOU_HYOU_TITLE");
                        tmp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];

                        titleTmp = tmp.Split(',');
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    for (int pos = 1; pos <= 3; pos++)
                    {
                        #region - Header -

                        if (dataTableHeaderTmp.Rows.Count > 0)
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, titleTmp[pos - 1]);
                            // システム設定-[計量伝票バーコード]より、計量票タイトル表示位置設定
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Left);
                            }
                            else
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Middle);
                            }
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 10));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 取引先CD
                            string torihikisaki_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                torihikisaki_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                            ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            if (torihikisaki_cd != "")
                            {
                                var torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisaki_cd);
                                if (torihikisakiEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(torihikisakiEntity.TORIHIKISAKI_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)  // No.2996(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));  // No.2996(改行込み)
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, torihikisakiEntity.TORIHIKISAKI_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 取引先マスタの取引先名略称と入力された取引先名が異なる場合（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }
                            // 取引先敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                            ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者CD
                            string gyousya_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = string.Format("DH_GYOUSHA_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                gyousya_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = string.Format("DH_GYOUSHA_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);

                            if (gyousya_cd != "")
                            {
                                var gyousyaEntity = this.accessor.GetGyousha(gyousya_cd);
                                if (gyousyaEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(gyousyaEntity.GYOUSHA_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(gyousyaEntity.GYOUSHA_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)  // No.2996(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));  // No.2996(改行込み)
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, gyousyaEntity.GYOUSHA_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 業者マスタの業者名略称と入力された業者名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = string.Format("DH_GYOUSHA_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("DH_GENBA_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = string.Format("DH_GENBA_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());

                                if (byteArray.Length > 82)    // No.3279(改行込み)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));    // No.3279(改行込み)
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }

                            // 現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = string.Format("DH_GENBA_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }



                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = string.Format("DH_SHARYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 総重量
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_JYUURYOU");
                            ctrlName = string.Format("DH_STACK_JYUURYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 空車重量
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                            ctrlName = string.Format("DH_EMPTY_JYUURYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 総重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_STACK_KEIRYOU_TIME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 空車重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_EMPTY_KEIRYOU_TIME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // システム設定-[計量伝票バーコード]より、バーコード表示有無
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldVisible(ctrlName, true);
                            }
                            else
                            {
                                this.SetFieldVisible(ctrlName, false);
                            }
                        }
                        else
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 取引先CD
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                            ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 取引先敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                            ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = string.Format("DH_GYOUSHA_CD_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = string.Format("DH_GYOUSHA_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = string.Format("DH_GYOUSHA_KEISYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("DH_GENBA_CD_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = string.Format("DH_GENBA_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = string.Format("DH_GENBA_KEISYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = string.Format("DH_SHARYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 総重量
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_JYUURYOU");
                            ctrlName = string.Format("DH_STACK_JYUURYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 空車重量
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                            ctrlName = string.Format("DH_EMPTY_JYUURYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 総重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_STACK_KEIRYOU_TIME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 空車重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_EMPTY_KEIRYOU_TIME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Header -

                        #region - Footer -

                        if (dataTableFooterTmp.Rows.Count > 0)
                        {
                            // 調整合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_CHOSEI_TOTAL");
                            ctrlName = string.Format("DF_NET_CHOSEI_TOTAL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 容器引合計
                            index = dataTableFooterTmp.Columns.IndexOf("YOUKI_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_YOUKI_JYUURYOU_TOTAL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 正味合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_NET_JYUURYOU_TOTAL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("DF_DENPYOU_BIKOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                        }
                        else
                        {
                            // 調整合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_CHOSEI_TOTAL");
                            ctrlName = string.Format("DF_NET_CHOSEI_TOTAL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 容器引合計
                            index = dataTableFooterTmp.Columns.IndexOf("YOUKI_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_YOUKI_JYUURYOU_TOTAL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 正味合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_NET_JYUURYOU_TOTAL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);

                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("DF_DENPYOU_BIKOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Footer -
                    }

                    #endregion - 三つ切り 複数品目 -

                    break;
                case OutputTypeDef.SingleH:   // 三つ切り 単品目

                    #region - 三つ切り 単品目 -

                    // 計量証明書タイトル(カンマ区切りの３個)
                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        index = dataTableHeaderTmp.Columns.IndexOf("KEIRYOU_HYOU_TITLE");
                        tmp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];

                        titleTmp = tmp.Split(',');
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    for (int pos = 1; pos <= 3; pos++)
                    {
                        #region - Header -

                        if (dataTableHeaderTmp.Rows.Count > 0)
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, titleTmp[pos - 1]);
                            // システム設定-[計量伝票バーコード]より、計量票タイトル表示位置設定
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Left);
                            }
                            else
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Middle);
                            }
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 10));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 取引先CD
                            string torihikisaki_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                torihikisaki_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                            ctrlName = "PHN_TORIHIKISAKI_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            if (torihikisaki_cd != "")
                            {
                                var torihikisakiEntity = this.accessor.GetTorihikisaki(torihikisaki_cd);
                                if (torihikisakiEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(torihikisakiEntity.TORIHIKISAKI_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(torihikisakiEntity.TORIHIKISAKI_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)      // No.2996(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, torihikisakiEntity.TORIHIKISAKI_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 取引先マスタの取引先名略称と入力された取引先名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }
                            // 取引先敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                            ctrlName = "PHN_TORIHIKISAKI_KEISYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者CD
                            string gyousya_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHN_GYOUSHA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                gyousya_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = "PHN_GYOUSHA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            if (gyousya_cd != "")
                            {
                                var gyousyaEntity = this.accessor.GetGyousha(gyousya_cd);
                                if (gyousyaEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(gyousyaEntity.GYOUSHA_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(gyousyaEntity.GYOUSHA_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)      // No.2996(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));      // No.2996(改行込み)
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, gyousyaEntity.GYOUSHA_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 業者マスタの業者名略称と入力された業者名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }

                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = "PHN_GYOUSHA_KEISYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場CD
                            string genba_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHN_GENBA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                genba_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = "PHN_GENBA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            if (gyousya_cd != "" && genba_cd != "")
                            {
                                var genbaEntity = this.accessor.GetGenba(gyousya_cd, genba_cd);
                                if (genbaEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(genbaEntity.GENBA_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(genbaEntity.GENBA_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)    // No.3279(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));    // No.3279
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, genbaEntity.GENBA_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 現場マスタの現場名略称と入力された現場名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }
                            // 現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = "PHN_GENBA_KEISYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }

                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = "PHN_SHARYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // システム設定-[計量伝票バーコード]より、バーコード表示有無
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldVisible(ctrlName, true);
                            }
                            else
                            {
                                this.SetFieldVisible(ctrlName, false);
                            }
                        }
                        else
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 取引先CD
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                            ctrlName = "PHN_TORIHIKISAKI_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 取引先敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                            ctrlName = "PHN_TORIHIKISAKI_KEISYOU_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHN_GYOUSHA_CD_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = "PHN_GYOUSHA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = "PHN_GYOUSHA_KEISYOU_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHN_GENBA_CD_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = "PHN_GENBA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            //現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = "PHN_GENBA_KEISYOU_FLB";
                            this.SetFieldName(ctrlName, string.Empty);

                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = "PHN_SHARYOU_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Header -

                        #region - Footer -

                        if (dataTableFooterTmp.Rows.Count > 0)
                        {
                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("PHN_DENPYOU_BIKOU_FLB");
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                        }
                        else
                        {
                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("PHN_DENPYOU_BIKOU_FLB");
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Footer -
                    }

                    #endregion - 三つ切り 単品目 -

                    break;
                case OutputTypeDef.MultiH_NoTorihikisaki:   // 三つ切り 複数品目 取引先なし

                    #region - 三つ切り 複数品目 取引先なし -

                    // 計量証明書タイトル(カンマ区切りの３個)
                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        index = dataTableHeaderTmp.Columns.IndexOf("KEIRYOU_HYOU_TITLE");
                        tmp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];

                        titleTmp = tmp.Split(',');
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    for (int pos = 1; pos <= 3; pos++)
                    {
                        #region - Header -

                        if (dataTableHeaderTmp.Rows.Count > 0)
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, titleTmp[pos - 1]);
                            // システム設定-[計量伝票バーコード]より、計量票タイトル表示位置設定
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Left);
                            }
                            else
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Middle);
                            }
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 10));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者CD
                            string gyousya_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = string.Format("DH_GYOUSHA_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                gyousya_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = string.Format("DH_GYOUSHA_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);

                            if (gyousya_cd != "")
                            {
                                var gyousyaEntity = this.accessor.GetGyousha(gyousya_cd);
                                if (gyousyaEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(gyousyaEntity.GYOUSHA_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(gyousyaEntity.GYOUSHA_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)  // No.2996(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));  // No.2996(改行込み)
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, gyousyaEntity.GYOUSHA_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 業者マスタの業者名略称と入力された業者名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = string.Format("DH_GYOUSHA_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("DH_GENBA_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = string.Format("DH_GENBA_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());

                                if (byteArray.Length > 82)    // No.3279(改行込み)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));    // No.3279(改行込み)
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }

                            // 現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = string.Format("DH_GENBA_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }

                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = string.Format("DH_SHARYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 総重量
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_JYUURYOU");
                            ctrlName = string.Format("DH_STACK_JYUURYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 空車重量
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                            ctrlName = string.Format("DH_EMPTY_JYUURYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 総重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_STACK_KEIRYOU_TIME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 空車重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_EMPTY_KEIRYOU_TIME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // システム設定-[計量伝票バーコード]より、バーコード表示有無
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldVisible(ctrlName, true);
                            }
                            else
                            {
                                this.SetFieldVisible(ctrlName, false);
                            }
                        }
                        else
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = string.Format("DH_GYOUSHA_CD_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = string.Format("DH_GYOUSHA_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = string.Format("DH_GYOUSHA_KEISYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("DH_GENBA_CD_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = string.Format("DH_GENBA_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = string.Format("DH_GENBA_KEISYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = string.Format("DH_SHARYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 総重量
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_JYUURYOU");
                            ctrlName = string.Format("DH_STACK_JYUURYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 空車重量
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_JYUURYOU");
                            ctrlName = string.Format("DH_EMPTY_JYUURYOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 総重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("STACK_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_STACK_KEIRYOU_TIME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 空車重量計量時間
                            index = dataTableHeaderTmp.Columns.IndexOf("EMPTY_KEIRYOU_TIME");
                            ctrlName = string.Format("DH_EMPTY_KEIRYOU_TIME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Header -

                        #region - Footer -

                        if (dataTableFooterTmp.Rows.Count > 0)
                        {
                            // 調整合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_CHOSEI_TOTAL");
                            ctrlName = string.Format("DF_NET_CHOSEI_TOTAL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 容器引合計
                            index = dataTableFooterTmp.Columns.IndexOf("YOUKI_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_YOUKI_JYUURYOU_TOTAL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 正味合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_NET_JYUURYOU_TOTAL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("DF_DENPYOU_BIKOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                        }
                        else
                        {
                            // 調整合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_CHOSEI_TOTAL");
                            ctrlName = string.Format("DF_NET_CHOSEI_TOTAL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 容器引合計
                            index = dataTableFooterTmp.Columns.IndexOf("YOUKI_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_YOUKI_JYUURYOU_TOTAL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 正味合計
                            index = dataTableFooterTmp.Columns.IndexOf("NET_JYUURYOU_TOTAL");
                            ctrlName = string.Format("DF_NET_JYUURYOU_TOTAL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);

                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("DF_DENPYOU_BIKOU_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Footer -
                    }

                    #endregion - 三つ切り 複数品目 取引先なし -

                    break;
                case OutputTypeDef.SingleH_NoTorihikisaki:   // 三つ切り 単品目 取引先なし

                    #region - 三つ切り 単品目 取引先なし -

                    // 計量証明書タイトル(カンマ区切りの３個)
                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        index = dataTableHeaderTmp.Columns.IndexOf("KEIRYOU_HYOU_TITLE");
                        tmp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];

                        titleTmp = tmp.Split(',');
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    for (int pos = 1; pos <= 3; pos++)
                    {
                        #region - Header -

                        if (dataTableHeaderTmp.Rows.Count > 0)
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, titleTmp[pos - 1]);
                            // システム設定-[計量伝票バーコード]より、計量票タイトル表示位置設定
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Left);
                            }
                            else
                            {
                                this.SetFieldAlign(ctrlName, ALIGN_TYPE.Middle);
                            }
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 10));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者CD
                            string gyousya_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHN_GYOUSHA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                gyousya_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = "PHN_GYOUSHA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            if (gyousya_cd != "")
                            {
                                var gyousyaEntity = this.accessor.GetGyousha(gyousya_cd);
                                if (gyousyaEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(gyousyaEntity.GYOUSHA_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(gyousyaEntity.GYOUSHA_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)      // No.2996(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));      // No.2996(改行込み)
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, gyousyaEntity.GYOUSHA_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 業者マスタの業者名略称と入力された業者名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }

                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = "PHN_GYOUSHA_KEISYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場CD
                            string genba_cd = "";
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHN_GENBA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                genba_cd = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = "PHN_GENBA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            if (gyousya_cd != "" && genba_cd != "")
                            {
                                var genbaEntity = this.accessor.GetGenba(gyousya_cd, genba_cd);
                                if (genbaEntity != null)
                                {
                                    if (dataTableHeaderTmp.Rows[0].ItemArray[index].Equals(genbaEntity.GENBA_NAME_RYAKU))
                                    {
                                        byteArray = encoding.GetBytes(genbaEntity.GENBA_NAME1);//印刷は正式名称を出力する
                                        if (byteArray.Length > 82)    // No.3279(改行込み)
                                        {
                                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 82));    // No.3279
                                        }
                                        else
                                        {
                                            this.SetFieldName(ctrlName, genbaEntity.GENBA_NAME1);
                                        }
                                    }
                                    else
                                    {
                                        // 現場マスタの現場名略称と入力された現場名が異なる場合は、入力名称を出力（諸口の場合にあり得る）
                                        this.SetFieldName(ctrlName, dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                    }
                                }
                            }
                            // 現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = "PHN_GENBA_KEISYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                var keisyou = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                                this.SetFieldName(ctrlName, keisyou);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }

                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = "PHN_SHARYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // システム設定-[計量伝票バーコード]より、バーコード表示有無
                            if (this.CheckBarcodeInfo())
                            {
                                this.SetFieldVisible(ctrlName, true);
                            }
                            else
                            {
                                this.SetFieldVisible(ctrlName, false);
                            }
                        }
                        else
                        {
                            // 計量証明書タイトル
                            ctrlName = string.Format("DH_KEIRYOU_HYOU_TITLE_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票日付
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                            ctrlName = string.Format("DH_DENPYOU_DATE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 伝票番号
                            index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_NUMBER");
                            ctrlName = string.Format("DH_DENPYOU_NUMBER_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHN_GYOUSHA_CD_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = "PHN_GYOUSHA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = "PHN_GYOUSHA_KEISYOU_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHN_GENBA_CD_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = "PHN_GENBA_NAME_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            //現場敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_KEISYOU");
                            ctrlName = "PHN_GENBA_KEISYOU_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = "PHN_SHARYOU_FLB";
                            this.SetFieldName(ctrlName, string.Empty);
                            // バーコード
                            index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                            ctrlName = string.Format("BARCODE_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Header -

                        #region - Footer -

                        if (dataTableFooterTmp.Rows.Count > 0)
                        {
                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("PHN_DENPYOU_BIKOU_FLB");
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 50)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                                }
                                else
                                {
                                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                                }
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                        }
                        else
                        {
                            // 伝票備考
                            index = dataTableFooterTmp.Columns.IndexOf("DENPYOU_BIKOU");
                            ctrlName = string.Format("PHN_DENPYOU_BIKOU_FLB");
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目1
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                            ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所1
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点住所2
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                            ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点電話
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                            ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                            // 拠点FAX
                            index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                            ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Footer -
                    }

                    #endregion - 三つ切り 単品目 取引先なし -

                    break;
            }
        }

        /// <summary>バーコード設定取得</summary>
        public bool CheckBarcodeInfo()
        {
            if (!this.sysInfo.KEIRYOU_BARCODE_KBN.IsNull)
            {
                if (this.sysInfo.KEIRYOU_BARCODE_KBN.Value == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
