using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using Shougun.Core.Common.BusinessCommon;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran
{   
    #region - Class -

    /// <summary>(R394)交付等状況報告書を表すクラス・コントロール</summary>
    public class ReportInfoR394 : ReportInfoBase
    {
        #region - Fields -
        private const int ConstMaxDispDetailRowCount = 6;        // Detailの最大表示行数

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR407"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR394(WINDOW_ID windowID)
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

            #region - Header -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Header";

            //提出先
            dataTableTmp.Columns.Add("GOV_OR_MAY_NAME");
            //事業場の名称
            dataTableTmp.Columns.Add("JGB_NAME");
            //事業場の住所
            dataTableTmp.Columns.Add("JGB_ADDRESS");
            //業種
            dataTableTmp.Columns.Add("GYOUSHU_CD");
            dataTableTmp.Columns.Add("GYOUSHU_NAME");
            //電話
            dataTableTmp.Columns.Add("GENBA_TEL");
            //作成日
            dataTableTmp.Columns.Add("PRINT_DATE");
            //報告者
            //住所
            dataTableTmp.Columns.Add("GYOUSHA_ADDRESS1");
            dataTableTmp.Columns.Add("GYOUSHA_ADDRESS2");
            //氏名
            dataTableTmp.Columns.Add("GYOUSHA_NAME1");
            dataTableTmp.Columns.Add("GYOUSHA_NAME2");
            //代表者
            dataTableTmp.Columns.Add("DAIHYOUSHA");
            //電話
            dataTableTmp.Columns.Add("GYOUSHA_TEL");
            //表題
            dataTableTmp.Columns.Add("TITLE1");
            dataTableTmp.Columns.Add("TITLE2");

            //行データ設定
            rowTmp = dataTableTmp.NewRow();
            //提出先
            rowTmp["GOV_OR_MAY_NAME"] = "千葉市市長千葉市市長千葉市市長千葉市市長";
            //事業場の名称
            rowTmp["JGB_NAME"] = "川崎市中原区大倉町名川崎市中原区大倉町名川崎市中原区大倉町名川崎市中原区大倉町名";
            //事業場の住所         
            rowTmp["JGB_ADDRESS"] = "川崎市中原区大倉町名川崎市中原区大倉町名川崎市中原区大倉町名川崎市中原区大倉町名";//"川崎市中原区大倉町1-12-335";
            //業種
            rowTmp["GYOUSHU_CD"] = "1001";
            rowTmp["GYOUSHU_NAME"] = "帳票業種帳票業種帳票業種帳票業種帳票業種";
            //電話
            rowTmp["GENBA_TEL"] = "080-1234-9874";

            //作成日
            rowTmp["PRINT_DATE"] = DateTime.Today;
            //報告者
            //住所
            rowTmp["GYOUSHA_ADDRESS1"] = "秋田市請求締字５１９－１帳票業種帳票業種";
            rowTmp["GYOUSHA_ADDRESS2"] = "大字５１９－１秋田市請求締字５１９－１１";
            //氏名
            rowTmp["GYOUSHA_NAME1"] = "田和家物芽画田和家物芽画田和家物芽画芽画";
            rowTmp["GYOUSHA_NAME2"] = "田和家あぽたん田和家あぽたん田和家あぽた";
            //代表者         
            rowTmp["DAIHYOUSHA"] = "資材管理取引先５１９担当資材管理取引先先";
            //電話
            rowTmp["GYOUSHA_TEL"] = "055-519-1234";
            //表題
            rowTmp["TITLE1"] = "川崎市中原区資材管理川崎市中原区資材管理川崎市中原区資材管理";
            rowTmp["TITLE2"] = "資材管理帳票業種資材管理帳票業種資材管理帳票業種資材管理帳票";

            dataTableTmp.Rows.Add(rowTmp); 
            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";
            dataTableTmp.Columns.Add("ROW_NO");
            dataTableTmp.Columns.Add("HAIKI_SHURUI_NAME_RYAKU");
            dataTableTmp.Columns.Add("HAISHU_RYOU");
            dataTableTmp.Columns.Add("COUFUMAISUU");
            dataTableTmp.Columns.Add("UPN_FUTSUU_KYOKA_NO");
            dataTableTmp.Columns.Add("UPN_GYOUSHA_NAME");
            dataTableTmp.Columns.Add("UPN_SAKI_GENBA_ADDRESS");
            dataTableTmp.Columns.Add("SBN_FUTSUU_KYOKA_NO");
            dataTableTmp.Columns.Add("SBN_GYOUSHA_NAME");
            dataTableTmp.Columns.Add("SBN_GENBA_ADDRESS");

           
            for (int rowIndex = 0; rowIndex <= 5; rowIndex++)
            {
                dataTableTmp.Rows.Add();
                dataTableTmp.Rows[rowIndex]["ROW_NO"] = rowIndex+1;
                if (rowIndex == 1 || rowIndex == 3 || rowIndex == 5 || rowIndex == 7)
                {
                    dataTableTmp.Rows[rowIndex]["HAIKI_SHURUI_NAME_RYAKU"] = "廃プラスチック類廃プラスチック類ク類";
                    dataTableTmp.Rows[rowIndex]["HAISHU_RYOU"] = "123456789.0";
                    dataTableTmp.Rows[rowIndex]["COUFUMAISUU"] = "10";
                    dataTableTmp.Rows[rowIndex]["UPN_FUTSUU_KYOKA_NO"] = "12345678901";
                    dataTableTmp.Rows[rowIndex]["UPN_GYOUSHA_NAME"] = "諸口業者諸口業者諸口業者諸口業者諸口業者諸口業者諸口業者諸口業者諸口業者諸口業者";
                    dataTableTmp.Rows[rowIndex]["UPN_SAKI_GENBA_ADDRESS"] = "茨城県茨木市取引先５２７茨城県茨木市取引先５２７茨城県茨木市取引先５２７市取引先";
                    dataTableTmp.Rows[rowIndex]["SBN_FUTSUU_KYOKA_NO"] = "12345678901";
                    dataTableTmp.Rows[rowIndex]["SBN_GYOUSHA_NAME"] = "受入テスト荷卸現場６受入テスト荷卸現場６受入テスト荷卸現場６受入テスト荷卸現場６";
                    dataTableTmp.Rows[rowIndex]["SBN_GENBA_ADDRESS"] = "東京都中央区日本橋○○町東京都中央区日本橋○○町東京都中央区日本橋○○町橋○○町";
                }
                else if (rowIndex == 2 || rowIndex == 4)
                {
                    dataTableTmp.Rows[rowIndex]["HAIKI_SHURUI_NAME_RYAKU"] = "ガラス・陶磁器くず";
                    dataTableTmp.Rows[rowIndex]["HAISHU_RYOU"] = "4000";
                    dataTableTmp.Rows[rowIndex]["COUFUMAISUU"] = "200";
                    dataTableTmp.Rows[rowIndex]["UPN_FUTSUU_KYOKA_NO"] = "87655678901";
                    dataTableTmp.Rows[rowIndex]["UPN_GYOUSHA_NAME"] = "諸口業者";
                    dataTableTmp.Rows[rowIndex]["UPN_SAKI_GENBA_ADDRESS"] = "東京都茨木市取引先５２７";
                    dataTableTmp.Rows[rowIndex]["SBN_FUTSUU_KYOKA_NO"] = "87655678901";
                    dataTableTmp.Rows[rowIndex]["SBN_GYOUSHA_NAME"] = "受入テスト荷卸現場６";
                    dataTableTmp.Rows[rowIndex]["SBN_GENBA_ADDRESS"] = "茨城県中央区日本橋○○町";
                }
                else
                {
                    dataTableTmp.Rows[rowIndex]["HAIKI_SHURUI_NAME_RYAKU"] = "石綿含有産業廃棄物";
                    dataTableTmp.Rows[rowIndex]["HAISHU_RYOU"] = "100";
                    dataTableTmp.Rows[rowIndex]["COUFUMAISUU"] = "10";
                    dataTableTmp.Rows[rowIndex]["UPN_FUTSUU_KYOKA_NO"] = "15436654366";
                    dataTableTmp.Rows[rowIndex]["UPN_GYOUSHA_NAME"] = "家物芽画";
                    dataTableTmp.Rows[rowIndex]["UPN_SAKI_GENBA_ADDRESS"] = "秋田市請求締字５１９－１";
                    dataTableTmp.Rows[rowIndex]["SBN_FUTSUU_KYOKA_NO"] = "15436654366";
                    dataTableTmp.Rows[rowIndex]["SBN_GYOUSHA_NAME"] = "荷卸現場６";
                    dataTableTmp.Rows[rowIndex]["SBN_GENBA_ADDRESS"] = "川崎市中原区大倉町1-12-335";
                }
            }

            this.DataTableList.Add("Detail", dataTableTmp);
            #endregion - Detail -
          
            #region - Footer -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";
            //排出量合計							
            dataTableTmp.Columns.Add("ALLHAISHURYO");
            rowTmp = dataTableTmp.NewRow();

            //合計						
            rowTmp["ALLHAISHURYO"] ="100000";

            dataTableTmp.Rows.Add(rowTmp);           
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
            ctrlName = "PHY_ROW_NO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //産業廃棄物の種類
            ctrlName = "PHY_HAIKI_SHURUI_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //排出量(t)	
            ctrlName = "PHY_HAISHU_RYOU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //管理票の交付枚数
            ctrlName = "PHY_COUFUMAISUU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //運搬受託者の許可番号
            ctrlName = "PHY_UPN_FUTSUU_KYOKA_NO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //運搬受託者の氏名又は名称
            ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //運搬先の住所
            ctrlName = "PHY_UPN_SAKI_GENBA_ADDRESS_FLB";
            this.dataTable.Columns.Add(ctrlName);
            //処分受託者の許可番号
            ctrlName = "PHY_SBN_FUTSUU_KYOKA_NO_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 処分受託者の氏名又は名称
            ctrlName = "PHY_SBN_GYOUSHA_NAME_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 処分場所の住所
            ctrlName = "PHY_SBN_GENBA_ADDRESS_FLB";
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
                    ctrlName = "PHY_ROW_NO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = i + 1;
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    //産業廃棄物の種類
                    index = dataTableDetailTmp.Columns.IndexOf("HAIKI_SHURUI_NAME_RYAKU");
                    ctrlName = "PHY_HAIKI_SHURUI_NAME_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 72)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 72);
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
                    //排出量(t)	
                    index = dataTableDetailTmp.Columns.IndexOf("HAISHU_RYOU");
                    ctrlName = "PHY_HAISHU_RYOU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    //管理票の交付枚数
                    index = dataTableDetailTmp.Columns.IndexOf("COUFUMAISUU");
                    ctrlName = "PHY_COUFUMAISUU_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    //運搬受託者の許可番号
                    index = dataTableDetailTmp.Columns.IndexOf("UPN_FUTSUU_KYOKA_NO");
                    ctrlName = "PHY_UPN_FUTSUU_KYOKA_NO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }

                    //運搬受託者の氏名又は名称
                    index = dataTableDetailTmp.Columns.IndexOf("UPN_GYOUSHA_NAME");
                    ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 80)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 80);
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
                    //運搬先の住所
                    index = dataTableDetailTmp.Columns.IndexOf("UPN_SAKI_GENBA_ADDRESS");
                    ctrlName = "PHY_UPN_SAKI_GENBA_ADDRESS_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 88)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 88);
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
                    //処分受託者の許可番号
                    index = dataTableDetailTmp.Columns.IndexOf("SBN_FUTSUU_KYOKA_NO");
                    ctrlName = "PHY_SBN_FUTSUU_KYOKA_NO_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {
                        row[ctrlName] = string.Empty;
                    }
                    // 処分受託者の氏名又は名称
                    index = dataTableDetailTmp.Columns.IndexOf("SBN_GYOUSHA_NAME");
                    ctrlName = "PHY_SBN_GYOUSHA_NAME_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 80)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 80);
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
                    // 処分場所の住所
                    index = dataTableDetailTmp.Columns.IndexOf("SBN_GENBA_ADDRESS");
                    ctrlName = "PHY_SBN_GENBA_ADDRESS_FLB";
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 88)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 88);
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
                   
                    this.dataTable.Rows.Add(row);
                }
            }
            else
            {
                row = this.dataTable.NewRow();

                // 行番号
                index = dataTableDetailTmp.Columns.IndexOf("ROW_NO");
                ctrlName = "PHY_ROW_NO_FLB";
                row[ctrlName] = string.Empty;

                //産業廃棄物の種類
                index = dataTableDetailTmp.Columns.IndexOf("HAIKI_SHURUI_NAME_RYAKU");
                ctrlName = "PHY_HAIKI_SHURUI_NAME_FLB";
                row[ctrlName] = string.Empty;
                //排出量(t)	
                index = dataTableDetailTmp.Columns.IndexOf("HAISHU_RYOU");
                ctrlName = "PHY_HAISHU_RYOU_FLB";
                row[ctrlName] = string.Empty;
                //管理票の交付枚数
                index = dataTableDetailTmp.Columns.IndexOf("COUFUMAISUU");
                ctrlName = "PHY_COUFUMAISUU_FLB";
                row[ctrlName] = string.Empty;

                //運搬受託者の許可番号
                index = dataTableDetailTmp.Columns.IndexOf("UPN_FUTSUU_KYOKA_NO");
                ctrlName = "PHY_UPN_FUTSUU_KYOKA_NO_FLB";
                row[ctrlName] = string.Empty;

                //運搬受託者の氏名又は名称
                index = dataTableDetailTmp.Columns.IndexOf("UPN_GYOUSHA_NAME");
                ctrlName = "PHY_UPN_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                //運搬先の住所
                index = dataTableDetailTmp.Columns.IndexOf("UPN_SAKI_GENBA_ADDRESS");
                ctrlName = "PHY_UPN_SAKI_GENBA_ADDRESS_FLB";
                row[ctrlName] = string.Empty;
                //処分受託者の許可番号
                index = dataTableDetailTmp.Columns.IndexOf("SBN_FUTSUU_KYOKA_NO");
                ctrlName = "PHY_SBN_FUTSUU_KYOKA_NO_FLB";
                row[ctrlName] = string.Empty;
                // 処分受託者の氏名又は名称
                index = dataTableDetailTmp.Columns.IndexOf("SBN_GYOUSHA_NAME");
                ctrlName = "PHY_SBN_GYOUSHA_NAME_FLB";
                row[ctrlName] = string.Empty;
                // 処分場所の住所
                index = dataTableDetailTmp.Columns.IndexOf("SBN_GENBA_ADDRESS");
                ctrlName = "PHY_SBN_GENBA_ADDRESS_FLB";
                row[ctrlName] = string.Empty;

                this.dataTable.Rows.Add(row);
            }

            #endregion -Detail -

            this.SetRecord(this.dataTable);
        }

        /// <summary>フィールド状態の更新処理を実行する</summary>
        protected override void UpdateFieldsStatus()
        {
            // 数値フォーマット情報取得
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            string ManifestSuuryoFormat = mSysInfo.MANIFEST_SUURYO_FORMAT.ToString();

            // 排出量
            this.SetFieldFormat("DTL_HAISHU_RYOU_CTL", ManifestSuuryoFormat + ";");

            // 排出量合計
            this.SetFieldFormat("D1F_ALLHAISHURYO_CTL", ManifestSuuryoFormat + ";");

            // 交付枚数は整数の3桁区切りを適用する
            this.SetFieldFormat("DTL_COUFUMAISUU_CTL", "#,###;");

            // 換算後単位はシステム設定の値によって変更。文言として「排出量(t)」が固定ではない
            short kansanKihonUnitCd = mSysInfo.MANI_KANSAN_KIHON_UNIT_CD.Value;

            r_framework.Dao.IM_UNITDao unitDao;
            unitDao = r_framework.Utility.DaoInitUtility.GetComponent<r_framework.Dao.IM_UNITDao>();

            M_UNIT mUnit = unitDao.GetDataByCd(kansanKihonUnitCd);
            if (mUnit != null)
            {
                string dispUnitName = "排出量(" + mUnit.UNIT_NAME + ")";
                this.SetFieldName("PHY_HAISHU_RYOU_FLB1", dispUnitName);
            }

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
                // 表題           
                index = dataTableHeaderTmp.Columns.IndexOf("NENDO_BASE_DATE");
                ctrlName = "FH_TITLE_FLB";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    //元号○○年○○月○○日   
                    DateTime temp = DateTime.Parse(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    //和暦でDataTimeを文字列に変換する
                    System.Globalization.CultureInfo ci =
                        new System.Globalization.CultureInfo("ja-JP", false);
                    ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                    //表題在設定（４月を期首月とする）
                    var nendo = new DateTime((temp.Month >= 4 ? temp.Year : temp.Year - 1), temp.Month, temp.Day);
                    string strTitle = "産業廃棄物管理票交付等状況報告書(" + nendo.ToString("ggyy年", ci) + "度)";

                    this.SetFieldName(ctrlName, strTitle);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                // 発行日時           
                index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                ctrlName = "FH_PRINT_DATE_VLB";
                if(!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    //元号○○年○○月○○日   
                    DateTime temp = DateTime.Parse(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    //和暦でDataTimeを文字列に変換する
                    System.Globalization.CultureInfo ci =
                        new System.Globalization.CultureInfo("ja-JP", false);
                    ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                    var minDate = new DateTime(1868, 9, 8);
                    if (temp >= minDate)
                    {
                        this.SetFieldName(ctrlName, temp.ToString("gy年MM月dd日", ci));
                    }
                    else
                    {
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //提出先
                index = dataTableHeaderTmp.Columns.IndexOf("GOV_OR_MAY_NAME");
                var chiikiNameIndex = dataTableHeaderTmp.Columns.IndexOf("CHIIKI_NAME_RYAKU");
                ctrlName = "FH_GOV_OR_MAY_NAME_VLB";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index])
                    && !this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[chiikiNameIndex]))
                {
                    string teishutsu_name = "";
                    string gov_or_may_name = dataTableHeaderTmp.Rows[0].ItemArray[index].ToString();
                    string chiiki_name_ryaku = dataTableHeaderTmp.Rows[0].ItemArray[chiikiNameIndex].ToString();

                    if (!string.IsNullOrEmpty(gov_or_may_name))
                    {
                        switch (chiiki_name_ryaku.Substring(chiiki_name_ryaku.Length - 1, 1))
                        {
                            case "県":
                            case "都":
                            case "道":
                            case "府":
                                teishutsu_name = chiiki_name_ryaku + "知事　　" + gov_or_may_name + "　殿";
                                break;
                            case "市":
                                teishutsu_name = chiiki_name_ryaku + "市長　　" + gov_or_may_name + "　殿";
                                break;
                        }
                    }

                    this.SetFieldName(ctrlName, teishutsu_name);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }              

                //事業場の名称               
                index = dataTableHeaderTmp.Columns.IndexOf("JGB_NAME");
                ctrlName = "FH_JGB_NAME_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 80)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 80));
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
                //事業場の住所
                index = dataTableHeaderTmp.Columns.IndexOf("JGB_ADDRESS");
                ctrlName = "FH_JGB_ADDRESS_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 80)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 80));
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
                //業種
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHU_CD");
                ctrlName = "FH_GYOUSHU_CD_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }

                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHU_NAME");
                ctrlName = "FH_GYOUSHU_NAME_CTL";
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
                //電話
                index = dataTableHeaderTmp.Columns.IndexOf("GENBA_TEL");
                ctrlName = "FH_GENBA_TEL_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //報告者
                //住所 
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_ADDRESS1");
                ctrlName = "FH_GYOUSHA_ADDRESS1_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 50)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 50));
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
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_ADDRESS2");
                ctrlName = "FH_GYOUSHA_ADDRESS2_CTL";
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
                //氏名              
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                ctrlName = "FH_GYOUSHA_NAME1_CTL";
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
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                ctrlName = "FH_GYOUSHA_NAME2_CTL";
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
                //代表者               
                index = dataTableHeaderTmp.Columns.IndexOf("DAIHYOUSHA");
                ctrlName = "FH_DAIHYOUSHA_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 56)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 56));
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
                //電話               
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_TEL");
                ctrlName = "FH_GYOUSHA_TEL_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
                //表題１               
                index = dataTableHeaderTmp.Columns.IndexOf("TITLE1");
                ctrlName = "FH_TITLE1_CTL";
                if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                {
                    byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                    if (byteArray.Length > 140)
                    {
                        this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 140));
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
            }
            else
            {
                // 発行日時           
                index = dataTableHeaderTmp.Columns.IndexOf("PRINT_DATE");
                ctrlName = "FH_PRINT_DATE_VLB";
                //元号○○年○○月○○日   
                DateTime temp = DateTime.Today;
                //和暦でDataTimeを文字列に変換する
                System.Globalization.CultureInfo ci =
                    new System.Globalization.CultureInfo("ja-JP", false);
                ci.DateTimeFormat.Calendar = new System.Globalization.JapaneseCalendar();

                this.SetFieldName(ctrlName, temp.ToString("gy年MM月dd日", ci));
                ctrlName = "FH_TITLE_FLB";
                string strTitle = "産業廃棄物管理票交付状況等報告書(" + temp.ToString("ggyy年", ci) + "度)";
                this.SetFieldName(ctrlName, strTitle);

                //提出先
                index = dataTableHeaderTmp.Columns.IndexOf("GOV_OR_MAY_NAME");
                ctrlName = "FH_GOV_OR_MAY_NAME_VLB";
                this.SetFieldName(ctrlName, string.Empty);

                //事業場の名称               
                index = dataTableHeaderTmp.Columns.IndexOf("JGB_NAME");
                ctrlName = "FH_JGB_NAME_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //事業場の住所
                index = dataTableHeaderTmp.Columns.IndexOf("JGB_ADDRESS");
                ctrlName = "FH_JGB_ADDRESS_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //業種
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHU_CD");
                ctrlName = "FH_GYOUSHU_CD_CTL";
                this.SetFieldName(ctrlName, string.Empty);

                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHU_NAME");
                ctrlName = "FH_GYOUSHU_NAME_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //電話
                index = dataTableHeaderTmp.Columns.IndexOf("GENBA_TEL");
                ctrlName = "FH_GENBA_TEL_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //報告者
                //住所 
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_ADDRESS1");
                ctrlName = "FH_GYOUSHA_ADDRESS1_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_ADDRESS2");
                ctrlName = "FH_GYOUSHA_ADDRESS2_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //氏名              
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME1");
                ctrlName = "FH_GYOUSHA_NAME1_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_NAME2");
                ctrlName = "FH_GYOUSHA_NAME2_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //代表者               
                index = dataTableHeaderTmp.Columns.IndexOf("DAIHYOUSHA");
                ctrlName = "FH_DAIHYOUSHA_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //電話               
                index = dataTableHeaderTmp.Columns.IndexOf("GYOUSHA_TEL");
                ctrlName = "FH_GYOUSHA_TEL_CTL";
                this.SetFieldName(ctrlName, string.Empty);
                //表題１               
                index = dataTableHeaderTmp.Columns.IndexOf("TITLE1");
                ctrlName = "FH_TITLE1_CTL";
                this.SetFieldName(ctrlName, string.Empty);
            }

            #endregion - Header -

            #region - Footer -
            ctrlName = "PHN_ALL_TOTAL_FLB";
            if (dataTableFooterTmp.Rows.Count > 0)
            {
                //合計
                index = dataTableFooterTmp.Columns.IndexOf("ALLHAISHURYO");               
                if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                {
                    this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                }
                else
                {
                    this.SetFieldName(ctrlName, string.Empty);
                }
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
