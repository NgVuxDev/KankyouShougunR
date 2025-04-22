using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
{   
    #region - Class -

    /// <summary>(R391)返送案内書を表すクラス・コントロール</summary>
    public class ReportInfoR407 : ReportInfoBase
    {
        #region - Fields -
        private const int ConstMaxDispDetailRowCount = 15;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR407"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR407(WINDOW_ID windowID)
        {
            this.windowID = windowID;
          
        }

        #endregion - Constructors -           

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

            

            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            this.DataTableList.Add("Detail", dataTableTmp);
            #endregion - Detail -

            #region - Footer -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";           

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
            // 一次二次区分
            ctrlName = "PHY_MANIFEST_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 排出事業者CD
            ctrlName = "PHN_JIGYOSHA_CODE_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 排出事業者名
            ctrlName = "PHN_JIGYOSHA_NAME_VLB";
            this.dataTable.Columns.Add(ctrlName);            
            // 排出事業場CD
            ctrlName = "PHY_JIGYOJO_CODE_FLB";
            this.dataTable.Columns.Add(ctrlName);

            // 排出事業場名
            ctrlName = "PHY_JIGYOJYO_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);            
            // 廃棄物種類
            ctrlName = "PHY_HAIKI_KIND_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 単位
            ctrlName = "PHY_UNIT_FLB";
            this.dataTable.Columns.Add(ctrlName);

                          
            // 廃棄物種類-月別
            ctrlName = "PHY_MONTH1_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH2_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH3_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH4_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH5_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH6_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH7_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH8_FLB";
            this.dataTable.Columns.Add(ctrlName);

            ctrlName = "PHY_MONTH9_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH10_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH11_FLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHY_MONTH12_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 廃棄物種類-合計
            ctrlName = "PHY_ALL_FLB";
            this.dataTable.Columns.Add(ctrlName);

               
            // 排出事業者計-月別
            ctrlName = "PHN_MONTH1_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH2_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH3_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH4_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH5_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH6_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH7_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH8_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH9_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH10_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH11_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            ctrlName = "PHN_MONTH12_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);
            // 排出事業者計-合計
            ctrlName = "PHN_JIGYOTOTAL_VLB";
            this.dataTable.Columns.Add(ctrlName);

           


         
            #endregion Columns

            #region - Detail -

            if (detailMaxCount > 0)
            {
                for (i = detailStart; i < detailMaxCount; i++)
                {
                    row = this.dataTable.NewRow();
                    // 行番号
                    index = dataTableDetailTmp.Columns.IndexOf("ROW_NO");
                    ctrlName = "PHY_BLANK1_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    //排出事業者1
                    index = dataTableDetailTmp.Columns.IndexOf("HST_GYOUSHA_CD1");
                    ctrlName = "PHY_HAISYUTSU_JIGYOSHA_FLB";
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
                    //排出事業者2	
                    index = dataTableDetailTmp.Columns.IndexOf("HST_GYOUSHA_CD2");
                    ctrlName = "PHY_HAISYUTSU_JIGYOSHA2_FLB";
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
                    //排出事業場1
                    index = dataTableDetailTmp.Columns.IndexOf("HST_GENBA_CD1");
                    ctrlName = "PHY_HAISYUTU_JIGYOJO_FLB";
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
                    //排出事業場2
                    index = dataTableDetailTmp.Columns.IndexOf("HST_GENBA_CD2");
                    ctrlName = "PHY_HAISYUTU_JIGYOJO2_FLB";
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
                    //廃棄物種類
                    index = dataTableDetailTmp.Columns.IndexOf("HAIKI_SHURUI_CD");
                    ctrlName = "PHY_HAIKIBUTSU_KIND_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 108)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 108);
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
                    //交付年月日
                    index = dataTableDetailTmp.Columns.IndexOf("KOUFU_DATE");
                    ctrlName = "PHY_KOFU_YMD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    //交付番号	
                    index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_ID");
                    ctrlName = "PHY_KOFU_NUMBER_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 返却日
                    index = dataTableDetailTmp.Columns.IndexOf("REF_DATE");
                    ctrlName = "PHY_HENKYAKU_YMD_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 返送先
                    index = dataTableDetailTmp.Columns.IndexOf("HENSOUSAKI_NAME");
                    ctrlName = "PHY_HENSOUSAKI_FLB";
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
                    // A票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_A");
                    ctrlName = "PHY_A_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // B1票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_B1");
                    ctrlName = "PHY_B1_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // B2票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_B2");
                    ctrlName = "PHY_B2_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // B4票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_B4");
                    ctrlName = "PHY_B4_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // B6票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_B6");
                    ctrlName = "PHY_B6_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // C1票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_C1");
                    ctrlName = "PHY_C1_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // C2票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_C2");
                    ctrlName = "PHY_C2_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // D票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_D");
                    ctrlName = "PHY_D_HYO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // E票
                    index = dataTableDetailTmp.Columns.IndexOf("SEND_E");
                    ctrlName = "PHY_E_HYO_FLB";
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

                // 行番号
                index = dataTableDetailTmp.Columns.IndexOf("ROW_NO");
                ctrlName = "PHY_BLANK1_FLB";
                row[ctrlName] = string.Empty;
                //排出事業者1
                index = dataTableDetailTmp.Columns.IndexOf("HST_GYOUSHA_CD1");
                ctrlName = "PHY_HAISYUTSU_JIGYOSHA_FLB";               
                row[ctrlName] = string.Empty;
                //排出事業者2	
                index = dataTableDetailTmp.Columns.IndexOf("HST_GYOUSHA_CD2");
                ctrlName = "PHY_HAISYUTSU_JIGYOSHA2_FLB";
                row[ctrlName] = string.Empty;
                //排出事業場1
                index = dataTableDetailTmp.Columns.IndexOf("HST_GENBA_CD1");
                ctrlName = "PHY_HAISYUTU_JIGYOJO_FLB";
                row[ctrlName] = string.Empty;
                //排出事業場2
                index = dataTableDetailTmp.Columns.IndexOf("HST_GENBA_CD2");
                ctrlName = "PHY_HAISYUTU_JIGYOJO2_FLB";
                row[ctrlName] = string.Empty;
                //廃棄物種類
                index = dataTableDetailTmp.Columns.IndexOf("HAIKI_SHURUI_CD");
                ctrlName = "PHY_HAIKIBUTSU_KIND_FLB";
                row[ctrlName] = string.Empty;
                //交付年月日
                index = dataTableDetailTmp.Columns.IndexOf("KOUFU_DATE");
                ctrlName = "PHY_KOFU_YMD_FLB";
                row[ctrlName] = string.Empty;
                //交付番号	
                index = dataTableDetailTmp.Columns.IndexOf("MANIFEST_ID");
                ctrlName = "PHY_KOFU_NUMBER_FLB";
                row[ctrlName] = string.Empty;
                // 返却日
                index = dataTableDetailTmp.Columns.IndexOf("REF_DATE");
                ctrlName = "PHY_HENKYAKU_YMD_FLB";
                row[ctrlName] = string.Empty;
                // 返送先
                index = dataTableDetailTmp.Columns.IndexOf("HENSOUSAKI_NAME");
                ctrlName = "PHY_HENSOUSAKI_FLB";
                row[ctrlName] = string.Empty;
                // A票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_A");
                ctrlName = "PHY_A_HYO_FLB";
                row[ctrlName] = string.Empty;
                // B1票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_B1");
                ctrlName = "PHY_B1_HYO_FLB";
                row[ctrlName] = string.Empty;
                // B2票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_B2");
                ctrlName = "PHY_B2_HYO_FLB";
                row[ctrlName] = string.Empty;
                // B4票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_B4");
                ctrlName = "PHY_B4_HYO_FLB";
                row[ctrlName] = string.Empty;
                // B6票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_B6");
                ctrlName = "PHY_B6_HYO_FLB";
                row[ctrlName] = string.Empty;
                // C1票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_C1");
                ctrlName = "PHY_C1_HYO_FLB";
                row[ctrlName] = string.Empty;
                // C2票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_C2");
                ctrlName = "PHY_C2_HYO_FLB";
                row[ctrlName] = string.Empty;
                // D票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_D");
                ctrlName = "PHY_D_HYO_FLB";
                row[ctrlName] = string.Empty;
                // E票
                index = dataTableDetailTmp.Columns.IndexOf("SEND_E");
                ctrlName = "PHY_E_HYO_FLB";
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
            string ctrlName = string.Empty;
            string dataColName = string.Empty;
            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            #region - Header -
           
            
            if (dataTableHeaderTmp.Rows.Count > 0)
            {
                // 発行日時               
                index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                ctrlName = "FH_PRINT_DATE_VLB";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                } 
            }
            else
            {
              
            }

            #endregion - Header -

            #region - Footer -
            // 総合計-１月別              
            if (dataTableFooterTmp.Rows.Count > 0)
            {
                ctrlName = "PHN_MONTH1_TOTAL_VLB";
                dataColName = "MONTH1_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH2_TOTAL_VLB";
                dataColName = "MONTH2_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH3_TOTAL_VLB";
                dataColName = "MONTH3_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH4_TOTAL_VLB";
                dataColName = "MONTH4_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH5_TOTAL_VLB";
                dataColName = "MONTH5_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH6_TOTAL_VLB";
                dataColName = "MONTH6_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH7_TOTAL_VLB";
                dataColName = "MONTH7_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH8_TOTAL_VLB";
                dataColName = "MONTH8_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH9_TOTAL_VLB";
                dataColName = "MONTH9_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH10_TOTAL_VLB";
                dataColName = "MONTH10_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH11_TOTAL_VLB";
                dataColName = "MONTH11_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                ctrlName = "PHN_MONTH12_TOTAL_VLB";
                dataColName = "MONTH12_TOTAL";
                index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }

                //// 総合計-合計
                //ctrlName = "PHN_TOTAL_VLB";
                //dataColName = "MONTH1_TOTAL";
                //index = dataTableFooterTmp.Columns.IndexOf(dataColName);
                //if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                //{
                //    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                //}
                //else
                //{
                //    this.SetFieldName(ctrlName, string.Empty);
                //}
            }
            else
            {
                //合計
                index = dataTableFooterTmp.Columns.IndexOf("ALLHAISHURYO");
                this.SetFieldName(ctrlName, string.Empty);
            }

            
            #endregion - Footer -
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
