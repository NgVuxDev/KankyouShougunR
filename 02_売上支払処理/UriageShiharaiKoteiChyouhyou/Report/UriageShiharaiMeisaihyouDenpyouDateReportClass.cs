using System;
using System.Data;
using System.Linq;
using CommonChouhyouPopup.App;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    /// <summary>
    /// 売上or支払明細表作成クラス（伝票日付順）
    /// </summary>
    internal class UriageShiharaiMeisaihyouDenpyouDateReportClass
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
        private static readonly string OutputFormFullPathName = "./Template/R570_R573DenpyouDate-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        private static readonly string LAYOUT = "LAYOUT1";

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal UriageShiharaiMeisaihyouDenpyouDateReportClass()
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
        internal void CreateReport(DataTable dt, UriageShiharaiMeisaihyouDtoClass dto)
        {
            LogUtility.DebugMethodStart(dt, dto);

            var chouhyouDataTable = this.EditChouhyouDataTable(dt, dto);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouDataTable);
            reportInfo.Create(OutputFormFullPathName, LAYOUT, chouhyouDataTable);

            // グループの表示制御
            reportInfo.SetGroupVisible("DENPYOU_NUMBER", true, dto.IsGroupDenpyouNumber);

            // フィールドの表示制御
            if (dto.GyoushaCdFrom != dto.GyoushaCdTo)
            {
                reportInfo.SetFieldVisible("FH_GENBA_FLB", false);
                reportInfo.SetFieldVisible("FH_GENBA_VLB", false);
            }

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = String.Empty;

            if (1 == dto.DenpyouKbnCd)
            {
                reportPopup.ReportCaption = ConstClass.URIAGE_MEISAIHYOU_TITLE;
            }
            else if (2 == dto.DenpyouKbnCd)
            {
                reportPopup.ReportCaption = ConstClass.SHIHARAI_MEISAIHYOU_TITLE;
            }
            reportPopup.ReportCaption += ConstClass.SORT_DENPYOU_DATE_SUB_TITLE;

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
        private DataTable EditChouhyouDataTable(DataTable dt, UriageShiharaiMeisaihyouDtoClass dto)
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
            // 伝票区分
            dt.Columns.Add("FH_DENPYOU_KBN_VLB");
            // 伝票種類
            dt.Columns.Add("FH_DENPYOU_SHURUI_VLB");
            // 日付
            dt.Columns.Add("FH_DATE_FLB");
            dt.Columns.Add("FH_DATE_VLB");
            // 入力担当者
            dt.Columns.Add("FH_TANTOUSHA_VLB");
            // 取引区分
            dt.Columns.Add("FH_TORIHIKI_KBN_VLB");
            // 締状況
            dt.Columns.Add("FH_SHIME_VLB");
            // 確定フラグ
            dt.Columns.Add("FH_KAKUTEI_FLG_VLB");
            // 取引先
            dt.Columns.Add("FH_TORIHIKISAKI_VLB");
            // 業者
            dt.Columns.Add("FH_GYOUSHA_VLB");
            // 現場
            dt.Columns.Add("FH_GENBA_VLB");
            // 売上or支払日付
            dt.Columns.Add("PH_URIAGE_SHIHARAI_DATE_VLB");
            // 書式設定後の正味
            dt.Columns.Add("FORMATTED_NET_JYUURYOU");
            // 書式設定後の数量
            dt.Columns.Add("FORMATTED_SUURYOU");
            // 書式設定後の単価
            dt.Columns.Add("FORMATTED_TANKA");
            // 書式設定後の金額
            dt.Columns.Add("FORMATTED_SUM_KINGAKU");
            // 書式設定後の税
            dt.Columns.Add("FORMATTED_ZEI");
            // 書式設定後の税込金額
            dt.Columns.Add("FORMATTED_ZEIKOMI_KINGAKU");
            // 伝票番号合計（正味）
            dt.Columns.Add("GF_DENPYOU_NUMBER_SHOUMI_VLB");
            // 伝票番号合計（金額）
            dt.Columns.Add("GF_DENPYOU_NUMBER_KINGAKU_VLB");
            // 伝票番号合計（税）
            dt.Columns.Add("GF_DENPYOU_NUMBER_ZEI_VLB");
            // 伝票番号合計（税込金額）
            dt.Columns.Add("GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB");
            // 伝票日付合計（正味）
            dt.Columns.Add("GF_DENPYOU_DATE_SHOUMI_VLB");
            // 伝票日付合計（金額）
            dt.Columns.Add("GF_DENPYOU_DATE_KINGAKU_VLB");
            // 伝票日付合計（税）
            dt.Columns.Add("GF_DENPYOU_DATE_ZEI_VLB");
            // 伝票日付合計（税込金額）
            dt.Columns.Add("GF_DENPYOU_DATE_ZEIKOMI_KINGAKU_VLB");
            // 総合計（正味）
            dt.Columns.Add("GF_ALL_SHOUMI_VLB");
            // 総合計（金額）
            dt.Columns.Add("GF_ALL_KINGAKU_VLB");
            // 総合計（税）
            dt.Columns.Add("GF_ALL_ZEI_VLB");
            // 総合計（税込金額）
            dt.Columns.Add("GF_ALL_ZEIKOMI_KINGAKU_VLB");

            // カラムを書き込み可にする
            dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

            // 現場CDの最大桁数を広げる
            dt.Columns["GENBA_CD"].MaxLength = 18;

            // データ加工

            decimal denpyouDateShoumi = 0;
            decimal denpyouDateKingaku = 0;
            decimal denpyouDateZei = 0;
            decimal denpyouDateZeikomiKingaku = 0;
            decimal denpyouNumberShoumi = 0;
            decimal denpyouNumberKingaku = 0;
            decimal denpyouNumberZei = 0;
            decimal denpyouNumberZeikomiKingaku = 0;
            decimal shoumi = 0;
            decimal kingaku = 0;
            decimal zei = 0;
            decimal zeikomiKingaku = 0;

            string denpyouDate = ret.Rows[0]["DENPYOU_DATE"].ToString();
            string denpyouNumber = ret.Rows[0]["DENPYOU_NUMBER"].ToString();

            foreach (DataRow row in ret.Rows)
            {
                // タイトルと項目名の設定
                if (1 == dto.DenpyouKbnCd)
                {
                    row["FH_TITLE_VLB"] = ConstClass.URIAGE_MEISAIHYOU_TITLE;
                    row["PH_URIAGE_SHIHARAI_DATE_VLB"] = ConstClass.URIAGE_DATE_COLUMN_NAME;
                }
                else if (2 == dto.DenpyouKbnCd)
                {
                    row["FH_TITLE_VLB"] = ConstClass.SHIHARAI_MEISAIHYOU_TITLE;
                    row["PH_URIAGE_SHIHARAI_DATE_VLB"] = ConstClass.SHIHARAI_DATE_COLUMN_NAME;
                }

                // 並び順の指定でタイトル設定
                row["FH_TITLE_VLB"] += ConstClass.SORT_DENPYOU_DATE_SUB_TITLE;

                // 出力日付の書式設定
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";
                row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;

                // 伝票区分
                row["FH_DENPYOU_KBN_VLB"] = ConstClass.DenpyouKbn[dto.DenpyouKbnCd];

                // 伝票種類
                row["FH_DENPYOU_SHURUI_VLB"] = ConstClass.DenpyouShurui[dto.DenpyouShuruiCd];

                // 日付の文字列作成
                if (1 == dto.DateShuruiCd)
                {
                    row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_DENPYOU;
                    row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                }
                else if (2 == dto.DateShuruiCd)
                {
                    if (1 == dto.DenpyouKbnCd)
                    {
                        row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_URIAGE;
                        row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                    }
                    else if (2 == dto.DenpyouKbnCd)
                    {
                        row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_SHIHARAI;
                        row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                    }
                }
                else if (3 == dto.DateShuruiCd)
                {
                    row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_INPUT;
                    row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                }

                // 入力担当者
                if (String.IsNullOrEmpty(dto.NyuuryokuTantousyaName))
                {
                    row["FH_TANTOUSHA_VLB"] = ConstClass.ALL;
                }
                else
                {
                    row["FH_TANTOUSHA_VLB"] = dto.NyuuryokuTantousyaName;
                }

                // 取引区分
                row["FH_TORIHIKI_KBN_VLB"] = ConstClass.TorihikiKbn[dto.TorihikiKbnCd];

                // 締状況
                row["FH_SHIME_VLB"] = ConstClass.ShimeJoukyou[dto.ShimeJoukyouCd];

                // 確定フラグ
                row["FH_KAKUTEI_FLG_VLB"] = ConstClass.KakuteiKbn[dto.KakuteiKbnCd];

                // 取引先の文字列作成
                if (!String.IsNullOrEmpty(dto.TorihikisakiFrom) || !String.IsNullOrEmpty(dto.TorihikisakiTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.TorihikisakiFrom))
                    {
                        from = dto.TorihikisakiCdFrom + " " + dto.TorihikisakiFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.TorihikisakiTo))
                    {
                        to = dto.TorihikisakiCdTo + " " + dto.TorihikisakiTo;
                    }
                    row["FH_TORIHIKISAKI_VLB"] = from + " ～ " + "\n" + to;
                }

                // 業者の文字列作成
                if (!String.IsNullOrEmpty(dto.GyoushaFrom) || !String.IsNullOrEmpty(dto.GyoushaTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.GyoushaFrom))
                    {
                        from = dto.GyoushaCdFrom + " " + dto.GyoushaFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.GyoushaTo))
                    {
                        to = dto.GyoushaCdTo + " " + dto.GyoushaTo;
                    }
                    row["FH_GYOUSHA_VLB"] = from + " ～ " + "\n" + to;
                }

                // 現場の文字列作成
                if (String.IsNullOrEmpty(dto.GenbaFrom) && String.IsNullOrEmpty(dto.GenbaTo))
                {
                    row["FH_GENBA_VLB"] = ConstClass.ALL;
                }
                if (!String.IsNullOrEmpty(dto.GenbaFrom) || !String.IsNullOrEmpty(dto.GenbaTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.GenbaFrom))
                    {
                        from = dto.GenbaCdFrom + " " + dto.GenbaFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.GenbaTo))
                    {
                        to = dto.GenbaCdTo + " " + dto.GenbaTo;
                    }
                    row["FH_GENBA_VLB"] = from + " ～ " + "\n" + to;
                }

                // 伝票日付
                row["DENPYOU_DATE"] = Convert.ToDateTime(row["DENPYOU_DATE"]).ToString("yyyy/MM/dd");

                // 売上or支払日付
                if (DBNull.Value != row["URIAGE_SHIHARAI_DATE"])
                {
                    row["URIAGE_SHIHARAI_DATE"] = Convert.ToDateTime(row["URIAGE_SHIHARAI_DATE"]).ToString("yyyy/MM/dd");
                }

                // 税（明細税のときだけ表示）
                row["FORMATTED_ZEI"] = String.Empty;
                if ("3" == row["ZEI_KEISAN_KBN_CD"].ToString() || !String.IsNullOrEmpty(row["HINMEI_ZEI_KBN_CD"].ToString()))
                {
                    row["FORMATTED_ZEI"] = ReportInfo.ConvertNullOrEmptyToZero(row["MEISAI_TAX"]);
                }

                // 税込金額
                row["FORMATTED_ZEIKOMI_KINGAKU"] = ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]) + ReportInfo.ConvertNullOrEmptyToZero(row["MEISAI_TAX"]);

                // 集計（全体）
                shoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                kingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                zei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                zeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_ALL_SHOUMI_VLB"] = shoumi;
                row["GF_ALL_KINGAKU_VLB"] = kingaku;
                row["GF_ALL_ZEI_VLB"] = zei;
                row["GF_ALL_ZEIKOMI_KINGAKU_VLB"] = kingaku + zei;

                // 集計（伝票番号）
                if (denpyouNumber != row["DENPYOU_NUMBER"].ToString())
                {
                    denpyouNumberShoumi = 0;
                    denpyouNumberKingaku = 0;
                    denpyouNumberZei = 0;
                    denpyouNumberZeikomiKingaku = 0;
                    denpyouNumber = row["DENPYOU_NUMBER"].ToString();
                }
                denpyouNumberShoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                denpyouNumberKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                denpyouNumberZei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                denpyouNumberZeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_DENPYOU_NUMBER_SHOUMI_VLB"] = denpyouNumberShoumi;
                row["GF_DENPYOU_NUMBER_KINGAKU_VLB"] = denpyouNumberKingaku;
                row["GF_DENPYOU_NUMBER_ZEI_VLB"] = denpyouNumberZei;
                row["GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB"] = denpyouNumberKingaku + denpyouNumberZei;

                // 集計（伝票日付）
                if (denpyouDate != row["DENPYOU_DATE"].ToString())
                {
                    denpyouDateShoumi = 0;
                    denpyouDateKingaku = 0;
                    denpyouDateZei = 0;
                    denpyouDateZeikomiKingaku = 0;
                    denpyouDate = row["DENPYOU_DATE"].ToString();
                }
                denpyouDateShoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                denpyouDateKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);  // 集計には税抜金額を使用する
                denpyouDateZei += ReportInfo.ConvertNullOrEmptyToZero(row["DENPYOU_TAX"]);
                denpyouDateZeikomiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["FORMATTED_ZEIKOMI_KINGAKU"]);

                row["GF_DENPYOU_DATE_SHOUMI_VLB"] = denpyouDateShoumi;
                row["GF_DENPYOU_DATE_KINGAKU_VLB"] = denpyouDateKingaku;
                row["GF_DENPYOU_DATE_ZEI_VLB"] = denpyouDateZei;
                row["GF_DENPYOU_DATE_ZEIKOMI_KINGAKU_VLB"] = denpyouDateKingaku + denpyouDateZei;

                // 正味の列を書式設定
                row["FORMATTED_NET_JYUURYOU"] = Convert.ToDecimal(row["NET_JYUURYOU"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_DENPYOU_DATE_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_DATE_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_DENPYOU_NUMBER_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
                row["GF_ALL_SHOUMI_VLB"] = Convert.ToDecimal(row["GF_ALL_SHOUMI_VLB"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);

                // 数量の列を書式設定
                row["FORMATTED_SUURYOU"] = Convert.ToDecimal(row["SUURYOU"]).ToString(mSysInfo.SYS_SUURYOU_FORMAT);

                // 単価の列を書式設定
                if (string.IsNullOrEmpty(row["TANKA"].ToString()))
                {
                    row["FORMATTED_TANKA"] = null;
                }
                else
                {
                    row["FORMATTED_TANKA"] = Convert.ToDecimal(row["TANKA"]).ToString(mSysInfo.SYS_TANKA_FORMAT);
                }

                // 金額の列を書式設定
                row["FORMATTED_SUM_KINGAKU"] = Convert.ToDecimal(row["ZEINUKI_KINGAKU"]).ToString("#,##0");
                row["GF_DENPYOU_DATE_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_DATE_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_ALL_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_ALL_KINGAKU_VLB"]).ToString("#,##0");

                // 消費税の列を書式設定
                if (!String.IsNullOrEmpty(row["FORMATTED_ZEI"].ToString()))
                {
                    if (row["UCHIZEI"] != null && !String.IsNullOrEmpty(row["UCHIZEI"].ToString()))
                    {
                        row["FORMATTED_ZEI"] = Convert.ToDecimal(row["FORMATTED_ZEI"]).ToString("(#,##0)");
                    }
                    else
                    {
                        row["FORMATTED_ZEI"] = Convert.ToDecimal(row["FORMATTED_ZEI"]).ToString("#,##0");
                    }
                }
                row["GF_DENPYOU_DATE_ZEI_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_DATE_ZEI_VLB"].ToString()).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_ZEI_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_ZEI_VLB"].ToString()).ToString("#,##0");
                row["GF_ALL_ZEI_VLB"] = Convert.ToDecimal(row["GF_ALL_ZEI_VLB"].ToString()).ToString("#,##0");

                // 税込金額の列を書式設定
                row["FORMATTED_ZEIKOMI_KINGAKU"] = Convert.ToDecimal(row["FORMATTED_ZEIKOMI_KINGAKU"]).ToString("#,##0");
                row["GF_DENPYOU_DATE_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_DATE_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_ALL_ZEIKOMI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_ALL_ZEIKOMI_KINGAKU_VLB"]).ToString("#,##0");

                // 集計ヘッダ表示用に現場CDを編集
                row["GENBA_CD"] = String.Format("{0,6}", row["TORIHIKISAKI_CD"]) + String.Format("{0,6}", row["GYOUSHA_CD"]) + String.Format("{0,6}", row["GENBA_CD"]);
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
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
