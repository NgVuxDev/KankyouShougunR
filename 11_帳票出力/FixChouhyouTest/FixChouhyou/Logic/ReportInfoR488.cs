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

    /// <summary>(R488)代納集計表を表すクラス・コントロール</summary>
    public class ReportInfoR488 : ReportInfoBase
    {
        #region - Fields -
        private const int ConstMaxDispDetailRowCount = 15;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR488"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR488(WINDOW_ID windowID)
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
            bool isPrintH = true;

            #region - Header -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Header";

            // 会社名
            dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
            // 拠点名
            dataTableTmp.Columns.Add("KYOTEN_NAME");
            // 伝票日付範囲
            dataTableTmp.Columns.Add("DENPYOU_DATE");

            // 受入取引先CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_1");
            // 受入業者CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_2");
            // 受入現場CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_3");
            // 受入品名CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_4");

            // 出荷取引先CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_5");
            // 出荷業者CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_6");
            // 出荷現場CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_7");
            // 出荷品名CD範囲
            dataTableTmp.Columns.Add("FILL_COND_CD_8");

            // 出荷運搬業者
            dataTableTmp.Columns.Add("FILL_COND_CD_9");

            if (isPrintH)
            {
                rowTmp = dataTableTmp.NewRow();

                // 会社名
                rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 拠点名
                rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 伝票日付範囲
                rowTmp["DENPYOU_DATE"] = "2013/12/01 ～ 2013/12/31";
                // 受入取引先CD範囲
                rowTmp["FILL_COND_CD_1"] = "123456 ～ 123456";
                // 受入業者CD範囲
                rowTmp["FILL_COND_CD_2"] = "123456 ～ 123456";
                // 受入現場CD範囲
                rowTmp["FILL_COND_CD_3"] = "123456 ～ 123456";
                // 受入品名CD範囲
                rowTmp["FILL_COND_CD_4"] = "123456 ～ 123456";
                // 出荷取引先CD範囲
                rowTmp["FILL_COND_CD_5"] = "123456 ～ 123456";
                // 出荷業者CD範囲
                rowTmp["FILL_COND_CD_6"] = "123456 ～ 123456";
                // 出荷現場CD範囲
                rowTmp["FILL_COND_CD_7"] = "123456 ～ 123456";
                // 出荷品名CD範囲
                rowTmp["FILL_COND_CD_8"] = "123456 ～ 123456";
                // 運搬業者CD範囲
                rowTmp["FILL_COND_CD_9"] = "123456 ～ 123456";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            // 受入取引先コード
            dataTableTmp.Columns.Add("UKEIRE_TORIHIKISAKI_CD");
            // 受入取引先名
            dataTableTmp.Columns.Add("UKEIRE_TORIHIKISAKI_NAME");
            // 受入業者コード
            dataTableTmp.Columns.Add("UKEIRE_GYOUSHA_CD");
            // 受入業者名
            dataTableTmp.Columns.Add("UKEIRE_GYOUSHA_NAME");
            // 受入運搬業者コード
            dataTableTmp.Columns.Add("UPN_GYOUSHA_CD");
            // 受入運搬業者名
            dataTableTmp.Columns.Add("UPN_GYOUSHA_NAME");
            // 出荷取引先コード
            dataTableTmp.Columns.Add("SHUKKA_TORIHIKISAKI_CD");
            // 出荷取引先名
            dataTableTmp.Columns.Add("SHUKKA_TORIHIKISAKI_NAME");
            // 出荷業者コード
            dataTableTmp.Columns.Add("SHUKKA_GYOUSHA_CD");
            // 出荷業者名
            dataTableTmp.Columns.Add("SHUKKA_GYOUSHA_NAME");
            // 受入現場コード
            dataTableTmp.Columns.Add("UKEIRE_GENBA_CD");
            // 受入品名コード
            dataTableTmp.Columns.Add("UKEIRE_HINMEI_CD");
            // 受入現場名
            dataTableTmp.Columns.Add("UKEIRE_GENBA_NAME");
            // 受入品名
            dataTableTmp.Columns.Add("UKEIRE_HINMEI_NAME");
            // 受入正味(kg)
            dataTableTmp.Columns.Add("UKEIRE_SYOUMI");
            // 受入数量
            dataTableTmp.Columns.Add("UKEIRE_SUURYOU");
            // 受入単位
            dataTableTmp.Columns.Add("UKEIRE_UNIT_NAME");
            // 受入金額
            dataTableTmp.Columns.Add("UKEIRE_KINGAKU");
            // 出荷現場コード
            dataTableTmp.Columns.Add("SHUKKA_GENBA_CD");
            // 出荷品名コード
            dataTableTmp.Columns.Add("SHUKKA_HINMEI_CD");
            // 出荷現場名
            dataTableTmp.Columns.Add("SHUKKA_GENBA_NAME");
            // 出荷品名
            dataTableTmp.Columns.Add("SHUKKA_HINMEI_NAME");
            // 出荷正味(kg)
            dataTableTmp.Columns.Add("SHUKKA_SYOUMI");
            // 出荷数量
            dataTableTmp.Columns.Add("SHUKKA_SUURYOU");
            // 出荷単位
            dataTableTmp.Columns.Add("SHUKKA_UNIT_NAME");
            // 出荷金額
            dataTableTmp.Columns.Add("SHUKKA_KINGAKU");
            // 差益金額
            dataTableTmp.Columns.Add("SAEKI_KINGAKU");
            // 運賃金額
            dataTableTmp.Columns.Add("UNCHIN_KINGAKU");
            // 受入正味合計
            dataTableTmp.Columns.Add("UKEIRE_SYOUMI_TOTAL");
            // 受入金額合計
            dataTableTmp.Columns.Add("UKEIRE_KINGAKU_TOTAL");
            // 出荷正味合計
            dataTableTmp.Columns.Add("SHUKKA_SYOUMI_TOTAL");
            // 出荷金額合計
            dataTableTmp.Columns.Add("SHUKKA_KINGAKU_TOTAL");
            // 差益金額合計
            dataTableTmp.Columns.Add("SAEK_KINGAKU_TOTAL");
            // 運賃金額合計
            dataTableTmp.Columns.Add("UNCHIN_KINGAKU_TOTAL");

            if (isPrint)
            {
                for (int i = 1; i < 10; i++)
                {
                    for (int j = 1; j < 3; j++)
                    {
                        for (int k = 1; k < 3; k++)
                        {
                            for (int l = 1; l < 3; l++)
                            {
                                for (int m = 1; m < 3; m++)
                                {
                                    for (int n = 1; n < 3; n++)
                                    {
                                        rowTmp = dataTableTmp.NewRow();

                                        // 受入取引先コード
                                        rowTmp["UKEIRE_TORIHIKISAKI_CD"] = (i * 1000000).ToString();
                                        // 受入取引先名
                                        rowTmp["UKEIRE_TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 受入業者コード
                                        rowTmp["UKEIRE_GYOUSHA_CD"] = (j * 1000000).ToString();
                                        // 受入業者名
                                        rowTmp["UKEIRE_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 受入運搬業者コード
                                        rowTmp["UPN_GYOUSHA_CD"] = (k * 1000000).ToString();
                                        // 受入運搬業者名
                                        rowTmp["UPN_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";

                                        // 出荷取引先コード
                                        rowTmp["SHUKKA_TORIHIKISAKI_CD"] = (l * 1000000).ToString();
                                        // 出荷取引先名
                                        rowTmp["SHUKKA_TORIHIKISAKI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 出荷業者コード
                                        rowTmp["SHUKKA_GYOUSHA_CD"] = (m * 1000000).ToString();
                                        // 出荷業者名
                                        rowTmp["SHUKKA_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 受入現場コード
                                        rowTmp["UKEIRE_GENBA_CD"] = "1234567890";
                                        // 受入品名コード
                                        rowTmp["UKEIRE_HINMEI_CD"] = "1234567890";
                                        // 受入現場名
                                        rowTmp["UKEIRE_GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 受入品名
                                        rowTmp["UKEIRE_HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 受入正味(kg)
                                        rowTmp["UKEIRE_SYOUMI"] = "123,456,789,000,123";
                                        // 受入数量
                                        rowTmp["UKEIRE_SUURYOU"] = "123,456,789,000,123";
                                        // 受入単位
                                        rowTmp["UKEIRE_UNIT_NAME"] = "あいうえおかきくけこ";
                                        // 受入金額
                                        rowTmp["UKEIRE_KINGAKU"] = "123,456,789,000,123";
                                        // 出荷現場コード
                                        rowTmp["SHUKKA_GENBA_CD"] = "1234567890";
                                        // 出荷品名コード
                                        rowTmp["SHUKKA_HINMEI_CD"] = "1234567890";
                                        // 出荷現場名
                                        rowTmp["SHUKKA_GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 出荷品名
                                        rowTmp["SHUKKA_HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                                        // 出荷正味(kg)
                                        rowTmp["SHUKKA_SYOUMI"] = "123,456,789,000,123";
                                        // 出荷数量
                                        rowTmp["SHUKKA_SUURYOU"] = "123,456,789,000,123";
                                        // 出荷単位
                                        rowTmp["SHUKKA_UNIT_NAME"] = "あいうえおかきくけこ";
                                        // 出荷金額
                                        rowTmp["SHUKKA_KINGAKU"] = "123,456,789,000,123";
                                        // 差益金額
                                        rowTmp["SAEKI_KINGAKU"] = "123,456,789,000,123";
                                        // 運賃金額
                                        rowTmp["UNCHIN_KINGAKU"] = "123,456,789,000,123";
                                        // 受入正味合計
                                        rowTmp["UKEIRE_SYOUMI_TOTAL"] = "123,456,789,000,123";
                                        // 受入金額合計
                                        rowTmp["UKEIRE_KINGAKU_TOTAL"] = "123,456,789,000,123";
                                        // 出荷正味合計
                                        rowTmp["SHUKKA_SYOUMI_TOTAL"] = "123,456,789,000,123";
                                        // 出荷金額合計
                                        rowTmp["SHUKKA_KINGAKU_TOTAL"] = "123,456,789,000,123";
                                        // 差益金額合計
                                        rowTmp["SAEK_KINGAKU_TOTAL"] = "123,456,789,000,123";
                                        // 運賃金額合計
                                        rowTmp["UNCHIN_KINGAKU_TOTAL"] = "123,456,789,000,123";

                                        dataTableTmp.Rows.Add(rowTmp);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            this.DataTableList.Add("Detail", dataTableTmp);
            #endregion - Detail -

            #region - Footer -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";

            // 受入正味総合計
            dataTableTmp.Columns.Add("UKEIRE_SYOUMI_TOTAL");
            // 受入金額総合計
            dataTableTmp.Columns.Add("UKEIRE_KINGAKU_TOTAL");
            // 出荷正味総合計
            dataTableTmp.Columns.Add("SHUKKA_SYOUMI_TOTAL");
            // 出荷金額総合計
            dataTableTmp.Columns.Add("SHUKKA_KINGAKU_TOTAL");
            // 差益金額総合計
            dataTableTmp.Columns.Add("SAEKI_KINGAKU_TOTAL");
            // 運賃金額総合計
            dataTableTmp.Columns.Add("UNCHIN_KINGAKU_TOTAL");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 受入正味総合計
                rowTmp["UKEIRE_SYOUMI_TOTAL"] = "123,456,789,000,123";
                // 受入金額総合計
                rowTmp["UKEIRE_KINGAKU_TOTAL"] = "123,456,789,000,123";
                // 出荷正味総合計
                rowTmp["SHUKKA_SYOUMI_TOTAL"] = "123,456,789,000,123";
                // 出荷金額総合計
                rowTmp["SHUKKA_KINGAKU_TOTAL"] = "123,456,789,000,123";
                // 差益金額総合計
                rowTmp["SAEKI_KINGAKU_TOTAL"] = "123,456,789,000,123";
                // 運賃金額総合計
                rowTmp["UNCHIN_KINGAKU_TOTAL"] = "123,456,789,000,123";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Footer", dataTableTmp);
            #endregion - Footer -
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int i;
            DataRow row = null;
            DataTable dataTableDetailTmp = this.DataTableList["Detail"];
            string ctrlName = string.Empty;
            
            int detailMaxCount = dataTableDetailTmp.Rows.Count;
            int detailStart = 0;

            string tmp;
            string strKeys = string.Empty;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region Columns

            // Key
            ctrlName = "PHN_Key_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入取引先コード
            ctrlName = "PHY_UKEIRE_TORIHIKISAKI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入取引先名
            ctrlName = "PHY_UKEIRE_TORIHIKISAKI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入業者コード
            ctrlName = "PHY_UKEIRE_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入業者名
            ctrlName = "PHY_UKEIRE_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運搬業者コード
            ctrlName = "PHY_UPN_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運搬業者名
            ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷取引先コード
            ctrlName = "PHY_SHUKKA_TORIHIKISAKI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷取引先名
            ctrlName = "PHY_SHUKKA_TORIHIKISAKI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷業者コード
            ctrlName = "PHY_SHUKKA_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷業者名
            ctrlName = "PHY_SHUKKA_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入現場コード
            ctrlName = "PHN_UKEIRE_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入品名コード
            ctrlName = "PHN_UKEIRE_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入現場名
            ctrlName = "PHY_UKEIRE_GENBA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入品名
            ctrlName = "PHY_UKEIRE_HINMEI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入正味(kg)
            ctrlName = "PHY_UKEIRE_SYOUMI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入数量
            ctrlName = "PHY_UKEIRE_SUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入単位
            ctrlName = "PHY_UKEIRE_UNIT_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入金額
            ctrlName = "PHY_UKEIRE_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷現場コード
            ctrlName = "PHN_SHUKKA_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷品名コード
            ctrlName = "PHN_SHUKKA_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷現場名
            ctrlName = "PHY_SHUKKA_GENBA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷品名
            ctrlName = "PHY_SHUKKA_HINMEI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷正味(kg)
            ctrlName = "PHY_SHUKKA_SYOUMI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷数量
            ctrlName = "PHY_SHUKKA_SUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷単位
            ctrlName = "PHY_SHUKKA_UNIT_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷金額
            ctrlName = "PHY_SHUKKA_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 差益金額
            ctrlName = "PHY_SAEKI_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃金額
            ctrlName = "PHY_UNCHIN_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入正味合計
            ctrlName = "PHN_G2F_UKEIRE_SYOUMI_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 受入金額合計
            ctrlName = "PHN_G2F_UKEIRE_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷正味合計
            ctrlName = "PHN_G2F_SHUKKA_SYOUMI_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 出荷金額合計
            ctrlName = "PHN_G2F_SHUKKA_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 差益金額合計
            ctrlName = "PHN_G2F_SAEKI_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃金額合計
            ctrlName = "PHN_G2F_UNCHIN_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            #endregion Columns

            #region - Detail -

            if (detailMaxCount > 0)
            {
                for (i = detailStart; i < detailMaxCount; i++)
                {
                    row = this.dataTable.NewRow();

                    // 受入取引先コード
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_TORIHIKISAKI_CD");
                    ctrlName = "PHY_UKEIRE_TORIHIKISAKI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    row[ctrlName] = tmp;
                    strKeys = tmp;

                    // 受入取引先名
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_TORIHIKISAKI_NAME");
                    ctrlName = "PHY_UKEIRE_TORIHIKISAKI_NAME_FLB";
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

                    // 受入業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GYOUSHA_CD");
                    ctrlName = "PHY_UKEIRE_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    row[ctrlName] = tmp;
                    strKeys += tmp;

                    // 受入業者名
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GYOUSHA_NAME");
                    ctrlName = "PHY_UKEIRE_GYOUSHA_NAME_FLB";
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

                    // 受入運搬業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("UPN_GYOUSHA_CD");
                    ctrlName = "PHY_UPN_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    row[ctrlName] = tmp;
                    strKeys += tmp;

                    // 受入運搬業者名
                    index = dataTableDetailTmp.Columns.IndexOf("UPN_GYOUSHA_NAME");
                    ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
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

                    // 出荷取引先コード
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_TORIHIKISAKI_CD");
                    ctrlName = "PHY_SHUKKA_TORIHIKISAKI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    row[ctrlName] = tmp;
                    strKeys += tmp;

                    // 出荷取引先名
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_TORIHIKISAKI_NAME");
                    ctrlName = "PHY_SHUKKA_TORIHIKISAKI_NAME_FLB";
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

                    // 出荷取業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GYOUSHA_CD");
                    ctrlName = "PHY_SHUKKA_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        tmp = (string)dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        tmp = string.Empty;
                    }

                    row[ctrlName] = tmp;
                    strKeys += tmp;

                    // 出荷取業者名
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GYOUSHA_NAME");
                    ctrlName = "PHY_SHUKKA_GYOUSHA_NAME_FLB";
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

                    // 受入現場コード
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GENBA_CD");
                    ctrlName = "PHN_UKEIRE_GENBA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 受入品名コード
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_HINMEI_CD");
                    ctrlName = "PHN_UKEIRE_HINMEI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 受入現場名
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GENBA_NAME");
                    ctrlName = "PHY_UKEIRE_GENBA_NAME_FLB";
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

                    // 受入品名
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_HINMEI_NAME");
                    ctrlName = "PHY_UKEIRE_HINMEI_NAME_FLB";
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

                    // 受入正味(kg)
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_SYOUMI");
                    ctrlName = "PHY_UKEIRE_SYOUMI_NAME_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 受入数量
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_SUURYOU");
                    ctrlName = "PHY_UKEIRE_SUURYOU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 受入単位
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_UNIT_NAME");
                    ctrlName = "PHY_UKEIRE_UNIT_NAME_FLB";
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

                    // 受入金額
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_KINGAKU");
                    ctrlName = "PHY_UKEIRE_KINGAKU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷現場コード
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GENBA_CD");
                    ctrlName = "PHN_SHUKKA_GENBA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷品名コード
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

                    // 出荷現場名
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GENBA_NAME");
                    ctrlName = "PHY_SHUKKA_GENBA_NAME_FLB";
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

                    // 出荷品名
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

                    // 出荷正味(kg)
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SYOUMI");
                    ctrlName = "PHY_SHUKKA_SYOUMI_NAME_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷数量
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
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_UNIT_NAME");
                    ctrlName = "PHY_SHUKKA_UNIT_NAME_FLB";
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

                    // 出荷金額
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_KINGAKU");
                    ctrlName = "PHY_SHUKKA_KINGAKU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 差益金額
                    index = dataTableDetailTmp.Columns.IndexOf("SAEKI_KINGAKU");
                    ctrlName = "PHY_SAEKI_KINGAKU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運賃金額
                    index = dataTableDetailTmp.Columns.IndexOf("UNCHIN_KINGAKU");
                    ctrlName = "PHY_UNCHIN_KINGAKU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 受入正味合計
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_SYOUMI_TOTAL");
                    ctrlName = "PHN_G2F_UKEIRE_SYOUMI_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 受入金額合計
                    index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_KINGAKU_TOTAL");
                    ctrlName = "PHN_G2F_UKEIRE_KINGAKU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷正味合計
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SYOUMI_TOTAL");
                    ctrlName = "PHN_G2F_SHUKKA_SYOUMI_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 出荷金額合計
                    index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SYOUMI_TOTAL");
                    ctrlName = "PHN_G2F_SHUKKA_KINGAKU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 差益金額合計
                    index = dataTableDetailTmp.Columns.IndexOf("SAEK_KINGAKU_TOTAL");
                    ctrlName = "PHN_G2F_SAEKI_KINGAKU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運賃金額合計
                    index = dataTableDetailTmp.Columns.IndexOf("UNCHIN_KINGAKU_TOTAL");
                    ctrlName = "PHN_G2F_UNCHIN_KINGAKU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // キー
                    row["PHN_Key_FLB"] = strKeys;

                    this.dataTable.Rows.Add(row);
                }
            }
            else
            {
                row = this.dataTable.NewRow();

                // 受入取引先コード
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_TORIHIKISAKI_CD");
                ctrlName = "PHY_UKEIRE_TORIHIKISAKI_CD_FLB";
                row[ctrlName] = string.Empty;
                // 受入取引先名
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_TORIHIKISAKI_NAME");
                ctrlName = "PHY_UKEIRE_TORIHIKISAKI_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 受入業者コード
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GYOUSHA_CD");
                ctrlName = "PHY_UKEIRE_GYOUSHA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 受入業者名
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GYOUSHA_NAME");
                ctrlName = "PHY_UKEIRE_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 受入運搬業者コード
                index = dataTableDetailTmp.Columns.IndexOf("UPN_GYOUSHA_CD");
                ctrlName = "PHY_UPN_GYOUSHA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 受入運搬業者名
                index = dataTableDetailTmp.Columns.IndexOf("UPN_GYOUSHA_NAME");
                ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 出荷取引先コード
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_TORIHIKISAKI_CD");
                ctrlName = "PHY_SHUKKA_TORIHIKISAKI_CD_FLB";
                row[ctrlName] = string.Empty;
                // 出荷取引先名
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_TORIHIKISAKI_NAME");
                ctrlName = "PHY_SHUKKA_TORIHIKISAKI_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 出荷取業者コード
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GYOUSHA_CD");
                ctrlName = "PHY_SHUKKA_GYOUSHA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 出荷取業者名
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GYOUSHA_NAME");
                ctrlName = "PHY_SHUKKA_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 受入現場コード
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GENBA_CD");
                ctrlName = "PHN_UKEIRE_GENBA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 受入品名コード
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_HINMEI_CD");
                ctrlName = "PHN_UKEIRE_HINMEI_CD_FLB";
                row[ctrlName] = string.Empty;
                // 受入現場名
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_GENBA_NAME");
                ctrlName = "PHY_UKEIRE_GENBA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 受入品名
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_HINMEI_NAME");
                ctrlName = "PHY_UKEIRE_HINMEI_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 受入正味(kg)
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_SYOUMI");
                ctrlName = "PHY_UKEIRE_SYOUMI_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 受入数量
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_SUURYOU");
                ctrlName = "PHY_UKEIRE_SUURYOU_FLB";
                row[ctrlName] = string.Empty;
                // 受入単位
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_UNIT_NAME");
                ctrlName = "PHY_UKEIRE_UNIT_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 受入金額
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_KINGAKU");
                ctrlName = "PHY_UKEIRE_KINGAKU_FLB";
                row[ctrlName] = string.Empty;
                // 出荷現場コード
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GENBA_CD");
                ctrlName = "PHN_SHUKKA_GENBA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 出荷品名コード
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_HINMEI_CD");
                ctrlName = "PHN_SHUKKA_HINMEI_CD_FLB";
                row[ctrlName] = string.Empty;
                // 出荷現場名
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_GENBA_NAME");
                ctrlName = "PHY_SHUKKA_GENBA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 出荷品名
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_HINMEI_NAME");
                ctrlName = "PHY_SHUKKA_HINMEI_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 出荷正味(kg)
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SYOUMI");
                ctrlName = "PHY_SHUKKA_SYOUMI_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 出荷数量
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SUURYOU");
                ctrlName = "PHY_SHUKKA_SUURYOU_FLB";
                row[ctrlName] = string.Empty;
                // 出荷単位
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_UNIT_NAME");
                ctrlName = "PHY_SHUKKA_UNIT_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 出荷金額
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_KINGAKU");
                ctrlName = "PHY_SHUKKA_KINGAKU_FLB";
                row[ctrlName] = string.Empty;
                // 差益金額
                index = dataTableDetailTmp.Columns.IndexOf("SAEKI_KINGAKU");
                ctrlName = "PHY_SAEKI_KINGAKU_FLB";
                row[ctrlName] = string.Empty;
                // 運賃金額
                index = dataTableDetailTmp.Columns.IndexOf("UNCHIN_KINGAKU");
                ctrlName = "PHY_UNCHIN_KINGAKU_FLB";
                row[ctrlName] = string.Empty;
                // 受入正味合計
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_SYOUMI_TOTAL");
                ctrlName = "PHN_G2F_UKEIRE_SYOUMI_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 受入金額合計
                index = dataTableDetailTmp.Columns.IndexOf("UKEIRE_KINGAKU_TOTAL");
                ctrlName = "PHN_G2F_UKEIRE_KINGAKU_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 出荷正味合計
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SYOUMI_TOTAL");
                ctrlName = "PHN_G2F_SHUKKA_SYOUMI_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 出荷金額合計
                index = dataTableDetailTmp.Columns.IndexOf("SHUKKA_SYOUMI_TOTAL");
                ctrlName = "PHN_G2F_SHUKKA_KINGAKU_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 差益金額合計
                index = dataTableDetailTmp.Columns.IndexOf("SAEK_KINGAKU_TOTAL");
                ctrlName = "PHN_G2F_SAEKI_KINGAKU_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 運賃金額合計
                index = dataTableDetailTmp.Columns.IndexOf("UNCHIN_KINGAKU_TOTAL");
                ctrlName = "PHN_G2F_UNCHIN_KINGAKU_TOTAL_FLB";
                row[ctrlName] = string.Empty;

                this.dataTable.Rows.Add(row);
            }

            #endregion - Detail -

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
            DataTable dataTableFooterTmp = this.DataTableList["Footer"];

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

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

                // 拠点名
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 20)
                    {
                        this.SetFieldName("PHY_KYOTEN_NAME_VLB", encoding.GetString(byteArray, 0, 20));
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

                // 伝票日付範囲
                index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_DENPYOU_DATE_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_DENPYOU_DATE_CTL", string.Empty);
                }

                // 受入取引先CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_1");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_1_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_1_CTL", string.Empty);
                }

                // 受入業者CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_2");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_2_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_2_CTL", string.Empty);
                }

                // 受入現場CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_3");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_3_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_3_CTL", string.Empty);
                }

                // 受入品名CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_4");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_4_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_4_CTL", string.Empty);
                }

                // 出荷取引先CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_5");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_5_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_5_CTL", string.Empty);
                }

                // 出荷業者CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_6");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_6_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_6_CTL", string.Empty);
                }

                // 出荷現場CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_7");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_7_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_7_CTL", string.Empty);
                }

                // 出荷品名CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_8");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_8_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_8_CTL", string.Empty);
                }

                // 運搬業者CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_9");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_9_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_9_CTL", string.Empty);
                }
            }
            else
            {
                // 会社名
                index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", string.Empty);
                // 拠点名
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME");
                this.SetFieldName("PHY_KYOTEN_NAME_VLB", string.Empty);
                // 伝票日付範囲
                index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                this.SetFieldName("PHY_DENPYOU_DATE_CTL", string.Empty);
                // 受入取引先CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_1");
                this.SetFieldName("PHY_FILL_COND_CD_1_CTL", string.Empty);
                // 受入業者CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_2");
                this.SetFieldName("PHY_FILL_COND_CD_2_CTL", string.Empty);
                // 受入現場CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_3");
                this.SetFieldName("PHY_FILL_COND_CD_3_CTL", string.Empty);
                // 受入品名CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_4");
                this.SetFieldName("PHY_FILL_COND_CD_4_CTL", string.Empty);
                // 出荷取引先CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_5");
                this.SetFieldName("PHY_FILL_COND_CD_5_CTL", string.Empty);
                // 出荷業者CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_6");
                this.SetFieldName("PHY_FILL_COND_CD_6_CTL", string.Empty);
                // 出荷現場CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_7");
                this.SetFieldName("PHY_FILL_COND_CD_7_CTL", string.Empty);
                // 出荷品名CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_8");
                this.SetFieldName("PHY_FILL_COND_CD_8_CTL", string.Empty);
                // 運搬業者CD範囲
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_9");
                this.SetFieldName("PHY_FILL_COND_CD_9_CTL", string.Empty);
            }

            #endregion - Header -

            #region - Footer -

            if (dataTableFooterTmp.Rows.Count > 0)
            {
                // 受入正味総合計
                index = dataTableFooterTmp.Columns.IndexOf("UKEIRE_SYOUMI_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_UKEIRE_SYOUMI_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_UKEIRE_SYOUMI_TOTAL_FLB", string.Empty);
                }

                // 受入金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("UKEIRE_KINGAKU_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_UKEIRE_KINGAKU_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_UKEIRE_KINGAKU_TOTAL_FLB", string.Empty);
                }

                // 出荷正味総合計
                index = dataTableFooterTmp.Columns.IndexOf("SHUKKA_SYOUMI_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_SHUKKA_SYOUMI_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_SHUKKA_SYOUMI_TOTAL_FLB", string.Empty);
                }

                // 出荷金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("SHUKKA_KINGAKU_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_SHUKKA_KINGAKU_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_SHUKKA_KINGAKU_TOTAL_FLB", string.Empty);
                }

                // 差益金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("SAEKI_KINGAKU_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_SAEKI_KINGAKU_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_SAEKI_KINGAKU_TOTAL_FLB", string.Empty);
                }

                // 運賃金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("UNCHIN_KINGAKU_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_UNCHIN_KINGAKU_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_UNCHIN_KINGAKU_TOTAL_FLB", string.Empty);
                }
            }
            else
            {
                // 受入正味総合計
                index = dataTableFooterTmp.Columns.IndexOf("UKEIRE_SYOUMI_TOTAL");
                this.SetFieldName("PHN_G1F_UKEIRE_SYOUMI_TOTAL_FLB", string.Empty);
                // 受入金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("UKEIRE_KINGAKU_TOTAL");
                this.SetFieldName("PHN_G1F_UKEIRE_KINGAKU_TOTAL_FLB", string.Empty);
                // 出荷正味総合計
                index = dataTableFooterTmp.Columns.IndexOf("SHUKKA_SYOUMI_TOTAL");
                this.SetFieldName("PHN_G1F_SHUKKA_SYOUMI_TOTAL_FLB", string.Empty);
                // 出荷金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("SHUKKA_KINGAKU_TOTAL");
                this.SetFieldName("PHN_G1F_SHUKKA_KINGAKU_TOTAL_FLB", string.Empty);
                // 差益金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("SAEKI_KINGAKU_TOTAL");
                this.SetFieldName("PHN_G1F_SAEKI_KINGAKU_TOTAL_FLB", string.Empty);
                // 運賃金額総合計
                index = dataTableFooterTmp.Columns.IndexOf("UNCHIN_KINGAKU_TOTAL");
                this.SetFieldName("PHN_G1F_UNCHIN_KINGAKU_TOTAL_FLB", string.Empty);
            }

            #endregion - Footer -
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
