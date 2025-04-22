using r_framework.Entity;
using System;

namespace Shougun.Core.Scale.KeiryouShukeiHyo
{
    /// <summary>
    /// 計量集計表帳票DTOクラス
    /// </summary>
    public class KeiryouShukeiHyoReportDto
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeiryouShukeiHyoReportDto()
        {
            this.SYS_INFO = new M_SYS_INFO() { SYS_JYURYOU_FORMAT = "#,##0.0", SYS_SUURYOU_FORMAT = "#,##0.0" };
        }

        /// <summary>
        /// システム情報を取得・設定します
        /// </summary>
        public M_SYS_INFO SYS_INFO { get; set; }

        /// <summary>
        /// 自社情報を取得・設定します
        /// </summary>
        public M_CORP_INFO CORP_INFO { get; set; }

        /// <summary>
        /// 帳票タイトルを取得・設定します
        /// </summary>
        public String TITLE { get; set; }

        /// <summary>
        /// 自社名を取得します
        /// </summary>
        public String JISHA { get { return this.CORP_INFO.CORP_RYAKU_NAME; } }

        /// <summary>
        /// 拠点名を取得・設定します
        /// </summary>
        public String KYOTEN { get; set; }

        /// <summary>
        /// 発行年月日を取得・設定します
        /// </summary>
        public String HAKKOU_DATE { get; set; }

        /// <summary>
        /// 条件欄１を取得・設定します
        /// </summary>
        public String JOUKEN_1 { get; set; }

        /// <summary>
        /// 条件欄２を取得・設定します
        /// </summary>
        public String JOUKEN_2 { get; set; }

        /// <summary>
        /// カラム名１を取得・設定します
        /// </summary>
        public String COLUMN_1 { get; set; }

        /// <summary>
        /// カラム名２を取得・設定します
        /// </summary>
        public String COLUMN_2 { get; set; }

        /// <summary>
        /// カラム名３を取得・設定します
        /// </summary>
        public String COLUMN_3 { get; set; }

        /// <summary>
        /// カラム名４を取得・設定します
        /// </summary>
        public String COLUMN_4 { get; set; }

        /// <summary>
        /// カラム名５を取得・設定します
        /// </summary>
        public String COLUMN_5 { get; set; }

        /// <summary>
        /// CD1を取得・設定します
        /// </summary>
        public String CD_1 { get; set; }

        /// <summary>
        /// CD2を取得・設定します
        /// </summary>
        public String CD_2 { get; set; }

        /// <summary>
        /// CD3を取得・設定します
        /// </summary>
        public String CD_3 { get; set; }

        /// <summary>
        /// CD4を取得・設定します
        /// </summary>
        public String CD_4 { get; set; }

        /// <summary>
        /// CD5を取得・設定します
        /// </summary>
        public String CD_5 { get; set; }

        /// <summary>
        /// 項目名１を取得・設定します
        /// </summary>
        public String NAME_1 { get; set; }

        /// <summary>
        /// 項目名２を取得・設定します
        /// </summary>
        public String NAME_2 { get; set; }

        /// <summary>
        /// 項目名３を取得・設定します
        /// </summary>
        public String NAME_3 { get; set; }

        /// <summary>
        /// 項目名４を取得・設定します
        /// </summary>
        public String NAME_4 { get; set; }

        /// <summary>
        /// 項目名５を取得・設定します
        /// </summary>
        public String NAME_5 { get; set; }

        /// <summary>
        /// 正味重量を取得・設定します
        /// </summary>
        public Decimal NET_JYUURYOU { get; set; }

        /// <summary>
        /// 書式設定済みの正味重量を取得します
        /// </summary>
        public String FORMAT_NET_JYUURYOU { get { return this.NET_JYUURYOU.ToString(this.SYS_INFO.SYS_JYURYOU_FORMAT); } }

        /// <summary>
        /// 数量を取得・設定します
        /// </summary>
        public Decimal SUURYOU { get; set; }

        /// <summary>
        /// 書式設定済みの数量を取得します
        /// </summary>
        public String FORMAT_SUURYOU { get { return this.SUURYOU.ToString(this.SYS_INFO.SYS_SUURYOU_FORMAT); } }

        /// <summary>
        /// 単位CDを取得・設定します
        /// </summary>
        public String UNIT_CD { get; set; }

        /// <summary>
        /// 単位名を取得・設定します
        /// </summary>
        public String UNIT_NAME { get; set; }

        /// <summary>
        /// 金額を取得・設定します
        /// </summary>
        public Decimal KINGAKU { get; set; }

        /// <summary>
        /// 書式設定済みの金額を取得します
        /// </summary>
        public String FORMAT_KINGAKU { get { return this.KINGAKU.ToString("#,##0"); } }

        /// <summary>
        /// 集計１のキーを取得・設定します
        /// </summary>
        public String GROUP1_KEY { get; set; }

        /// <summary>
        /// 集計１の項目名を取得・設定します
        /// </summary>
        public String GROUP1_NAME { get; set; }

        /// <summary>
        /// 集計１の合計正味重量を取得します
        /// </summary>
        public Decimal GROUP1_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計１の合計正味重量を取得します
        /// </summary>
        public String FORMAT_GROUP1_NET_JYUURYOU_SUM { get { return this.GROUP1_NET_JYUURYOU_SUM.ToString(this.SYS_INFO.SYS_JYURYOU_FORMAT); } }

        /// <summary>
        /// 集計１の合計金額を取得・設定します
        /// </summary>
        public Decimal GROUP1_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計１の合計金額を取得します
        /// </summary>
        public String FORMAT_GROUP1_KINGAKU_SUM { get { return this.GROUP1_KINGAKU_SUM.ToString("#,##0"); } }

        /// <summary>
        /// 集計２のキーを取得・設定します
        /// </summary>
        public String GROUP2_KEY { get; set; }

        /// <summary>
        /// 集計２の項目名を取得・設定します
        /// </summary>
        public String GROUP2_NAME { get; set; }

        /// <summary>
        /// 集計２の合計正味重量を取得します
        /// </summary>
        public Decimal GROUP2_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計２の合計正味重量を取得します
        /// </summary>
        public String FORMAT_GROUP2_NET_JYUURYOU_SUM { get { return this.GROUP2_NET_JYUURYOU_SUM.ToString(this.SYS_INFO.SYS_JYURYOU_FORMAT); } }

        /// <summary>
        /// 集計２の合計金額を取得・設定します
        /// </summary>
        public Decimal GROUP2_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計２の合計金額を取得します
        /// </summary>
        public String FORMAT_GROUP2_KINGAKU_SUM { get { return this.GROUP2_KINGAKU_SUM.ToString("#,##0"); } }

        /// <summary>
        /// 集計３のキーを取得・設定します
        /// </summary>
        public String GROUP3_KEY { get; set; }

        /// <summary>
        /// 集計３の項目名を取得・設定します
        /// </summary>
        public String GROUP3_NAME { get; set; }

        /// <summary>
        /// 集計３の合計正味重量を取得します
        /// </summary>
        public Decimal GROUP3_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計３の合計正味重量を取得します
        /// </summary>
        public String FORMAT_GROUP3_NET_JYUURYOU_SUM { get { return this.GROUP3_NET_JYUURYOU_SUM.ToString(this.SYS_INFO.SYS_JYURYOU_FORMAT); } }

        /// <summary>
        /// 集計３の合計金額を取得・設定します
        /// </summary>
        public Decimal GROUP3_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計３の合計金額を取得します
        /// </summary>
        public String FORMAT_GROUP3_KINGAKU_SUM { get { return this.GROUP3_KINGAKU_SUM.ToString("#,##0"); } }

        /// <summary>
        /// 集計４のキーを取得・設定します
        /// </summary>
        public String GROUP4_KEY { get; set; }

        /// <summary>
        /// 集計４の項目名を取得・設定します
        /// </summary>
        public String GROUP4_NAME { get; set; }

        /// <summary>
        /// 集計４の合計正味重量を取得します
        /// </summary>
        public Decimal GROUP4_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計４の合計正味重量を取得します
        /// </summary>
        public String FORMAT_GROUP4_NET_JYUURYOU_SUM { get { return this.GROUP4_NET_JYUURYOU_SUM.ToString(this.SYS_INFO.SYS_JYURYOU_FORMAT); } }

        /// <summary>
        /// 集計４の合計金額を取得・設定します
        /// </summary>
        public Decimal GROUP4_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計４の合計金額を取得します
        /// </summary>
        public String FORMAT_GROUP4_KINGAKU_SUM { get { return this.GROUP4_KINGAKU_SUM.ToString("#,##0"); } }

        /// <summary>
        /// 総合計正味重量を取得・設定します
        /// </summary>
        public Decimal ALL_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの総合計正味重量を取得します
        /// </summary>
        public String FORMAT_ALL_NET_JYUURYOU_SUM { get { return this.ALL_NET_JYUURYOU_SUM.ToString(this.SYS_INFO.SYS_JYURYOU_FORMAT); } }

        /// <summary>
        /// 総合計金額を取得・設定します
        /// </summary>
        public Decimal ALL_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの総合計金額を取得します
        /// </summary>
        public String FORMAT_ALL_KINGAKU_SUM { get { return this.ALL_KINGAKU_SUM.ToString("#,##0"); } }
    }
}
