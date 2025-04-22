using System;
using System.Data;
using CommonChouhyouPopup.App;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Scale.KeiryouHoukoku.APP;
using Shougun.Core.Scale.KeiryouHoukoku.Const;
using Shougun.Core.Scale.KeiryouHoukoku.DTO;

namespace Shougun.Core.Scale.KeiryouHoukoku.Report
{
    /// <summary>
    /// 計量推移表作成クラス
    /// </summary>
    internal class ReportClsR677
    {
        /// <summary>
        ///
        /// </summary>
        private UIForm form;

        private string jyuryouFormat;

        private int GroupRow = 0;

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal ReportClsR677(UIForm targetForm)
        {
            LogUtility.DebugMethodStart();
            this.form = targetForm;
            M_SYS_INFO mSysInfo = new DBAccessor().GetSysInfo();
            this.jyuryouFormat = mSysInfo.SYS_JYURYOU_FORMAT.ToString();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport(DataTable dt, DTOCls dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            var chouhyouTable = this.EditChouhyouTable(dt, dto);

            this.AddRowToDataTable(chouhyouTable.Rows.Count, chouhyouTable);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouTable);
            reportInfo.Create(ConstCls.R677OutputFormFullPathName, ConstCls.LAYOUT_1, chouhyouTable);
            reportInfo.Title = ConstCls.KEIRYOU_SUIIHYOU_TITLE;

            // グループの表示制御
            this.UpdateReportFields(reportInfo, dto, dt);

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;
            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票に渡すデータを加工します
        /// </summary>
        /// <param name="dt">元となるデータ</param>
        /// <param name="dto">画面入力データ</param>
        /// <returns>帳票用に作成したデータ</returns>
        private DataTable EditChouhyouTable(DataTable dtin, DTOCls dto)
        {
            LogUtility.DebugMethodStart(dtin, dto);

            DataTable dt = new DataTable();

            // DBで取得できない項目のカラムを追加
            // キー
            dt.Columns.Add("GROUP_KEY");
            // 変換グループ
            dt.Columns.Add("G2H_KAHEN_VLB");
            // 変換CD
            dt.Columns.Add("PHY_KAHEN_CD_VLB");
            // 変換名
            dt.Columns.Add("PHY_KAHEN_NAME_VLB");
            for (int i = 0; i < 12; i++)
            {
                dt.Columns.Add(string.Format("PHY_MEISAI_{0}_VLB", i + 1));
                dt.Columns.Add(string.Format("PHY_MEISAI_{0}_VLB_FORMATED", i + 1));
            }
            // 合計
            dt.Columns.Add("PHY_TOTAL_FLB");
            dt.Columns.Add("PHY_TOTAL_FLB_FORMATED");
            // VISIBLE_GROUP
            dt.Columns.Add("FLG_VISIBLE_GROUP");
            dt.Columns.Add("FLG_VISIBLE_GROUP_FOOTER");

            decimal sumR = 0;
            DateTime dateTimeStartTmp;
            DateTime dateTimeEndTmp;
            DateTime dateTimeTmp;
            string tmp;
            string tmp_fd;
            string torihikisakiCd = null;
            string gyoushaCd = null;
            string genbaCd = null;
            string hinmeiCd = null;
            string shuruiCd = null;
            string bunruiCd = null;
            dateTimeStartTmp = Convert.ToDateTime(dto.DateFrom);
            dateTimeEndTmp = Convert.ToDateTime(dto.DateTo);
            GroupRow = 0;
            foreach (DataRow row in dtin.Rows)
            {
                if (((dto.GroupTani == 3 && gyoushaCd != row["GYOUSHA_CD"].ToString()) ||
                    (dto.GroupTani == 5 && bunruiCd != row["BUNRUI_CD"].ToString()) ||
                    (dto.GroupTani == 6 && shuruiCd != row["SHURUI_CD"].ToString())))
                {
                    GroupRow += 1;
                }

                if ((dto.GroupTani == 1 && torihikisakiCd == row["TORIHIKISAKI_CD"].ToString()) ||
                    (dto.GroupTani == 2 && gyoushaCd == row["GYOUSHA_CD"].ToString()) ||
                    (dto.GroupTani == 3 && gyoushaCd == row["GYOUSHA_CD"].ToString() && genbaCd == row["GENBA_CD"].ToString()) ||
                    (dto.GroupTani == 4 && hinmeiCd == row["HINMEI_CD"].ToString()) ||
                    (dto.GroupTani == 5 && bunruiCd == row["BUNRUI_CD"].ToString() && hinmeiCd == row["HINMEI_CD"].ToString()) ||
                    (dto.GroupTani == 6 && shuruiCd == row["SHURUI_CD"].ToString() && hinmeiCd == row["HINMEI_CD"].ToString()))
                {
                    DataRow dr = dt.Rows[dt.Rows.Count - 1];
                    if (!string.IsNullOrEmpty(row["NET_JYUURYOU"].ToString()))
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            dateTimeTmp = dateTimeStartTmp.AddMonths(i);

                            tmp = string.Format("PHY_MEISAI_{0}_VLB", i + 1);

                            if ((dateTimeTmp.Year * 100 + dateTimeTmp.Month) > (dateTimeEndTmp.Year * 100 + dateTimeEndTmp.Month))
                            {
                                break;
                            }

                            if ((dateTimeTmp.Year * 100 + dateTimeTmp.Month).ToString() == row["YM"].ToString())
                            {
                                dr[tmp] = Convert.ToDecimal(dr[tmp]) + Convert.ToDecimal(row["NET_JYUURYOU"]);
                                dr["PHY_TOTAL_FLB"] = Convert.ToDecimal(dr["PHY_TOTAL_FLB"]) + Convert.ToDecimal(row["NET_JYUURYOU"]);
                            }
                        }
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();

                    switch (dto.GroupTani)
                    {
                        case 1:
                            dr["GROUP_KEY"] = "";
                            dr["G2H_KAHEN_VLB"] = "";
                            dr["PHY_KAHEN_CD_VLB"] = row["TORIHIKISAKI_CD"].ToString();
                            dr["PHY_KAHEN_NAME_VLB"] = row["TORIHIKISAKI_NAME"].ToString();
                            break;
                        case 2:
                            dr["GROUP_KEY"] = "";
                            dr["G2H_KAHEN_VLB"] = "";
                            dr["PHY_KAHEN_CD_VLB"] = row["GYOUSHA_CD"].ToString();
                            dr["PHY_KAHEN_NAME_VLB"] = row["GYOUSHA_NAME"].ToString();
                            break;
                        case 3:
                            dr["GROUP_KEY"] = row["GYOUSHA_CD"].ToString();
                            dr["G2H_KAHEN_VLB"] = row["GYOUSHA_CD"].ToString() + " " + row["GYOUSHA_NAME"].ToString();
                            dr["PHY_KAHEN_CD_VLB"] = row["GENBA_CD"].ToString();
                            dr["PHY_KAHEN_NAME_VLB"] = row["GENBA_NAME"].ToString();
                            break;
                        case 4:
                            dr["GROUP_KEY"] = "";
                            dr["G2H_KAHEN_VLB"] = "";
                            dr["PHY_KAHEN_CD_VLB"] = row["HINMEI_CD"].ToString();
                            dr["PHY_KAHEN_NAME_VLB"] = row["HINMEI_NAME"].ToString();
                            break;
                        case 5:
                            dr["GROUP_KEY"] = row["BUNRUI_CD"].ToString();
                            dr["G2H_KAHEN_VLB"] = row["BUNRUI_CD"].ToString() + " " + row["BUNRUI_NAME"].ToString();
                            dr["PHY_KAHEN_CD_VLB"] = row["HINMEI_CD"].ToString();
                            dr["PHY_KAHEN_NAME_VLB"] = row["HINMEI_NAME"].ToString();
                            break;
                        case 6:
                            dr["GROUP_KEY"] = row["SHURUI_CD"].ToString();
                            dr["G2H_KAHEN_VLB"] = row["SHURUI_CD"].ToString() + " " + row["SHURUI_NAME"].ToString();
                            dr["PHY_KAHEN_CD_VLB"] = row["HINMEI_CD"].ToString();
                            dr["PHY_KAHEN_NAME_VLB"] = row["HINMEI_NAME"].ToString();
                            break;
                    }

                    sumR = 0;
                    for (int i = 0; i < 12; i++)
                    {
                        dateTimeTmp = dateTimeStartTmp.AddMonths(i);

                        tmp = string.Format("PHY_MEISAI_{0}_VLB", i + 1);

                        if ((dateTimeTmp.Year * 100 + dateTimeTmp.Month) > (dateTimeEndTmp.Year * 100 + dateTimeEndTmp.Month))
                        {
                            break;
                        }

                        dr[tmp] = 0;
                        if ((dateTimeTmp.Year * 100 + dateTimeTmp.Month).ToString() == row["YM"].ToString())
                        {
                            if (!string.IsNullOrEmpty(row["NET_JYUURYOU"].ToString()))
                            {
                                dr[tmp] = row["NET_JYUURYOU"].ToString();
                                sumR += Convert.ToDecimal(row["NET_JYUURYOU"]);
                            }
                        }
                    }
                    dr["PHY_TOTAL_FLB"] = sumR;
                    dt.Rows.Add(dr);
                }
                torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();
                gyoushaCd = row["GYOUSHA_CD"].ToString();
                genbaCd = row["GENBA_CD"].ToString();
                hinmeiCd = row["HINMEI_CD"].ToString();
                shuruiCd = row["SHURUI_CD"].ToString();
                bunruiCd = row["BUNRUI_CD"].ToString();
            }

            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < 12; i++)
                {
                    tmp = string.Format("PHY_MEISAI_{0}_VLB", i + 1);
                    tmp_fd = string.Format("PHY_MEISAI_{0}_VLB_FORMATED", i + 1);
                    if (!string.IsNullOrEmpty(row[tmp].ToString()))
                    {
                        row[tmp_fd] = Convert.ToDecimal(row[tmp]).ToString(jyuryouFormat);
                    }
                }
                row["PHY_TOTAL_FLB_FORMATED"] = Convert.ToDecimal(row["PHY_TOTAL_FLB"]).ToString(jyuryouFormat);
            }

            LogUtility.DebugMethodEnd(dt);

            return dt;
        }

        /// <summary> フィールド状態の更新処理を実行する </summary>
        private void UpdateReportFields(ReportInfoBase reportInfo, DTOCls dto, DataTable dt)
        {
            try
            {
                DateTime dateTimeStartTmp;
                DateTime dateTimeEndTmp;
                DateTime dateTimeTmp;
                string tmp;

                #region - 全てのタイトルカラムテキスト初期化 -

                // 集計項目領域初期化
                reportInfo.SetFieldName("PHY_KAHEN_CD_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_NAME_VLB", string.Empty);

                // 帳票出力項目(明細伝票月)領域初期化
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_1_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_2_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_3_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_4_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_5_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_6_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_7_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_8_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_9_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_10_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_11_VLB", string.Empty);
                reportInfo.SetFieldName("PHY_KAHEN_MONTH_12_VLB", string.Empty);

                // 出力日付の書式設定
                reportInfo.SetFieldName("FH_PRINT_DATE_VLB", ReportUtil.GetSysDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行");

                // 自社略称
                reportInfo.SetFieldName("FH_CORP_NAME_RYAKU_VLB", this.form.CommInfo.CorpInfo.CORP_RYAKU_NAME);

                // 拠点名称
                reportInfo.SetFieldName("FH_KYOTEN_NAME_RYAKU_VLB", dto.KyotenName);

                #endregion - 全てのタイトルカラムテキスト初期化 -


                #region - 集計項目用タイトルカラムテキスト -
                switch (dto.GroupTani)
                {
                    case 1:
                        // 帳票タイトル
                        reportInfo.SetFieldName("FH_TITLE_VLB", ConstCls.KEIRYOU_SUIIHYOU_TITLE + ConstCls.SORT_TORIHIKISAKI_SUB_TITLE_SUIIHYOU);
                        reportInfo.SetGroupVisible("GROUP2", false, false);
                        reportInfo.SetFieldName("PHN_GRUOP_TOTAL_FLB", string.Empty);
                        reportInfo.SetFieldName("PHY_KAHEN_CD_VLB", "取引先CD");
                        reportInfo.SetFieldName("PHY_KAHEN_NAME_VLB", "取引先名");
                        break;
                    case 2:
                        reportInfo.SetFieldName("FH_TITLE_VLB", ConstCls.KEIRYOU_SUIIHYOU_TITLE + ConstCls.SORT_GYOUSHA_SUB_TITLE_SUIIHYOU);
                        reportInfo.SetGroupVisible("GROUP2", false, false);
                        reportInfo.SetFieldName("PHN_GRUOP_TOTAL_FLB", string.Empty);
                        reportInfo.SetFieldName("PHY_KAHEN_CD_VLB", "業者CD");
                        reportInfo.SetFieldName("PHY_KAHEN_NAME_VLB", "業者名");
                        break;
                    case 3:
                        reportInfo.SetFieldName("FH_TITLE_VLB", ConstCls.KEIRYOU_SUIIHYOU_TITLE + ConstCls.SORT_GENBA_SUB_TITLE_SUIIHYOU);
                        reportInfo.SetGroupVisible("GROUP2", true, true);
                        reportInfo.SetFieldName("PHN_GRUOP_TOTAL_FLB", "業者計");
                        reportInfo.SetFieldName("PHY_KAHEN_CD_VLB", "現場CD");
                        reportInfo.SetFieldName("PHY_KAHEN_NAME_VLB", "現場名");
                        break;
                    case 4:
                        reportInfo.SetFieldName("FH_TITLE_VLB", ConstCls.KEIRYOU_SUIIHYOU_TITLE + ConstCls.SORT_HINMEI_SUB_TITLE_SUIIHYOU);
                        reportInfo.SetGroupVisible("GROUP2", false, false);
                        reportInfo.SetFieldName("PHN_GRUOP_TOTAL_FLB", string.Empty);
                        reportInfo.SetFieldName("PHY_KAHEN_CD_VLB", "品名CD");
                        reportInfo.SetFieldName("PHY_KAHEN_NAME_VLB", "品名");
                        break;
                    case 5:
                        reportInfo.SetFieldName("FH_TITLE_VLB", ConstCls.KEIRYOU_SUIIHYOU_TITLE + ConstCls.SORT_BUNRUI_SUB_TITLE_SUIIHYOU);
                        reportInfo.SetGroupVisible("GROUP2", true, true);
                        reportInfo.SetFieldName("PHN_GRUOP_TOTAL_FLB", "分類計");
                        reportInfo.SetFieldName("PHY_KAHEN_CD_VLB", "品名CD");
                        reportInfo.SetFieldName("PHY_KAHEN_NAME_VLB", "品名");
                        break;
                    case 6:
                        reportInfo.SetFieldName("FH_TITLE_VLB", ConstCls.KEIRYOU_SUIIHYOU_TITLE + ConstCls.SORT_SHURUI_SUB_TITLE_SUIIHYOU);
                        reportInfo.SetGroupVisible("GROUP2", true, true);
                        reportInfo.SetFieldName("PHN_GRUOP_TOTAL_FLB", "種類計");
                        reportInfo.SetFieldName("PHY_KAHEN_CD_VLB", "品名CD");
                        reportInfo.SetFieldName("PHY_KAHEN_NAME_VLB", "品名");
                        break;
                }
                #endregion - 集計項目用タイトルカラムテキスト -

                #region - 集計月用タイトルカラムテキスト -

                dateTimeStartTmp = Convert.ToDateTime(dto.DateFrom);
                dateTimeEndTmp = Convert.ToDateTime(dto.DateTo);

                int year = dateTimeStartTmp.Year;
                string tmpM;
                for (int i = 0; i < 12; i++)
                {
                    dateTimeTmp = dateTimeStartTmp.AddMonths(i);

                    tmp = string.Format("PHY_KAHEN_MONTH_{0}_VLB", i + 1);
                    tmpM = string.Format("DTL_MEISAI_{0}_CTL", i + 1);
                    if ((dateTimeTmp.Year * 100 + dateTimeTmp.Month) > (dateTimeEndTmp.Year * 100 + dateTimeEndTmp.Month))
                    {
                        reportInfo.SetFieldName(tmp, string.Empty);
                    }
                    else
                    {
                        reportInfo.SetFieldName(tmp, dateTimeTmp.ToString("M月"));
                        tmp = string.Format("G2F_SURYOU_TOTAL_{0}_CTL", i + 1);
                        reportInfo.SetFieldFormat(tmp, jyuryouFormat);
                        reportInfo.SetFieldName(tmp, "IIf(Sum(" + tmpM + ") = \"\",0,Sum(" + tmpM + ")) ");
                        tmp = string.Format("G1F_SURYOU_TOTAL_{0}_CTL", i + 1);
                        reportInfo.SetFieldFormat(tmp, jyuryouFormat);
                        reportInfo.SetFieldName(tmp, "IIf(Sum(" + tmpM + ") = \"\",0,Sum(" + tmpM + ")) ");
                    }
                }
                reportInfo.SetFieldFormat("G2F_SURYOU_TOTAL_13_CTL", jyuryouFormat);
                reportInfo.SetFieldFormat("G1F_SURYOU_TOTAL_13_CTL", jyuryouFormat);

                #endregion - 集計月用タイトルカラムテキスト -
            }
            catch (Exception e)
            {
                LogUtility.Error(e.Message, e);
            }
        }

        private void AddRowToDataTable(int rowCount, DataTable inputDataTable)
        {

            rowCount = rowCount + this.GroupRow * 2 + 1;

            int numOfRowToAdd = 0;

            decimal t = Math.Ceiling((decimal)rowCount / 15);

            numOfRowToAdd = (15 * (int)t) - rowCount;

            foreach (DataRow row in inputDataTable.Rows)
            {
                row["FLG_VISIBLE_GROUP"] = "False";
                row["FLG_VISIBLE_GROUP_FOOTER"] = "False";
            }

            int lastRow = inputDataTable.Rows.Count - 1;
            DataRow dr;
            for (int i = 0; i < numOfRowToAdd; i++)
            {
                dr = inputDataTable.NewRow();
                if (lastRow >= 0)
                {
                    if (i == 0)
                    {
                        inputDataTable.Rows[lastRow]["FLG_VISIBLE_GROUP"] = "True";
                    }
                    dr["GROUP_KEY"] = "GROUP_VISIBLE_OFF";
                    dr["FLG_VISIBLE_GROUP"] = "True";
                    dr["FLG_VISIBLE_GROUP_FOOTER"] = "True";
                }
                inputDataTable.Rows.Add(dr);
            }
        }
    }
}