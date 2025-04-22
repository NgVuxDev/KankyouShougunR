using r_framework.Entity;

namespace Shougun.Core.Carriage.UnchinShukeiHyo
{
    /// <summary>
    /// 運賃集計表帳票DTOクラス
    /// </summary>
    public class UnchinShukeiHyoReportDto
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnchinShukeiHyoReportDto()
        {
            this.Format = null;
            this.CORP_INFO = null;
        }

        /// <summary>
        /// 自社情報を取得・設定します
        /// </summary>
        internal M_CORP_INFO CORP_INFO { private get; set; }

        internal UnchinShukeiHyoLogic.Format Format { private get; set; }

        /// <summary>
        /// 帳票タイトルを取得・設定します
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>
        /// 自社名を取得します
        /// </summary>
        public string JISHA { get { return this.CORP_INFO.CORP_RYAKU_NAME; } }

        /// <summary>
        /// 拠点名を取得・設定します
        /// </summary>
        public string KYOTEN { get; set; }

        /// <summary>
        /// 発行年月日を取得・設定します
        /// </summary>
        public string HAKKOU_DATE { get; set; }

        /// <summary>
        /// 条件欄１を取得・設定します
        /// </summary>
        public string JOUKEN_1 { get; set; }

        /// <summary>
        /// 条件欄２を取得・設定します
        /// </summary>
        public string JOUKEN_2 { get; set; }

        /// <summary>
        /// カラム名１を取得・設定します
        /// </summary>
        public string COLUMN_1 { get; set; }

        /// <summary>
        /// カラム名２を取得・設定します
        /// </summary>
        public string COLUMN_2 { get; set; }

        /// <summary>
        /// カラム名３を取得・設定します
        /// </summary>
        public string COLUMN_3 { get; set; }

        /// <summary>
        /// カラム名４を取得・設定します
        /// </summary>
        public string COLUMN_4 { get; set; }

        /// <summary>
        /// カラム名５を取得・設定します
        /// </summary>
        public string COLUMN_5 { get; set; }

        /// <summary>
        /// CD1を取得・設定します
        /// </summary>
        public string CD_1 { get; set; }

        /// <summary>
        /// CD2を取得・設定します
        /// </summary>
        public string CD_2 { get; set; }

        /// <summary>
        /// CD3を取得・設定します
        /// </summary>
        public string CD_3 { get; set; }

        /// <summary>
        /// CD4を取得・設定します
        /// </summary>
        public string CD_4 { get; set; }

        /// <summary>
        /// CD5を取得・設定します
        /// </summary>
        public string CD_5 { get; set; }

        /// <summary>
        /// 項目名１を取得・設定します
        /// </summary>
        public string NAME_1 { get; set; }

        /// <summary>
        /// 項目名２を取得・設定します
        /// </summary>
        public string NAME_2 { get; set; }

        /// <summary>
        /// 項目名３を取得・設定します
        /// </summary>
        public string NAME_3 { get; set; }

        /// <summary>
        /// 項目名４を取得・設定します
        /// </summary>
        public string NAME_4 { get; set; }

        /// <summary>
        /// 項目名５を取得・設定します
        /// </summary>
        public string NAME_5 { get; set; }

        /// <summary>
        /// 正味重量を取得・設定します
        /// </summary>
        internal decimal? NET_JYUURYOU { get; set; }

        /// <summary>
        /// 書式設定済みの正味重量を取得します
        /// </summary>
        public string FORMAT_NET_JYUURYOU
        {
            get
            {
                if (this.NET_JYUURYOU.HasValue)
                {
                    return this.NET_JYUURYOU.Value.ToString(this.Format.JYUURYOU_FORMAT);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 数量を取得・設定します
        /// </summary>
        internal decimal? SUURYOU { get; set; }

        /// <summary>
        /// 書式設定済みの数量を取得します
        /// </summary>
        public string FORMAT_SUURYOU
        {
            get
            {
                if (this.SUURYOU.HasValue)
                {
                    return this.SUURYOU.Value.ToString(this.Format.SUURYOU_FORMAT);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 単位CDを取得・設定します
        /// </summary>
        public string UNIT_CD { get; set; }

        /// <summary>
        /// 単位名を取得・設定します
        /// </summary>
        public string UNIT_NAME { get; set; }

        /// <summary>
        /// 金額を取得・設定します
        /// </summary>
        internal decimal? KINGAKU { get; set; }

        /// <summary>
        /// 書式設定済みの金額を取得します
        /// </summary>
        public string FORMAT_KINGAKU
        {
            get
            {
                if (this.KINGAKU.HasValue)
                {
                    return this.KINGAKU.Value.ToString(this.Format.KINGAKU_FORMAT);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計１のキーを取得・設定します
        /// </summary>
        public string GROUP1_KEY { get; set; }

        /// <summary>
        /// 集計１の項目名を取得・設定します
        /// </summary>
        public string GROUP1_NAME { get; set; }

        /// <summary>
        /// 集計１の合計正味重量を取得します
        /// </summary>
        internal decimal? GROUP1_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計１の合計正味重量を取得します
        /// </summary>
        public string FORMAT_GROUP1_NET_JYUURYOU_SUM
        {
            get
            {
                if (this.GROUP1_NET_JYUURYOU_SUM.HasValue)
                {
                    return this.GROUP1_NET_JYUURYOU_SUM.Value.ToString(this.Format.ZERO_FORMAT_DECIMAL);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計１の合計金額を取得・設定します
        /// </summary>
        internal decimal? GROUP1_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計１の合計金額を取得します
        /// </summary>
        public string FORMAT_GROUP1_KINGAKU_SUM
        {
            get
            {
                if (this.GROUP1_KINGAKU_SUM.HasValue)
                {
                    return this.GROUP1_KINGAKU_SUM.Value.ToString(this.Format.ZERO_FORMAT_INTEGER);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計２のキーを取得・設定します
        /// </summary>
        public string GROUP2_KEY { get; set; }

        /// <summary>
        /// 集計２の項目名を取得・設定します
        /// </summary>
        public string GROUP2_NAME { get; set; }

        /// <summary>
        /// 集計２の合計正味重量を取得します
        /// </summary>
        internal decimal? GROUP2_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計２の合計正味重量を取得します
        /// </summary>
        public string FORMAT_GROUP2_NET_JYUURYOU_SUM
        {
            get
            {
                if (this.GROUP2_NET_JYUURYOU_SUM.HasValue)
                {
                    return this.GROUP2_NET_JYUURYOU_SUM.Value.ToString(this.Format.ZERO_FORMAT_DECIMAL);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計２の合計金額を取得・設定します
        /// </summary>
        internal decimal? GROUP2_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計２の合計金額を取得します
        /// </summary>
        public string FORMAT_GROUP2_KINGAKU_SUM
        {
            get
            {
                if (this.GROUP2_KINGAKU_SUM.HasValue)
                {
                    return this.GROUP2_KINGAKU_SUM.Value.ToString(this.Format.ZERO_FORMAT_INTEGER);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計３のキーを取得・設定します
        /// </summary>
        public string GROUP3_KEY { get; set; }

        /// <summary>
        /// 集計３の項目名を取得・設定します
        /// </summary>
        public string GROUP3_NAME { get; set; }

        /// <summary>
        /// 集計３の合計正味重量を取得します
        /// </summary>
        internal decimal? GROUP3_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計３の合計正味重量を取得します
        /// </summary>
        public string FORMAT_GROUP3_NET_JYUURYOU_SUM
        {
            get
            {
                if (this.GROUP3_NET_JYUURYOU_SUM.HasValue)
                {
                    return this.GROUP3_NET_JYUURYOU_SUM.Value.ToString(this.Format.ZERO_FORMAT_DECIMAL);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計３の合計金額を取得・設定します
        /// </summary>
        internal decimal? GROUP3_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計３の合計金額を取得します
        /// </summary>
        public string FORMAT_GROUP3_KINGAKU_SUM
        {
            get
            {
                if (this.GROUP3_KINGAKU_SUM.HasValue)
                {
                    return this.GROUP3_KINGAKU_SUM.Value.ToString(this.Format.ZERO_FORMAT_INTEGER);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計４のキーを取得・設定します
        /// </summary>
        public string GROUP4_KEY { get; set; }

        /// <summary>
        /// 集計４の項目名を取得・設定します
        /// </summary>
        public string GROUP4_NAME { get; set; }

        /// <summary>
        /// 集計４の合計正味重量を取得します
        /// </summary>
        internal decimal? GROUP4_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計４の合計正味重量を取得します
        /// </summary>
        public string FORMAT_GROUP4_NET_JYUURYOU_SUM
        {
            get
            {
                if (this.GROUP4_NET_JYUURYOU_SUM.HasValue)
                {
                    return this.GROUP4_NET_JYUURYOU_SUM.Value.ToString(this.Format.ZERO_FORMAT_DECIMAL);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 集計４の合計金額を取得・設定します
        /// </summary>
        internal decimal? GROUP4_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの集計４の合計金額を取得します
        /// </summary>
        public string FORMAT_GROUP4_KINGAKU_SUM
        {
            get
            {
                if (this.GROUP4_KINGAKU_SUM.HasValue)
                {
                    return this.GROUP4_KINGAKU_SUM.Value.ToString(this.Format.ZERO_FORMAT_INTEGER);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 総合計正味重量を取得・設定します
        /// </summary>
        internal decimal? ALL_NET_JYUURYOU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの総合計正味重量を取得します
        /// </summary>
        public string FORMAT_ALL_NET_JYUURYOU_SUM
        {
            get
            {
                if (this.ALL_NET_JYUURYOU_SUM.HasValue)
                {
                    return this.ALL_NET_JYUURYOU_SUM.Value.ToString(this.Format.ZERO_FORMAT_DECIMAL);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// 総合計金額を取得・設定します
        /// </summary>
        internal decimal? ALL_KINGAKU_SUM { get; set; }

        /// <summary>
        /// 書式設定済みの総合計金額を取得します
        /// </summary>
        public string FORMAT_ALL_KINGAKU_SUM
        {
            get
            {
                if (this.ALL_KINGAKU_SUM.HasValue)
                {
                    return this.ALL_KINGAKU_SUM.Value.ToString(this.Format.ZERO_FORMAT_INTEGER);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
