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

    /// <summary>(R405)在庫管理表を表すクラス・コントロール</summary>
    public class ReportInfoR405 : ReportInfoBase
    {
        #region - Fields -

        private const int ConstMaxDispDetailRowCount = 20;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR405"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR405(WINDOW_ID windowID)
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

            for (int j = 1; j <= 5; j++)
            {
                #region - Header -
                string strKey = "00000" + j.ToString();
                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Header";

                // 会社名
                dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
                // 発行日
                dataTableTmp.Columns.Add("PRINT_DATE");
                // 対象期間範囲
                dataTableTmp.Columns.Add("UKETSUKE_DATE");
                // 業者コード
                dataTableTmp.Columns.Add("GYOUSHA_CD");
                // 現場コード
                dataTableTmp.Columns.Add("GENBA_CD");
                // 評価方法
                dataTableTmp.Columns.Add("ZAIKO_ASSESSMENT");

                if (isPrintH)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // 会社名
                    rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 発行日
                    rowTmp["PRINT_DATE"] = "b";
                    // 対象期間範囲
                    rowTmp["UKETSUKE_DATE"] = "2013/11/01 ～ 2013/11/30";
                    // 業者コード
                    rowTmp["GYOUSHA_CD"] = strKey;
                    // 現場コード
                    rowTmp["GENBA_CD"] = "123456";
                    // 評価方法
                    rowTmp["ZAIKO_ASSESSMENT"] = "最終仕入れ単価法";

                    dataTableTmp.Rows.Add(rowTmp);
                }

                this.DataTablePageList[strKey] = new Dictionary<string, DataTable>();
                this.DataTablePageList[strKey].Add("Header", dataTableTmp);

                #endregion - Header -

                #region - Detail -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "Detail";

                // 現場コード
                dataTableTmp.Columns.Add("D_GENBA_CD");
                // 現場名
                dataTableTmp.Columns.Add("D_GENBA_MEI");
                // 在庫品名CD
                dataTableTmp.Columns.Add("ZAIKO_HINMEI_CD");
                // 在庫品名
                dataTableTmp.Columns.Add("ZAIKO_HINMEI");
                // 前月残量
                dataTableTmp.Columns.Add("REMAIN_SUU");
                // 当月受入量
                dataTableTmp.Columns.Add("ENTER_SUU");
                // 当月出荷量
                dataTableTmp.Columns.Add("OUT_SUU");
                // 調整量
                dataTableTmp.Columns.Add("ADJUST_SUU");
                // 当月在庫残
                dataTableTmp.Columns.Add("TOTAL_SUU");
                // 評価単価
                dataTableTmp.Columns.Add("TANKA");
                // 在庫金額
                dataTableTmp.Columns.Add("KINGAKU");

                if (isPrint)
                {
                    for (int i = 0; i < 16 + (j * 2); i++)
                    {
                        rowTmp = dataTableTmp.NewRow();

                        // 現場コード
                        rowTmp["D_GENBA_CD"] = (i + 1).ToString();
                        // 現場名
                        rowTmp["D_GENBA_MEI"] = "あいうえおかきくけこさしすせそ";
                        // 在庫品名CD
                        rowTmp["ZAIKO_HINMEI_CD"] = "1234567890";
                        // 在庫品名
                        rowTmp["ZAIKO_HINMEI"] = "あいうえおかきくけこさしすせそ";
                        // 前月残量
                        rowTmp["REMAIN_SUU"] = "1,234,567,890";
                        // 当月受入量
                        rowTmp["ENTER_SUU"] = "1,234,567,890";
                        // 当月出荷量
                        rowTmp["OUT_SUU"] = "1,234,567,890";
                        // 調整量
                        rowTmp["ADJUST_SUU"] = "1,234,567,890";
                        // 当月在庫残
                        rowTmp["TOTAL_SUU"] = "1,234,567,890";
                        // 評価単価
                        rowTmp["TANKA"] = "1,234,567,890";
                        // 在庫金額
                        rowTmp["KINGAKU"] = "1,234,567,890";

                        dataTableTmp.Rows.Add(rowTmp);
                    }
                }

                this.DataTablePageList[strKey].Add("Detail", dataTableTmp);

                #endregion - Detail -

                #region - Group Footer -

                dataTableTmp = new DataTable();
                dataTableTmp.TableName = "GroupFooter";

                // 前月残量合計値
                dataTableTmp.Columns.Add("ALL_REMAIN_SUU");
                // 当月受入量計値
                dataTableTmp.Columns.Add("ALL_ENTER_SUU");
                // 当月出荷量計値
                dataTableTmp.Columns.Add("ALL_OUT_SUU");
                // 調整量計値
                dataTableTmp.Columns.Add("ALL_ADJUST_SUU");
                // 当月在庫残計値
                dataTableTmp.Columns.Add("ALL_TOTAL_SUU");
                // 在庫金額計値
                dataTableTmp.Columns.Add("ALL_KINGAKU");

                if (isPrint)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // 前月残量合計値
                    rowTmp["ALL_REMAIN_SUU"] = "1,234,567,890";
                    // 当月受入量計値
                    rowTmp["ALL_ENTER_SUU"] = "1,234,567,890";
                    // 当月出荷量計値
                    rowTmp["ALL_OUT_SUU"] = "1,234,567,890";
                    // 調整量計値
                    rowTmp["ALL_ADJUST_SUU"] = "1,234,567,890";
                    // 当月在庫残計値
                    rowTmp["ALL_TOTAL_SUU"] = "1,234,567,890";
                    // 在庫金額計値
                    rowTmp["ALL_KINGAKU"] = "1,234,567,890";

                    dataTableTmp.Rows.Add(rowTmp);
                }

                this.DataTablePageList[strKey].Add("GroupFooter", dataTableTmp);
                #endregion - Group Footer -

                if ((!isPrint) && (!isPrintH))
                {
                    break;
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
            DataTable dataTableGroupFooterTmp = null;
            string ctrlName = string.Empty;

            bool detailComp = false;
            int detailMaxCount = 0;
            int detailStart = 0;
            int rowNo;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region Columns
            // 会社名
            ctrlName = "PHN_CORP_RYAKU_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 発行日
            ctrlName = "PHY_PRINT_DATE_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 対象期間範囲
            ctrlName = "PHY_UKETSUKE_DATE_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 業者コード
            ctrlName = "PHY_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場コード
            ctrlName = "PHY_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 評価方法
            ctrlName = "PHY_ZAIKO_ASSESSMENT_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場コード
            ctrlName = "PHN_D_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場名
            ctrlName = "PHY_GENBA_LABEL_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 在庫品名CD
            ctrlName = "PHN_ZAIKO_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 在庫品名
            ctrlName = "PHY_ZAIKO_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 前月残量
            ctrlName = "PHY_REMAIN_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 当月受入量
            ctrlName = "PHY_ENTER_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 当月出荷量
            ctrlName = "PHY_OUT_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 調整量
            ctrlName = "PHY_ADJUST_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 当月在庫残
            ctrlName = "PHY_TOTAL_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 評価単価
            ctrlName = "PHY_TANKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 在庫金額
            ctrlName = "PHY_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 前月残量合計値
            ctrlName = "PHN_ALL_REMAIN_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 当月受入量計値
            ctrlName = "PHN_ALL_ENTER_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 当月出荷量計値
            ctrlName = "PHN_ALL_OUT_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 調整量計値
            ctrlName = "PHN_ALL_ADJUST_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 当月在庫残計値
            ctrlName = "PHN_ALL_TOTAL_SUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 在庫金額計値
            ctrlName = "PHN_ALL_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            #endregion - Columns -

            var varKey = DataTablePageList.Keys;
            foreach (string strKey in varKey)
            {
                dataTableHeaderTmp = this.DataTablePageList[strKey]["Header"];
                dataTableDetailTmp = this.DataTablePageList[strKey]["Detail"];
                dataTableGroupFooterTmp = this.DataTablePageList[strKey]["GroupFooter"];
                detailMaxCount = dataTableDetailTmp.Rows.Count;
                detailComp = false;
                rowNo = 1;

                int loopCount = (int)Math.Ceiling((double)dataTableDetailTmp.Rows.Count / ConstMaxDispDetailRowCount) * ConstMaxDispDetailRowCount;

                if ((dataTableDetailTmp.Rows.Count % ConstMaxDispDetailRowCount) != 0)
                {
                    loopCount--;
                }

                if (loopCount >= 0)
                {
                    for (i = detailStart; i < loopCount; i++)
                    {
                        row = this.dataTable.NewRow();

                        #region - Header -

                        // 会社名
                        index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                        ctrlName = "PHN_CORP_RYAKU_NAME_FLB";
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

                        // 発行日
                        index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                        ctrlName = "PHY_PRINT_DATE_VLB";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 対象期間範囲
                        index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_DATE");
                        ctrlName = "PHY_UKETSUKE_DATE_FLB";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 業者コード
                        index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                        ctrlName = "PHY_GYOUSHA_CD_FLB";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 現場コード
                        index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                        ctrlName = "PHY_GENBA_CD_FLB";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 評価方法
                        index = dataTableHeaderTmp.Columns.IndexOf("ZAIKO_ASSESSMENT");
                        ctrlName = "PHY_ZAIKO_ASSESSMENT_FLB";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        #endregion - Header -

                        #region - Detail -

                        if (!detailComp)
                        {
                            // 現場コード
                            index = dataTableDetailTmp.Columns.IndexOf("D_GENBA_CD");
                            ctrlName = "PHN_D_GENBA_CD_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場名
                            index = dataTableDetailTmp.Columns.IndexOf("D_GENBA_MEI");
                            ctrlName = "PHY_GENBA_LABEL_FLB";
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

                            // 在庫品名CD
                            index = dataTableDetailTmp.Columns.IndexOf("ZAIKO_HINMEI_CD");
                            ctrlName = "PHN_ZAIKO_HINMEI_CD_FLB";
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

                            // 在庫品名
                            index = dataTableDetailTmp.Columns.IndexOf("ZAIKO_HINMEI");
                            ctrlName = "PHY_ZAIKO_HINMEI_CD_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 前月残量
                            index = dataTableDetailTmp.Columns.IndexOf("REMAIN_SUU");
                            ctrlName = "PHY_REMAIN_SUU_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 当月受入量
                            index = dataTableDetailTmp.Columns.IndexOf("ENTER_SUU");
                            ctrlName = "PHY_ENTER_SUU_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 当月出荷量
                            index = dataTableDetailTmp.Columns.IndexOf("OUT_SUU");
                            ctrlName = "PHY_OUT_SUU_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 調整量
                            index = dataTableDetailTmp.Columns.IndexOf("ADJUST_SUU");
                            ctrlName = "PHY_ADJUST_SUU_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 当月在庫残
                            index = dataTableDetailTmp.Columns.IndexOf("TOTAL_SUU");
                            ctrlName = "PHY_TOTAL_SUU_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 評価単価
                            index = dataTableDetailTmp.Columns.IndexOf("TANKA");
                            ctrlName = "PHY_TANKA_FLB";
                            if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 在庫金額
                            index = dataTableDetailTmp.Columns.IndexOf("KINGAKU");
                            ctrlName = "PHY_KINGAKU_FLB";
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
                            // 現場コード
                            index = dataTableDetailTmp.Columns.IndexOf("D_GENBA_CD");
                            ctrlName = "PHN_D_GENBA_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 現場名
                            index = dataTableDetailTmp.Columns.IndexOf("D_GENBA_MEI");
                            ctrlName = "PHY_GENBA_LABEL_FLB";
                            row[ctrlName] = string.Empty;
                            // 在庫品名CD
                            index = dataTableDetailTmp.Columns.IndexOf("ZAIKO_HINMEI_CD");
                            ctrlName = "PHN_ZAIKO_HINMEI_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 在庫品名
                            index = dataTableDetailTmp.Columns.IndexOf("ZAIKO_HINMEI");
                            ctrlName = "PHY_ZAIKO_HINMEI_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 前月残量
                            index = dataTableDetailTmp.Columns.IndexOf("REMAIN_SUU");
                            ctrlName = "PHY_REMAIN_SUU_FLB";
                            row[ctrlName] = string.Empty;
                            // 当月受入量
                            index = dataTableDetailTmp.Columns.IndexOf("ENTER_SUU");
                            ctrlName = "PHY_ENTER_SUU_FLB";
                            row[ctrlName] = string.Empty;
                            // 当月出荷量
                            index = dataTableDetailTmp.Columns.IndexOf("OUT_SUU");
                            ctrlName = "PHY_OUT_SUU_FLB";
                            row[ctrlName] = string.Empty;
                            // 調整量
                            index = dataTableDetailTmp.Columns.IndexOf("ADJUST_SUU");
                            ctrlName = "PHY_ADJUST_SUU_FLB";
                            row[ctrlName] = string.Empty;
                            // 当月在庫残
                            index = dataTableDetailTmp.Columns.IndexOf("TOTAL_SUU");
                            ctrlName = "PHY_TOTAL_SUU_FLB";
                            row[ctrlName] = string.Empty;
                            // 評価単価
                            index = dataTableDetailTmp.Columns.IndexOf("TANKA");
                            ctrlName = "PHY_TANKA_FLB";
                            row[ctrlName] = string.Empty;
                            // 在庫金額
                            index = dataTableDetailTmp.Columns.IndexOf("KINGAKU");
                            ctrlName = "PHY_KINGAKU_FLB";
                            row[ctrlName] = string.Empty;
                        }

                        #endregion - Detail -

                        #region - Group Footer -

                        // 前月残量合計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_REMAIN_SUU");
                        ctrlName = "PHN_ALL_REMAIN_SUU_FLB";
                        if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 当月受入量計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_ENTER_SUU");
                        ctrlName = "PHN_ALL_ENTER_SUU_FLB";
                        if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 当月出荷量計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_OUT_SUU");
                        ctrlName = "PHN_ALL_OUT_SUU_FLB";
                        if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 調整量計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_ADJUST_SUU");
                        ctrlName = "PHN_ALL_ADJUST_SUU_FLB";
                        if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 当月在庫残計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_TOTAL_SUU");
                        ctrlName = "PHN_ALL_TOTAL_SUU_FLB";
                        if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        // 在庫金額計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_KINGAKU");
                        ctrlName = "PHN_ALL_KINGAKU_FLB";
                        if (!this.IsDBNull(dataTableGroupFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = dataTableGroupFooterTmp.Rows[0].ItemArray[index];
                        }
                        else
                        {
                            row[ctrlName] = string.Empty;
                        }

                        #endregion - Group Footer -

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
                }
                else
                {
                    for (i = detailStart; i < ConstMaxDispDetailRowCount - 1; i++)
                    {
                        row = this.dataTable.NewRow();

                        #region - Header -

                        if (dataTableHeaderTmp.Rows.Count > 0)
                        {
                            // 会社名
                            index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = "PHN_CORP_RYAKU_NAME_FLB";
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

                            // 発行日
                            index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                            ctrlName = "PHY_PRINT_DATE_VLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 対象期間範囲
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_DATE");
                            ctrlName = "PHY_UKETSUKE_DATE_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 業者コード
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHY_GYOUSHA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 現場コード
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHY_GENBA_CD_FLB";
                            if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                            {
                                row[ctrlName] = dataTableHeaderTmp.Rows[0].ItemArray[index];
                            }
                            else
                            {
                                row[ctrlName] = string.Empty;
                            }

                            // 評価方法
                            index = dataTableHeaderTmp.Columns.IndexOf("ZAIKO_ASSESSMENT");
                            ctrlName = "PHY_ZAIKO_ASSESSMENT_FLB";
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
                            // 会社名
                            index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                            ctrlName = "PHN_CORP_RYAKU_NAME_FLB";
                            row[ctrlName] = string.Empty;
                            // 発行日
                            index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                            ctrlName = "PHY_PRINT_DATE_VLB";
                            row[ctrlName] = string.Empty;
                            // 対象期間範囲
                            index = dataTableHeaderTmp.Columns.IndexOf("UKETSUKE_DATE");
                            ctrlName = "PHY_UKETSUKE_DATE_FLB";
                            row[ctrlName] = string.Empty;
                            // 業者コード
                            index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                            ctrlName = "PHY_GYOUSHA_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 現場コード
                            index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                            ctrlName = "PHY_GENBA_CD_FLB";
                            row[ctrlName] = string.Empty;
                            // 評価方法
                            index = dataTableHeaderTmp.Columns.IndexOf("ZAIKO_ASSESSMENT");
                            ctrlName = "PHY_ZAIKO_ASSESSMENT_FLB";
                            row[ctrlName] = string.Empty;
                        }

                        #endregion - Header -

                        #region - Detail -

                        // 現場コード
                        index = dataTableDetailTmp.Columns.IndexOf("D_GENBA_CD");
                        ctrlName = "PHN_D_GENBA_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 現場名
                        index = dataTableDetailTmp.Columns.IndexOf("D_GENBA_MEI");
                        ctrlName = "PHY_GENBA_LABEL_FLB";
                        row[ctrlName] = string.Empty;
                        // 在庫品名CD
                        index = dataTableDetailTmp.Columns.IndexOf("ZAIKO_HINMEI_CD");
                        ctrlName = "PHN_ZAIKO_HINMEI_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 在庫品名
                        index = dataTableDetailTmp.Columns.IndexOf("ZAIKO_HINMEI");
                        ctrlName = "PHY_ZAIKO_HINMEI_CD_FLB";
                        row[ctrlName] = string.Empty;
                        // 前月残量
                        index = dataTableDetailTmp.Columns.IndexOf("REMAIN_SUU");
                        ctrlName = "PHY_REMAIN_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 当月受入量
                        index = dataTableDetailTmp.Columns.IndexOf("ENTER_SUU");
                        ctrlName = "PHY_ENTER_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 当月出荷量
                        index = dataTableDetailTmp.Columns.IndexOf("OUT_SUU");
                        ctrlName = "PHY_OUT_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 調整量
                        index = dataTableDetailTmp.Columns.IndexOf("ADJUST_SUU");
                        ctrlName = "PHY_ADJUST_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 当月在庫残
                        index = dataTableDetailTmp.Columns.IndexOf("TOTAL_SUU");
                        ctrlName = "PHY_TOTAL_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 評価単価
                        index = dataTableDetailTmp.Columns.IndexOf("TANKA");
                        ctrlName = "PHY_TANKA_FLB";
                        row[ctrlName] = string.Empty;
                        // 在庫金額
                        index = dataTableDetailTmp.Columns.IndexOf("KINGAKU");
                        ctrlName = "PHY_KINGAKU_FLB";
                        row[ctrlName] = string.Empty;

                        #endregion - Detail -

                        #region - Group Footer -

                        // 前月残量合計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_REMAIN_SUU");
                        ctrlName = "PHN_ALL_REMAIN_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 当月受入量計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_ENTER_SUU");
                        ctrlName = "PHN_ALL_ENTER_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 当月出荷量計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_OUT_SUU");
                        ctrlName = "PHN_ALL_OUT_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 調整量計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_ADJUST_SUU");
                        ctrlName = "PHN_ALL_ADJUST_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 当月在庫残計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_TOTAL_SUU");
                        ctrlName = "PHN_ALL_TOTAL_SUU_FLB";
                        row[ctrlName] = string.Empty;
                        // 在庫金額計値
                        index = dataTableGroupFooterTmp.Columns.IndexOf("ALL_KINGAKU");
                        ctrlName = "PHN_ALL_KINGAKU_FLB";
                        row[ctrlName] = string.Empty;

                        #endregion - Group Footer -

                        this.dataTable.Rows.Add(row);
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
