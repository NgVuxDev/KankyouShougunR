using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace FixChouhyou
{
    #region - Class -

    /// <summary>(R354・R549・R550)計量票を表すクラス・コントロール</summary>
    public class ReportInfoR354_R549_R550 : ReportInfoBase
    {
        #region - Fields -

        /// <summary>Detail部に表示するレコート最大数を保持するフィールド</summary>
        private const int ConstMaxDispDetailRowCount = 5;

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR354_R549_R550"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR354_R549_R550(WINDOW_ID windowID)
        {
            this.windowID = windowID;

            /// <summary>出力タイプを保持するプロパティ</summary>
            this.OutputType = OutputTypeDef.Normal;
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
        }

        #endregion - Enums -

        #region - Properties -

        /// <summary>出力タイプを保持するプロパティ</summary>
        public OutputTypeDef OutputType { get; set; }

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
                    dataTableTmp.Columns.Add("BUMON_NAME");
                    dataTableTmp.Columns.Add("PAGE");

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量証明書,計量表,計量表（控）";
                        rowTmp["TANTOU"] = "あいうえおかきくけこ";

                        rowTmp["TORIHIKISAKI_CD"] = "1234567890";
                        rowTmp["TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["TORIHIKISAKI_KEISYOU"] = "あいうえおかきくけこ";
                        rowTmp["DENPYOU_NUMBER"] = "1234567890";
                        rowTmp["JYOUIN"] = "12345";
                        rowTmp["SHABAN"] = "1234567890123456789012345";
                        rowTmp["DENPYOU_DATE"] = "2013/12/10 12:00:00";
                        rowTmp["BUMON_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
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

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            rowTmp["ROW_NO"] = ((i + 1) * 10000).ToString();
                            rowTmp["STACK_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["EMPTY_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["NET_CHOUSEI"] = "123,456,789,000,123,456";
                            rowTmp["YOUKI_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["NET_JYUURYOU"] = "123,456,789,000,123,456";
                            rowTmp["HINMEI_CD"] = "1234567890";
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";

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

                        rowTmp["GENBA_CD"] = "1234567890";
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそ";
                        rowTmp["NET_JYUURYOU_TOTAL"] = "123,456,789,000,123,456";

                        rowTmp["DENPYOU_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KEIRYOU_JYOUHOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KEIRYOU_JYOUHOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KEIRYOU_JYOUHOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        rowTmp["KYOTEN_TEL"] = "123456789012345";
                        rowTmp["KYOTEN_FAX"] = "123456789012345";

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

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 計量証明書タイトル
                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量票１,計量票２,計量票３";
                        // 伝票日付
                        rowTmp["DENPYOU_DATE"] = "2013/12/10 12:00:00";
                        // 伝票番号
                        rowTmp["DENPYOU_NUMBER"] = "1234567890";
                        // 取引先CD
                        rowTmp["TORIHIKISAKI_CD"] = "1234567890";
                        // 取引先名
                        rowTmp["TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 取引先敬称
                        rowTmp["TORIHIKISAKI_KEISYOU"] = "あいうえおかきくけこ";
                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "1234567890";
                        // 業者名
                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 業者敬称
                        rowTmp["GYOUSHA_KEISYOU"] = "あいうえおかきくけこ";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "1234567890";
                        // 現場名
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 車輌
                        rowTmp["SHARYOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 総重量
                        rowTmp["STACK_JYUURYOU"] = "123,456,789,000,123,456";
                        // 空車重量
                        rowTmp["EMPTY_JYUURYOU"] = "123,456,789,000,123,456";

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

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 品名CD
                            rowTmp["HINMEI_CD"] = "1234567890";
                            // 品名
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                            // 調整
                            rowTmp["NET_CHOUSEI"] = "123,456,789,000,123,456";
                            // 容器引
                            rowTmp["YOUKI_JYUURYOU"] = "123,456,789,000,123,456";
                            // 正味
                            rowTmp["NET_JYUURYOU"] = "123,456,789,000,123,456";

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
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点住所1
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点住所2
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点電話
                        rowTmp["KYOTEN_TEL"] = "123456789012345";
                        // 拠点FAX
                        rowTmp["KYOTEN_FAX"] = "123456789012345";

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
                    // 車輌
                    dataTableTmp.Columns.Add("SHARYOU");

                    if (isPrintH)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 計量証明書タイトル
                        rowTmp["KEIRYOU_HYOU_TITLE"] = "計量票１,計量票２,計量票３";
                        // 伝票日付
                        rowTmp["DENPYOU_DATE"] = "2013/12/10 12:00:00";
                        // 伝票番号
                        rowTmp["DENPYOU_NUMBER"] = "1234567890";
                        // 取引先CD
                        rowTmp["TORIHIKISAKI_CD"] = "1234567890";
                        // 取引先名
                        rowTmp["TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 取引先敬称
                        rowTmp["TORIHIKISAKI_KEISYOU"] = "あいうえおかきくけこ";
                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "1234567890";
                        // 業者名
                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 業者敬称
                        rowTmp["GYOUSHA_KEISYOU"] = "あいうえおかきくけこ";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "1234567890";
                        // 現場名
                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 車輌
                        rowTmp["SHARYOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";

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

                    if (isPrint)
                    {
                        for (int i = 0; i < 1; i++)
                        {
                            rowTmp = dataTableTmp.NewRow();

                            // 品名CD
                            rowTmp["HINMEI_CD"] = "1234567890";
                            // 品名
                            rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
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
                        rowTmp["DENPYOU_BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点
                        rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点住所1
                        rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点住所2
                        rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 拠点電話
                        rowTmp["KYOTEN_TEL"] = "123456789012345";
                        // 拠点FAX
                        rowTmp["KYOTEN_FAX"] = "123456789012345";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    #endregion - 三つ切り 単品目 -

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
                    }

                    dataTableDetailTmp = this.DataTableList["Detail"];
                    detailMaxCount = dataTableDetailTmp.Rows.Count;
                    maxPage = (int)Math.Ceiling((double)detailMaxCount / ConstMaxDispDetailRowCount);

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
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
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
                    }

                    dataTableDetailTmp = this.DataTableList["Detail"];

                    detailMaxCount = dataTableDetailTmp.Rows.Count;
                    maxPage = (int)Math.Ceiling((double)detailMaxCount / ConstMaxDispDetailRowCount);

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
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                            ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {   // Null
                                row[ctrlName] = string.Empty;
                            }
                            // 品名
                            index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                            ctrlName = string.Format("PHN_HINMEI_NAME_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 30)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 30);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
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
                    }

                    this.dataTable.Rows.Add(row);

                    break;

                    #endregion - 三つ切り 単品目 -
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

                            // 取引先CD
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                            ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
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
                            // 取引先敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                            ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 6)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 6));
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
                            // 部門名
                            index = dataTableHeaderTmp.Columns.IndexOf("BUMON_NAME");
                            ctrlName = string.Format("DH_BUMON_NAME_CTL{0}", pos);
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
                            // 部門名
                            index = dataTableHeaderTmp.Columns.IndexOf("BUMON_NAME");
                            ctrlName = string.Format("DH_BUMON_NAME_CTL{0}", pos);
                            this.SetFieldName(ctrlName, string.Empty);
                        }
                        #endregion - Header -

                        #region - Footer -
                        if (dataTableFooterTmp.Rows.Count > 0)
                        {
                            // 現場CD
                            index = dataTableFooterTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("DF_GENBA_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
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
                            // 計量情報計量証明項目2
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU2_CTL{0}", pos);
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
                            // 計量情報計量証明項目3
                            index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                            ctrlName = string.Format("DF_KEIRYOU_JYOUHOU3_CTL{0}", pos);
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
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
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
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = string.Format("DH_TORIHIKISAKI_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                            ctrlName = string.Format("DH_TORIHIKISAKI_NAME_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
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
                            // 取引先敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                            ctrlName = string.Format("DH_TORIHIKISAKI_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 6)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 6));
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
                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = string.Format("DH_GYOUSHA_CD_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Null
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = string.Format("DH_GYOUSHA_NAME_CTL{0}", pos);
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
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = string.Format("DH_GYOUSHA_KEISYOU_CTL{0}", pos);
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 6)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 6));
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
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
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
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                            ctrlName = "PHN_TORIHIKISAKI_NAME_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
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
                            // 取引先敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_KEISYOU");
                            ctrlName = "PHN_TORIHIKISAKI_KEISYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 6)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 6));
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
                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHN_GYOUSHA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = "PHN_GYOUSHA_NAME_FLB";
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
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 業者敬称
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_KEISYOU");
                            ctrlName = "PHN_GYOUSHA_KEISYOU_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 6)
                                {
                                    this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 6));
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
                            // 現場CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHN_GENBA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
                            }
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = "PHN_GENBA_NAME_FLB";
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
                            {   // Nuill
                                this.SetFieldName(ctrlName, string.Empty);
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
                            // 車輌
                            index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU");
                            ctrlName = "PHN_SHARYOU_FLB";
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
                            // 会社名
                            index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = string.Format("DF_CORP_RYAKU_NAME_VLB{0}", pos);
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
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
