using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Report
{
    #region - Class -

    /// <summary>(R424)単価変更対象一覧表を表すクラス・コントロール</summary>
    public class ReportInfoR424 : ReportInfoBase
    {
        #region - Fields -

        private const int ConstMaxDispDetailRowCount = 18;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR424"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR424(WINDOW_ID windowID)
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
            // 伝種区分
            dataTableTmp.Columns.Add("DENSHU_KBN_NAME");
            // 伝票区分
            dataTableTmp.Columns.Add("DENPYOU_KBN_NAME");
            // 取引先コード
            dataTableTmp.Columns.Add("H_TORIHIKISAKI_CD");
            // 業者コード
            dataTableTmp.Columns.Add("H_GYOUSHA_CD");
            // 現場コード
            dataTableTmp.Columns.Add("H_GENBA_CD");
            // 運搬業者コード
            dataTableTmp.Columns.Add("H_UNPAN_GYOUSHA_CD");
            // 荷降業者コード
            dataTableTmp.Columns.Add("H_NIOROSHI_GYOUSHA_CD");
            // 荷降現場コード
            dataTableTmp.Columns.Add("H_NIOROSHI_GENBA_CD");
            // 種類コード
            dataTableTmp.Columns.Add("H_SHURUI_CD");
            // 分類コード
            dataTableTmp.Columns.Add("H_BUNRUI_CD");
            // 品名コード
            dataTableTmp.Columns.Add("H_HINMEI_CD");
            // 単位コード
            dataTableTmp.Columns.Add("H_UNIT_CD");

            if (isPrintH)
            {
                rowTmp = dataTableTmp.NewRow();

                // 会社略称
                rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 拠点名
                rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 伝種区分
                rowTmp["DENSHU_KBN_NAME"] = "あいうえおかきくけこ";
                // 伝票区分
                rowTmp["DENPYOU_KBN_NAME"] = "あいうえおかきくけこ";
                // 取引先コード
                rowTmp["H_TORIHIKISAKI_CD"] = "1234567890";
                // 業者コード
                rowTmp["H_GYOUSHA_CD"] = "1234567890";
                // 現場コード
                rowTmp["H_GENBA_CD"] = "1234567890";
                // 運搬業者コード
                rowTmp["H_UNPAN_GYOUSHA_CD"] = "1234567890";
                // 荷降業者コード
                rowTmp["H_NIOROSHI_GYOUSHA_CD"] = "1234567890";
                // 荷降現場コード
                rowTmp["H_NIOROSHI_GENBA_CD"] = "1234567890";
                // 種類コード
                rowTmp["H_SHURUI_CD"] = "1234567890";
                // 分類コード
                rowTmp["H_BUNRUI_CD"] = "1234567890";
                // 品名コード
                rowTmp["H_HINMEI_CD"] = "1234567890";
                // 単位コード
                rowTmp["H_UNIT_CD"] = "1234567890";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            // 取引先コード
            dataTableTmp.Columns.Add("TORIHIKISAKI_CD");
            // 取引先名
            dataTableTmp.Columns.Add("TORIHIKISAKI_NAME");
            // 現場コード
            dataTableTmp.Columns.Add("GENBA_CD");
            // 現場名
            dataTableTmp.Columns.Add("GENBA_NAME");
            // 荷降業者コード
            dataTableTmp.Columns.Add("NIOROSHI_GYOUSHA_CD");
            // 荷降業者名
            dataTableTmp.Columns.Add("NIOROSHI_GYOUSHA_NAME");
            // 種類コード
            dataTableTmp.Columns.Add("SHURUI_CD");
            // 種類名
            dataTableTmp.Columns.Add("SHURUI_NAME");
            // 品名コード
            dataTableTmp.Columns.Add("HINMEI_CD");
            // 品名
            dataTableTmp.Columns.Add("HINMEI_NAME");
            // 単位
            dataTableTmp.Columns.Add("UNIT_NAME");
            // 適用開始日
            dataTableTmp.Columns.Add("TEKIYOU_BEGIN");
            // 業者コード
            dataTableTmp.Columns.Add("GYOUSHA_CD");
            // 業者名
            dataTableTmp.Columns.Add("GYOUSHA_NAME");
            // 運搬業者コード
            dataTableTmp.Columns.Add("UNPAN_GYOUSHA_CD");
            // 運搬業者名
            dataTableTmp.Columns.Add("UNPAN_GYOUSHA_NAME");
            // 荷降現場コード
            dataTableTmp.Columns.Add("NIOROSHI_GENBA_CD");
            // 荷降現場名
            dataTableTmp.Columns.Add("NIOROSHI_GENBA_NAME");
            // 分類コード
            dataTableTmp.Columns.Add("BUNRUI_CD");
            // 分類名
            dataTableTmp.Columns.Add("BUNRUI_NAME");
            // 単価
            dataTableTmp.Columns.Add("TANKA");
            // 増減単価
            dataTableTmp.Columns.Add("ZOUGEN_TANKA");
            // 適用単価
            dataTableTmp.Columns.Add("TEKIYOU_TANKA");

            if (isPrint)
            {
                for (int i = 0; i < 21; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // 取引先コード
                    rowTmp["TORIHIKISAKI_CD"] = "1234567890";
                    // 取引先名
                    rowTmp["TORIHIKISAKI_NAME"] = "1234567890";
                    // 現場コード
                    rowTmp["GENBA_CD"] = "1234567890";
                    // 現場名
                    rowTmp["GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 荷降業者コード
                    rowTmp["NIOROSHI_GYOUSHA_CD"] = "1234567890";
                    // 荷降業者名
                    rowTmp["NIOROSHI_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 種類コード
                    rowTmp["SHURUI_CD"] = "1234567890";
                    // 種類名
                    rowTmp["SHURUI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 品名コード 
                    rowTmp["HINMEI_CD"] = "1234567890";
                    // 品名
                    rowTmp["HINMEI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 単位
                    rowTmp["UNIT_NAME"] = "あいうえおかきくけこ";
                    // 適用開始日
                    rowTmp["TEKIYOU_BEGIN"] = "2013/12/06 12:00:00";
                    // 業者コード
                    rowTmp["GYOUSHA_CD"] = "1234567890";
                    // 業者名
                    rowTmp["GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 運搬業者コード
                    rowTmp["UNPAN_GYOUSHA_CD"] = "1234567890";
                    // 運搬業者名
                    rowTmp["UNPAN_GYOUSHA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 荷降現場コード
                    rowTmp["NIOROSHI_GENBA_CD"] = "1234567890";
                    // 荷降現場名
                    rowTmp["NIOROSHI_GENBA_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 分類コード
                    rowTmp["BUNRUI_CD"] = "1234567890";
                    // 分類名
                    rowTmp["BUNRUI_NAME"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                    // 単価
                    rowTmp["TANKA"] = "1,234,567,890";
                    // 増減単価
                    rowTmp["ZOUGEN_TANKA"] = "1,234,567,890";
                    // 単価単価
                    rowTmp["TEKIYOU_TANKA"] = "1,234,567,890";

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }

            this.DataTableList.Add("Detail", dataTableTmp);
            #endregion - Detail -
        }

        /// <summary>詳細情報作成処理を実行する</summary>
        protected override void CreateDataTableInfo()
        {
            int index;
            int i;
            DataRow row;
            DataTable dataTableDetailTmp = this.DataTableList["Detail"];
            string ctrlName = string.Empty;

            bool detailComp = false;
            int detailMaxCount = dataTableDetailTmp.Rows.Count;
            int detailStart = 0;
            int rowNo = 1;
            int maxPage = (int)Math.Ceiling((double)detailMaxCount / ConstMaxDispDetailRowCount);

            if (maxPage == 0)
            {
                maxPage = 1;
                detailComp = true;
            }

            int maxRow = maxPage * ConstMaxDispDetailRowCount;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region Columns
            // 取引先コード
            ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 取引先名
            ctrlName = "PHY_TORIHIKISAKI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場コード
            ctrlName = "PHN_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 現場名
            ctrlName = "PHY_GENBA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降業者コード
            ctrlName = "PHN_NIOROSHI_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降業者名
            ctrlName = "PHY_NIOROSHI_GYOUSHA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 種類コード
            ctrlName = "PHN_SHURUI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 種類名
            ctrlName = "PHY_SHURUI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 品名コード
            ctrlName = "PHN_HINMEI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 品名
            ctrlName = "PHY_HINMEI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 単位
            ctrlName = "PHY_UNIT_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 適用開始日
            ctrlName = "PHY_TEKIYOU_BEGIN_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 業者コード
            ctrlName = "PHN_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 業者名
            ctrlName = "PHY_GYOUSHA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運搬業者コード
            ctrlName = "PHN_UNPAN_GYOUSHA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 運搬業者名
            ctrlName = "PHY_UNPAN_GYOUSHA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降現場コード
            ctrlName = "PHN_NIOROSHI_GENBA_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 荷降現場名
            ctrlName = "PHY_NIOROSHI_GENBA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 分類コード
            ctrlName = "PHN_BUNRUI_CD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 分類名
            ctrlName = "PHY_BUNRUI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 単価
            ctrlName = "PHY_TANKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 増減単価
            ctrlName = "PHY_ZOUGEN_TANKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 適用単価
            ctrlName = "PHY_TEKIYOU_TANKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            #endregion Columns

            for (i = detailStart; i < maxRow; i++)
            {
                row = this.dataTable.NewRow();

                if (!detailComp)
                {
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
                    // 取引先名
                    //index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI");
                    ctrlName = "PHY_TORIHIKISAKI_FLB";
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
                    //index = dataTableDetailTmp.Columns.IndexOf("GENBA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("GENBA");
                    ctrlName = "PHY_GENBA_FLB";
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
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                    ctrlName = "PHN_NIOROSHI_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 荷降業者名
                    //index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GYOUSHA");
                    ctrlName = "PHY_NIOROSHI_GYOUSHA_FLB";
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
                    // 種類コード
                    index = dataTableDetailTmp.Columns.IndexOf("SHURUI_CD");
                    ctrlName = "PHN_SHURUI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 種類名
                    //index = dataTableDetailTmp.Columns.IndexOf("SHURUI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("SHURUI");
                    ctrlName = "PHY_SHURUI_FLB";
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
                    //index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("HINMEI");
                    ctrlName = "PHY_HINMEI_FLB";
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
                    // 単位
                    //index = dataTableDetailTmp.Columns.IndexOf("UNIT_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("UNIT");
                    ctrlName = "PHY_UNIT_FLB";
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
                    // 適用開始日
                    index = dataTableDetailTmp.Columns.IndexOf("TEKIYOU_BEGIN");
                    ctrlName = "PHY_TEKIYOU_BEGIN_FLB";
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
                    //index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA");
                    ctrlName = "PHY_GYOUSHA_FLB";
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
                    // 運搬業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_CD");
                    ctrlName = "PHN_UNPAN_GYOUSHA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 運搬業者名
                    //index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA");
                    ctrlName = "PHY_UNPAN_GYOUSHA_FLB";
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
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GENBA_CD");
                    ctrlName = "PHN_NIOROSHI_GENBA_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 荷降現場名
                    //index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GENBA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GENBA");
                    ctrlName = "PHY_NIOROSHI_GENBA_FLB";
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
                    // 分類コード
                    index = dataTableDetailTmp.Columns.IndexOf("BUNRUI_CD");
                    ctrlName = "PHN_BUNRUI_CD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 分類名
                    //index = dataTableDetailTmp.Columns.IndexOf("BUNRUI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("BUNRUI");
                    ctrlName = "PHY_BUNRUI_FLB";
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
                    // 単価
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
                    // 増減単価
                    index = dataTableDetailTmp.Columns.IndexOf("ZOUGEN_TANKA");
                    ctrlName = "PHY_ZOUGEN_TANKA_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 適用単価
                    index = dataTableDetailTmp.Columns.IndexOf("TEKIYOU_TANKA");
                    ctrlName = "PHY_TEKIYOU_TANKA_FLB";
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
                    // 取引先コード
                    index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                    ctrlName = "PHN_TORIHIKISAKI_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 取引先名
                    //index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("TORIHIKISAKI");
                    ctrlName = "PHY_TORIHIKISAKI_FLB";
                    row[ctrlName] = string.Empty;
                    // 現場コード
                    index = dataTableDetailTmp.Columns.IndexOf("GENBA_CD");
                    ctrlName = "PHN_GENBA_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 現場名
                    //index = dataTableDetailTmp.Columns.IndexOf("GENBA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("GENBA");
                    ctrlName = "PHY_GENBA_FLB";
                    row[ctrlName] = string.Empty;
                    // 荷降業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                    ctrlName = "PHN_NIOROSHI_GYOUSHA_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 荷降業者名
                    //index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GYOUSHA");
                    ctrlName = "PHY_NIOROSHI_GYOUSHA_FLB";
                    row[ctrlName] = string.Empty;
                    // 種類コード
                    index = dataTableDetailTmp.Columns.IndexOf("SHURUI_CD");
                    ctrlName = "PHN_SHURUI_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 種類名
                    //index = dataTableDetailTmp.Columns.IndexOf("SHURUI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("SHURUI");
                    ctrlName = "PHY_SHURUI_FLB";
                    row[ctrlName] = string.Empty;
                    // 品名コード
                    index = dataTableDetailTmp.Columns.IndexOf("HINMEI_CD");
                    ctrlName = "PHN_HINMEI_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 品名
                    //index = dataTableDetailTmp.Columns.IndexOf("HINMEI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("HINMEI");
                    ctrlName = "PHY_HINMEI_FLB";
                    row[ctrlName] = string.Empty;
                    // 単位
                    //index = dataTableDetailTmp.Columns.IndexOf("UNIT_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("UNIT");
                    ctrlName = "PHY_UNIT_FLB";
                    row[ctrlName] = string.Empty;
                    // 適用開始日
                    index = dataTableDetailTmp.Columns.IndexOf("TEKIYOU_BEGIN");
                    ctrlName = "PHY_TEKIYOU_BEGIN_FLB";
                    row[ctrlName] = string.Empty;
                    // 業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_CD");
                    ctrlName = "PHN_GYOUSHA_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 業者名
                    //index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("GYOUSHA");
                    ctrlName = "PHY_GYOUSHA_FLB";
                    row[ctrlName] = string.Empty;
                    // 運搬業者コード
                    index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_CD");
                    ctrlName = "PHN_UNPAN_GYOUSHA_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 運搬業者名
                    //index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("UNPAN_GYOUSHA");
                    ctrlName = "PHY_UNPAN_GYOUSHA_FLB";
                    row[ctrlName] = string.Empty;
                    // 荷降現場コード
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GENBA_CD");
                    ctrlName = "PHN_NIOROSHI_GENBA_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 荷降現場名
                    //index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GENBA_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("NIOROSHI_GENBA");
                    ctrlName = "PHY_NIOROSHI_GENBA_FLB";
                    row[ctrlName] = string.Empty;
                    // 分類コード
                    index = dataTableDetailTmp.Columns.IndexOf("BUNRUI_CD");
                    ctrlName = "PHN_BUNRUI_CD_FLB";
                    row[ctrlName] = string.Empty;
                    // 分類名
                    //index = dataTableDetailTmp.Columns.IndexOf("BUNRUI_NAME");
                    index = dataTableDetailTmp.Columns.IndexOf("BUNRUI");
                    ctrlName = "PHY_BUNRUI_FLB";
                    row[ctrlName] = string.Empty;
                    // 単価
                    index = dataTableDetailTmp.Columns.IndexOf("TANKA");
                    ctrlName = "PHY_TANKA_FLB";
                    row[ctrlName] = string.Empty;
                    // 増減単価
                    index = dataTableDetailTmp.Columns.IndexOf("ZOUGEN_TANKA");
                    ctrlName = "PHY_ZOUGEN_TANKA_FLB";
                    row[ctrlName] = string.Empty;
                    // 適用単価
                    index = dataTableDetailTmp.Columns.IndexOf("TEKIYOU_TANKA");
                    ctrlName = "PHY_TEKIYOU_TANKA_FLB";
                    row[ctrlName] = string.Empty;
                }

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

                //// 発行日時
                //index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                //this.SetFieldName("PHY_PRINT_DATE_VLB", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);

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

                // 伝種区分
                index = dataTableHeaderTmp.Columns.IndexOf("DENSHU_KBN_NAME");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_DENSHU_KBN_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_DENSHU_KBN_NAME_CTL", string.Empty);
                }

                // 伝票区分
                index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_KBN_NAME");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_DENPYOU_KBN_NAME_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_DENPYOU_KBN_NAME_CTL", string.Empty);
                }

                // 取引先コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_TORIHIKISAKI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_TORIHIKISAKI_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_TORIHIKISAKI_CD_CTL", string.Empty);
                }

                // 業者コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_GYOUSHA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_GYOUSHA_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_GYOUSHA_CD_CTL", string.Empty);
                }

                // 現場コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_GENBA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_GENBA_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_GENBA_CD_CTL", string.Empty);
                }

                // 運搬業者コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_UNPAN_GYOUSHA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("UNPAN_GYOUSHA_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_UNPAN_GYOUSHA_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_UNPAN_GYOUSHA_CD_CTL", string.Empty);
                }

                // 荷降業者コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_NIOROSHI_GYOUSHA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_NIOROSHI_GYOUSHA_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_NIOROSHI_GYOUSHA_CD_CTL", string.Empty);
                }

                // 荷降現場コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_NIOROSHI_GENBA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GENBA_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_NIOROSHI_GENBA_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_NIOROSHI_GENBA_CD_CTL", string.Empty);
                }

                // 種類コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_SHURUI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("SHURUI_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_SHURUI_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_SHURUI_CD_CTL", string.Empty);
                }

                // 分類コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_BUNRUI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("BUNRUI_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_BUNRUI_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_BUNRUI_CD_CTL", string.Empty);
                }

                // 品名コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_HINMEI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("HINMEI_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_HINMEI_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_HINMEI_CD_CTL", string.Empty);
                }

                // 単位コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_UNIT_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("UNIT_CD");
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHY_UNIT_CD_CTL", (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHY_UNIT_CD_CTL", string.Empty);
                }
            }
            else
            {
                // 会社略称
                index = dataTableHeaderTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                this.SetFieldName("PHY_CORP_RYAKU_NAME_VLB", string.Empty);

                //// 発行日時
                //index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                //this.SetFieldName("PHY_PRINT_DATE_VLB", string.Empty);

                // 拠点名
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_NAME");
                this.SetFieldName("PHY_KYOTEN_NAME_VLB", string.Empty);

                // 伝種区分
                index = dataTableHeaderTmp.Columns.IndexOf("DENSHU_KBN_NAME");
                this.SetFieldName("PHY_DENSHU_KBN_NAME_CTL", string.Empty);

                // 伝票区分
                index = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_KBN_NAME");
                this.SetFieldName("PHY_DENPYOU_KBN_NAME_CTL", string.Empty);

                // 取引先コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_TORIHIKISAKI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKI_CD");
                this.SetFieldName("PHY_TORIHIKISAKI_CD_CTL", string.Empty);

                // 業者コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_GYOUSHA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_CD");
                this.SetFieldName("PHY_GYOUSHA_CD_CTL", string.Empty);

                // 現場コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_GENBA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("GENBA_CD");
                this.SetFieldName("PHY_GENBA_CD_CTL", string.Empty);

                // 運搬業者コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_UNPAN_GYOUSHA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("UNPAN_GYOUSHA_CD");
                this.SetFieldName("PHY_UNPAN_GYOUSHA_CD_CTL", string.Empty);

                // 荷降業者コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_NIOROSHI_GYOUSHA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GYOUSHA_CD");
                this.SetFieldName("PHY_NIOROSHI_GYOUSHA_CD_CTL", string.Empty);

                // 荷降現場コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_NIOROSHI_GENBA_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("NIOROSHI_GENBA_CD");
                this.SetFieldName("PHY_NIOROSHI_GENBA_CD_CTL", string.Empty);

                // 種類コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_SHURUI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("SHURUI_CD");
                this.SetFieldName("PHY_SHURUI_CD_CTL", string.Empty);

                // 分類コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_BUNRUI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("BUNRUI_CD");
                this.SetFieldName("PHY_BUNRUI_CD_CTL", string.Empty);

                // 品名コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_HINMEI_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("HINMEI_CD");
                this.SetFieldName("PHY_HINMEI_CD_CTL", string.Empty);

                // 単位コード
                //index = dataTableHeaderTmp.Columns.IndexOf("H_UNIT_CD");
                index = dataTableHeaderTmp.Columns.IndexOf("UNIT_CD");
                this.SetFieldName("PHY_UNIT_CD_CTL", string.Empty);
            }
            #endregion - Header -
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
