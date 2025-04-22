using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Utility;

namespace Shougun.Core.Inspection.KensyuuIchiran
{
    #region - Class -

    /// <summary>(R400)検収一覧表を表すクラス・コントロール</summary>
    public class ReportInfoR400 : ReportInfoBase
    {
        #region - Fields -

        private const int ConstMaxDispDetailRowCount = 9;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR400"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR400(WINDOW_ID windowID)
        {
            this.windowID = windowID;
        }

        #endregion - Constructors -

        #region - Properties -

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

            // 会社略称
            dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
            // 発行日
            dataTableTmp.Columns.Add("PRINT_DATE");
            // タイトル
            dataTableTmp.Columns.Add("TITLE");
            // 拠点
            dataTableTmp.Columns.Add("KYOTEN_NAME");
            // 出荷日付
            dataTableTmp.Columns.Add("SHUKKA_DATE");
            // 検収日付
            dataTableTmp.Columns.Add("KENSHU_DATE");
            // 取引先コード
            dataTableTmp.Columns.Add("FILL_COND_CD_1");
            // 検収要否
            dataTableTmp.Columns.Add("KENSHU_YOUHI");
            // 検収有無
            dataTableTmp.Columns.Add("KENSHU_UMU");
            // 業者コード
            dataTableTmp.Columns.Add("FILL_COND_CD_2");
            // 現場コード
            dataTableTmp.Columns.Add("FILL_COND_CD_3");
            // 荷積業者コード
            dataTableTmp.Columns.Add("FILL_COND_CD_4");
            // 荷積現場コード
            dataTableTmp.Columns.Add("FILL_COND_CD_5");
            // 出荷品名コード
            dataTableTmp.Columns.Add("FILL_COND_CD_6");
            // 検収品名コード
            dataTableTmp.Columns.Add("FILL_COND_CD_7");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 会社略称
                rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 発行日
                rowTmp["PRINT_DATE"] = "b";
                // タイトル
                rowTmp["TITLE"] = "c";
                // 拠点
                rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 出荷日付
                rowTmp["SHUKKA_DATE"] = "2013/01/01 ～ 2013/12/31";
                // 検収日付
                rowTmp["KENSHU_DATE"] = "2013/01/01 ～ 2013/12/31";
                // 取引先コード
                rowTmp["FILL_COND_CD_1"] = "123456 ～ 123456";
                // 検収要否
                rowTmp["KENSHU_YOUHI"] = "検収必要";
                // 検収有無
                rowTmp["KENSHU_UMU"] = "検収済み";
                // 業者名コード
                rowTmp["FILL_COND_CD_2"] = "123456 ～ 123456";
                // 現場名コード
                rowTmp["FILL_COND_CD_3"] = "123456 ～ 123456";
                // 荷積業者コード
                rowTmp["FILL_COND_CD_4"] = "123456 ～ 123456";
                // 荷積現場コード
                rowTmp["FILL_COND_CD_5"] = "123456 ～ 123456";
                // 出荷品名コード
                rowTmp["FILL_COND_CD_6"] = "123456 ～ 123456";
                // 検収品名コード
                rowTmp["FILL_COND_CD_7"] = "123456 ～ 123456";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";
            // 出荷番号
            dataTableTmp.Columns.Add("SHUKKA_NUMBER");
            // 出荷日時
            dataTableTmp.Columns.Add("SHUKKA_DATE");
            // 検収日時
            dataTableTmp.Columns.Add("KENSHU_DATE");
            // 業者コード
            dataTableTmp.Columns.Add("GYOUSHA_CD");
            // 業者名
            dataTableTmp.Columns.Add("GYOUSHA_NAME");
            // 出荷時品コード
            dataTableTmp.Columns.Add("SHUKKA_HINMEI_CD");
            // 出荷時品名
            dataTableTmp.Columns.Add("SHUKKA_HINMEI_NAME");
            // 出荷時正味（kg）
            dataTableTmp.Columns.Add("SHUKKA_NET_JUURYOU");
            // 出荷時数量
            dataTableTmp.Columns.Add("SHUKKA_SUURYOU");
            // 出荷単位
            dataTableTmp.Columns.Add("SHUKKA_UNIT");
            // 出荷時単価
            dataTableTmp.Columns.Add("SHUKKA_TANKA");
            // 出荷時金額
            dataTableTmp.Columns.Add("SHUKKA_KINGAKU");
            // 明細行番
            dataTableTmp.Columns.Add("ROW_NO");
            // 取引先コード
            dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
            // 取引先
            dataTableTmp.Columns.Add("TORIHIKISAKI_NAME");
            // 現場コード
            dataTableTmp.Columns.Add("GENBA_CD");
            // 現場名
            dataTableTmp.Columns.Add("GENBA_NAME");
            // 検収時品コード
            dataTableTmp.Columns.Add("KENSHU_HINMEI_CD");
            // 検収時品名
            dataTableTmp.Columns.Add("KENSHU_HINMEI_NAME");
            // 検収時正味（kg）
            dataTableTmp.Columns.Add("KENSHU_JUURYOU");
            // 検収時数量
            dataTableTmp.Columns.Add("KENSHU_SUURYOU");
            // 歩引
            dataTableTmp.Columns.Add("BUBUKI");
            // 検収単位
            dataTableTmp.Columns.Add("KENSHU_UNIT");
            // 検収時単価
            dataTableTmp.Columns.Add("KENSHU_TANKA");
            // 検収時金額
            dataTableTmp.Columns.Add("KENSHU_KINGAKU");

            if (isPrint)
            {
                for (int i = 0; i < 18; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // 出荷番号
                    rowTmp["SHUKKA_NUMBER"] = (i + 1100).ToString();
                    // 出荷日時
                    rowTmp["SHUKKA_DATE"] = "2013/12/01 12:00:00";
                    // 検収日時
                    rowTmp["KENSHU_DATE"] = "2013/12/01 12:00:00";
                    // 業者コード
                    rowTmp["GYOUSHA_CD"] = "1234567890";
                    // 業者名
                    rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそ";
                    // 出荷時品コード
                    rowTmp["SHUKKA_HINMEI_CD"] = "1234567890";
                    // 出荷時品名
                    rowTmp["SHUKKA_HINMEI_NAME"] = "あいうえおかきくけこさしすせそ";
                    // 出荷時正味（kg）
                    rowTmp["SHUKKA_NET_JUURYOU"] = "123,456,789,000,123";
                    // 出荷時数量
                    rowTmp["SHUKKA_SUURYOU"] = "123,456,789,000,123";
                    // 出荷単位
                    rowTmp["SHUKKA_UNIT"] = "あいうえ";
                    // 出荷時単価
                    rowTmp["SHUKKA_TANKA"] = "123,456,789,000,123";
                    // 出荷時金額
                    rowTmp["SHUKKA_KINGAKU"] = "123,456,789,000,123";
                    // 明細行番
                    rowTmp["ROW_NO"] = (i + 2200).ToString();
                    // 取引先コード
                    rowTmp["TORIHIKISAKI_CD"] = "1234567890";
                    // 取引先
                    rowTmp["TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそ";
                    // 現場コード
                    rowTmp["GENBA_CD"] = "1234567890";
                    // 現場名
                    rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそ";
                    // 検収時品コード
                    rowTmp["KENSHU_HINMEI_CD"] = "1234567890";
                    // 検収時品名
                    rowTmp["KENSHU_HINMEI_NAME"] = "あいうえおかきくけこさしすせそ";
                    // 検収時正味（kg）
                    rowTmp["KENSHU_JUURYOU"] = "123,456,789,000,123";
                    // 検収時数量
                    rowTmp["KENSHU_SUURYOU"] = "123,456,789,000,123";
                    // 歩引
                    rowTmp["BUBUKI"] = "123,456,789,000,123";
                    // 検収単位
                    rowTmp["KENSHU_UNIT"] = "あいうえ";
                    // 検収時単価
                    rowTmp["KENSHU_TANKA"] = "123,456,789,000,123";
                    // 検収時金額
                    rowTmp["KENSHU_KINGAKU"] = "123,456,789,000,123";

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }

            this.DataTableList.Add("Detail", dataTableTmp);

            #endregion - Detail -

            #region - Group Footer -
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "GroupFooter";

            // 出荷時正味合計
            dataTableTmp.Columns.Add("SHUKKA_SHOUMI_KEI");
            // 出荷時金額計
            dataTableTmp.Columns.Add("SHUKKA_KINGAKU_KEI");
            // 検収時正味合計
            dataTableTmp.Columns.Add("KENSHU_SHOUMI_KEI");
            // 歩引
            dataTableTmp.Columns.Add("BUBUKI_KEI");
            // 検収時金額計
            dataTableTmp.Columns.Add("KENSHU_KINGAKU_KEI");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 出荷時正味合計
                rowTmp["SHUKKA_SHOUMI_KEI"] = "123,456,789,000,123";
                // 出荷時金額計
                rowTmp["SHUKKA_KINGAKU_KEI"] = "123,456,789,000,123";
                // 検収時正味合計
                rowTmp["KENSHU_SHOUMI_KEI"] = "123,456,789,000,123";
                // 歩引
                rowTmp["BUBUKI_KEI"] = "123,456,789,000,123";
                // 検収時金額計
                rowTmp["KENSHU_KINGAKU_KEI"] = "123,456,789,000,123";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("GroupFooter", dataTableTmp);
            #endregion

            #region - Footer -
            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";

            // 出荷時正味合計
            dataTableTmp.Columns.Add("SHUKKA_NET_JYUURYOU_TOTAL");
            // 検収時正味合計
            dataTableTmp.Columns.Add("KENSHU_NET_JYUURYOU_TOTAL");
            // 歩引合計
            dataTableTmp.Columns.Add("KENSHU_BUBIKI_TOTAL");
            // 検収時売上金額合計
            dataTableTmp.Columns.Add("KENSHU_URIAGE_KINGAKU_TOTAL");
            // 検収時支払金額合計
            dataTableTmp.Columns.Add("KENSHU_SHIHARAI_KINGAKU_TOTAL");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 出荷時正味合計
                rowTmp["SHUKKA_NET_JYUURYOU_TOTAL"] = "123,456,789,000,123";
                // 出荷時金額計
                rowTmp["KENSHU_NET_JYUURYOU_TOTAL"] = "123,456,789,000,123";
                // 検収時正味合計
                rowTmp["KENSHU_BUBIKI_TOTAL"] = "123,456,789,000,123";
                // 歩引
                rowTmp["KENSHU_URIAGE_KINGAKU_TOTAL"] = "123,456,789,000,123";
                // 検収時金額計
                rowTmp["KENSHU_SHIHARAI_KINGAKU_TOTAL"] = "123,456,789,000,123";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Footer", dataTableTmp);
            #endregion - Footer -
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int rowNo = 1;
            int i;
            DataRow row = null;
            DataTable dataTableDetailTmp = this.DataTableList["Detail"];
            DataTable dataTableGroupFooterTmp = this.DataTableList["GroupFooter"];
            DataTable dataTableFooterTmp = this.DataTableList["Footer"];
            string ctrlName = string.Empty;

            int maxPage;
            bool detailComp = false;
            int detailMaxCount = dataTableDetailTmp.Rows.Count;
            int detailStart = 0;

            int maxRow = 0;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region Columns
            // 出荷番号
            ctrlName = "PHY_SHUKKA_NUMBER_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷日時
            ctrlName = "PHY_SHUKKA_DATE_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収日時
            ctrlName = "PHY_KENSHU_DATE_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 業者コード
            ctrlName = "PHN_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 業者名
            ctrlName = "PHY_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時品コード
            ctrlName = "PHN_SHUKKA_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時品名
            ctrlName = "PHY_SHUKKA_HINMEI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時伝票区分
            ctrlName = "PHY_SHUKKA_DENPYOU_KBN_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時正味（kg）
            ctrlName = "PHY_SHUKKA_NET_JUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時数量
            ctrlName = "PHY_SHUKKA_SUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷単位
            ctrlName = "PHY_SHUKKA_UNIT_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 明細行番
            ctrlName = "PHY_ROW_NO_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 取引先コード
            ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 取引先
            ctrlName = "PHY_TORIHIKISAKI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 現場コード
            ctrlName = "PHN_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 現場名
            ctrlName = "PHY_GENBA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時品コード
            ctrlName = "PHN_KENSHU_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時品名
            ctrlName = "PHY_KENSHU_HINMEI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時伝票区分
            ctrlName = "PHY_KENSHU_DENPYOU_KBN_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時正味（kg）
            ctrlName = "PHY_KENSHU_JUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時数量
            ctrlName = "PHY_KENSHU_SUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 歩引
            ctrlName = "PHY_BUBUKI_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収単位
            ctrlName = "PHY_KENSHU_UNIT_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時単価
            ctrlName = "PHY_KENSHU_TANKA_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時金額
            ctrlName = "PHY_KENSHU_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時正味合計
            ctrlName = "PHN_SHUKKA_SHOUMI_KEI_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時金額合計
            ctrlName = "PHN_SHUKKA_KINGAKU_KEI_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時正味合計
            ctrlName = "PHN_KENSHU_SHOUMI_KEI_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 歩引
            ctrlName = "PHN_BUBUKI_KEI_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時金額計
            ctrlName = "PHN_KENSHU_KINGAKU_KEI_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 出荷時正味合計
            ctrlName = "F_SHUKKA_NET_JYUURYOU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時正味合計
            ctrlName = "F_KENSHU_NET_JYUURYOU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 歩引合計
            ctrlName = "F_KENSHU_BUBIKI_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時売上金額合計
            ctrlName = "F_KENSHU_URIAGE_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 検収時支払金額合計
            ctrlName = "F_KENSHU_SHIHARAI_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            #endregion

            maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispDetailRowCount);
            if (maxPage == 0)
            {
                maxPage = 1;
                detailComp = true;
            }

            maxRow = maxPage * ConstMaxDispDetailRowCount;

            if ((dataTableDetailTmp.Rows.Count % ConstMaxDispDetailRowCount) != 0)
            {
                maxRow--;
            }

            rowNo = 1;

            for (i = detailStart; i < maxRow; i++)
            {
                #region - Detail -

                row = this.dataTable.NewRow();

                if (!detailComp)
                {
                    // 出荷番号
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_NUMBER");
                    ctrlName = "PHY_SHUKKA_NUMBER_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷日時
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_DATE");
                    ctrlName = "PHY_SHUKKA_DATE_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収日時
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_DATE");
                    ctrlName = "PHY_KENSHU_DATE_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 業者コード
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
                    ctrlName = "PHY_GYOUSHA_NAME_FLB";
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

                    // 出荷時品コード
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_HINMEI_CD");
                    ctrlName = "PHN_SHUKKA_HINMEI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷時品名
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_HINMEI_NAME");
                    ctrlName = "PHY_SHUKKA_HINMEI_NAME_FLB";
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

                    // 出荷時伝票区分
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_DENPYOU_KBN");
                    ctrlName = "PHY_SHUKKA_DENPYOU_KBN_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷時正味（kg）
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_NET_JUURYOU");
                    ctrlName = "PHY_SHUKKA_NET_JUURYOU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷時数量
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SUURYOU");
                    ctrlName = "PHY_SHUKKA_SUURYOU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷単位
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_UNIT");
                    ctrlName = "PHY_SHUKKA_UNIT_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 8)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 8);
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

                    // 明細行番
                    index = dataTableDetailTmp.Columns.IndexOf("ROW_NO");
                    ctrlName = "PHY_ROW_NO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 取引先コード
                    index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                    ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 取引先
                    index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                    ctrlName = "PHY_TORIHIKISAKI_NAME_FLB";
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

                    // 現場コード
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
                    ctrlName = "PHY_GENBA_NAME_FLB";
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

                    // 検収時品コード
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_HINMEI_CD");
                    ctrlName = "PHN_KENSHU_HINMEI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時品名
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_HINMEI_NAME");
                    ctrlName = "PHY_KENSHU_HINMEI_NAME_FLB";
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

                    // 出荷時伝票区分
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_DENPYOU_KBN");
                    ctrlName = "PHY_KENSHU_DENPYOU_KBN_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時正味（kg）
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_JUURYOU");
                    ctrlName = "PHY_KENSHU_JUURYOU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時数量
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_SUURYOU");
                    ctrlName = "PHY_KENSHU_SUURYOU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 歩引
                    index = dataTableDetailTmp.Columns.IndexOf("BUBUKI");
                    ctrlName = "PHY_BUBUKI_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収単位
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_UNIT");
                    ctrlName = "PHY_KENSHU_UNIT_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 8)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 8);
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

                    // 検収時単価
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_TANKA");
                    ctrlName = "PHY_KENSHU_TANKA_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時金額
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_KINGAKU");
                    ctrlName = "PHY_KENSHU_KINGAKU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    if (rowNo == dataTableDetailTmp.Rows.Count)
                    {
                        detailComp = true;
                    }
                }
                else
                {
                    // 出荷番号
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_NUMBER");
                    ctrlName = "PHY_SHUKKA_NUMBER_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷日時
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_DATE");
                    ctrlName = "PHY_SHUKKA_DATE_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収日時
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_DATE");
                    ctrlName = "PHY_KENSHU_DATE_FLB";
                    row[ctrlName] = string.Empty;

                    // 業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_CD");
                    ctrlName = "PHN_GYOUSHA_CD_FLB";
                    row[ctrlName] = string.Empty;

                    // 業者名
                    index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_NAME");
                    ctrlName = "PHY_GYOUSHA_NAME_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷時品コード
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_HINMEI_CD");
                    ctrlName = "PHN_SHUKKA_HINMEI_CD_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷時品名
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_HINMEI_NAME");
                    ctrlName = "PHY_SHUKKA_HINMEI_NAME_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷時伝票区分
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_DENPYOU_KBN");
                    ctrlName = "PHY_SHUKKA_DENPYOU_KBN_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷時正味（kg）
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_NET_JUURYOU");
                    ctrlName = "PHY_SHUKKA_NET_JUURYOU_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷時数量
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SUURYOU");
                    ctrlName = "PHY_SHUKKA_SUURYOU_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷単位
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_UNIT");
                    ctrlName = "PHY_SHUKKA_UNIT_FLB";
                    row[ctrlName] = string.Empty;

                    // 明細行番
                    index = dataTableDetailTmp.Columns.IndexOf("ROW_NO");
                    ctrlName = "PHY_ROW_NO_FLB";
                    row[ctrlName] = string.Empty;

                    // 取引先コード
                    index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                    ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
                    row[ctrlName] = string.Empty;

                    // 取引先
                    index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                    ctrlName = "PHY_TORIHIKISAKI_NAME_FLB";
                    row[ctrlName] = string.Empty;

                    // 現場コード
                    index = dataTableDetailTmp.Columns.IndexOf("GENBA_CD");
                    ctrlName = "PHN_GENBA_CD_FLB";
                    row[ctrlName] = string.Empty;

                    // 現場名
                    index = dataTableDetailTmp.Columns.IndexOf("GENBA_NAME");
                    ctrlName = "PHY_GENBA_NAME_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時品コード
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_HINMEI_CD");
                    ctrlName = "PHN_KENSHU_HINMEI_CD_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時品名
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_HINMEI_NAME");
                    ctrlName = "PHY_KENSHU_HINMEI_NAME_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時伝票区分
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_DENPYOU_KBN");
                    ctrlName = "PHY_KENSHU_DENPYOU_KBN_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時正味（kg）
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_JUURYOU");
                    ctrlName = "PHY_KENSHU_JUURYOU_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時数量
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_SUURYOU");
                    ctrlName = "PHY_KENSHU_SUURYOU_FLB";
                    row[ctrlName] = string.Empty;

                    // 歩引
                    index = dataTableDetailTmp.Columns.IndexOf("BUBUKI");
                    ctrlName = "PHY_BUBUKI_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収単位
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_UNIT");
                    ctrlName = "PHY_KENSHU_UNIT_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時単価
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_TANKA");
                    ctrlName = "PHY_KENSHU_TANKA_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時金額
                    index = dataTableDetailTmp.Columns.IndexOf("KENSHU_KINGAKU");
                    ctrlName = "PHY_KENSHU_KINGAKU_FLB";
                    row[ctrlName] = string.Empty;
                }

                #endregion - Detail -

                #region - Goup Footer -

                if ((rowNo == maxRow) && (dataTableGroupFooterTmp.Rows.Count > 0))
                {
                    // 出荷時正味合計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("SHUKKA_SHOUMI_KEI");
                    ctrlName = "PHN_SHUKKA_SHOUMI_KEI_FLB";
                    if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷時金額合計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("SHUKKA_KINGAKU_KEI");
                    ctrlName = "PHN_SHUKKA_KINGAKU_KEI_FLB";
                    if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時正味合計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("KENSHU_SHOUMI_KEI");
                    ctrlName = "PHN_KENSHU_SHOUMI_KEI_FLB";
                    if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 歩引
                    index = dataTableGroupFooterTmp.Columns.IndexOf("BUBUKI_KEI");
                    ctrlName = "PHN_BUBUKI_KEI_FLB";
                    if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時金額計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("KENSHU_KINGAKU_KEI");
                    ctrlName = "PHN_KENSHU_KINGAKU_KEI_FLB";
                    if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                }
                else
                {
                    // 出荷時正味合計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("SHUKKA_SHOUMI_KEI");
                    ctrlName = "PHN_SHUKKA_SHOUMI_KEI_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷時金額合計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("SHUKKA_KINGAKU_KEI");
                    ctrlName = "PHN_SHUKKA_KINGAKU_KEI_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時正味合計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("KENSHU_SHOUMI_KEI");
                    ctrlName = "PHN_KENSHU_SHOUMI_KEI_FLB";
                    row[ctrlName] = string.Empty;

                    // 歩引
                    index = dataTableGroupFooterTmp.Columns.IndexOf("BUBUKI_KEI");
                    ctrlName = "PHN_BUBUKI_KEI_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時金額計
                    index = dataTableGroupFooterTmp.Columns.IndexOf("KENSHU_KINGAKU_KEI");
                    ctrlName = "PHN_KENSHU_KINGAKU_KEI_FLB";
                    row[ctrlName] = string.Empty;
                }

                #endregion - Goup Footer -

                #region - Footer -
                if ((rowNo == maxRow) && (dataTableFooterTmp.Rows.Count > 0))
                {
                    // 出荷時正味合計
                    index = dataTableFooterTmp.Columns.IndexOf("SHUKKA_NET_JYUURYOU_TOTAL");
                    ctrlName = "F_SHUKKA_NET_JYUURYOU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時正味合計
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_NET_JYUURYOU_TOTAL");
                    ctrlName = "F_KENSHU_NET_JYUURYOU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 歩引合計
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_BUBIKI_TOTAL");
                    ctrlName = "F_KENSHU_BUBIKI_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時売上金額合計
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_URIAGE_KINGAKU_TOTAL");
                    ctrlName = "F_KENSHU_URIAGE_KINGAKU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableFooterTmp.Rows[0].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 検収時支払金額合計
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_SHIHARAI_KINGAKU_TOTAL");
                    ctrlName = "F_KENSHU_SHIHARAI_KINGAKU_TOTAL_FLB";
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
                    // 出荷時正味合計
                    index = dataTableFooterTmp.Columns.IndexOf("SHUKKA_NET_JYUURYOU_TOTAL");
                    ctrlName = "F_SHUKKA_NET_JYUURYOU_TOTAL_FLB";
                    row[ctrlName] = string.Empty;

                    // 出荷時金額合計
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_NET_JYUURYOU_TOTAL");
                    ctrlName = "F_KENSHU_NET_JYUURYOU_TOTAL_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時正味合計
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_BUBIKI_TOTAL");
                    ctrlName = "F_KENSHU_BUBIKI_TOTAL_FLB";
                    row[ctrlName] = string.Empty;

                    // 歩引
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_URIAGE_KINGAKU_TOTAL");
                    ctrlName = "F_KENSHU_URIAGE_KINGAKU_TOTAL_FLB";
                    row[ctrlName] = string.Empty;

                    // 検収時金額計
                    index = dataTableFooterTmp.Columns.IndexOf("KENSHU_SHIHARAI_KINGAKU_TOTAL");
                    ctrlName = "F_KENSHU_SHIHARAI_KINGAKU_TOTAL_FLB";
                    row[ctrlName] = string.Empty;
                }
                #endregion

                this.dataTable.Rows.Add(row);

                rowNo++;
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

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            #region - Header -

            if (dataTableHeaderTmp.Rows.Count > 0)
            {
                // 会社略称
                index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 40)
                    {
                        this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", encoding.GetString(byteArray, 0, 40));
                    }
                    else
                    {
                        this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", string.Empty);
                }

                //// 発行日
                //index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                //this.SetFieldName("PHY_PRINT_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                //// タイトル
                //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                //this.SetFieldName("PHY_TITLE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

                // 拠点
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 10)
                    {
                        this.SetFieldName("PHY_KYOTEN_NAME_VLB", encoding.GetString(byteArray, 0, 10));
                    }
                    else
                    {
                        this.SetFieldName("PHY_KYOTEN_NAME_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName("PHY_KYOTEN_NAME_VLB", string.Empty);
                }

                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                this.SetFieldName("PHY_PRINT_DATE_VLB", this.getDBDateTime().ToString("yyyy/MM/dd H:mm:ss") + " 発行");
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // 出荷日付
                index = dataTableHeaderTmp.Columns.IndexOf("SHUKKA_DATE");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_SHUKKA_DATE_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_SHUKKA_DATE_CTL", string.Empty);
                }

                // 検収日付
                index = dataTableHeaderTmp.Columns.IndexOf("KENSHU_DATE");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_KENSHU_DATE_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_KENSHU_DATE_CTL", string.Empty);
                }

                // 取引先コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_1");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_1_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_1_CTL", string.Empty);
                }

                // 検収要否
                index = dataTableHeaderTmp.Columns.IndexOf("KENSHU_YOUHI");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_KENSHU_YOUHI_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_KENSHU_YOUHI_CTL", string.Empty);
                }

                // 検収有無
                index = dataTableHeaderTmp.Columns.IndexOf("KENSHU_UMU");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_KENSHU_UMU_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_KENSHU_UMU_CTL", string.Empty);
                }

                // 業者コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_2");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_2_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_2_CTL", string.Empty);
                }

                // 現場コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_3");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_3_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_3_CTL", string.Empty);
                }

                // 荷積業者コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_4");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_4_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_4_CTL", string.Empty);
                }

                // 荷積現場コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_5");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_5_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_5_CTL", string.Empty);
                }

                // 出荷品名コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_6");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_6_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_6_CTL", string.Empty);
                }

                // 検収品名コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_7");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_7_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_7_CTL", string.Empty);
                }
            }
            else
            {
                // 会社略称
                index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", string.Empty);

                //// 発行日
                //index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                //this.SetFieldName("PHY_PRINT_DATE_VLB", string.Empty);

                //// タイトル
                //index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                //this.SetFieldName("PHY_TITLE_VLB", string.Empty);

                // 拠点
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME");
                this.SetFieldName("PHY_KYOTEN_NAME_VLB", string.Empty);

                // 出荷日付
                index = dataTableHeaderTmp.Columns.IndexOf("SHUKKA_DATE");
                this.SetFieldName("PHY_SHUKKA_DATE_CTL", string.Empty);

                // 検収日付
                index = dataTableHeaderTmp.Columns.IndexOf("KENSHU_DATE");
                this.SetFieldName("PHY_KENSHU_DATE_CTL", string.Empty);

                // 取引先コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_1");
                this.SetFieldName("PHY_FILL_COND_CD_1_CTL", string.Empty);

                // 検収要否
                index = dataTableHeaderTmp.Columns.IndexOf("KENSHU_YOUHI");
                this.SetFieldName("PHY_KENSHU_YOUHI_CTL", string.Empty);

                // 検収有無
                index = dataTableHeaderTmp.Columns.IndexOf("KENSHU_UMU");
                this.SetFieldName("PHY_KENSHU_UMU_CTL", string.Empty);

                // 業者コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_2");
                this.SetFieldName("PHY_FILL_COND_CD_2_CTL", string.Empty);

                // 現場コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_3");
                this.SetFieldName("PHY_FILL_COND_CD_3_CTL", string.Empty);

                // 荷積業者コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_4");
                this.SetFieldName("PHY_FILL_COND_CD_4_CTL", string.Empty);

                // 荷積現場コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_5");
                this.SetFieldName("PHY_FILL_COND_CD_5_CTL", string.Empty);

                // 出荷品名コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_6");
                this.SetFieldName("PHY_FILL_COND_CD_6_CTL", string.Empty);

                // 検収品名コード
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_7");
                this.SetFieldName("PHY_FILL_COND_CD_7_CTL", string.Empty);
            }

            #endregion - Header -
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            System.Data.DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #endregion - Methods -
    }

    #endregion - Class -
}
