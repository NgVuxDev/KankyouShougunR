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

namespace Shougun.Core.SalesPayment.UriageSuiiChouhyou
{
    /// <summary>
    /// 入金集計表帳票作成クラス
    /// </summary>
    internal class UriageSuiihyouReportClass
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
        internal UriageSuiihyouReportClass()
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
        internal void CreateReport(DataTable dt, UriageSuiihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            // ヘッダー情報のDataTableを作成
            var headerDataTable = this.CreateHeaderDt(dto);

            // レポート定義ファイルをLoad
            var chouhyouDataTable = this.CreateChouhyouDt(dt, dto);

            string layOut = "LAYOUT1";
            if (dto.Select.Count > 2)
            {
                layOut = "LAYOUT2";
            }

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoR582 reportInfo = new ReportInfoR582(headerDataTable, chouhyouDataTable);
            reportInfo.CreateR582(layOut);
            reportInfo.Title = "売上推移表(" + dto.Pattern.PATTERN_NAME + ")";
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
        private DataTable CreateHeaderDt(UriageSuiihyouDtoClass dto)
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
            retDt.Columns.Add("COLUMN_1");
            // 項目名2
            retDt.Columns.Add("COLUMN_2");
            // 項目名3
            retDt.Columns.Add("COLUMN_3");
            // 項目名4
            retDt.Columns.Add("COLUMN_4");

            for (int i = 0; i < 12; i++)
            {
                // 年月i
                retDt.Columns.Add("COL_" + (i + 1));
            }

            DataRow dr = retDt.NewRow();

            // Header項目の値
            // タイトル
            dr["TITLE"] = "売上推移表 (" + dto.Pattern.PATTERN_NAME + ")";
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
            for (int i = 0; i < 4; i++)
            {
                string colName = "COLUMN_" + (i + 1);
                dr[colName] = string.Empty;
                if (dto.Pattern.ColumnSelectList.Count > i)
                {
                    dr[colName] = dto.Pattern.ColumnSelectList[i].KOUMOKU_RONRI_NAME;
                }
            }

            // 年月
            int preYear = 0;
            for (int x = 0; x < 12; x++)
            {
                string colNameMonth = "COL_" + (x + 1);
                dr[colNameMonth] = string.Empty;
                if (dto.Pivot.Count > x)
                {
                    DateTime dt = DateTime.Parse(dto.Pivot[x].ToString() + "/01");
                    if (preYear != dt.Year)
                    {
                        dr[colNameMonth] = dt.Year + "年" + dt.Month + "月";
                        preYear = dt.Year;
                    }
                    else
                    {
                        dr[colNameMonth] = dt.Month + "月";
                    }
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
        public DataTable CreateChouhyouDt(DataTable dt, UriageSuiihyouDtoClass dto)
        {
            DataTable retDt = new DataTable();

            retDt.Columns.Add("CD_1");
            retDt.Columns.Add("NAME_1");
            retDt.Columns.Add("CD_2");
            retDt.Columns.Add("NAME_2");
            retDt.Columns.Add("CD_3");
            retDt.Columns.Add("NAME_3");
            retDt.Columns.Add("CD_4");
            retDt.Columns.Add("NAME_4");

            for (int i = 0; i < 12; i++)
            {
                retDt.Columns.Add("VAL_" + (i + 1));
            }

            retDt.Columns.Add("VAL_TOTAL");
            retDt.Columns.Add("VAL_AVG");

            foreach (DataRow row in dt.Rows)
            {
                DataRow dr = retDt.NewRow();

                // 集計項目
                for (int x = 0; x < 4; x++)
                {
                    string col = "CD_" + (x + 1);
                    string name = "NAME_" + (x + 1);

                    dr[col] = string.Empty;
                    dr[name] = string.Empty;

                    if (dto.Select.Count > x)
                    {
                        if (dto.Select[x].ToString() != "DAIKAN_KBN")
                        {
                            dr[col] = row[dto.Select[x].ToString()];
                            dr[name] = row[dto.Select[x].ToString().Replace("_CD", "_NAME")];
                        }
                        else
                        {
                            dr[col] = row[dto.Select[x].ToString() + "_CD"];
                            dr[name] = row[dto.Select[x].ToString() + "_NAME"];
                        }
                    }
                }

                // 月別の値
                for (int i = 0; i < 12; i++)
                {
                    string colName = "VAL_" + (i + 1);
                    dr[colName] = string.Empty;
                    if (dto.Pivot.Count > i)
                    {
                        dr[colName] = row[dto.Pivot[i].ToString()].ToString();
                    }
                }

                dr["VAL_TOTAL"] = row["SUM_KINGAKU"].ToString();
                dr["VAL_AVG"] = row["AVR"].ToString();

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
    }
}
