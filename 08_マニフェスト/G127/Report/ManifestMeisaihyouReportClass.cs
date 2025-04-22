using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Utility;
using System.Data;
using r_framework.Dao;
using System.Reflection;
using System.Xml;
using CommonChouhyouPopup.App;

namespace Shougun.Core.PaperManifest.Manifestmeisaihyo
{
    /// <summary>
    /// マニフェスト明細表帳票作成クラス
    /// </summary>
    internal class ManifestMeisaihyouReportClass
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
        /// 帳票テンプレートのパス
        /// </summary>
        private static readonly string OutputFormFullPathName = "./Template/R390-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        /// <remarks>
        /// 一次二次区分：一次　選択用レイアウト
        /// (二次交付番号・減容後数量カラム表示有)
        /// </remarks>
        private static readonly string LAYOUT1 = "LAYOUT1";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        /// <remarks>
        /// 一次二次区分：二次　選択用レイアウト
        /// (二次交付番号・減容後数量カラム表示無)
        /// </remarks>
        private static readonly string LAYOUT2 = "LAYOUT2";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal ManifestMeisaihyouReportClass()
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
        internal void CreateReport(DataTable dt, ManifestMeisaihyouDto dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            var chouhyouDataTable = this.EditChouhyouDataTable(dt.Copy(), dto);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouDataTable);

            // 検索条件の「一次二次区分」の選択値によって、レイアウト変更
            string layout = (dto.IchijiKbn == 1) ? LAYOUT1 : LAYOUT2;

            reportInfo.Create(OutputFormFullPathName, layout, chouhyouDataTable);

            // グループの表示制御
            reportInfo.SetGroupVisible("DATE", false, dto.IsGroupDate);
            reportInfo.SetGroupVisible("HST_GYOUSHA", false, dto.IsGroupHaishutsuJigyousha);
            reportInfo.SetGroupVisible("HST_GENBA", false, dto.IsGroupHaishutsuJigyoujou);
            reportInfo.SetGroupVisible("UPN_GYOUSHA", false, dto.IsGroupUnpanJutakusha1 || dto.IsGroupUnpanJutakusha2);
            reportInfo.SetGroupVisible("SBN_GENBA", false, dto.IsGroupShobunJigyoujou);
            reportInfo.SetGroupVisible("LAST_SBN_GENBA", false, dto.IsGroupLastShobunGenba);
            reportInfo.SetGroupVisible("HOUKOKUSHO_BUNRUI", false, dto.IsGroupHoukokushoBunrui);
            reportInfo.SetGroupVisible("SBN_HOUHOU", false, dto.IsGroupShobunHouhou);

            // 日付グループのブレークキーと見出し設定
            if (ConstClass.SORT_KOFU_DATE == dto.Sort)
            {
                reportInfo.SetGroupGroupBy("DATE", "KOUFU_DATE");
            }
            else if (ConstClass.SORT_UPN_END_DATE == dto.Sort)
            {
                reportInfo.SetGroupGroupBy("DATE", "UPN_END_DATE");
            }
            else if (ConstClass.SORT_SBN_END_DATE == dto.Sort)
            {
                reportInfo.SetGroupGroupBy("DATE", "SBN_END_DATE");
            }
            else if (ConstClass.SORT_LAST_SBN_END_DATE == dto.Sort)
            {
                reportInfo.SetGroupGroupBy("DATE", "LAST_SBN_END_DATE");
            }

            // 運搬受託者グループのブレークキー設定
            if (dto.IsGroupUnpanJutakusha1)
            {
                reportInfo.SetGroupGroupBy("UPN_GYOUSHA", "UPN_GYOUSHA_CD1");
            }
            else
            {
                reportInfo.SetGroupGroupBy("UPN_GYOUSHA", "UPN_GYOUSHA_CD2");
            }

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;

            //レポートタイトルの設定
            if (dto.Sort == ConstClass.SORT_KOFU_DATE)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_KOFU_DATE_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_UPN_END_DATE)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_UPN_END_DATE_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_SBN_END_DATE)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_SBN_END_DATE_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_LAST_SBN_END_DATE)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_LAST_SBN_END_DATE_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_HST_GYOUSHA)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_HST_GYOUSHA_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_UPN_GYOUSHA_1)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_UPN_GYOUSHA_1_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_UPN_GYOUSHA_2)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_UPN_GYOUSHA_2_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_SBN_GYOUSHA)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_SBN_GYOUSHA_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_LAST_SBN_GYOUSHA)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_LAST_SBN_GYOUSHA_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_HOUKOKUSHO_BUNRUI)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_HOUKOKUSHO_BUNRUI_SUB_TITLE + "順）";
            }
            else if (dto.Sort == ConstClass.SORT_SBN_HOUHOU)
            {
                reportPopup.ReportCaption = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_SBN_HOUHOU_SUB_TITLE + "順）";
            }

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
        private DataTable EditChouhyouDataTable(DataTable dt, ManifestMeisaihyouDto dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            DataTable ret = dt;

            bool isIchijiKbn = false;
            if (dto != null && dto.IchijiKbn == 1)
            {
                isIchijiKbn = true;
            }

            // DBで取得できない項目のカラムを追加

            // 帳票タイトル
            dt.Columns.Add("FH_TITLE_VLB");
            // 自社名称
            dt.Columns.Add("FH_CORP_NAME_RYAKU_VLB");
            // 拠点名称
            dt.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 出力日時
            dt.Columns.Add("FH_PRINT_DATE_VLB");
            // 日付
            dt.Columns.Add("FH_DATE_VLB");

            // 区分
            dt.Columns.Add("D_FORMATTED_HAIKI_KBN");
            // 交付年月日
            dt.Columns.Add("D_FORMATTED_KOUFU_DATE");
            // 数量
            dt.Columns.Add("D_FORMATTED_HAIKI_SUU");
            // 換算後数量
            dt.Columns.Add("D_FORMATTED_KANSAN_SUU");

            // 一次区分選択時のみ設定
            if (isIchijiKbn)
            {
                // 減容後数量
                dt.Columns.Add("D_FORMATTED_GENNYOU_SUU");
            }

            // 運搬終了年月日
            dt.Columns.Add("D_FORMATTED_UPN_END_DATE");
            // 処分終了年月日
            dt.Columns.Add("D_FORMATTED_SBN_END_DATE");
            // 最終処分終了年月日
            dt.Columns.Add("D_FORMATTED_LAST_SBN_END_DATE");

            // 排出事業場ブレークキー
            dt.Columns.Add("HST_JIGYOUJOU_KEY");

            // 処分事業場ブレークキー
            dt.Columns.Add("SBN_JIGYOUJOU_KEY");

            // 処分事業場ブレークキー
            dt.Columns.Add("LAST_SBN_JIGYOUJOU_KEY");

            // 交付年月日計
            dt.Columns.Add("GF_KOFU_DATE_HAIKI_SUU");
            dt.Columns.Add("GF_KOFU_DATE_MAISUU");

            // 運搬終了日計
            dt.Columns.Add("GF_UPN_END_DATE_HAIKI_SUU");
            dt.Columns.Add("GF_UPN_END_DATE_MAISUU");

            // 処分終了日計
            dt.Columns.Add("GF_SBN_END_DATE_HAIKI_SUU");
            dt.Columns.Add("GF_SBN_END_DATE_MAISUU");

            // 最終処分終了日計
            dt.Columns.Add("GF_LAST_SBN_END_DATE_HAIKI_SUU");
            dt.Columns.Add("GF_LAST_SBN_END_DATE_MAISUU");

            // 日計
            dt.Columns.Add("GF_DATE_HAIKI_SUU_VLB");
            dt.Columns.Add("GF_DATE_VLB");
            dt.Columns.Add("GF_DATE_HAIKI_SUU");
            dt.Columns.Add("GF_DATE_MAISUU");

            // 排出事業者計
            dt.Columns.Add("GF_HST_GYOUSHA_HAIKI_SUU");
            dt.Columns.Add("GF_HST_GYOUSHA_MAISUU");

            // 排出事業場計
            dt.Columns.Add("GF_HST_GENBA_HAIKI_SUU");
            dt.Columns.Add("GF_HST_GENBA_MAISUU");

            // 運搬受託者計
            dt.Columns.Add("GF_UPN_GYOUSHA_HAIKI_SUU");
            dt.Columns.Add("GF_UPN_GYOUSHA_MAISUU");
            dt.Columns.Add("GF_UPN_JYUTAKUSHA_NAME");

            dt.Columns.Add("GF_UPN_GYOUSHA_HAIKI_SUU_VLB");

            // 処分事業場計
            dt.Columns.Add("GF_SBN_GENBA_HAIKI_SUU");
            dt.Columns.Add("GF_SBN_GENBA_MAISUU");

            // 最終処分場計
            dt.Columns.Add("GF_LAST_SBN_GENBA_HAIKI_SUU");
            dt.Columns.Add("GF_LAST_SBN_GENBA_MAISUU");

            // 報告書分類計
            dt.Columns.Add("GF_HOUKOKUSHO_BUNRUI_HAIKI_SUU");
            dt.Columns.Add("GF_HOUKOKUSHO_BUNRUI_MAISUU");

            // 処分方法計
            dt.Columns.Add("GF_SBN_HOUHOU_HAIKI_SUU");
            dt.Columns.Add("GF_SBN_HOUHOU_MAISUU");

            // 総計
            dt.Columns.Add("GF_ALL_HAIKI_SUU");
            dt.Columns.Add("GF_ALL_MAISUU");

            // カラムを書き込み可にする
            dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

            var id = String.Empty;
            var kofuDate = ret.Rows[0]["KOUFU_DATE"].ToString();
            var upnEndDate = ret.Rows[0]["UPN_END_DATE"].ToString();
            var sbnEndDate = ret.Rows[0]["SBN_END_DATE"].ToString();
            var lastSbnEndDate = ret.Rows[0]["LAST_SBN_END_DATE"].ToString();
            var hstGyoushaCd = ret.Rows[0]["HST_GYOUSHA_CD"].ToString();
            var hstGenbaCd = ret.Rows[0]["HST_GENBA_CD"].ToString();
            var upnGyoushaCd = ret.Rows[0]["UPN_GYOUSHA_CD1"].ToString();
            if (dto.IsGroupUnpanJutakusha2)
            {
                upnGyoushaCd = ret.Rows[0]["UPN_GYOUSHA_CD2"].ToString();
            }
            var sbnGyoushaCd = ret.Rows[0]["SBN_GYOUSHA_CD"].ToString();
            var sbnGenbaCd = ret.Rows[0]["UPN_SAKI_GENBA_CD"].ToString();
            var lastSbnGyoushaCd = ret.Rows[0]["LAST_SBN_GYOUSHA_CD"].ToString();
            var lastSbnGenbaCd = ret.Rows[0]["LAST_SBN_GENBA_CD"].ToString();
            var houkokushoBunruiCd = ret.Rows[0]["HOUKOKUSHO_BUNRUI_CD"].ToString();
            var sbnHouhouCd = ret.Rows[0]["SBN_HOUHOU_CD"].ToString();

            decimal kofuDateHaikiSuu = 0;
            decimal upnEndDateHaikiSuu = 0;
            decimal sbnEndDateHaikiSuu = 0;
            decimal lastSbnEndDateHaikiSuu = 0;
            decimal hstGyoushaHaikiSuu = 0;
            decimal hstGenbaHaikiSuu = 0;
            decimal upnGyoushaHaikiSuu = 0;
            decimal sbnGenbaHaikiSuu = 0;
            decimal lastSbnGyoushaHaikiSuu = 0;
            decimal houkokushoBunruiHaikiSuu = 0;
            decimal sbnHouhouHaikiSuu = 0;
            decimal allHaikiSuu = 0;

            decimal kofuDateMaisuu = 0;
            decimal upnEndDateMaisuu = 0;
            decimal sbnEndDateMaisuu = 0;
            decimal lastSbnEndDateMaisuu = 0;
            decimal hstGyoushaMaisuu = 0;
            decimal hstGenbaMaisuu = 0;
            decimal upnGyoushaMaisuu = 0;
            decimal sbnGenbaMaisuu = 0;
            decimal lastSbnGenbaMaisuu = 0;
            decimal sbnHouhouMaisuu = 0;
            decimal houkokushoBunruiMaisuu = 0;

            foreach (DataRow row in ret.Rows)
            {
                var suuryoFormat = this.mSysInfo.MANIFEST_SUURYO_FORMAT;
                if ("電子" == row["HAIKI_KBN"].ToString())
                {
                    suuryoFormat = "#,##0.000";
                }

                row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE;
                if (dto.Sort == ConstClass.SORT_KOFU_DATE)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_KOFU_DATE_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_UPN_END_DATE)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_UPN_END_DATE_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_SBN_END_DATE)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_SBN_END_DATE_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_LAST_SBN_END_DATE)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_LAST_SBN_END_DATE_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_HST_GYOUSHA)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_HST_GYOUSHA_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_UPN_GYOUSHA_1)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_UPN_GYOUSHA_1_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_UPN_GYOUSHA_2)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_UPN_GYOUSHA_2_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_SBN_GYOUSHA)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_SBN_GYOUSHA_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_LAST_SBN_GYOUSHA)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_LAST_SBN_GYOUSHA_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_HOUKOKUSHO_BUNRUI)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_HOUKOKUSHO_BUNRUI_SUB_TITLE + "順）";
                }
                else if (dto.Sort == ConstClass.SORT_SBN_HOUHOU)
                {
                    row["FH_TITLE_VLB"] = ConstClass.MANIFEST_MEISAIHYOU_TITLE + "（" + ConstClass.SORT_SBN_HOUHOU_SUB_TITLE + "順）";
                }

                // 出力日付の書式設定
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";
                row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.Kyoten;

                row["D_FORMATTED_HAIKI_KBN"] = row["HAIKI_KBN"].ToString().Substring(0, 1);
                if (null != row["KOUFU_DATE"] && !String.IsNullOrEmpty(row["KOUFU_DATE"].ToString()))
                {
                    row["D_FORMATTED_KOUFU_DATE"] = DateTime.ParseExact(row["KOUFU_DATE"].ToString(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("yyyy/MM/dd");
                }
                if (null != row["HAIKI_SUU"] && !String.IsNullOrEmpty(row["HAIKI_SUU"].ToString()))
                {
                    row["D_FORMATTED_HAIKI_SUU"] = Convert.ToDecimal(decimal.Parse(row["HAIKI_SUU"].ToString())).ToString(suuryoFormat);
                }
                if (null != row["KANSAN_SUU"] && !String.IsNullOrEmpty(row["KANSAN_SUU"].ToString()))
                {
                    row["D_FORMATTED_KANSAN_SUU"] = Convert.ToDecimal(decimal.Parse(row["KANSAN_SUU"].ToString())).ToString(suuryoFormat);
                }
                if (null != row["GENNYOU_SUU"] && !String.IsNullOrEmpty(row["GENNYOU_SUU"].ToString()) && isIchijiKbn)
                {
                    row["D_FORMATTED_GENNYOU_SUU"] = Convert.ToDecimal(decimal.Parse(row["GENNYOU_SUU"].ToString())).ToString(suuryoFormat);
                }
                if (null != row["UPN_END_DATE"] && !String.IsNullOrEmpty(row["UPN_END_DATE"].ToString()))
                {
                    row["D_FORMATTED_UPN_END_DATE"] = DateTime.ParseExact(row["UPN_END_DATE"].ToString(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("yyyy/MM/dd");
                }
                if (null != row["SBN_END_DATE"] && !String.IsNullOrEmpty(row["SBN_END_DATE"].ToString()))
                {
                    row["D_FORMATTED_SBN_END_DATE"] = DateTime.ParseExact(row["SBN_END_DATE"].ToString(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("yyyy/MM/dd");
                }
                if (null != row["LAST_SBN_END_DATE"] && !String.IsNullOrEmpty(row["LAST_SBN_END_DATE"].ToString()))
                {
                    row["D_FORMATTED_LAST_SBN_END_DATE"] = DateTime.ParseExact(row["LAST_SBN_END_DATE"].ToString(), "yyyyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo).ToString("yyyy/MM/dd");
                }
                if (dto.IsGroupUnpanJutakusha1)
                {
                    row["GF_UPN_JYUTAKUSHA_NAME"] = row["UPN_JYUTAKUSHA_NAME1"];
                }
                else
                {
                    row["GF_UPN_JYUTAKUSHA_NAME"] = row["UPN_JYUTAKUSHA_NAME2"];
                }

                if (dto.IsGroupUnpanJutakusha1)
                {
                    row["GF_UPN_GYOUSHA_HAIKI_SUU_VLB"] = ConstClass.GROUP_FOOTER_UPN_GYOUSHA_1;
                }
                else
                {
                    row["GF_UPN_GYOUSHA_HAIKI_SUU_VLB"] = ConstClass.GROUP_FOOTER_UPN_GYOUSHA_2;
                }

                row["HST_JIGYOUJOU_KEY"] = (row["HST_GYOUSHA_CD"].ToString() + "      ").Substring(0, 6) + (row["HST_GENBA_CD"].ToString() + "      ").Substring(0, 6);
                row["SBN_JIGYOUJOU_KEY"] = (row["SBN_GYOUSHA_CD"].ToString() + "      ").Substring(0, 6) + (row["UPN_SAKI_GENBA_CD"].ToString() + "      ").Substring(0, 6);
                row["LAST_SBN_JIGYOUJOU_KEY"] = (row["LAST_SBN_GYOUSHA_CD"].ToString() + "      ").Substring(0, 6) + (row["LAST_SBN_GENBA_CD"].ToString() + "      ").Substring(0, 6);

                // 集計（交付年月日）
                if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_KOFU_DATE && kofuDate != row["KOUFU_DATE"].ToString())
                {
                    kofuDateHaikiSuu = 0;
                    kofuDateMaisuu = 0;
                    id = String.Empty;
                    kofuDate = row["KOUFU_DATE"].ToString();
                }
                kofuDateHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        kofuDateMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        kofuDateMaisuu++;
                    }
                }
                row["GF_KOFU_DATE_HAIKI_SUU"] = kofuDateHaikiSuu.ToString("#,##0.000");
                row["GF_KOFU_DATE_MAISUU"] = kofuDateMaisuu.ToString("#,##0 枚");

                // 集計（運搬終了日）
                if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_UPN_END_DATE && upnEndDate != row["UPN_END_DATE"].ToString())
                {
                    upnEndDateHaikiSuu = 0;
                    upnEndDateMaisuu = 0;
                    id = String.Empty;
                    upnEndDate = row["UPN_END_DATE"].ToString();
                }
                upnEndDateHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        upnEndDateMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        upnEndDateMaisuu++;
                    }
                }
                row["GF_UPN_END_DATE_HAIKI_SUU"] = upnEndDateHaikiSuu.ToString("#,##0.000");
                row["GF_UPN_END_DATE_MAISUU"] = upnEndDateMaisuu.ToString("#,##0 枚");

                // 集計（処分終了日）
                if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_SBN_END_DATE && sbnEndDate != row["SBN_END_DATE"].ToString())
                {
                    sbnEndDateHaikiSuu = 0;
                    sbnEndDateMaisuu = 0;
                    id = String.Empty;
                    sbnEndDate = row["SBN_END_DATE"].ToString();
                }
                sbnEndDateHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        sbnEndDateMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        sbnEndDateMaisuu++;
                    }
                }
                row["GF_SBN_END_DATE_HAIKI_SUU"] = sbnEndDateHaikiSuu.ToString("#,##0.000");
                row["GF_SBN_END_DATE_MAISUU"] = sbnEndDateMaisuu.ToString("#,##0 枚");

                // 集計（最終処分終了日）
                if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_LAST_SBN_END_DATE && lastSbnEndDate != row["LAST_SBN_END_DATE"].ToString())
                {
                    lastSbnEndDateHaikiSuu = 0;
                    lastSbnEndDateMaisuu = 0;
                    id = String.Empty;
                    lastSbnEndDate = row["LAST_SBN_END_DATE"].ToString();
                }
                lastSbnEndDateHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        lastSbnEndDateMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        lastSbnEndDateMaisuu++;
                    }
                }
                row["GF_LAST_SBN_END_DATE_HAIKI_SUU"] = lastSbnEndDateHaikiSuu.ToString("#,##0.000");
                row["GF_LAST_SBN_END_DATE_MAISUU"] = lastSbnEndDateMaisuu.ToString("#,##0 枚");

                // 使用する集計だけセット
                if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_KOFU_DATE)
                {
                    row["GF_DATE_HAIKI_SUU_VLB"] = ConstClass.GROUP_FOOTER_KOFU_DATE;
                    row["GF_DATE_VLB"] = row["D_FORMATTED_KOUFU_DATE"];
                    row["GF_DATE_HAIKI_SUU"] = row["GF_KOFU_DATE_HAIKI_SUU"];
                    row["GF_DATE_MAISUU"] = row["GF_KOFU_DATE_MAISUU"];
                }
                else if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_UPN_END_DATE)
                {
                    row["GF_DATE_HAIKI_SUU_VLB"] = ConstClass.GROUP_FOOTER_UPN_END_DATE;
                    row["GF_DATE_VLB"] = row["D_FORMATTED_UPN_END_DATE"];
                    row["GF_DATE_HAIKI_SUU"] = row["GF_UPN_END_DATE_HAIKI_SUU"];
                    row["GF_DATE_MAISUU"] = row["GF_UPN_END_DATE_MAISUU"];
                }
                else if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_SBN_END_DATE)
                {
                    row["GF_DATE_HAIKI_SUU_VLB"] = ConstClass.GROUP_FOOTER_SBN_END_DATE;
                    row["GF_DATE_VLB"] = row["D_FORMATTED_SBN_END_DATE"];
                    row["GF_DATE_HAIKI_SUU"] = row["GF_SBN_END_DATE_HAIKI_SUU"];
                    row["GF_DATE_MAISUU"] = row["GF_SBN_END_DATE_MAISUU"];
                }
                else if (dto.IsGroupDate && dto.Sort == ConstClass.SORT_LAST_SBN_END_DATE)
                {
                    row["GF_DATE_HAIKI_SUU_VLB"] = ConstClass.GROUP_FOOTER_LAST_SBN_END_DATE;
                    row["GF_DATE_VLB"] = row["D_FORMATTED_LAST_SBN_END_DATE"];
                    row["GF_DATE_HAIKI_SUU"] = row["GF_LAST_SBN_END_DATE_HAIKI_SUU"];
                    row["GF_DATE_MAISUU"] = row["GF_LAST_SBN_END_DATE_MAISUU"];
                }

                // 集計（排出事業者）
                if (dto.IsGroupHaishutsuJigyousha && hstGyoushaCd != row["HST_GYOUSHA_CD"].ToString())
                {
                    hstGyoushaHaikiSuu = 0;
                    hstGyoushaMaisuu = 0;
                    id = String.Empty;
                    hstGyoushaCd = row["HST_GYOUSHA_CD"].ToString();

                    hstGenbaHaikiSuu = 0;
                    hstGenbaMaisuu = 0;
                    id = String.Empty;
                    hstGenbaCd = row["HST_GENBA_CD"].ToString();
                }
                hstGyoushaHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        hstGyoushaMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        hstGyoushaMaisuu++;
                    }
                }
                row["GF_HST_GYOUSHA_HAIKI_SUU"] = hstGyoushaHaikiSuu.ToString("#,##0.000");
                row["GF_HST_GYOUSHA_MAISUU"] = hstGyoushaMaisuu.ToString("#,##0 枚");

                // 集計（排出事業場）
                if (dto.IsGroupHaishutsuJigyoujou && (hstGyoushaCd != row["HST_GYOUSHA_CD"].ToString() || hstGenbaCd != row["HST_GENBA_CD"].ToString()))
                {
                    hstGenbaHaikiSuu = 0;
                    hstGenbaMaisuu = 0;
                    id = String.Empty;
                    hstGenbaCd = row["HST_GENBA_CD"].ToString();
                }
                hstGenbaHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        hstGenbaMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        hstGenbaMaisuu++;
                    }
                }
                row["GF_HST_GENBA_HAIKI_SUU"] = hstGenbaHaikiSuu.ToString("#,##0.000");
                row["GF_HST_GENBA_MAISUU"] = hstGenbaMaisuu.ToString("#,##0 枚");

                // 集計（運搬受託者）
                if (dto.IsGroupUnpanJutakusha1 && upnGyoushaCd != row["UPN_GYOUSHA_CD1"].ToString())
                {
                    upnGyoushaHaikiSuu = 0;
                    upnGyoushaMaisuu = 0;
                    id = String.Empty;
                    upnGyoushaCd = row["UPN_GYOUSHA_CD1"].ToString();
                }
                if (dto.IsGroupUnpanJutakusha2 && upnGyoushaCd != row["UPN_GYOUSHA_CD2"].ToString())
                {
                    upnGyoushaHaikiSuu = 0;
                    upnGyoushaMaisuu = 0;
                    id = String.Empty;
                    upnGyoushaCd = row["UPN_GYOUSHA_CD2"].ToString();
                }
                upnGyoushaHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        upnGyoushaMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        upnGyoushaMaisuu++;
                    }
                }
                row["GF_UPN_GYOUSHA_HAIKI_SUU"] = upnGyoushaHaikiSuu.ToString("#,##0.000");
                row["GF_UPN_GYOUSHA_MAISUU"] = upnGyoushaMaisuu.ToString("#,##0 枚");

                // 集計（処分事業場）
                if (dto.IsGroupShobunJigyoujou && (sbnGyoushaCd != row["SBN_GYOUSHA_CD"].ToString() || sbnGenbaCd != row["UPN_SAKI_GENBA_CD"].ToString()))
                {
                    sbnGenbaHaikiSuu = 0;
                    sbnGenbaMaisuu = 0;
                    id = String.Empty;
                    sbnGyoushaCd = row["SBN_GYOUSHA_CD"].ToString();
                    sbnGenbaCd = row["UPN_SAKI_GENBA_CD"].ToString();
                }
                sbnGenbaHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        sbnGenbaMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        sbnGenbaMaisuu++;
                    }
                }
                row["GF_SBN_GENBA_HAIKI_SUU"] = sbnGenbaHaikiSuu.ToString("#,##0.000");
                row["GF_SBN_GENBA_MAISUU"] = sbnGenbaMaisuu.ToString("#,##0 枚");

                // 集計（最終処分場）
                if (dto.IsGroupLastShobunGenba && (lastSbnGyoushaCd != row["LAST_SBN_GYOUSHA_CD"].ToString() || lastSbnGenbaCd != row["LAST_SBN_GENBA_CD"].ToString()))
                {
                    lastSbnGyoushaHaikiSuu = 0;
                    lastSbnGenbaMaisuu = 0;
                    id = String.Empty;
                    lastSbnGyoushaCd = row["LAST_SBN_GYOUSHA_CD"].ToString();
                    lastSbnGenbaCd = row["LAST_SBN_GENBA_CD"].ToString();
                }
                lastSbnGyoushaHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        lastSbnGenbaMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        lastSbnGenbaMaisuu++;
                    }
                }
                row["GF_LAST_SBN_GENBA_HAIKI_SUU"] = lastSbnGyoushaHaikiSuu.ToString("#,##0.000");
                row["GF_LAST_SBN_GENBA_MAISUU"] = lastSbnGenbaMaisuu.ToString("#,##0 枚");

                // 集計（報告書分類）
                if (dto.IsGroupHoukokushoBunrui && houkokushoBunruiCd != row["HOUKOKUSHO_BUNRUI_CD"].ToString())
                {
                    houkokushoBunruiHaikiSuu = 0;
                    houkokushoBunruiMaisuu = 0;
                    id = String.Empty;
                    houkokushoBunruiCd = row["HOUKOKUSHO_BUNRUI_CD"].ToString();
                }
                houkokushoBunruiHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        houkokushoBunruiMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        houkokushoBunruiMaisuu++;
                    }
                }
                row["GF_HOUKOKUSHO_BUNRUI_HAIKI_SUU"] = houkokushoBunruiHaikiSuu.ToString("#,##0.000");
                row["GF_HOUKOKUSHO_BUNRUI_MAISUU"] = houkokushoBunruiMaisuu.ToString("#,##0 枚");

                // 集計（処分方法）
                if (dto.IsGroupShobunHouhou && sbnHouhouCd != row["SBN_HOUHOU_CD"].ToString())
                {
                    sbnHouhouHaikiSuu = 0;
                    sbnHouhouMaisuu = 0;
                    id = String.Empty;
                    sbnHouhouCd = row["SBN_HOUHOU_CD"].ToString();
                }
                sbnHouhouHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    if (id != row["MANIFEST_ID"].ToString())
                    {
                        sbnHouhouMaisuu++;
                    }
                }
                else
                {
                    if (id != row["ID"].ToString())
                    {
                        sbnHouhouMaisuu++;
                    }
                }
                row["GF_SBN_HOUHOU_HAIKI_SUU"] = sbnHouhouHaikiSuu.ToString("#,##0.000");
                row["GF_SBN_HOUHOU_MAISUU"] = sbnHouhouMaisuu.ToString("#,##0 枚");

                // 集計（総計）
                allHaikiSuu += this.ConvertNullOrEmptyToZero(row["KANSAN_SUU"]);
                row["GF_ALL_HAIKI_SUU"] = allHaikiSuu.ToString("#,##0.000");

                var maniCount = dt.AsEnumerable().Where(r => null != r["MANIFEST_ID"] && !String.IsNullOrEmpty(r["MANIFEST_ID"].ToString())).GroupBy(r => r["MANIFEST_ID"]).Count();
                var idCount = dt.AsEnumerable().Where(r => null == r["MANIFEST_ID"] || String.IsNullOrEmpty(r["MANIFEST_ID"].ToString())).GroupBy(r => r["ID"]).Count();
                row["GF_ALL_MAISUU"] = (maniCount + idCount).ToString("#,##0 枚");

                if (null != row["MANIFEST_ID"] && !String.IsNullOrEmpty(row["MANIFEST_ID"].ToString()))
                {
                    id = row["MANIFEST_ID"].ToString();
                }
                else
                {
                    id = row["ID"].ToString();
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトをdecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>NullかString.Emptyの場合、decimal.Zeroを返します</returns>
        internal decimal ConvertNullOrEmptyToZero(object value)
        {
            LogUtility.DebugMethodStart(value);

            decimal ret = decimal.Zero;

            if (!String.IsNullOrEmpty(Convert.ToString(value)))
            {
                decimal.TryParse(Convert.ToString(value), out ret);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();
            System.Data.DataTable dt = dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}
