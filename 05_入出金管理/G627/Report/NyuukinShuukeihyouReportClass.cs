using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using CommonChouhyouPopup.App;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.NyuukinShuukeiChouhyou
{
    /// <summary>
    /// 入金集計表帳票作成クラス
    /// </summary>
    internal class NyuukinShuukeihyouReportClass
    {
        /// <summary>
        /// システム設定エンティティ
        /// </summary>
        private M_SYS_INFO mSysInfo;

        /// <summary>
        /// 会社情報エンティティ
        /// </summary>
        private M_CORP_INFO mCorpInfo;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal NyuukinShuukeihyouReportClass()
        {
            LogUtility.DebugMethodStart();

            var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

            var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport(DataTable dt, NyuukinShuukeihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            // ヘッダー情報のDataTableを作成
            var headerDataTable = this.CreateHeaderDt(dto);

            // レポート定義ファイルをLoad
            var chouhyouDataTable = this.CreateChouhyouDt(dt, dto);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoR628 reportInfo = new ReportInfoR628(headerDataTable, chouhyouDataTable);
            reportInfo.CreateR628();
            reportInfo.Title = "入金集計表(" + dto.Pattern.PATTERN_NAME + ")";
            //reportInfo.ReportID = "Rxxx";

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;
            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ヘッダー用DataTable作成
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable CreateHeaderDt(NyuukinShuukeihyouDtoClass dto)
        {
            DataTable retDt = new DataTable();

            // Header項目
            // タイトル
            retDt.Columns.Add("TITLE");
            // 自社
            retDt.Columns.Add("JISHA");
            // 拠点
            retDt.Columns.Add("KYOTEN");
            // 発行日時
            retDt.Columns.Add("HAKKOU_DATE");
            // 条件1
            retDt.Columns.Add("JOUKEN_1");
            // 条件2
            retDt.Columns.Add("JOUKEN_2");

            // PageHeader項目
            // 項目名1
            retDt.Columns.Add("COL_1");
            // 項目名2
            retDt.Columns.Add("COL_2");
            // 項目名3
            retDt.Columns.Add("COL_3");
            // 項目名4
            retDt.Columns.Add("COL_4");
            // 項目名5
            retDt.Columns.Add("COL_5");

            DataRow dr = retDt.NewRow();

            // Header項目の値
            // タイトル
            dr["TITLE"] = "入金集計表" + " (" + dto.Pattern.PATTERN_NAME + ")";
            // 自社
            dr["JISHA"] = this.mCorpInfo.CORP_RYAKU_NAME;
            // 拠点
            dr["KYOTEN"] = dto.KyotenName;
            // 発行日付
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //dr["HAKKOU_DATE"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";
            dr["HAKKOU_DATE"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            // 条件1
            dr["JOUKEN_1"] = dto.Jyouken1;
            // 条件2
            dr["JOUKEN_2"] = dto.Jyouken2;

            // PageHeader項目の値
            // 項目1～4
            for (int i = 0; i < 5; i++)
            {
                string colName = "COL_" + (i + 1);
                if (dto.Pattern.ColumnSelectList.Count <= i)
                {
                    dr[colName] = string.Empty;
                }
                else
                {
                    dr[colName] = dto.Pattern.ColumnSelectList[i].KOUMOKU_RONRI_NAME;
                }
            }

            retDt.Rows.Add(dr);

            return retDt;
        }

        /// <summary>
        /// 帳票用DataTable作成
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public DataTable CreateChouhyouDt(DataTable dt, NyuukinShuukeihyouDtoClass dto)
        {
            DataTable retDt = new DataTable();

            retDt.Columns.Add("VAL_1");
            retDt.Columns.Add("VAL_2");
            retDt.Columns.Add("VAL_3");
            retDt.Columns.Add("VAL_4");
            retDt.Columns.Add("VAL_5");
            retDt.Columns.Add("NYUUKIN_KBN");
            retDt.Columns.Add("KINGAKU");

            retDt.Columns.Add("GROUP5_NAME");
            retDt.Columns.Add("GROUP5_KEY");
            retDt.Columns.Add("GROUP4_NAME");
            retDt.Columns.Add("GROUP4_KEY"); ;
            retDt.Columns.Add("GROUP3_NAME");
            retDt.Columns.Add("GROUP3_KEY");
            retDt.Columns.Add("GROUP2_NAME");
            retDt.Columns.Add("GROUP2_KEY");
            retDt.Columns.Add("GROUP1_NAME");
            retDt.Columns.Add("GROUP1_KEY");

            // ソート
            //DataView dv = new DataView(dt);
            //if (dto.ShuukeiIsChecked.Count > 0)
            //{
            //    dv.Sort = string.Join(",", dto.ShuukeiIsChecked.Values.ToArray());
            //}

            Dictionary<string, string> valDic = new Dictionary<string, string>();
            foreach (DataRow row in dt.Rows)
            {
                DataRow dr = retDt.NewRow();

                for (int i = 0; i < 5; i++)
                {
                    string colName = "VAL_" + (i + 1);
                    if (dto.Pattern.ColumnSelectList.Count <= i)
                    {
                        dr[colName] = string.Empty;
                    }
                    else
                    {
                        string butsuriName = dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME;
                        int butsuriNameLength = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Length;
                        int butsuriNameGetByteCount = Encoding.Default.GetByteCount(row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString());
                        switch (butsuriName)
                        {
                            case "TORIHIKISAKI_CD":
                                //if (butsuriNameLength != butsuriNameGetByteCount)
                                //{
                                //    if (butsuriNameLength > 18)
                                //    {
                                //        dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(0, 18)
                                //                      + "　　　 "
                                //                      + row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(18, butsuriNameLength - 18);
                                //    }
                                //}
                                //else
                                //{
                                //    if (butsuriNameLength > 27)
                                //    {
                                //        dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(0, 27)
                                //                      + "　　　 "
                                //                      + row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(27, butsuriNameLength - 27);
                                //    }

                                //}
                                //break;
                            case "NYUUKINSAKI_CD":
                                //  if (butsuriNameLength != butsuriNameGetByteCount)
                                //{
                                //    if (butsuriNameLength > 18)
                                //    {
                                //        dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(0, 18)
                                //                      + "　　　 "
                                //                      + row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(18, butsuriNameLength - 18);
                                //    }
                                //}
                                //else
                                //{
                                //    if (butsuriNameLength > 27)
                                //    {
                                //        dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(0, 27)
                                //                      + "　　　 "
                                //                      + row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(27, butsuriNameLength - 27);
                                //    }

                                //}
                                int index = getSubIndex(row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString(), 27);
                                if (index > 0)
                                {
                                    dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(0, index)
                                                   + " 　　　 "
                                                   + row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString().Substring(index);
                                }
                                else
                                {
                                    dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString();
                                }
                                break;
                            case "BANK_CD":
                                dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString();
                                break;
                            case "BANK_SHITEN_CD":
                                dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString();
                                break;
                            case "NYUUSHUKKIN_KBN_CD":
                                dr[colName] = row[dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME].ToString();
                                break;
                        }
                        if (!valDic.ContainsKey(colName))
                        {
                            valDic.Add(colName, dto.Pattern.ColumnSelectDetailList[i].BUTSURI_NAME);
                        }
                    }
                }

                dr["KINGAKU"] = row["KINGAKU"].ToString();

                int y = 5;
                for (int x = 0; x < 5; x++)
                {
                    string grColName = "GROUP" + y + "_NAME";
                    string grColKey = "GROUP" + y + "_KEY";
                    if (dto.ShuukeiIsChecked.Count <= x)
                    {
                        dr[grColName] = string.Empty;
                        dr[grColKey] = string.Empty;
                    }
                    else
                    {
                        dr[grColName] = row[dto.ShuukeiIsChecked.Values.ToList()[dto.ShuukeiIsChecked.Count() - (x + 1)].ToString() + "_NAME"].ToString();
                        dr[grColKey] = row[dto.ShuukeiIsChecked.Values.ToList()[dto.ShuukeiIsChecked.Count() - (x + 1)].ToString() + "_RNK"].ToString();
                    }
                    y--;
                }

                retDt.Rows.Add(dr);
            }

            return retDt;
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

        // 20160127 chenzz 帳票に 文字列中にスペースがあれば、改行したこと対応　start
        private static int getSubIndex(string str, int endIndex)
        {
            if (str == null || str.Length == 0 || endIndex < 0)
            {
                return 0;
            }

            int index = 0;
            int bytesCount = Encoding.Default.GetByteCount(str);
            if (bytesCount > endIndex)
            {
                int readyLength = 0;
                int byteLength = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    byteLength = Encoding.Default.GetByteCount(new char[] { str[i] });
                    readyLength += byteLength;
                    if (readyLength == endIndex)
                    {
                        index = i + 1;
                        break;
                    }
                    else if (readyLength == endIndex)
                    {
                        index = i;
                        break;
                    }
                }
            }
            else if (bytesCount == endIndex)
            {
                index = -1;
            }
            return index;
        }
        // 20160127 chenzz 帳票に 文字列中にスペースがあれば、改行したこと対応　end
    }
}
