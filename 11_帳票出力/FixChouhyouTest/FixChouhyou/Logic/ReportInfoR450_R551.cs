using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Dao;
using System.Globalization;

namespace FixChouhyou
{
    #region - Class -

    /// <summary>(R450・R551)運転日報を表すクラス・コントロール</summary>
    public class ReportInfoR450_R551 : ReportInfoBase
    {
        #region - Fields -

        /// <summary>縦のDetail1の最大表示行数を保持するフィールド</summary>
        private const int ConstMaxDispDetail1VRowCount = 25;

        /// <summary>横のDetail1の最大表示行数を保持するフィールド</summary>
        private const int ConstMaxDispDetail1RowCount = 3;

        /// <summary>縦のDetail2の最大表示行数を保持するフィールド</summary>
        private const int ConstMaxDispDetail2VRowCount = 3;

        /// <summary>横Detail2の最大表示行数を保持するフィールド</summary>
        private const int ConstMaxDispDetail2RowCount = 16;

        /// <summary>横Detail2ヘッダの最大表示行数を保持するフィールド</summary>
        private const int ConstMaxDispHinmeiColCount = 10;

        /// <summary>横Detail2ヘッダの増分数を保持するフィールド</summary>
        private const int ConstSampleDispHinmeiColCount = 0;         // 横Detail2ヘッダの増分数

        /// <summary>S2Daoインターフェースを保持するフィールド</summary>
        private IS2Dao dao = null;

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>会社名を保持するフィールド</summary>
        private string corpName = string.Empty;

        /// <summary>定期実績か否かを保持するフィールド</summary>
        private bool isTeikiJitsuseki = false;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR450_R551"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR450_R551(WINDOW_ID windowID, IS2Dao dao)
        {
            this.windowID = windowID;

            this.dao = dao;

            // 出力タイプ
            this.OutputType = OutputTypeDef.Normal; // 通常

            // 定期タイプ
            this.TeikiType = TeikiTypeDef.Normal;   // 通常

            // 定期配車番号
            this.TeikiHaishaNo = "1";
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>出力タイプに関する列挙型</summary>
        public enum OutputTypeDef
        {
            /// <summary>通常に関する列挙型</summary>
            Normal,

            /// <summary>縦出力に関する列挙型</summary>
            Vertical,

            /// <summary>横出力に関する列挙型</summary>
            Holizontal,
        }

        /// <summary>定期タイプに関する列挙型</summary>
        public enum TeikiTypeDef
        {
            /// <summary>通常に関する列挙型</summary>
            Normal,

            /// <summary>定期配車に関する列挙型</summary>
            Haisha,

            /// <summary>定期実績に関する列挙型</summary>
            Jitsuseki,
        }

        /// <summary>出力フォーマットタイプに関する列挙型</summary>
        private enum OutputFormatTypeDef
        {
            /// <summary>重量フォーマットに関する列挙型</summary>
            JyuryouFormat,
            
            /// <summary>数量フォーマットに関する列挙型</summary>
            SuuryouFormat,

            /// <summary>単価フォーマットに関する列挙型</summary>
            TankaFormat,
        }

        #endregion - Enums -

        #region - Properties -

        /// <summary>出力タイプを保持するプロパティ</summary>
        public OutputTypeDef OutputType { get; set; }

        /// <summary>定期タイプを保持するプロパティ</summary>
        public TeikiTypeDef TeikiType { get; set; }

        /// <summary>サンプルデーターを使用するか否かを保持するプロパティ</summary>
        /// <remarks>真の場合：サンプルデータ使用、偽の場合：データーベースデータを使用</remarks>
        public bool IsSampleDataUse { get; set; }

        /// <summary>定期配車番号を保持するプロパティ</summary>
        public string TeikiHaishaNo { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>サンプルデータの作成処理を実行する</summary>
        public void CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;
            bool isPrint = true;

            switch (this.OutputType)
            {
                case OutputTypeDef.Normal:      // 通常
                case OutputTypeDef.Vertical:    // 縦出力

                    this.OutputType = OutputTypeDef.Vertical;

                    #region - 縦出力 -

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    // 会社名
                    dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                    // 発行日時
                    dataTableTmp.Columns.Add("PRINT_DATE");
                    // タイトル
                    dataTableTmp.Columns.Add("TITLE");

                    // 作業日
                    dataTableTmp.Columns.Add("SAGYOU_DATE");
                    // 作業日_曜日
                    dataTableTmp.Columns.Add("SAGYOU_YOUBI");
                    // 天候
                    dataTableTmp.Columns.Add("WEATHER");
                    // 運転者CD
                    dataTableTmp.Columns.Add("UNTENSHA_CD");
                    // 運転者名
                    dataTableTmp.Columns.Add("SHAIN_NAME");
                    // 車種CD
                    dataTableTmp.Columns.Add("SHASHU_CD");
                    // 車種名
                    dataTableTmp.Columns.Add("SHASHU_NAME_RYAKU");
                    // コースCD
                    dataTableTmp.Columns.Add("COURSE_NAME_CD");
                    // コース名
                    dataTableTmp.Columns.Add("COURSE_NAME");
                    // 車輌CD
                    dataTableTmp.Columns.Add("SHARYOU_CD");
                    // 車輌名
                    dataTableTmp.Columns.Add("SHARYOU_NAME");
                    // 出庫時メーター
                    dataTableTmp.Columns.Add("SHUKKO_METER");
                    // 出庫時間
                    dataTableTmp.Columns.Add("SHUKKO_TIME");
                    // 帰庫時メーター
                    dataTableTmp.Columns.Add("KIKO_METER");
                    // 帰庫時間
                    dataTableTmp.Columns.Add("KIKO_TIME");

                    rowTmp = dataTableTmp.NewRow();

                    if (isPrint)
                    {
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 発行日時
                        rowTmp["PRINT_DATE"] = "b";
                        // タイトル
                        rowTmp["TITLE"] = "運転日報";
                        // 作業日
                        rowTmp["SAGYOU_DATE"] = "2013年12月04日 12:00:00";
                        // 作業日_曜日
                        rowTmp["SAGYOU_YOUBI"] = "(水曜日)";
                        // 天候
                        rowTmp["WEATHER"] = "天候(あめあめふれふれ)";
                        // 運転者CD
                        rowTmp["UNTENSHA_CD"] = "1234567890";
                        // 運転者名
                        rowTmp["SHAIN_NAME"] = "あいうえおかきくけこ";
                        // 車種CD
                        rowTmp["SHASHU_CD"] = "1234567890";
                        // 車種名
                        rowTmp["SHASHU_NAME_RYAKU"] = "あいうえおかきくけこ";
                        // コースCD
                        rowTmp["COURSE_NAME_CD"] = "1234567890";
                        // コース名
                        rowTmp["COURSE_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 車輌CD
                        rowTmp["SHARYOU_CD"] = "1234567890";
                        // 車輌名
                        rowTmp["SHARYOU_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 出庫時メーター
                        rowTmp["SHUKKO_METER"] = "12,345,678,900";
                        // 出庫時間
                        rowTmp["SHUKKO_TIME"] = "12:00:00";
                        // 帰庫時メーター
                        rowTmp["KIKO_METER"] = "12,345,678,900";
                        // 帰庫時間
                        rowTmp["KIKO_TIME"] = "00:00:00";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail 1 -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail1";

                    // No
                    dataTableTmp.Columns.Add("NO");
                    // 収集時間
                    dataTableTmp.Columns.Add("SHUUSHUU_JIKAN");
                    // 業者CD
                    dataTableTmp.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmp.Columns.Add("GYOUSHA_MEI");
                    // 現場CD
                    dataTableTmp.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmp.Columns.Add("GENBA_MEI");
                    // 品名CD
                    dataTableTmp.Columns.Add("HINMEI_CD");
                    // 品名
                    dataTableTmp.Columns.Add("HINMEI_MEI");
                    // 収集量
                    dataTableTmp.Columns.Add("SUURYOU");
                    // 単位
                    dataTableTmp.Columns.Add("UNIT");
                    // 換算収集量
                    dataTableTmp.Columns.Add("KANSANSUURYOU");
                    // 換算単位
                    dataTableTmp.Columns.Add("KANSANUNIT");
                    // 備考
                    dataTableTmp.Columns.Add("BIKOU");
                    // 荷降No
                    dataTableTmp.Columns.Add("NIOROSHI_NUMBER");

                    for (int i = 0; i < 20; i++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // No
                        rowTmp["NO"] = (i + 1100).ToString();
                        // 収集時間
                        rowTmp["SHUUSHUU_JIKAN"] = "12:00:00";
                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "1234567890";
                        // 業者名
                        rowTmp["GYOUSHA_MEI"] = "あいうえおかきくけこさしすせそ";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "1234567890";
                        // 現場名
                        rowTmp["GENBA_MEI"] = "あいうえおかきくけこさしすせそ";
                        // 品名CD
                        rowTmp["HINMEI_CD"] = "1234567890";
                        // 品名
                        rowTmp["HINMEI_MEI"] = "あいうえおかきくけこさしすせそ";
                        // 収集量
                        rowTmp["SUURYOU"] = "12,345,678,900";
                        // 単位
                        rowTmp["UNIT"] = "あいうえお";
                        // 換算収集量
                        rowTmp["KANSANSUURYOU"] = "12,345,678,900";
                        // 換算単位
                        rowTmp["KANSANUNIT"] = "あいうえお";
                        // 備考
                        rowTmp["BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 荷降No
                        rowTmp["NIOROSHI_NUMBER"] = "00001";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Detail1", dataTableTmp);

                    #endregion - Detail 1 -

                    #region - Detail 2 -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail2";

                    // No
                    dataTableTmp.Columns.Add("NO2");
                    // 荷降現場CD
                    dataTableTmp.Columns.Add("NIOROSHIGENBA_CD");
                    // 荷降現場名
                    dataTableTmp.Columns.Add("NIOROSHIGENBA_MEI");
                    // 計量値（重量）
                    dataTableTmp.Columns.Add("KEIRYOUCHI");
                    // 搬入時間
                    dataTableTmp.Columns.Add("HANNYUU_JIKAN");

                    for (int i = 0; i < 4; i++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // No
                        rowTmp["NO2"] = (i + 1100).ToString();
                        // 荷降現場CD
                        rowTmp["NIOROSHIGENBA_CD"] = "1234567890";
                        // 荷降現場名
                        rowTmp["NIOROSHIGENBA_MEI"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量値（重量）
                        rowTmp["KEIRYOUCHI"] = 12345;
                        // 搬入時間
                        rowTmp["HANNYUU_JIKAN"] = "12:00:00";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Detail2", dataTableTmp);

                    #endregion - Detail 2 -

                    #endregion - 縦出力 -

                    break;
                case OutputTypeDef.Holizontal:  // 横出力

                    this.OutputType = OutputTypeDef.Holizontal;

                    #region - 横出力 -

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    // 会社名
                    dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                    // 発行日時
                    dataTableTmp.Columns.Add("PRINT_DATE");
                    // タイトル
                    dataTableTmp.Columns.Add("TITLE");

                    // 作業日
                    dataTableTmp.Columns.Add("SAGYOU_DATE");
                    // 作業日_曜日
                    dataTableTmp.Columns.Add("SAGYOU_YOUBI");
                    // 天候
                    dataTableTmp.Columns.Add("WEATHER");
                    // 運転者CD
                    dataTableTmp.Columns.Add("UNTENSHA_CD");
                    // 運転者名
                    dataTableTmp.Columns.Add("SHAIN_NAME");
                    // 車種CD
                    dataTableTmp.Columns.Add("SHASHU_CD");
                    // 車種名
                    dataTableTmp.Columns.Add("SHASHU_NAME_RYAKU");
                    // コースCD
                    dataTableTmp.Columns.Add("COURSE_NAME_CD");
                    // コース名
                    dataTableTmp.Columns.Add("COURSE_NAME");
                    // 車輌CD
                    dataTableTmp.Columns.Add("SHARYOU_CD");
                    // 車輌名
                    dataTableTmp.Columns.Add("SHARYOU_NAME");
                    // 出庫時メーター
                    dataTableTmp.Columns.Add("SHUKKO_METER");
                    // 出庫時間
                    dataTableTmp.Columns.Add("SHUKKO_TIME");
                    // 帰庫時メーター
                    dataTableTmp.Columns.Add("KIKO_METER");
                    // 帰庫時間
                    dataTableTmp.Columns.Add("KIKO_TIME");

                    rowTmp = dataTableTmp.NewRow();

                    if (isPrint)
                    {
                        // 会社名
                        rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 発行日時
                        rowTmp["PRINT_DATE"] = "b";
                        // タイトル
                        rowTmp["TITLE"] = "運転日報";

                        // 作業日
                        rowTmp["SAGYOU_DATE"] = "2013年12月04日 12:00:00";
                        // 作業日_曜日
                        rowTmp["SAGYOU_YOUBI"] = "(水曜日)";
                        // 天候
                        rowTmp["WEATHER"] = "天候(あめあめふれふれ)";
                        // 運転者CD
                        rowTmp["UNTENSHA_CD"] = "1234567890";
                        // 運転者名
                        rowTmp["SHAIN_NAME"] = "あいうえおかきくけこ";
                        // 車種CD
                        rowTmp["SHASHU_CD"] = "1234567890";
                        // 車種名
                        rowTmp["SHASHU_NAME_RYAKU"] = "あいうえおかきくけこ";
                        // コースCD
                        rowTmp["COURSE_NAME_CD"] = "1234567890";
                        // コース名
                        rowTmp["COURSE_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 車輌CD
                        rowTmp["SHARYOU_CD"] = "1234567890";
                        // 車輌名
                        rowTmp["SHARYOU_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 出庫時メーター
                        rowTmp["SHUKKO_METER"] = "12,345,678,900";
                        // 出庫時間
                        rowTmp["SHUKKO_TIME"] = "12:00:00";
                        // 帰庫時メーター
                        rowTmp["KIKO_METER"] = "12,345,678,900";
                        // 帰庫時間
                        rowTmp["KIKO_TIME"] = "00:00:00";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail 1 -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail 1";

                    // No
                    dataTableTmp.Columns.Add("NO2");
                    // 荷降現場CD
                    dataTableTmp.Columns.Add("NIOROSHIGENBA_CD");
                    // 荷降現場名
                    dataTableTmp.Columns.Add("NIOROSHIGENBA_MEI");
                    // 計量値（重量）
                    dataTableTmp.Columns.Add("KEIRYOUCHI");
                    // 搬入時間
                    dataTableTmp.Columns.Add("HANNYUU_JIKAN");

                    for (int i = 0; i < 4; i++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // No
                        rowTmp["NO2"] = (i + 1100).ToString();
                        // 荷降現場CD
                        rowTmp["NIOROSHIGENBA_CD"] = "1234567890";
                        // 荷降現場名
                        rowTmp["NIOROSHIGENBA_MEI"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 計量値（重量）
                        rowTmp["KEIRYOUCHI"] = 12345;
                        // 搬入時間
                        rowTmp["HANNYUU_JIKAN"] = "12:00:00";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Detail1", dataTableTmp);

                    #endregion - Detail 1 -

                    #region - Detail 2 -

                    #region - Detail 2 Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail2Header";

                    if (isPrint)
                    {
                        for (int i = 1; i <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount; i++)
                        {
                            // 品名Ｎ
                            dataTableTmp.Columns.Add(string.Format("HINMEI{0}", i));
                            dataTableTmp.Columns.Add(string.Format("UNIT{0}", i));
                            dataTableTmp.Columns.Add(string.Format("KANSANUNIT{0}", i));
                        }

                        rowTmp = dataTableTmp.NewRow();

                        for (int i = 1; i <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount; i++)
                        {
                            // 品名Ｎ
                            rowTmp[string.Format("HINMEI{0}", i)] = "あいうえおかきくけこさしすせそ";
                            rowTmp[string.Format("UNIT{0}", i)] = "あいうえお";
                            rowTmp[string.Format("KANSANUNIT{0}", i)] = "あいうえお";
                        }

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Detail2Header", dataTableTmp);

                    #endregion - Detail 2 Header -

                    #region - Detail 2 Detail -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail2";

                    // 業者CD
                    dataTableTmp.Columns.Add("GYOUSHA_CD");
                    // 業者名
                    dataTableTmp.Columns.Add("GYOUSHA_MEI");
                    // 現場CD
                    dataTableTmp.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmp.Columns.Add("GENBA_MEI");

                    for (int i = 1; i <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount; i++)
                    {
                        // 収集量(N)
                        dataTableTmp.Columns.Add(string.Format("SUURYOU{0}", i));
                        dataTableTmp.Columns.Add(string.Format("KANSANSUURYOU{0}", i));
                    }

                    // 備考
                    dataTableTmp.Columns.Add("BIKOU");
                    // 荷降No
                    dataTableTmp.Columns.Add("NIOROSHI_NUMBER");

                    for (int i = 0; i < 0; i++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 業者CD
                        rowTmp["GYOUSHA_CD"] = "1234567890";
                        // 業者名
                        rowTmp["GYOUSHA_MEI"] = "あいうえおかきくけこさしすせそ";
                        // 現場CD
                        rowTmp["GENBA_CD"] = "1234567890";
                        // 現場名
                        rowTmp["GENBA_MEI"] = "あいうえおかきくけこさしすせそ";
                        for (int j = 1; j <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount; j++)
                        {
                            // 収集量(N)
                            rowTmp[string.Format("SUURYOU{0}", j)] = "12,345,678,900";
                            // 換算収集量(N)
                            rowTmp[string.Format("KANSANSUURYOU{0}", j)] = "12,345,678,900";
                        }

                        // 備考
                        rowTmp["BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                        // 荷降No
                        rowTmp["NIOROSHI_NUMBER"] = "00001";

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    this.DataTableList.Add("Detail2", dataTableTmp);

                    #endregion - Detail 2 Detail -

                    #endregion - Detail 2 -

                    #region - Footer -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Footer";

                    for (int j = 1; j <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount; j++)
                    {
                        // 収集量・合計(N)
                        dataTableTmp.Columns.Add(string.Format("SUURYOU_GOUKEI{0}", j));
                        // 換算収集量・合計(N)
                        dataTableTmp.Columns.Add(string.Format("KANSANSUURYOU_GOUKEI{0}", j));
                    }

                    rowTmp = dataTableTmp.NewRow();

                    for (int j = 1; j <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount; j++)
                    {
                        // 収集量・合計(N)
                        rowTmp[string.Format("SUURYOU_GOUKEI{0}", j)] = "12,345,678,900";
                        // 換算収集量・合計(N)
                        rowTmp[string.Format("KANSANSUURYOU_GOUKEI{0}", j)] = "12,345,678,900";
                    }

                    dataTableTmp.Rows.Add(rowTmp);

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    #endregion - 横出力 -

                    break;
            }
        }

        /// <summary>レイアウトが縦か否か取得する</summary>
        /// <returns>縦の場合は真、横の場合は偽</returns>
        public bool IsLayoutV()
        {
            string sql = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT ");
            stringBuilder.Append("ISNULL(HAISHA_NIPPOU_LAYOUT_KBN,'') AS HAISHA_NIPPOU_LAYOUT_KBN ");
            stringBuilder.Append("FROM ");
            stringBuilder.Append("M_SYS_INFO ");
            stringBuilder.Append("WHERE ");
            stringBuilder.Append("SYS_ID = 0");

            sql = stringBuilder.ToString();

            // データーベースから取得
            DataTable dataTableTmp = this.dao.GetDateForStringSql(sql);
            int index = dataTableTmp.Columns.IndexOf("HAISHA_NIPPOU_LAYOUT_KBN");
            if ((short)dataTableTmp.Rows[0].ItemArray[index] == 1)
            {   // 縦
                this.OutputType = OutputTypeDef.Vertical;

                return true;
            }
            else
            {   // 横
                this.OutputType = OutputTypeDef.Holizontal;

                return false;
            }
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            // データーベースからデータを取得
            this.GetDatabaseData();

            int index;
            int i;
            int j;
            int rowNo = 1;
            DataRow row;
            DataTable dataTableDetail1Tmp = this.DataTableList["Detail1"];
            DataTable dataTableDetail2Tmp = this.DataTableList["Detail2"];
            string ctrlName = string.Empty;

            string tmp;
            int maxPage;
            bool detail1Comp = false;
            bool detail2Comp = false;
            int detail1MaxCount = dataTableDetail1Tmp.Rows.Count;
            int detail2MaxCount = dataTableDetail2Tmp.Rows.Count;
            int detail1Start = 0;
            int detail2Start = 0;

            int detail1Page;
            int detail2Page;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            switch (this.OutputType)
            {
                case OutputTypeDef.Vertical:    // 縦出力

                    #region - 縦出力 -

                    #region - Column -

                    for (i = 1; i <= ConstMaxDispDetail1VRowCount; i++)
                    {
                        // No
                        ctrlName = string.Format("PHN_NO_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 収集時間
                        ctrlName = string.Format("PHN_SHUUSHUU_JIKAN_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 業者CD
                        ctrlName = string.Format("PHN_GYOUSHA_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 業者名
                        ctrlName = string.Format("PHN_GYOUSHA_MEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 現場CD
                        ctrlName = string.Format("PHN_GENBA_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 現場名
                        ctrlName = string.Format("PHN_GENBA_MEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名CD
                        ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名
                        ctrlName = string.Format("PHN_HINMEI_MEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 収集量
                        ctrlName = string.Format("PHN_SUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 単位
                        ctrlName = string.Format("PHN_UNIT_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 換算収集量
                        ctrlName = string.Format("PHN_KANSANSUURYOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 換算単位
                        ctrlName = string.Format("PHN_KANSANUNIT_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 備考
                        ctrlName = string.Format("PHN_BIKOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 荷降No
                        ctrlName = string.Format("PHN_NIOROSHI_NUMBER_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    for (i = 1; i <= ConstMaxDispDetail2VRowCount; i++)
                    {
                        // No
                        ctrlName = string.Format("PHN_NO2_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 荷降現場CD
                        ctrlName = string.Format("PHN_NIOROSHIGENBA_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 荷降現場名
                        ctrlName = string.Format("PHN_NIOROSHIGENBA_MEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 計量値（重量）
                        ctrlName = string.Format("PHN_KEIRYOUCHI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 搬入時間
                        ctrlName = string.Format("PHN_HANNYUU_JIKAN_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    #endregion - Column -

                    detail1Page = (int)Math.Ceiling((double)detail1MaxCount / ConstMaxDispDetail1VRowCount);
                    detail2Page = (int)Math.Ceiling((double)detail2MaxCount / ConstMaxDispDetail2VRowCount);

                    if (detail1Page >= detail2Page)
                    {
                        maxPage = detail1Page;

                        if (detail2Page == 0)
                        {
                            detail2Comp = true;
                        }
                    }
                    else
                    {
                        maxPage = detail2Page;

                        if (detail1Page == 0)
                        {
                            detail1Comp = true;
                        }
                    }

                    if (maxPage == 0)
                    {
                        maxPage = 1;
                    }

                    for (int pageNo = 1; pageNo <= maxPage; pageNo++)
                    {
                        #region - Detail 1 -

                        row = this.dataTable.NewRow();

                        if (detail1Comp == false)
                        {   // Detail1はまだ未完了

                            rowNo = 1;
                            
                            for (i = detail1Start; i < dataTableDetail1Tmp.Rows.Count; i++)
                            {
                                // No
                                index = dataTableDetail1Tmp.Columns.IndexOf("NO");
                                ctrlName = string.Format("PHN_NO_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 収集時間
                                index = dataTableDetail1Tmp.Columns.IndexOf("SHUUSHUU_JIKAN");
                                ctrlName = string.Format("PHN_SHUUSHUU_JIKAN_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 業者CD
                                index = dataTableDetail1Tmp.Columns.IndexOf("GYOUSHA_CD");
                                ctrlName = string.Format("PHN_GYOUSHA_CD_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 業者名
                                index = dataTableDetail1Tmp.Columns.IndexOf("GYOUSHA_MEI");
                                ctrlName = string.Format("PHN_GYOUSHA_MEI_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 20)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 現場CD
                                index = dataTableDetail1Tmp.Columns.IndexOf("GENBA_CD");
                                ctrlName = string.Format("PHN_GENBA_CD_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 現場名
                                index = dataTableDetail1Tmp.Columns.IndexOf("GENBA_MEI");
                                ctrlName = string.Format("PHN_GENBA_MEI_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 20)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 品名CD
                                index = dataTableDetail1Tmp.Columns.IndexOf("HINMEI_CD");
                                ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 品名
                                index = dataTableDetail1Tmp.Columns.IndexOf("HINMEI_MEI");
                                ctrlName = string.Format("PHN_HINMEI_MEI_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 20)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 収集量
                                index = dataTableDetail1Tmp.Columns.IndexOf("SUURYOU");
                                ctrlName = string.Format("PHN_SUURYOU_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 単位
                                index = dataTableDetail1Tmp.Columns.IndexOf("UNIT");
                                ctrlName = string.Format("PHN_UNIT_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 6)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 換算収集量
                                index = dataTableDetail1Tmp.Columns.IndexOf("KANSANSUURYOU");
                                ctrlName = string.Format("PHN_KANSANSUURYOU_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 換算単位
                                index = dataTableDetail1Tmp.Columns.IndexOf("KANSANUNIT");
                                ctrlName = string.Format("PHN_KANSANUNIT_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 6)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 備考
                                index = dataTableDetail1Tmp.Columns.IndexOf("BIKOU");
                                ctrlName = string.Format("PHN_BIKOU_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 荷降No
                                index = dataTableDetail1Tmp.Columns.IndexOf("NIOROSHI_NUMBER");
                                ctrlName = string.Format("PHN_NIOROSHI_NUMBER_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                if (rowNo == ConstMaxDispDetail1VRowCount)
                                {
                                    detail1Start = i + 1;
                                    break;
                                }
                                else
                                {
                                    rowNo++;
                                }
                            }
                        }
                        else
                        {   // Detail1は既に完了
                            rowNo = 1;
                            for (i = 0; i < ConstMaxDispDetail1RowCount; i++)
                            {
                                // No
                                index = dataTableDetail1Tmp.Columns.IndexOf("NO");
                                ctrlName = string.Format("PHN_NO_{0}_FLB", rowNo);
                                if (rowNo == 1)
                                {
                                    row[ctrlName] = "***";
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }
                                // 収集時間
                                index = dataTableDetail1Tmp.Columns.IndexOf("SHUUSHUU_JIKAN");
                                ctrlName = string.Format("PHN_SHUUSHUU_JIKAN_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 業者CD
                                index = dataTableDetail1Tmp.Columns.IndexOf("GYOUSHA_CD");
                                ctrlName = string.Format("PHN_GYOUSHA_CD_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 業者名
                                index = dataTableDetail1Tmp.Columns.IndexOf("GYOUSHA_MEI");
                                ctrlName = string.Format("PHN_GYOUSHA_MEI_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 現場CD
                                index = dataTableDetail1Tmp.Columns.IndexOf("GENBA_CD");
                                ctrlName = string.Format("PHN_GENBA_CD_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 現場名
                                index = dataTableDetail1Tmp.Columns.IndexOf("GENBA_MEI");
                                ctrlName = string.Format("PHN_GENBA_MEI_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 品名CD
                                index = dataTableDetail1Tmp.Columns.IndexOf("HINMEI_CD");
                                ctrlName = string.Format("PHN_HINMEI_CD_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 品名
                                index = dataTableDetail1Tmp.Columns.IndexOf("HINMEI_MEI");
                                ctrlName = string.Format("PHN_HINMEI_MEI_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 収集量
                                index = dataTableDetail1Tmp.Columns.IndexOf("SUURYOU");
                                ctrlName = string.Format("PHN_SUURYOU_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 単位
                                index = dataTableDetail1Tmp.Columns.IndexOf("UNIT");
                                ctrlName = string.Format("PHN_UNIT_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 換算収集量
                                index = dataTableDetail1Tmp.Columns.IndexOf("KANSANSUURYOU");
                                ctrlName = string.Format("PHN_KANSANSUURYOU_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 単位
                                index = dataTableDetail1Tmp.Columns.IndexOf("KANSANUNIT");
                                ctrlName = string.Format("PHN_KANSANUNIT_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 備考
                                index = dataTableDetail1Tmp.Columns.IndexOf("BIKOU");
                                ctrlName = string.Format("PHN_BIKOU_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;
                                // 荷降No
                                index = dataTableDetail1Tmp.Columns.IndexOf("NIOROSHI_NUMBER");
                                ctrlName = string.Format("PHN_NIOROSHI_NUMBER_{0}_FLB", rowNo);
                                row[ctrlName] = string.Empty;

                                if (rowNo == ConstMaxDispDetail1VRowCount)
                                {
                                    detail1Start = i + 1;
                                    break;
                                }
                                else
                                {
                                    rowNo++;
                                }
                            }
                        }

                        if (i >= detail1MaxCount - 1)
                        {
                            detail1Comp = true;
                        }

                        #endregion - Detail 1 -

                        #region - Detail 2-

                        if (detail2Comp == false)
                        {   // Detail2はまだ未完了

                            rowNo = 1;
                            
                            for (i = detail2Start; i < dataTableDetail2Tmp.Rows.Count; i++)
                            {
                                // No
                                index = dataTableDetail2Tmp.Columns.IndexOf("NO2");
                                ctrlName = string.Format("PHN_NO2_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 荷降現場CD
                                index = dataTableDetail2Tmp.Columns.IndexOf("NIOROSHIGENBA_CD");
                                ctrlName = string.Format("PHN_NIOROSHIGENBA_CD_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 荷降現場名
                                index = dataTableDetail2Tmp.Columns.IndexOf("NIOROSHIGENBA_MEI");
                                ctrlName = string.Format("PHN_NIOROSHIGENBA_MEI_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                {
                                    byteArray = encoding.GetBytes(dataTableDetail2Tmp.Rows[i].ItemArray[index].ToString());
                                    if (byteArray.Length > 40)
                                    {
                                        row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                    }
                                    else
                                    {
                                        row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                    }
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 計量値（重量）
                                index = dataTableDetail2Tmp.Columns.IndexOf("KEIRYOUCHI");
                                ctrlName = string.Format("PHN_KEIRYOUCHI_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                // 搬入時間
                                index = dataTableDetail2Tmp.Columns.IndexOf("HANNYUU_JIKAN");
                                ctrlName = string.Format("PHN_HANNYUU_JIKAN_{0}_FLB", rowNo);
                                if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                {
                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }

                                if (rowNo == ConstMaxDispDetail2VRowCount)
                                {
                                    detail2Start = i + 1;
                                    break;
                                }
                                else
                                {
                                    rowNo++;
                                }
                            }
                        }
                        else
                        {   // Detail2は既に完了
                            rowNo = 1;
                            for (i = 0; i < ConstMaxDispDetail2VRowCount; i++)
                            {
                                // No
                                index = dataTableDetail2Tmp.Columns.IndexOf("NO2");
                                ctrlName = string.Format("PHN_NO2_{0}_FLB", i + 1);
                                row[ctrlName] = string.Empty;
                                // 荷降現場CD
                                index = dataTableDetail2Tmp.Columns.IndexOf("NIOROSHIGENBA_CD");
                                ctrlName = string.Format("PHN_NIOROSHIGENBA_CD_{0}_FLB", i + 1);
                                row[ctrlName] = string.Empty;
                                // 荷降現場名
                                index = dataTableDetail2Tmp.Columns.IndexOf("NIOROSHIGENBA_MEI");
                                ctrlName = string.Format("PHN_NIOROSHIGENBA_MEI_{0}_FLB", i + 1);
                                row[ctrlName] = string.Empty;
                                // 計量値（重量）
                                index = dataTableDetail2Tmp.Columns.IndexOf("KEIRYOUCHI");
                                ctrlName = string.Format("PHN_KEIRYOUCHI_{0}_FLB", i + 1);
                                row[ctrlName] = string.Empty;
                                // 搬入時間
                                index = dataTableDetail2Tmp.Columns.IndexOf("HANNYUU_JIKAN");
                                ctrlName = string.Format("PHN_HANNYUU_JIKAN_{0}_FLB", i + 1);
                                row[ctrlName] = string.Empty;

                                if (rowNo == ConstMaxDispDetail2VRowCount)
                                {
                                    detail2Start = i + 1;
                                    break;
                                }
                                else
                                {
                                    rowNo++;
                                }
                            }
                        }

                        this.dataTable.Rows.Add(row);

                        if (i >= detail2MaxCount - 1)
                        {
                            detail2Comp = true;
                        }

                        #endregion - Detail 2 -
                    }

                    #endregion - 縦出力 -

                    break;
                case OutputTypeDef.Holizontal:  // 横出力

                    #region - 横出力 -

                    DataTable dataTableDetail2HeaderTmp = this.DataTableList["Detail2Header"];
                    DataTable dataTableFooterTmp = this.DataTableList["Footer"];

                    #region - Column -

                    for (i = 1; i <= ConstMaxDispDetail1RowCount; i++)
                    {
                        // No
                        ctrlName = string.Format("PHN_NO2_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 荷降現場CD
                        ctrlName = string.Format("PHN_NIOROSHIGENBA_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 荷降現場名
                        ctrlName = string.Format("PHN_NIOROSHIGENBA_MEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 計量値（重量）
                        ctrlName = string.Format("PHN_KEIRYOUCHI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 搬入時間
                        ctrlName = string.Format("PHN_HANNYUU_JIKAN_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    // 品名Ｎ
                    for (i = 1; i <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount + 1; i++)
                    {
                        ctrlName = string.Format("PHN_HINMEI{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);

                        ctrlName = string.Format("PHN_UNIT{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);

                        ctrlName = string.Format("PHN_KANSANUNIT{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    for (i = 1; i <= ConstMaxDispDetail2RowCount; i++)
                    {
                        // 業者CD
                        ctrlName = string.Format("PHN_GYOUSHA_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 業者名
                        ctrlName = string.Format("PHN_GYOUSHA_MEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 現場CD
                        ctrlName = string.Format("PHN_GENBA_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 現場名
                        ctrlName = string.Format("PHN_GENBA_MEI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);

                        for (j = 1; j <= ConstMaxDispHinmeiColCount; j++)
                        {
                            // 収集量(N)
                            ctrlName = string.Format("PHN_SUURYOU{0}_{1}_FLB", j, i);
                            this.dataTable.Columns.Add(ctrlName);
                            // 換算収集量(N)
                            ctrlName = string.Format("PHN_KASANSUURYOU{0}_{1}_FLB", j, i);
                            this.dataTable.Columns.Add(ctrlName);
                        }
                        // 備考
                        ctrlName = string.Format("PHN_BIKOU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 荷降No
                        ctrlName = string.Format("PHN_NIOROSHI_NUMBER_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    for (i = 1; i <= ConstMaxDispHinmeiColCount + ConstSampleDispHinmeiColCount; i++)
                    {
                        // 収集量・合計(N)
                        ctrlName = string.Format("PHN_SUURYOU_GOUKEI{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 換算収集量・合計(N)
                        ctrlName = string.Format("PHN_KANSANSUURYOU_GOUKEI{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    #endregion - Column -

                    detail1Page = (int)Math.Ceiling((double)detail1MaxCount / ConstMaxDispDetail1RowCount);
                    detail2Page = (int)Math.Ceiling((double)detail2MaxCount / ConstMaxDispDetail2RowCount);
                    int intHinmeiCount = (dataTableDetail2Tmp.Columns.Count - 6) / 2;
                    int maxLoopCount = (int)Math.Ceiling((double)intHinmeiCount / ConstMaxDispHinmeiColCount);

                    if (detail1Page >= detail2Page)
                    {
                        maxPage = detail1Page;

                        if (detail2Page == 0)
                        {
                            detail2Comp = true;
                        }
                    }
                    else
                    {
                        maxPage = detail2Page;

                        if (detail1Page == 0)
                        {
                            detail1Comp = true;
                        }
                    }

                    if (maxPage == 0)
                    {
                        maxPage = 1;
                    }

                    if (maxLoopCount == 0)
                    {
                        maxLoopCount = 1;
                    }

                    for (int pageNo = 1; pageNo <= maxPage; pageNo++)
                    {
                        int intCount = 0;
                        for (int loop = 0; loop < maxLoopCount; loop++)
                        {
                            row = this.dataTable.NewRow();

                            #region - Detail 1 -

                            if (detail1Comp == false)
                            {   // Detail1はまだ未完了
                                rowNo = 1;
                                for (i = detail1Start; i < dataTableDetail1Tmp.Rows.Count; i++)
                                {
                                    // No
                                    index = dataTableDetail1Tmp.Columns.IndexOf("NO2");
                                    ctrlName = string.Format("PHN_NO2_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 荷降現場CD
                                    index = dataTableDetail1Tmp.Columns.IndexOf("NIOROSHIGENBA_CD");
                                    ctrlName = string.Format("PHN_NIOROSHIGENBA_CD_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 荷降現場名
                                    index = dataTableDetail1Tmp.Columns.IndexOf("NIOROSHIGENBA_MEI");
                                    ctrlName = string.Format("PHN_NIOROSHIGENBA_MEI_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                                        if (byteArray.Length > 20)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 計量値（重量）
                                    index = dataTableDetail1Tmp.Columns.IndexOf("KEIRYOUCHI");
                                    ctrlName = string.Format("PHN_KEIRYOUCHI_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                    {
                                        if (dataTableDetail1Tmp.Rows[i].ItemArray[index].Equals(string.Empty))
                                        {
                                            row[ctrlName] = string.Empty;
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 搬入時間
                                    index = dataTableDetail1Tmp.Columns.IndexOf("HANNYUU_JIKAN");
                                    ctrlName = string.Format("PHN_HANNYUU_JIKAN_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    if (rowNo == ConstMaxDispDetail1RowCount)
                                    {
                                        if (intCount + 10 >= intHinmeiCount)
                                        {
                                            detail1Start = i + 1;
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        rowNo++;
                                    }
                                }

                                if (i >= detail1MaxCount - 1)
                                {
                                    if (intCount + 10 >= intHinmeiCount)
                                    {
                                        detail1Comp = true;
                                    }
                                }
                            }
                            else
                            {   // Detail1は既に完了

                                rowNo = 1;

                                for (i = 0; i < ConstMaxDispDetail1RowCount; i++)
                                {
                                    // No
                                    index = dataTableDetail1Tmp.Columns.IndexOf("NO2");
                                    ctrlName = string.Format("PHN_NO2_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;
                                    // 荷降現場CD
                                    index = dataTableDetail1Tmp.Columns.IndexOf("NIOROSHIGENBA_CD");
                                    ctrlName = string.Format("PHN_NIOROSHIGENBA_CD_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;
                                    // 荷降現場名
                                    index = dataTableDetail1Tmp.Columns.IndexOf("NIOROSHIGENBA_MEI");
                                    ctrlName = string.Format("PHN_NIOROSHIGENBA_MEI_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;
                                    // 計量値（重量）
                                    index = dataTableDetail1Tmp.Columns.IndexOf("KEIRYOUCHI");
                                    ctrlName = string.Format("PHN_KEIRYOUCHI_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;
                                    // 搬入時間
                                    index = dataTableDetail1Tmp.Columns.IndexOf("HANNYUU_JIKAN");
                                    ctrlName = string.Format("PHN_HANNYUU_JIKAN_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;

                                    if (rowNo == ConstMaxDispDetail1RowCount)
                                    {
                                        if (intCount + 10 >= intHinmeiCount)
                                        {
                                            detail1Start = i + 1;
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        rowNo++;
                                    }
                                }
                            }

                            #endregion - Detail 2 -

                            #region - Detail Header -

                            if (dataTableDetail2HeaderTmp.Rows.Count > 0)
                            {
                                for (i = 1; i <= ConstMaxDispHinmeiColCount; i++)
                                {
                                    // 品名１
                                    index = dataTableDetail2HeaderTmp.Columns.IndexOf(string.Format("HINMEI{0}", i + intCount));
                                    ctrlName = string.Format("PHN_HINMEI{0}_FLB", i);
                                    if (!this.IsDBNull(dataTableDetail2HeaderTmp.Rows[0].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableDetail2HeaderTmp.Rows[0].ItemArray[index].ToString());
                                        if (byteArray.Length > 20)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail2HeaderTmp.Rows[0].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    index = dataTableDetail2HeaderTmp.Columns.IndexOf(string.Format("UNIT{0}", i + intCount));
                                    ctrlName = string.Format("PHN_UNIT{0}_FLB", i);
                                    if (!this.IsDBNull(dataTableDetail2HeaderTmp.Rows[0].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableDetail2HeaderTmp.Rows[0].ItemArray[index].ToString());
                                        if (byteArray.Length > 6)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail2HeaderTmp.Rows[0].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    index = dataTableDetail2HeaderTmp.Columns.IndexOf(string.Format("KANSANUNIT{0}", i + intCount));
                                    ctrlName = string.Format("PHN_KANSANUNIT{0}_FLB", i);
                                    if (!this.IsDBNull(dataTableDetail2HeaderTmp.Rows[0].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableDetail2HeaderTmp.Rows[0].ItemArray[index].ToString());
                                        if (byteArray.Length > 6)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail2HeaderTmp.Rows[0].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    if (i + intCount >= intHinmeiCount)
                                    {
                                        break;
                                    }
                                }
                            }
                            #endregion

                            #region - Detail 2 -

                            if (detail2Comp == false)
                            {   // Detail2はまだ未完了

                                rowNo = 1;

                                j = ConstMaxDispHinmeiColCount + 1;
                                
                                for (i = detail2Start; i < dataTableDetail2Tmp.Rows.Count; i++)
                                {
                                    // 業者CD
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GYOUSHA_CD");
                                    ctrlName = string.Format("PHN_GYOUSHA_CD_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 業者名
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GYOUSHA_MEI");
                                    ctrlName = string.Format("PHN_GYOUSHA_MEI_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableDetail2Tmp.Rows[i].ItemArray[index].ToString());
                                        if (byteArray.Length > 20)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 現場CD
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GENBA_CD");
                                    ctrlName = string.Format("PHN_GENBA_CD_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 現場名
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GENBA_MEI");
                                    ctrlName = string.Format("PHN_GENBA_MEI_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableDetail2Tmp.Rows[i].ItemArray[index].ToString());
                                        if (byteArray.Length > 20)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    for (j = 1; j <= ConstMaxDispHinmeiColCount; j++)
                                    {
                                        if (j + intCount < intHinmeiCount)
                                        {
                                            // 収集量(n)
                                            index = dataTableDetail2Tmp.Columns.IndexOf(string.Format("SUURYOU{0}", j + intCount));
                                            ctrlName = string.Format("PHN_SUURYOU{0}_{1}_FLB", j, rowNo);
                                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                            {
                                                if (dataTableDetail2Tmp.Rows[i].ItemArray[index].Equals(string.Empty))
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }
                                                else
                                                {
                                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                                }
                                            }
                                            else
                                            {
                                                row[ctrlName] = string.Empty;
                                            }

                                            // 換算収集量(n)
                                            index = dataTableDetail2Tmp.Columns.IndexOf(string.Format("KANSANSUURYOU{0}", j + intCount));
                                            ctrlName = string.Format("PHN_KASANSUURYOU{0}_{1}_FLB", j, rowNo);
                                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                            {
                                                if (dataTableDetail2Tmp.Rows[i].ItemArray[index].Equals(string.Empty))
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }
                                                else
                                                {
                                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                                }
                                            }
                                            else
                                            {
                                                row[ctrlName] = string.Empty;
                                            }

                                            if (pageNo == maxPage)
                                            {
                                                // 収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("SUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_SUURYOU_GOUKEI{0}_FLB", j);
                                                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                                {
                                                    if (dataTableFooterTmp.Rows[0].ItemArray[index].Equals(string.Empty))
                                                    {
                                                        row[ctrlName] = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                                                    }
                                                }
                                                else
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }

                                                // 換算収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("KANSANSUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_KANSANSUURYOU_GOUKEI{0}_FLB", j);
                                                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                                {
                                                    if (dataTableFooterTmp.Rows[0].ItemArray[index].Equals(string.Empty))
                                                    {
                                                        row[ctrlName] = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                                                    }
                                                }
                                                else
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }
                                            }
                                            else
                                            {
                                                // 収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("SUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_SUURYOU_GOUKEI{0}_FLB", j);
                                                row[ctrlName] = string.Empty;
                                                // 換算収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("KANSANSUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_KANSANSUURYOU_GOUKEI{0}_FLB", j);
                                                row[ctrlName] = string.Empty;
                                            }
                                        }
                                        else
                                        {
                                            // 収集量(n)
                                            index = dataTableDetail2Tmp.Columns.IndexOf(string.Format("SUURYOU{0}", j + intCount));
                                            ctrlName = string.Format("PHN_SUURYOU{0}_{1}_FLB", j, rowNo);
                                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                            {
                                                if (dataTableDetail2Tmp.Rows[i].ItemArray[index].Equals(string.Empty))
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }
                                                else
                                                {
                                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                                }
                                            }
                                            else
                                            {
                                                row[ctrlName] = string.Empty;
                                            }

                                            // 換算収集量(n)
                                            index = dataTableDetail2Tmp.Columns.IndexOf(string.Format("KANSANSUURYOU{0}", j + intCount));
                                            ctrlName = string.Format("PHN_KASANSUURYOU{0}_{1}_FLB", j, rowNo);
                                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                            {
                                                if (dataTableDetail2Tmp.Rows[i].ItemArray[index].Equals(string.Empty))
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }
                                                else
                                                {
                                                    row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                                }
                                            }
                                            else
                                            {
                                                row[ctrlName] = string.Empty;
                                            }

                                            if (pageNo == maxPage)
                                            {
                                                // 収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("SUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_SUURYOU_GOUKEI{0}_FLB", j);
                                                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                                {
                                                    if (dataTableFooterTmp.Rows[0].ItemArray[index].Equals(string.Empty))
                                                    {
                                                        row[ctrlName] = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                                                    }
                                                }
                                                else
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }

                                                // 換算収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("KANSANSUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_KANSANSUURYOU_GOUKEI{0}_FLB", j);
                                                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                                {
                                                    if (dataTableFooterTmp.Rows[0].ItemArray[index].Equals(string.Empty))
                                                    {
                                                        row[ctrlName] = string.Empty;
                                                    }
                                                    else
                                                    {
                                                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                                                    }
                                                }
                                                else
                                                {
                                                    row[ctrlName] = string.Empty;
                                                }
                                            }
                                            else
                                            {
                                                // 収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("SUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_SUURYOU_GOUKEI{0}_FLB", j);
                                                row[ctrlName] = string.Empty;

                                                // 換算収集量合計(n)
                                                index = dataTableFooterTmp.Columns.IndexOf(string.Format("KANSANSUURYOU_GOUKEI{0}", j + intCount));
                                                ctrlName = string.Format("PHN_KANSANSUURYOU_GOUKEI{0}_FLB", j);
                                                row[ctrlName] = string.Empty;
                                            }

                                            break;
                                        }
                                    }

                                    // 備考
                                    index = dataTableDetail2Tmp.Columns.IndexOf("BIKOU");
                                    ctrlName = string.Format("PHN_BIKOU_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                    {
                                        byteArray = encoding.GetBytes(dataTableDetail2Tmp.Rows[i].ItemArray[index].ToString());
                                        if (byteArray.Length > 40)
                                        {
                                            row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                        }
                                        else
                                        {
                                            row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                        }
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    // 荷降No
                                    index = dataTableDetail2Tmp.Columns.IndexOf("NIOROSHI_NUMBER");
                                    ctrlName = string.Format("PHN_NIOROSHI_NUMBER_{0}_FLB", rowNo);
                                    if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                                    {
                                        row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }

                                    if (rowNo == ConstMaxDispDetail2RowCount)
                                    {
                                        intCount = intCount + ConstMaxDispHinmeiColCount;

                                        if (intCount < intHinmeiCount)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            detail2Start = i + 1;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        rowNo++;
                                    }
                                }

                                if (i >= detail2MaxCount - 1)
                                {
                                    intCount = intCount + ConstMaxDispHinmeiColCount;

                                    if (intCount >= intHinmeiCount)
                                    {
                                        detail2Comp = true;
                                    }
                                }
                            }
                            else
                            {   // Detail2は既に完了

                                rowNo = 1;

                                j = ConstMaxDispHinmeiColCount + 1;

                                for (i = 0; i < ConstMaxDispDetail2RowCount; i++)
                                {
                                    // 業者CD
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GYOUSHA_CD");
                                    ctrlName = string.Format("PHN_GYOUSHA_CD_{0}_FLB", rowNo);
                                    if (rowNo == 1)
                                    {
                                        row[ctrlName] = "***";
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }
                                    // 業者名
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GYOUSHA_MEI");
                                    ctrlName = string.Format("PHN_GYOUSHA_MEI_{0}_FLB", rowNo);
                                    if (rowNo == 1)
                                    {
                                        row[ctrlName] = "**********";
                                    }
                                    else
                                    {
                                        row[ctrlName] = string.Empty;
                                    }
                                    // 現場CD
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GENBA_CD");
                                    ctrlName = string.Format("PHN_GENBA_CD_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;
                                    // 現場名
                                    index = dataTableDetail2Tmp.Columns.IndexOf("GENBA_MEI");
                                    ctrlName = string.Format("PHN_GENBA_MEI_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;

                                    for (j = 1; j <= ConstMaxDispHinmeiColCount; j++)
                                    {
                                        // 収集量(n)
                                        index = dataTableDetail2Tmp.Columns.IndexOf(string.Format("SUURYOU{0}", j + intCount));
                                        ctrlName = string.Format("PHN_SUURYOU{0}_{1}_FLB", j, rowNo);
                                        row[ctrlName] = string.Empty;
                                        // 換算収集量(n)
                                        index = dataTableDetail2Tmp.Columns.IndexOf(string.Format("KANSANSUURYOU{0}", j + intCount));
                                        ctrlName = string.Format("PHN_KASANSUURYOU{0}_{1}_FLB", j, rowNo);
                                        row[ctrlName] = string.Empty;

                                        if (pageNo == maxPage)
                                        {
                                            // 収集量合計(n)
                                            index = dataTableFooterTmp.Columns.IndexOf(string.Format("SUURYOU_GOUKEI{0}", j + intCount));
                                            ctrlName = string.Format("PHN_SUURYOU_GOUKEI{0}_FLB", j);
                                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                            {
                                                row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                                            }
                                            else
                                            {
                                                row[ctrlName] = string.Empty;
                                            }

                                            // 換算収集量合計(n)
                                            index = dataTableFooterTmp.Columns.IndexOf(string.Format("KANSANSUURYOU_GOUKEI{0}", j + intCount));
                                            ctrlName = string.Format("PHN_KANSANSUURYOU_GOUKEI{0}_FLB", j);
                                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                                            {
                                                row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                                            }
                                            else
                                            {
                                                row[ctrlName] = string.Empty;
                                            }
                                        }
                                        else
                                        {
                                            // 収集量合計(n)
                                            index = dataTableFooterTmp.Columns.IndexOf(string.Format("SUURYOU_GOUKEI{0}", j + intCount));
                                            ctrlName = string.Format("PHN_SUURYOU_GOUKEI{0}_FLB", j);
                                            row[ctrlName] = string.Empty;
                                            // 換算収集量合計(n)
                                            index = dataTableFooterTmp.Columns.IndexOf(string.Format("KANSANSUURYOU_GOUKEI{0}", j + intCount));
                                            ctrlName = string.Format("PHN_KANSANSUURYOU_GOUKEI{0}_FLB", j);
                                            row[ctrlName] = string.Empty;
                                        }

                                        if (j + intCount >= intHinmeiCount)
                                        {
                                            break;
                                        }
                                    }

                                    // 備考
                                    index = dataTableDetail2Tmp.Columns.IndexOf("BIKOU");
                                    ctrlName = string.Format("PHN_BIKOU_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;
                                    // 荷降No
                                    index = dataTableDetail2Tmp.Columns.IndexOf("NIOROSHI_NUMBER");
                                    ctrlName = string.Format("PHN_NIOROSHI_NUMBER_{0}_FLB", rowNo);
                                    row[ctrlName] = string.Empty;

                                    if (rowNo == ConstMaxDispDetail2RowCount)
                                    {
                                        intCount = intCount + ConstMaxDispHinmeiColCount;

                                        if (intCount < intHinmeiCount)
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        rowNo++;
                                    }
                                }
                            }

                            #endregion - Detail 2 -

                            this.dataTable.Rows.Add(row);
                        }
                    }

                    #endregion - 横出力 -

                    break;
            }

            this.SetRecord(this.dataTable);
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
        }

        /// <summary>帳票出力用データテーブル作成処理を実行する</summary>
        private void SetChouhyouInfo()
        {
            int index;
            DataTable dataTableHeaderTmp = this.DataTableList["Header"];
            string ctrlName = string.Empty;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            switch (this.OutputType)
            {
                case OutputTypeDef.Vertical: // 縦出力

                    #region - 縦出力 -

                    #region - Header -

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("DH_CORP_RYAKU_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("DH_CORP_RYAKU_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_CORP_RYAKU_NAME_VLB", string.Empty);
                        }
 
                        //// 発行日時
                        //index = dataTableHeaderTmp.Columns.IndexOf("DH_PRINT_DATE_VLB");
                        //this.SetFieldName("DH_PRINT_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        //// タイトル
                        //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                        //this.SetFieldName("DH_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                        // 作業日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 14)
                            {
                                this.SetFieldName("DH_SAGYOU_DATE_CTL", encoding.GetString(byteArray, 0, 14));
                            }
                            else
                            {
                                this.SetFieldName("DH_SAGYOU_DATE_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SAGYOU_DATE_CTL", string.Empty);
                        }

                        // 作業日_曜日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_YOUBI");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 6)
                            {
                                this.SetFieldName("DH_SAGYOU_YOUBI_CTL", encoding.GetString(byteArray, 0, 6));
                            }
                            else
                            {
                                this.SetFieldName("DH_SAGYOU_YOUBI_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SAGYOU_YOUBI_CTL", string.Empty);
                        }

                        // 天候
                        index = dataTableHeaderTmp.Columns.IndexOf("WEATHER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 18)
                            {
                                this.SetFieldName("DH_WEATHER_CTL", encoding.GetString(byteArray, 0, 18));
                            }
                            else
                            {
                                this.SetFieldName("DH_WEATHER_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_WEATHER_CTL", string.Empty);
                        }

                        // 伝票番号
                        if (!this.isTeikiJitsuseki)
                        {   // 定期配車

                            index = dataTableHeaderTmp.Columns.IndexOf("HAISHA_NO");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName("DH_DENPYOU_NO_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {
                                this.SetFieldName("DH_DENPYOU_NO_CTL", string.Empty);
                            }
                        }
                        else
                        {   // 定期実績

                            this.SetFieldName("DH_DENPYOU_NO_FLB", "配車番号（実績）");

                            string haishaNo = string.Empty;
                            string jissekiNo = string.Empty;

                            index = dataTableHeaderTmp.Columns.IndexOf("HAISHA_NO");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                haishaNo = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                haishaNo = string.Empty;
                            }

                            index = dataTableHeaderTmp.Columns.IndexOf("JISSEKI_NO");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                jissekiNo = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                jissekiNo = string.Empty;
                            }

                            this.SetFieldName("DH_DENPYOU_NO_CTL", haishaNo + " ( " + jissekiNo + " )");
                        }

                        // 運転者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_UNTENSHA_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_UNTENSHA_CD_CTL", string.Empty);
                        }

                        // 運転者名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHAIN_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 16)
                            {
                                this.SetFieldName("DH_SHAIN_NAME_CTL", encoding.GetString(byteArray, 0, 16));
                            }
                            else
                            {
                                this.SetFieldName("DH_SHAIN_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SHAIN_NAME_CTL", string.Empty);
                        }

                        // 車種CD
                        index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHASHU_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHASHU_CD_CTL", string.Empty);
                        }

                        // 車種名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("DH_SHASHU_NAME_RYAKU_CTL", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("DH_SHASHU_NAME_RYAKU_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SHASHU_NAME_RYAKU_CTL", string.Empty);
                        }

                        // コースCD
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NAME_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_COURSE_NAME_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_COURSE_NAME_CD_CTL", string.Empty);
                        }

                        // コース名
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("DH_COURSE_NAME_CTL", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("DH_COURSE_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_COURSE_NAME_CTL", string.Empty);
                        }

                        // 車輌CD
                        index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHARYOU_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHARYOU_CD_CTL", string.Empty);
                        }

                        // 車輌名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("DH_SHARYOU_NAME_CTL", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("DH_SHARYOU_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SHARYOU_NAME_CTL", string.Empty);
                        }

                        // 出庫時メーター
                        index = dataTableHeaderTmp.Columns.IndexOf("SHUKKO_METER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHUKKO_METER_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHUKKO_METER_CTL", string.Empty);
                        }

                        // 出庫時間
                        index = dataTableHeaderTmp.Columns.IndexOf("SHUKKO_TIME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHUKKO_TIME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHUKKO_TIME_CTL", string.Empty);
                        }

                        // 帰庫時メーター
                        index = dataTableHeaderTmp.Columns.IndexOf("KIKO_METER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_KIKO_METER_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_KIKO_METER_CTL", string.Empty);
                        }

                        // 帰庫時間
                        index = dataTableHeaderTmp.Columns.IndexOf("KIKO_TIME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_KIKO_TIME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_KIKO_TIME_CTL", string.Empty);
                        }
                    }
                    #endregion - Header -

                    #region - Footer -
                    // Footerという概念なし
                    #endregion - Footer -

                    #endregion - 縦出力 -

                    break;
                case OutputTypeDef.Holizontal:  // 横出力

                    #region - 横出力 -

                    DataTable dataTableDetail2HeaderTmp = this.DataTableList["Detail2Header"];

                    #region - Header -

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                this.SetFieldName("DH_CORP_RYAKU_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                            }
                            else
                            {
                                this.SetFieldName("DH_CORP_RYAKU_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_CORP_RYAKU_NAME_VLB", string.Empty);
                        }

                        //// 発行日時
                        //index = dataTableHeaderTmp.Columns.IndexOf("DH_PRINT_DATE_VLB");
                        //this.SetFieldName("DH_PRINT_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        //// タイトル
                        //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                        //this.SetFieldName("DH_TITLE_FLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                        // 作業日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 14)
                            {
                                this.SetFieldName("DH_SAGYOU_DATE_CTL", encoding.GetString(byteArray, 0, 14));
                            }
                            else
                            {
                                this.SetFieldName("DH_SAGYOU_DATE_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SAGYOU_DATE_CTL", string.Empty);
                        }

                        // 作業日_曜日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_YOUBI");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 6)
                            {
                                this.SetFieldName("DH_SAGYOU_YOUBI_CTL", encoding.GetString(byteArray, 0, 6));
                            }
                            else
                            {
                                this.SetFieldName("DH_SAGYOU_YOUBI_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SAGYOU_YOUBI_CTL", string.Empty);
                        }

                        // 天候
                        index = dataTableHeaderTmp.Columns.IndexOf("WEATHER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 18)
                            {
                                this.SetFieldName("DH_WEATHER_CTL", encoding.GetString(byteArray, 0, 18));
                            }
                            else
                            {
                                this.SetFieldName("DH_WEATHER_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_WEATHER_CTL", string.Empty);
                        }

                        // 伝票番号
                        if (!this.isTeikiJitsuseki)
                        {   // 定期配車
                            index = dataTableHeaderTmp.Columns.IndexOf("HAISHA_NO");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                this.SetFieldName("DH_DENPYOU_NO_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {
                                this.SetFieldName("DH_DENPYOU_NO_CTL", string.Empty);
                            }
                        }
                        else
                        {   // 定期実績
                            this.SetFieldName("DH_DENPYOU_NO_FLB", "配車番号（実績）");

                            string haishaNo = string.Empty;
                            string jissekiNo = string.Empty;

                            index = dataTableHeaderTmp.Columns.IndexOf("HAISHA_NO");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                haishaNo = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                haishaNo = string.Empty;
                            }

                            index = dataTableHeaderTmp.Columns.IndexOf("JISSEKI_NO");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                jissekiNo = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                jissekiNo = string.Empty;
                            }

                            this.SetFieldName("DH_DENPYOU_NO_CTL", haishaNo + " ( " + jissekiNo + " )");
                        }

                        // 運転者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_UNTENSHA_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_UNTENSHA_CD_CTL", string.Empty);
                        }

                        // 運転者名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHAIN_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 16)
                            {
                                this.SetFieldName("DH_SHAIN_NAME_CTL", encoding.GetString(byteArray, 0, 16));
                            }
                            else
                            {
                                this.SetFieldName("DH_SHAIN_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SHAIN_NAME_CTL", string.Empty);
                        }

                        // 車種CD
                        index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHASHU_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHASHU_CD_CTL", string.Empty);
                        }

                        // 車種名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("DH_SHASHU_NAME_RYAKU_CTL", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("DH_SHASHU_NAME_RYAKU_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SHASHU_NAME_RYAKU_CTL", string.Empty);
                        }

                        // コースCD
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NAME_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_COURSE_NAME_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_COURSE_NAME_CD_CTL", string.Empty);
                        }

                        // コース名
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("DH_COURSE_NAME_CTL", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("DH_COURSE_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_COURSE_NAME_CTL", string.Empty);
                        }

                        // 車輌CD
                        index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHARYOU_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHARYOU_CD_CTL", string.Empty);
                        }

                        // 車輌名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                this.SetFieldName("DH_SHARYOU_NAME_CTL", encoding.GetString(byteArray, 0, 20));
                            }
                            else
                            {
                                this.SetFieldName("DH_SHARYOU_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                        }
                        else
                        {
                            this.SetFieldName("DH_SHARYOU_NAME_CTL", string.Empty);
                        }

                        // 出庫時メーター
                        index = dataTableHeaderTmp.Columns.IndexOf("SHUKKO_METER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHUKKO_METER_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHUKKO_METER_CTL", string.Empty);
                        }

                        // 出庫時間
                        index = dataTableHeaderTmp.Columns.IndexOf("SHUKKO_TIME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_SHUKKO_TIME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_SHUKKO_TIME_CTL", string.Empty);
                        }

                        // 帰庫時メーター
                        index = dataTableHeaderTmp.Columns.IndexOf("KIKO_METER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_KIKO_METER_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_KIKO_METER_CTL", string.Empty);
                        }

                        // 帰庫時間
                        index = dataTableHeaderTmp.Columns.IndexOf("KIKO_TIME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DH_KIKO_TIME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {
                            this.SetFieldName("DH_KIKO_TIME_CTL", string.Empty);
                        }
                    }
                    #endregion - Header -

                    #region - Detail2 Header-
                    // Deatail2 Headerという概念なし
                    #endregion

                    #region - Footer -
                    // Footerという概念なし
                    #endregion - Footer -

                    #endregion - 横出力 -

                    break;
            }
        }

        /// <summary>定期配車実績か否か取得する</summary>
        /// <returns>定期実績の場合は真、定期配車の場合は偽</returns>
        private bool IsTeikiJitsuseki()
        {
            string sql = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("COUNT(SYSTEM_ID) AS SYSTEM_ID_COUNT ");
            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY ");
            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo);

            sql = stringBuilder.ToString();

            // データーベースから取得
            DataTable dataTableSrcTmp = this.dao.GetDateForStringSql(sql);

            int index = dataTableSrcTmp.Columns.IndexOf("SYSTEM_ID_COUNT");

            if (!this.IsDBNull(dataTableSrcTmp.Rows[0].ItemArray[index]))
            {
                if (dataTableSrcTmp.Rows[0].ItemArray[index].ToString() != "0")
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

        /// <summary>会社名を取得する</summary>
        /// <returns>会社名</returns>
        private string GetCorpName()
        {
            string sql = string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("CORP_NAME ");
            stringBuilder.Append("FROM ");
            stringBuilder.Append("M_CORP_INFO ");
            stringBuilder.Append("WHERE SYS_ID = 0");

            sql = stringBuilder.ToString();

            // データーベースから取得
            DataTable dataTableTmp = this.dao.GetDateForStringSql(sql);

            int index = dataTableTmp.Columns.IndexOf("CORP_NAME");
            if (!this.IsDBNull(dataTableTmp.Rows[0].ItemArray[index]))
            {
                return (string)dataTableTmp.Rows[0].ItemArray[index];
            }
            else
            {
                return string.Empty;
            }
        }

        #region -- 定期配車系 --

        /// <summary>定期配車系情報（縦）（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringHeaderDetai1And2TeikiHaishaV()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SEQ AS SEQ, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SAGYOU_DATE AS SAGYOU_DATE, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER AS HAISHA_NO, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.UNTENSHA_CD AS UNTENSHA_CD, ");
            stringBuilder.Append("M_SHAIN.SHAIN_NAME_RYAKU AS SHAIN_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SHASHU_CD AS SHASHU_CD, ");
            stringBuilder.Append("M_SHASHU.SHASHU_NAME_RYAKU AS SHASHU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD AS COURSE_NAME_CD, ");
            stringBuilder.Append("M_COURSE_NAME.COURSE_NAME_RYAKU AS COURSE_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SHARYOU_CD AS SHARYOU_CD, ");
            stringBuilder.Append("M_SHARYOU.SHARYOU_NAME_RYAKU AS SHARYOU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD AS GYOUSHA_CD, ");
            stringBuilder.Append("M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.GENBA_CD AS GENBA_CD, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_SHOUSAI.HINMEI_CD AS HINMEI_CD, ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.MEISAI_BIKOU AS HINMEI_BIKOU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.NIOROSHI_NUMBER AS NIOROSHI_NUMBER, ");

            stringBuilder.Append("T_TEIKI_HAISHA_SHOUSAI.UNIT_CD, ");
            stringBuilder.Append("M_UNIT.UNIT_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_SHOUSAI.KANSAN_UNIT_CD, ");
            stringBuilder.Append("M_KANSAN_UNIT.UNIT_NAME_RYAKU AS KANSAN_UNIT_NAME_RYAKU ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_HAISHA_DETAIL ");
            stringBuilder.Append("ON T_TEIKI_HAISHA_DETAIL.SYSTEM_ID = T_TEIKI_HAISHA_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_DETAIL.SEQ = T_TEIKI_HAISHA_ENTRY.SEQ ");
            //stringBuilder.Append("INNER JOIN T_TEIKI_HAISHA_SHOUSAI ");
            stringBuilder.Append("LEFT JOIN T_TEIKI_HAISHA_SHOUSAI ");
            stringBuilder.Append("ON T_TEIKI_HAISHA_SHOUSAI.SYSTEM_ID = T_TEIKI_HAISHA_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_SHOUSAI.SEQ = T_TEIKI_HAISHA_ENTRY.SEQ ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_SHOUSAI.DETAIL_SYSTEM_ID = T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID ");
            stringBuilder.Append("LEFT JOIN M_SHAIN ");
            stringBuilder.Append("ON M_SHAIN.SHAIN_CD = T_TEIKI_HAISHA_ENTRY.UNTENSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_SHASHU ");
            stringBuilder.Append("ON M_SHASHU.SHASHU_CD = T_TEIKI_HAISHA_ENTRY.SHASHU_CD ");
            stringBuilder.Append("LEFT JOIN M_SHARYOU ");
            stringBuilder.Append("ON M_SHARYOU.GYOUSHA_CD = '' ");
            stringBuilder.Append("AND M_SHARYOU.SHARYOU_CD = T_TEIKI_HAISHA_ENTRY.SHARYOU_CD ");
            stringBuilder.Append("LEFT JOIN M_COURSE_NAME ");
            stringBuilder.Append("ON M_COURSE_NAME.COURSE_NAME_CD = T_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD ");
            stringBuilder.Append("LEFT JOIN M_GYOUSHA ");
            stringBuilder.Append("ON M_GYOUSHA.GYOUSHA_CD = T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_GENBA ");
            stringBuilder.Append("ON M_GENBA.GYOUSHA_CD = T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("AND M_GENBA.GENBA_CD = T_TEIKI_HAISHA_DETAIL.GENBA_CD ");
            stringBuilder.Append("LEFT JOIN M_HINMEI ");
            stringBuilder.Append("ON M_HINMEI.HINMEI_CD = T_TEIKI_HAISHA_SHOUSAI.HINMEI_CD ");

            stringBuilder.Append("LEFT JOIN M_UNIT AS M_UNIT ");
            stringBuilder.Append("ON M_UNIT.UNIT_CD = T_TEIKI_HAISHA_SHOUSAI.UNIT_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT AS M_KANSAN_UNIT ");
            stringBuilder.Append("ON M_UNIT.UNIT_CD = T_TEIKI_HAISHA_SHOUSAI.KANSAN_UNIT_CD ");

            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo);

            return stringBuilder.ToString();
        }

        /// <summary>定期配車系情報（横）（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringHeaderDetai1And2TeikiHaishaH()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SEQ AS SEQ, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SAGYOU_DATE AS SAGYOU_DATE, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER AS HAISHA_NO, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.UNTENSHA_CD AS UNTENSHA_CD, ");
            stringBuilder.Append("M_SHAIN.SHAIN_NAME_RYAKU AS SHAIN_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SHASHU_CD AS SHASHU_CD, ");
            stringBuilder.Append("M_SHASHU.SHASHU_NAME_RYAKU AS SHASHU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD AS COURSE_NAME_CD, ");
            stringBuilder.Append("M_COURSE_NAME.COURSE_NAME_RYAKU AS COURSE_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SHARYOU_CD AS SHARYOU_CD, ");
            stringBuilder.Append("M_SHARYOU.SHARYOU_NAME_RYAKU AS SHARYOU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD AS GYOUSHA_CD, ");
            stringBuilder.Append("M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.GENBA_CD AS GENBA_CD, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.MEISAI_BIKOU AS MEISAI_BIKOU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.NIOROSHI_NUMBER AS NIOROSHI_NUMBER, ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU AS KANSAN_UNIT_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.MEISAI_BIKOU AS MEISAI_BIKOU, ");
            stringBuilder.Append("SUM(T_TEIKI_HAISHA_SHOUSAI.STANDARD_VALUE) AS STANDARD_VALUE_TOTAL, ");
            stringBuilder.Append("SUM(T_TEIKI_HAISHA_SHOUSAI.KANSANCHI) AS KANSANCHI_TOTAL ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_HAISHA_DETAIL ");
            stringBuilder.Append("ON T_TEIKI_HAISHA_DETAIL.SYSTEM_ID = T_TEIKI_HAISHA_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_DETAIL.SEQ = T_TEIKI_HAISHA_ENTRY.SEQ ");
            //stringBuilder.Append("INNER JOIN T_TEIKI_HAISHA_SHOUSAI ");
            stringBuilder.Append("LEFT JOIN T_TEIKI_HAISHA_SHOUSAI ");
            stringBuilder.Append("ON T_TEIKI_HAISHA_SHOUSAI.SYSTEM_ID = T_TEIKI_HAISHA_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_SHOUSAI.SEQ = T_TEIKI_HAISHA_ENTRY.SEQ ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_SHOUSAI.DETAIL_SYSTEM_ID = T_TEIKI_HAISHA_DETAIL.DETAIL_SYSTEM_ID ");
            stringBuilder.Append("LEFT JOIN M_SHAIN ");
            stringBuilder.Append("ON M_SHAIN.SHAIN_CD = T_TEIKI_HAISHA_ENTRY.UNTENSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_SHASHU ");
            stringBuilder.Append("ON M_SHASHU.SHASHU_CD = T_TEIKI_HAISHA_ENTRY.SHASHU_CD ");
            stringBuilder.Append("LEFT JOIN M_SHARYOU ");
            stringBuilder.Append("ON M_SHARYOU.GYOUSHA_CD = '' ");
            stringBuilder.Append("AND M_SHARYOU.SHARYOU_CD = T_TEIKI_HAISHA_ENTRY.SHARYOU_CD ");
            stringBuilder.Append("LEFT JOIN M_COURSE_NAME ");
            stringBuilder.Append("ON M_COURSE_NAME.COURSE_NAME_CD = T_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD ");
            stringBuilder.Append("LEFT JOIN M_GYOUSHA ");
            stringBuilder.Append("ON M_GYOUSHA.GYOUSHA_CD = T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_GENBA ");
            stringBuilder.Append("ON M_GENBA.GYOUSHA_CD = T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("AND M_GENBA.GENBA_CD = T_TEIKI_HAISHA_DETAIL.GENBA_CD ");
            stringBuilder.Append("LEFT JOIN M_HINMEI ");
            stringBuilder.Append("ON M_HINMEI.HINMEI_CD = T_TEIKI_HAISHA_SHOUSAI.HINMEI_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_1 ");
            stringBuilder.Append("ON M_UNIT_1.UNIT_CD = T_TEIKI_HAISHA_SHOUSAI.UNIT_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_2 ");
            stringBuilder.Append("ON M_UNIT_2.UNIT_CD = T_TEIKI_HAISHA_SHOUSAI.KANSAN_UNIT_CD ");

            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo + " ");

            stringBuilder.Append("GROUP BY ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.GYOUSHA_CD, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.GENBA_CD, ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SEQ, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SAGYOU_DATE, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.UNTENSHA_CD, ");
            stringBuilder.Append("M_SHAIN.SHAIN_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SHASHU_CD, ");
            stringBuilder.Append("M_SHASHU.SHASHU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD, ");
            stringBuilder.Append("M_COURSE_NAME.COURSE_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SHARYOU_CD, ");
            stringBuilder.Append("M_SHARYOU.SHARYOU_NAME_RYAKU, ");
            stringBuilder.Append("M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.MEISAI_BIKOU, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.NIOROSHI_NUMBER, ");
            stringBuilder.Append("T_TEIKI_HAISHA_DETAIL.MEISAI_BIKOU ");

            return stringBuilder.ToString();
        }

        /// <summary>荷降明細部情報用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringNioroshiMeisaiTeikiHaisha()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.SEQ AS SEQ, ");
            stringBuilder.Append("T_TEIKI_HAISHA_NIOROSHI.NIOROSHI_GENBA_CD AS NIOROSHI_GENBA_CD, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_HAISHA_NIOROSHI ");
            stringBuilder.Append("ON T_TEIKI_HAISHA_NIOROSHI.SYSTEM_ID = T_TEIKI_HAISHA_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_NIOROSHI.SEQ = T_TEIKI_HAISHA_ENTRY.SEQ ");
            stringBuilder.Append("LEFT JOIN M_GENBA ");
            stringBuilder.Append("ON M_GENBA.GYOUSHA_CD = T_TEIKI_HAISHA_NIOROSHI.NIOROSHI_GYOUSHA_CD ");
            stringBuilder.Append("AND M_GENBA.GENBA_CD = T_TEIKI_HAISHA_NIOROSHI.NIOROSHI_GENBA_CD ");
            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo);

            return stringBuilder.ToString();
        }

        /// <summary>品名・単位情報（横バージョンに利用）用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringHinmeiTaniTeikiHaisha()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU1, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU2 ");

            stringBuilder.Append("FROM  ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_HAISHA_SHOUSAI ");
            stringBuilder.Append("ON T_TEIKI_HAISHA_SHOUSAI.SYSTEM_ID = T_TEIKI_HAISHA_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_SHOUSAI.SEQ = T_TEIKI_HAISHA_ENTRY.SEQ ");
            stringBuilder.Append("INNER JOIN M_HINMEI ");
            stringBuilder.Append("ON M_HINMEI.HINMEI_CD = T_TEIKI_HAISHA_SHOUSAI.HINMEI_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_1 ");
            stringBuilder.Append("ON M_UNIT_1.UNIT_CD = T_TEIKI_HAISHA_SHOUSAI.UNIT_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_2 ");
            stringBuilder.Append("ON M_UNIT_2.UNIT_CD = T_TEIKI_HAISHA_SHOUSAI.KANSAN_UNIT_CD ");

            stringBuilder.Append("WHERE  ");
            stringBuilder.Append("T_TEIKI_HAISHA_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo + " ");

            stringBuilder.Append("GROUP BY  ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU ");

            return stringBuilder.ToString();
        }

        #endregion -- 定期配車系 --

        #region -- 定期実績系 --

        /// <summary>定期実績系情報（縦）（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringHeaderDetai1And2TeikiJitsusekiV()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SEQ AS SEQ, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE AS SAGYOU_DATE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.WEATHER AS WEATHER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER AS HAISHA_NO, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.TEIKI_JISSEKI_NUMBER AS JISSEKI_NO, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.UNTENSHA_CD AS UNTENSHA_CD, ");
            stringBuilder.Append("M_SHAIN.SHAIN_NAME_RYAKU AS SHAIN_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHASHU_CD AS SHASHU_CD, ");
            stringBuilder.Append("M_SHASHU.SHASHU_NAME_RYAKU AS SHASHU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.COURSE_NAME_CD AS COURSE_NAME_CD, ");
            stringBuilder.Append("M_COURSE_NAME.COURSE_NAME_RYAKU AS COURSE_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHARYOU_CD AS SHARYOU_CD, ");
            stringBuilder.Append("M_SHARYOU.SHARYOU_NAME_RYAKU AS SHARYOU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_METER AS SHUKKO_METER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_HOUR AS SHUKKO_HOUR, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_MINUTE AS SHUKKO_MINUTE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_METER AS KIKO_METER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_HOUR AS KIKO_HOUR, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_MINUTE AS KIKO_MINUTE, ");
            //stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.SHUUSHUU_TIME AS SHUUSHUU_TIME, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.SHUUSHUU_TIME AS SHUUSHUU_TIME, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD AS GYOUSHA_CD, ");
            stringBuilder.Append("M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.GENBA_CD AS GENBA_CD, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.HINMEI_CD AS HINMEI_CD, ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.SUURYOU AS SUURYOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.KANSAN_SUURYOU AS KANSAN_SUURYOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.HINMEI_BIKOU AS HINMEI_BIKOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.NIOROSHI_NUMBER AS NIOROSHI_NUMBER, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU AS KANSAN_UNIT_NAME_RYAKU ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_JISSEKI_DETAIL ");
            stringBuilder.Append("ON T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID = T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_DETAIL.SEQ = T_TEIKI_JISSEKI_ENTRY.SEQ ");

            //stringBuilder.Append("AND T_TEIKI_JISSEKI_DETAIL.HINMEI_CD <> '' ");

            stringBuilder.Append("LEFT JOIN M_SHAIN ");
            stringBuilder.Append("ON M_SHAIN.SHAIN_CD = T_TEIKI_JISSEKI_ENTRY.UNTENSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_SHASHU ");
            stringBuilder.Append("ON M_SHASHU.SHASHU_CD = T_TEIKI_JISSEKI_ENTRY.SHASHU_CD ");
            stringBuilder.Append("LEFT JOIN M_SHARYOU ");
            stringBuilder.Append("ON M_SHARYOU.GYOUSHA_CD = '' ");
            stringBuilder.Append("AND M_SHARYOU.SHARYOU_CD = T_TEIKI_JISSEKI_ENTRY.SHARYOU_CD ");
            stringBuilder.Append("LEFT JOIN M_COURSE_NAME ");
            stringBuilder.Append("ON M_COURSE_NAME.COURSE_NAME_CD = T_TEIKI_JISSEKI_ENTRY.COURSE_NAME_CD ");
            stringBuilder.Append("LEFT JOIN M_GYOUSHA ");
            stringBuilder.Append("ON M_GYOUSHA.GYOUSHA_CD = T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_GENBA ");
            stringBuilder.Append("ON M_GENBA.GYOUSHA_CD = T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("AND M_GENBA.GENBA_CD = T_TEIKI_JISSEKI_DETAIL.GENBA_CD ");
            stringBuilder.Append("LEFT JOIN M_HINMEI ");
            stringBuilder.Append("ON M_HINMEI.HINMEI_CD = T_TEIKI_JISSEKI_DETAIL.HINMEI_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_1 ");
            stringBuilder.Append("ON M_UNIT_1.UNIT_CD = T_TEIKI_JISSEKI_DETAIL.UNIT_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_2 ");
            stringBuilder.Append("ON M_UNIT_2.UNIT_CD = T_TEIKI_JISSEKI_DETAIL.KANSAN_UNIT_CD ");

            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo);

            return stringBuilder.ToString();
        }

        /// <summary>定期実績系情報（横）（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringHeaderDetai1And2TeikiJitsusekiH()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SEQ AS SEQ, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE AS SAGYOU_DATE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.WEATHER AS WEATHER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER AS HAISHA_NO, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.TEIKI_JISSEKI_NUMBER AS JISSEKI_NO, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.UNTENSHA_CD AS UNTENSHA_CD, ");
            stringBuilder.Append("M_SHAIN.SHAIN_NAME_RYAKU AS SHAIN_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHASHU_CD AS SHASHU_CD, ");
            stringBuilder.Append("M_SHASHU.SHASHU_NAME_RYAKU AS SHASHU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.COURSE_NAME_CD AS COURSE_NAME_CD, ");
            stringBuilder.Append("M_COURSE_NAME.COURSE_NAME_RYAKU AS COURSE_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHARYOU_CD AS SHARYOU_CD, ");
            stringBuilder.Append("M_SHARYOU.SHARYOU_NAME_RYAKU AS SHARYOU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_METER AS SHUKKO_METER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_HOUR AS SHUKKO_HOUR, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_MINUTE AS SHUKKO_MINUTE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_METER AS KIKO_METER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_HOUR AS KIKO_HOUR, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_MINUTE AS KIKO_MINUTE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD AS GYOUSHA_CD, ");
            stringBuilder.Append("M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.GENBA_CD AS GENBA_CD, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.HINMEI_BIKOU AS HINMEI_BIKOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.NIOROSHI_NUMBER AS NIOROSHI_NUMBER, ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.SUURYOU AS SUURYOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.KANSAN_SUURYOU AS KANSAN_SUURYOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.KAISHUU_BIKOU AS KAISHUU_BIKOU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU AS KANSAN_UNIT_NAME_RYAKU ");
            //stringBuilder.Append("SUM(T_TEIKI_JISSEKI_DETAIL.SUURYOU) AS SUURYOU_TOTAL, ");
            //stringBuilder.Append("SUM(T_TEIKI_JISSEKI_DETAIL.KANSAN_SUURYOU) AS KANSANCHI_TOTAL ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_JISSEKI_DETAIL ");
            stringBuilder.Append("ON T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID = T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_DETAIL.SEQ = T_TEIKI_JISSEKI_ENTRY.SEQ ");

            //stringBuilder.Append("AND T_TEIKI_JISSEKI_DETAIL.HINMEI_CD <> '' ");

            stringBuilder.Append("LEFT JOIN M_SHAIN ");
            stringBuilder.Append("ON M_SHAIN.SHAIN_CD = T_TEIKI_JISSEKI_ENTRY.UNTENSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_SHASHU ");
            stringBuilder.Append("ON M_SHASHU.SHASHU_CD = T_TEIKI_JISSEKI_ENTRY.SHASHU_CD ");
            stringBuilder.Append("LEFT JOIN M_SHARYOU ");
            stringBuilder.Append("ON M_SHARYOU.GYOUSHA_CD = '' ");
            stringBuilder.Append("AND M_SHARYOU.SHARYOU_CD = T_TEIKI_JISSEKI_ENTRY.SHARYOU_CD ");
            stringBuilder.Append("LEFT JOIN M_COURSE_NAME ");
            stringBuilder.Append("ON M_COURSE_NAME.COURSE_NAME_CD = T_TEIKI_JISSEKI_ENTRY.COURSE_NAME_CD ");
            stringBuilder.Append("LEFT JOIN M_GYOUSHA ");
            stringBuilder.Append("ON M_GYOUSHA.GYOUSHA_CD = T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_GENBA ");
            stringBuilder.Append("ON M_GENBA.GYOUSHA_CD = T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD ");
            stringBuilder.Append("AND M_GENBA.GENBA_CD = T_TEIKI_JISSEKI_DETAIL.GENBA_CD ");
            stringBuilder.Append("LEFT JOIN M_HINMEI ");
            stringBuilder.Append("ON M_HINMEI.HINMEI_CD = T_TEIKI_JISSEKI_DETAIL.HINMEI_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_1 ");
            stringBuilder.Append("ON M_UNIT_1.UNIT_CD = T_TEIKI_JISSEKI_DETAIL.UNIT_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_2 ");
            stringBuilder.Append("ON M_UNIT_2.UNIT_CD = T_TEIKI_JISSEKI_DETAIL.KANSAN_UNIT_CD ");

            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo + " ");

            stringBuilder.Append("GROUP BY ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.GYOUSHA_CD, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.GENBA_CD, ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SEQ, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SAGYOU_DATE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.WEATHER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.TEIKI_JISSEKI_NUMBER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.UNTENSHA_CD, ");
            stringBuilder.Append("M_SHAIN.SHAIN_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHASHU_CD, ");
            stringBuilder.Append("M_SHASHU.SHASHU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.COURSE_NAME_CD, ");
            stringBuilder.Append("M_COURSE_NAME.COURSE_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHARYOU_CD, ");
            stringBuilder.Append("M_SHARYOU.SHARYOU_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_METER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_HOUR, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SHUKKO_MINUTE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_METER, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_HOUR, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.KIKO_MINUTE, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.HINMEI_BIKOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.NIOROSHI_NUMBER, ");
            stringBuilder.Append("M_GYOUSHA.GYOUSHA_NAME_RYAKU, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.SUURYOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.KANSAN_SUURYOU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_DETAIL.KAISHUU_BIKOU");

            return stringBuilder.ToString();
        }

        /// <summary>荷降明細部情報用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringNioroshiMeisaiTeikiJitsuseki()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.SEQ AS SEQ, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_NIOROSHI.NIOROSHI_GENBA_CD AS NIOROSHI_GENBA_CD, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU, ");
            stringBuilder.Append("T_TEIKI_JISSEKI_NIOROSHI.NIOROSHI_RYOU AS NIOROSHI_RYOU, ");
            // stringBuilder.Append("T_TEIKI_JISSEKI_NIOROSHI.HANNYUU_DATE AS HANNYUU_DATE ");
            stringBuilder.Append("T_TEIKI_JISSEKI_NIOROSHI.HANNYUU_DATE AS HANNYUU_DATE ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_JISSEKI_NIOROSHI ");
            stringBuilder.Append("ON T_TEIKI_JISSEKI_NIOROSHI.SYSTEM_ID = T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_NIOROSHI.SEQ = T_TEIKI_JISSEKI_ENTRY.SEQ ");
            stringBuilder.Append("LEFT JOIN M_GENBA ");
            stringBuilder.Append("ON M_GENBA.GYOUSHA_CD = T_TEIKI_JISSEKI_NIOROSHI.NIOROSHI_GYOUSHA_CD ");
            stringBuilder.Append("AND M_GENBA.GENBA_CD = T_TEIKI_JISSEKI_NIOROSHI.NIOROSHI_GENBA_CD ");

            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo);

            return stringBuilder.ToString();
        }

        /// <summary>品名・単位情報（横バージョンに利用）用ＳＱＬ文を取得する</summary>
        /// <returns>ＳＱＬ文</returns>
        private string GetSqlStringHinmeiTaniTeikiJitsuseki()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU AS HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU1, ");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU2, ");
            stringBuilder.Append("SUM(T_TEIKI_JISSEKI_DETAIL.SUURYOU) AS SUURYOU_TOTAL, ");
            stringBuilder.Append("SUM(T_TEIKI_JISSEKI_DETAIL.KANSAN_SUURYOU) AS KANSANCHI_TOTAL ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY ");
            stringBuilder.Append("INNER JOIN T_TEIKI_JISSEKI_DETAIL ");
            stringBuilder.Append("ON T_TEIKI_JISSEKI_DETAIL.SYSTEM_ID = T_TEIKI_JISSEKI_ENTRY.SYSTEM_ID ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_DETAIL.SEQ = T_TEIKI_JISSEKI_ENTRY.SEQ ");
            stringBuilder.Append("INNER JOIN M_HINMEI ");
            stringBuilder.Append("ON M_HINMEI.HINMEI_CD = T_TEIKI_JISSEKI_DETAIL.HINMEI_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_1 ");
            stringBuilder.Append("ON M_UNIT_1.UNIT_CD = T_TEIKI_JISSEKI_DETAIL.UNIT_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT M_UNIT_2 ");
            stringBuilder.Append("ON M_UNIT_2.UNIT_CD = T_TEIKI_JISSEKI_DETAIL.KANSAN_UNIT_CD ");

            stringBuilder.Append("WHERE ");
            stringBuilder.Append("T_TEIKI_JISSEKI_ENTRY.DELETE_FLG = 0 ");
            stringBuilder.Append("AND T_TEIKI_JISSEKI_ENTRY.TEIKI_HAISHA_NUMBER = " + this.TeikiHaishaNo + " ");

            stringBuilder.Append("GROUP BY ");
            stringBuilder.Append("M_HINMEI.HINMEI_NAME_RYAKU, ");
            stringBuilder.Append("M_UNIT_1.UNIT_NAME_RYAKU,");
            stringBuilder.Append("M_UNIT_2.UNIT_NAME_RYAKU");

            return stringBuilder.ToString();
        }

        #endregion -- 定期実績系 --

        /// <summary>データーベースからデータを取得する処理を実行する</summary>
        private void GetDatabaseData()
        {
            if (this.dao == null)
            {
                return;
            }

            if (this.IsSampleDataUse)
            {   // サンプルデータを使用する

                // サンプルデータテーブル作成
                this.CreateSampleData();

                return;
            }
            else
            {   // サンプルデータを使用しない
                this.DataTableList.Clear();
            }

            // 会社名取得
            this.corpName = this.GetCorpName();

            // 定期実績か否か取得
            if (this.TeikiType == TeikiTypeDef.Normal)
            {   // 通常
                this.isTeikiJitsuseki = this.IsTeikiJitsuseki();
            }
            else
            {
                this.isTeikiJitsuseki = this.TeikiType == TeikiTypeDef.Haisha ? false : true;
            }

            string sql = string.Empty;

            string format = string.Empty;
            string jyuuryouFormat = this.GetOutputFormat(OutputFormatTypeDef.JyuryouFormat);
            string suuryouFormat = this.GetOutputFormat(OutputFormatTypeDef.SuuryouFormat);
            int indexTmp;
            string tmp;

            DataTable dataTableHeaderDetai1And2Tmp = null;
            DataTable dataTableNioroshiMeisaiTmp = null;
            DataTable dataTableHinmeiTaniTmp = null;

            if (!this.isTeikiJitsuseki)
            {   // 定期配車
                if (this.OutputType == OutputTypeDef.Vertical)
                {   // 縦

                    // 定期配車系情報（縦）（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得
                    sql = this.GetSqlStringHeaderDetai1And2TeikiHaishaV();
                    dataTableHeaderDetai1And2Tmp = this.dao.GetDateForStringSql(sql);

                    // 荷降明細部情報用ＳＱＬ文を取得
                    sql = this.GetSqlStringNioroshiMeisaiTeikiHaisha();
                    dataTableNioroshiMeisaiTmp = this.dao.GetDateForStringSql(sql);
                }
                else
                {   // 横

                    // 定期配車系情報（横）（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得
                    sql = this.GetSqlStringHeaderDetai1And2TeikiHaishaH();
                    dataTableHeaderDetai1And2Tmp = this.dao.GetDateForStringSql(sql);

                    // 荷降明細部情報用ＳＱＬ文を取得
                    sql = this.GetSqlStringNioroshiMeisaiTeikiHaisha();
                    dataTableNioroshiMeisaiTmp = this.dao.GetDateForStringSql(sql);

                    // 品名・単位情報用ＳＱＬ文を取得
                    sql = this.GetSqlStringHinmeiTaniTeikiHaisha();
                    dataTableHinmeiTaniTmp = this.dao.GetDateForStringSql(sql);
                }
            }
            else
            {   // 定期実績
                if (this.OutputType == OutputTypeDef.Vertical)
                {   // 縦

                    // 定期実績系情報（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得
                    sql = this.GetSqlStringHeaderDetai1And2TeikiJitsusekiV();
                    dataTableHeaderDetai1And2Tmp = this.dao.GetDateForStringSql(sql);

                    // 荷降実績部情報用ＳＱＬ文を取得
                    sql = this.GetSqlStringNioroshiMeisaiTeikiJitsuseki();
                    dataTableNioroshiMeisaiTmp = this.dao.GetDateForStringSql(sql);
                }
                else
                {   // 横

                    // 定期実績系情報（ヘッダー＋明細＋詳細部）用ＳＱＬ文を取得
                    sql = this.GetSqlStringHeaderDetai1And2TeikiJitsusekiH();
                    dataTableHeaderDetai1And2Tmp = this.dao.GetDateForStringSql(sql);

                    // 荷降実績部情報用ＳＱＬ文を取得
                    sql = this.GetSqlStringNioroshiMeisaiTeikiJitsuseki();
                    dataTableNioroshiMeisaiTmp = this.dao.GetDateForStringSql(sql);

                    // 品名・単位情報用ＳＱＬ文を取得
                    sql = this.GetSqlStringHinmeiTaniTeikiJitsuseki();
                    dataTableHinmeiTaniTmp = this.dao.GetDateForStringSql(sql);
                }
            }

            // 品名の表示列順を算出
            int index = 0;
            int hinmeiIndex = 0;
            int intIdex = 0;
            string hinmei = string.Empty;
            string shushu_tani = string.Empty;
            string kansan_tani = string.Empty;
            string strhinmei_taihi = string.Empty;
            string strshushu_tani_taihi = string.Empty;
            string strkansan_tani_taihi = string.Empty;
            Dictionary<string, int> hinmeiList = new Dictionary<string, int>();
            if (dataTableHinmeiTaniTmp != null)
            {
                if (dataTableHinmeiTaniTmp.Rows.Count != 0)
                {
                    for (intIdex = 0; intIdex < dataTableHinmeiTaniTmp.Rows.Count; intIdex++)
                    {
                        index = dataTableHinmeiTaniTmp.Columns.IndexOf("HINMEI_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[intIdex].ItemArray[index]))
                        {
                            hinmei = (string)dataTableHinmeiTaniTmp.Rows[intIdex].ItemArray[index];
                        }
                        else
                        {
                            hinmei = string.Empty;
                        }

                        index = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU1");
                        if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[intIdex].ItemArray[index]))
                        {
                            shushu_tani = (string)dataTableHinmeiTaniTmp.Rows[intIdex].ItemArray[index];
                        }
                        else
                        {
                            shushu_tani = string.Empty;
                        }

                        index = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU2");
                        if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[intIdex].ItemArray[index]))
                        {
                            kansan_tani = (string)dataTableHinmeiTaniTmp.Rows[intIdex].ItemArray[index];
                        }
                        else
                        {
                            kansan_tani = string.Empty;
                        }

                        if (strhinmei_taihi != hinmei || strshushu_tani_taihi != shushu_tani || strkansan_tani_taihi != kansan_tani)
                        {
                            hinmeiList.Add(hinmei + "_" + shushu_tani + "_" + kansan_tani, intIdex);

                            strhinmei_taihi = hinmei;
                            strshushu_tani_taihi = shushu_tani;
                            strkansan_tani_taihi = kansan_tani;
                        }
                    }

                    hinmeiList.Add("__", intIdex);
                }
                else
                {
                    hinmeiList.Add("__", 0);
                }
            }

            DataTable dataTableTmp = null;
            DataRow rowTmp = null;
            DateTime dateTime;
            short hour;
            short minute;

            if (this.OutputType == OutputTypeDef.Vertical)
            {   // 縦

                #region - 縦出力 -

                #region - Header -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Header";

                #region - Header Columns -

                // 会社名
                dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                //// 発行日時
                //dataTableTmp.Columns.Add("PRINT_DATE");
                // タイトル
                dataTableTmp.Columns.Add("TITLE");

                // 作業日
                dataTableTmp.Columns.Add("SAGYOU_DATE");
                // 作業日_曜日
                dataTableTmp.Columns.Add("SAGYOU_YOUBI");
                // 天候
                dataTableTmp.Columns.Add("WEATHER");
                
                // 伝票番号（配車）
                dataTableTmp.Columns.Add("HAISHA_NO");
                if (this.isTeikiJitsuseki)
                {   // 定期実績

                    // 伝票番号（実績）
                    dataTableTmp.Columns.Add("JISSEKI_NO");
                }

                // 運転者CD
                dataTableTmp.Columns.Add("UNTENSHA_CD");
                // 運転者名
                dataTableTmp.Columns.Add("SHAIN_NAME");
                // 車種CD
                dataTableTmp.Columns.Add("SHASHU_CD");
                // 車種名
                dataTableTmp.Columns.Add("SHASHU_NAME_RYAKU");
                // コースCD
                dataTableTmp.Columns.Add("COURSE_NAME_CD");
                // コース名
                dataTableTmp.Columns.Add("COURSE_NAME");
                // 車輌CD
                dataTableTmp.Columns.Add("SHARYOU_CD");
                // 車輌名
                dataTableTmp.Columns.Add("SHARYOU_NAME");
                // 出庫時メーター
                dataTableTmp.Columns.Add("SHUKKO_METER");
                // 出庫時間
                dataTableTmp.Columns.Add("SHUKKO_TIME");
                // 帰庫時メーター
                dataTableTmp.Columns.Add("KIKO_METER");
                // 帰庫時間
                dataTableTmp.Columns.Add("KIKO_TIME");

                #endregion - Header Columns -

                rowTmp = dataTableTmp.NewRow();

                #region - Header Rows -

                // 会社名
                rowTmp["CORP_RYAKU_NAME"] = this.corpName;

                //// 発行日時
                //rowTmp["PRINT_DATE"] = dataTableSrcTmp.Rows[0].ItemArray[index];
                
                // タイトル
                rowTmp["TITLE"] = "運転日報";

                if (dataTableHeaderDetai1And2Tmp.Rows.Count != 0)
                {
                    // 作業日
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SAGYOU_DATE");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                        {
                            rowTmp["SAGYOU_DATE"] = string.Empty;
                            rowTmp["SAGYOU_YOUBI"] = string.Empty;
                        }
                        else
                        {
                            dateTime = (DateTime)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            rowTmp["SAGYOU_DATE"] = dateTime.ToString("yyyy年MM月dd日");

                            // 作業日_曜日
                            //index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SAGYOU_YOUBI");
                            rowTmp["SAGYOU_YOUBI"] = "(" + "日月火水木金土".Substring(int.Parse(dateTime.DayOfWeek.ToString("d")), 1) + ")";
                        }
                    }
                    else
                    {
                        rowTmp["SAGYOU_DATE"] = string.Empty;
                        rowTmp["SAGYOU_YOUBI"] = string.Empty;
                    }

                    // 天候
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["WEATHER"] = "天候(　　　　　)";
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("WEATHER");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["WEATHER"] = "天候(" + (string)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index] + ")";
                        }
                        else
                        {
                            rowTmp["WEATHER"] = string.Empty;
                        }
                    }

                    // 伝票番号（配車）
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("HAISHA_NO");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["HAISHA_NO"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["HAISHA_NO"] = string.Empty;
                    }


                    if (this.isTeikiJitsuseki)
                    {   // 定期実績

                        // 伝票番号（実績）
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("JISSEKI_NO");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["JISSEKI_NO"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["JISSEKI_NO"] = string.Empty;
                        }
                    }

                    // 運転者CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("UNTENSHA_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["UNTENSHA_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["UNTENSHA_CD"] = string.Empty;
                    }

                    // 運転者名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHAIN_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHAIN_NAME"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHAIN_NAME"] = string.Empty;
                    }

                    // 車種CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHASHU_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHASHU_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHASHU_CD"] = string.Empty;
                    }

                    // 車種名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHASHU_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHASHU_NAME_RYAKU"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHASHU_NAME_RYAKU"] = string.Empty;
                    }

                    // コースCD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("COURSE_NAME_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["COURSE_NAME_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["COURSE_NAME_CD"] = string.Empty;
                    }

                    // コース名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("COURSE_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["COURSE_NAME"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["COURSE_NAME"] = string.Empty;
                    }

                    // 車輌CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHARYOU_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHARYOU_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHARYOU_CD"] = string.Empty;
                    }

                    // 車輌名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHARYOU_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHARYOU_NAME"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHARYOU_NAME"] = string.Empty;
                    }

                    // 出庫時メーター
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["SHUKKO_METER"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHUKKO_METER");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                rowTmp["SHUKKO_METER"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["SHUKKO_METER"] = ((double)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]).ToString(jyuuryouFormat);
                            }
                        }
                        else
                        {
                            rowTmp["SHUKKO_METER"] = string.Empty;
                        }
                    }

                    // 出庫時間
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["SHUKKO_TIME"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHUKKO_HOUR");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                hour = -1;
                            }
                            else
                            {
                                hour = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            hour = -1;
                        }

                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHUKKO_MINUTE");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                minute = -1;
                            }
                            else
                            {
                                minute = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            minute = -1;
                        }

                        if (hour == -1 || minute == -1)
                        {
                            rowTmp["SHUKKO_TIME"] = string.Empty;
                        }
                        else
                        {
                            rowTmp["SHUKKO_TIME"] = string.Format("{0:D2}:{1:D2}", hour, minute);
                        }
                    }

                    // 帰庫時メーター
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["KIKO_METER"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KIKO_METER");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                rowTmp["KIKO_METER"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["KIKO_METER"] = ((double)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]).ToString(jyuuryouFormat);
                            }
                        }
                        else
                        {
                            rowTmp["KIKO_METER"] = string.Empty;
                        }
                    }

                    // 帰庫時間
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["KIKO_TIME"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KIKO_HOUR");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                hour = -1;
                            }
                            else
                            {
                                hour = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            hour = -1;
                        }

                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KIKO_MINUTE");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                minute = -1;
                            }
                            else
                            {
                                minute = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            minute = -1;
                        }

                        if (hour == -1 || minute == -1)
                        {
                            rowTmp["KIKO_TIME"] = string.Empty;
                        }
                        else
                        {
                            rowTmp["KIKO_TIME"] = string.Format("{0:D2}:{1:D2}", hour, minute);
                        }
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }

                #endregion - Header Rows -

                this.DataTableList.Add("Header", dataTableTmp);

                #endregion - Header -

                #region - Detail 1 -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Detail1";

                #region - Detail 1 Columns -

                // No
                dataTableTmp.Columns.Add("NO");
                // 収集時間
                dataTableTmp.Columns.Add("SHUUSHUU_JIKAN");
                // 業者CD
                dataTableTmp.Columns.Add("GYOUSHA_CD");
                // 業者名
                dataTableTmp.Columns.Add("GYOUSHA_MEI");
                // 現場CD
                dataTableTmp.Columns.Add("GENBA_CD");
                // 現場名
                dataTableTmp.Columns.Add("GENBA_MEI");
                // 品名CD
                dataTableTmp.Columns.Add("HINMEI_CD");
                // 品名
                dataTableTmp.Columns.Add("HINMEI_MEI");
                // 収集量
                dataTableTmp.Columns.Add("SUURYOU");
                // 単位
                dataTableTmp.Columns.Add("UNIT");
                // 換算収集量
                dataTableTmp.Columns.Add("KANSANSUURYOU");
                // 換算単位
                dataTableTmp.Columns.Add("KANSANUNIT");
                // 備考
                dataTableTmp.Columns.Add("BIKOU");
                // 荷降No
                dataTableTmp.Columns.Add("NIOROSHI_NUMBER");

                #endregion - Detail 1 Columns -

                #region - Detail 1 Rows -

                for (int i = 0; i < dataTableHeaderDetai1And2Tmp.Rows.Count; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // No
                    rowTmp["NO"] = (i + 1).ToString();

                    // 収集時間
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["SHUUSHUU_JIKAN"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHUUSHUU_TIME");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index].Equals(string.Empty))
                            {
                                rowTmp["SHUUSHUU_JIKAN"] = string.Empty;
                            }
                            else
                            {
                                dateTime = (DateTime)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                                rowTmp["SHUUSHUU_JIKAN"] = dateTime.ToString("hh:mm");
                            }
                        }
                        else
                        {
                            rowTmp["SHUUSHUU_JIKAN"] = string.Empty;
                        }
                    }

                    // 業者CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GYOUSHA_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["GYOUSHA_CD"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["GYOUSHA_CD"] = string.Empty;
                    }

                    // 業者名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GYOUSHA_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["GYOUSHA_MEI"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["GYOUSHA_MEI"] = string.Empty;
                    }

                    // 現場CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GENBA_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["GENBA_CD"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["GENBA_CD"] = string.Empty;
                    }

                    // 現場名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GENBA_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["GENBA_MEI"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["GENBA_MEI"] = string.Empty;
                    }

                    // 品名CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("HINMEI_CD");
                    string strHimei_Cd = string.Empty;
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        strHimei_Cd = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }

                    rowTmp["HINMEI_CD"] = strHimei_Cd;

                    // 品名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("HINMEI_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["HINMEI_MEI"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["HINMEI_MEI"] = string.Empty;
                    }

                    // 収集量
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["SUURYOU"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SUURYOU");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                        {
                            indexTmp = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("UNIT_NAME_RYAKU");
                            if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp]))
                            {
                                tmp = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp];

                                if (tmp.Equals("kg") || tmp.Equals("ｔ"))
                                {   // 重量系
                                    format = jyuuryouFormat;
                                }
                                else
                                {   // 数量系
                                    format = suuryouFormat;
                                }
                            }
                            else
                            {   // 数量系
                                format = suuryouFormat;
                            }

                            if (dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index].Equals(string.Empty))
                            {
                                rowTmp["SUURYOU"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["SUURYOU"] = ((double)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]).ToString(format);
                            }
                        }
                        else
                        {
                            rowTmp["SUURYOU"] = string.Empty;
                        }
                    }

                    // 単位
                    //if (!this.isTeikiJitsuseki)
                    //{   // 定期配車
                    //    rowTmp["UNIT"] = string.Empty;
                    //}
                    //else
                    //{   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("UNIT_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["UNIT"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UNIT"] = string.Empty;
                        }
                    //}

                    // 換算収集量
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["KANSANSUURYOU"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KANSAN_SUURYOU");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                        {
                            indexTmp = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KANSAN_UNIT_NAME_RYAKU");

                            if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp]))
                            {
                                tmp = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp];

                                if (tmp.Equals("kg") || tmp.Equals("ｔ"))
                                {   // 重量系
                                    format = jyuuryouFormat;
                                }
                                else
                                {   // 数量系
                                    format = suuryouFormat;
                                }
                            }
                            else
                            {   // 数量系
                                format = suuryouFormat;
                            }

                            rowTmp["KANSANSUURYOU"] = ((double)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]).ToString(format);
                        }
                        else
                        {
                            rowTmp["KANSANSUURYOU"] = string.Empty;
                        }
                    }

                    // 換算単位
                    //if (!this.isTeikiJitsuseki)
                    //{   // 定期配車
                    //    rowTmp["KANSANUNIT"] = string.Empty;
                    //}
                    //else
                    //{   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KANSAN_UNIT_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KANSANUNIT"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["KANSANUNIT"] = string.Empty;
                        }
                    //}

                    // 備考
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("HINMEI_BIKOU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["BIKOU"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["BIKOU"] = string.Empty;
                    }

                    // 荷降No
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("NIOROSHI_NUMBER");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        if ((int)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index] == 0)
                        {
                            rowTmp["NIOROSHI_NUMBER"] = string.Empty;
                        }
                        else
                        {
                            rowTmp["NIOROSHI_NUMBER"] = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                        }
                    }
                    else
                    {
                        rowTmp["NIOROSHI_NUMBER"] = string.Empty;
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }

                #endregion - Detail 1 Rows -

                this.DataTableList.Add("Detail1", dataTableTmp);

                #endregion - Detail 1 -

                #region - Detail 2 -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Detail2";

                #region - Detail 2 Columns -

                // No
                dataTableTmp.Columns.Add("NO2");
                // 荷降現場CD
                dataTableTmp.Columns.Add("NIOROSHIGENBA_CD");
                // 荷降現場名
                dataTableTmp.Columns.Add("NIOROSHIGENBA_MEI");
                // 計量値（重量）
                dataTableTmp.Columns.Add("KEIRYOUCHI");
                // 搬入時間
                dataTableTmp.Columns.Add("HANNYUU_JIKAN");

                #endregion - Detail 2 Columns -

                #region - Detail 2 Rows -

                for (int i = 0; i < dataTableNioroshiMeisaiTmp.Rows.Count; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // No
                    rowTmp["NO2"] = (i + 1).ToString();

                    // 荷降現場CD
                    index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("NIOROSHI_GENBA_CD");
                    if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["NIOROSHIGENBA_CD"] = dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["NIOROSHIGENBA_CD"] = string.Empty;
                    }

                    // 荷降現場名
                    index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("GENBA_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["NIOROSHIGENBA_MEI"] = dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["NIOROSHIGENBA_MEI"] = string.Empty;
                    }

                    // 計量値（重量）
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["KEIRYOUCHI"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("NIOROSHI_RYOU");
                        if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KEIRYOUCHI"] = ((double)dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]).ToString(jyuuryouFormat);
                        }
                        else
                        {
                            rowTmp["KEIRYOUCHI"] = string.Empty;
                        }
                    }

                    // 搬入時間
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["HANNYUU_JIKAN"] = string.Empty;
                    }
                    else
                    {
                        index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("HANNYUU_DATE");
                        if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                        {
                            if (dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index].Equals(string.Empty))
                            {
                                rowTmp["HANNYUU_JIKAN"] = string.Empty;
                            }
                            else
                            {
                                dateTime = (DateTime)dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index];
                                rowTmp["HANNYUU_JIKAN"] = dateTime.ToString("hh:mm");
                            }
                        }
                        else
                        {
                            rowTmp["HANNYUU_JIKAN"] = string.Empty;
                        }
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }

                #endregion - Detail 2 Rows -

                this.DataTableList.Add("Detail2", dataTableTmp);

                #endregion - Detail 2 -

                #endregion - 縦出力 -
            }
            else
            {   // 横

                #region - 横出力 -

                #region - Header -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Header";

                #region - Header Columns -

                // 会社名
                dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                //// 発行日時
                //dataTableTmp.Columns.Add("PRINT_DATE");
                // タイトル
                dataTableTmp.Columns.Add("TITLE");

                // 作業日
                dataTableTmp.Columns.Add("SAGYOU_DATE");
                // 作業日_曜日
                dataTableTmp.Columns.Add("SAGYOU_YOUBI");
                // 天候
                dataTableTmp.Columns.Add("WEATHER");

                // 伝票番号（配車）
                dataTableTmp.Columns.Add("HAISHA_NO");

                if (this.isTeikiJitsuseki)
                {   // 定期実績

                    // 伝票番号（実績）
                    dataTableTmp.Columns.Add("JISSEKI_NO");
                }

                // 運転者CD
                dataTableTmp.Columns.Add("UNTENSHA_CD");
                // 運転者名
                dataTableTmp.Columns.Add("SHAIN_NAME");
                // 車種CD
                dataTableTmp.Columns.Add("SHASHU_CD");
                // 車種名
                dataTableTmp.Columns.Add("SHASHU_NAME_RYAKU");
                // コースCD
                dataTableTmp.Columns.Add("COURSE_NAME_CD");
                // コース名
                dataTableTmp.Columns.Add("COURSE_NAME");
                // 車輌CD
                dataTableTmp.Columns.Add("SHARYOU_CD");
                // 車輌名
                dataTableTmp.Columns.Add("SHARYOU_NAME");
                // 出庫時メーター
                dataTableTmp.Columns.Add("SHUKKO_METER");
                // 出庫時間
                dataTableTmp.Columns.Add("SHUKKO_TIME");
                // 帰庫時メーター
                dataTableTmp.Columns.Add("KIKO_METER");
                // 帰庫時間
                dataTableTmp.Columns.Add("KIKO_TIME");

                #endregion - Header Columns -

                if (dataTableHeaderDetai1And2Tmp.Rows.Count != 0)
                {
                    rowTmp = dataTableTmp.NewRow();

                    #region - Header Rows -

                    // 会社名
                    rowTmp["CORP_RYAKU_NAME"] = this.corpName;

                    //// 発行日時
                    //rowTmp["PRINT_DATE"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    
                    // タイトル
                    rowTmp["TITLE"] = "運転日報";

                    // 作業日
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SAGYOU_DATE");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                        {
                            rowTmp["SAGYOU_DATE"] = string.Empty;
                            rowTmp["SAGYOU_YOUBI"] = string.Empty;
                        }
                        else
                        {
                            dateTime = (DateTime)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            rowTmp["SAGYOU_DATE"] = dateTime.ToString("yyyy年MM月dd日");

                            // 作業日_曜日
                            index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SAGYOU_YOUBI");
                            rowTmp["SAGYOU_YOUBI"] = "(" + "日月火水木金土".Substring(int.Parse(dateTime.DayOfWeek.ToString("d")), 1) + ")";
                        }
                    }
                    else
                    {
                        rowTmp["SAGYOU_DATE"] = string.Empty;
                        rowTmp["SAGYOU_YOUBI"] = string.Empty;
                    }

                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車

                        // 天候
                        rowTmp["WEATHER"] = "天候(　　　　　)";
                    }
                    else
                    {   // 定期実績

                        // 天候
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("WEATHER");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["WEATHER"] = "天候(" + (string)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index] + ")";
                        }
                        else
                        {
                            rowTmp["WEATHER"] = string.Empty;
                        }
                    }

                    // 伝票番号（配車）
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("HAISHA_NO");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["HAISHA_NO"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["HAISHA_NO"] = string.Empty;
                    }

                    if (this.isTeikiJitsuseki)
                    {   // 定期実績

                        // 伝票番号（実績）
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("JISSEKI_NO");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["JISSEKI_NO"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["JISSEKI_NO"] = string.Empty;
                        }
                    }

                    // 運転者CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("UNTENSHA_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["UNTENSHA_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["UNTENSHA_CD"] = string.Empty;
                    }

                    // 運転者名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHAIN_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHAIN_NAME"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHAIN_NAME"] = string.Empty;
                    }

                    // 車種CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHASHU_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHASHU_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHASHU_CD"] = string.Empty;
                    }

                    // 車種名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHASHU_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHASHU_NAME_RYAKU"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHASHU_NAME_RYAKU"] = string.Empty;
                    }

                    // コースCD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("COURSE_NAME_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["COURSE_NAME_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["COURSE_NAME_CD"] = string.Empty;
                    }

                    // コース名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("COURSE_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["COURSE_NAME"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["COURSE_NAME"] = string.Empty;
                    }

                    // 車輌CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHARYOU_CD");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHARYOU_CD"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHARYOU_CD"] = string.Empty;
                    }

                    // 車輌名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHARYOU_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                    {
                        rowTmp["SHARYOU_NAME"] = dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["SHARYOU_NAME"] = string.Empty;
                    }

                    // 出庫時メーター
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["SHUKKO_METER"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHUKKO_METER");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                rowTmp["SHUKKO_METER"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["SHUKKO_METER"] = ((double)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]).ToString(jyuuryouFormat);
                            }
                        }
                        else
                        {
                            rowTmp["SHUKKO_METER"] = string.Empty;
                        }
                    }

                    // 出庫時間
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["SHUKKO_TIME"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHUKKO_HOUR");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                hour = -1;
                            }
                            else
                            {
                                hour = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            hour = -1;
                        }

                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SHUKKO_MINUTE");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                minute = -1;
                            }
                            else
                            {
                                minute = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            minute = -1;
                        }

                        if (hour == -1 || minute == -1)
                        {
                            rowTmp["SHUKKO_TIME"] = string.Empty;
                        }
                        else
                        {
                            rowTmp["SHUKKO_TIME"] = string.Format("{0:D2}:{1:D2}", hour, minute);
                        }
                    }

                    // 帰庫時メーター
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["KIKO_METER"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KIKO_METER");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                rowTmp["KIKO_METER"] = string.Empty;
                            }
                            else
                            {
                                rowTmp["KIKO_METER"] = ((double)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]).ToString(jyuuryouFormat);
                            }
                        }
                        else
                        {
                            rowTmp["KIKO_METER"] = string.Empty;
                        }
                    }

                    // 帰庫時間
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["KIKO_TIME"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KIKO_HOUR");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                hour = -1;
                            }
                            else
                            {
                                hour = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            hour = -1;
                        }

                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KIKO_MINUTE");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index]))
                        {
                            if (dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index].Equals(string.Empty))
                            {
                                minute = -1;
                            }
                            else
                            {
                                minute = (short)dataTableHeaderDetai1And2Tmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {
                            minute = -1;
                        }

                        if (hour == -1 || minute == -1)
                        {
                            rowTmp["KIKO_TIME"] = string.Empty;
                        }
                        else
                        {
                            rowTmp["KIKO_TIME"] = string.Format("{0:D2}:{1:D2}", hour, minute);
                        }
                    }

                    dataTableTmp.Rows.Add(rowTmp);

                    #endregion - Header Rows -
                }

                this.DataTableList.Add("Header", dataTableTmp);

                #endregion - Header -

                #region - Detail 1 -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Detail 1";

                #region - Detail 1 Columns -

                // No
                dataTableTmp.Columns.Add("NO2");
                // 荷降現場CD
                dataTableTmp.Columns.Add("NIOROSHIGENBA_CD");
                // 荷降現場名
                dataTableTmp.Columns.Add("NIOROSHIGENBA_MEI");
                // 計量値（重量）
                dataTableTmp.Columns.Add("KEIRYOUCHI");
                // 搬入時間
                dataTableTmp.Columns.Add("HANNYUU_JIKAN");

                #endregion - Detail 1 Columns -

                #region - Detail 1 Rows -

                for (int i = 0; i < dataTableNioroshiMeisaiTmp.Rows.Count; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // No
                    rowTmp["NO2"] = (i + 1).ToString();

                    // 荷降現場CD
                    index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("NIOROSHI_GENBA_CD");
                    if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["NIOROSHIGENBA_CD"] = dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["NIOROSHIGENBA_CD"] = string.Empty;
                    }

                    // 荷降現場名
                    index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("GENBA_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                    {
                        rowTmp["NIOROSHIGENBA_MEI"] = dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        rowTmp["NIOROSHIGENBA_MEI"] = string.Empty;
                    }

                    // 計量値（重量）
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["KEIRYOUCHI"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("NIOROSHI_RYOU");
                        if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KEIRYOUCHI"] = ((double)dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]).ToString(jyuuryouFormat);
                        }
                        else
                        {
                            rowTmp["KEIRYOUCHI"] = string.Empty;
                        }
                    }

                    // 搬入時間
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["HANNYUU_JIKAN"] = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableNioroshiMeisaiTmp.Columns.IndexOf("HANNYUU_DATE");
                        if (!this.IsDBNull(dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index]))
                        {
                            dateTime = (DateTime)dataTableNioroshiMeisaiTmp.Rows[i].ItemArray[index];
                            rowTmp["HANNYUU_JIKAN"] = dateTime.ToString("hh:mm");
                        }
                        else
                        {
                            rowTmp["HANNYUU_JIKAN"] = string.Empty;
                        }
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }

                #endregion - Detail 1 Rows -

                this.DataTableList.Add("Detail1", dataTableTmp);

                #endregion - Detail 1 -

                #region - Detail 2 -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Detail2";

                #region - Detail 2 Header -

                for (int i = 1; i <= dataTableHinmeiTaniTmp.Rows.Count + 1; i++)
                {
                    // 品名Ｎ
                    dataTableTmp.Columns.Add(string.Format("HINMEI{0}", i));
                    dataTableTmp.Columns.Add(string.Format("UNIT{0}", i));
                    dataTableTmp.Columns.Add(string.Format("KANSANUNIT{0}", i));
                }

                if (dataTableHinmeiTaniTmp.Rows.Count != 0)
                {
                    rowTmp = dataTableTmp.NewRow();

                    for (int i = 0; i < dataTableHinmeiTaniTmp.Rows.Count + 1; i++)
                    {
                        if (i < dataTableHinmeiTaniTmp.Rows.Count)
                        {
                            // 品名Ｎ
                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("HINMEI_NAME_RYAKU");
                            if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp[string.Format("HINMEI{0}", i + 1)] = dataTableHinmeiTaniTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                rowTmp[string.Format("HINMEI{0}", i + 1)] = string.Empty;
                            }

                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU1");
                            if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp[string.Format("UNIT{0}", i + 1)] = dataTableHinmeiTaniTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                rowTmp[string.Format("UNIT{0}", i + 1)] = string.Empty;
                            }

                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU2");
                            if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp[string.Format("KANSANUNIT{0}", i + 1)] = dataTableHinmeiTaniTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                rowTmp[string.Format("KANSANUNIT{0}", i + 1)] = string.Empty;
                            }
                        }
                        else
                        {
                            // 品名Ｎ
                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("HINMEI_NAME_RYAKU");
                            rowTmp[string.Format("HINMEI{0}", i + 1)] = string.Empty;

                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU1");
                            rowTmp[string.Format("UNIT{0}", i + 1)] = string.Empty;

                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU2");
                            rowTmp[string.Format("KANSANUNIT{0}", i + 1)] = string.Empty;
                        }
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }

                #endregion - Detail 2 Header -

                this.DataTableList.Add("Detail2Header", dataTableTmp);

                #region - Detail 2 Detail -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Detail2";

                #region - Detail 2 Columns -

                // 業者CD
                dataTableTmp.Columns.Add("GYOUSHA_CD");
                // 業者名
                dataTableTmp.Columns.Add("GYOUSHA_MEI");
                // 現場CD
                dataTableTmp.Columns.Add("GENBA_CD");
                // 現場名
                dataTableTmp.Columns.Add("GENBA_MEI");

                if (dataTableHinmeiTaniTmp.Rows.Count != 0)
                {
                    for (int i = 1; i <= dataTableHinmeiTaniTmp.Rows.Count + 1; i++)
                    {
                        // 収集量(N)
                        dataTableTmp.Columns.Add(string.Format("SUURYOU{0}", i));
                        dataTableTmp.Columns.Add(string.Format("KANSANSUURYOU{0}", i));
                    }
                }
                else
                {
                    // 収集量(N)
                    dataTableTmp.Columns.Add("SUURYOU1");
                    dataTableTmp.Columns.Add("KANSANSUURYOU1");
                }

                // 備考
                dataTableTmp.Columns.Add("BIKOU");
                // 荷降No
                dataTableTmp.Columns.Add("NIOROSHI_NUMBER");

                #endregion - Detail 2 Columns -

                #region - Detail 2 Rows -

                string key = string.Empty;
                Dictionary<string, HinmeiInfo> headerDetai1And2Tmp = new Dictionary<string, HinmeiInfo>();
                string gyoushaCD = string.Empty;
                string genbaCD = string.Empty;

                HinmeiInfo hinmeiInfo = null;
                Suuryou suuryou = null;
                for (int i = 0; i < dataTableHeaderDetai1And2Tmp.Rows.Count; i++)
                {
                    // 業者CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GYOUSHA_CD");
                    gyoushaCD = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];

                    // 現場CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GENBA_CD");
                    genbaCD = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];

                    key = gyoushaCD + "_" + genbaCD;
                    if (headerDetai1And2Tmp.ContainsKey(key))
                    {   // キーが含まれている
                        hinmeiInfo = headerDetai1And2Tmp[key];
                    }
                    else
                    {   // キーが含まれていない
                        hinmeiInfo = new HinmeiInfo();
                    }

                    // 業者CD
                    hinmeiInfo.GyoushaCD = gyoushaCD;

                    // 現場CD
                    hinmeiInfo.GenbaCD = genbaCD;

                    // 業者名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GYOUSHA_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        hinmeiInfo.GyoushaName = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        hinmeiInfo.GyoushaName = string.Empty;
                    }

                    // 現場名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GENBA_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        hinmeiInfo.GenbaName = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        hinmeiInfo.GenbaName = string.Empty;
                    }

                    // 品名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("HINMEI_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        hinmei = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        hinmei = string.Empty;
                    }

                    // 単位名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("UNIT_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        shushu_tani = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        shushu_tani = string.Empty;
                    }

                    // 単位換算名
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KANSAN_UNIT_NAME_RYAKU");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        kansan_tani = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        kansan_tani = string.Empty;
                    }

                    // 表示インデックス取得
                    hinmeiIndex = hinmeiList[hinmei + "_" + shushu_tani + "_" + kansan_tani];

                    suuryou = new Suuryou();
                    suuryou.HinmeiIndex = hinmeiIndex;

                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        suuryou.SyuusyuuRyou = string.Empty;
                        suuryou.KansanSuuryou = string.Empty;
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("SUURYOU");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                        {
                            indexTmp = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("UNIT_NAME_RYAKU");
                            if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp]))
                            {
                                tmp = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp];

                                if (tmp.Equals("kg") || tmp.Equals("ｔ"))
                                {   // 重量系
                                    format = jyuuryouFormat;
                                }
                                else
                                {   // 数量系
                                    format = suuryouFormat;
                                }
                            }
                            else
                            {   // 数量系
                                format = suuryouFormat;
                            }

                            suuryou.SyuusyuuRyou = ((double)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]).ToString(format);
                        }
                        else
                        {
                            suuryou.SyuusyuuRyou = string.Empty;
                        }

                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KANSAN_SUURYOU");
                        if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                        {
                            indexTmp = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KANSAN_UNIT_NAME_RYAKU");

                            if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp]))
                            {
                                tmp = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[indexTmp];

                                if (tmp.Equals("kg") || tmp.Equals("ｔ"))
                                {   // 重量系
                                    format = jyuuryouFormat;
                                }
                                else
                                {   // 数量系
                                    format = suuryouFormat;
                                }
                            }
                            else
                            {   // 数量系
                                format = suuryouFormat;
                            }

                            suuryou.KansanSuuryou = ((double)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]).ToString(format);
                        }
                        else
                        {
                            suuryou.KansanSuuryou = string.Empty;
                        }
                    }

                    hinmeiInfo.SuuryouList.Add(suuryou);

                    // 備考
                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("MEISAI_BIKOU");
                    }
                    else
                    {   // 定期実績
                        index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("KAISHUU_BIKOU");
                    }

                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        hinmeiInfo.Bikou = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        hinmeiInfo.Bikou = string.Empty;
                    }

                    // 荷降No
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("NIOROSHI_NUMBER");
                    if (!this.IsDBNull(dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index]))
                    {
                        hinmeiInfo.NioroshiNo = dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index].ToString();
                    }
                    else
                    {
                        hinmeiInfo.NioroshiNo = string.Empty;
                    }

                    headerDetai1And2Tmp[key] = hinmeiInfo;
                }

                string prevKey = string.Empty;
                key = string.Empty;
                for (int i = 0; i < dataTableHeaderDetai1And2Tmp.Rows.Count; i++)
                {
                    // 業者CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GYOUSHA_CD");
                    gyoushaCD = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];

                    // 現場CD
                    index = dataTableHeaderDetai1And2Tmp.Columns.IndexOf("GENBA_CD");
                    genbaCD = (string)dataTableHeaderDetai1And2Tmp.Rows[i].ItemArray[index];

                    key = gyoushaCD + "_" + genbaCD;

                    if (prevKey == key)
                    {
                        continue;
                    }

                    prevKey = key;

                    hinmeiInfo = headerDetai1And2Tmp[key];

                    rowTmp = dataTableTmp.NewRow();

                    for (int j = 1; j <= dataTableHinmeiTaniTmp.Rows.Count; j++)
                    {
                        // 収集量(N)
                        rowTmp[string.Format("SUURYOU{0}", j)] = string.Empty;

                        // 収集量：換算値(N)
                        rowTmp[string.Format("KANSANSUURYOU{0}", j)] = string.Empty;
                    }

                    // 業者CD
                    rowTmp["GYOUSHA_CD"] = hinmeiInfo.GyoushaCD;
                    // 業者名
                    rowTmp["GYOUSHA_MEI"] = hinmeiInfo.GyoushaName;
                    // 現場CD
                    rowTmp["GENBA_CD"] = hinmeiInfo.GenbaCD;
                    // 現場名
                    rowTmp["GENBA_MEI"] = hinmeiInfo.GenbaName;

                    for (int j = 0; j < hinmeiInfo.SuuryouList.Count; j++)
                    {
                        suuryou = hinmeiInfo.SuuryouList[j];

                        hinmeiIndex = suuryou.HinmeiIndex;

                        if (this.isTeikiJitsuseki)
                        {   // 定期実績

                            // 収集量(N)
                            rowTmp[string.Format("SUURYOU{0}", hinmeiIndex + 1)] = suuryou.SyuusyuuRyou;

                            // 収集量：換算値(N)
                            rowTmp[string.Format("KANSANSUURYOU{0}", hinmeiIndex + 1)] = suuryou.KansanSuuryou;
                        }
                    }

                    // 備考
                    rowTmp["BIKOU"] = hinmeiInfo.Bikou;

                    // 荷降No
                    if (hinmeiInfo.NioroshiNo.Equals(string.Empty) || int.Parse(hinmeiInfo.NioroshiNo) == 0)
                    {
                        rowTmp["NIOROSHI_NUMBER"] = string.Empty;
                    }
                    else
                    {
                        rowTmp["NIOROSHI_NUMBER"] = hinmeiInfo.NioroshiNo;
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }

                #endregion - Detail 2 Rows -

                this.DataTableList.Add("Detail2", dataTableTmp);

                #endregion - Detail 2 Detail -

                #endregion - Detail 2 -

                #region - Footer -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Footer";

                #region - Footer Columns -

                if (dataTableHinmeiTaniTmp.Rows.Count != 0)
                {
                    for (int j = 0; j < dataTableHinmeiTaniTmp.Rows.Count + 1; j++)
                    {
                        // 収集量・合計(1～N)
                        dataTableTmp.Columns.Add(string.Format("SUURYOU_GOUKEI{0}", j + 1));
                        // 換算収集量・合計(1～N)
                        dataTableTmp.Columns.Add(string.Format("KANSANSUURYOU_GOUKEI{0}", j + 1));
                    }
                }
                else
                {
                    // 収集量・合計(1～N)
                    dataTableTmp.Columns.Add("SUURYOU_GOUKEI1");
                    // 換算収集量・合計(1～N)
                    dataTableTmp.Columns.Add("KANSANSUURYOU_GOUKEI1");
                }

                #endregion - Footer Columns -

                #region - Footer Rows -

                if (dataTableHinmeiTaniTmp.Rows.Count != 0)
                {
                    rowTmp = dataTableTmp.NewRow();

                    for (int j = 0; j < dataTableHinmeiTaniTmp.Rows.Count; j++)
                    {
                        if (!this.isTeikiJitsuseki)
                        {   // 定期配車
                            rowTmp[string.Format("SUURYOU_GOUKEI{0}", j + 1)] = string.Empty;
                        }
                        else
                        {   // 定期実績

                            // 収集量・合計(1～N)
                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("SUURYOU_TOTAL");
                            if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[j].ItemArray[index]))
                            {
                                if (dataTableHinmeiTaniTmp.Rows[j].ItemArray[index].Equals(string.Empty))
                                {
                                    rowTmp[string.Format("SUURYOU_GOUKEI{0}", j + 1)] = string.Empty;
                                }
                                else
                                {
                                    indexTmp = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU1");

                                    if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[j].ItemArray[indexTmp]))
                                    {
                                        tmp = (string)dataTableHinmeiTaniTmp.Rows[j].ItemArray[indexTmp];

                                        if (tmp.Equals("kg") || tmp.Equals("ｔ"))
                                        {   // 重量系
                                            format = jyuuryouFormat;
                                        }
                                        else
                                        {   // 数量系
                                            format = suuryouFormat;
                                        }
                                    }
                                    else
                                    {   // 数量系
                                        format = suuryouFormat;
                                    }
                                    
                                    rowTmp[string.Format("SUURYOU_GOUKEI{0}", j + 1)] = ((double)dataTableHinmeiTaniTmp.Rows[j].ItemArray[index]).ToString(format);
                                }
                            }
                            else
                            {
                                rowTmp[string.Format("SUURYOU_GOUKEI{0}", j + 1)] = string.Empty;
                            }

                            // 換算収集量・合計(1～N)
                            index = dataTableHinmeiTaniTmp.Columns.IndexOf("KANSANCHI_TOTAL");
                            if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[j].ItemArray[index]))
                            {
                                if (dataTableHinmeiTaniTmp.Rows[j].ItemArray[index].Equals(string.Empty))
                                {
                                    rowTmp[string.Format("KANSANSUURYOU_GOUKEI{0}", j + 1)] = string.Empty;
                                }
                                else
                                {
                                    indexTmp = dataTableHinmeiTaniTmp.Columns.IndexOf("UNIT_NAME_RYAKU2");

                                    if (!this.IsDBNull(dataTableHinmeiTaniTmp.Rows[j].ItemArray[indexTmp]))
                                    {
                                        tmp = (string)dataTableHinmeiTaniTmp.Rows[j].ItemArray[indexTmp];

                                        if (tmp.Equals("kg") || tmp.Equals("ｔ"))
                                        {   // 重量系
                                            format = jyuuryouFormat;
                                        }
                                        else
                                        {   // 数量系
                                            format = suuryouFormat;
                                        }
                                    }
                                    else
                                    {   // 数量系
                                        format = suuryouFormat;
                                    }

                                    rowTmp[string.Format("KANSANSUURYOU_GOUKEI{0}", j + 1)] = ((double)dataTableHinmeiTaniTmp.Rows[j].ItemArray[index]).ToString(format);
                                }
                            }
                            else
                            {
                                rowTmp[string.Format("KANSANSUURYOU_GOUKEI{0}", j + 1)] = string.Empty;
                            }
                        }
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }
                else
                {
                    rowTmp = dataTableTmp.NewRow();

                    if (!this.isTeikiJitsuseki)
                    {   // 定期配車
                        rowTmp["SUURYOU_GOUKEI1"] = string.Empty;
                    }
                    else
                    {   // 定期実績

                        // 収集量・合計(1～N)
                        rowTmp["SUURYOU_GOUKEI1"] = string.Empty;

                        // 換算収集量・合計(1～N)
                        rowTmp["KANSANSUURYOU_GOUKEI1"] = string.Empty;
                    }

                    dataTableTmp.Rows.Add(rowTmp);
                }

                #endregion - Footer Rows -

                this.DataTableList.Add("Footer", dataTableTmp);

                #endregion - Footer -

                #endregion - 横出力 -
            }
        }

        /// <summary>出力フォーマット文字列を取得する</summary>
        /// <param name="outputFormatType">出力フォーマットタイプ</param>
        /// <returns>出力フォーマット文字列</returns>
        private string GetOutputFormat(OutputFormatTypeDef outputFormatType)
        {
            string sql;
            string outputFormat;
            DataTable dataTableTmp;

            switch (outputFormatType)
            {
                case OutputFormatTypeDef.JyuryouFormat:
                    sql = "SELECT M_SYS_INFO.SYS_JYURYOU_FORMAT FROM M_SYS_INFO";
                    dataTableTmp = this.dao.GetDateForStringSql(sql);
                    outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];

                    break;
                case OutputFormatTypeDef.SuuryouFormat:
                    sql = "SELECT M_SYS_INFO.SYS_SUURYOU_FORMAT FROM M_SYS_INFO";
                    dataTableTmp = this.dao.GetDateForStringSql(sql);
                    outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];

                    break;
                case OutputFormatTypeDef.TankaFormat:
                    sql = "SELECT M_SYS_INFO.SYS_TANKA_FORMAT FROM M_SYS_INFO";
                    dataTableTmp = this.dao.GetDateForStringSql(sql);
                    outputFormat = (string)dataTableTmp.Rows[0].ItemArray[0];
                            
                    break;
                default:
                    outputFormat = "#,##0";

                    break;
            }

            return outputFormat;
        }

        #endregion - Methods -

        #region - Inner Class -

        /// <summary>品名情報を表すクラス・コントロール</summary>
        private class HinmeiInfo
        {
            #region - Constructors -

            /// <summary>Initializes a new instance of the <see cref="HinmeiInfo"/> class.</summary>
            public HinmeiInfo()
            {
                this.SuuryouList = new List<Suuryou>();
            }

            #endregion - Constructors -

            #region - Properties -

            /// <summary>業者CDを保持するプロパティ</summary>
            public string GyoushaCD { get; set; }

            /// <summary>業者名を保持するプロパティ</summary>
            public string GyoushaName { get; set; }

            /// <summary>現場CDを保持するプロパティ</summary>
            public string GenbaCD { get; set; }

            /// <summary>現場名を保持するプロパティ</summary>
            public string GenbaName { get; set; }

            /// <summary>数量を保持するプロパティ</summary>
            public List<Suuryou> SuuryouList { get; set; }

            /// <summary>備考を保持するプロパティ</summary>
            public string Bikou { get; set; }

            /// <summary>荷降Noを保持するプロパティ</summary>
            public string NioroshiNo { get; set; }

            #endregion - Properties -
        }

        /// <summary>数量を表すクラス・コントロール</summary>
        private class Suuryou
        {
            #region - Properties -

            /// <summary>表示インデックスを保持するプロパティ</summary>
            public int HinmeiIndex { get; set; }

            /// <summary>収集量(N)を保持するプロパティ</summary>
            public string SyuusyuuRyou { get; set; }

            /// <summary>収集量:換算値(N)を保持するプロパティ</summary>
            public string KansanSuuryou { get; set; }

            #endregion - Properties -
        }

        #endregion - Inner Class -
    }

    #endregion - Class -
}
