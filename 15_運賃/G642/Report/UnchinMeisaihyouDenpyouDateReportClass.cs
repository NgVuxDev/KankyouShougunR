using System;
using System.Data;
using System.Linq;
using CommonChouhyouPopup.App;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Carriage.UnchinMeisaihyou;

namespace Shougun.Core.Carriage.UnchinMeisaihyouDto
{
    /// <summary>
    /// 運賃明細表作成クラス（伝票日付順）
    /// </summary>
    internal class UnchinMeisaihyouDenpyouDateReportClass
    {
        /// <summary>
        /// 会社情報エンティティ
        /// </summary>
        private M_CORP_INFO mCorpInfo;

        public WINDOW_ID WindowId { get; set; }

        /// <summary>
        /// 帳票テンプレートのパス
        /// </summary>
        private static readonly string OutputFormFullPathName = "./Template/R644-Form.xml";

        /// <summary>
        /// 帳票レイアウト名
        /// </summary>
        private static readonly string LAYOUT = "LAYOUT1";

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal UnchinMeisaihyouDenpyouDateReportClass()
        {
            LogUtility.DebugMethodStart();

            var mCorpInfoDao = DaoInitUtility.GetComponent<IM_CORP_INFODao>();
            this.mCorpInfo = mCorpInfoDao.GetAllData().FirstOrDefault();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 帳票を作成します
        /// </summary>
        /// <param name="dt">出力データ</param>
        /// <param name="dto">画面入力データ</param>
        internal void CreateReport(DataTable dt, DtoClass dto, LogicClass.Format format)
        {
            LogUtility.DebugMethodStart(dt, dto, format);

            var chouhyouDataTable = this.EditChouhyouDataTable(dt, dto, format);

            // 現在表示されている一覧をレポート情報として生成
            ReportInfoBase reportInfo = new ReportInfoBase(chouhyouDataTable);
            reportInfo.Create(OutputFormFullPathName, LAYOUT, chouhyouDataTable);

            // グループの表示制御
            reportInfo.SetGroupVisible("DENPYOU_NUMBER", false, dto.IsGroupDenpyouNumber);

            // 印刷ポップアップ表示
            FormReportPrintPopup reportPopup = new FormReportPrintPopup(reportInfo);
            reportPopup.ReportCaption = string.Empty;
            reportPopup.Caption = "運賃明細表";
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
        private DataTable EditChouhyouDataTable(DataTable dt, DtoClass dto, LogicClass.Format format)
        {
            LogUtility.DebugMethodStart(dt, dto, format);

            // DTコピー作成
            DataTable ret = dt.DefaultView.ToTable();

            // DBで取得できない項目のカラムを追加
            // 帳票タイトル
            ret.Columns.Add("FH_TITLE_VLB");
            // 自社名称
            ret.Columns.Add("FH_CORP_NAME_VLB");
            // 拠点名称
            ret.Columns.Add("FH_KYOTEN_NAME_RYAKU_VLB");
            // 出力日時
            ret.Columns.Add("FH_PRINT_DATE_VLB");
            // 形態区分
            ret.Columns.Add("FH_KEITAI_KBN_VLB");
            // 伝種区分
            ret.Columns.Add("FH_DENSHU_KBN_VLB");
            // 運搬業者
            ret.Columns.Add("FH_UNPAN_GYOUSHA_VLB");
            // 日付
            ret.Columns.Add("FH_DATE_FLB");
            ret.Columns.Add("FH_DATE_VLB");
            // 書式設定後の正味
            ret.Columns.Add("FORMATTED_NET_JYUURYOU");
            // 書式設定後の数量
            ret.Columns.Add("FORMATTED_SUURYOU");
            // 書式設定後の単価
            ret.Columns.Add("FORMATTED_TANKA");
            // 書式設定後の金額
            ret.Columns.Add("FORMATTED_SUM_KINGAKU");
            // 伝票番号合計（正味）
            ret.Columns.Add("GF_DENPYOU_NET_JYUURYOU_VLB");
            // 伝票日付合計（正味）
            ret.Columns.Add("GF_DENPYOU_DATE_NET_JYUURYOU_VLB");
            // 総合計（正味）
            ret.Columns.Add("GF_ALL_NET_JYUURYOU_VLB");
            // 伝票番号合計（金額）
            ret.Columns.Add("GF_DENPYOU_KINGAKU_VLB");
            // 伝票日付合計（金額）
            ret.Columns.Add("GF_DENPYOU_DATE_KINGAKU_VLB");
            // 総合計（金額）
            ret.Columns.Add("GF_ALL_KINGAKU_VLB");

            // 一時カラムを書き込み可にする
            foreach (DataColumn column in ret.Columns)
            {
                column.ReadOnly = false;
            }

            // データ整理
            // 正味重量
            decimal? numNetJyuuryou = null; // 伝票番号計
            decimal? dateNetJyuuryou = null; // 日付小計
            decimal? allNetJyuuryou = null; // 合計
            // 金額
            decimal? numKingaku = null; // 伝票番号計
            decimal? dateKingaku = null; // 日付小計
            decimal? allKingaku = null; // 合計
            // 伝票
            string num = ret.Rows[0]["DENPYOU_NUMBER"].ToString(); // 伝票番号
            string date = ret.Rows[0]["DENPYOU_DATE"].ToString(); // 伝票日付

            foreach (DataRow row in ret.Rows)
            {
                // タイトルと項目名の設定
                row["FH_TITLE_VLB"] = ConstClass.UNCHIN_MEISAI_TITLE;
                // 並び順の指定でタイトル設定
                row["FH_TITLE_VLB"] += ConstClass.SORT_DENPYOU_DATE_SUB_TITLE;
                // 出力日付の書式設定
                // 20151030 katen #12048 「システム日付」の基準作成、適用 start
                //row["FH_PRINT_DATE_VLB"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
                row["FH_PRINT_DATE_VLB"] = this.getDBDateTime().ToString("yyyy/MM/dd HH:mm:ss") + " 発行";
                // 20151030 katen #12048 「システム日付」の基準作成、適用 end
                // 自社名
                row["FH_CORP_NAME_VLB"] = this.mCorpInfo.CORP_RYAKU_NAME;
                // 拠点名称
                row["FH_KYOTEN_NAME_RYAKU_VLB"] = dto.KyotenName;
                // 形態名称
                if (string.IsNullOrEmpty(dto.KeitaiKbnName))
                {
                    row["FH_KEITAI_KBN_VLB"] = ConstClass.ALL;
                }
                else
                {
                    row["FH_KEITAI_KBN_VLB"] = dto.KeitaiKbnName;
                }
                // 伝種区分
                row["FH_DENSHU_KBN_VLB"] = ConstClass.DenshuKbn[dto.DenshuKbn];
                // 運搬業者の文字列作成
                if (!string.IsNullOrEmpty(dto.UnpanGyoushaCdFrom) || !string.IsNullOrEmpty(dto.UnpanGyoushaCdTo))
                {
                    var from = string.Empty;
                    var to = string.Empty;
                    if (!string.IsNullOrEmpty(dto.UnpanGyoushaCdFrom))
                    {
                        from = dto.UnpanGyoushaCdFrom + dto.UnpanGyoushaFrom;
                    }
                    if (!string.IsNullOrEmpty(dto.UnpanGyoushaCdTo))
                    {
                        to = dto.UnpanGyoushaCdTo + dto.UnpanGyoushaTo;
                    }
                    row["FH_UNPAN_GYOUSHA_VLB"] = from + " ～ " + to;
                }
                // 日付の文字列作成
                if (1 == dto.DateShuruiCd)
                {
                    row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_DENPYOU;
                    row["FH_DATE_VLB"] = dto.DateFrom.Substring(0, 10) + " ～ " + dto.DateTo.Substring(0, 10);
                }
                else if (2 == dto.DateShuruiCd)
                {
                    row["FH_DATE_FLB"] = ConstClass.DATE_SHURUI_INPUT;
                    row["FH_DATE_VLB"] = dto.DateFrom.Substring(0, 10) + " ～ " + dto.DateTo.Substring(0, 10);
                }

                // 伝票番号毎リセット
                if (num != row["DENPYOU_NUMBER"].ToString())
                {
                    numNetJyuuryou = null;
                    numKingaku = null;

                    num = row["DENPYOU_NUMBER"].ToString();
                }

                // 伝票日付毎リセット
                if (date != row["DENPYOU_DATE"].ToString())
                {
                    dateNetJyuuryou = null;
                    dateKingaku = null;

                    date = row["DENPYOU_DATE"].ToString();
                }

                // 正味の列を書式設定
                if (!Convert.IsDBNull(row["NET_JYUURYOU"]))
                {
                    // DBNull以外の場合、指定フォーマットで表示
                    var tmpNetJyuuryou = ReportInfo.ConvertNullOrEmptyToZero(row["NET_JYUURYOU"]);
                    row["FORMATTED_NET_JYUURYOU"] = tmpNetJyuuryou.ToString(format.JYUURYOU_FORMAT);

                    // 集計
                    // 正味は0以外、又は0の時重量書式は0無視しない場合
                    if (tmpNetJyuuryou != decimal.Zero || !format.JYUURYOU_EMPTY_ZERO)
                    {
                        numNetJyuuryou = (numNetJyuuryou ?? decimal.Zero) + tmpNetJyuuryou;
                        dateNetJyuuryou = (dateNetJyuuryou ?? decimal.Zero) + tmpNetJyuuryou;
                        allNetJyuuryou = (allNetJyuuryou ?? decimal.Zero) + tmpNetJyuuryou;
                    }
                }
                else
                {
                    // DBNullの場合、空白で表示
                    row["FORMATTED_NET_JYUURYOU"] = string.Empty;
                }

                // 数量の列を書式設定
                if (!Convert.IsDBNull(row["SUURYOU"]))
                {
                    row["FORMATTED_SUURYOU"] = ReportInfo.ConvertNullOrEmptyToZero(row["SUURYOU"]).ToString(format.SUURYOU_FORMAT);
                }
                else
                {
                    row["FORMATTED_SUURYOU"] = string.Empty;
                }

                // 単価の列を書式設定
                if (!Convert.IsDBNull(row["TANKA"]))
                {
                    row["FORMATTED_TANKA"] = ReportInfo.ConvertNullOrEmptyToZero(row["TANKA"]).ToString(format.TANKA_FORMAT);
                }
                else
                {
                    row["FORMATTED_TANKA"] = string.Empty;
                }

                // 金額の列を書式設定
                if (!Convert.IsDBNull(row["KINGAKU"]))
                {
                    var tmpKingaku = ReportInfo.ConvertNullOrEmptyToZero(row["KINGAKU"]);
                    row["FORMATTED_SUM_KINGAKU"] = tmpKingaku.ToString(format.KINGAKU_FORMAT);

                    // 集計
                    // 金額は0以外、又は0の時金額書式は0無視しないの場合
                    if (tmpKingaku != decimal.Zero || !format.KINGAKU_EMPTY_ZERO)
                    {
                        numKingaku = (numKingaku ?? decimal.Zero) + tmpKingaku;
                        dateKingaku = (dateKingaku ?? decimal.Zero) + tmpKingaku;
                        allKingaku = (allKingaku ?? decimal.Zero) + tmpKingaku;
                    }
                }
                else
                {
                    row["FORMATTED_SUM_KINGAKU"] = string.Empty;
                }

                // 正味重量(伝票番号・伝票日付・総)合計
                // 計算値があれば、システム書式を無視し、0は0で表示する。
                row["GF_DENPYOU_NET_JYUURYOU_VLB"] = numNetJyuuryou.HasValue ? numNetJyuuryou.Value.ToString(format.ZERO_FORMAT_DECIMAL) : string.Empty;
                row["GF_DENPYOU_DATE_NET_JYUURYOU_VLB"] = dateNetJyuuryou.HasValue ? dateNetJyuuryou.Value.ToString(format.ZERO_FORMAT_DECIMAL) : string.Empty;
                row["GF_ALL_NET_JYUURYOU_VLB"] = allNetJyuuryou.HasValue ? allNetJyuuryou.Value.ToString(format.ZERO_FORMAT_DECIMAL) : string.Empty;

                // 金額(伝票番号・伝票日付・総)合計
                // 計算値があれば、0は0で表示する。
                row["GF_DENPYOU_KINGAKU_VLB"] = numKingaku.HasValue ? numKingaku.Value.ToString(format.ZERO_FORMAT_INTEGER) : string.Empty;
                row["GF_DENPYOU_DATE_KINGAKU_VLB"] = dateKingaku.HasValue ? dateKingaku.Value.ToString(format.ZERO_FORMAT_INTEGER) : string.Empty;
                row["GF_ALL_KINGAKU_VLB"] = allKingaku.HasValue ? allKingaku.Value.ToString(format.ZERO_FORMAT_INTEGER) : string.Empty;
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            r_framework.Dao.GET_SYSDATEDao dao = r_framework.Utility.DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
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
