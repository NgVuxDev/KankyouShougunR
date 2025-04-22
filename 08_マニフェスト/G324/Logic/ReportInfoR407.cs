using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;

namespace Shougun.Core.PaperManifest.HensoSakiAnnaisho
{
    #region - Class -

    /// <summary>(R407)返送案内書を表すクラス・コントロール</summary>
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
            /// <summary>出力タイプを保持するプロパティ</summary>
            this.OutputType = OutputTypeDef.KohuBango;
            this.InsatuKbn = InsatuKbnDef.Tyokkoyo;
        }

        #endregion - Constructors -

        #region - Enums -

        /// <summary>印刷区分に関する列挙型</summary>
        public enum InsatuKbnDef
        {
            /// <summary>1.直行用</summary>
            Tyokkoyo = 1,

            /// <summary>2.積替用</summary>
            Tumikaeyo = 2,
        }

        /// <summary>出力内容に関する列挙型</summary>
        public enum OutputTypeDef
        {
            /// <summary>1交付番号毎</summary>
            KohuBango = 1,

            /// <summary>2現場毎</summary>
            Genba = 2,

            /// <summary>3返却先集計</summary>
            HenkyakusakiShukei = 3,
        }

        #endregion - Enums -

        #region - Properties -
        /// <summary>出力タイプを保持するプロパティ</summary>
        public OutputTypeDef OutputType { get; set; }
        /// <summary>出力タイプを保持するプロパティ</summary>
        public InsatuKbnDef InsatuKbn { get; set; }
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

            // 行番号
            ctrlName = "PHY_BLANK1_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //排出事業者1
            ctrlName = "PHY_HAISYUTSU_JIGYOSHA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //排出事業者2	
            ctrlName = "PHY_HAISYUTSU_JIGYOSHA2_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //排出事業場1
            ctrlName = "PHY_HAISYUTU_JIGYOJO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //排出事業場2							
            ctrlName = "PHY_HAISYUTU_JIGYOJO2_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //廃棄物種類
            ctrlName = "PHY_HAIKIBUTSU_KIND_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //交付年月日
            ctrlName = "PHY_KOFU_YMD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //交付番号	
            ctrlName = "PHY_KOFU_NUMBER_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 返却日
            ctrlName = "PHY_HENKYAKU_YMD_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 返送先
            ctrlName = "PHY_HENSOUSAKI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // A票
            ctrlName = "PHY_A_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // B1票
            ctrlName = "PHY_B1_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // B2票
            ctrlName = "PHY_B2_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // B4票
            ctrlName = "PHY_B4_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // B6票
            ctrlName = "PHY_B6_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // C1票
            ctrlName = "PHY_C1_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // C2票
            ctrlName = "PHY_C2_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // D票
            ctrlName = "PHY_D_HYO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // E票
            ctrlName = "PHY_E_HYO_FLB";
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

                        // 20140714 syunrei EV005190_帳票として出力した時業者名２、現場名１、２が10文字で切れてしまう。　start

                        //if (byteArray.Length > 20)
                        //{
                        //    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                        //}
                        if (byteArray.Length > 40)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                        }
                        // 20140714 syunrei EV005190_帳票として出力した時業者名２、現場名１、２が10文字で切れてしまう。　end
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

                        // 20140714 syunrei EV005190_帳票として出力した時業者名２、現場名１、２が10文字で切れてしまう。　start

                        //if (byteArray.Length > 20)
                        //{
                        //    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                        //}
                        if (byteArray.Length > 40)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                        }
                        // 20140714 syunrei EV005190_帳票として出力した時業者名２、現場名１、２が10文字で切れてしまう。　end
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

                        // 20140714 syunrei EV005190_帳票として出力した時業者名２、現場名１、２が10文字で切れてしまう。　start

                        //if (byteArray.Length > 20)
                        //{
                        //    row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                        //}
                        if (byteArray.Length > 40)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                        }
                        // 20140714 syunrei EV005190_帳票として出力した時業者名２、現場名１、２が10文字で切れてしまう。　end
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
                        //切り捨てを行わず、2行表示させる
                        //byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        //if (byteArray.Length > 40)
                        //{
                        //    row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                        //}
                        //else
                        //{
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        //}
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
                //①－１
                //返送先-名称	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_NAME_REPORT");
                ctrlName = "FH_MANI_HENSOUSAKI_NAME1_CTL";
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
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先ー郵便番号
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_POST");
                ctrlName = "FH_MANI_HENSOUSAKI_POST_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) && 
                    !String.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                    // 返送元ー郵便番号がないの場合は、〒マークを表示しない。
                    this.SetFieldVisible("FH_POST1_FLB", false);
                }
                //返送先ー住所1               
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_ADDRESS1");
                ctrlName = "FH_MANI_HENSOUSAKI_ADDRESS1_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 48)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 48));
                    }
                    else
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先ー住所2	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_ADDRESS2");
                ctrlName = "FH_MANI_HENSOUSAKI_ADDRESS2_CTL";
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
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先-名称1
                var keishou = true;     // TRUE:敬称別出力, FALSE:名称+敬称で出力
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_NAME1_REPORT");
                ctrlName = "FH_MANI_HENSOUSAKI_NAME1_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    // 名称の文字数算出
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if(byteArray.Length > 40)
                    {
                        // 規定文字数以上であれば、規定文字数で切って出力
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                    }
                    else
                    {
                        // 規定文字数以内であれば、名称+敬称で出力
                        var keiIndex = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_KEISHOU1");
                        if(!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[keiIndex]))
                        {
                            // 名称+敬称で出力
                            var outStr = (((string)dataTableHeaderTmp.Rows[0].ItemArray[index]) + "　" + ((string)dataTableHeaderTmp.Rows[0].ItemArray[keiIndex]));
                            this.SetFieldName(ctrlName, outStr);
                            keishou = false;
                        }
                        else
                        {
                            // 敬称が未登録のため名称のみで出力
                            var outStr = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            this.SetFieldName(ctrlName, outStr);
                        }
                    }
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先-敬称1
                ctrlName = "FH_MANI_HENSOUSAKI_KEISHOU1_CTL";
                if(keishou)
                {
                    index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_KEISHOU1");
                    if(!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
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
                    // 名称+敬称で出力しているため別出力無し
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先-名称2	
                keishou = true;
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_NAME2");
                ctrlName = "FH_MANI_HENSOUSAKI_NAME2_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    // 名称の文字数算出
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if(byteArray.Length > 40)
                    {
                        // 規定文字数以上であれば、規定文字数で切って出力
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                    }
                    else
                    {
                        // 規定文字数以内であれば、名称+敬称で出力
                        var keiIndex = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_KEISHOU2");
                        if(!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[keiIndex]))
                        {
                            // 名称+敬称で出力
                            var outStr = (((string)dataTableHeaderTmp.Rows[0].ItemArray[index]) + "　" + ((string)dataTableHeaderTmp.Rows[0].ItemArray[keiIndex]));
                            this.SetFieldName(ctrlName, outStr);
                            keishou = false;
                        }
                        else
                        {
                            // 敬称が未登録のため名称のみで出力
                            var outStr = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];
                            this.SetFieldName(ctrlName, outStr);
                        }
                    }
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先-敬称2	               
                ctrlName = "FH_MANI_HENSOUSAKI_KEISHOU2_CTL";
                if(keishou)
                {
                    index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_KEISHOU2");
                    if(!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
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
                    // 名称+敬称で出力しているため別出力無し
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先-部署
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_BUSHO");
                ctrlName = "FH_MANI_HENSOUSAKI_BUSHO_CTL";
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
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送先ーマニフェスト担当	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_TANTOU");
                ctrlName = "FH_MANI_HENSOUSAKI_TANTOU_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) && !dataTableHeaderTmp.Rows[0].ItemArray[index].ToString().Equals(string.Empty))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 16)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 16));
                    }
                    else
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }

                    // マニフェスト担当者を表示するときのみ下記のラベルを表示
                    this.SetFieldName("FH_MANI_HENSOUSAKI_TANTOU_FLB", "マニフェスト担当　：　");
                    this.SetFieldName("FH_KEISHOU_FLB", "様");
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                    this.SetFieldName("FH_MANI_HENSOUSAKI_TANTOU_FLB", string.Empty);
                    this.SetFieldName("FH_KEISHOU_FLB", string.Empty);
                }


                //①－２
                //連絡先1
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_RENRAKUSAKI1");
                ctrlName = "FH_MANIFEST_HENSOU_RENRAKU_1_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //連絡先2
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_RENRAKUSAKI2");
                ctrlName = "FH_MANIFEST_HENSOU_RENRAKU_2_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //①－３			
                //返送元-名称
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUMODO_NAME");
                ctrlName = "FH_JISHA_NAME1_CTL";
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
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送元ー郵便番号
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST");
                ctrlName = "FH_KYOTEN_POST_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]) && 
                    !String.IsNullOrEmpty(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString()))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                    // 返送元ー郵便番号がないの場合は、〒マークを表示しない。
                    this.SetFieldVisible("FH_POST2_FLB", false);
                }
                //返送元ー住所1	
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                ctrlName = "FH_KYOTEN_ADDRESS1_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {

                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 48)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 48));
                    }
                    else
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送元ー住所2	
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                ctrlName = "FH_KYOTEN_ADDRESS2_CTL";
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
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送元ー電話番号
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL");
                ctrlName = "FH_KYOTEN_TEL_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //返送元ーFAX番号	
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAX");
                ctrlName = "FH_KYOTEN_FAX_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //①－４
                //捺印タイトル1	
                index = dataTableHeaderTmp.Columns.IndexOf("BLANK1");
                ctrlName = "FH_MANIFEST_HENSOU_NATSUIN_1_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //捺印タイトル2		
                index = dataTableHeaderTmp.Columns.IndexOf("BLANK2");
                ctrlName = "FH_MANIFEST_HENSOU_NATSUIN_2_CTL";
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
                //①－１
                //返送先-名称	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_NAME");
                this.SetFieldName("FH_MANI_HENSOUSAKI_NAME1_CTL", string.Empty);
                //返送先ー郵便番号
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_POST");
                this.SetFieldName("FH_MANI_HENSOUSAKI_POST_CTL", string.Empty);
                //返送先ー住所1               
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_ADDRESS1");
                this.SetFieldName("FH_MANI_HENSOUSAKI_ADDRESS1_CTL", string.Empty);
                //返送先ー住所2	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_ADDRESS2");
                this.SetFieldName("FH_MANI_HENSOUSAKI_ADDRESS2_CTL", string.Empty);
                //返送先-名称1	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_NAME1");
                this.SetFieldName("FH_MANI_HENSOUSAKI_NAME1_CTL", string.Empty);

                //返送先-敬称1
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_KEISHOU1");
                this.SetFieldName("FH_MANI_HENSOUSAKI_KEISHOU1_CTL", string.Empty);
                //返送先-名称2	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_NAME2");
                this.SetFieldName("FH_MANI_HENSOUSAKI_NAME2_CTL", string.Empty);

                //返送先-敬称2	               
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_KEISHOU2");
                this.SetFieldName("FH_MANI_HENSOUSAKI_KEISHOU2_CTL", string.Empty);
                //返送先-部署
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_BUSHO");
                this.SetFieldName("FH_MANI_HENSOUSAKI_BUSHO_CTL", string.Empty);
                //返送先ーマニフェスト担当	
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUSAKI_TANTOU");
                this.SetFieldName("FH_MANI_HENSOUSAKI_TANTOU_CTL", string.Empty);
                this.SetFieldName("FH_MANI_HENSOUSAKI_TANTOU_FLB", string.Empty);   // ラベル
                this.SetFieldName("FH_KEISHOU_FLB", string.Empty);                  // 敬称
                //①－２
                //連絡先1
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_RENRAKUSAKI1");
                this.SetFieldName("FH_MANIFEST_HENSOU_RENRAKU_1_CTL", string.Empty);
                //連絡先2
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_RENRAKUSAKI2");
                this.SetFieldName("FH_MANIFEST_HENSOU_RENRAKU_2_CTL", string.Empty);

                //①－３			
                //返送元-名称
                index = dataTableHeaderTmp.Columns.IndexOf("MANI_HENSOUMODO_NAME");
                this.SetFieldName("FH_JISHA_NAME1_CTL", string.Empty);
                //返送元ー郵便番号
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_POST");
                this.SetFieldName("FH_KYOTEN_POST_CTL", string.Empty);
                //返送元ー住所1	
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                this.SetFieldName("FH_KYOTEN_ADDRESS1_CTL", string.Empty);
                //返送元ー住所2	
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                this.SetFieldName("FH_KYOTEN_ADDRESS2_CTL", string.Empty);
                //返送元ー電話番号
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_TEL");
                this.SetFieldName("FH_KYOTEN_TEL_CTL", string.Empty);
                //返送元ーFAX番号	
                index = dataTableHeaderTmp.Columns.IndexOf("KYOTEN_FAX");
                this.SetFieldName("FH_KYOTEN_FAX_CTL", string.Empty);
                //①－４
                //捺印タイトル1	
                index = dataTableHeaderTmp.Columns.IndexOf("BLANK1");
                this.SetFieldName("FH_MANIFEST_HENSOU_NATSUIN_1_CTL", string.Empty);
                //捺印タイトル2		
                index = dataTableHeaderTmp.Columns.IndexOf("BLANK2");
                this.SetFieldName("FH_MANIFEST_HENSOU_NATSUIN_2_CTL", string.Empty);
            }

            #endregion - Header -

            #region - Footer -

            if (dataTableFooterTmp.Rows.Count > 0)
            {
                // 計ーA票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_A");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_A_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_A_HYO_TOTAL_FLB", string.Empty);
                }

                //計ーB1票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B1");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_B1_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_B1_HYO_TOTAL_FLB", string.Empty);
                }

                //計ーB2票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B2");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_B2_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_B2_HYO_TOTAL_FLB", string.Empty);
                }

                //計ーB4票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B4");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_B4_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_B4_HYO_TOTAL_FLB", string.Empty);
                }
                //計ーB6票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B6");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_B6_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_B6_HYO_TOTAL_FLB", string.Empty);
                }

                //計ーC1票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_C1");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_C1_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_C1_HYO_TOTAL_FLB", string.Empty);
                }
                //計ーC2票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_C2");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_C2_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_C2_HYO_TOTAL_FLB", string.Empty);
                }
                //計ーD票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_D");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_D_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_D_HYO_TOTAL_FLB", string.Empty);
                }
                //計ーE票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_E");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_E_HYO_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_E_HYO_TOTAL_FLB", string.Empty);
                }
                //合計
                index = dataTableFooterTmp.Columns.IndexOf("SUM_NUM");
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName("PHN_ALL_TOTAL_FLB", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName("PHN_ALL_TOTAL_FLB", string.Empty);
                }
            }
            else
            {
                // 計ーA票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_A");
                this.SetFieldName("PHN_A_HYO_TOTAL_FLB", string.Empty);

                //計ーB1票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B1");
                this.SetFieldName("PHN_B1_HYO_TOTAL_FLB", string.Empty);

                //計ーB2票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B2");
                this.SetFieldName("PHN_B2_HYO_TOTAL_FLB", string.Empty);

                //計ーB4票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B4");
                this.SetFieldName("PHN_B4_HYO_TOTAL_FLB", string.Empty);
                //計ーB6票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_B6");
                this.SetFieldName("PHN_B6_HYO_TOTAL_FLB", string.Empty);

                //計ーC1票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_C1");
                this.SetFieldName("PHN_C1_HYO_TOTAL_FLB", string.Empty);

                //計ーC2票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_C2");
                this.SetFieldName("PHN_C2_HYO_TOTAL_FLB", string.Empty);

                //計ーD票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_D");
                this.SetFieldName("PHN_D_HYO_TOTAL_FLB", string.Empty);

                //計ーE票
                index = dataTableFooterTmp.Columns.IndexOf("SEND_E");
                this.SetFieldName("PHN_E_HYO_TOTAL_FLB", string.Empty);
                //合計
                index = dataTableFooterTmp.Columns.IndexOf("SUM_NUM");
                this.SetFieldName("PHN_ALL_TOTAL_FLB", string.Empty);
            }

            #endregion - Footer -
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
