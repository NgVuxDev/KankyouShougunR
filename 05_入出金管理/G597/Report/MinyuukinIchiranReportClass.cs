using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using CommonChouhyouPopup.App;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ReceiptPayManagement.MinyuukinIchiran
{
    /// <summary>
    /// 未入金一覧表帳票作成クラス
    /// </summary>
    internal class MinyuukinIchiranReportClass
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
        /// 帳票テンプレートのパス(取引先別)
        /// </summary>
        private static readonly string OutputFormFullPathName = "./Template/R369-Form.xml";

        // <summary>
        /// 帳票テンプレートのパス(業者別/現場別)
        /// </summary>
        private static readonly string OutputFormFullPathName2 = "./Template/R621-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        private static readonly string LAYOUT = "LAYOUT1";

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal MinyuukinIchiranReportClass()
        {
            LogUtility.DebugMethodStart();

            var mSysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mSysInfo = mSysInfoDao.GetAllData().FirstOrDefault();

            var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します(取引先別)
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport(DataTable dt, MinyuukinIchiranDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            var chouhyouDataTable = this.EditChouhyouDataTable(dt, dto);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouDataTable);

            reportInfo.Create(OutputFormFullPathName, LAYOUT, chouhyouDataTable);

            // グループの表示制御
            reportInfo.SetGroupVisible("EIGYOUSHA", false, dto.IsGroupEigyousha);
            reportInfo.SetGroupVisible("TORIHIKISAKI", false, dto.IsGroupTorihikisaki);

            // グループのキーを設定
            if (dto.Sort1 == ConstClass.SORT_1_EIGYOUSHA)
            {
                reportInfo.SetGroupGroupBy("SUB", "EIGYOUSHA_CD");
            }
            else
            {
                reportInfo.SetGroupGroupBy("SUB", "TORIHIKISAKI_CD");
            }

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;

            //レポートタイトルの設定
            if (dto.Sort1 == ConstClass.SORT_1_EIGYOUSHA)
            {
                reportPopup.ReportCaption = ConstClass.MINYUUKIN_ICHIRAN_TITLE + "（" + ConstClass.SORT_EIGYOUSHA_SUB_TITLE;
            }
            else
            {
                reportPopup.ReportCaption = ConstClass.MINYUUKIN_ICHIRAN_TITLE + "（" + ConstClass.SORT_TORIHIKISAKI_SUB_TITLE;
            }
            if (dto.Sort2 == ConstClass.SORT_2_CD)
            {
                reportPopup.ReportCaption = reportPopup.ReportCaption + ConstClass.SORT_CD_SUB_TITLE + "順）";
            }
            else if (dto.Sort2 == ConstClass.SORT_2_FURIGANA)
            {
                reportPopup.ReportCaption = reportPopup.ReportCaption + ConstClass.SORT_FURIGANA_SUB_TITLE + "順）";
            }

            reportPopup.ShowDialog();
            reportPopup.Dispose();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します(業者別/現場別)
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport2(DataTable dt, MinyuukinIchiranDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            var chouhyouDataTable = this.EditChouhyouDataTable2(dt, dto);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouDataTable);
            reportInfo.Create(OutputFormFullPathName2, LAYOUT, chouhyouDataTable);

            // グループのキーを設定
            reportInfo.SetGroupGroupBy("TORIHIKISAKI", "GROUP_CD");

            reportInfo.SetGroupVisible("SUB", false, dto.IsGroupTorihikisaki);

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
        private DataTable EditChouhyouDataTable(DataTable dt, MinyuukinIchiranDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            DataTable ret = dt;

            // DBで取得できない項目のカラムを追加

            // 帳票タイトル
            dt.Columns.Add("FH_TITLE_VLB");
            // 自社名称
            dt.Columns.Add("FH_CORP_NAME_RYAKU_VLB");
            // 拠点名称
            dt.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 出力日時
            dt.Columns.Add("FH_PRINT_DATE_VLB");
            // 営業者
            dt.Columns.Add("FH_EIGYOUSHA_VLB");
            // 取引先
            dt.Columns.Add("FH_TORIHIKISAKI_VLB");

            // ページヘッダラベル
            dt.Columns.Add("PH_LABEL_1_VLB");
            dt.Columns.Add("PH_LABEL_2_VLB");
            dt.Columns.Add("PH_LABEL_3_VLB");
            dt.Columns.Add("PH_LABEL_4_VLB");

            // グループヘッダCD&名称
            dt.Columns.Add("GH_SUB_CD_VLB");
            dt.Columns.Add("GH_SUB_NAME_VLB");

            // 明細CD&名称
            dt.Columns.Add("D_CD_VLB");
            dt.Columns.Add("D_NAME_VLB");

            // 明細未入金額
            dt.Columns.Add("D_MINYUUKIN_GAKU_VLB");
            // 明細請求額
            dt.Columns.Add("D_SEIKYUU_GAKU_VLB");
            // 明細入金額
            dt.Columns.Add("D_NYUUKIN_GAKU_VLB");

            // 取引先未入金額計
            dt.Columns.Add("GF_TORIHIKISAKI_MINYUUKIN_GAKU_VLB");
            // 取引先請求額計
            dt.Columns.Add("GF_TORIHIKISAKI_SEIKYUU_GAKU_VLB");
            // 取引先入金額計
            dt.Columns.Add("GF_TORIHIKISAKI_NYUUKIN_GAKU_VLB");
            // 営業者未入金額計
            dt.Columns.Add("GF_EIGYOUSHA_MINYUUKIN_GAKU_VLB");
            // 営業者請求額計
            dt.Columns.Add("GF_EIGYOUSHA_SEIKYUU_GAKU_VLB");
            // 営業者入金額計
            dt.Columns.Add("GF_EIGYOUSHA_NYUUKIN_GAKU_VLB");
            // 未入金額計
            dt.Columns.Add("GF_ALL_MINYUUKIN_GAKU_VLB");
            // 請求額計
            dt.Columns.Add("GF_ALL_SEIKYUU_GAKU_VLB");
            // 入金額計
            dt.Columns.Add("GF_ALL_NYUUKIN_GAKU_VLB");

            // カラムを書き込み可にする
            dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

            // データ加工

            var eigyoushaCd = ret.Rows[0]["EIGYOUSHA_CD"].ToString();
            var torihikisakiCd = ret.Rows[0]["TORIHIKISAKI_CD"].ToString();

            decimal eigyoushaMinyuukinGaku = 0;
            decimal eigyoushaSeikyuuGaku = 0;
            decimal eigyoushaNyuukinGaku = 0;
            decimal torihikisakiMinyuukinGaku = 0;
            decimal torihikisakiSeikyuuGaku = 0;
            decimal torihikisakiNyuukinGaku = 0;
            decimal minyuukinGaku = 0;
            decimal seikyuuGaku = 0;
            decimal nyuukinGaku = 0;

            foreach (DataRow row in ret.Rows)
            {
                row["FH_TITLE_VLB"] = ConstClass.MINYUUKIN_ICHIRAN_TITLE;

                // 並び順の指定でタイトル設定
                StringBuilder subTitleStringBuilder = new StringBuilder();
                subTitleStringBuilder.Append("（");
                if (dto.Sort1 == ConstClass.SORT_1_EIGYOUSHA)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_EIGYOUSHA_SUB_TITLE);
                }
                else
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_TORIHIKISAKI_SUB_TITLE);
                }
                if (dto.Sort2 == ConstClass.SORT_2_CD)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_CD_SUB_TITLE);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_FURIGANA_SUB_TITLE);
                }
                subTitleStringBuilder.Append("順）");
                row["FH_TITLE_VLB"] += subTitleStringBuilder.ToString();

                // 出力日付の書式設定
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";
                row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;

                // 営業者の文字列作成
                row["FH_EIGYOUSHA_VLB"] = "全て";
                if (!String.IsNullOrEmpty(dto.EigyoushaFrom) || !String.IsNullOrEmpty(dto.EigyoushaTo))
                {
                    var from = String.Empty;
                    if (!String.IsNullOrEmpty(dto.EigyoushaFrom))
                    {
                        from = dto.EigyoushaCdFrom + " " + dto.EigyoushaFrom;
                    }
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.EigyoushaTo))
                    {
                        to = dto.EigyoushaCdTo + " " + dto.EigyoushaTo;
                    }

                    row["FH_EIGYOUSHA_VLB"] = from + " ～ " + to;
                }

                // 取引先の文字列作成
                row["FH_TORIHIKISAKI_VLB"] = "全て";
                if (!String.IsNullOrEmpty(dto.TorihikisakiFrom) || !String.IsNullOrEmpty(dto.TorihikisakiTo))
                {
                    var from = String.Empty;
                    if (!String.IsNullOrEmpty(dto.TorihikisakiFrom))
                    {
                        from = dto.TorihikisakiCdFrom + " " + dto.TorihikisakiFrom;
                    }
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.TorihikisakiTo))
                    {
                        to = dto.TorihikisakiCdTo + " " + dto.TorihikisakiTo;
                    }

                    row["FH_TORIHIKISAKI_VLB"] = from + " ～ " + to;
                }

                // CDと名称
                if (dto.Sort1 == ConstClass.SORT_1_EIGYOUSHA)
                {
                    row["PH_LABEL_1_VLB"] = ConstClass.COLUMN_EIGYOUSHA_CD;
                    row["PH_LABEL_2_VLB"] = ConstClass.COLUMN_EIGYOUSHA;
                    row["PH_LABEL_3_VLB"] = ConstClass.COLUMN_TORIHIKISAKI_CD;
                    row["PH_LABEL_4_VLB"] = ConstClass.COLUMN_TORIHIKISAKI;

                    row["GH_SUB_CD_VLB"] = row["EIGYOUSHA_CD"];
                    row["GH_SUB_NAME_VLB"] = row["SHAIN_NAME_RYAKU"];

                    row["D_CD_VLB"] = row["TORIHIKISAKI_CD"];
                    row["D_NAME_VLB"] = row["TORIHIKISAKI_NAME_RYAKU"];
                }
                else
                {
                    row["PH_LABEL_1_VLB"] = ConstClass.COLUMN_TORIHIKISAKI_CD;
                    row["PH_LABEL_2_VLB"] = ConstClass.COLUMN_TORIHIKISAKI;
                    row["PH_LABEL_3_VLB"] = ConstClass.COLUMN_EIGYOUSHA_CD;
                    row["PH_LABEL_4_VLB"] = ConstClass.COLUMN_EIGYOUSHA;

                    row["GH_SUB_CD_VLB"] = row["TORIHIKISAKI_CD"];
                    row["GH_SUB_NAME_VLB"] = row["TORIHIKISAKI_NAME_RYAKU"];

                    row["D_CD_VLB"] = row["EIGYOUSHA_CD"];
                    row["D_NAME_VLB"] = row["SHAIN_NAME_RYAKU"];
                }

                // 集計（総計）
                minyuukinGaku += this.ConvertNullOrEmptyToZero(row["MINYUUKIN_GAKU"]);
                seikyuuGaku += this.ConvertNullOrEmptyToZero(row["SEIKYUU_GAKU"]);
                nyuukinGaku += this.ConvertNullOrEmptyToZero(row["NYUUKIN_GAKU"]);
                row["GF_ALL_MINYUUKIN_GAKU_VLB"] = minyuukinGaku.ToString("#,##0");
                row["GF_ALL_SEIKYUU_GAKU_VLB"] = seikyuuGaku.ToString("#,##0");
                row["GF_ALL_NYUUKIN_GAKU_VLB"] = nyuukinGaku.ToString("#,##0");

                // 集計（営業者）
                if (eigyoushaCd != row["EIGYOUSHA_CD"].ToString())
                {
                    eigyoushaMinyuukinGaku = 0;
                    eigyoushaSeikyuuGaku = 0;
                    eigyoushaNyuukinGaku = 0;
                    eigyoushaCd = row["EIGYOUSHA_CD"].ToString();
                }
                eigyoushaMinyuukinGaku += this.ConvertNullOrEmptyToZero(row["MINYUUKIN_GAKU"]);
                eigyoushaSeikyuuGaku += this.ConvertNullOrEmptyToZero(row["SEIKYUU_GAKU"]);
                eigyoushaNyuukinGaku += this.ConvertNullOrEmptyToZero(row["NYUUKIN_GAKU"]);
                row["GF_EIGYOUSHA_MINYUUKIN_GAKU_VLB"] = eigyoushaMinyuukinGaku.ToString("#,##0");
                row["GF_EIGYOUSHA_SEIKYUU_GAKU_VLB"] = eigyoushaSeikyuuGaku.ToString("#,##0");
                row["GF_EIGYOUSHA_NYUUKIN_GAKU_VLB"] = eigyoushaNyuukinGaku.ToString("#,##0");

                // 集計（取引先）
                if (torihikisakiCd != row["TORIHIKISAKI_CD"].ToString())
                {
                    torihikisakiMinyuukinGaku = 0;
                    torihikisakiSeikyuuGaku = 0;
                    torihikisakiNyuukinGaku = 0;
                    torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();
                }
                torihikisakiMinyuukinGaku += this.ConvertNullOrEmptyToZero(row["MINYUUKIN_GAKU"]);
                torihikisakiSeikyuuGaku += this.ConvertNullOrEmptyToZero(row["SEIKYUU_GAKU"]);
                torihikisakiNyuukinGaku += this.ConvertNullOrEmptyToZero(row["NYUUKIN_GAKU"]);
                row["GF_TORIHIKISAKI_MINYUUKIN_GAKU_VLB"] = torihikisakiMinyuukinGaku.ToString("#,##0");
                row["GF_TORIHIKISAKI_SEIKYUU_GAKU_VLB"] = torihikisakiSeikyuuGaku.ToString("#,##0");
                row["GF_TORIHIKISAKI_NYUUKIN_GAKU_VLB"] = torihikisakiNyuukinGaku.ToString("#,##0");

                // 金額をフォーマット
                row["D_MINYUUKIN_GAKU_VLB"] = Decimal.Parse(row["MINYUUKIN_GAKU"].ToString()).ToString("#,##0");
                row["D_SEIKYUU_GAKU_VLB"] = Decimal.Parse(row["SEIKYUU_GAKU"].ToString()).ToString("#,##0");
                row["D_NYUUKIN_GAKU_VLB"] = Decimal.Parse(row["NYUUKIN_GAKU"].ToString()).ToString("#,##0");
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// 帳票に渡すデータを加工します
        /// </summary>
        /// <param name="dt">元となるデータ</param>
        /// <param name="dto">画面入力データ</param>
        /// <returns>帳票用に作成したデータ</returns>
        private DataTable EditChouhyouDataTable2(DataTable dt, MinyuukinIchiranDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            DataTable ret = dt;

            // DBで取得できない項目のカラムを追加

            // 帳票タイトル
            dt.Columns.Add("FH_TITLE_VLB");
            // 自社名称
            dt.Columns.Add("FH_CORP_NAME_RYAKU_VLB");
            // 拠点名称
            dt.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 出力日時
            dt.Columns.Add("FH_PRINT_DATE_VLB");
            // 営業者
            dt.Columns.Add("FH_EIGYOUSHA_VLB");
            // 取引先
            dt.Columns.Add("FH_TORIHIKISAKI_VLB");

            // グループヘッダCD&名称
            dt.Columns.Add("GH_SUB_CD_VLB");
            dt.Columns.Add("GH_SUB_NAME_VLB");

            // 明細未入金額
            dt.Columns.Add("D_MINYUUKIN_GAKU_VLB");
            // 明細請求額
            dt.Columns.Add("D_SEIKYUU_GAKU_VLB");
            // 明細入金額
            dt.Columns.Add("D_NYUUKIN_GAKU_VLB");

            // 取引先未入金額計
            dt.Columns.Add("GF_TORIHIKISAKI_MINYUUKIN_GAKU_VLB");
            // 取引先請求額計
            dt.Columns.Add("GF_TORIHIKISAKI_SEIKYUU_GAKU_VLB");
            // 取引先入金額計
            dt.Columns.Add("GF_TORIHIKISAKI_NYUUKIN_GAKU_VLB");
            // 未入金額計
            dt.Columns.Add("GF_ALL_MINYUUKIN_GAKU_VLB");
            // 請求額計
            dt.Columns.Add("GF_ALL_SEIKYUU_GAKU_VLB");
            // 入金額計
            dt.Columns.Add("GF_ALL_NYUUKIN_GAKU_VLB");
            // グループCD
            dt.Columns.Add("GROUP_CD");

            // カラムを書き込み可にする
            dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

            // データ加工

            var torihikisakiCd = ret.Rows[0]["TORIHIKISAKI_CD"].ToString();
            //string gyoushaCd = Convert.ToString(ret.Rows[0]["GYOUSHA_CD"]);
            //string genbaCd = Convert.ToString(ret.Rows[0]["GENBA_CD"]);
            var groupKey = string.Format("{0}_{1}_{2}", ret.Rows[0]["TORIHIKISAKI_CD"], ret.Rows[0]["GYOUSHA_CD"], ret.Rows[0]["GENBA_CD"]);

            decimal torihikisakiMinyuukinGaku = 0;
            decimal torihikisakiSeikyuuGaku = 0;
            decimal torihikisakiNyuukinGaku = 0;
            decimal minyuukinGaku = 0;
            decimal seikyuuGaku = 0;
            decimal nyuukinGaku = 0;
            int groupCd = 0;

            foreach (DataRow row in ret.Rows)
            {
                row["FH_TITLE_VLB"] = ConstClass.MINYUUKIN_ICHIRAN_TITLE;

                // 並び順の指定でタイトル設定
                StringBuilder subTitleStringBuilder = new StringBuilder();
                subTitleStringBuilder.Append("（");
                subTitleStringBuilder.Append(ConstClass.SORT_TORIHIKISAKI_SUB_TITLE);
                if (dto.Sort2 == ConstClass.SORT_2_CD)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_CD_SUB_TITLE);
                }
                else if (dto.Sort2 == ConstClass.SORT_2_FURIGANA)
                {
                    subTitleStringBuilder.Append(ConstClass.SORT_FURIGANA_SUB_TITLE);
                }
                subTitleStringBuilder.Append("順）");
                row["FH_TITLE_VLB"] += subTitleStringBuilder.ToString();

                // 出力日付の書式設定
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";
                row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;

                // 営業者の文字列作成
                row["FH_EIGYOUSHA_VLB"] = "全て";
                if (!String.IsNullOrEmpty(dto.EigyoushaFrom) || !String.IsNullOrEmpty(dto.EigyoushaTo))
                {
                    var from = String.Empty;
                    if (!String.IsNullOrEmpty(dto.EigyoushaFrom))
                    {
                        from = dto.EigyoushaCdFrom + " " + dto.EigyoushaFrom;
                    }
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.EigyoushaTo))
                    {
                        to = dto.EigyoushaCdTo + " " + dto.EigyoushaTo;
                    }

                    row["FH_EIGYOUSHA_VLB"] = from + " ～ " + to;
                }

                // 取引先の文字列作成
                row["FH_TORIHIKISAKI_VLB"] = "全て";
                if (!String.IsNullOrEmpty(dto.TorihikisakiFrom) || !String.IsNullOrEmpty(dto.TorihikisakiTo))
                {
                    var from = String.Empty;
                    if (!String.IsNullOrEmpty(dto.TorihikisakiFrom))
                    {
                        from = dto.TorihikisakiCdFrom + " " + dto.TorihikisakiFrom;
                    }
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.TorihikisakiTo))
                    {
                        to = dto.TorihikisakiCdTo + " " + dto.TorihikisakiTo;
                    }

                    row["FH_TORIHIKISAKI_VLB"] = from + " ～ " + to;
                }

                // 金額Format
                row["D_MINYUUKIN_GAKU_VLB"] = this.ConvertNullOrEmptyToZero(row["MINYUUKIN_GAKU"]).ToString("#,##0");
                row["D_SEIKYUU_GAKU_VLB"] = this.ConvertNullOrEmptyToZero(row["SEIKYUU_GAKU"]).ToString("#,##0");
                row["D_NYUUKIN_GAKU_VLB"] = this.ConvertNullOrEmptyToZero(row["NYUUKIN_GAKU"]).ToString("#,##0");
                // 集計（総計）
                minyuukinGaku += this.ConvertNullOrEmptyToZero(row["MINYUUKIN_GAKU"]);
                seikyuuGaku += this.ConvertNullOrEmptyToZero(row["SEIKYUU_GAKU"]);
                nyuukinGaku += this.ConvertNullOrEmptyToZero(row["NYUUKIN_GAKU"]);
                row["GF_ALL_MINYUUKIN_GAKU_VLB"] = minyuukinGaku.ToString("#,##0");
                row["GF_ALL_SEIKYUU_GAKU_VLB"] = seikyuuGaku.ToString("#,##0");
                row["GF_ALL_NYUUKIN_GAKU_VLB"] = nyuukinGaku.ToString("#,##0");

                // 集計（取引先）
                var key = string.Format("{0}_{1}_{2}", row["TORIHIKISAKI_CD"], row["GYOUSHA_CD"], row["GENBA_CD"]);
                if (key != groupKey)
                {
                    groupCd++;
                    groupKey = key;
                }
                //if (torihikisakiCd != Convert.ToString(row["TORIHIKISAKI_CD"])
                //    || gyoushaCd != Convert.ToString(row["GYOUSHA_CD"])
                //    || genbaCd != Convert.ToString(row["GENBA_CD"]))
                //{
                //    groupCd++;
                //}

                // 集計（取引先）
                if (torihikisakiCd != row["TORIHIKISAKI_CD"].ToString())
                {
                    torihikisakiMinyuukinGaku = 0;
                    torihikisakiSeikyuuGaku = 0;
                    torihikisakiNyuukinGaku = 0;
                    torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();
                }
                torihikisakiMinyuukinGaku += this.ConvertNullOrEmptyToZero(row["MINYUUKIN_GAKU"]);
                torihikisakiSeikyuuGaku += this.ConvertNullOrEmptyToZero(row["SEIKYUU_GAKU"]);
                torihikisakiNyuukinGaku += this.ConvertNullOrEmptyToZero(row["NYUUKIN_GAKU"]);
                row["GF_TORIHIKISAKI_MINYUUKIN_GAKU_VLB"] = torihikisakiMinyuukinGaku.ToString("#,##0");
                row["GF_TORIHIKISAKI_SEIKYUU_GAKU_VLB"] = torihikisakiSeikyuuGaku.ToString("#,##0");
                row["GF_TORIHIKISAKI_NYUUKIN_GAKU_VLB"] = torihikisakiNyuukinGaku.ToString("#,##0");

                // 金額をフォーマット
                row["MINYUUKIN_GAKU"] = Decimal.Parse(row["MINYUUKIN_GAKU"].ToString()).ToString("#,##0");
                row["SEIKYUU_GAKU"] = Decimal.Parse(row["SEIKYUU_GAKU"].ToString()).ToString("#,##0");
                row["NYUUKIN_GAKU"] = Decimal.Parse(row["NYUUKIN_GAKU"].ToString()).ToString("#,##0");
                row["GROUP_CD"] = groupCd;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        /// <summary>
        /// オブジェクトをDecimal型に変換します
        /// </summary>
        /// <param name="value">対象のオブジェクト</param>
        /// <returns>NullかString.Emptyの場合、Decimal.Zeroを返します</returns>
        internal decimal ConvertNullOrEmptyToZero(object value)
        {
            LogUtility.DebugMethodStart(value);

            decimal ret = Decimal.Zero;

            if (!String.IsNullOrEmpty(Convert.ToString(value)))
            {
                Decimal.TryParse(Convert.ToString(value), out ret);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();
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
