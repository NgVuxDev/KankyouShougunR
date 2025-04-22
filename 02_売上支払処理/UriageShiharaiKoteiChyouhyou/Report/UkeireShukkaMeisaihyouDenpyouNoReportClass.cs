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
    /// 受入or出荷明細表作成クラス（伝票番号順）
    /// </summary>
    internal class UkeireShukkaMeisaihyouDenpyouNoReportClass
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
        private static readonly string OutputFormFullPathName = "./Template/R571_R575DenpyouNo-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        private static readonly string LAYOUT = "LAYOUT1";

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal UkeireShukkaMeisaihyouDenpyouNoReportClass()
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
        internal void CreateReport(DataTable dt, UkeireShukkaMeisaihyouDtoClass dto)
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

            if (1 == dto.DenpyouShuruiCd)
            {
                reportPopup.ReportCaption = ConstClass.UKEIRE_MEISAIHYOU_TITLE;
            }
            else if (2 == dto.DenpyouShuruiCd)
            {
                reportPopup.ReportCaption = ConstClass.SHUKKA_MEISAIHYOU_TITLE;
            }
            reportPopup.ReportCaption += ConstClass.SORT_DENPYOU_NO_SUB_TITLE;



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
        private DataTable EditChouhyouDataTable(DataTable dt, UkeireShukkaMeisaihyouDtoClass dto)
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
            // 伝票種類
            dt.Columns.Add("FH_DENPYOU_SHURUI_VLB");
            // 日付
            dt.Columns.Add("FH_HIDUKE_FLB");
            dt.Columns.Add("FH_DATE_VLB");
            // 入力担当者
            dt.Columns.Add("FH_TANTOUSHA_VLB");
            // 業者
            dt.Columns.Add("FH_GYOUSHA_VLB");
            // 現場
            dt.Columns.Add("FH_GENBA_VLB");
            // 運搬業者
            dt.Columns.Add("FH_UNPAN_GYOUSHA_VLB");
            // 形態
            dt.Columns.Add("FH_KEITAI_VLB");
            // 入出荷区分
            dt.Columns.Add("FH_NYUUSHUKKA_KBN_FLB");
            dt.Columns.Add("FH_NYUUSHUKKA_KBN_VLB");
            // 書式設定後の正味
            dt.Columns.Add("FORMATTED_NET_JYUURYOU");
            // 書式設定後の数量
            dt.Columns.Add("FORMATTED_SUURYOU");
            // 書式設定後の単価
            dt.Columns.Add("FORMATTED_TANKA");
            // 書式設定後の金額
            dt.Columns.Add("FORMATTED_SUM_KINGAKU");
            dt.Columns.Add("FORMATTED_ZEINUKI_KINGAKU");
            // 伝票番号合計（正味）
            dt.Columns.Add("GF_DENPYOU_NUMBER_SHOUMI_VLB");
            // 伝票番号合計（売上金額）
            dt.Columns.Add("GF_DENPYOU_NUMBER_URIAGE_KINGAKU_VLB");
            // 伝票番号合計（支払金額）
            dt.Columns.Add("GF_DENPYOU_NUMBER_SHIHARAI_KINGAKU_VLB");
            // 総合計（正味）
            dt.Columns.Add("GF_ALL_SHOUMI_VLB");
            // 総合計（売上金額）
            dt.Columns.Add("GF_ALL_URIAGE_KINGAKU_VLB");
            // 総合計（支払金額）
            dt.Columns.Add("GF_ALL_SHIHARAI_KINGAKU_VLB");

            // カラムを書き込み可にする
            dt.Columns.Cast<DataColumn>().ToList().ForEach(c => c.ReadOnly = false);

            // 現場CDの最大桁数を広げる
            dt.Columns["GENBA_CD"].MaxLength = 18;

            // データ加工

            decimal denpyouNumberShoumi = 0;
            decimal denpyouNumberUriageKingaku = 0;
            decimal denpyouNumberShiharaiKingaku = 0;
            decimal shoumi = 0;
            decimal uriageKingaku = 0;
            decimal shiharaiKingaku = 0;

            string denpyouNumber = ret.Rows[0]["DENPYOU_NUMBER"].ToString();

            foreach (DataRow row in ret.Rows)
            {
                // タイトルと項目名の設定
                if (1 == dto.DenpyouShuruiCd)
                {
                    row["FH_TITLE_VLB"] = ConstClass.UKEIRE_MEISAIHYOU_TITLE;
                }
                else if (2 == dto.DenpyouShuruiCd)
                {
                    row["FH_TITLE_VLB"] = ConstClass.SHUKKA_MEISAIHYOU_TITLE;
                }

                // 並び順の指定でタイトル設定
                row["FH_TITLE_VLB"] += ConstClass.SORT_DENPYOU_NO_SUB_TITLE;

                // 出力日付の書式設定
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm") + " 発行";
                row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm") + " 発行";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end

                // 自社略称
                row["FH_CORP_NAME_RYAKU_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;

                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;

                // 伝票種類
                row["FH_DENPYOU_SHURUI_VLB"] = ConstClass.DenpyouShurui[dto.DenpyouShuruiCd];

                // 日付の文字列作成
                if (1 == dto.DateShuruiCd)
                {
                    row["FH_HIDUKE_FLB"] = ConstClass.DATE_SHURUI_DENPYOU;
                    row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                }
                else if (2 == dto.DateShuruiCd)
                {
                    row["FH_HIDUKE_FLB"] = ConstClass.DATE_SHURUI_INPUT;
                    row["FH_DATE_VLB"] = dto.DateFrom + " ～ " + dto.DateTo;
                }

                // 入力担当者
                if (String.IsNullOrEmpty(dto.NyuuryokuTantoushaName))
                {
                    row["FH_TANTOUSHA_VLB"] = ConstClass.ALL;
                }
                else
                {
                    row["FH_TANTOUSHA_VLB"] = dto.NyuuryokuTantoushaCd + " " + dto.NyuuryokuTantoushaName;
                }

                // 業者の文字列作成
                if (!String.IsNullOrEmpty(dto.GyoushaFrom) || !String.IsNullOrEmpty(dto.GyoushaTo))
                {
                    row["FH_GYOUSHA_VLB"] = dto.GyoushaCdFrom + " " + dto.GyoushaFrom + " ～ " + "\n" + dto.GyoushaCdTo + " " + dto.GyoushaTo;
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

                // 運搬業者の文字列作成
                if (String.IsNullOrEmpty(dto.UnpanGyoushaFrom) && String.IsNullOrEmpty(dto.UnpanGyoushaTo))
                {
                    row["FH_UNPAN_GYOUSHA_VLB"] = ConstClass.ALL;
                }
                if (!String.IsNullOrEmpty(dto.UnpanGyoushaFrom) || !String.IsNullOrEmpty(dto.UnpanGyoushaTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.UnpanGyoushaFrom))
                    {
                        from = dto.UnpanGyoushaCdFrom + " " + dto.UnpanGyoushaFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.UnpanGyoushaTo))
                    {
                        to = dto.UnpanGyoushaCdTo + " " + dto.UnpanGyoushaTo;
                    }
                    row["FH_UNPAN_GYOUSHA_VLB"] = from + " ～ " + "\n" + to;
                }

                // 形態の文字列作成
                if (String.IsNullOrEmpty(dto.KeitaiFrom) && String.IsNullOrEmpty(dto.KeitaiTo))
                {
                    row["FH_KEITAI_VLB"] = ConstClass.ALL;
                }
                if (!String.IsNullOrEmpty(dto.KeitaiFrom) || !String.IsNullOrEmpty(dto.KeitaiTo))
                {
                    var from = String.Empty;
                    var to = String.Empty;
                    if (!String.IsNullOrEmpty(dto.KeitaiFrom))
                    {
                        from = Int32.Parse(dto.KeitaiKbnFrom).ToString("00") + " " + dto.KeitaiFrom;
                    }
                    if (!String.IsNullOrEmpty(dto.KeitaiTo))
                    {
                        to = Int32.Parse(dto.KeitaiKbnTo).ToString("00") + " " + dto.KeitaiTo;
                    }
                    row["FH_KEITAI_VLB"] = from + " ～ " + to;
                }

                if (dto.NyuushukkaKbn == ConstClass.NYUUSHUKKA_KBN_HINMEI)
                {
                    row["FH_NYUUSHUKKA_KBN_FLB"] = ConstClass.LABEL_NYUUSHUKKA_KBN_HINMEI;

                    // 品名の文字列作成
                    if (String.IsNullOrEmpty(dto.HinmeiFrom) && String.IsNullOrEmpty(dto.HinmeiTo))
                    {
                        row["FH_NYUUSHUKKA_KBN_VLB"] = ConstClass.ALL;
                    }
                    if (!String.IsNullOrEmpty(dto.HinmeiFrom) || !String.IsNullOrEmpty(dto.HinmeiTo))
                    {
                        var from = String.Empty;
                        var to = String.Empty;
                        if (!String.IsNullOrEmpty(dto.HinmeiFrom))
                        {
                            from = dto.HinmeiCdFrom + " " + dto.HinmeiFrom;
                        }
                        if (!String.IsNullOrEmpty(dto.HinmeiTo))
                        {
                            to = dto.HinmeiCdTo + " " + dto.HinmeiTo;
                        }
                        row["FH_NYUUSHUKKA_KBN_VLB"] = from + " ～ " + to;
                    }
                }
                else if (dto.NyuushukkaKbn == ConstClass.NYUUSHUKKA_KBN_SHURUI)
                {
                    row["FH_NYUUSHUKKA_KBN_FLB"] = ConstClass.LABEL_NYUUSHUKKA_KBN_SHURUI;

                    // 種類の文字列作成
                    if (String.IsNullOrEmpty(dto.ShuruiFrom) && String.IsNullOrEmpty(dto.ShuruiTo))
                    {
                        row["FH_NYUUSHUKKA_KBN_VLB"] = ConstClass.ALL;
                    }
                    if (!String.IsNullOrEmpty(dto.ShuruiFrom) || !String.IsNullOrEmpty(dto.ShuruiTo))
                    {
                        var from = String.Empty;
                        var to = String.Empty;
                        if (!String.IsNullOrEmpty(dto.ShuruiFrom))
                        {
                            from = dto.ShuruiCdFrom + " " + dto.ShuruiFrom;
                        }
                        if (!String.IsNullOrEmpty(dto.ShuruiTo))
                        {
                            to = dto.ShuruiCdTo + " " + dto.ShuruiTo;
                        }
                        row["FH_NYUUSHUKKA_KBN_VLB"] = from + " ～ " + to;
                    }
                }
                else if (dto.NyuushukkaKbn == ConstClass.NYUUSHUKKA_KBN_BUNRUI)
                {
                    row["FH_NYUUSHUKKA_KBN_FLB"] = ConstClass.LABEL_NYUUSHUKKA_KBN_BUNRUI;

                    // 分類の文字列作成
                    if (String.IsNullOrEmpty(dto.BunruiFrom) && String.IsNullOrEmpty(dto.BunruiTo))
                    {
                        row["FH_NYUUSHUKKA_KBN_VLB"] = ConstClass.ALL;
                    }
                    if (!String.IsNullOrEmpty(dto.BunruiFrom) || !String.IsNullOrEmpty(dto.BunruiTo))
                    {
                        var from = String.Empty;
                        var to = String.Empty;
                        if (!String.IsNullOrEmpty(dto.BunruiFrom))
                        {
                            from = dto.BunruiCdFrom + " " + dto.BunruiFrom;
                        }
                        if (!String.IsNullOrEmpty(dto.BunruiTo))
                        {
                            to = dto.BunruiCdTo + " " + dto.BunruiTo;
                        }
                        row["FH_NYUUSHUKKA_KBN_VLB"] = from + " ～ " + to;
                    }
                }

                // 伝票日付
                row["DENPYOU_DATE"] = Convert.ToDateTime(row["DENPYOU_DATE"]).ToString("yyyy/MM/dd");

                // 売上or支払日付
                if (DBNull.Value != row["URIAGE_SHIHARAI_DATE"])
                {
                    row["URIAGE_SHIHARAI_DATE"] = Convert.ToDateTime(row["URIAGE_SHIHARAI_DATE"]).ToString("yyyy/MM/dd");
                }

                // 集計（伝票番号）
                if (denpyouNumber != row["DENPYOU_NUMBER"].ToString())
                {
                    denpyouNumberShoumi = 0;
                    denpyouNumberUriageKingaku = 0;
                    denpyouNumberShiharaiKingaku = 0;
                    denpyouNumber = row["DENPYOU_NUMBER"].ToString();
                }
                denpyouNumberShoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                if ("1" == row["DENPYOU_KBN_CD"].ToString())
                {
                    denpyouNumberUriageKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                }
                else if ("2" == row["DENPYOU_KBN_CD"].ToString())
                {
                    denpyouNumberShiharaiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                }
                row["GF_DENPYOU_NUMBER_URIAGE_KINGAKU_VLB"] = denpyouNumberUriageKingaku;
                row["GF_DENPYOU_NUMBER_SHIHARAI_KINGAKU_VLB"] = denpyouNumberShiharaiKingaku;
                row["GF_DENPYOU_NUMBER_SHOUMI_VLB"] = denpyouNumberShoumi;

                // 集計（全体）
                shoumi += ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                if ("1" == row["DENPYOU_KBN_CD"].ToString())
                {
                    uriageKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                }
                else if ("2" == row["DENPYOU_KBN_CD"].ToString())
                {
                    shiharaiKingaku += ReportInfo.ConvertNullOrEmptyToZero(row["ZEINUKI_KINGAKU"]);
                }
                row["GF_ALL_URIAGE_KINGAKU_VLB"] = uriageKingaku;
                row["GF_ALL_SHIHARAI_KINGAKU_VLB"] = shiharaiKingaku;
                row["GF_ALL_SHOUMI_VLB"] = shoumi;

                // 正味の列を書式設定
                row["FORMATTED_NET_JYUURYOU"] = Convert.ToDecimal(row["NET_JYUURYOU"]).ToString(mSysInfo.SYS_JYURYOU_FORMAT);
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
                row["FORMATTED_SUM_KINGAKU"] = Convert.ToDecimal(row["SUM_KINGAKU"]).ToString("#,##0");
                row["FORMATTED_ZEINUKI_KINGAKU"] = Convert.ToDecimal(row["ZEINUKI_KINGAKU"]).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_URIAGE_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_URIAGE_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_DENPYOU_NUMBER_SHIHARAI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_DENPYOU_NUMBER_SHIHARAI_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_ALL_URIAGE_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_ALL_URIAGE_KINGAKU_VLB"]).ToString("#,##0");
                row["GF_ALL_SHIHARAI_KINGAKU_VLB"] = Convert.ToDecimal(row["GF_ALL_SHIHARAI_KINGAKU_VLB"]).ToString("#,##0");

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
