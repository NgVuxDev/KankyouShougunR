using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonChouhyouPopup.App;
using r_framework.Const;
using Shougun.Core.PayByProxy.DainoDenpyoHakkou.Const;
using r_framework.Utility;
using r_framework.Entity;
using r_framework.Dao;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.Report
{
    #region - Class -

    /// <summary>(R338)仕切書を表すクラス・コントロール</summary>
    public class ReportInfoR338 : ReportInfoBase
    {
        #region - Fields -

        /// <summary>Detail部に表示するレコート最大数を保持するフィールド</summary>
        private const int ConstMaxDispDetailRowCount = 5;

        /// <summary>画面ＩＤを保持するフィールド</summary>
        private WINDOW_ID windowID;

        /// <summary>帳票出力用データテーブルを保持するフィールド</summary>
        private DataTable dataTable = new DataTable();

        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        private M_SYS_INFO mSysInfo;
        #endregion - Fields -

        #region - Constructors -

        /// <summary>Initializes a new instance of the <see cref="ReportInfoR338"/> class.</summary>
        /// <param name="windowID">ウィンドウＩＤ</param>
        public ReportInfoR338(WINDOW_ID windowID)
        {
            this.windowID = windowID;

            var dao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mSysInfo = dao.GetAllData().FirstOrDefault();

            this.SetRecord(this.dataTable);
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

            // タイトル名
            dataTableTmp.Columns.Add("TITLE");
            // 担当名
            dataTableTmp.Columns.Add("TANTOU");
            // お取引先CD
            dataTableTmp.Columns.Add("TORIHIKISAKICD");
            // お取引先名
            dataTableTmp.Columns.Add("TORIHIKISAKIMEI");
            // お取引先名2
            dataTableTmp.Columns.Add("TORIHIKISAKIMEI2");
            // お取引先名敬称
            dataTableTmp.Columns.Add("TORIHIKISAKIKEISHOU");
            // お取引先名敬称2
            dataTableTmp.Columns.Add("TORIHIKISAKIKEISHOU2");
            // 伝票No
            dataTableTmp.Columns.Add("DENPYOUNO");
            // 乗員
            dataTableTmp.Columns.Add("JYOUIN");
            // 車番
            dataTableTmp.Columns.Add("SHABAN");
            // 車輌CD
            dataTableTmp.Columns.Add("SHARYOUCD");  // No.3837
            // 伝票日付
            dataTableTmp.Columns.Add("DENPYOUDATE");

            if (isPrintH)
            {
                rowTmp = dataTableTmp.NewRow();

                // タイトル名
                rowTmp["TITLE"] = "搬入御請求書１２３４,御請求書（控）１２３,計量証明１２３４５６";
                // 担当名
                rowTmp["TANTOU"] = "あいうえおかきく";
                // お取引先CD
                rowTmp["TORIHIKISAKICD"] = "123456";
                // お取引先名
                rowTmp["TORIHIKISAKIMEI"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // お取引先名2
                rowTmp["TORIHIKISAKIMEI2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // お取引先名敬称
                rowTmp["TORIHIKISAKIKEISHOU"] = "あい";
                // お取引先名敬称2
                rowTmp["TORIHIKISAKIKEISHOU2"] = "あい";
                // 伝票No
                rowTmp["DENPYOUNO"] = "1234567";
                // 乗員
                rowTmp["JYOUIN"] = "12";
                // 車番
                rowTmp["SHABAN"] = "1234567890";
                // 車輌CD
                rowTmp["SHARYOUCD"] = "123456";     // No.3837
                // 伝票日付
                rowTmp["DENPYOUDATE"] = "2013/12/09";

                dataTableTmp.Rows.Add(rowTmp);
            }

            this.DataTableList.Add("Header", dataTableTmp);

            #endregion - Header -

            #region - Detail -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Detail";

            // No
            dataTableTmp.Columns.Add("NUMBER");
            // 総重量
            dataTableTmp.Columns.Add("SOU_JYUURYOU");
            // 空車重量
            dataTableTmp.Columns.Add("KUUSHA_JYUURYOU");
            // 調整
            dataTableTmp.Columns.Add("CHOUSEI");
            // 容器引
            dataTableTmp.Columns.Add("YOUKIBIKI");
            // 正味
            dataTableTmp.Columns.Add("SHOUMI");
            // 数量
            dataTableTmp.Columns.Add("SUURYOU");
            // 数量単位名
            dataTableTmp.Columns.Add("FHN_SUURYOU_TANI");
            // 品名CD
            dataTableTmp.Columns.Add("FHN_HINMEICD");
            // 品名
            dataTableTmp.Columns.Add("HINMEI");
            // 単価
            dataTableTmp.Columns.Add("TANKA");
            // 金額
            dataTableTmp.Columns.Add("KINGAKU");

            if (isPrint)
            {
                for (int i = 0; i < 5; i++)
                {
                    rowTmp = dataTableTmp.NewRow();

                    // No
                    rowTmp["NUMBER"] = ((i + 1) * 10).ToString();
                    // 総重量
                    rowTmp["SOU_JYUURYOU"] = "123,456,789,000";
                    // 空車重量
                    rowTmp["KUUSHA_JYUURYOU"] = "123,456,789,000";
                    // 調整
                    rowTmp["CHOUSEI"] = "1,234,567,890";
                    // 容器引
                    rowTmp["YOUKIBIKI"] = "1,234,567,890";
                    // 正味
                    rowTmp["SHOUMI"] = "123,456,789,000";
                    // 数量
                    rowTmp["SUURYOU"] = "123,456,789,000";
                    // 数量単位名
                    rowTmp["FHN_SUURYOU_TANI"] = "あいうえお";
                    // 品名CD
                    rowTmp["FHN_HINMEICD"] = "123456";
                    // 品名
                    rowTmp["HINMEI"] = "あいうえおかきくけこさしすせそたちつてと";
                    // 単価
                    rowTmp["TANKA"] = "123,456,789,000";
                    // 金額
                    rowTmp["KINGAKU"] = "123,456,789,000";

                    dataTableTmp.Rows.Add(rowTmp);
                }
            }

            this.DataTableList.Add("Detail", dataTableTmp);

            #endregion - Detail -

            #region - Footer -

            dataTableTmp = new DataTable();
            dataTableTmp.TableName = "Footer";

            // 現場名
            dataTableTmp.Columns.Add("GENBA");
            // 正味合計
            dataTableTmp.Columns.Add("SHOUMI_GOUKEI");
            // 合計金額
            dataTableTmp.Columns.Add("GOUKEI_KINGAKU");
            // 備考
            dataTableTmp.Columns.Add("BIKOU");
            // 上段の請求・支払いラベル
            dataTableTmp.Columns.Add("SEIKYUU_SHIHARAI1");
            // 上段の請求・前回残高
            dataTableTmp.Columns.Add("UP_ZENKAI_ZANDAKA");
            // 上段の請求・伝票額（税抜）
            dataTableTmp.Columns.Add("UP_DENPYOUGAKU");
            // 上段の請求・消費税
            dataTableTmp.Columns.Add("UP_SHOUHIZEI");
            // 上段の請求・合計（税込）
            dataTableTmp.Columns.Add("UP_GOUKEI_ZEIKOMI");
            // 上段の請求・御精算額
            dataTableTmp.Columns.Add("UP_SEISANGAKU");
            // 上段の請求・差引残高
            dataTableTmp.Columns.Add("UP_SASHIHIKIZANDAKA");
            // 下段の請求・支払いラベル
            dataTableTmp.Columns.Add("SEIKYUU_SHIHARAI2");
            // 下段の請求・前回残高
            dataTableTmp.Columns.Add("DOWN_ZENKAI_ZANDAKA");
            // 下段の請求・伝票額（税抜）
            dataTableTmp.Columns.Add("DOWN_DENPYOUGAKU");
            // 下段の請求・消費税
            dataTableTmp.Columns.Add("DOWN_SHOUHIZEI");
            // 下段の請求・合計（税込）
            dataTableTmp.Columns.Add("DOWN_GOUKEI_ZEIKOMI");
            // 下段の請求・御精算額
            dataTableTmp.Columns.Add("DOWN_SEISANGAKU");
            // 下段の請求・差引残高
            dataTableTmp.Columns.Add("DOWN_SASHIHIKIZANDAKA");

            // 計量情報計量証明項目1
            dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU1");
            // 計量情報計量証明項目2
            dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU2");
            // 計量情報計量証明項目3
            dataTableTmp.Columns.Add("KEIRYOU_JYOUHOU3");

            // 会社名
            dataTableTmp.Columns.Add("CORP_RYAKU_NAME");
            // 拠点
            dataTableTmp.Columns.Add("KYOTEN_NAME");
            // 拠郵便番号
            dataTableTmp.Columns.Add("KYOTEN_POST");    // No.3048
            // 拠点住所1
            dataTableTmp.Columns.Add("KYOTEN_ADDRESS1");
            // 拠点住所2
            dataTableTmp.Columns.Add("KYOTEN_ADDRESS2");
            // 拠点電話
            dataTableTmp.Columns.Add("KYOTEN_TEL");
            // 拠点FAX
            dataTableTmp.Columns.Add("KYOTEN_FAX");

            // 相殺後金額
            dataTableTmp.Columns.Add("SOUSAI_KINGAKU");

            if (isPrint)
            {
                rowTmp = dataTableTmp.NewRow();

                // 現場名
                rowTmp["GENBA"] = "あいうえおかきくけこさしすせそたちつてと";
                // 正味合計
                rowTmp["SHOUMI_GOUKEI"] = "123,456,789,000";
                // 合計金額
                rowTmp["GOUKEI_KINGAKU"] = "123,456,789,000";
                // 備考
                rowTmp["BIKOU"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 上段の請求・支払いラベル
                rowTmp["SEIKYUU_SHIHARAI1"] = "支払";
                // 上段の請求・前回残高
                rowTmp["UP_ZENKAI_ZANDAKA"] = "123,456,789,000";
                // 上段の請求・伝票額（税抜）
                rowTmp["UP_DENPYOUGAKU"] = "123,456,789,000";
                // 上段の請求・消費税
                rowTmp["UP_SHOUHIZEI"] = "123,456,789,000";
                // 上段の請求・合計（税込）
                rowTmp["UP_GOUKEI_ZEIKOMI"] = "123,456,789,000";
                // 上段の請求・御精算額
                rowTmp["UP_SEISANGAKU"] = "123,456,789,000";
                // 上段の請求・差引残高
                rowTmp["UP_SASHIHIKIZANDAKA"] = "123,456,789,000";
                // 下段の請求・支払いラベル
                rowTmp["SEIKYUU_SHIHARAI2"] = "請求";
                // 下段の請求・前回残高
                rowTmp["DOWN_ZENKAI_ZANDAKA"] = "999";
                // 下段の請求・伝票額（税抜）
                rowTmp["DOWN_DENPYOUGAKU"] = "10";
                // 下段の請求・消費税
                rowTmp["DOWN_SHOUHIZEI"] = "222";
                // 下段の請求・合計（税込）
                rowTmp["DOWN_GOUKEI_ZEIKOMI"] = "3";
                // 下段の請求・御精算額
                rowTmp["DOWN_SEISANGAKU"] = "44";
                // 下段の請求・差引残高
                rowTmp["DOWN_SASHIHIKIZANDAKA"] = "000";

                // 計量情報計量証明項目1
                rowTmp["KEIRYOU_JYOUHOU1"] = "あいうえおかきくけこさしすせそたちつてと";
                // 計量情報計量証明項目2
                rowTmp["KEIRYOU_JYOUHOU2"] = "あいうえおかきくけこさしすせそたちつてと";
                // 計量情報計量証明項目3
                rowTmp["KEIRYOU_JYOUHOU3"] = "あいうえおかきくけこさしすせそたちつてと";

                // 会社名
                rowTmp["CORP_RYAKU_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                // 拠点
                rowTmp["KYOTEN_NAME"] = "あいうえおかきくけこさしすせそたちつてと";
                // 拠郵便番号
                rowTmp["KYOTEN_POST"] = "000-0000";
                // 拠点住所1
                rowTmp["KYOTEN_ADDRESS1"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 拠点住所2
                rowTmp["KYOTEN_ADDRESS2"] = "あいうえおかきくけこさしすせそたちつてとなにぬねの";
                // 拠点電話
                rowTmp["KYOTEN_TEL"] = "1234567890123";
                // 拠点FAX
                rowTmp["KYOTEN_FAX"] = "1234567890123";

                // 相殺後金額
                rowTmp["SOUSAI_KINGAKU"] = "相殺後御精算額:123,456,789,000,123,456,789";

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
            int pageNo = 1;
            DataRow row;
            string ctrlName = string.Empty;
            bool detailComp = false;

            // 帳票出力用データの設定処理
            this.SetChouhyouInfo();

            #region - Detail -

            #region - Columns -

            for (int i = 1; i <= ConstMaxDispDetailRowCount; i++)
            {
                // No
                ctrlName = string.Format("PHN_NUMBER_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 総重量
                ctrlName = string.Format("PHN_SOU_JYUURYOU_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 空車重量
                ctrlName = string.Format("PHN_KUUSHA_JYUURYOU_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 調整
                ctrlName = string.Format("PHN_CHOUSEI_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 容器引
                ctrlName = string.Format("PHN_YOUKIBIKI_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 正味
                ctrlName = string.Format("PHN_SHOUMI_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 数量
                ctrlName = string.Format("PHN_SUURYOU_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 数量単位名
                ctrlName = string.Format("PHN_SUURYOU_TANI_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 品名CD
                ctrlName = string.Format("PHN_HINMEICD_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 品名
                ctrlName = string.Format("PHN_HINMEI_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 単価
                ctrlName = string.Format("PHN_TANKA_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
                // 金額
                ctrlName = string.Format("PHN_KINGAKU_{0}_FLB", i);
                this.dataTable.Columns.Add(ctrlName);
            }

            // 正味合計
            ctrlName = "PHN_SHOUMI_GOUKEI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 合計金額
            ctrlName = "PHN_GOUKEI_KINGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 上段の前回残高
            ctrlName = "PHN_UP_ZENKAI_ZANDAKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 上段の伝票額(税抜き)
            ctrlName = "PHN_UP_DENPYOUGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 上段の消費税
            ctrlName = "PHN_UP_SHOUHIZEI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 上段の合計(税込み)
            ctrlName = "PHN_UP_GOUKEI_ZEIKOMI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 上段の御精算額
            ctrlName = "PHN_UP_SEISANGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 上段の差引残高
            ctrlName = "PHN_UP_SASHIHIKIZANDAKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 下段の前回残高
            ctrlName = "PHN_DOWN_ZENKAI_ZANDAKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 下段の伝票額(税抜き)
            ctrlName = "PHN_DOWN_DENPYOUGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 下段の消費税
            ctrlName = "PHN_DOWN_SHOUHIZEI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 下段の合計(税込み)
            ctrlName = "PHN_DOWN_GOUKEI_ZEIKOMI_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 下段の御精算額
            ctrlName = "PHN_DOWN_SEISANGAKU_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 下段の差引残高
            ctrlName = "PHN_DOWN_SASHIHIKIZANDAKA_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 相殺後金額
            ctrlName = "PHN_SOUSAI_KINGAKU_1_FLB";
            this.dataTable.Columns.Add(ctrlName);
            // 相殺後金額（控）
            ctrlName = "PHN_SOUSAI_KINGAKU_2_FLB";
            this.dataTable.Columns.Add(ctrlName);

            #endregion - Columns -

            DataTable dataTableDetailTmp = this.DataTableList["Detail"];
            DataTable dataTableFooterTmp = this.DataTableList["Footer"];
            int detailMaxCount = dataTableDetailTmp.Rows.Count;
            int maxPage = (int)Math.Ceiling((decimal)detailMaxCount / ConstMaxDispDetailRowCount);

            if (maxPage == 0)
            {
                maxPage = 1;
                detailMaxCount = 1;
                detailComp = true;
            }

            int maxRow = maxPage * ConstMaxDispDetailRowCount;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            rowNo = 1;
            row = this.dataTable.NewRow();
            for (int i = 0; i < detailMaxCount; i++)
            {
                if (!detailComp)
                {
                    // No
                    index = dataTableDetailTmp.Columns.IndexOf("NUMBER");
                    ctrlName = string.Format("PHN_NUMBER_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }

                    // 総重量
                    index = dataTableDetailTmp.Columns.IndexOf("SOU_JYUURYOU");
                    ctrlName = string.Format("PHN_SOU_JYUURYOU_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 1);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 空車重量
                    index = dataTableDetailTmp.Columns.IndexOf("KUUSHA_JYUURYOU");
                    ctrlName = string.Format("PHN_KUUSHA_JYUURYOU_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 1);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 調整
                    index = dataTableDetailTmp.Columns.IndexOf("CHOUSEI");
                    ctrlName = string.Format("PHN_CHOUSEI_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 1);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 容器引
                    index = dataTableDetailTmp.Columns.IndexOf("YOUKIBIKI");
                    ctrlName = string.Format("PHN_YOUKIBIKI_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 1);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 正味
                    index = dataTableDetailTmp.Columns.IndexOf("SHOUMI");
                    ctrlName = string.Format("PHN_SHOUMI_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 1);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 数量
                    index = dataTableDetailTmp.Columns.IndexOf("SUURYOU");
                    ctrlName = string.Format("PHN_SUURYOU_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 2);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 数量単位名
                    index = dataTableDetailTmp.Columns.IndexOf("FHN_SUURYOU_TANI");
                    ctrlName = string.Format("PHN_SUURYOU_TANI_{0}_FLB", rowNo);
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
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 品名CD
                    index = dataTableDetailTmp.Columns.IndexOf("FHN_HINMEICD");
                    ctrlName = string.Format("PHN_HINMEICD_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 品名
                    index = dataTableDetailTmp.Columns.IndexOf("HINMEI");
                    ctrlName = string.Format("PHN_HINMEI_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableDetailTmp.Rows[i].ItemArray[index].ToString());
                        if (byteArray.Length > 40)
                        {
                            row[ctrlName] = encoding.GetString(byteArray, 0, 20);
                        }
                        else
                        {
                            row[ctrlName] = dataTableDetailTmp.Rows[i].ItemArray[index];
                        }
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 単価
                    index = dataTableDetailTmp.Columns.IndexOf("TANKA");
                    ctrlName = string.Format("PHN_TANKA_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 3);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                    
                    // 金額
                    index = dataTableDetailTmp.Columns.IndexOf("KINGAKU");
                    ctrlName = string.Format("PHN_KINGAKU_{0}_FLB", rowNo);
                    if (!this.IsDBNull(dataTableDetailTmp.Rows[i].ItemArray[index]))
                    {
                        row[ctrlName] = DataFormatChange(dataTableDetailTmp.Rows[i].ItemArray[index].ToString(), 0);
                    }
                    else
                    {   // NULL
                        row[ctrlName] = string.Empty;
                    }
                }
                else
                {
                    // No
                    index = dataTableDetailTmp.Columns.IndexOf("NUMBER");
                    ctrlName = string.Format("PHN_NUMBER_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 総重量
                    index = dataTableDetailTmp.Columns.IndexOf("SOU_JYUURYOU");
                    ctrlName = string.Format("PHN_SOU_JYUURYOU_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 空車重量
                    index = dataTableDetailTmp.Columns.IndexOf("KUUSHA_JYUURYOU");
                    ctrlName = string.Format("PHN_KUUSHA_JYUURYOU_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 調整
                    index = dataTableDetailTmp.Columns.IndexOf("CHOUSEI");
                    ctrlName = string.Format("PHN_CHOUSEI_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 容器引
                    index = dataTableDetailTmp.Columns.IndexOf("YOUKIBIKI");
                    ctrlName = string.Format("PHN_YOUKIBIKI_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 正味
                    index = dataTableDetailTmp.Columns.IndexOf("SHOUMI");
                    ctrlName = string.Format("PHN_SHOUMI_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 数量
                    index = dataTableDetailTmp.Columns.IndexOf("SUURYOU");
                    ctrlName = string.Format("PHN_SUURYOU_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 数量単位名
                    index = dataTableDetailTmp.Columns.IndexOf("FHN_SUURYOU_TANI");
                    ctrlName = string.Format("PHN_SUURYOU_TANI_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 品名CD
                    index = dataTableDetailTmp.Columns.IndexOf("FHN_HINMEICD");
                    ctrlName = string.Format("PHN_HINMEICD_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 品名
                    index = dataTableDetailTmp.Columns.IndexOf("HINMEI");
                    ctrlName = string.Format("PHN_HINMEI_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 単価
                    index = dataTableDetailTmp.Columns.IndexOf("TANKA");
                    ctrlName = string.Format("PHN_TANKA_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                    
                    // 金額
                    index = dataTableDetailTmp.Columns.IndexOf("KINGAKU");
                    ctrlName = string.Format("PHN_KINGAKU_{0}_FLB", rowNo);
                    row[ctrlName] = string.Empty;
                }

                if (pageNo == maxPage)
                {
                    if (dataTableFooterTmp.Rows.Count > 0)
                    {
                        // 正味合計
                        index = dataTableFooterTmp.Columns.IndexOf("SHOUMI_GOUKEI");
                        ctrlName = string.Format("PHN_SHOUMI_GOUKEI_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 1);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }

                        // 合計金額
                        index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        ctrlName = string.Format("PHN_GOUKEI_KINGAKU_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 上段の請求・前回残高
                        index = dataTableFooterTmp.Columns.IndexOf("UP_ZENKAI_ZANDAKA");
                        ctrlName = string.Format("PHN_UP_ZENKAI_ZANDAKA_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 上段の請求・伝票額（税抜）
                        index = dataTableFooterTmp.Columns.IndexOf("UP_DENPYOUGAKU");
                        ctrlName = string.Format("PHN_UP_DENPYOUGAKU_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 上段の請求・消費税
                        index = dataTableFooterTmp.Columns.IndexOf("UP_SHOUHIZEI");
                        ctrlName = string.Format("PHN_UP_SHOUHIZEI_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 上段の請求・合計（税込）
                        index = dataTableFooterTmp.Columns.IndexOf("UP_GOUKEI_ZEIKOMI");
                        ctrlName = string.Format("PHN_UP_GOUKEI_ZEIKOMI_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 上段の請求・御精算額
                        index = dataTableFooterTmp.Columns.IndexOf("UP_SEISANGAKU");
                        ctrlName = string.Format("PHN_UP_SEISANGAKU_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 上段の請求・差引残高
                        index = dataTableFooterTmp.Columns.IndexOf("UP_SASHIHIKIZANDAKA");
                        ctrlName = string.Format("PHN_UP_SASHIHIKIZANDAKA_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 下段の請求・前回残高
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_ZENKAI_ZANDAKA");
                        ctrlName = string.Format("PHN_DOWN_ZENKAI_ZANDAKA_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 下段の請求・伝票額（税抜）
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_DENPYOUGAKU");
                        ctrlName = string.Format("PHN_DOWN_DENPYOUGAKU_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 下段の請求・消費税
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_SHOUHIZEI");
                        ctrlName = string.Format("PHN_DOWN_SHOUHIZEI_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 下段の請求・合計（税込）
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_GOUKEI_ZEIKOMI");
                        ctrlName = string.Format("PHN_DOWN_GOUKEI_ZEIKOMI_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 下段の請求・御精算額
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_SEISANGAKU");
                        ctrlName = string.Format("PHN_DOWN_SEISANGAKU_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }
                        
                        // 下段の請求・差引残高
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_SASHIHIKIZANDAKA");
                        ctrlName = string.Format("PHN_DOWN_SASHIHIKIZANDAKA_FLB");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            row[ctrlName] = DataFormatChange(dataTableFooterTmp.Rows[0].ItemArray[index].ToString(), 0);
                        }
                        else
                        {   // NULL
                            row[ctrlName] = string.Empty;
                        }

                        // 相殺後金額
                        index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                        ctrlName = "PHN_SOUSAI_KINGAKU_1_FLB";
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                            }
                            else
                            {
                                row[ctrlName] = (string)dataTableFooterTmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {   // Null
                            row[ctrlName] = string.Empty;
                        }

                        // 相殺後金額（控）
                        index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                        ctrlName = "PHN_SOUSAI_KINGAKU_2_FLB";
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                            if (byteArray.Length > 40)
                            {
                                row[ctrlName] = encoding.GetString(byteArray, 0, 40);
                            }
                            else
                            {
                                row[ctrlName] = (string)dataTableFooterTmp.Rows[0].ItemArray[index];
                            }
                        }
                        else
                        {   // Null
                            row[ctrlName] = string.Empty;
                        }
                    }
                    else
                    {
                        // 正味合計
                        index = dataTableFooterTmp.Columns.IndexOf("SHOUMI_GOUKEI");
                        ctrlName = string.Format("PHN_SHOUMI_GOUKEI_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 合計金額
                        index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                        ctrlName = string.Format("PHN_GOUKEI_KINGAKU_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 上段の請求・前回残高
                        index = dataTableFooterTmp.Columns.IndexOf("UP_ZENKAI_ZANDAKA");
                        ctrlName = string.Format("PHN_UP_ZENKAI_ZANDAKA_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 上段の請求・伝票額（税抜）
                        index = dataTableFooterTmp.Columns.IndexOf("UP_DENPYOUGAKU");
                        ctrlName = string.Format("PHN_UP_DENPYOUGAKU_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 上段の請求・消費税
                        index = dataTableFooterTmp.Columns.IndexOf("UP_SHOUHIZEI");
                        ctrlName = string.Format("PHN_UP_SHOUHIZEI_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 上段の請求・合計（税込）
                        index = dataTableFooterTmp.Columns.IndexOf("UP_GOUKEI_ZEIKOMI");
                        ctrlName = string.Format("PHN_UP_GOUKEI_ZEIKOMI_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 上段の請求・御精算額
                        index = dataTableFooterTmp.Columns.IndexOf("UP_SEISANGAKU");
                        ctrlName = string.Format("PHN_UP_SEISANGAKU_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 上段の請求・差引残高
                        index = dataTableFooterTmp.Columns.IndexOf("UP_SASHIHIKIZANDAKA");
                        ctrlName = string.Format("PHN_UP_SASHIHIKIZANDAKA_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 下段の請求・前回残高
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_ZENKAI_ZANDAKA");
                        ctrlName = string.Format("PHN_DOWN_ZENKAI_ZANDAKA_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 下段の請求・伝票額（税抜）
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_DENPYOUGAKU");
                        ctrlName = string.Format("PHN_DOWN_DENPYOUGAKU_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 下段の請求・消費税
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_SHOUHIZEI");
                        ctrlName = string.Format("PHN_DOWN_SHOUHIZEI_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 下段の請求・合計（税込）
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_GOUKEI_ZEIKOMI");
                        ctrlName = string.Format("PHN_DOWN_GOUKEI_ZEIKOMI_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 下段の請求・御精算額
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_SEISANGAKU");
                        ctrlName = string.Format("PHN_DOWN_SEISANGAKU_FLB");
                        row[ctrlName] = string.Empty;
                        
                        // 下段の請求・差引残高
                        index = dataTableFooterTmp.Columns.IndexOf("DOWN_SASHIHIKIZANDAKA");
                        ctrlName = string.Format("PHN_DOWN_SASHIHIKIZANDAKA_FLB");
                        row[ctrlName] = string.Empty;

                        // 相殺後金額
                        index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                        ctrlName = "PHN_SOUSAI_KINGAKU_1_FLB";
                        row[ctrlName] = string.Empty;

                        // 相殺後金額（控）
                        index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                        ctrlName = "PHN_SOUSAI_KINGAKU_2_FLB";
                        row[ctrlName] = string.Empty;
                    }
                }
                else
                {
                    // 正味合計
                    index = dataTableFooterTmp.Columns.IndexOf("SHOUMI_GOUKEI");
                    ctrlName = string.Format("PHN_SHOUMI_GOUKEI_FLB");
                    row[ctrlName] = "************";
                    
                    // 合計金額
                    index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                    ctrlName = string.Format("PHN_GOUKEI_KINGAKU_FLB");
                    row[ctrlName] = "*************";
                    
                    // 上段の請求・前回残高
                    index = dataTableFooterTmp.Columns.IndexOf("UP_ZENKAI_ZANDAKA");
                    ctrlName = string.Format("PHN_UP_ZENKAI_ZANDAKA_FLB");
                    row[ctrlName] = "*************";
                    
                    // 上段の請求・伝票額（税抜）
                    index = dataTableFooterTmp.Columns.IndexOf("UP_DENPYOUGAKU");
                    ctrlName = string.Format("PHN_UP_DENPYOUGAKU_FLB");
                    row[ctrlName] = "*************";
                    
                    // 上段の請求・消費税
                    index = dataTableFooterTmp.Columns.IndexOf("UP_SHOUHIZEI");
                    ctrlName = string.Format("PHN_UP_SHOUHIZEI_FLB");
                    row[ctrlName] = "*******";
                    
                    // 上段の請求・合計（税込）
                    index = dataTableFooterTmp.Columns.IndexOf("UP_GOUKEI_ZEIKOMI");
                    ctrlName = string.Format("PHN_UP_GOUKEI_ZEIKOMI_FLB");
                    row[ctrlName] = "*************";
                    
                    // 上段の請求・御精算額
                    index = dataTableFooterTmp.Columns.IndexOf("UP_SEISANGAKU");
                    ctrlName = string.Format("PHN_UP_SEISANGAKU_FLB");
                    row[ctrlName] = "*************";
                    
                    // 上段の請求・差引残高
                    index = dataTableFooterTmp.Columns.IndexOf("UP_SASHIHIKIZANDAKA");
                    ctrlName = string.Format("PHN_UP_SASHIHIKIZANDAKA_FLB");
                    row[ctrlName] = "*************";
                    
                    // 下段の請求・前回残高
                    index = dataTableFooterTmp.Columns.IndexOf("DOWN_ZENKAI_ZANDAKA");
                    ctrlName = string.Format("PHN_DOWN_ZENKAI_ZANDAKA_FLB");
                    row[ctrlName] = "*************";
                    
                    // 下段の請求・伝票額（税抜）
                    index = dataTableFooterTmp.Columns.IndexOf("DOWN_DENPYOUGAKU");
                    ctrlName = string.Format("PHN_DOWN_DENPYOUGAKU_FLB");
                    row[ctrlName] = "*************";
                    
                    // 下段の請求・消費税
                    index = dataTableFooterTmp.Columns.IndexOf("DOWN_SHOUHIZEI");
                    ctrlName = string.Format("PHN_DOWN_SHOUHIZEI_FLB");
                    row[ctrlName] = "*******";
                    
                    // 下段の請求・合計（税込）
                    index = dataTableFooterTmp.Columns.IndexOf("DOWN_GOUKEI_ZEIKOMI");
                    ctrlName = string.Format("PHN_DOWN_GOUKEI_ZEIKOMI_FLB");
                    row[ctrlName] = "*************";
                    
                    // 下段の請求・御精算額
                    index = dataTableFooterTmp.Columns.IndexOf("DOWN_SEISANGAKU");
                    ctrlName = string.Format("PHN_DOWN_SEISANGAKU_FLB");
                    row[ctrlName] = "*************";
                    
                    // 下段の請求・差引残高
                    index = dataTableFooterTmp.Columns.IndexOf("DOWN_SASHIHIKIZANDAKA");
                    ctrlName = string.Format("PHN_DOWN_SASHIHIKIZANDAKA_FLB");
                    row[ctrlName] = "*************";

                    // 相殺後金額
                    index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                    ctrlName = "PHN_SOUSAI_KINGAKU_1_FLB";
                    row[ctrlName] = string.Empty;

                    // 相殺後金額（控）
                    index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                    ctrlName = "PHN_SOUSAI_KINGAKU_2_FLB";
                    row[ctrlName] = string.Empty;
                }

                if (rowNo == ConstMaxDispDetailRowCount)
                {
                    this.dataTable.Rows.Add(row);

                    rowNo = 1;
                    pageNo++;
                    row = this.dataTable.NewRow();
                }
                else
                {
                    rowNo++;
                }
            }

            if (rowNo > 1)
            {
                this.dataTable.Rows.Add(row);
            }

            this.SetRecord(this.dataTable);

            #endregion - Detail -
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
            string[] titleTmp = null;
            string ctrlName = string.Empty;
            string fieldName = string.Empty;
            string tmp;

            Encoding encoding = Encoding.GetEncoding("Shift_JIS");
            byte[] byteArray;

            r_framework.Utility.LogUtility.DebugMethodStart();

            // タイトル名(カンマ区切りの３個)
            if (dataTableHeaderTmp.Rows.Count > 0)
            {
                index = dataTableHeaderTmp.Columns.IndexOf("TITLE");
                tmp = (string)dataTableHeaderTmp.Rows[0].ItemArray[index];

                titleTmp = tmp.Split(',');
            }
            else
            {
                tmp = string.Empty;
            }

            for (int pos = 1; pos <= 3; pos++)
            {
                #region - Header -

                if (dataTableHeaderTmp.Rows.Count > 0)
                {
                    // タイトル名
                    ctrlName = string.Format("DH_TITLE_VLB{0}", pos);
                    this.SetFieldName(ctrlName, titleTmp[pos - 1]);

                    // 担当名
                    index = dataTableHeaderTmp.Columns.IndexOf("TANTOU");
                    ctrlName = string.Format("DH_TANTOU_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                        if (byteArray.Length > 20)
                        {
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                    // お取引先CD
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKICD");
                    ctrlName = string.Format("DH_TORIHIKISAKICD_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                    // お取引先名
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIMEI");
                    ctrlName = string.Format("DH_TORIHIKISAKIMEI_CTL{0}", pos);
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
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // お取引先名2
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIMEI2");
                    ctrlName = string.Format("DH_TORIHIKISAKIMEI2_CTL{0}", pos);
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
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // お取引先名敬称
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIKEISHOU");
                    ctrlName = string.Format("DH_TORIHIKISAKIKEISHOU_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                        if (byteArray.Length > 10)
                        {
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 10));
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // お取引先名敬称2
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIKEISHOU2");
                    ctrlName = string.Format("DH_TORIHIKISAKIKEISHOU2_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                        if (byteArray.Length > 10)
                        {
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 10));
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 伝票No
                    index = dataTableHeaderTmp.Columns.IndexOf("DENPYOUNO");
                    ctrlName = string.Format("DH_DENPYOUNO_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                    // 乗員
                    index = dataTableHeaderTmp.Columns.IndexOf("JYOUIN");
                    ctrlName = string.Format("DH_JYOUIN_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                    // 車番
                    index = dataTableHeaderTmp.Columns.IndexOf("SHABAN");
                    ctrlName = string.Format("DH_SHABAN_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                    // No.3837-->
                    // 車輌CD
                    index = dataTableHeaderTmp.Columns.IndexOf("SHARYOUCD");
                    ctrlName = string.Format("DH_SHARYOUCD_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                    // No.3837<--
                    // 伝票日付
                    index = dataTableHeaderTmp.Columns.IndexOf("DENPYOUDATE");
                    ctrlName = string.Format("DH_DENPYOUDATE_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                }
                else
                {
                    // タイトル名
                    ctrlName = string.Format("DH_TITLE_VLB{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // 担当名
                    index = dataTableHeaderTmp.Columns.IndexOf("TANTOU");
                    ctrlName = string.Format("DH_TANTOU_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // お取引先CD
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKICD");
                    ctrlName = string.Format("DH_TORIHIKISAKICD_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // お取引先名
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIMEI");
                    ctrlName = string.Format("DH_TORIHIKISAKIMEI_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // お取引先名2
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIMEI2");
                    ctrlName = string.Format("DH_TORIHIKISAKIMEI2_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // お取引先名敬称
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIKEISHOU");
                    ctrlName = string.Format("DH_TORIHIKISAKIKEISHOU_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // お取引先名敬称2
                    index = dataTableHeaderTmp.Columns.IndexOf("TORIHIKISAKIKEISHOU2");
                    ctrlName = string.Format("DH_TORIHIKISAKIKEISHOU2_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // 伝票No
                    index = dataTableHeaderTmp.Columns.IndexOf("DENPYOUNO");
                    ctrlName = string.Format("DH_DENPYOUNO_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // 乗員
                    index = dataTableHeaderTmp.Columns.IndexOf("JYOUIN");
                    ctrlName = string.Format("DH_JYOUIN_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // 車番
                    index = dataTableHeaderTmp.Columns.IndexOf("SHABAN");
                    ctrlName = string.Format("DH_SHABAN_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // No.3837-->
                    // 車輌CD
                    index = dataTableHeaderTmp.Columns.IndexOf("SHARYOUCD");
                    ctrlName = string.Format("DH_SHARYOUCD_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // No.3837<--
                    // 伝票日付
                    index = dataTableHeaderTmp.Columns.IndexOf("DENPYOUDATE");
                    ctrlName = string.Format("DH_DENPYOUDATE_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                }
                #endregion - Header -

                #region - Footer -

                if (dataTableFooterTmp.Rows.Count > 0)
                {
                    // 現場名
                    index = dataTableFooterTmp.Columns.IndexOf("GENBA");
                    ctrlName = string.Format("DF_GENBA_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                        //if (byteArray.Length > 20)    // No.3049
                        if (byteArray.Length > 80)      // No.3049
                        {
                            //this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));    // No.3049
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 80));      // No.3049
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 備考
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU");
                    ctrlName = string.Format("DF_BIKOU_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableHeaderTmp.Rows[0].ItemArray[index].ToString());
                        if (byteArray.Length > 40)
                        {
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 上段の支払い・請求ラベル
                    index = dataTableFooterTmp.Columns.IndexOf("SEIKYUU_SHIHARAI1");
                    ctrlName = string.Format("DF_SEIKYUU_SHIHARAI1_VLB{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        //if ((string)dataTableFooterTmp.Rows[0].ItemArray[index] == ConstClass.SASIHIKI_KIJUN_SHIHARAI)
                        //{
                        //    ctrlName = string.Format("DF_SKY_SEISANGAKU_FLB{0}", pos);
                        //    this.SetFieldName(ctrlName, ConstClass.GOSEISANGAKU);
                        //}

                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 下段の支払い・請求ラベル
                    index = dataTableFooterTmp.Columns.IndexOf("SEIKYUU_SHIHARAI2");
                    ctrlName = string.Format("DF_SEIKYUU_SHIHARAI2_VLB{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        //if ((string)dataTableFooterTmp.Rows[0].ItemArray[index] == ConstClass.SASIHIKI_KIJUN_SEIKYUU)
                        //{
                        //    ctrlName = string.Format("DF_SHR_SEISANGAKU_FLB{0}", pos);
                        //    this.SetFieldName(ctrlName, ConstClass.GOSEIKYUUGAKU);
                        //}
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    if (pos == 3)
                    {   // 計量証明

                        // 計量情報計量証明項目1
                        index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DF_KEIRYOU_JYOUHOU1_CTL3", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // Null
                            this.SetFieldName("DF_KEIRYOU_JYOUHOU1_CTL3", string.Empty);
                        }
                        // 計量情報計量証明項目2
                        index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DF_KEIRYOU_JYOUHOU2_CTL3", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // Null
                            this.SetFieldName("DF_KEIRYOU_JYOUHOU2_CTL3", string.Empty);
                        }
                        // 計量情報計量証明項目3
                        index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                        if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        {
                            this.SetFieldName("DF_KEIRYOU_JYOUHOU3_CTL3", (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                        else
                        {   // Null
                            this.SetFieldName("DF_KEIRYOU_JYOUHOU3_CTL3", string.Empty);
                        }
                    }
                    else
                    {   // 御請求書又は御請求書（控）

                        //// 相殺後金額
                        //index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                        //ctrlName = string.Format("DF_SOUSAI_KINGAKU_CTL{0}", pos);
                        //if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                        //{
                        //    btBytes = hEncoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                        //    if (btBytes.Length > 40)
                        //    {
                        //        this.SetFieldName(ctrlName, hEncoding.GetString(btBytes, 0, 40));
                        //    }
                        //    else
                        //    {
                        //        this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        //    }
                        //}
                        //else
                        //{   // Null
                        //    this.SetFieldName(ctrlName, string.Empty);
                        //}
                    }

                    // 会社名
                    index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                    ctrlName = string.Format("DF_CORP_RYAKU_NAME_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                        //if (byteArray.Length > 20)  // No.3837
                        if (byteArray.Length > 40)  // No.3837
                        {
                            //this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));    // No.3837
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));    // No.3837
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 拠点
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                    ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                        if (byteArray.Length > 20)
                        {
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 20));
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // No.3048-->
                    // 拠点郵便番号
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_POST");
                    ctrlName = string.Format("DF_KYOTEN_POST_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, "〒 " + (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                    // No.3048<--

                    // 拠点住所1
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                    ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                        if (byteArray.Length > 40)
                        {
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 拠点住所2
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                    ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        byteArray = encoding.GetBytes(dataTableFooterTmp.Rows[0].ItemArray[index].ToString());
                        if (byteArray.Length > 40)
                        {
                            this.SetFieldName(ctrlName, encoding.GetString(byteArray, 0, 40));
                        }
                        else
                        {
                            this.SetFieldName(ctrlName, (string)dataTableFooterTmp.Rows[0].ItemArray[index]);
                        }
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 拠点電話
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                    ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        //this.SetFieldName(ctrlName, "電話 " + (string)dataTableFooterTmp.Rows[0].ItemArray[index]);    // No.3048
                        this.SetFieldName(ctrlName, "TEL " + (string)dataTableFooterTmp.Rows[0].ItemArray[index]);    // No.3837
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 拠点FAX
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                    ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                    if (!this.IsDBNull(dataTableFooterTmp.Rows[0].ItemArray[index]))
                    {
                        this.SetFieldName(ctrlName, "FAX " + (string)dataTableFooterTmp.Rows[0].ItemArray[index]);    // No.3048
                    }
                    else
                    {   // Null
                        this.SetFieldName(ctrlName, string.Empty);
                    }
                }
                else
                {
                    // 現場名
                    index = dataTableFooterTmp.Columns.IndexOf("GENBA");
                    ctrlName = string.Format("DF_GENBA_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);

                    // 正味合計
                    index = dataTableFooterTmp.Columns.IndexOf("SHOUMI_GOUKEI");
                    ctrlName = string.Format("DF_SHOUMI_GOUKEI_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // 合計金額
                    index = dataTableFooterTmp.Columns.IndexOf("GOUKEI_KINGAKU");
                    ctrlName = string.Format("DF_GOUKEI_KINGAKU_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);

                    // 備考
                    index = dataTableFooterTmp.Columns.IndexOf("BIKOU");
                    ctrlName = string.Format("DF_BIKOU_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);

                    if (pos == 3)
                    {   // 計量証明

                        // 計量情報計量証明項目1
                        index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU1");
                        this.SetFieldName("DF_KEIRYOU_JYOUHOU1_CTL3", string.Empty);

                        // 計量情報計量証明項目2
                        index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU2");
                        this.SetFieldName("DF_KEIRYOU_JYOUHOU2_CTL3", string.Empty);

                        // 計量情報計量証明項目3
                        index = dataTableFooterTmp.Columns.IndexOf("KEIRYOU_JYOUHOU3");
                        this.SetFieldName("DF_KEIRYOU_JYOUHOU3_CTL3", string.Empty);
                    }
                    else
                    {   // 御請求書又は御請求書（控）

                        // 相殺後金額
                        index = dataTableFooterTmp.Columns.IndexOf("SOUSAI_KINGAKU");
                        ctrlName = string.Format("DF_SOUSAI_KINGAKU_CTL{0}", pos);
                        this.SetFieldName(ctrlName, string.Empty);
                    }

                    // 会社名
                    index = dataTableFooterTmp.Columns.IndexOf("CORP_RYAKU_NAME");
                    ctrlName = string.Format("DF_CORP_RYAKU_NAME_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);

                    // 拠点
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_NAME");
                    ctrlName = string.Format("DF_KYOTEN_NAME_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);

                    // No.3048-->
                    // 拠点郵便番号
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_POST");
                    ctrlName = string.Format("DF_KYOTEN_POST_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    // No.3048<--

                    // 拠点住所1
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS1");
                    ctrlName = string.Format("DF_KYOTEN_ADDRESS1_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    
                    // 拠点住所2
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_ADDRESS2");
                    ctrlName = string.Format("DF_KYOTEN_ADDRESS2_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    
                    // 拠点電話
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_TEL");
                    ctrlName = string.Format("DF_KYOTEN_TEL_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                    
                    // 拠点FAX
                    index = dataTableFooterTmp.Columns.IndexOf("KYOTEN_FAX");
                    ctrlName = string.Format("DF_KYOTEN_FAX_CTL{0}", pos);
                    this.SetFieldName(ctrlName, string.Empty);
                }

                #endregion - Footer -
            }

            if (this.mSysInfo.UKEIRESHUKA_GAMEN_SIZE.Value == 1)
            {
                int index1 = dataTableHeaderTmp.Columns.IndexOf("DENPYOU_SHURUI");
                if (index1 >= 0)
                {
                    string shurui = (string)dataTableHeaderTmp.Rows[0].ItemArray[index1];
                    if (this.mSysInfo.SYS_BARCODO_SHINKAKU_KBN.Value == 1)
                    {
                        index = dataTableHeaderTmp.Columns.IndexOf("BARCODE");
                        string ctrlName1 = "BARCODE_CTL1";
                        string ctrlName2 = "BARCODE_CTL2";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            if ((shurui == "1" && this.mSysInfo.UKEIRE_BARCODE_JOUDAN_KBN.Value == 1)
                                || (shurui == "2" && this.mSysInfo.SHUKKA_BARCODE_JOUDAN_KBN.Value == 1)
                                || (shurui == "3" && this.mSysInfo.UR_SH_BARCODE_JOUDAN_KBN.Value == 1)
                                || (shurui == "4" && this.mSysInfo.KENSHUU_BARCODE_JOUDAN_KBN.Value == 1))
                            {
                                this.SetFieldName(ctrlName1, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {
                                this.SetFieldName(ctrlName1, string.Empty);
                            }

                            if ((shurui == "1" && this.mSysInfo.UKEIRE_BARCODE_CHUUDAN_KBN.Value == 1)
                                || (shurui == "2" && this.mSysInfo.SHUKKA_BARCODE_CHUUDAN_KBN.Value == 1)
                                || (shurui == "3" && this.mSysInfo.UR_SH_BARCODE_CHUUDAN_KBN.Value == 1)
                                || (shurui == "4" && this.mSysInfo.KENSHUU_BARCODE_CHUUDAN_KBN.Value == 1))
                            {
                                this.SetFieldName(ctrlName2, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {
                                this.SetFieldName(ctrlName2, string.Empty);
                            }
                        }
                        else
                        {
                            this.SetFieldName(ctrlName1, string.Empty);
                            this.SetFieldName(ctrlName2, string.Empty);
                        }

                        string ctrlName3 = "QRCODE_CTL1";
                        string ctrlName4 = "QRCODE_CTL2";
                        this.SetFieldVisible(ctrlName3, false);
                        this.SetFieldVisible(ctrlName4, false);
                    }
                    else
                    {
                        index = dataTableHeaderTmp.Columns.IndexOf("QRCODE");
                        string ctrlName1 = "QRCODE_CTL1";
                        string ctrlName2 = "QRCODE_CTL2";
                        if (!this.IsDBNull(dataTableHeaderTmp.Rows[0].ItemArray[index]))
                        {
                            if ((shurui == "1" && this.mSysInfo.UKEIRE_BARCODE_JOUDAN_KBN.Value == 1)
                                || (shurui == "2" && this.mSysInfo.SHUKKA_BARCODE_JOUDAN_KBN.Value == 1)
                                || (shurui == "3" && this.mSysInfo.UR_SH_BARCODE_JOUDAN_KBN.Value == 1)
                                || (shurui == "4" && this.mSysInfo.KENSHUU_BARCODE_JOUDAN_KBN.Value == 1))
                            {
                                this.SetFieldName(ctrlName1, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {
                                this.SetFieldName(ctrlName1, string.Empty);
                            }

                            if ((shurui == "1" && this.mSysInfo.UKEIRE_BARCODE_CHUUDAN_KBN.Value == 1)
                                || (shurui == "2" && this.mSysInfo.SHUKKA_BARCODE_CHUUDAN_KBN.Value == 1)
                                || (shurui == "3" && this.mSysInfo.UR_SH_BARCODE_CHUUDAN_KBN.Value == 1)
                                || (shurui == "4" && this.mSysInfo.KENSHUU_BARCODE_CHUUDAN_KBN.Value == 1))
                            {
                                this.SetFieldName(ctrlName2, (string)dataTableHeaderTmp.Rows[0].ItemArray[index]);
                            }
                            else
                            {
                                this.SetFieldName(ctrlName2, string.Empty);
                            }
                        }
                        else
                        {
                            this.SetFieldName(ctrlName1, string.Empty);
                            this.SetFieldName(ctrlName2, string.Empty);
                        }

                        string ctrlName3 = "BARCODE_CTL1";
                        string ctrlName4 = "BARCODE_CTL2";
                        this.SetFieldVisible(ctrlName3, false);
                        this.SetFieldVisible(ctrlName4, false);
                    }
                }
                else
                {

                    ctrlName = "BARCODE_CTL1";
                    this.SetFieldVisible(ctrlName, false);
                    ctrlName = "BARCODE_CTL2";
                    this.SetFieldVisible(ctrlName, false);
                    ctrlName = "QRCODE_CTL1";
                    this.SetFieldVisible(ctrlName, false);
                    ctrlName = "QRCODE_CTL2";
                    this.SetFieldVisible(ctrlName, false);

                }
            }
            else
            {

                ctrlName = "BARCODE_CTL1";
                this.SetFieldVisible(ctrlName, false);
                ctrlName = "BARCODE_CTL2";
                this.SetFieldVisible(ctrlName, false);
                ctrlName = "QRCODE_CTL1";
                this.SetFieldVisible(ctrlName, false);
                ctrlName = "QRCODE_CTL2";
                this.SetFieldVisible(ctrlName, false);

            }

            r_framework.Utility.LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 数値データを書式設定します
        /// </summary>
        /// <param name="strdata">ソースデータ</param>
        /// <param name="mode">フォーマットモード 0:金額 1:重量 2:数量 3:単位</param>
        private string DataFormatChange(string strdata, int mode)
        {
            string str = strdata;

            if (!string.IsNullOrEmpty(str))
            {
                decimal dtmp = decimal.Parse(str);
                switch (mode)
                {
                    case 0: // 金額
                        str = dtmp.ToString("#,##0");
                        break;
                    case 1: // 重量
                        str = dtmp.ToString(this.mSysInfo.SYS_JYURYOU_FORMAT);
                        break;
                    case 2: // 数量
                        str = dtmp.ToString(this.mSysInfo.SYS_SUURYOU_FORMAT);
                        break;
                    case 3: // 単価
                        str = dtmp.ToString(this.mSysInfo.SYS_TANKA_FORMAT);
                        break;
                }
            }
            return str;
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
