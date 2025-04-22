using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace Shougun.Core.Allocation.HaishaMeisai
{
    #region - Class -

    /// <summary>(R346)配車明細表を表すクラス・コントロール</summary>
    public class ReportInfoR346 : ReportInfoBase
    {
        #region - Fields -

        private const int ConstMaxDispDetailRowCount = 9;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR346"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR346(WINDOW_ID windowID)
        {
            this.windowID = windowID;
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>出力タイプに関する列挙型</summary>
        public enum OutputTypeDef : int
        {
            /// <summary>収集に関する列挙型</summary>
            Collection = 1,

            /// <summary>出荷に関する列挙型</summary>
            Shipment,

            /// <summary>物販に関する列挙型</summary>
            Sale,
        }

        #endregion - Enums -

        #region - Properties -

        #endregion - Properties -

        #region - Methods -

        /// <summary>サンプルデータの作成処理を実行する</summary>
        public void CreateSampleData()
        {
            DataTable dataTableTmp;
            DataRow rowTmp;

            bool isPrint = true;
            bool isPrintH = true;

            for (int j = 0; j < 3; j++)
            {
                //for (OutputTypeDef outputTypeDef = OutputTypeDef.Collection; outputTypeDef <= OutputTypeDef.Sale; outputTypeDef++)
                for (OutputTypeDef outputTypeDef = OutputTypeDef.Sale; outputTypeDef >= OutputTypeDef.Collection; outputTypeDef--)
                {
                    string key = string.Empty;

                    switch (outputTypeDef)
                    {
                        case OutputTypeDef.Collection:     // 収集

                            #region - Header -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Header";

                            // 作業日
                            dataTableTmp.Columns.Add("SAGYOU_DATE");
                            // 運転者
                            dataTableTmp.Columns.Add("UNTENSHA");
                            // 種別
                            dataTableTmp.Columns.Add("SYUBETU");

                            if (isPrintH)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // 作業日
                                rowTmp["SAGYOU_DATE"] = "2013年11月28日(木)";
                                // 運転者
                                rowTmp["UNTENSHA"] = "あいうえおかきくけこさしすせそたちつてと";
                                // 識別
                                rowTmp["SYUBETU"] = "収集";

                                dataTableTmp.Rows.Add(rowTmp);
                            }

                            key = ((int)outputTypeDef).ToString() + "_20131128_001";
                            this.DataTablePageList[key] = new Dictionary<string, DataTable>();
                            this.DataTablePageList[key].Add("Header", dataTableTmp);

                            #endregion - Header -

                            #region - Detail -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Detail";

                            // №
                            dataTableTmp.Columns.Add("ROW");
                            // マニ情報
                            dataTableTmp.Columns.Add("MANIFEST_SHURUI_NAME");
                            // マニ手配
                            dataTableTmp.Columns.Add("MANIFEST_TEHAI_NAME");
                            // 受付番号
                            dataTableTmp.Columns.Add("UKETSUKE_NUMBER");
                            // 現着時間名
                            dataTableTmp.Columns.Add("GENCHAKU_TIME_NAME");
                            // 現着時間
                            dataTableTmp.Columns.Add("GENCHAKU_TIME");
                            // 作業時間
                            dataTableTmp.Columns.Add("SAGYOU_TIME");
                            // 業者CD
                            dataTableTmp.Columns.Add("GYOUSHA_CD");
                            // 業者名
                            dataTableTmp.Columns.Add("GYOUSHA_NAME");
                            // 車種CD
                            dataTableTmp.Columns.Add("SHASHU_CD");
                            // 車種名
                            dataTableTmp.Columns.Add("SHASHU_NAME");
                            // 車輌CD
                            dataTableTmp.Columns.Add("SHARYOU_CD");
                            // 車輌名
                            dataTableTmp.Columns.Add("SHARYOU_NAME");
                            // 現場CD
                            dataTableTmp.Columns.Add("GENBA_CD");
                            // 現場名
                            dataTableTmp.Columns.Add("GENBA_NAME");
                            // 荷降先・荷積先CD
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_CD");
                            // 荷降先・荷積先
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_NAME");
                            // 現場住所
                            dataTableTmp.Columns.Add("GENBA_ADDRESS");
                            // 荷降先住所・荷積先住所
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_ADDRESS");
                            // 現場電話番号
                            dataTableTmp.Columns.Add("GENBA_TEL");
                            // 担当者名
                            dataTableTmp.Columns.Add("TANTOUSHA");
                            // 担当者携帯番号
                            dataTableTmp.Columns.Add("GENBA_KEITAI_TEL");
                            // 受付備考１
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU1");
                            // 受付備考２
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU2");
                            // 受付備考３
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU3");
                            //// 受付備考４
                            //dataTableTmp.Columns.Add("UKETSUKE_BIKOU4");
                            // 運転者指示事項１
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU1");
                            // 運転者指示事項２
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU2");
                            // 運転者指示事項３
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU3");
                            //// 運転者指示事項４
                            //dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU4");
                            // コンテナ種類CD
                            dataTableTmp.Columns.Add("CONTENA_SHURUI_CD");
                            // コンテナ種類
                            dataTableTmp.Columns.Add("CONTENA_SHURUI_NAME");
                            // 設置・引揚
                            dataTableTmp.Columns.Add("CONTENA_JOUKYOU_NAME");
                            // 台数
                            dataTableTmp.Columns.Add("DAISUU");
                            // 品名CD
                            dataTableTmp.Columns.Add("HINMEI_CD");
                            // 品名
                            dataTableTmp.Columns.Add("HINMEI_NAME");
                            // 数量
                            dataTableTmp.Columns.Add("SUURYOU");
                            // 単位
                            dataTableTmp.Columns.Add("UNIT_NAME");

                            if (isPrint)
                            {
                                for (int i = 0; i < 5; i++)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        rowTmp = dataTableTmp.NewRow();

                                        // №
                                        rowTmp["ROW"] = ((i * 1000) + k).ToString();
                                        // マニ情報
                                        rowTmp["MANIFEST_SHURUI_NAME"] = "あいうえお";
                                        // マニ手配
                                        rowTmp["MANIFEST_TEHAI_NAME"] = "あいうえお";
                                        // 受付番号
                                        rowTmp["UKETSUKE_NUMBER"] = (i * 10000000).ToString();
                                        // 現着時間名
                                        rowTmp["GENCHAKU_TIME_NAME"] = "あいうえお";
                                        // 現着時間
                                        rowTmp["GENCHAKU_TIME"] = "12時00分";
                                        // 作業時間
                                        rowTmp["SAGYOU_TIME"] = "99分";
                                        // 業者CD
                                        rowTmp["GYOUSHA_CD"] = "1234567890";
                                        // 業者名
                                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 車種CD
                                        rowTmp["SHASHU_CD"] = "1234567890";
                                        // 車種名
                                        rowTmp["SHASHU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 車輌CD
                                        rowTmp["SHARYOU_CD"] = "1234567890";
                                        // 車輌名
                                        rowTmp["SHARYOU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 現場CD
                                        rowTmp["GENBA_CD"] = "1234567890";
                                        // 現場名
                                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 荷降先・荷積先CD
                                        rowTmp["NIOROSHI_NIZUMI_CD"] = "1234567890";
                                        // 荷降先・荷積先
                                        rowTmp["NIOROSHI_NIZUMI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 現場住所
                                        rowTmp["GENBA_ADDRESS"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 荷降先住所・荷積先住所
                                        rowTmp["NIOROSHI_NIZUMI_ADDRESS"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 現場電話番号
                                        rowTmp["GENBA_TEL"] = "123-1234-1234";
                                        // 担当者名
                                        rowTmp["TANTOUSHA"] = "あいうえおかきくけこさしすせそ";
                                        // 担当者携帯番号
                                        rowTmp["GENBA_KEITAI_TEL"] = "123-1234-1234";
                                        // 受付備考１
                                        rowTmp["UKETSUKE_BIKOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 受付備考２
                                        rowTmp["UKETSUKE_BIKOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 受付備考３
                                        rowTmp["UKETSUKE_BIKOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        //// 受付備考４
                                        //rowTmp["UKETSUKE_BIKOU4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項１
                                        rowTmp["UNTENSHA_SIJIJIKOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項２
                                        rowTmp["UNTENSHA_SIJIJIKOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項３
                                        rowTmp["UNTENSHA_SIJIJIKOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        //// 運転者指示事項４
                                        //rowTmp["UNTENSHA_SIJIJIKOU4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // コンテナ種類CD
                                        rowTmp["CONTENA_SHURUI_CD"] = "1234567890";
                                        // コンテナ種類
                                        rowTmp["CONTENA_SHURUI_NAME"] = "あいうえおかきくけこさしすせそ";
                                        // 設置・引揚
                                        rowTmp["CONTENA_JOUKYOU_NAME"] = "あいうえおかきくけこさしすせそ";
                                        // 台数
                                        rowTmp["DAISUU"] = "1234567890";
                                        // 品名CD
                                        rowTmp["HINMEI_CD"] = "1234567890";
                                        // 品名
                                        rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそ";
                                        // 数量
                                        rowTmp["SUURYOU"] = "123,456,789,000,123,456";
                                        // 単位
                                        rowTmp["UNIT_NAME"] = "あいうえお";

                                        dataTableTmp.Rows.Add(rowTmp);
                                    }
                                }
                            }

                            this.DataTablePageList[key].Add("Detail", dataTableTmp);

                            #endregion - Detail -

                            break;
                        case OutputTypeDef.Shipment:       // 出荷

                            #region - Header -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Header";

                            // 作業日
                            dataTableTmp.Columns.Add("SAGYOU_DATE");
                            // 運転者
                            dataTableTmp.Columns.Add("UNTENSHA");
                            // 種別
                            dataTableTmp.Columns.Add("SYUBETU");

                            if (isPrintH)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // 作業日
                                rowTmp["SAGYOU_DATE"] = "2013年11月28日(木)";
                                // 運転者
                                rowTmp["UNTENSHA"] = "あいうえおかきくけこさしすせそたちつてと";
                                // 識別
                                rowTmp["SYUBETU"] = "出荷";

                                dataTableTmp.Rows.Add(rowTmp);
                            }

                            key = ((int)outputTypeDef).ToString() + "_20131128_002";
                            this.DataTablePageList[key] = new Dictionary<string, DataTable>();
                            this.DataTablePageList[key].Add("Header", dataTableTmp);

                            #endregion - Header -

                            #region - Detail -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Detail";

                            // №
                            dataTableTmp.Columns.Add("ROW");
                            // マニ情報
                            dataTableTmp.Columns.Add("MANIFEST_SHURUI_NAME");
                            // マニ手配
                            dataTableTmp.Columns.Add("MANIFEST_TEHAI_NAME");
                            // 受付番号
                            dataTableTmp.Columns.Add("UKETSUKE_NUMBER");
                            // 現着時間名
                            dataTableTmp.Columns.Add("GENCHAKU_TIME_NAME");
                            // 現着時間
                            dataTableTmp.Columns.Add("GENCHAKU_TIME");
                            // 作業時間
                            dataTableTmp.Columns.Add("SAGYOU_TIME");
                            // 業者CD
                            dataTableTmp.Columns.Add("GYOUSHA_CD");
                            // 業者名
                            dataTableTmp.Columns.Add("GYOUSHA_NAME");
                            // 車種CD
                            dataTableTmp.Columns.Add("SHASHU_CD");
                            // 車種名
                            dataTableTmp.Columns.Add("SHASHU_NAME");
                            // 車輌CD
                            dataTableTmp.Columns.Add("SHARYOU_CD");
                            // 車輌名
                            dataTableTmp.Columns.Add("SHARYOU_NAME");
                            // 現場CD
                            dataTableTmp.Columns.Add("GENBA_CD");
                            // 現場名
                            dataTableTmp.Columns.Add("GENBA_NAME");
                            // 荷降先・荷積先CD
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_CD");
                            // 荷降先・荷積先
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_NAME");
                            // 現場住所
                            dataTableTmp.Columns.Add("GENBA_ADDRESS");
                            // 荷降先住所・荷積先住所
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_ADDRESS");
                            // 現場電話番号
                            dataTableTmp.Columns.Add("GENBA_TEL");
                            // 担当者名
                            dataTableTmp.Columns.Add("TANTOUSHA");
                            // 担当者携帯番号
                            dataTableTmp.Columns.Add("GENBA_KEITAI_TEL");
                            // 受付備考１
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU1");
                            // 受付備考２
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU2");
                            // 受付備考３
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU3");
                            //// 受付備考４
                            //dataTableTmp.Columns.Add("UKETSUKE_BIKOU4");
                            // 運転者指示事項１
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU1");
                            // 運転者指示事項２
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU2");
                            // 運転者指示事項３
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU3");
                            //// 運転者指示事項４
                            //dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU4");
                            // コンテナ種類CD
                            dataTableTmp.Columns.Add("CONTENA_SHURUI_CD");
                            // コンテナ種類
                            dataTableTmp.Columns.Add("CONTENA_SHURUI_NAME");
                            // 設置・引揚
                            dataTableTmp.Columns.Add("CONTENA_JOUKYOU_NAME");
                            // 台数
                            dataTableTmp.Columns.Add("DAISUU");
                            // 品名CD
                            dataTableTmp.Columns.Add("HINMEI_CD");
                            // 品名
                            dataTableTmp.Columns.Add("HINMEI_NAME");
                            // 数量
                            dataTableTmp.Columns.Add("SUURYOU");
                            // 単位
                            dataTableTmp.Columns.Add("UNIT_NAME");

                            if (isPrint)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        rowTmp = dataTableTmp.NewRow();

                                        // №
                                        rowTmp["ROW"] = ((i * 1000) + k).ToString();
                                        // マニ情報
                                        rowTmp["MANIFEST_SHURUI_NAME"] = "あいうえお";
                                        // マニ手配
                                        rowTmp["MANIFEST_TEHAI_NAME"] = "あいうえお";
                                        // 受付番号
                                        rowTmp["UKETSUKE_NUMBER"] = (i * 10000000).ToString();
                                        // 現着時間名
                                        rowTmp["GENCHAKU_TIME_NAME"] = "あいうえお";
                                        // 現着時間
                                        rowTmp["GENCHAKU_TIME"] = "12時00分";
                                        // 作業時間
                                        rowTmp["SAGYOU_TIME"] = "99分";
                                        // 業者CD
                                        rowTmp["GYOUSHA_CD"] = "1234567890";
                                        // 業者名
                                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 車種CD
                                        rowTmp["SHASHU_CD"] = "1234567890";
                                        // 車種名
                                        rowTmp["SHASHU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 車輌CD
                                        rowTmp["SHARYOU_CD"] = "1234567890";
                                        // 車輌名
                                        rowTmp["SHARYOU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 現場CD
                                        rowTmp["GENBA_CD"] = "1234567890";
                                        // 現場名
                                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 荷降先・荷積先CD
                                        rowTmp["NIOROSHI_NIZUMI_CD"] = "1234567890";
                                        // 荷降先・荷積先
                                        rowTmp["NIOROSHI_NIZUMI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 現場住所
                                        rowTmp["GENBA_ADDRESS"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 荷降先住所・荷積先住所
                                        rowTmp["NIOROSHI_NIZUMI_ADDRESS"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 現場電話番号
                                        rowTmp["GENBA_TEL"] = "123-1234-1234";
                                        // 担当者名
                                        rowTmp["TANTOUSHA"] = "あいうえおかきくけこさしすせそ";
                                        // 担当者携帯番号
                                        rowTmp["GENBA_KEITAI_TEL"] = "123-1234-1234";
                                        // 受付備考１
                                        rowTmp["UKETSUKE_BIKOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 受付備考２
                                        rowTmp["UKETSUKE_BIKOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 受付備考３
                                        rowTmp["UKETSUKE_BIKOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        //// 受付備考４
                                        //rowTmp["UKETSUKE_BIKOU4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項１
                                        rowTmp["UNTENSHA_SIJIJIKOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項２
                                        rowTmp["UNTENSHA_SIJIJIKOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項３
                                        rowTmp["UNTENSHA_SIJIJIKOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        //// 運転者指示事項４
                                        //rowTmp["UNTENSHA_SIJIJIKOU4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // コンテナ種類CD
                                        rowTmp["CONTENA_SHURUI_CD"] = string.Empty;
                                        // コンテナ種類
                                        rowTmp["CONTENA_SHURUI_NAME"] = string.Empty;
                                        // 設置・引揚
                                        rowTmp["CONTENA_JOUKYOU_NAME"] = string.Empty;
                                        // 台数
                                        rowTmp["DAISUU"] = string.Empty;
                                        // 品名CD
                                        rowTmp["HINMEI_CD"] = "1234567890";
                                        // 品名
                                        rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそ";
                                        // 数量
                                        rowTmp["SUURYOU"] = "123,456,789,000,123,456";
                                        // 単位
                                        rowTmp["UNIT_NAME"] = "あいうえお";

                                        dataTableTmp.Rows.Add(rowTmp);
                                    }
                                }
                            }

                            this.DataTablePageList[key].Add("Detail", dataTableTmp);

                            #endregion - Detail -

                            break;
                        case OutputTypeDef.Sale:           // 物販

                            #region - Header -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Header";

                            // 作業日
                            dataTableTmp.Columns.Add("SAGYOU_DATE");
                            // 運転者
                            dataTableTmp.Columns.Add("UNTENSHA");
                            // 種別
                            dataTableTmp.Columns.Add("SYUBETU");

                            if (isPrintH)
                            {
                                rowTmp = dataTableTmp.NewRow();

                                // 作業日
                                rowTmp["SAGYOU_DATE"] = "2013年11月28日(木)";
                                // 運転者
                                rowTmp["UNTENSHA"] = "あいうえおかきくけこさしすせそたちつてと";
                                // 識別
                                rowTmp["SYUBETU"] = "物販";

                                dataTableTmp.Rows.Add(rowTmp);
                            }

                            key = ((int)outputTypeDef).ToString() + "_20131128_003";
                            this.DataTablePageList[key] = new Dictionary<string, DataTable>();
                            this.DataTablePageList[key].Add("Header", dataTableTmp);

                            #endregion - Header -

                            #region - Detail -

                            dataTableTmp = new DataTable();
                            dataTableTmp.TableName = "Detail";

                            // №
                            dataTableTmp.Columns.Add("ROW");
                            // マニ情報
                            dataTableTmp.Columns.Add("MANIFEST_SHURUI_NAME");
                            // マニ手配
                            dataTableTmp.Columns.Add("MANIFEST_TEHAI_NAME");
                            // 受付番号
                            dataTableTmp.Columns.Add("UKETSUKE_NUMBER");
                            // 現着時間名
                            dataTableTmp.Columns.Add("GENCHAKU_TIME_NAME");
                            // 現着時間
                            dataTableTmp.Columns.Add("GENCHAKU_TIME");
                            // 作業時間
                            dataTableTmp.Columns.Add("SAGYOU_TIME");
                            // 業者CD
                            dataTableTmp.Columns.Add("GYOUSHA_CD");
                            // 業者名
                            dataTableTmp.Columns.Add("GYOUSHA_NAME");
                            // 車種CD
                            dataTableTmp.Columns.Add("SHASHU_CD");
                            // 車種名
                            dataTableTmp.Columns.Add("SHASHU_NAME");
                            // 車輌CD
                            dataTableTmp.Columns.Add("SHARYOU_CD");
                            // 車輌名
                            dataTableTmp.Columns.Add("SHARYOU_NAME");
                            // 現場CD
                            dataTableTmp.Columns.Add("GENBA_CD");
                            // 現場名
                            dataTableTmp.Columns.Add("GENBA_NAME");
                            // 荷降先・荷積先CD
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_CD");
                            // 荷降先・荷積先
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_NAME");
                            // 現場住所
                            dataTableTmp.Columns.Add("GENBA_ADDRESS");
                            // 荷降先住所・荷積先住所
                            dataTableTmp.Columns.Add("NIOROSHI_NIZUMI_ADDRESS");
                            // 現場電話番号
                            dataTableTmp.Columns.Add("GENBA_TEL");
                            // 担当者名
                            dataTableTmp.Columns.Add("TANTOUSHA");
                            // 担当者携帯番号
                            dataTableTmp.Columns.Add("GENBA_KEITAI_TEL");
                            // 受付備考１
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU1");
                            // 受付備考２
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU2");
                            // 受付備考３
                            dataTableTmp.Columns.Add("UKETSUKE_BIKOU3");
                            //// 受付備考４
                            //dataTableTmp.Columns.Add("UKETSUKE_BIKOU4");
                            // 運転者指示事項１
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU1");
                            // 運転者指示事項２
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU2");
                            // 運転者指示事項３
                            dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU3");
                            //// 運転者指示事項４
                            //dataTableTmp.Columns.Add("UNTENSHA_SIJIJIKOU4");
                            // コンテナ種類CD
                            dataTableTmp.Columns.Add("CONTENA_SHURUI_CD");
                            // コンテナ種類
                            dataTableTmp.Columns.Add("CONTENA_SHURUI_NAME");
                            // 設置・引揚
                            dataTableTmp.Columns.Add("CONTENA_JOUKYOU_NAME");
                            // 台数
                            dataTableTmp.Columns.Add("DAISUU");
                            // 品名CD
                            dataTableTmp.Columns.Add("HINMEI_CD");
                            // 品名
                            dataTableTmp.Columns.Add("HINMEI_NAME");
                            // 数量
                            dataTableTmp.Columns.Add("SUURYOU");
                            // 単位
                            dataTableTmp.Columns.Add("UNIT_NAME");

                            if (isPrint)
                            {
                                for (int i = 0; i < 31; i++)
                                {
                                    for (int k = 0; k < 3; k++)
                                    {
                                        rowTmp = dataTableTmp.NewRow();

                                        // №
                                        rowTmp["ROW"] = ((i * 1000) + k).ToString();
                                        // マニ情報
                                        rowTmp["MANIFEST_SHURUI_NAME"] = "あいうえお";
                                        // マニ手配
                                        rowTmp["MANIFEST_TEHAI_NAME"] = "あいうえお";
                                        // 受付番号
                                        rowTmp["UKETSUKE_NUMBER"] = (i * 10000000).ToString();
                                        // 現着時間名
                                        rowTmp["GENCHAKU_TIME_NAME"] = "あいうえお";
                                        // 現着時間
                                        rowTmp["GENCHAKU_TIME"] = "12時00分";
                                        // 作業時間
                                        rowTmp["SAGYOU_TIME"] = "99分";
                                        // 業者CD
                                        rowTmp["GYOUSHA_CD"] = "1234567890";
                                        // 業者名
                                        rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 車種CD
                                        rowTmp["SHASHU_CD"] = "1234567890";
                                        // 車種名
                                        rowTmp["SHASHU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 車輌CD
                                        rowTmp["SHARYOU_CD"] = "1234567890";
                                        // 車輌名
                                        rowTmp["SHARYOU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 現場CD
                                        rowTmp["GENBA_CD"] = "1234567890";
                                        // 現場名
                                        rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 荷降先・荷積先CD
                                        rowTmp["NIOROSHI_NIZUMI_CD"] = string.Empty;
                                        // 荷降先・荷積先
                                        rowTmp["NIOROSHI_NIZUMI_NAME"] = string.Empty;
                                        // 現場住所
                                        rowTmp["GENBA_ADDRESS"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 荷降先住所・荷積先住所
                                        rowTmp["NIOROSHI_NIZUMI_ADDRESS"] = string.Empty;
                                        // 現場電話番号
                                        rowTmp["GENBA_TEL"] = "123-1234-1234";
                                        // 担当者名
                                        rowTmp["TANTOUSHA"] = "あいうえおかきくけこさしすせそ";
                                        // 担当者携帯番号
                                        rowTmp["GENBA_KEITAI_TEL"] = "123-1234-1234";
                                        // 受付備考１
                                        rowTmp["UKETSUKE_BIKOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 受付備考２
                                        rowTmp["UKETSUKE_BIKOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 受付備考３
                                        rowTmp["UKETSUKE_BIKOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        //// 受付備考４
                                        //rowTmp["UKETSUKE_BIKOU4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項１
                                        rowTmp["UNTENSHA_SIJIJIKOU1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項２
                                        rowTmp["UNTENSHA_SIJIJIKOU2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // 運転者指示事項３
                                        rowTmp["UNTENSHA_SIJIJIKOU3"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        //// 運転者指示事項４
                                        //rowTmp["UNTENSHA_SIJIJIKOU4"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                                        // コンテナ種類CD
                                        rowTmp["CONTENA_SHURUI_CD"] = string.Empty;
                                        // コンテナ種類
                                        rowTmp["CONTENA_SHURUI_NAME"] = string.Empty;
                                        // 設置・引揚
                                        rowTmp["CONTENA_JOUKYOU_NAME"] = string.Empty;
                                        // 台数
                                        rowTmp["DAISUU"] = string.Empty;
                                        // 品名CD
                                        rowTmp["HINMEI_CD"] = "1234567890";
                                        // 品名
                                        rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそ";
                                        // 数量
                                        rowTmp["SUURYOU"] = "123,456,789,000,123,456";
                                        // 単位
                                        rowTmp["UNIT_NAME"] = "あいうえお";

                                        dataTableTmp.Rows.Add(rowTmp);
                                    }
                                }
                            }

                            this.DataTablePageList[key].Add("Detail", dataTableTmp);

                            #endregion - Detail -

                            break;
                    }
                }
            }
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int i;
            DataRow row;
            DataTable dataTableHeaderTmp = null;
            DataTable dataTableDetailTmp = null;
            string ctrlName = string.Empty;

            bool detailComp = false;
            int detailMaxCount = 0;
            int detailStart = 0;
            int rowNo;
            string tmp;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region Columns
            // 作業日
            ctrlName = "PHN_SAGYOU_DATE_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運転者
            ctrlName = "PHN_UNTENSHA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 種別
            ctrlName = "PHN_SYUBETU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // マニ情報カラム
            ctrlName = "PHN_H_MANIFEST_SHURUI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // マニ手配カラム
            ctrlName = "PHN_H_MANIFEST_TEHAI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現着時間カラム
            ctrlName = "PHN_H_GENCHAKU_TIME_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 作業時間カラム
            ctrlName = "PHN_H_SAGYOU_TIME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降先・荷積先カラム
            ctrlName = "PHN_NIOROSHI_NIZUMI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降先住所・荷積先住所カラム
            ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // コンテナ種類カラム
            ctrlName = "PHN_CONTENA_SHURUI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 設置・引揚カラム
            ctrlName = "PHN_CONTENA_JOUKYOU_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 台数カラム
            ctrlName = "PHN_DAISUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // №
            ctrlName = "PHN_ROW_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // マニ情報
            ctrlName = "PHN_MANIFEST_SHURUI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // マニ手配
            ctrlName = "PHN_MANIFEST_TEHAI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受付番号
            ctrlName = "PHN_UKETSUKE_NUMBER_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現着時間名
            ctrlName = "PHN_GENCHAKU_TIME_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現着時間
            ctrlName = "PHN_GENCHAKU_TIME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 作業時間
            ctrlName = "PHN_SAGYOU_TIME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 業者CD
            ctrlName = "PHN_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 業者名
            ctrlName = "PHN_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車種CD
            ctrlName = "PHN_SHASHU_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車種名
            ctrlName = "PHN_SHASHU_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車輌CD
            ctrlName = "PHN_SHARYOU_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車輌名
            ctrlName = "PHN_SHARYOU_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場CD
            ctrlName = "PHN_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場名
            ctrlName = "PHN_GENBA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降先・荷積先CD
            ctrlName = "PHN_NIOROSHI_NIZUMI_CD_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降先・荷積先
            ctrlName = "PHN_NIOROSHI_NIZUMI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場住所
            ctrlName = "PHN_GENBA_ADDRESS_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降先住所・荷積先住所
            ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場電話番号
            ctrlName = "PHN_GENBA_TEL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 担当者名
            ctrlName = "PHN_TANTOUSHA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 担当者携帯番号
            ctrlName = "PHN_GENBA_KEITAI_TEL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受付備考１
            ctrlName = "PHN_UKETSUKE_BIKOU1_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受付備考２
            ctrlName = "PHN_UKETSUKE_BIKOU2_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受付備考３
            ctrlName = "PHN_UKETSUKE_BIKOU3_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受付備考４
            ctrlName = "PHN_UKETSUKE_BIKOU4_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運転者指示事項１
            ctrlName = "PHN_UNTENSHA_SIJIJIKOU1_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運転者指示事項２
            ctrlName = "PHN_UNTENSHA_SIJIJIKOU2_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運転者指示事項３
            ctrlName = "PHN_UNTENSHA_SIJIJIKOU3_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運転者指示事項４
            ctrlName = "PHN_UNTENSHA_SIJIJIKOU4_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // コンテナ種類CD
            ctrlName = "PHN_CONTENA_SHURUI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // コンテナ種類
            ctrlName = "PHN_CONTENA_SHURUI_NAME_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 設置・引揚
            ctrlName = "PHN_CONTENA_JOUKYOU_NAME_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 台数
            ctrlName = "PHN_DAISUU_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 品名CD
            ctrlName = "PHN_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 品名
            ctrlName = "PHN_HINMEI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 数量
            ctrlName = "PHN_SUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 単位
            ctrlName = "PHN_UNIT_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            #endregion - Columns -

            var varKey = DataTablePageList.Keys;
            foreach (string strKey in varKey)
            {
                string[] strKeys = strKey.Split(Convert.ToChar("_"));
                int intKeys = int.Parse(strKeys[0]);
                string strUketukeNo = string.Empty;

                dataTableHeaderTmp = this.DataTablePageList[strKey]["Header"];
                dataTableDetailTmp = this.DataTablePageList[strKey]["Detail"];
                detailMaxCount = dataTableDetailTmp.Rows.Count;
                detailComp = false;
                rowNo = 1;

                int loopCount = (int)Math.Ceiling((decimal)dataTableDetailTmp.Rows.Count / ConstMaxDispDetailRowCount) * ConstMaxDispDetailRowCount;

                if (loopCount == 0)
                {
                    loopCount = ConstMaxDispDetailRowCount;
                    detailComp = true;
                }

                for (i = detailStart; i < loopCount; i++)
                {
                    row = this.dataTable.NewRow();

                    #region - Header -

                    switch (intKeys)
                    {
                        case (int)OutputTypeDef.Collection: // 収集

                            #region - 収集 -
                            
                            // マニ情報カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_SHURUI_NAME");
                            ctrlName = "PHN_H_MANIFEST_SHURUI_NAME_FLB";
                            row[ctrlName] = "マニ情報";
                            // マニ手配カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_TEHAI_NAME");
                            ctrlName = "PHN_H_MANIFEST_TEHAI_NAME_FLB";
                            row[ctrlName] = "マニ手配";
                            // 到着時間カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_TIME");
                            ctrlName = "PHN_H_GENCHAKU_TIME_NAME_FLB";
                            row[ctrlName] = "現着時間";
                            // 作業時間カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_TIME");
                            ctrlName = "PHN_H_SAGYOU_TIME_FLB";
                            row[ctrlName] = "作業時間";
                            // 荷降先・荷積先カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_NIZUMI_CD");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_CD_FLB";
                            row[ctrlName] = "荷降先";
                            // 荷降先住所・荷積先住所カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_NIZUMI_ADDRESS");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_FLB";
                            row[ctrlName] = "荷降先住所";
                            // コンテナ種類カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("CONTENA_SHURUI");
                            ctrlName = "PHN_CONTENA_SHURUI_FLB";
                            row[ctrlName] = "コンテナ種類";
                            // 設置・引揚カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("CONTENA_JOUKYOU_NAME");
                            ctrlName = "PHN_CONTENA_JOUKYOU_NAME_FLB";
                            row[ctrlName] = "設置・引揚";
                            // 台数カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("DAISUU");
                            ctrlName = "PHN_DAISUU_FLB";
                            row[ctrlName] = "台数";

                            #endregion - 収集 -

                            break;
                        case (int)OutputTypeDef.Shipment:   // 出荷

                            #region - 出荷 -

                            // マニ情報カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_SHURUI_NAME");
                            ctrlName = "PHN_H_MANIFEST_SHURUI_NAME_FLB";
                            row[ctrlName] = "マニ情報";
                            // マニ手配カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_TEHAI_NAME");
                            ctrlName = "PHN_H_MANIFEST_TEHAI_NAME_FLB";
                            row[ctrlName] = "マニ手配";
                            // 到着時間カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_TIME");
                            ctrlName = "PHN_H_GENCHAKU_TIME_NAME_FLB";
                            row[ctrlName] = "現着時間";
                            // 作業時間カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_TIME");
                            ctrlName = "PHN_H_SAGYOU_TIME_FLB";
                            row[ctrlName] = "作業時間";
                            // 荷降先・荷積先カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_NIZUMI_CD");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_CD_FLB";
                            row[ctrlName] = "荷積先";
                            // 荷降先住所・荷積先住所カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_NIZUMI_ADDRESS");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_FLB";
                            row[ctrlName] = "荷積先住所";
                            // コンテナ種類カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("CONTENA_SHURUI");
                            ctrlName = "PHN_CONTENA_SHURUI_FLB";
                            row[ctrlName] = "コンテナ種類";
                            // 設置・引揚カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("CONTENA_JOUKYOU_NAME");
                            ctrlName = "PHN_CONTENA_JOUKYOU_NAME_FLB";
                            row[ctrlName] = "設置・引揚";
                            // 台数カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("DAISUU");
                            ctrlName = "PHN_DAISUU_FLB";
                            row[ctrlName] = "台数";

                            #endregion - 出荷 -

                            break;
                        case (int)OutputTypeDef.Sale:

                            #region - 物販 -

                            // マニ情報カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_SHURUI_NAME");
                            ctrlName = "PHN_H_MANIFEST_SHURUI_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // マニ手配カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("MANIFEST_TEHAI_NAME");
                            ctrlName = "PHN_H_MANIFEST_TEHAI_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 到着時間カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("GENCHAKU_TIME");
                            ctrlName = "PHN_H_GENCHAKU_TIME_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 作業時間カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_TIME");
                            ctrlName = "PHN_H_SAGYOU_TIME_FLB";
                            row[ctrlName] = string.Empty;
                            // 荷降先・荷積先カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_NIZUMI_CD");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 荷降先住所・荷積先住所カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_NIZUMI_ADDRESS");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_FLB";
                            row[ctrlName] = string.Empty;
                            // コンテナ種類カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("CONTENA_SHURUI");
                            ctrlName = "PHN_CONTENA_SHURUI_FLB";
                            row[ctrlName] = string.Empty;
                            // 設置・引揚カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("CONTENA_JOUKYOU_NAME");
                            ctrlName = "PHN_CONTENA_JOUKYOU_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 台数カラム
                            index = dataTableHeaderTmp.Columns.IndexOf("DAISUU");
                            ctrlName = "PHN_DAISUU_FLB";
                            row[ctrlName] = string.Empty;

                            #endregion - 物販 -

                            break;
                    }

                    if (dataTableHeaderTmp.Rows.Count > 0)
                    {
                        // 作業日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_DATE");
                        ctrlName = "PHN_SAGYOU_DATE_FLB";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 運転者
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA");
                        ctrlName = "PHN_UNTENSHA_FLB";
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

                        // 種別
                        index = dataTableHeaderTmp.Columns.IndexOf("SYUBETU");
                        ctrlName = "PHN_SYUBETU_FLB";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }
                    }
                    else
                    {
                        // 作業日
                        index = dataTableHeaderTmp.Columns.IndexOf("SAGYOU_DATE");
                        ctrlName = "PHN_SAGYOU_DATE_FLB";
                        row[ctrlName] = string.Empty;

                        // 運転者
                        index = dataTableHeaderTmp.Columns.IndexOf("UNTENSHA");
                        ctrlName = "PHN_UNTENSHA_FLB";
                        row[ctrlName] = string.Empty;
                        
                        // 種別
                        index = dataTableHeaderTmp.Columns.IndexOf("SYUBETU");
                        ctrlName = "PHN_SYUBETU_FLB";
                        row[ctrlName] = string.Empty;
                    }

                    #endregion - Header -

                    #region - Detail -

                    if (!detailComp)
                    {
                        // №
                        index = dataTableDetailTmp.Columns.IndexOf("ROW");
                        ctrlName = "PHN_ROW_FLB";
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 受付番号
                        index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_NUMBER");
                        ctrlName = "PHN_UKETSUKE_NUMBER_FLB";
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            tmp = string.Empty;
                        }

                        if (strUketukeNo != tmp)
                        {
                            strUketukeNo = tmp;

                            // 受付番号
                            row[ctrlName] = tmp;

                            // マニ情報
                            index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_SHURUI_NAME");
                            ctrlName = "PHN_MANIFEST_SHURUI_NAME_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // マニ手配
                            index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_TEHAI_NAME");
                            ctrlName = "PHN_MANIFEST_TEHAI_NAME_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現着時間名
                            index = dataTableDetailTmp.Columns.IndexOf("GENCHAKU_TIME_NAME");
                            ctrlName = "PHN_GENCHAKU_TIME_NAME_FLB";
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
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現着時間
                            index = dataTableDetailTmp.Columns.IndexOf("GENCHAKU_TIME");
                            ctrlName = "PHN_GENCHAKU_TIME_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 作業時間
                            index = dataTableDetailTmp.Columns.IndexOf("SAGYOU_TIME");
                            ctrlName = "PHN_SAGYOU_TIME_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 10);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 業者CD
                            index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHN_GYOUSHA_CD_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 業者名
                            index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = "PHN_GYOUSHA_NAME_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 車種CD
                            index = dataTableDetailTmp.Columns.IndexOf("SHASHU_CD");
                            ctrlName = "PHN_SHASHU_CD_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 車種名
                            index = dataTableDetailTmp.Columns.IndexOf("SHASHU_NAME");
                            ctrlName = "PHN_SHASHU_NAME_FLB";
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
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 車輌CD
                            index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_CD");
                            ctrlName = "PHN_SHARYOU_CD_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 車輌名
                            index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_NAME");
                            ctrlName = "PHN_SHARYOU_NAME_FLB";
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
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場CD
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHN_GENBA_CD_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場名
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = "PHN_GENBA_NAME_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降先・荷積先CD
                            index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_CD");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_CD_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降先・荷積先
                            index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_NAME");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_NAME_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場住所(住所1+改行コード+住所2)
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_ADDRESS");
                            ctrlName = "PHN_GENBA_ADDRESS_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 82)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 82);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 荷降先住所・荷積先住所(住所1+改行コード+住所2)
                            index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_ADDRESS");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 82)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 82);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場電話番号
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_TEL");
                            ctrlName = "PHN_GENBA_TEL_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 担当者名
                            index = dataTableDetailTmp.Columns.IndexOf("TANTOUSHA");
                            ctrlName = "PHN_TANTOUSHA_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 16)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 16);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 担当者携帯番号
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_KEITAI_TEL");
                            ctrlName = "PHN_GENBA_KEITAI_TEL_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付備考１
                            index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU1");
                            ctrlName = "PHN_UKETSUKE_BIKOU1_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付備考２
                            index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU2");
                            ctrlName = "PHN_UKETSUKE_BIKOU2_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 受付備考３
                            index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU3");
                            ctrlName = "PHN_UKETSUKE_BIKOU3_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            //// 受付備考４
                            //index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU4");
                            //ctrlName = "PHN_UKETSUKE_BIKOU4_FLB";
                            //btBytes = hEncoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                            //if (btBytes.Length > 40)
                            //{
                            //    row[ctrlName] = hEncoding.GetString(btBytes, 0, 40);
                            //}
                            //else
                            //{
                            //    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            //}

                            // 運転者指示事項１
                            index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU1");
                            ctrlName = "PHN_UNTENSHA_SIJIJIKOU1_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 運転者指示事項２
                            index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU2");
                            ctrlName = "PHN_UNTENSHA_SIJIJIKOU2_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 運転者指示事項３
                            index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU3");
                            ctrlName = "PHN_UNTENSHA_SIJIJIKOU3_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 40)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            //// 運転者指示事項４
                            //index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU4");
                            //ctrlName = "PHN_UNTENSHA_SIJIJIKOU4_FLB";
                            //btBytes = hEncoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                            //if (btBytes.Length > 40)
                            //{
                            //    row[ctrlName] = hEncoding.GetString(btBytes, 0, 40);
                            //}
                            //else
                            //{
                            //    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            //}

                            // コンテナ種類CD
                            index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_CD");
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                tmp = string.Empty;
                            }
                            ctrlName = "PHN_CONTENA_SHURUI_CD_FLB";
                            row[ctrlName] = tmp;

                            // コンテナ種類
                            index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_NAME");
                            ctrlName = "PHN_CONTENA_SHURUI_NAME_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 16)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 16);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 設置・引揚
                            index = dataTableDetailTmp.Columns.IndexOf("CONTENA_JOUKYOU_NAME");
                            ctrlName = "PHN_CONTENA_JOUKYOU_NAME_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 台数
                            index = dataTableDetailTmp.Columns.IndexOf("DAISUU");
                            ctrlName = "PHN_DAISUU_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }
                        }
                        else
                        {
                            // 受付番号
                            row[ctrlName] = string.Empty;
                            // マニ情報
                            index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_SHURUI_NAME");
                            ctrlName = "PHN_MANIFEST_SHURUI_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // マニ手配
                            index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_TEHAI_NAME");
                            ctrlName = "PHN_MANIFEST_TEHAI_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 現着時間名
                            index = dataTableDetailTmp.Columns.IndexOf("GENCHAKU_TIME_NAME");
                            ctrlName = "PHN_GENCHAKU_TIME_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 現着時間
                            index = dataTableDetailTmp.Columns.IndexOf("GENCHAKU_TIME");
                            ctrlName = "PHN_GENCHAKU_TIME_FLB";
                            row[ctrlName] = string.Empty;
                            // 作業時間
                            index = dataTableDetailTmp.Columns.IndexOf("SAGYOU_TIME");
                            ctrlName = "PHN_SAGYOU_TIME_FLB";
                            row[ctrlName] = string.Empty;
                            // 業者CD
                            index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHN_GYOUSHA_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 業者名
                            index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_NAME");
                            ctrlName = "PHN_GYOUSHA_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 車種CD
                            index = dataTableDetailTmp.Columns.IndexOf("SHASHU_CD");
                            ctrlName = "PHN_SHASHU_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 車種名
                            index = dataTableDetailTmp.Columns.IndexOf("SHASHU_NAME");
                            ctrlName = "PHN_SHASHU_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 車輌CD
                            index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_CD");
                            ctrlName = "PHN_SHARYOU_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 車輌名
                            index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_NAME");
                            ctrlName = "PHN_SHARYOU_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 現場CD
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHN_GENBA_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 現場名
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_NAME");
                            ctrlName = "PHN_GENBA_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 荷降先・荷積先CD
                            index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_CD");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_CD_VLB";
                            row[ctrlName] = string.Empty;
                            // 荷降先・荷積先
                            index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_NAME");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 現場住所
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_ADDRESS");
                            ctrlName = "PHN_GENBA_ADDRESS_FLB";
                            row[ctrlName] = string.Empty;
                            // 荷降先住所・荷積先住所
                            index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_ADDRESS");
                            ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_VLB";
                            row[ctrlName] = string.Empty;
                            // 現場電話番号
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_TEL");
                            ctrlName = "PHN_GENBA_TEL_FLB";
                            row[ctrlName] = string.Empty;
                            // 担当者名
                            index = dataTableDetailTmp.Columns.IndexOf("TANTOUSHA");
                            ctrlName = "PHN_TANTOUSHA_FLB";
                            row[ctrlName] = string.Empty;
                            // 担当者携帯番号
                            index = dataTableDetailTmp.Columns.IndexOf("GENBA_KEITAI_TEL");
                            ctrlName = "PHN_GENBA_KEITAI_TEL_FLB";
                            row[ctrlName] = string.Empty;
                            // 受付備考１
                            index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU1");
                            ctrlName = "PHN_UKETSUKE_BIKOU1_FLB";
                            row[ctrlName] = string.Empty;
                            // 受付備考２
                            index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU2");
                            ctrlName = "PHN_UKETSUKE_BIKOU2_FLB";
                            row[ctrlName] = string.Empty;
                            // 受付備考３
                            index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU3");
                            ctrlName = "PHN_UKETSUKE_BIKOU3_FLB";
                            row[ctrlName] = string.Empty;
                            //// 受付備考４
                            //index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU4");
                            //ctrlName = "PHN_UKETSUKE_BIKOU4_FLB";
                            //row[ctrlName] = string.Empty;
                            // 運転者指示事項１
                            index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU1");
                            ctrlName = "PHN_UNTENSHA_SIJIJIKOU1_FLB";
                            row[ctrlName] = string.Empty;
                            // 運転者指示事項２
                            index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU2");
                            ctrlName = "PHN_UNTENSHA_SIJIJIKOU2_FLB";
                            row[ctrlName] = string.Empty;
                            // 運転者指示事項３
                            index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU3");
                            ctrlName = "PHN_UNTENSHA_SIJIJIKOU3_FLB";
                            row[ctrlName] = string.Empty;
                            //// 運転者指示事項４
                            //index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU4");
                            //ctrlName = "PHN_UNTENSHA_SIJIJIKOU4_FLB";
                            //row[ctrlName] = string.Empty;

                            // コンテナ種類CD
                            index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_CD");
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                tmp = string.Empty;
                            }
                            ctrlName = "PHN_CONTENA_SHURUI_CD_FLB";
                            row[ctrlName] = tmp;

                            // コンテナ種類
                            index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_NAME");
                            ctrlName = "PHN_CONTENA_SHURUI_NAME_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                                if (byteArray.Length > 10)
                                {
                                    row[ctrlName] = encoding.GetString(byteArray, 0, 10);
                                }
                                else
                                {
                                    row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                                }
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 設置・引揚
                            index = dataTableDetailTmp.Columns.IndexOf("CONTENA_JOUKYOU_NAME");
                            ctrlName = "PHN_CONTENA_JOUKYOU_NAME_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 台数
                            index = dataTableDetailTmp.Columns.IndexOf("DAISUU");
                            ctrlName = "PHN_DAISUU_VLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }
                        }

                        // 品名CD
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                        ctrlName = "PHN_HINMEI_CD_FLB";
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 品名
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                        ctrlName = "PHN_HINMEI_NAME_FLB";
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
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 数量
                        index = dataTableDetailTmp.Columns.IndexOf("SUURYOU");
                        ctrlName = "PHN_SUURYOU_FLB";
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 単位
                        index = dataTableDetailTmp.Columns.IndexOf("UNIT_NAME");
                        ctrlName = "PHN_UNIT_NAME_FLB";
                        if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                            if (byteArray.Length > 6)
                            {
                                row[ctrlName] = encoding.GetString(byteArray, 0, 6);
                            }
                            else
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }
                    }
                    else
                    {
                        // №
                        index = dataTableDetailTmp.Columns.IndexOf("ROW");
                        ctrlName = "PHN_ROW_FLB";
                        row[ctrlName] = string.Empty;
                        // マニ情報
                        index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_SHURUI_NAME");
                        ctrlName = "PHN_MANIFEST_SHURUI_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // マニ手配
                        index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_TEHAI_NAME");
                        ctrlName = "PHN_MANIFEST_TEHAI_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 受付番号
                        index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_NUMBER");
                        ctrlName = "PHN_UKETSUKE_NUMBER_FLB";
                        row[ctrlName] = string.Empty;
                        // 現着時間名
                        index = dataTableDetailTmp.Columns.IndexOf("GENCHAKU_TIME_NAME");
                        ctrlName = "PHN_GENCHAKU_TIME_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 現着時間
                        index = dataTableDetailTmp.Columns.IndexOf("GENCHAKU_TIME");
                        ctrlName = "PHN_GENCHAKU_TIME_FLB";
                        row[ctrlName] = string.Empty;
                        // 作業時間
                        index = dataTableDetailTmp.Columns.IndexOf("SAGYOU_TIME");
                        ctrlName = "PHN_SAGYOU_TIME_FLB";
                        row[ctrlName] = string.Empty;
                        // 業者CD
                        index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_CD");
                        ctrlName = "PHN_GYOUSHA_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 業者名
                        index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_NAME");
                        ctrlName = "PHN_GYOUSHA_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 車種CD
                        index = dataTableDetailTmp.Columns.IndexOf("SHASHU_CD");
                        ctrlName = "PHN_SHASHU_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 車種名
                        index = dataTableDetailTmp.Columns.IndexOf("SHASHU_NAME");
                        ctrlName = "PHN_SHASHU_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 車輌CD
                        index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_CD");
                        ctrlName = "PHN_SHARYOU_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 車輌名
                        index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_NAME");
                        ctrlName = "PHN_SHARYOU_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 現場CD
                        index = dataTableDetailTmp.Columns.IndexOf("GENBA_CD");
                        ctrlName = "PHN_GENBA_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 現場名
                        index = dataTableDetailTmp.Columns.IndexOf("GENBA_NAME");
                        ctrlName = "PHN_GENBA_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 荷降先・荷積先CD
                        index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_CD");
                        ctrlName = "PHN_NIOROSHI_NIZUMI_CD_VLB";
                        row[ctrlName] = string.Empty;
                        // 荷降先・荷積先
                        index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_NAME");
                        ctrlName = "PHN_NIOROSHI_NIZUMI_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 現場住所
                        index = dataTableDetailTmp.Columns.IndexOf("GENBA_ADDRESS");
                        ctrlName = "PHN_GENBA_ADDRESS_FLB";
                        row[ctrlName] = string.Empty;
                        // 荷降先住所・荷積先住所
                        index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_NIZUMI_ADDRESS");
                        ctrlName = "PHN_NIOROSHI_NIZUMI_ADDRESS_VLB";
                        row[ctrlName] = string.Empty;
                        // 現場電話番号
                        index = dataTableDetailTmp.Columns.IndexOf("GENBA_TEL");
                        ctrlName = "PHN_GENBA_TEL_FLB";
                        row[ctrlName] = string.Empty;
                        // 担当者名
                        index = dataTableDetailTmp.Columns.IndexOf("TANTOUSHA");
                        ctrlName = "PHN_TANTOUSHA_FLB";
                        row[ctrlName] = string.Empty;
                        // 担当者携帯番号
                        index = dataTableDetailTmp.Columns.IndexOf("GENBA_KEITAI_TEL");
                        ctrlName = "PHN_GENBA_KEITAI_TEL_FLB";
                        row[ctrlName] = string.Empty;
                        // 受付備考１
                        index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU1");
                        ctrlName = "PHN_UKETSUKE_BIKOU1_FLB";
                        row[ctrlName] = string.Empty;
                        // 受付備考２
                        index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU2");
                        ctrlName = "PHN_UKETSUKE_BIKOU2_FLB";
                        row[ctrlName] = string.Empty;
                        // 受付備考３
                        index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU3");
                        ctrlName = "PHN_UKETSUKE_BIKOU3_FLB";
                        row[ctrlName] = string.Empty;
                        //// 受付備考４
                        //index = dataTableDetailTmp.Columns.IndexOf("UKETSUKE_BIKOU4");
                        //ctrlName = "PHN_UKETSUKE_BIKOU4_FLB";
                        //row[ctrlName] = string.Empty;
                        // 運転者指示事項１
                        index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU1");
                        ctrlName = "PHN_UNTENSHA_SIJIJIKOU1_FLB";
                        row[ctrlName] = string.Empty;
                        // 運転者指示事項２
                        index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU2");
                        ctrlName = "PHN_UNTENSHA_SIJIJIKOU2_FLB";
                        row[ctrlName] = string.Empty;
                        // 運転者指示事項３
                        index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU3");
                        ctrlName = "PHN_UNTENSHA_SIJIJIKOU3_FLB";
                        row[ctrlName] = string.Empty;
                        //// 運転者指示事項４
                        //index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_SIJIJIKOU4");
                        //ctrlName = "PHN_UNTENSHA_SIJIJIKOU4_FLB";
                        //row[ctrlName] = string.Empty;
                        // コンテナ種類CD
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_CD");
                        ctrlName = "PHN_CONTENA_SHURUI_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // コンテナ種類
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_SHURUI_NAME");
                        ctrlName = "PHN_CONTENA_SHURUI_NAME_VLB";
                        row[ctrlName] = string.Empty;
                        // 設置・引揚
                        index = dataTableDetailTmp.Columns.IndexOf("CONTENA_JOUKYOU_NAME");
                        ctrlName = "PHN_CONTENA_JOUKYOU_NAME_VLB";
                        row[ctrlName] = string.Empty;
                        // 台数
                        index = dataTableDetailTmp.Columns.IndexOf("DAISUU");
                        ctrlName = "PHN_DAISUU_VLB";
                        row[ctrlName] = string.Empty;
                        // 品名CD
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                        ctrlName = "PHN_HINMEI_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 品名
                        index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                        ctrlName = "PHN_HINMEI_NAME_FLB";
                        row[ctrlName] = string.Empty;
                        // 数量
                        index = dataTableDetailTmp.Columns.IndexOf("SUURYOU");
                        ctrlName = "PHN_SUURYOU_FLB";
                        row[ctrlName] = string.Empty;
                        // 単位
                        index = dataTableDetailTmp.Columns.IndexOf("UNIT_NAME");
                        ctrlName = "PHN_UNIT_NAME_FLB";
                        row[ctrlName] = string.Empty;
                    }

                    #endregion - Detail -

                    this.dataTable.Rows.Add(row);

                    if (rowNo >= dataTableDetailTmp.Rows.Count)
                    {
                        detailComp = true;
                    }
                    else
                    {
                        rowNo++;
                    }
                }

                this.SetRecord(this.dataTable);
            }
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
        }

        /// <summary>帳票出力用データテーブル作成処理を実行する</summary>
        private void SetChouhyouInfo()
        {
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
