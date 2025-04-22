using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace Shougun.Core.Carriage.UnchinSyuukeihyou
{
    #region - Class -

    /// <summary>(R483)運賃集計表を表すクラス・コントロール</summary>
    public class ReportInfoR483 : ReportInfoBase
    {
        #region - Fields -
        private const int ConstMaxDispDetailRowCount = 15;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR483"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR483(WINDOW_ID windowID)
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

            // 会社略称
            dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
            // 拠点名
            dataTableTmp.Columns.Add("KYOTEN_NAME");
            // 伝票日付
            dataTableTmp.Columns.Add("DENPYOU_DATE");
            // 運搬業者
            dataTableTmp.Columns.Add("FILL_COND_CD_1");
            // 荷積業者
            dataTableTmp.Columns.Add("FILL_COND_CD_2");
            // 荷積現場
            dataTableTmp.Columns.Add("FILL_COND_CD_3");
            // 荷降業者
            dataTableTmp.Columns.Add("FILL_COND_CD_4");
            // 荷降現場
            dataTableTmp.Columns.Add("FILL_COND_CD_5");

            if (isPrintH)
            {
                rowTmp = dataTableTmp.NewRow();

                // 会社略称
                rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 拠点名
                rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 伝票日付
                rowTmp["DENPYOU_DATE"] = "2013/12/01 ～ 2013/12/31";
                // 運搬業者
                rowTmp["FILL_COND_CD_1"] = "123456 ～ 123456";
                // 荷積業者
                rowTmp["FILL_COND_CD_2"] = "123456 ～ 123456";
                // 荷積現場
                rowTmp["FILL_COND_CD_3"] = "123456 ～ 123456";
                // 荷降業者
                rowTmp["FILL_COND_CD_4"] = "123456 ～ 123456";
                // 荷降現場
                rowTmp["FILL_COND_CD_5"] = "123456 ～ 123456";
                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            // 運搬業者コード
            dataTableTmp.Columns.Add("UNPAN_GYOUSHA_CD");
            // 運搬業者名
            dataTableTmp.Columns.Add("UNPAN_GYOUSHA_NAME");
            // 荷積業者コード
            dataTableTmp.Columns.Add("NZM_GYOUSHA_CD");
            // 荷積業者名
            dataTableTmp.Columns.Add("NZM_GYOUSHA_NAME");
            // 荷積現場コード
            dataTableTmp.Columns.Add("NZM_GENBA_CD");
            // 荷積現場名
            dataTableTmp.Columns.Add("NZM_GENBA_NAME");
            // 荷降業者コード
            dataTableTmp.Columns.Add("NOS_GYOUSHA_CD");
            // 荷降業者名
            dataTableTmp.Columns.Add("NOS_GYOUSHA_NAME");
            // 荷降現場コード
            dataTableTmp.Columns.Add("NOS_GENBA_CD");
            // 荷降現場名
            dataTableTmp.Columns.Add("NOS_GENBA_NAME");
            // 伝票種類
            dataTableTmp.Columns.Add("DENPYOU_SHURUI");
            // 車種コード
            dataTableTmp.Columns.Add("SHASHU_CD");
            // 車輌コード
            dataTableTmp.Columns.Add("SHARYOU_CD");
            // 運転者コード
            dataTableTmp.Columns.Add("UNTENSHA_CD");
            // 車種名
            dataTableTmp.Columns.Add("SHASHU_NAME");
            // 車輌名
            dataTableTmp.Columns.Add("SHARYOU_NAME");
            // 運転者名
            dataTableTmp.Columns.Add("UNTENSHA_NAME");
            // 品名コード
            dataTableTmp.Columns.Add("HINMEI_CD");
            // 品名
            dataTableTmp.Columns.Add("HINMEI_NAME");
            // 正味(kg)
            dataTableTmp.Columns.Add("SYOUMI");
            // 数量
            dataTableTmp.Columns.Add("SUURYOU");
            // 単位
            dataTableTmp.Columns.Add("UNIT_NAME");
            // 運賃金額
            dataTableTmp.Columns.Add("UNCHIN");
            // 運賃消費税
            dataTableTmp.Columns.Add("TAX");
            // 運賃合計額
            dataTableTmp.Columns.Add("KINGAKU");
            // 正味合計
            dataTableTmp.Columns.Add("SHOUMI_KEI");
            // 運賃金額計
            dataTableTmp.Columns.Add("KINGAKU_KEI");
            // 運賃消費税
            dataTableTmp.Columns.Add("SHOHIZEI_TOTAL");
            // 運賃合計金額
            dataTableTmp.Columns.Add("KINGAKU_TOTAL");

            if (isPrint)
            {
                for (int i = 1; i < 10; i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 運搬業者コード
                        rowTmp["UNPAN_GYOUSHA_CD"] = (i * 100000).ToString();
                        // 運搬業者名
                        rowTmp["UNPAN_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 荷積業者コード
                        rowTmp["NZM_GYOUSHA_CD"] = "1234567890";
                        // 荷積業者名
                        rowTmp["NZM_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 荷積現場コード
                        rowTmp["NZM_GENBA_CD"] = "1234567890";
                        // 荷積現場名
                        rowTmp["NZM_GENBA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 荷降業者コード
                        rowTmp["NOS_GYOUSHA_CD"] = "1234567890";
                        // 荷降業者名
                        rowTmp["NOS_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 荷降現場コード
                        rowTmp["NOS_GENBA_CD"] = "1234567890";
                        // 荷降現場名
                        rowTmp["NOS_GENBA_NAME"] = "あいうえおかきくけこさしすせそ";
                        // 伝票種類
                        rowTmp["DENPYOU_SHURUI"] = "あいうえおかきくけこさしすせそ";
                        // 車種コード
                        rowTmp["SHASHU_CD"] = "1234567890";
                        // 車輌コード
                        rowTmp["SHARYOU_CD"] = "1234567890";
                        // 運転者コード
                        rowTmp["UNTENSHA_CD"] = "1234567890";
                        // 車種名
                        rowTmp["SHASHU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 車輌名
                        rowTmp["SHARYOU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 運転者名
                        rowTmp["UNTENSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 品名コード
                        rowTmp["HINMEI_CD"] = "1234567890";
                        // 品名
                        rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                        // 正味(kg)
                        rowTmp["SYOUMI"] = "123,456,789,000,123";
                        // 数量
                        rowTmp["SUURYOU"] = "123,456,789,000,123";
                        // 単位
                        rowTmp["UNIT_NAME"] = "あいうえおかきくけこ";
                        // 運賃金額
                        rowTmp["UNCHIN"] = "123,456,789,000,123";
                        // 運賃消費税
                        rowTmp["TAX"] = "123,456,789,000,123";
                        // 運賃合計額
                        rowTmp["KINGAKU"] = "123,456,789,000,123";
                        // 正味合計
                        rowTmp["SHOUMI_KEI"] = "123,456,789,000,123";
                        // 運賃金額計
                        rowTmp["KINGAKU_KEI"] = "123,456,789,000,123";
                        // 運賃消費税
                        rowTmp["SHOHIZEI_TOTAL"] = "123,456,789,000,123";
                        // 運賃合計金額
                        rowTmp["KINGAKU_TOTAL"] = "123,456,789,000,123";

                        dataTableTmp.Rows.Add(rowTmp);
                    }
                }
            }

            this.DataTableList.Add("Detail", dataTableTmp);
            #endregion - Detail -

            #region - Footer -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";

            // 正味計
            dataTableTmp.Columns.Add("SHOUMI_SOU_KEI");
            // 金額計
            dataTableTmp.Columns.Add("KINGAKU_SOU_KEI");
            // 消費税
            dataTableTmp.Columns.Add("SHOHIZEI_SOU_TOTAL");
            // 総合計
            dataTableTmp.Columns.Add("KINGAKU_SOU_TOTAL");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 正味計
                rowTmp["SHOUMI_SOU_KEI"] = "123,456,789,000,123";
                // 金額計
                rowTmp["KINGAKU_SOU_KEI"] = "123,456,789,000,123";
                // 消費税
                rowTmp["SHOHIZEI_SOU_TOTAL"] = "123,456,789,000,123";
                // 総合計
                rowTmp["KINGAKU_SOU_TOTAL"] = "123,456,789,000,123";

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

            string strKeys = string.Empty;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region Columns
            
            // 運搬業者コード
            ctrlName = "PHN_UPN_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運搬業者名
            ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷積業者コード
            ctrlName = "PHN_NZM_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷積業者名
            ctrlName = "PHY_NZM_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷積現場コード
            ctrlName = "PHN_NZM_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷積現場名
            ctrlName = "PHY_NZM_GENBA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降業者コード
            ctrlName = "PHN_NOS_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降業者名
            ctrlName = "PHY_NOS_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降現場コード
            ctrlName = "PHN_NOS_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降現場名
            ctrlName = "PHY_NOS_GENBA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 伝票種類
            ctrlName = "PHY_DENPYOU_SHURUI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車種コード
            ctrlName = "PHN_SHASHU_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車輌コード
            ctrlName = "PHN_SHARYOU_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運転者コード
            ctrlName = "PHN_UNTENSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車種名
            ctrlName = "PHY_SHASHU_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 車輌名
            ctrlName = "PHY_SHARYOU_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運転者名
            ctrlName = "PHY_UNTENSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 品名コード
            ctrlName = "PHN_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 品名
            ctrlName = "PHY_HINMEI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 正味(kg)
            ctrlName = "PHY_NET_JUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 数量
            ctrlName = "PHY_SUURYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 単位
            ctrlName = "PHY_UNIT_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃金額
            ctrlName = "PHY_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃消費税
            ctrlName = "PHY_TAX_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃合計額
            ctrlName = "PHY_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 正味合計
            ctrlName = "PHN_G2F_SHOUMI_KEI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃金額計
            ctrlName = "PHN_G2F_KINGAKU_KEI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃消費税
            ctrlName = "PHN_G2F_SHOHIZEI_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運賃合計金額
            ctrlName = "PHN_G2F_KINGAKU_TOTAL_FLB";
            this.dataTable.Columns.Add(ctrlName);

            #endregion Columns

            #region - Detail -

            if (detailMaxCount > 0)
            {
                for (i = detailStart; i < detailMaxCount; i++)
                {
                    row = this.dataTable.NewRow();

                    // 運搬業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_CD");
                    ctrlName = "PHN_UPN_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運搬業者名
                    index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_NAME");
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

                    // 荷積業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("NZM_GYOUSHA_CD");
                    ctrlName = "PHN_NZM_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 荷積業者名
                    index = dataTableDetailTmp.Columns.IndexOf("NZM_GYOUSHA_NAME");
                    ctrlName = "PHY_NZM_GYOUSHA_NAME_FLB";
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

                    // 荷積現場コード
                    index = dataTableDetailTmp.Columns.IndexOf("NZM_GENBA_CD");
                    ctrlName = "PHN_NZM_GENBA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 荷積現場名
                    index = dataTableDetailTmp.Columns.IndexOf("NZM_GENBA_NAME");
                    ctrlName = "PHY_NZM_GENBA_NAME_FLB";
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

                    // 荷降業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("NOS_GYOUSHA_CD");
                    ctrlName = "PHN_NOS_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 荷降業者名
                    index = dataTableDetailTmp.Columns.IndexOf("NOS_GYOUSHA_NAME");
                    ctrlName = "PHY_NOS_GYOUSHA_NAME_FLB";
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

                    // 荷降現場コード
                    index = dataTableDetailTmp.Columns.IndexOf("NOS_GENBA_CD");
                    ctrlName = "PHN_NOS_GENBA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 荷降現場名
                    index = dataTableDetailTmp.Columns.IndexOf("NOS_GENBA_NAME");
                    ctrlName = "PHY_NOS_GENBA_NAME_FLB";
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

                    // 伝票種類
                    index = dataTableDetailTmp.Columns.IndexOf("DENPYOU_SHURUI");
                    ctrlName = "PHY_DENPYOU_SHURUI_FLB";
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

                    // 車種コード
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

                    // 車輌コード
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

                    // 運転者コード
                    index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_CD");
                    ctrlName = "PHN_UNTENSHA_CD_FLB";
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
                    ctrlName = "PHY_SHASHU_NAME_FLB";
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

                    // 車輌名
                    index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_NAME");
                    ctrlName = "PHY_SHARYOU_NAME_FLB";
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

                    // 運転者名
                    index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_NAME");
                    ctrlName = "PHY_UNTENSHA_NAME_FLB";
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

                    // 品名コード
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
                    ctrlName = "PHY_HINMEI_NAME_FLB";
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

                    // 正味(kg)
                    index = dataTableDetailTmp.Columns.IndexOf("SYOUMI");
                    ctrlName = "PHY_NET_JUURYOU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 数量
                    index = dataTableDetailTmp.Columns.IndexOf("SUURYOU");
                    ctrlName = "PHY_SUURYOU_FLB";
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
                    ctrlName = "PHY_UNIT_FLB";
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

                    // 運賃金額
                    index = dataTableDetailTmp.Columns.IndexOf("UNCHIN");
                    ctrlName = "PHY_KINGAKU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運賃消費税
                    index = dataTableDetailTmp.Columns.IndexOf("TAX");
                    ctrlName = "PHY_TAX_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運賃合計額
                    index = dataTableDetailTmp.Columns.IndexOf("KINGAKU");
                    ctrlName = "PHY_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 正味合計
                    index = dataTableDetailTmp.Columns.IndexOf("SHOUMI_KEI");
                    ctrlName = "PHN_G2F_SHOUMI_KEI_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運賃金額計
                    index = dataTableDetailTmp.Columns.IndexOf("KINGAKU_KEI");
                    ctrlName = "PHN_G2F_KINGAKU_KEI_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運賃消費税
                    index = dataTableDetailTmp.Columns.IndexOf("SHOHIZEI_TOTAL");
                    ctrlName = "PHN_G2F_SHOHIZEI_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    // 運賃合計金額
                    index = dataTableDetailTmp.Columns.IndexOf("KINGAKU_TOTAL");
                    ctrlName = "PHN_G2F_KINGAKU_TOTAL_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    this.dataTable.Rows.Add(row);
                }
            }
            else
            {
                row = this.dataTable.NewRow();

                // 運搬業者コード
                index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_CD");
                ctrlName = "PHN_UPN_GYOUSHA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 運搬業者名
                index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_NAME");
                ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 荷積業者コード
                index = dataTableDetailTmp.Columns.IndexOf("NZM_GYOUSHA_CD");
                ctrlName = "PHN_NZM_GYOUSHA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 荷積業者名
                index = dataTableDetailTmp.Columns.IndexOf("NZM_GYOUSHA_NAME");
                ctrlName = "PHY_NZM_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 荷積現場コード
                index = dataTableDetailTmp.Columns.IndexOf("NZM_GENBA_CD");
                ctrlName = "PHN_NZM_GENBA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 荷積現場名
                index = dataTableDetailTmp.Columns.IndexOf("NZM_GENBA_NAME");
                ctrlName = "PHY_NZM_GENBA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 荷降業者コード
                index = dataTableDetailTmp.Columns.IndexOf("NOS_GYOUSHA_CD");
                ctrlName = "PHN_NOS_GYOUSHA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 荷降業者名
                index = dataTableDetailTmp.Columns.IndexOf("NOS_GYOUSHA_NAME");
                ctrlName = "PHY_NOS_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 荷降現場コード
                index = dataTableDetailTmp.Columns.IndexOf("NOS_GENBA_CD");
                ctrlName = "PHN_NOS_GENBA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 荷降現場名
                index = dataTableDetailTmp.Columns.IndexOf("NOS_GENBA_NAME");
                ctrlName = "PHY_NOS_GENBA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 伝票種類
                index = dataTableDetailTmp.Columns.IndexOf("DENPYOU_SHURUI");
                ctrlName = "PHY_DENPYOU_SHURUI_FLB";
                row[ctrlName] = string.Empty;
                // 車種コード
                index = dataTableDetailTmp.Columns.IndexOf("SHASHU_CD");
                ctrlName = "PHN_SHASHU_CD_FLB";
                row[ctrlName] = string.Empty;
                // 車輌コード
                index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_CD");
                ctrlName = "PHN_SHARYOU_CD_FLB";
                row[ctrlName] = string.Empty;
                // 運転者コード
                index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_CD");
                ctrlName = "PHN_UNTENSHA_CD_FLB";
                row[ctrlName] = string.Empty;
                // 車種名
                index = dataTableDetailTmp.Columns.IndexOf("SHASHU_NAME");
                ctrlName = "PHY_SHASHU_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 車輌名
                index = dataTableDetailTmp.Columns.IndexOf("SHARYOU_NAME");
                ctrlName = "PHY_SHARYOU_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 運転者名
                index = dataTableDetailTmp.Columns.IndexOf("UNTENSHA_NAME");
                ctrlName = "PHY_UNTENSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 品名コード
                index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                ctrlName = "PHN_HINMEI_CD_FLB";
                row[ctrlName] = string.Empty;
                // 品名
                index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                ctrlName = "PHY_HINMEI_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 正味(kg)
                index = dataTableDetailTmp.Columns.IndexOf("SYOUMI");
                ctrlName = "PHY_NET_JUURYOU_FLB";
                row[ctrlName] = string.Empty;
                // 数量
                index = dataTableDetailTmp.Columns.IndexOf("SUURYOU");
                ctrlName = "PHY_SUURYOU_FLB";
                row[ctrlName] = string.Empty;
                // 単位
                index = dataTableDetailTmp.Columns.IndexOf("UNIT_NAME");
                ctrlName = "PHY_UNIT_FLB";
                row[ctrlName] = string.Empty;
                // 運賃金額
                index = dataTableDetailTmp.Columns.IndexOf("UNCHIN");
                ctrlName = "PHY_KINGAKU_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 運賃消費税
                index = dataTableDetailTmp.Columns.IndexOf("TAX");
                ctrlName = "PHY_TAX_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 運賃合計額
                index = dataTableDetailTmp.Columns.IndexOf("KINGAKU");
                ctrlName = "PHY_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 正味合計
                index = dataTableDetailTmp.Columns.IndexOf("SHOUMI_KEI");
                ctrlName = "PHN_G2F_SHOUMI_KEI_FLB";
                row[ctrlName] = string.Empty;
                // 運賃金額計
                index = dataTableDetailTmp.Columns.IndexOf("KINGAKU_KEI");
                ctrlName = "PHN_G2F_KINGAKU_KEI_FLB";
                row[ctrlName] = string.Empty;
                // 運賃消費税
                index = dataTableDetailTmp.Columns.IndexOf("SHOHIZEI_TOTAL");
                ctrlName = "PHN_G2F_SHOHIZEI_TOTAL_FLB";
                row[ctrlName] = string.Empty;
                // 運賃合計金額
                index = dataTableDetailTmp.Columns.IndexOf("KINGAKU_TOTAL");
                ctrlName = "PHN_G2F_KINGAKU_TOTAL_FLB";
                row[ctrlName] = string.Empty;

                this.dataTable.Rows.Add(row);
            }

            #endregion -Detail -

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

                // 伝票日付
                index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_DENPYOU_DATE_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_DENPYOU_DATE_CTL", string.Empty);
                }

                // 運搬業者
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_1");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_1_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_1_CTL", string.Empty);
                }

                // 荷積業者
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_2");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_2_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_2_CTL", string.Empty);
                }

                // 荷積現場
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_3");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_3_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_3_CTL", string.Empty);
                }

                // 荷降業者
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_4");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_4_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_4_CTL", string.Empty);
                }

                // 荷降現場
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_5");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_FILL_COND_CD_5_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_FILL_COND_CD_5_CTL", string.Empty);
                }
            }
            else
            {
                // 会社略称
                index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", string.Empty);

                // 拠点名
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME");
                this.SetFieldName("PHY_KYOTEN_NAME_VLB", string.Empty);
                
                // 伝票日付
                index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_DATE");
                this.SetFieldName("PHY_DENPYOU_DATE_CTL", string.Empty);
                
                // 運搬業者
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_1");
                this.SetFieldName("PHY_FILL_COND_CD_1_CTL", string.Empty);
                
                // 荷積業者
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_2");
                this.SetFieldName("PHY_FILL_COND_CD_2_CTL", string.Empty);
                
                // 荷積現場
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_3");
                this.SetFieldName("PHY_FILL_COND_CD_3_CTL", string.Empty);
                
                // 荷降業者
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_4");
                this.SetFieldName("PHY_FILL_COND_CD_4_CTL", string.Empty);
                
                // 荷降現場
                index = dataTableHeaderTmp.Columns.IndexOf("FILL_COND_CD_5");
                this.SetFieldName("PHY_FILL_COND_CD_5_CTL", string.Empty);
            }

            #endregion - Header -

            #region - Footer -

            if (dataTableFooterTmp.Rows.Count > 0)
            {
                // 正味計
                index = dataTableFooterTmp.Columns.IndexOf("SHOUMI_SOU_KEI");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_SHOUMI_KEI_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_SHOUMI_KEI_FLB", string.Empty);
                }

                // 金額計
                index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_SOU_KEI");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_KINGAKU_KEI_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_KINGAKU_KEI_FLB", string.Empty);
                }

                // 消費税
                index = dataTableFooterTmp.Columns.IndexOf("SHOHIZEI_SOU_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_SHOHIZEI_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_SHOHIZEI_TOTAL_FLB", string.Empty);
                }

                // 総合計
                index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_SOU_TOTAL");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_G1F_KINGAKU_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_G1F_KINGAKU_TOTAL_FLB", string.Empty);
                }
            }
            else
            {
                // 正味計
                index = dataTableFooterTmp.Columns.IndexOf("SHOUMI_SOU_KEI");
                this.SetFieldName("PHN_G1F_SHOUMI_KEI_FLB", string.Empty);

                // 金額計
                index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_SOU_KEI");
                this.SetFieldName("PHN_G1F_KINGAKU_KEI_FLB", string.Empty);
                
                // 消費税
                index = dataTableFooterTmp.Columns.IndexOf("SHOHIZEI_SOU_TOTAL");
                this.SetFieldName("PHN_G1F_SHOHIZEI_TOTAL_FLB", string.Empty);
                
                // 総合計
                index = dataTableFooterTmp.Columns.IndexOf("KINGAKU_SOU_TOTAL");
                this.SetFieldName("PHN_G1F_KINGAKU_TOTAL_FLB", string.Empty);
            }

            #endregion - Footer -
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
