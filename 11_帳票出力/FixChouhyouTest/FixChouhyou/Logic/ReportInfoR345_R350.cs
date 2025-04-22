using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Dao;

namespace FixChouhyou
{
    #region - Class -

    /// <summary>(R345)配車依頼書を表すクラス・コントロール</summary>
    public class ReportInfoR345_R350 : ReportInfoBase
    {
        #region - Fields -

        /// <summary>Detail-1部に表示するレコート最大数を保持するフィールド</summary>
        private const int ConstMaxDispDetail1RowCount = 5;

        /// <summary>Detail-2部に表示するレコート最大数を保持するフィールド</summary>
        private const int ConstMaxDispDetail2RowCount = 5;

        /// <summary>S2Daoインターフェースを保持するフィールド</summary>
        private IS2Dao dao;

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR345_R350"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR345_R350(WINDOW_ID windowID, IS2Dao dao)
        {
            this.windowID = windowID;

            this.dao = dao;

            // 控え印刷
            this.HikaeType = HikaeTypeDef.Normal;

            // 伝票番号
            this.DenpyouNo = "0";

            // 受付種類
            this.UketsukeType = UketsukeTypeDef.Normal;
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>控え印刷に関する列挙型</summary>
        public enum HikaeTypeDef
        {
            /// <summary>通常/summary>
            Normal = -1,

            /// <summary>正のみ</summary>
            SeiNomi,

            /// <summary>正・控え２部</summary>
            SeiHikae2Bu,
        }

        /// <summary>受付タイプに関する列挙型</summary>
        public enum UketsukeTypeDef
        {
            /// <summary>通常/summary>
            Normal = 0,

            /// <summary>収集</summary>
            Syuusyuu,

            /// <summary>出荷</summary>
            Syutsuka,
        }

        #endregion - Enums -

        #region - Properties -

        /// <summary>控え印刷を保持するプロパティ</summary>
        public HikaeTypeDef HikaeType { get; private set; }

        /// <summary>伝票番号を保持するプロパティ</summary>
        public string DenpyouNo { get; private set; }

        /// <summary>受付種類を保持するプロパティ</summary>
        public UketsukeTypeDef UketsukeType { get; private set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>サンプルデータの作成処理を実行する</summary>
        public void CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;
            bool isPrint = true;

            #region - Header -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Header";

            // 運転者CD
            dataTableTmp.Columns.Add("UNTENSHA_CD");
            // 運転者名
            dataTableTmp.Columns.Add("UNTENSHA_NM");
            // 補助員CD
            dataTableTmp.Columns.Add("HOJOIN_CD");
            // 補助員名
            dataTableTmp.Columns.Add("HOJOIN_NM");
            // 車種CD
            dataTableTmp.Columns.Add("SHASHU_CD");
            // 車種名
            dataTableTmp.Columns.Add("SHASHU_NM");
            // 車輌CD
            dataTableTmp.Columns.Add("SHARYO_CD");
            // 車輌名
            dataTableTmp.Columns.Add("SHARYO_NM");
            // 作業日
            dataTableTmp.Columns.Add("SAGYOBI_DATE");
            // 荷降日/荷積日
            dataTableTmp.Columns.Add("NIOROSHI_DATE");
            // 現着時間
            dataTableTmp.Columns.Add("GENCHAKU_T");
            // 作業時間
            dataTableTmp.Columns.Add("SAGYO_T");
            // 受付番号
            dataTableTmp.Columns.Add("UKETSUKE_NO");
            // 営業担当者CD
            dataTableTmp.Columns.Add("EIGYOTANTO_CD");
            // 営業担当者名
            dataTableTmp.Columns.Add("EIGYOTANTO_NM");
            // 受付日
            dataTableTmp.Columns.Add("UKETSUKE_DATE");
            // 初回登録日
            dataTableTmp.Columns.Add("1ST_UKETSUKE");
            // 最終更新日
            dataTableTmp.Columns.Add("LAST_UKETSUKE");
            // 取引先CD
            dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
            // 取引先名
            dataTableTmp.Columns.Add("TORIHIKISAKI_NM");
            // 取引区分CD
            dataTableTmp.Columns.Add("TORIHIKI_KBN_CD");
            // 取引区分名
            dataTableTmp.Columns.Add("TORIHIKI_KBN_NM");
            // 業者CD
            dataTableTmp.Columns.Add("GYOSHA_CD");
            // 業者名
            dataTableTmp.Columns.Add("GYOSHA_NM");
            // 現場名CD
            dataTableTmp.Columns.Add("GENBA_CD");
            // 現場名
            dataTableTmp.Columns.Add("GENBA_NM");
            // 現場住所
            dataTableTmp.Columns.Add("GENBA_JUSHO");
            // 現場電話
            dataTableTmp.Columns.Add("GENBA_DENWA_NO");
            // 担当者
            dataTableTmp.Columns.Add("TANTOSHA_NM");
            // 担当携帯
            dataTableTmp.Columns.Add("TANTO_KEITAI_NO");
            // 荷降業者CD/荷積業者CD
            dataTableTmp.Columns.Add("NIOROSHIGYOSHA_CD");
            // 荷降業者名/荷積業者名
            dataTableTmp.Columns.Add("NIOROSHIGYOSHA_NM");
            // 荷降場CD/荷積CD
            dataTableTmp.Columns.Add("NIOROSHIJO_CD");
            // 荷降場名/荷積場名
            dataTableTmp.Columns.Add("NIOROSHIJO_NM");
            // マニ種類CD
            dataTableTmp.Columns.Add("MANI_TYPE_CD");
            // マニ種類
            dataTableTmp.Columns.Add("MANI_TYPE_NM");
            // マニ手配CD
            dataTableTmp.Columns.Add("MANI_TEHAI_CD");
            // マニ手配
            dataTableTmp.Columns.Add("MANI_TEHAI_NM");
            // コース組込CD
            dataTableTmp.Columns.Add("COURSE_KK_CD");
            // コース組込名称
            dataTableTmp.Columns.Add("COURSE_KK_NM");
            // コース名CD
            dataTableTmp.Columns.Add("COURSE_CD");
            // コース名
            dataTableTmp.Columns.Add("COURSE_NM");
            // 受付備考１
            dataTableTmp.Columns.Add("UKETSUKE_BK1");
            // 受付備考２
            dataTableTmp.Columns.Add("UKETSUKE_BK2");
            // 受付備考３
            dataTableTmp.Columns.Add("UKETSUKE_BK3");
            // 運転者備考１
            dataTableTmp.Columns.Add("UNTENSHA_BK1");
            // 運転者備考２
            dataTableTmp.Columns.Add("UNTENSHA_BK2");
            // 運転者備考３
            dataTableTmp.Columns.Add("UNTENSHA_BK3");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 運転者CD
                rowTmp["UNTENSHA_CD"] = "1234567890";
                // 運転者名
                rowTmp["UNTENSHA_NM"] = "あいうえおかきくけこさしすせそ";
                // 補助員CD
                rowTmp["HOJOIN_CD"] = "1234567890";
                // 補助員名
                rowTmp["HOJOIN_NM"] = "あいうえおかきくけこさしすせそ";
                // 車種CD
                rowTmp["SHASHU_CD"] = "1234567890";
                // 車種名
                rowTmp["SHASHU_NM"] = "あいうえおかきくけこさしすせそ";
                // 車輌CD
                rowTmp["SHARYO_CD"] = "1234567890";
                // 車輌名
                rowTmp["SHARYO_NM"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 作業日
                rowTmp["SAGYOBI_DATE"] = "2013/12/01";
                // 荷降日/荷積日
                rowTmp["NIOROSHI_DATE"] = "2013/12/01";
                // 現着時間
                rowTmp["GENCHAKU_T"] = "12時00分";
                // 作業時間
                rowTmp["SAGYO_T"] = "11時間00分";
                // 受付番号
                rowTmp["UKETSUKE_NO"] = "1234567890";
                // 営業担当者CD
                rowTmp["EIGYOTANTO_CD"] = "1234567890";
                // 営業担当者名
                rowTmp["EIGYOTANTO_NM"] = "あいうえおかきくけこさしすせそ";
                // 受付日
                rowTmp["UKETSUKE_DATE"] = "2013/12/01";
                // 初回登録日
                rowTmp["1ST_UKETSUKE"] = "2013/12/01";
                // 最終更新日
                rowTmp["LAST_UKETSUKE"] = "2013/12/01";
                // 取引先CD
                rowTmp["TORIHIKISAKI_CD"] = "1234567890";
                // 取引先名
                rowTmp["TORIHIKISAKI_NM"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 取引区分CD
                rowTmp["TORIHIKI_KBN_CD"] = "1234567890";
                // 取引区分名
                rowTmp["TORIHIKI_KBN_NM"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 業者CD
                rowTmp["GYOSHA_CD"] = "1234567890";
                // 業者名
                rowTmp["GYOSHA_NM"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 現場名CD
                rowTmp["GENBA_CD"] = "1234567890";
                // 現場名
                rowTmp["GENBA_NM"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 現場住所
                rowTmp["GENBA_JUSHO"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 現場電話
                rowTmp["GENBA_DENWA_NO"] = "12345678901234567890";
                // 担当者
                rowTmp["TANTOSHA_NM"] = "あいうえおかきくけこさしすせそ";
                // 担当携帯
                rowTmp["TANTO_KEITAI_NO"] = "12345678901234567890";
                // 荷降業者CD/荷積業者CD
                rowTmp["NIOROSHIGYOSHA_CD"] = "1234567890";
                // 荷降業者名/荷積業者名
                rowTmp["NIOROSHIGYOSHA_NM"] = "あいうえおかきくけこさしすせそ";
                // 荷降場CD/荷積CD
                rowTmp["NIOROSHIJO_CD"] = "1234567890";
                // 荷降場名/荷積場名
                rowTmp["NIOROSHIJO_NM"] = "あいうえおかきくけこさしすせそ";
                // マニ種類CD
                rowTmp["MANI_TYPE_CD"] = "1234567890";
                // マニ種類
                rowTmp["MANI_TYPE_NM"] = "あいうえお";
                // マニ手配CD
                rowTmp["MANI_TEHAI_CD"] = "1234567890";
                // マニ手配
                rowTmp["MANI_TEHAI_NM"] = "あいうえお";
                // コース組込CD
                rowTmp["COURSE_KK_CD"] = "1234567890";
                // コース組込名称
                rowTmp["COURSE_KK_NM"] = "あいうえお";
                // コース名CD
                rowTmp["COURSE_CD"] = "1234567890";
                // コース名
                rowTmp["COURSE_NM"] = "あいうえおかきくけこさしすせそ";
                // 受付備考１
                rowTmp["UKETSUKE_BK1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 受付備考２
                rowTmp["UKETSUKE_BK2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 受付備考３
                rowTmp["UKETSUKE_BK3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 運転者備考１
                rowTmp["UNTENSHA_BK1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 運転者備考２
                rowTmp["UNTENSHA_BK2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 運転者備考３
                rowTmp["UNTENSHA_BK3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail 1 -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail1";

            // コンテナ項番
            dataTableTmp.Columns.Add("KONTENA_KBN");
            // コンテナ種類CD
            dataTableTmp.Columns.Add("KONTENA_TYPE_CD");
            // コンテナ種類名称
            dataTableTmp.Columns.Add("KONTENA_TYPE_NM");
            // コンテナCD
            dataTableTmp.Columns.Add("KONTENA_CD");
            // コンテナ名称
            dataTableTmp.Columns.Add("KONTENA_NM");
            // コンテナ状況CD
            dataTableTmp.Columns.Add("KONTENA_JOKYO_CD");
            // コンテナ状況
            dataTableTmp.Columns.Add("KONTENA_JOKYO_NM");
            // 台数
            dataTableTmp.Columns.Add("DAISU");

            if (isPrint)
            {
                for (int i = 0; i < 20; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // コンテナ項番
                    rowTmp["KONTENA_KBN"] = "1234567890";
                    // コンテナ種類CD
                    rowTmp["KONTENA_TYPE_CD"] = "1234567890";
                    // コンテナ種類名称
                    rowTmp["KONTENA_TYPE_NM"] = "あいうえおかきくけこ";
                    // コンテナCD
                    rowTmp["KONTENA_CD"] = "1234567890";
                    // コンテナ名称
                    rowTmp["KONTENA_NM"] = "あいうえおかきくけこ";
                    // コンテナ状況CD
                    rowTmp["KONTENA_JOKYO_CD"] = "1234567890";
                    // コンテナ状況
                    rowTmp["KONTENA_JOKYO_NM"] = "あいうえおかきくけこ";
                    // 台数
                    rowTmp["DAISU"] = "1234567890";

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }

            this.DataTableList.Add("Detail1", dataTableTmp);

            #endregion - Detail 1 -

            #region - Detail 2 -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail2";

            // 品目項番
            dataTableTmp.Columns.Add("HINMOKU_KBN");
            // 品名CD
            dataTableTmp.Columns.Add("HINMOKU_CD");
            // 品名
            dataTableTmp.Columns.Add("HINMOKU_NM");
            // 予定数量
            dataTableTmp.Columns.Add("YOTEISU");
            // 実績数量
            dataTableTmp.Columns.Add("JISSEKISU");
            // 単位
            dataTableTmp.Columns.Add("TANI");
            // 単価
            dataTableTmp.Columns.Add("TANKA");
            // 金額
            dataTableTmp.Columns.Add("KINGAKU");
            // 明細備考
            dataTableTmp.Columns.Add("MEISAI_BKO");

            if (isPrint)
            {
                for (int i = 0; i < 20; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // 品目項番
                    rowTmp["HINMOKU_KBN"] = "1234567890";
                    // 品名CD
                    rowTmp["HINMOKU_CD"] = "1234567890";
                    // 品名
                    rowTmp["HINMOKU_NM"] = "あいうえおかきくけこさしすせそ";
                    // 予定数量
                    rowTmp["YOTEISU"] = "1,234,567,890";
                    // 実績数量
                    rowTmp["JISSEKISU"] = "1,234,567,890";
                    // 単位
                    rowTmp["TANI"] = "あいうえお";
                    // 単価
                    rowTmp["TANKA"] = "12,345,678";
                    // 金額
                    rowTmp["KINGAKU"] = 1.234567890;
                    // 明細備考
                    rowTmp["MEISAI_BKO"] = "あいうえおかきくけこさしすせそ";

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }

            this.DataTableList.Add("Detail2", dataTableTmp);

            #endregion - Detail 2 -

            #region - Footer -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";

            // 金額計
            dataTableTmp.Columns.Add("KINGAKU_KEI");
            // 消費税
            dataTableTmp.Columns.Add("SHOHIZEI");
            // 合計金額
            dataTableTmp.Columns.Add("GOKEI");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 金額計
                rowTmp["KINGAKU_KEI"] = "1,234,567,890";
                // 消費税
                rowTmp["SHOHIZEI"] = "1,234,567,890";
                // 合計金額
                rowTmp["GOKEI"] = "1,234,567,890";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Footer", dataTableTmp);

            #endregion - Footer -
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int i;
            int index;
            int rowNo = 1;
            DataRow row;

            foreach (string key in this.ParameterList.Keys)
            {
                switch (key)
                {
                    case "HikaeType":       // 控え印刷

                        // 控え印刷
                        this.HikaeType = (HikaeTypeDef)int.Parse((string)this.ParameterList[key]);

                        break;
                    case "DenpyouNumber":   // 伝票番号

                        // 伝票番号
                        this.DenpyouNo = (string)this.ParameterList[key];

                        break;
                    case "UketukeType":     // 受付種類

                        // 受付種類
                        this.UketsukeType = (UketsukeTypeDef)int.Parse((string)this.ParameterList[key]);

                        break;
                    default:
                        return;
                }
            }

            // データーベースからデータを取得
            this.GetDatabaseData();

            DataTable dataTableHeaderTmp = this.DataTableList["Header"];
            DataTable dataTableDetail1Tmp = this.DataTableList["Detail1"];
            DataTable dataTableDetail2Tmp = this.DataTableList["Detail2"];
            DataTable dataTableFooterTmp = this.DataTableList["Footer"];
            string ctrlName = string.Empty;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            int roopMaxCount = this.HikaeType == HikaeTypeDef.SeiHikae2Bu ? 2 : 1;

            for (int roopCount = 1; roopCount <= roopMaxCount; roopCount++)
            {
                // 帳票出力用データの設定処理
                this.SetChouhyouInfo();

                #region - Columns -

                if (roopCount == 1)
                {
                    // タイトル
                    ctrlName = string.Format("PHN_TITLE_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降日・荷積日
                    ctrlName = string.Format("PHN_NIOROSHI_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降業者・荷積業者
                    ctrlName = string.Format("PHN_NIOROSHIGYOSHA_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降場・荷積場
                    ctrlName = string.Format("PHN_NIOROSHIJO_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 作業日
                    ctrlName = string.Format("DH_SAGYOBI_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降日/荷積日
                    ctrlName = string.Format("PHN_NIOROSHI_DATE_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 現着時間
                    ctrlName = string.Format("DH_GENCHAKU_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 作業時間
                    ctrlName = string.Format("DH_SAGYO_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 受付番号
                    ctrlName = string.Format("DH_UKETSUKE_NO_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 営業担当者CD
                    ctrlName = string.Format("PHN_EIGYOTANTO_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 営業担当者名
                    ctrlName = string.Format("DH_EIGYOTANTO_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 受付日
                    ctrlName = string.Format("DH_UKETSUKE_DATE_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 初回登録日
                    ctrlName = string.Format("DH_1ST_UKETSUKE_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 最終更新日
                    ctrlName = string.Format("DH_LAST_UKETSUKE_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 取引先CD
                    ctrlName = string.Format("PHN_TORIHIKISAKI_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 取引先名
                    ctrlName = string.Format("DH_TORIHIKISAKI_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 取引区分CD
                    ctrlName = string.Format("PHN_TORIHIKI_KBN_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 取引区分名
                    ctrlName = string.Format("DH_TORIHIKI_KBN_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 業者CD
                    ctrlName = string.Format("PHN_GYOSHA_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 業者名
                    ctrlName = string.Format("DH_GYOSHA_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 現場名CD
                    ctrlName = string.Format("PHN_GENBA_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 現場名
                    ctrlName = string.Format("DH_GENBA_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 現場住所
                    ctrlName = string.Format("DH_GENNBA_JUSHO_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 現場電話
                    ctrlName = string.Format("DH_GENBA_DENWA_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 担当者
                    ctrlName = string.Format("DH_TANTOSHA_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 担当携帯
                    ctrlName = string.Format("DH_TANTO_KEITAI_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降業者CD/荷積業者CD
                    ctrlName = string.Format("PHN_NIOROSHIGYOSHA_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降業者名/荷積業者名
                    ctrlName = string.Format("PHN_NIOROSHIGYOSHA_NM_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降場CD/荷積CD
                    ctrlName = string.Format("PHN_NIOROSHIJO_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 荷降場名/荷積場名
                    ctrlName = string.Format("PHN_NIOROSHIJO_NM_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // マニ種類CD
                    ctrlName = string.Format("PHN_MANI_TYPE_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // マニ種類
                    ctrlName = string.Format("DH_MANI_TYPE_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // マニ手配CD
                    ctrlName = string.Format("PHN_MANI_TEHAI_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // マニ手配
                    ctrlName = string.Format("DH_MANI_TEHAI_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // コース組込CD
                    ctrlName = string.Format("PHN_COURSE_KK_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // コース組込名称
                    ctrlName = string.Format("DH_COURSE_KK_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // コース名CD
                    ctrlName = string.Format("PHN_COURSE_CD_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // コース名
                    ctrlName = string.Format("DH_COURSE_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 受付備考
                    ctrlName = string.Format("PHN_UKETSUKE_BK1_FLB");
                    this.dataTable.Columns.Add(ctrlName);
                    ctrlName = string.Format("PHN_UKETSUKE_BK2_FLB");
                    this.dataTable.Columns.Add(ctrlName);
                    ctrlName = string.Format("PHN_UKETSUKE_BK3_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    // 運転者備考
                    ctrlName = string.Format("PHN_UNTENSHA_BK1_FLB");
                    this.dataTable.Columns.Add(ctrlName);
                    ctrlName = string.Format("PHN_UNTENSHA_BK2_FLB");
                    this.dataTable.Columns.Add(ctrlName);
                    ctrlName = string.Format("PHN_UNTENSHA_BK3_FLB");
                    this.dataTable.Columns.Add(ctrlName);

                    for (i = 1; i <= ConstMaxDispDetail1RowCount; i++)
                    {
                        // コンテナ項番
                        ctrlName = string.Format("PHN_KONTENA_KBN_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // コンテナ種類CD
                        ctrlName = string.Format("PHN_KONTENA_TYPE_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // コンテナ種類名称
                        ctrlName = string.Format("PHN_KONTENA_TYPE_NM_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // コンテナCD
                        ctrlName = string.Format("PHN_KONTENA_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // コンテナ名称
                        ctrlName = string.Format("PHN_KONTENA_NM_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // コンテナ状況CD
                        ctrlName = string.Format("PHN_KONTENA_JOKYO_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // コンテナ状況
                        ctrlName = string.Format("PHN_KONTENA_JOKYO_NM_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 台数
                        ctrlName = string.Format("PHN_DAISU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    for (i = 1; i <= ConstMaxDispDetail2RowCount; i++)
                    {
                        // 品目項番
                        ctrlName = string.Format("PHN_HINMOKU_KBN_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名CD
                        ctrlName = string.Format("PHN_HINMOKU_CD_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 品名
                        ctrlName = string.Format("PHN_HINMOKU_NM_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 予定数量
                        ctrlName = string.Format("PHN_YOTEISU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 実績数量
                        ctrlName = string.Format("PHN_JISSEKISU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 単位
                        ctrlName = string.Format("PHN_TANI_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 単価
                        ctrlName = string.Format("PHN_TANKA_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 金額
                        ctrlName = string.Format("PHN_KINGAKU_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                        // 明細備考
                        ctrlName = string.Format("PHN_MEISAI_BKO_{0}_FLB", i);
                        this.dataTable.Columns.Add(ctrlName);
                    }

                    // 金額計
                    ctrlName = "DF_KINGAKU_FLB";
                    this.dataTable.Columns.Add(ctrlName);
                    // 消費税
                    ctrlName = "DF_SHOHIZEI_FLB";
                    this.dataTable.Columns.Add(ctrlName);
                    // 合計金額
                    ctrlName = "DF_GOKEI_FLB";
                    this.dataTable.Columns.Add(ctrlName);

                    // ページ番号
                    this.dataTable.Columns.Add("PHN_PAGE_NO_FLB");
                }

                #endregion - Columns -

                int maxPage;
                bool detail1Comp = false;
                bool detail2Comp = false;
                int detail1MaxCount = dataTableDetail1Tmp.Rows.Count;
                int detail2MaxCount = dataTableDetail2Tmp.Rows.Count;
                if (detail1MaxCount > detail2MaxCount)
                {
                    maxPage = (int)Math.Ceiling((double)detail1MaxCount / ConstMaxDispDetail1RowCount);
                }
                else
                {
                    maxPage = (int)Math.Ceiling((double)detail2MaxCount / ConstMaxDispDetail2RowCount);
                }

                if (maxPage == 0)
                {
                    maxPage = 1;
                }

                int detail1Start = 0;
                int detail2Start = 0;
                for (int pageNo = 1; pageNo <= maxPage; pageNo++)
                {
                    #region - Header -

                    row = this.dataTable.NewRow();
                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        if (pageNo == 1)
                        {
                            // 作業日
                            index = dataTableHeaderTmp.Columns.IndexOf("SAGYOBI_DATE");
                            ctrlName = string.Format("DH_SAGYOBI_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降日/荷積日
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_DATE");
                            ctrlName = string.Format("PHN_NIOROSHI_DATE_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }
                            // 現着時間
                            index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_T");
                            ctrlName = string.Format("DH_GENCHAKU_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 作業時間
                            index = dataTableHeaderTmp.Columns.IndexOf("SAGYO_T");
                            ctrlName = string.Format("DH_SAGYO_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付番号
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_NO");
                            ctrlName = string.Format("DH_UKETSUKE_NO_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 営業担当者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("EIGYOTANTO_CD");
                            ctrlName = string.Format("PHN_EIGYOTANTO_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 営業担当者名
                            index = dataTableHeaderTmp.Columns.IndexOf("EIGYOTANTO_NM");
                            ctrlName = string.Format("DH_EIGYOTANTO_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 16)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 16);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付日
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_DATE");
                            ctrlName = string.Format("DH_UKETSUKE_DATE_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 初回登録日
                            index = dataTableHeaderTmp.Columns.IndexOf("1ST_UKETSUKE");
                            ctrlName = string.Format("DH_1ST_UKETSUKE_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 最終更新日
                            index = dataTableHeaderTmp.Columns.IndexOf("LAST_UKETSUKE");
                            ctrlName = string.Format("DH_LAST_UKETSUKE_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 取引先CD
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = string.Format("PHN_TORIHIKISAKI_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NM");
                            ctrlName = string.Format("DH_TORIHIKISAKI_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 取引区分CD
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKI_KBN_CD");
                            ctrlName = string.Format("PHN_TORIHIKI_KBN_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 取引区分名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKI_KBN_NM");
                            ctrlName = string.Format("DH_TORIHIKI_KBN_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 10);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOSHA_CD");
                            ctrlName = string.Format("PHN_GYOSHA_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOSHA_NM");
                            ctrlName = string.Format("DH_GYOSHA_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場名CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("PHN_GENBA_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NM");
                            ctrlName = string.Format("DH_GENBA_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場住所
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_JUSHO");
                            ctrlName = string.Format("DH_GENNBA_JUSHO_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 80)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 80);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場電話
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_DENWA_NO");
                            ctrlName = string.Format("DH_GENBA_DENWA_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 担当者
                            index = dataTableHeaderTmp.Columns.IndexOf("TANTOSHA_NM");
                            ctrlName = string.Format("DH_TANTOSHA_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 16)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 16);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 担当携帯
                            index = dataTableHeaderTmp.Columns.IndexOf("TANTO_KEITAI_NO");
                            ctrlName = string.Format("DH_TANTO_KEITAI_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 13)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 13);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降業者CD/荷積業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIGYOSHA_CD");
                            ctrlName = string.Format("PHN_NIOROSHIGYOSHA_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降業者名/荷積業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIGYOSHA_NM");
                            ctrlName = string.Format("PHN_NIOROSHIGYOSHA_NM_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降場CD/荷積CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIJO_CD");
                            ctrlName = string.Format("PHN_NIOROSHIJO_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降場名/荷積場名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIJO_NM");
                            ctrlName = string.Format("PHN_NIOROSHIJO_NM_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 20)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // マニ種類CD
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TYPE_CD");
                            ctrlName = string.Format("PHN_MANI_TYPE_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // マニ種類
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TYPE_NM");
                            ctrlName = string.Format("DH_MANI_TYPE_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 4)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 4);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // マニ手配CD
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TEHAI_CD");
                            ctrlName = string.Format("PHN_MANI_TEHAI_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // マニ手配
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TEHAI_NM");
                            ctrlName = string.Format("DH_MANI_TEHAI_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 8)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 8);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コース組込CD
                            index = dataTableHeaderTmp.Columns.IndexOf("COURSE_KK_CD");
                            ctrlName = string.Format("PHN_COURSE_KK_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コース組込名称
                            //index = dataTableHeaderTmp.Columns.IndexOf("COURSE_KK_NM");
                            ctrlName = string.Format("DH_COURSE_KK_FLB");
                            //btBytes = hEncoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                            //if (btBytes.Length > 4)
                            //{
                            //    row[ctrlName] = hEncoding.GetString(btBytes, 0, 4);
                            //}
                            //else
                            //{
                            //    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];

                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                if (dataTableHeaderTmp.Rows[0].ItemArray[index].ToString() == "1")
                                {
                                    row[ctrlName] = "臨時";
                                }
                                else if (dataTableHeaderTmp.Rows[0].ItemArray[index].ToString() == "2")
                                {
                                    row[ctrlName] = "組込";
                                }
                                else
                                {
                                    row[ctrlName] = string.Empty;
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コース名CD
                            index = dataTableHeaderTmp.Columns.IndexOf("COURSE_CD");
                            ctrlName = string.Format("PHN_COURSE_CD_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コース名
                            index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NM");
                            ctrlName = string.Format("DH_COURSE_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 10);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付備考1
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK1");
                            ctrlName = string.Format("PHN_UKETSUKE_BK1_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付備考2
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK2");
                            ctrlName = string.Format("PHN_UKETSUKE_BK2_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付備考3
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK3");
                            ctrlName = string.Format("PHN_UKETSUKE_BK3_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 運転者備考1
                            index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK1");
                            ctrlName = string.Format("PHN_UNTENSHA_BK1_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 運転者備考2
                            index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK2");
                            ctrlName = string.Format("PHN_UNTENSHA_BK2_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 運転者備考3
                            index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK3");
                            ctrlName = string.Format("PHN_UNTENSHA_BK3_FLB");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }
                        }
                        else
                        {
                            // 作業日
                            index = dataTableHeaderTmp.Columns.IndexOf("SAGYOBI_DATE");
                            ctrlName = string.Format("DH_SAGYOBI_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // 荷降日/荷積日
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_DATE");
                            ctrlName = string.Format("PHN_NIOROSHI_DATE_FLB");
                            row[ctrlName] = "****/**/**";
                            // 現着時間
                            index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_T");
                            ctrlName = string.Format("DH_GENCHAKU_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // 作業時間
                            index = dataTableHeaderTmp.Columns.IndexOf("SAGYO_T");
                            ctrlName = string.Format("DH_SAGYO_FLB");
                            row[ctrlName] = "**時**分";
                            // 受付番号
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_NO");
                            ctrlName = string.Format("DH_UKETSUKE_NO_FLB");
                            row[ctrlName] = "**********";
                            // 営業担当者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("EIGYOTANTO_CD");
                            ctrlName = string.Format("PHN_EIGYOTANTO_CD_FLB");
                            row[ctrlName] = "******";
                            // 営業担当者名
                            index = dataTableHeaderTmp.Columns.IndexOf("EIGYOTANTO_NM");
                            ctrlName = string.Format("DH_EIGYOTANTO_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊";
                            // 受付日
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_DATE");
                            ctrlName = string.Format("DH_UKETSUKE_DATE_FLB");
                            row[ctrlName] = "****/**/**";
                            // 初回登録日
                            index = dataTableHeaderTmp.Columns.IndexOf("1ST_UKETSUKE");
                            ctrlName = string.Format("DH_1ST_UKETSUKE_FLB");
                            row[ctrlName] = "****/**/**";
                            // 最終更新日
                            index = dataTableHeaderTmp.Columns.IndexOf("LAST_UKETSUKE");
                            ctrlName = string.Format("DH_LAST_UKETSUKE_FLB");
                            row[ctrlName] = "****/**/**";
                            // 取引先CD
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                            ctrlName = string.Format("PHN_TORIHIKISAKI_CD_FLB");
                            row[ctrlName] = "******";
                            // 取引先名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NM");
                            ctrlName = string.Format("DH_TORIHIKISAKI_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // 取引区分CD
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKI_KBN_CD");
                            ctrlName = string.Format("PHN_TORIHIKI_KBN_CD_FLB");
                            row[ctrlName] = "**";
                            // 取引区分名
                            index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKI_KBN_NM");
                            ctrlName = string.Format("DH_TORIHIKI_KBN_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // 業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOSHA_CD");
                            ctrlName = string.Format("PHN_GYOSHA_CD_FLB");
                            row[ctrlName] = "******";
                            // 業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOSHA_NM");
                            ctrlName = string.Format("DH_GYOSHA_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // 現場名CD
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = string.Format("PHN_GENBA_CD_FLB");
                            row[ctrlName] = "******";
                            // 現場名
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NM");
                            ctrlName = string.Format("DH_GENBA_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                            // 現場住所
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_JUSHO");
                            ctrlName = string.Format("DH_GENNBA_JUSHO_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                            // 現場電話
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_DENWA_NO");
                            ctrlName = string.Format("DH_GENBA_DENWA_FLB");
                            row[ctrlName] = "***-****-****";
                            // 担当者
                            index = dataTableHeaderTmp.Columns.IndexOf("TANTOSHA_NM");
                            ctrlName = string.Format("DH_TANTOSHA_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊";
                            // 担当携帯
                            index = dataTableHeaderTmp.Columns.IndexOf("TANTO_KEITAI_NO");
                            ctrlName = string.Format("DH_TANTO_KEITAI_FLB");
                            row[ctrlName] = "***-****-****";
                            // 荷降業者CD/荷積業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIGYOSHA_CD");
                            ctrlName = string.Format("PHN_NIOROSHIGYOSHA_CD_FLB");
                            row[ctrlName] = "******";
                            // 荷降業者名/荷積業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIGYOSHA_NM");
                            ctrlName = string.Format("PHN_NIOROSHIGYOSHA_NM_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // 荷降場CD/荷積CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIJO_CD");
                            ctrlName = string.Format("PHN_NIOROSHIJO_CD_FLB");
                            row[ctrlName] = "******";
                            // 荷降場名/荷積場名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIJO_NM");
                            ctrlName = string.Format("PHN_NIOROSHIJO_NM_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // マニ種類CD
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TYPE_CD");
                            ctrlName = string.Format("PHN_MANI_TYPE_CD_FLB");
                            row[ctrlName] = "**";
                            // マニ種類
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TYPE_NM");
                            ctrlName = string.Format("DH_MANI_TYPE_FLB");
                            row[ctrlName] = "＊＊";
                            // マニ手配CD
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TEHAI_CD");
                            ctrlName = string.Format("PHN_MANI_TEHAI_CD_FLB");
                            row[ctrlName] = "**";
                            // マニ手配
                            index = dataTableHeaderTmp.Columns.IndexOf("MANI_TEHAI_NM");
                            ctrlName = string.Format("DH_MANI_TEHAI_FLB");
                            row[ctrlName] = "＊＊";
                            // コース組込CD
                            index = dataTableHeaderTmp.Columns.IndexOf("COURSE_KK_CD");
                            ctrlName = string.Format("PHN_COURSE_KK_CD_FLB");
                            row[ctrlName] = "**";
                            // コース組込名称
                            index = dataTableHeaderTmp.Columns.IndexOf("COURSE_KK_NM");
                            ctrlName = string.Format("DH_COURSE_KK_FLB");
                            row[ctrlName] = "＊＊";
                            // コース名CD
                            index = dataTableHeaderTmp.Columns.IndexOf("COURSE_CD");
                            ctrlName = string.Format("PHN_COURSE_CD_FLB");
                            row[ctrlName] = "******";
                            // コース名
                            index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NM");
                            ctrlName = string.Format("DH_COURSE_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊";
                            // 受付備考
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK1");
                            ctrlName = string.Format("PHN_UKETSUKE_BK1_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK2");
                            ctrlName = string.Format("PHN_UKETSUKE_BK2_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK3");
                            ctrlName = string.Format("PHN_UKETSUKE_BK3_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                            // 運転者備考  
                            index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK1");
                            ctrlName = string.Format("PHN_UNTENSHA_BK1_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                            index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK2");
                            ctrlName = string.Format("PHN_UNTENSHA_BK2_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                            index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK3");
                            ctrlName = string.Format("PHN_UNTENSHA_BK3_FLB");
                            row[ctrlName] = "＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊";
                        }
                    }
                    else
                    {
                        // 作業日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOBI_DATE");
                        ctrlName = string.Format("DH_SAGYOBI_FLB");
                        row[ctrlName] = string.Empty;
                        // 荷降日/荷積日
                        index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_DATE");
                        ctrlName = string.Format("PHN_NIOROSHI_DATE_FLB");
                        row[ctrlName] = string.Empty;
                        // 現着時間
                        index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_T");
                        ctrlName = string.Format("DH_GENCHAKU_FLB");
                        row[ctrlName] = string.Empty;
                        // 作業時間
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYO_T");
                        ctrlName = string.Format("DH_SAGYO_FLB");
                        row[ctrlName] = string.Empty;
                        // 受付番号
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_NO");
                        ctrlName = string.Format("DH_UKETSUKE_NO_FLB");
                        row[ctrlName] = string.Empty;
                        // 営業担当者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYOTANTO_CD");
                        ctrlName = string.Format("PHN_EIGYOTANTO_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYOTANTO_NM");
                        ctrlName = string.Format("DH_EIGYOTANTO_FLB");
                        row[ctrlName] = string.Empty;
                        // 受付日
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_DATE");
                        ctrlName = string.Format("DH_UKETSUKE_DATE_FLB");
                        row[ctrlName] = string.Empty;
                        // 初回登録日
                        index = dataTableHeaderTmp.Columns.IndexOf("1ST_UKETSUKE");
                        ctrlName = string.Format("DH_1ST_UKETSUKE_FLB");
                        row[ctrlName] = string.Empty;
                        // 最終更新日
                        index = dataTableHeaderTmp.Columns.IndexOf("LAST_UKETSUKE");
                        ctrlName = string.Format("DH_LAST_UKETSUKE_FLB");
                        row[ctrlName] = string.Empty;
                        // 取引先CD
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                        ctrlName = string.Format("PHN_TORIHIKISAKI_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // 取引先名
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NM");
                        ctrlName = string.Format("DH_TORIHIKISAKI_FLB");
                        row[ctrlName] = string.Empty;
                        // 取引区分CD
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKI_KBN_CD");
                        ctrlName = string.Format("PHN_TORIHIKI_KBN_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // 取引区分名
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKI_KBN_NM");
                        ctrlName = string.Format("DH_TORIHIKI_KBN_FLB");
                        row[ctrlName] = string.Empty;
                        // 業者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOSHA_CD");
                        ctrlName = string.Format("PHN_GYOSHA_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // 業者名
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOSHA_NM");
                        ctrlName = string.Format("DH_GYOSHA_FLB");
                        row[ctrlName] = string.Empty;
                        // 現場名CD
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                        ctrlName = string.Format("PHN_GENBA_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // 現場名
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NM");
                        ctrlName = string.Format("DH_GENBA_FLB");
                        row[ctrlName] = string.Empty;
                        // 現場住所
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_JUSHO");
                        ctrlName = string.Format("DH_GENNBA_JUSHO_FLB");
                        row[ctrlName] = string.Empty;
                        // 現場電話
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_DENWA_NO");
                        ctrlName = string.Format("DH_GENBA_DENWA_FLB");
                        row[ctrlName] = string.Empty;
                        // 担当者
                        index = dataTableHeaderTmp.Columns.IndexOf("TANTOSHA_NM");
                        ctrlName = string.Format("DH_TANTOSHA_FLB");
                        row[ctrlName] = string.Empty;
                        // 担当携帯
                        index = dataTableHeaderTmp.Columns.IndexOf("TANTO_KEITAI_NO");
                        ctrlName = string.Format("DH_TANTO_KEITAI_FLB");
                        row[ctrlName] = string.Empty;
                        // 荷降業者CD/荷積業者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIGYOSHA_CD");
                        ctrlName = string.Format("PHN_NIOROSHIGYOSHA_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // 荷降業者名/荷積業者名
                        index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIGYOSHA_NM");
                        ctrlName = string.Format("PHN_NIOROSHIGYOSHA_NM_FLB");
                        row[ctrlName] = string.Empty;
                        // 荷降場CD/荷積CD
                        index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIJO_CD");
                        ctrlName = string.Format("PHN_NIOROSHIJO_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // 荷降場名/荷積場名
                        index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHIJO_NM");
                        ctrlName = string.Format("PHN_NIOROSHIJO_NM_FLB");
                        row[ctrlName] = string.Empty;
                        // マニ種類CD
                        index = dataTableHeaderTmp.Columns.IndexOf("MANI_TYPE_CD");
                        ctrlName = string.Format("PHN_MANI_TYPE_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // マニ種類
                        index = dataTableHeaderTmp.Columns.IndexOf("MANI_TYPE_NM");
                        ctrlName = string.Format("DH_MANI_TYPE_FLB");
                        row[ctrlName] = string.Empty;
                        // マニ手配CD
                        index = dataTableHeaderTmp.Columns.IndexOf("MANI_TEHAI_CD");
                        ctrlName = string.Format("PHN_MANI_TEHAI_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // マニ手配
                        index = dataTableHeaderTmp.Columns.IndexOf("MANI_TEHAI_NM");
                        ctrlName = string.Format("DH_MANI_TEHAI_FLB");
                        row[ctrlName] = string.Empty;
                        // コース組込CD
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_KK_CD");
                        ctrlName = string.Format("PHN_COURSE_KK_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // コース組込名称
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_KK_NM");
                        ctrlName = string.Format("DH_COURSE_KK_FLB");
                        row[ctrlName] = string.Empty;
                        // コース名CD
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_CD");
                        ctrlName = string.Format("PHN_COURSE_CD_FLB");
                        row[ctrlName] = string.Empty;
                        // コース名
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NM");
                        ctrlName = string.Format("DH_COURSE_FLB");
                        row[ctrlName] = string.Empty;
                        // 受付備考
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK1");
                        ctrlName = string.Format("PHN_UKETSUKE_BK1_FLB");
                        row[ctrlName] = string.Empty;
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK2");
                        ctrlName = string.Format("PHN_UKETSUKE_BK2_FLB");
                        row[ctrlName] = string.Empty;
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BK3");
                        ctrlName = string.Format("PHN_UKETSUKE_BK3_FLB");
                        row[ctrlName] = string.Empty;
                        // 運転者備考  
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK1");
                        ctrlName = string.Format("PHN_UNTENSHA_BK1_FLB");
                        row[ctrlName] = string.Empty;
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK2");
                        ctrlName = string.Format("PHN_UNTENSHA_BK2_FLB");
                        row[ctrlName] = string.Empty;
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_BK3");
                        ctrlName = string.Format("PHN_UNTENSHA_BK3_FLB");
                        row[ctrlName] = string.Empty;
                    }

                    #endregion - Header -

                    #region - Detail 1 -

                    // タイトル
                    if (roopCount == 1)
                    {   // 正
                        row["PHN_TITLE_FLB"] = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.windowID);
                    }
                    else
                    {   // 控え
                        row["PHN_TITLE_FLB"] = r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.windowID) + "（控）";
                    }

                    if (this.UketsukeType == UketsukeTypeDef.Syuusyuu)
                    {
                        // 荷降日・荷積日
                        row["PHN_NIOROSHI_FLB"] = " 荷 降 日";
                        // 荷降業者・荷積業者
                        row["PHN_NIOROSHIGYOSHA_FLB"] = " 荷降業者";
                        // 荷降場・荷積場
                        row["PHN_NIOROSHIJO_FLB"] = " 荷 降 場";
                    }
                    else
                    {
                        // 荷降日・荷積日
                        row["PHN_NIOROSHI_FLB"] = " 荷 積 日";
                        // 荷降業者・荷積業者
                        row["PHN_NIOROSHIGYOSHA_FLB"] = " 荷積業者";
                        // 荷降場・荷積場
                        row["PHN_NIOROSHIJO_FLB"] = " 荷 積 場";
                    }

                    if (dataTableDetail1Tmp.Rows.Count == 0)
                    {
                        detail1Comp = true;
                    }

                    if (detail1Comp == false)
                    {   // Detail1はまだ未完了

                        rowNo = 1;
                        
                        for (i = detail1Start; i < dataTableDetail1Tmp.Rows.Count; i++)
                        {
                            // コンテナ項番
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_KBN");
                            ctrlName = string.Format("PHN_KONTENA_KBN_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コンテナ種類CD
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_TYPE_CD");
                            ctrlName = string.Format("PHN_KONTENA_TYPE_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コンテナ種類名称
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_TYPE_NM");
                            ctrlName = string.Format("PHN_KONTENA_TYPE_NM_{0}_FLB", rowNo);
                            byteArray = encoding.GetBytes(dataTableDetail1Tmp.Rows[i].ItemArray[index].ToString());
                            if (byteArray.Length > 20)
                            {
                                row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                            }
                            else
                            {
                                row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                            }

                            // コンテナCD
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_CD");
                            ctrlName = string.Format("PHN_KONTENA_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コンテナ名称
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_NM");
                            ctrlName = string.Format("PHN_KONTENA_NM_{0}_FLB", rowNo);
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

                            // コンテナ状況CD
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_JOKYO_CD");
                            ctrlName = string.Format("PHN_KONTENA_JOKYO_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail1Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail1Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // コンテナ状況
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_JOKYO_NM");
                            ctrlName = string.Format("PHN_KONTENA_JOKYO_NM_{0}_FLB", rowNo);
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

                            // 台数
                            index = dataTableDetail1Tmp.Columns.IndexOf("DAISU");
                            ctrlName = string.Format("PHN_DAISU_{0}_FLB", rowNo);
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
                                detail1Start = i + 1;
                                break;
                            }
                            else
                            {
                                rowNo++;
                            }
                        }

                        if (i == detail1MaxCount)
                        {
                            detail1Comp = true;
                        }
                    }
                    else
                    {   // Detail1は既に完了
                        rowNo = 1;
                        for (i = 0; i < ConstMaxDispDetail1RowCount; i++)
                        {
                            // コンテナ項番
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_KBN");
                            ctrlName = string.Format("PHN_KONTENA_KBN_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // コンテナ種類CD
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_TYPE_CD");
                            ctrlName = string.Format("PHN_KONTENA_TYPE_CD_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // コンテナ種類名称
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_TYPE_NM");
                            ctrlName = string.Format("PHN_KONTENA_TYPE_NM_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // コンテナCD
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_CD");
                            ctrlName = string.Format("PHN_KONTENA_CD_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // コンテナ名称
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_NM");
                            ctrlName = string.Format("PHN_KONTENA_NM_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // コンテナ状況CD
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_JOKYO_CD");
                            ctrlName = string.Format("PHN_KONTENA_JOKYO_CD_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // コンテナ状況
                            index = dataTableDetail1Tmp.Columns.IndexOf("KONTENA_JOKYO_NM");
                            ctrlName = string.Format("PHN_KONTENA_JOKYO_NM_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 台数
                            index = dataTableDetail1Tmp.Columns.IndexOf("DAISU");
                            ctrlName = string.Format("PHN_DAISU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;

                            if (rowNo == ConstMaxDispDetail1RowCount)
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

                    #endregion - Detail 1 -

                    #region - Detail 2 -

                    if (dataTableDetail2Tmp.Rows.Count == 0)
                    {
                        detail2Comp = false;
                    }

                    if (detail2Comp == false)
                    {   // Detail2はまだ未完了

                        rowNo = 1;
                        
                        for (i = detail2Start; i < dataTableDetail2Tmp.Rows.Count; i++)
                        {
                            // 品目項番
                            index = dataTableDetail2Tmp.Columns.IndexOf("HINMOKU_KBN");
                            ctrlName = string.Format("PHN_HINMOKU_KBN_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 品名CD
                            index = dataTableDetail2Tmp.Columns.IndexOf("HINMOKU_CD");
                            ctrlName = string.Format("PHN_HINMOKU_CD_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 品名
                            index = dataTableDetail2Tmp.Columns.IndexOf("HINMOKU_NM");
                            ctrlName = string.Format("PHN_HINMOKU_NM_{0}_FLB", rowNo);
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

                            // 予定数量
                            index = dataTableDetail2Tmp.Columns.IndexOf("YOTEISU");
                            ctrlName = string.Format("PHN_YOTEISU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 実績数量
                            index = dataTableDetail2Tmp.Columns.IndexOf("JISSEKISU");
                            ctrlName = string.Format("PHN_JISSEKISU_{0}_FLB", rowNo);
                            // row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                            row[ctrlName] = string.Empty;

                            // 単位
                            index = dataTableDetail2Tmp.Columns.IndexOf("TANI");
                            ctrlName = string.Format("PHN_TANI_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetail2Tmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 4)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 4);
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

                            // 単価
                            index = dataTableDetail2Tmp.Columns.IndexOf("TANKA");
                            ctrlName = string.Format("PHN_TANKA_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 金額
                            index = dataTableDetail2Tmp.Columns.IndexOf("KINGAKU");
                            ctrlName = string.Format("PHN_KINGAKU_{0}_FLB", rowNo);
                            if (!this.IsDBNull(dataTableDetail2Tmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetail2Tmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 明細備考
                            index = dataTableDetail2Tmp.Columns.IndexOf("MEISAI_BKO");
                            ctrlName = string.Format("PHN_MEISAI_BKO_{0}_FLB", rowNo);
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

                            if (rowNo == ConstMaxDispDetail2RowCount)
                            {
                                detail2Start = i + 1;
                                break;
                            }
                            else
                            {
                                rowNo++;
                            }
                        }

                        if (i == detail2MaxCount)
                        {
                            detail2Comp = true;
                        }
                    }
                    else
                    {   // Detail2は既に完了
                        
                        rowNo = 1;

                        for (i = 0; i < ConstMaxDispDetail2RowCount; i++)
                        {
                            // 品目項番
                            index = dataTableDetail2Tmp.Columns.IndexOf("HINMOKU_KBN");
                            ctrlName = string.Format("PHN_HINMOKU_KBN_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 品名CD
                            index = dataTableDetail2Tmp.Columns.IndexOf("HINMOKU_CD");
                            ctrlName = string.Format("PHN_HINMOKU_CD_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 品名
                            index = dataTableDetail2Tmp.Columns.IndexOf("HINMOKU_NM");
                            ctrlName = string.Format("PHN_HINMOKU_NM_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 予定数量
                            index = dataTableDetail2Tmp.Columns.IndexOf("YOTEISU");
                            ctrlName = string.Format("PHN_YOTEISU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 実績数量
                            index = dataTableDetail2Tmp.Columns.IndexOf("JISSEKISU");
                            ctrlName = string.Format("PHN_JISSEKISU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 単位
                            index = dataTableDetail2Tmp.Columns.IndexOf("TANI");
                            ctrlName = string.Format("PHN_TANI_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 単価
                            index = dataTableDetail2Tmp.Columns.IndexOf("TANKA");
                            ctrlName = string.Format("PHN_TANKA_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 金額
                            index = dataTableDetail2Tmp.Columns.IndexOf("KINGAKU");
                            ctrlName = string.Format("PHN_KINGAKU_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;
                            // 明細備考
                            index = dataTableDetail2Tmp.Columns.IndexOf("MEISAI_BKO");
                            ctrlName = string.Format("PHN_MEISAI_BKO_{0}_FLB", rowNo);
                            row[ctrlName] = string.Empty;

                            if (rowNo == ConstMaxDispDetail2RowCount)
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

                    #endregion - Detail 2 -

                    #region - Footer -

                    if (dataTableFooterTmp.Rows.Count > 0)
                    {
                        if (pageNo == maxPage)
                        {
                            // 金額計
                            index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_KEI");
                            ctrlName = "DF_KINGAKU_FLB";
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 消費税
                            index = dataTableFooterTmp.Columns.IndexOf("SHOHIZEI");
                            ctrlName = "DF_SHOHIZEI_FLB";
                            if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 合計金額
                            index = dataTableFooterTmp.Columns.IndexOf("GOKEI");
                            ctrlName = "DF_GOKEI_FLB";
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
                            // 金額計
                            index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_KEI");
                            ctrlName = "DF_KINGAKU_FLB";
                            row[ctrlName] = "***********";

                            // 消費税
                            index = dataTableFooterTmp.Columns.IndexOf("SHOHIZEI");
                            ctrlName = "DF_SHOHIZEI_FLB";
                            row[ctrlName] = "**********";
                            
                            // 合計金額
                            index = dataTableFooterTmp.Columns.IndexOf("GOKEI");
                            ctrlName = "DF_GOKEI_FLB";
                            row[ctrlName] = "*************";
                        }
                    }
                    else
                    {
                        // 金額計
                        index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_KEI");
                        ctrlName = "DF_KINGAKU_FLB";
                        row[ctrlName] = string.Empty;

                        // 消費税
                        index = dataTableFooterTmp.Columns.IndexOf("SHOHIZEI");
                        ctrlName = "DF_SHOHIZEI_FLB";
                        row[ctrlName] = string.Empty;
                        
                        // 合計金額
                        index = dataTableFooterTmp.Columns.IndexOf("GOKEI");
                        ctrlName = "DF_GOKEI_FLB";
                        row[ctrlName] = string.Empty;
                    }

                    #endregion - Footer -

                    row["PHN_PAGE_NO_FLB"] = string.Format("{0}/{1}頁", pageNo, maxPage);

                    this.dataTable.Rows.Add(row);
                }
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

            #region - Header -

            //// タイトル
            //this.SetFieldName("DH_TITLE_VLB", r_framework.Const.WINDOW_TITLEExt.ToTitleString(this.windowID));

            if (this.UketsukeType == UketsukeTypeDef.Syuusyuu)
            {   // 収集
                this.SetFieldName("FH_DENPYOU_SHURUI_VLB", "収集");
            }
            else if (this.UketsukeType == UketsukeTypeDef.Syutsuka)
            {   // 出荷
                this.SetFieldName("FH_DENPYOU_SHURUI_VLB", "出荷");
            }
            else
            {
                this.SetFieldVisible("FH_DENPYOU_SHURUI_VLB", false);
            }

            if (dataTableHeaderTmp.Rows.Count > 0)
            {
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
                index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_NM");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 16)
                    {
                        this.SetFieldName("DH_UNTENSHA_NM_CTL", encoding.GetString(byteArray, 0, 16));
                    }
                    else
                    {
                        this.SetFieldName("DH_UNTENSHA_NM_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName("DH_UNTENSHA_NM_CTL", string.Empty);
                }

                // 補助員CD
                index = dataTableHeaderTmp.Columns.IndexOf("HOJOIN_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("DH_HOJOIN_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("DH_HOJOIN_CD_CTL", string.Empty);
                }

                // 補助員名
                index = dataTableHeaderTmp.Columns.IndexOf("HOJOIN_NM");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 16)
                    {
                        this.SetFieldName("DH_HOJOIN_NM_CTL", encoding.GetString(byteArray, 0, 16));
                    }
                    else
                    {
                        this.SetFieldName("DH_HOJOIN_NM_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName("DH_HOJOIN_NM_CTL", string.Empty);
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
                index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_NM");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 20)
                    {
                        this.SetFieldName("DH_SHASHU_NM_CTL", encoding.GetString(byteArray, 0, 20));
                    }
                    else
                    {
                        this.SetFieldName("DH_SHASHU_NM_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName("DH_SHASHU_NM_CTL", string.Empty);
                }

                // 車輌CD
                index = dataTableHeaderTmp.Columns.IndexOf("SHARYO_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("DH_SHARYO_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("DH_SHARYO_CD_CTL", string.Empty);
                }

                // 車輌名
                index = dataTableHeaderTmp.Columns.IndexOf("SHARYO_NM");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 20)
                    {
                        this.SetFieldName("DH_SHARYO_NM_CTL", encoding.GetString(byteArray, 0, 20));
                    }
                    else
                    {
                        this.SetFieldName("DH_SHARYO_NM_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName("DH_SHARYO_NM_CTL", string.Empty);
                }
            }
            else
            {
                // 運転者CD
                index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_CD");
                this.SetFieldName("DH_UNTENSHA_CD_CTL", string.Empty);

                // 運転者名
                index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_NM");
                this.SetFieldName("DH_UNTENSHA_NM_CTL", string.Empty);
                
                // 補助員CD
                index = dataTableHeaderTmp.Columns.IndexOf("HOJOIN_CD");
                this.SetFieldName("DH_HOJOIN_CD_CTL", string.Empty);
                
                // 補助員名
                index = dataTableHeaderTmp.Columns.IndexOf("HOJOIN_NM");
                this.SetFieldName("DH_HOJOIN_NM_CTL", string.Empty);
                
                // 車種CD
                index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_CD");
                this.SetFieldName("DH_SHASHU_CD_CTL", string.Empty);
                
                // 車種名
                index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_NM");
                this.SetFieldName("DH_SHASHU_NM_CTL", string.Empty);
                
                // 車輌CD
                index = dataTableHeaderTmp.Columns.IndexOf("SHARYO_CD");
                this.SetFieldName("DH_SHARYO_CD_CTL", string.Empty);
                
                // 車輌名
                index = dataTableHeaderTmp.Columns.IndexOf("SHARYO_NM");
                this.SetFieldName("DH_SHARYO_NM_CTL", string.Empty);
            }

            #endregion - Header -

            #region - Footer -
            
            // Footerの概念なし

            #endregion - Footer -
        }

        /// <summary>SQL文でヘッダ情報を取得する</summary>
        /// <returns>SQL文</returns>
        private string GetSqlStringHeader()
        {
            StringBuilder stringBuilder = new StringBuilder();

            string tableNameEntry = this.UketsukeType == UketsukeTypeDef.Syuusyuu ? "T_UKETSUKE_SS_ENTRY" : "T_UKETSUKE_SK_ENTRY";
            string tableNameDetail = this.UketsukeType == UketsukeTypeDef.Syuusyuu ? "T_UKETSUKE_SS_DETAIL" : "T_UKETSUKE_SK_DETAIL";

            stringBuilder.Append("SELECT ");
            stringBuilder.Append(tableNameEntry + ".SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append(tableNameEntry + ".SEQ AS SEQ, ");
            stringBuilder.Append(tableNameEntry + ".UNTENSHA_CD AS UNTENSHA_CD, ");
            stringBuilder.Append(tableNameEntry + ".UNTENSHA_NAME AS UNTENSHA_NAME, ");
            stringBuilder.Append(tableNameEntry + ".HOJOIN_CD AS HOJOIN_CD, ");
            stringBuilder.Append(tableNameEntry + ".HOJOIN_NAME AS HOJOIN_NAME, ");
            stringBuilder.Append(tableNameEntry + ".SHASHU_CD AS SHASHU_CD, ");
            stringBuilder.Append(tableNameEntry + ".SHASHU_NAME AS SHASHU_NAME, ");
            stringBuilder.Append(tableNameEntry + ".SHARYOU_CD AS SHARYOU_CD, ");
            stringBuilder.Append(tableNameEntry + ".SHARYOU_NAME AS SHARYOU_NAME, ");
            stringBuilder.Append(tableNameEntry + ".SAGYOU_DATE AS SAGYOU_DATE, ");

            if (this.UketsukeType == UketsukeTypeDef.Syuusyuu)
            {   // 収集
                stringBuilder.Append(tableNameEntry + ".NIOROSHI_DATE AS NIOROSHI_DATE, ");
            }
            else
            {   // 出荷
                stringBuilder.Append(tableNameEntry + ".NIZUMI_DATE AS NIZUMI_DATE, ");
            }

            stringBuilder.Append(tableNameEntry + ".GENCHAKU_TIME_NAME AS GENCHAKU_TIME_NAME, ");
            stringBuilder.Append(tableNameEntry + ".GENCHAKU_TIME AS GENCHAKU_TIME, ");
            stringBuilder.Append(tableNameEntry + ".SAGYOU_TIME AS SAGYOU_TIME, ");
            stringBuilder.Append(tableNameEntry + ".UKETSUKE_NUMBER AS UKETSUKE_NUMBER, ");
            stringBuilder.Append(tableNameEntry + ".EIGYOU_TANTOUSHA_CD AS EIGYOU_TANTOUSHA_CD, ");
            stringBuilder.Append(tableNameEntry + ".EIGYOU_TANTOUSHA_NAME AS EIGYOU_TANTOUSHA_NAME, ");
            stringBuilder.Append(tableNameEntry + ".UKETSUKE_DATE AS UKETSUKE_DATE, ");
            stringBuilder.Append(tableNameEntry + ".CREATE_DATE AS CREATE_DATE, ");
            stringBuilder.Append(tableNameEntry + ".UPDATE_DATE AS UPDATE_DATE, ");
            stringBuilder.Append(tableNameEntry + ".TORIHIKISAKI_CD AS TORIHIKISAKI_CD, ");
            stringBuilder.Append(tableNameEntry + ".TORIHIKISAKI_NAME AS TORIHIKISAKI_NAME, ");
            stringBuilder.Append("M_TORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD AS TORIHIKI_KBN_CD, ");
            stringBuilder.Append("M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME_RYAKU AS NYUUSHUKKIN_KBN_NAME_RYAKU, ");
            stringBuilder.Append(tableNameEntry + ".GYOUSHA_CD AS GYOUSHA_CD, ");
            stringBuilder.Append(tableNameEntry + ".GYOUSHA_NAME AS GYOUSHA_NAME, ");
            stringBuilder.Append(tableNameEntry + ".GENBA_CD AS GENBA_CD, ");
            stringBuilder.Append(tableNameEntry + ".GENBA_NAME AS GENBA_NAME, ");
            stringBuilder.Append("M_GENBA.GENBA_ADDRESS1 AS GENBA_ADDRESS1, ");
            stringBuilder.Append("M_GENBA.GENBA_ADDRESS2 AS GENBA_ADDRESS2, ");
            stringBuilder.Append(tableNameEntry + ".GENBA_TEL AS GENBA_TEL, ");
            stringBuilder.Append(tableNameEntry + ".TANTOSHA_NAME AS TANTOSHA_NAME, ");
            stringBuilder.Append(tableNameEntry + ".TANTOSHA_TEL AS TANTOSHA_TEL, ");

            if (this.UketsukeType == UketsukeTypeDef.Syuusyuu)
            {   // 収集
                stringBuilder.Append(tableNameEntry + ".NIOROSHI_GYOUSHA_CD AS NIOROSHI_GYOUSHA_CD, ");
                stringBuilder.Append(tableNameEntry + ".NIOROSHI_GYOUSHA_NAME AS NIOROSHI_GYOUSHA_NAME, ");
                stringBuilder.Append(tableNameEntry + ".NIOROSHI_GENBA_CD AS NIOROSHI_GENBA_CD, ");
                stringBuilder.Append(tableNameEntry + ".NIOROSHI_GENBA_NAME AS NIOROSHI_GENBA_NAME, ");
            }
            else
            {   // 出荷
                stringBuilder.Append(tableNameEntry + ".NIZUMI_GYOUSHA_CD AS NIZUMI_GYOUSHA_CD, ");
                stringBuilder.Append(tableNameEntry + ".NIZUMI_GYOUSHA_NAME AS NIZUMI_GYOUSHA_NAME, ");
                stringBuilder.Append(tableNameEntry + ".NIZUMI_GENBA_CD AS NIZUMI_GENBA_CD, ");
                stringBuilder.Append(tableNameEntry + ".NIZUMI_GENBA_NAME AS NIZUMI_GENBA_NAME, ");
            }

            stringBuilder.Append(tableNameEntry + ".MANIFEST_SHURUI_CD AS MANIFEST_SHURUI_CD, ");
            stringBuilder.Append("M_MANIFEST_SHURUI.MANIFEST_SHURUI_NAME_RYAKU AS MANIFEST_SHURUI_NAME_RYAKU, ");
            stringBuilder.Append(tableNameEntry + ".MANIFEST_TEHAI_CD AS MANIFEST_TEHAI_CD, ");
            stringBuilder.Append("M_MANIFEST_TEHAI.MANIFEST_TEHAI_NAME_RYAKU AS MANIFEST_TEHAI_NAME_RYAKU, ");
            stringBuilder.Append(tableNameEntry + ".COURSE_KUMIKOMI_CD AS COURSE_KUMIKOMI_CD, ");
            stringBuilder.Append(tableNameEntry + ".COURSE_NAME_CD AS COURSE_NAME_CD, ");
            stringBuilder.Append("M_COURSE_NAME.COURSE_NAME_RYAKU AS COURSE_NAME_RYAKU, ");
            stringBuilder.Append(tableNameEntry + ".UKETSUKE_BIKOU1 AS UKETSUKE_BIKOU1, ");
            stringBuilder.Append(tableNameEntry + ".UKETSUKE_BIKOU2 AS UKETSUKE_BIKOU2, ");
            stringBuilder.Append(tableNameEntry + ".UKETSUKE_BIKOU3 AS UKETSUKE_BIKOU3, ");
            stringBuilder.Append(tableNameEntry + ".UNTENSHA_SIJIJIKOU1 AS UNTENSHA_SIJIJIKOU1, ");
            stringBuilder.Append(tableNameEntry + ".UNTENSHA_SIJIJIKOU2 AS UNTENSHA_SIJIJIKOU2, ");
            stringBuilder.Append(tableNameEntry + ".UNTENSHA_SIJIJIKOU3 AS UNTENSHA_SIJIJIKOU3, ");

            stringBuilder.Append(tableNameEntry + ".KINGAKU_TOTAL AS KINGAKU_TOTAL, ");
            stringBuilder.Append(tableNameEntry + ".SHOUHIZEI_TOTAL AS SHOUHIZEI_TOTAL, ");
            stringBuilder.Append(tableNameEntry + ".GOUKEI_KINGAKU_TOTAL AS GOUKEI_KINGAKU_TOTAL, ");

            stringBuilder.Append("T_CONTENA_RESERVE.CONTENA_SHURUI_CD AS CONTENA_SHURUI_CD, ");
            stringBuilder.Append(tableNameDetail + ".HINMEI_CD AS HINMEI_CD, ");
            stringBuilder.Append(tableNameDetail + ".HINMEI_NAME AS HINMEI_NAME, ");
            stringBuilder.Append(tableNameDetail + ".SUURYOU AS SUURYOU, ");
            stringBuilder.Append("M_UNIT.UNIT_NAME_RYAKU AS UNIT_NAME_RYAKU, ");
            stringBuilder.Append(tableNameDetail + ".TANKA AS TANKA, ");
            stringBuilder.Append(tableNameDetail + ".KINGAKU AS KINGAKU, ");
            stringBuilder.Append(tableNameDetail + ".MEISAI_BIKOU AS MEISAI_BIKOU, ");
            stringBuilder.Append(tableNameEntry + ".SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append(tableNameEntry + ".SEQ AS SEQ, ");
            stringBuilder.Append(tableNameDetail + ".DETAIL_SYSTEM_ID AS DETAIL_SYSTEM_ID, ");
            stringBuilder.Append("M_GYOUSHA.GYOUSHA_NAME_RYAKU AS GYOUSHA_NAME_RYAKU, ");
            stringBuilder.Append("M_GENBA.GENBA_NAME_RYAKU AS GENBA_NAME_RYAKU ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append(tableNameEntry + " ");
            stringBuilder.Append("LEFT JOIN " + tableNameDetail + " ");
            stringBuilder.Append("ON " + tableNameDetail + ".SYSTEM_ID = " + tableNameEntry + ".SYSTEM_ID ");
            stringBuilder.Append("AND " + tableNameDetail + ".SEQ = " + tableNameEntry + ".SEQ ");
            stringBuilder.Append("LEFT JOIN M_TORIHIKISAKI_SEIKYUU ");
            stringBuilder.Append("ON M_TORIHIKISAKI_SEIKYUU.TORIHIKISAKI_CD = " + tableNameEntry + ".TORIHIKISAKI_CD ");
            stringBuilder.Append("LEFT JOIN M_NYUUSHUKKIN_KBN ");
            stringBuilder.Append("ON M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD = M_TORIHIKISAKI_SEIKYUU.TORIHIKI_KBN_CD ");
            stringBuilder.Append("LEFT JOIN M_GYOUSHA ");
            stringBuilder.Append("ON M_GYOUSHA.GYOUSHA_CD = " + tableNameEntry + ".GYOUSHA_CD ");
            stringBuilder.Append("LEFT JOIN M_GENBA ");
            stringBuilder.Append("ON M_GENBA.GYOUSHA_CD = " + tableNameEntry + ".GYOUSHA_CD ");
            stringBuilder.Append("AND M_GENBA.GENBA_CD = " + tableNameEntry + ".GENBA_CD ");
            stringBuilder.Append("LEFT JOIN M_UNIT ");
            stringBuilder.Append("ON M_UNIT.UNIT_CD = " + tableNameDetail + ".UNIT_CD ");
            stringBuilder.Append("LEFT JOIN M_MANIFEST_SHURUI ");
            stringBuilder.Append("ON M_MANIFEST_SHURUI.MANIFEST_SHURUI_CD = " + tableNameEntry + ".MANIFEST_SHURUI_CD ");
            stringBuilder.Append("LEFT JOIN M_MANIFEST_TEHAI ");
            stringBuilder.Append("ON M_MANIFEST_TEHAI.MANIFEST_TEHAI_CD = " + tableNameEntry + ".MANIFEST_TEHAI_CD ");
            stringBuilder.Append("LEFT JOIN M_COURSE_NAME ");
            stringBuilder.Append("ON M_COURSE_NAME.COURSE_NAME_CD = " + tableNameEntry + ".COURSE_NAME_CD ");
            stringBuilder.Append("LEFT JOIN T_CONTENA_RESERVE ");
            stringBuilder.Append("ON T_CONTENA_RESERVE.CONTENA_SHURUI_CD = " + tableNameEntry + ".COURSE_NAME_CD ");
            stringBuilder.Append("WHERE ");
            stringBuilder.Append(tableNameEntry + ".DELETE_FLG = 0 AND ");
            stringBuilder.Append(tableNameEntry + ".UKETSUKE_NUMBER = " + this.DenpyouNo);

            return stringBuilder.ToString();
        }

        /// <summary>SQL文で詳細情報を取得する</summary>
        /// <returns>SQL文</returns>
        private string GetSqlStringDetail()
        {
            StringBuilder stringBuilder = new StringBuilder();

            string tableNameEntry = this.UketsukeType == UketsukeTypeDef.Syuusyuu ? "T_UKETSUKE_SS_ENTRY" : "T_UKETSUKE_SK_ENTRY";
            //string tableNameDetail = this.UketsukeType == UketsukeTypeDef.Syuusyuu ? "T_UKETSUKE_SS_DETAIL" : "T_UKETSUKE_SK_DETAIL";

            stringBuilder.Append("SELECT ");
            stringBuilder.Append(tableNameEntry + ".SYSTEM_ID AS SYSTEM_ID, ");
            stringBuilder.Append(tableNameEntry + ".SEQ AS SEQ, ");
            stringBuilder.Append("T_CONTENA_RESERVE.CONTENA_SHURUI_CD AS CONTENA_SHURUI_CD, ");
            stringBuilder.Append("M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU AS CONTENA_SHURUI_NAME_RYAKU, ");
            stringBuilder.Append("T_CONTENA_RESERVE.CONTENA_CD AS CONTENA_CD, ");
            stringBuilder.Append("M_CONTENA.CONTENA_NAME_RYAKU AS CONTENA_NAME_RYAKU, ");
            stringBuilder.Append("T_CONTENA_RESERVE.CONTENA_SET_KBN AS CONTENA_SET_KBN, ");
            stringBuilder.Append("CASE T_CONTENA_RESERVE.CONTENA_SET_KBN ");
            stringBuilder.Append("WHEN '1' THEN '設置' ");
            stringBuilder.Append("WHEN '2' THEN '引揚' ");
            stringBuilder.Append("END AS CONTENA_JYOUKYOU, ");
            stringBuilder.Append("T_CONTENA_RESERVE.DAISUU_CNT AS DAISUU_CNT ");

            stringBuilder.Append("FROM ");
            stringBuilder.Append("T_CONTENA_RESERVE ");
            stringBuilder.Append("INNER JOIN " + tableNameEntry + " ");
            stringBuilder.Append("ON " + tableNameEntry + ".SYSTEM_ID = T_CONTENA_RESERVE.SYSTEM_ID ");
            stringBuilder.Append("AND " + tableNameEntry + ".SEQ = T_CONTENA_RESERVE.SEQ ");
            stringBuilder.Append("AND " + tableNameEntry + ".DELETE_FLG = 0");
            stringBuilder.Append("LEFT JOIN M_CONTENA_SHURUI ");
            stringBuilder.Append("ON M_CONTENA_SHURUI.CONTENA_SHURUI_CD = T_CONTENA_RESERVE.CONTENA_SHURUI_CD ");
            stringBuilder.Append("LEFT JOIN M_CONTENA ");
            stringBuilder.Append("ON M_CONTENA.CONTENA_SHURUI_CD = T_CONTENA_RESERVE.CONTENA_SHURUI_CD ");
            stringBuilder.Append("AND M_CONTENA.CONTENA_CD = T_CONTENA_RESERVE.CONTENA_CD ");

            stringBuilder.Append("WHERE ");
            stringBuilder.Append(tableNameEntry + ".UKETSUKE_NUMBER = " + this.DenpyouNo + " ");
            stringBuilder.Append("ORDER BY CONTENA_SET_KBN");

            return stringBuilder.ToString();
        }

        /// <summary>データーベースからデータを取得する処理を実行する</summary>
        private void GetDatabaseData()
        {
            if (this.dao == null)
            {
                return;
            }

            int index;
            int indexTmp;
            string format = string.Empty;

            DataTable dataTableTmp = null;
            DataRow rowTmp = null;

            string sql = string.Empty;
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");
            stringBuilder.Append("SYS_SUURYOU_FORMAT , ");
            stringBuilder.Append("SYS_JYURYOU_FORMAT ,");
            stringBuilder.Append("SYS_TANKA_FORMAT ");
            stringBuilder.Append("FROM ");
            stringBuilder.Append("M_SYS_INFO ");

            DataTable dataTableSysInfo = this.dao.GetDateForStringSql(stringBuilder.ToString());

            // データーベースからヘッダ情報を取得
            sql = this.GetSqlStringHeader();
            DataTable dataTableHeaderTmp = this.dao.GetDateForStringSql(sql);

            // データーベースから詳細情報取得
            sql = this.GetSqlStringDetail();
            DataTable dataTableDetailTmp = this.dao.GetDateForStringSql(sql);

            switch (this.windowID)
            {
                case WINDOW_ID.R_HAISYA_IRAISYO:                    // R345 配車依頼書
                case WINDOW_ID.R_SAGYOU_SIJISYO:                    // R350 作業指示書

                    #region - Header -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Header";

                    #region - Header Columns -

                    // 運転者CD
                    dataTableTmp.Columns.Add("UNTENSHA_CD");
                    // 運転者名
                    dataTableTmp.Columns.Add("UNTENSHA_NM");
                    // 補助員CD
                    dataTableTmp.Columns.Add("HOJOIN_CD");
                    // 補助員名
                    dataTableTmp.Columns.Add("HOJOIN_NM");
                    // 車種CD
                    dataTableTmp.Columns.Add("SHASHU_CD");
                    // 車種名
                    dataTableTmp.Columns.Add("SHASHU_NM");
                    // 車輌CD
                    dataTableTmp.Columns.Add("SHARYO_CD");
                    // 車輌名
                    dataTableTmp.Columns.Add("SHARYO_NM");
                    // 作業日
                    dataTableTmp.Columns.Add("SAGYOBI_DATE");
                    // 荷降日/荷積日
                    dataTableTmp.Columns.Add("NIOROSHI_DATE");
                    // 現着時間
                    dataTableTmp.Columns.Add("GENCHAKU_T");
                    // 作業時間
                    dataTableTmp.Columns.Add("SAGYO_T");
                    // 受付番号
                    dataTableTmp.Columns.Add("UKETSUKE_NO");
                    // 営業担当者CD
                    dataTableTmp.Columns.Add("EIGYOTANTO_CD");
                    // 営業担当者名
                    dataTableTmp.Columns.Add("EIGYOTANTO_NM");
                    // 受付日
                    dataTableTmp.Columns.Add("UKETSUKE_DATE");
                    // 初回登録日
                    dataTableTmp.Columns.Add("1ST_UKETSUKE");
                    // 最終更新日
                    dataTableTmp.Columns.Add("LAST_UKETSUKE");
                    // 取引先CD
                    dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
                    // 取引先名
                    dataTableTmp.Columns.Add("TORIHIKISAKI_NM");
                    // 取引区分CD
                    dataTableTmp.Columns.Add("TORIHIKI_KBN_CD");
                    // 取引区分名
                    dataTableTmp.Columns.Add("TORIHIKI_KBN_NM");
                    // 業者CD
                    dataTableTmp.Columns.Add("GYOSHA_CD");
                    // 業者名
                    dataTableTmp.Columns.Add("GYOSHA_NM");
                    // 現場名CD
                    dataTableTmp.Columns.Add("GENBA_CD");
                    // 現場名
                    dataTableTmp.Columns.Add("GENBA_NM");
                    // 現場住所
                    dataTableTmp.Columns.Add("GENBA_JUSHO");
                    // 現場電話
                    dataTableTmp.Columns.Add("GENBA_DENWA_NO");
                    // 担当者
                    dataTableTmp.Columns.Add("TANTOSHA_NM");
                    // 担当携帯
                    dataTableTmp.Columns.Add("TANTO_KEITAI_NO");
                    // 荷降業者CD/荷積業者CD
                    dataTableTmp.Columns.Add("NIOROSHIGYOSHA_CD");
                    // 荷降業者名/荷積業者名
                    dataTableTmp.Columns.Add("NIOROSHIGYOSHA_NM");
                    // 荷降場CD/荷積CD
                    dataTableTmp.Columns.Add("NIOROSHIJO_CD");
                    // 荷降場名/荷積場名
                    dataTableTmp.Columns.Add("NIOROSHIJO_NM");
                    // マニ種類CD
                    dataTableTmp.Columns.Add("MANI_TYPE_CD");
                    // マニ種類
                    dataTableTmp.Columns.Add("MANI_TYPE_NM");
                    // マニ手配CD
                    dataTableTmp.Columns.Add("MANI_TEHAI_CD");
                    // マニ手配
                    dataTableTmp.Columns.Add("MANI_TEHAI_NM");
                    // コース組込CD
                    dataTableTmp.Columns.Add("COURSE_KK_CD");
                    // コース組込名称
                    dataTableTmp.Columns.Add("COURSE_KK_NM");
                    // コース名CD
                    dataTableTmp.Columns.Add("COURSE_CD");
                    // コース名
                    dataTableTmp.Columns.Add("COURSE_NM");
                    // 受付備考１
                    dataTableTmp.Columns.Add("UKETSUKE_BK1");
                    // 受付備考２
                    dataTableTmp.Columns.Add("UKETSUKE_BK2");
                    // 受付備考３
                    dataTableTmp.Columns.Add("UKETSUKE_BK3");
                    // 運転者備考１
                    dataTableTmp.Columns.Add("UNTENSHA_BK1");
                    // 運転者備考２
                    dataTableTmp.Columns.Add("UNTENSHA_BK2");
                    // 運転者備考３
                    dataTableTmp.Columns.Add("UNTENSHA_BK3");

                    #endregion - Header Columns -

                    #region - Header Rows -

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 運転者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UNTENSHA_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UNTENSHA_CD"] = string.Empty;
                        }

                        // 運転者名
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UNTENSHA_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UNTENSHA_NM"] = string.Empty;
                        }

                        // 補助員CD
                        index = dataTableHeaderTmp.Columns.IndexOf("HOJOIN_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["HOJOIN_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["HOJOIN_CD"] = string.Empty;
                        }

                        // 補助員名
                        index = dataTableHeaderTmp.Columns.IndexOf("HOJOIN_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["HOJOIN_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["HOJOIN_NM"] = string.Empty;
                        }

                        // 車種CD
                        index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["SHASHU_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["SHASHU_CD"] = string.Empty;
                        }

                        // 車種名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHASHU_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["SHASHU_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["SHASHU_NM"] = string.Empty;
                        }

                        // 車輌CD
                        index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["SHARYO_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["SHARYO_CD"] = string.Empty;
                        }

                        // 車輌名
                        index = dataTableHeaderTmp.Columns.IndexOf("SHARYOU_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["SHARYO_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["SHARYO_NM"] = string.Empty;
                        }

                        // 作業日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["SAGYOBI_DATE"] = DateTime.Parse(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()).ToString("yyy/MM/dd (ddd)");
                        }
                        else
                        {
                            rowTmp["SAGYOBI_DATE"] = string.Empty;
                        }

                        // 荷降日/荷積日
                        if (this.UketsukeType == UketsukeTypeDef.Syuusyuu)
                        {   // 収集
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_DATE");
                        }
                        else
                        {   // 出荷
                            index = dataTableHeaderTmp.Columns.IndexOf("NIZUMI_DATE");
                        }

                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["NIOROSHI_DATE"] = DateTime.Parse(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()).ToString("yyy/MM/dd (ddd)");
                        }
                        else
                        {
                            rowTmp["NIOROSHI_DATE"] = string.Empty;
                        }

                        // 現着時間
                        string strGenchaku = string.Empty;
                        index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_TIME_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            strGenchaku = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_TIME");
                        string[] strGenchakuTime = null;
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            strGenchakuTime = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString().Split(':');

                            strGenchaku = strGenchaku + " " + strGenchakuTime[0] + "時" + strGenchakuTime[1] + "分";
                            rowTmp["GENCHAKU_T"] = strGenchaku;
                        }
                        else
                        {
                            rowTmp["GENCHAKU_T"] = string.Empty;
                        }

                        // 作業時間
                        string[] strSagyoTime;
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_TIME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            strSagyoTime = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString().Split(':');
                            rowTmp["SAGYO_T"] = strSagyoTime[0] + "時" + strSagyoTime[1] + "分";
                        }
                        else
                        {
                            rowTmp["SAGYO_T"] = string.Empty;
                        }

                        // 受付番号
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_NUMBER");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UKETSUKE_NO"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UKETSUKE_NO"] = string.Empty;
                        }

                        // 営業担当者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYOU_TANTOUSHA_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["EIGYOTANTO_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["EIGYOTANTO_CD"] = string.Empty;
                        }

                        // 営業担当者名
                        index = dataTableHeaderTmp.Columns.IndexOf("EIGYOU_TANTOUSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["EIGYOTANTO_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["EIGYOTANTO_NM"] = string.Empty;
                        }

                        // 受付日
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UKETSUKE_DATE"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UKETSUKE_DATE"] = string.Empty;
                        }

                        // 初回登録日
                        index = dataTableHeaderTmp.Columns.IndexOf("CREATE_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["1ST_UKETSUKE"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["1ST_UKETSUKE"] = string.Empty;
                        }

                        // 最終更新日
                        index = dataTableHeaderTmp.Columns.IndexOf("UPDATE_DATE");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["LAST_UKETSUKE"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["LAST_UKETSUKE"] = string.Empty;
                        }

                        // 取引先CD
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["TORIHIKISAKI_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["TORIHIKISAKI_CD"] = string.Empty;
                        }

                        // 取引先名
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["TORIHIKISAKI_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["TORIHIKISAKI_NM"] = string.Empty;
                        }

                        // 取引区分CD
                        index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKI_KBN_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["TORIHIKI_KBN_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["TORIHIKI_KBN_CD"] = string.Empty;
                        }

                        // 取引区分名
                        index = dataTableHeaderTmp.Columns.IndexOf("NYUUSHUKKIN_KBN_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["TORIHIKI_KBN_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["TORIHIKI_KBN_NM"] = string.Empty;
                        }

                        // 業者CD
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["GYOSHA_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["GYOSHA_CD"] = string.Empty;
                        }

                        // 業者名
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["GYOSHA_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["GYOSHA_NM"] = string.Empty;
                        }

                        // 現場名CD
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["GENBA_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["GENBA_CD"] = string.Empty;
                        }

                        // 現場名
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["GENBA_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["GENBA_NM"] = string.Empty;
                        }

                        // 現場住所
                        string strAdd = string.Empty;
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_ADDRESS1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            strAdd = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                        }

                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_ADDRESS2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            strAdd += dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                        }

                        //rowTmp["GENBA_JUSHO"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        rowTmp["GENBA_JUSHO"] = strAdd;

                        // 現場電話
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_TEL");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["GENBA_DENWA_NO"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["GENBA_DENWA_NO"] = string.Empty;
                        }

                        // 担当者
                        index = dataTableHeaderTmp.Columns.IndexOf("TANTOSHA_NAME");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["TANTOSHA_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["TANTOSHA_NM"] = string.Empty;
                        }

                        // 担当携帯
                        index = dataTableHeaderTmp.Columns.IndexOf("TANTOSHA_TEL");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["TANTO_KEITAI_NO"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["TANTO_KEITAI_NO"] = string.Empty;
                        }

                        if (this.UketsukeType == UketsukeTypeDef.Syuusyuu)
                        {   // 収集

                            // 荷降業者CD/荷積業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIGYOSHA_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIGYOSHA_CD"] = string.Empty;
                            }

                            // 荷降業者名/荷積業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIGYOSHA_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIGYOSHA_NM"] = string.Empty;
                            }

                            // 荷降場CD/荷積CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GENBA_CD");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIJO_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIJO_CD"] = string.Empty;
                            }

                            // 荷降場名/荷積場名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GENBA_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIJO_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIJO_NM"] = string.Empty;
                            }
                        }
                        else
                        {
                            // 荷降業者CD/荷積業者CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIZUMI_GYOUSHA_CD");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIGYOSHA_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIGYOSHA_CD"] = string.Empty;
                            }

                            // 荷降業者名/荷積業者名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIZUMI_GYOUSHA_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIGYOSHA_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIGYOSHA_NM"] = string.Empty;
                            }

                            // 荷降場CD/荷積CD
                            index = dataTableHeaderTmp.Columns.IndexOf("NIZUMI_GENBA_CD");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIJO_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIJO_CD"] = string.Empty;
                            }

                            // 荷降場名/荷積場名
                            index = dataTableHeaderTmp.Columns.IndexOf("NIZUMI_GENBA_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                rowTmp["NIOROSHIJO_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["NIOROSHIJO_NM"] = string.Empty;
                            }
                        }

                        // マニ種類CD
                        index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_SHURUI_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["MANI_TYPE_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["MANI_TYPE_CD"] = string.Empty;
                        }

                        // マニ種類
                        index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_SHURUI_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["MANI_TYPE_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["MANI_TYPE_NM"] = string.Empty;
                        }

                        // マニ手配CD
                        index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_TEHAI_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["MANI_TEHAI_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["MANI_TEHAI_CD"] = string.Empty;
                        }

                        // マニ手配
                        index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_TEHAI_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["MANI_TEHAI_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["MANI_TEHAI_NM"] = string.Empty;
                        }

                        // コース組込CD
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_KUMIKOMI_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["COURSE_KK_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["COURSE_KK_CD"] = string.Empty;
                        }

                        // コース組込名称
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            if ((short)dataTableHeaderTmp.Rows[0].ItemArray[index] == 1)
                            {   // 臨時
                                rowTmp["COURSE_KK_NM"] = "臨時";
                            }
                            else if ((short)dataTableHeaderTmp.Rows[0].ItemArray[index] == 2)
                            {   // 組込
                                rowTmp["COURSE_KK_NM"] = "組込";
                            }
                            else
                            {
                                rowTmp["COURSE_KK_NM"] = string.Empty;
                            }
                        }
                        else
                        {
                            rowTmp["COURSE_KK_NM"] = string.Empty;
                        }

                        // コース名CD
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NAME_CD");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["COURSE_CD"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["COURSE_CD"] = string.Empty;
                        }

                        // コース名
                        index = dataTableHeaderTmp.Columns.IndexOf("COURSE_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["COURSE_NM"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["COURSE_NM"] = string.Empty;
                        }

                        // 受付備考１
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BIKOU1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UKETSUKE_BK1"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UKETSUKE_BK1"] = string.Empty;
                        }

                        // 受付備考２
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BIKOU2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UKETSUKE_BK2"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UKETSUKE_BK2"] = string.Empty;
                        }

                        // 受付備考３
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_BIKOU3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UKETSUKE_BK3"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UKETSUKE_BK3"] = string.Empty;
                        }

                        // 運転者備考１
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU1");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UNTENSHA_BK1"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UNTENSHA_BK1"] = string.Empty;
                        }

                        // 運転者備考２
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU2");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UNTENSHA_BK2"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UNTENSHA_BK2"] = string.Empty;
                        }

                        // 運転者備考３
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU3");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["UNTENSHA_BK3"] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["UNTENSHA_BK3"] = string.Empty;
                        }

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    #endregion - Header Rows -

                    this.DataTableList.Add("Header", dataTableTmp);

                    #endregion - Header -

                    #region - Detail 1 -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Detail1";

                    #region - Detail 1 Colums -

                    // コンテナ項番
                    dataTableTmp.Columns.Add("KONTENA_KBN");
                    // コンテナ種類CD
                    dataTableTmp.Columns.Add("KONTENA_TYPE_CD");
                    // コンテナ種類名称
                    dataTableTmp.Columns.Add("KONTENA_TYPE_NM");
                    // コンテナCD
                    dataTableTmp.Columns.Add("KONTENA_CD");
                    // コンテナ名称
                    dataTableTmp.Columns.Add("KONTENA_NM");
                    // コンテナ状況CD
                    dataTableTmp.Columns.Add("KONTENA_JOKYO_CD");
                    // コンテナ状況
                    dataTableTmp.Columns.Add("KONTENA_JOKYO_NM");
                    // 台数
                    dataTableTmp.Columns.Add("DAISU");

                    #endregion - Detail 1 Colums -

                    #region - Detail 1 Rows -

                    for (int i = 0; i < dataTableDetailTmp.Rows.Count; i++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // コンテナ項番
                        rowTmp["KONTENA_KBN"] = (i + 1).ToString();
                        // コンテナ種類CD
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_CD");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KONTENA_TYPE_CD"] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["KONTENA_TYPE_CD"] = string.Empty;
                        }

                        // コンテナ種類名称
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KONTENA_TYPE_NM"] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["KONTENA_TYPE_NM"] = string.Empty;
                        }

                        // コンテナCD
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_CD");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KONTENA_CD"] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["KONTENA_CD"] = string.Empty;
                        }

                        // コンテナ名称
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_NAME_RYAKU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KONTENA_NM"] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["KONTENA_NM"] = string.Empty;
                        }

                        // コンテナ状況CD
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SET_KBN");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KONTENA_JOKYO_CD"] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["KONTENA_JOKYO_CD"] = string.Empty;
                        }

                        // コンテナ状況
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_JYOUKYOU");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["KONTENA_JOKYO_NM"] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            rowTmp["KONTENA_JOKYO_NM"] = string.Empty;
                        }

                        // 数量フォーマット
                        format = string.Empty;
                        indexTmp = dataTableSysInfo.Columns.IndexOf("SYS_SUURYOU_FORMAT");
                        if (!this.IsDBNull(dataTableSysInfo.Rows[0].ItemArray[indexTmp]))
                        {
                            format = (string)dataTableSysInfo.Rows[0].ItemArray[indexTmp];
                        }

                        // 台数
                        index = dataTableDetailTmp.Columns.IndexOf("DAISUU_CNT");
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            rowTmp["DAISU"] = ((int)dataTableDetailTmp.Rows[i].ItemArray[index]).ToString();
                        }
                        else
                        {
                            rowTmp["DAISU"] = string.Empty;
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

                    // 品目項番
                    dataTableTmp.Columns.Add("HINMOKU_KBN");
                    // 品名CD
                    dataTableTmp.Columns.Add("HINMOKU_CD");
                    // 品名
                    dataTableTmp.Columns.Add("HINMOKU_NM");
                    // 予定数量
                    dataTableTmp.Columns.Add("YOTEISU");
                    // 実績数量
                    dataTableTmp.Columns.Add("JISSEKISU");
                    // 単位
                    dataTableTmp.Columns.Add("TANI");
                    // 単価
                    dataTableTmp.Columns.Add("TANKA");
                    // 金額
                    dataTableTmp.Columns.Add("KINGAKU");
                    // 明細備考
                    dataTableTmp.Columns.Add("MEISAI_BKO");

                    #endregion - Detail 2 Columns -

                    #region -  Detail 2 Rows -

                    for (int i = 0; i < dataTableHeaderTmp.Rows.Count; i++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 品目項番
                        rowTmp["HINMOKU_KBN"] = (i + 1).ToString();

                        // 品名CD
                        index = dataTableHeaderTmp.Columns.IndexOf("HINMEI_CD");
                        if (string.IsNullOrEmpty(dataTableHeaderTmp.Rows[i].ItemArray[index].ToString()))
                        {
                            rowTmp["HINMOKU_KBN"] = string.Empty;
                            rowTmp["HINMOKU_CD"] = string.Empty;
                            rowTmp["HINMOKU_NM"] = string.Empty;
                            rowTmp["YOTEISU"] = string.Empty;
                            rowTmp["JISSEKISU"] = string.Empty;
                            rowTmp["TANI"] = string.Empty;
                            rowTmp["TANKA"] = string.Empty;
                            rowTmp["KINGAKU"] = string.Empty;
                            rowTmp["MEISAI_BKO"] = string.Empty;
                        }
                        else
                        {
                            // 品名CD
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp["HINMOKU_CD"] = dataTableHeaderTmp.Rows[i].ItemArray[index];
                            }

                            // 品名
                            index = dataTableHeaderTmp.Columns.IndexOf("HINMEI_NAME");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp["HINMOKU_NM"] = dataTableHeaderTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["HINMOKU_NM"] = string.Empty;
                            }

                            // 数量フォーマット
                            format = string.Empty;
                            indexTmp = dataTableSysInfo.Columns.IndexOf("SYS_SUURYOU_FORMAT");
                            if (!this.IsDBNull(dataTableSysInfo.Rows[0].ItemArray[indexTmp]))
                            {
                                format = (string)dataTableSysInfo.Rows[0].ItemArray[indexTmp];
                            }

                            // 予定数量
                            index = dataTableHeaderTmp.Columns.IndexOf("SUURYOU");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp["YOTEISU"] = ((double)dataTableHeaderTmp.Rows[i].ItemArray[index]).ToString(format);
                            }
                            else
                            {
                                rowTmp["YOTEISU"] = string.Empty;
                            }

                            // 実績数量
                            ////index = dataTableSrcTmp.Columns.IndexOf("SUURYOU");
                            rowTmp["JISSEKISU"] = string.Empty;

                            // 単位
                            index = dataTableHeaderTmp.Columns.IndexOf("UNIT_NAME_RYAKU");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp["TANI"] = dataTableHeaderTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["TANI"] = string.Empty;
                            }

                            // 単価フォーマット
                            format = string.Empty;
                            indexTmp = dataTableSysInfo.Columns.IndexOf("SYS_TANKA_FORMAT");
                            if (!this.IsDBNull(dataTableSysInfo.Rows[0].ItemArray[indexTmp]))
                            {
                                format = (string)dataTableSysInfo.Rows[0].ItemArray[indexTmp];
                            }

                            // 単価
                            index = dataTableHeaderTmp.Columns.IndexOf("TANKA");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp["TANKA"] = ((decimal)dataTableHeaderTmp.Rows[i].ItemArray[index]).ToString(format);
                            }
                            else
                            {
                                rowTmp["TANKA"] = string.Empty;
                            }

                            // 金額
                            index = dataTableHeaderTmp.Columns.IndexOf("KINGAKU");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp["KINGAKU"] = ((decimal)dataTableHeaderTmp.Rows[i].ItemArray[index]).ToString("#,##0");
                            }
                            else
                            {
                                rowTmp["KINGAKU"] = string.Empty;
                            }

                            // 明細備考
                            index = dataTableHeaderTmp.Columns.IndexOf("MEISAI_BIKOU");
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[i].ItemArray[index]))
                            {
                                rowTmp["MEISAI_BKO"] = dataTableHeaderTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                rowTmp["MEISAI_BKO"] = string.Empty;
                            }
                        }

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    #endregion -  Detail 2 Rows -

                    this.DataTableList.Add("Detail2", dataTableTmp);

                    #endregion - Detail 2 -

                    #region - Footer -

                    dataTableTmp = new DataTable();
                    dataTableTmp.TableName = "Footer";

                    #region - Footer Columns -

                    dataTableTmp.Columns.Add("KINGAKU_KEI");
                    dataTableTmp.Columns.Add("SHOHIZEI");
                    dataTableTmp.Columns.Add("GOKEI");

                    #endregion - Footer Columns -

                    #region - Footer Rows -

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 金額計
                        index = dataTableHeaderTmp.Columns.IndexOf("KINGAKU_TOTAL");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["KINGAKU_KEI"] = ((decimal)dataTableHeaderTmp.Rows[0].ItemArray[index]).ToString("#,##0");
                        }
                        else
                        {
                            rowTmp["KINGAKU_KEI"] = 0;
                        }

                        // 消費税
                        index = dataTableHeaderTmp.Columns.IndexOf("SHOUHIZEI_TOTAL");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["SHOHIZEI"] = ((decimal)dataTableHeaderTmp.Rows[0].ItemArray[index]).ToString("#,##0");
                        }
                        else
                        {
                            rowTmp["SHOHIZEI"] = 0;
                        }

                        // 合計金額
                        index = dataTableHeaderTmp.Columns.IndexOf("GOUKEI_KINGAKU_TOTAL");
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            rowTmp["GOKEI"] = ((decimal)dataTableHeaderTmp.Rows[0].ItemArray[index]).ToString("#,##0");
                        }
                        else
                        {
                            rowTmp["GOKEI"] = 0;
                        }

                        dataTableTmp.Rows.Add(rowTmp);
                    }

                    #endregion - Footer Rows -

                    this.DataTableList.Add("Footer", dataTableTmp);

                    #endregion - Footer -

                    break;
            }
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
