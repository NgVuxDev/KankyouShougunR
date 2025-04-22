using System;
using System.Data.SqlTypes;
using r_framework.Entity;

namespace Shougun.Core.Allocation.TeikiJissekiHoukoku.Dto
{
    /// <summary>
    /// 定期実績CSV出力DTO
    /// </summary>
    public class TeikiJissekiHoukokuDto : T_TEIKI_JISSEKI_DETAIL
    {
        /// <summary>
        /// 市区町村グループ
        /// </summary>
        public class ShikuchousonGroup
        {
            /// <summary>
            /// 市区町村CD
            /// </summary>
            public string SHIKUCHOUSON_CD { get; set; }

            /// <summary>
            /// 市区町村略称
            /// </summary>
            public string SHIKUCHOUSON_NAME_RYAKU { get; set; }
        }

        /// <summary>
        /// 実績分類グループ
        /// </summary>
        public class JissekiBunruiGroup
        {
            /// <summary>
            /// 実績分類CD
            /// </summary>
            public string JISSEKI_BUNRUI_CD { get; set; }

            /// <summary>
            /// 実績分類名
            /// </summary>
            public string JISSEKI_BUNRUI_NAME { get; set; }
        }

        /// <summary>
        /// 品名・単位グループ
        /// </summary>
        public class HinmeiUnitGroup
        {
            /// <summary>
            /// 実績分類CD
            /// </summary>
            public string JISSEKI_BUNRUI_CD { get; set; }

            /// <summary>
            /// 実績分類名
            /// </summary>
            public string JISSEKI_BUNRUI_NAME { get; set; }
            
            /// <summary>
            /// 計算後単位CD
            /// </summary>
            public SqlInt16 CALC_UNIT_CD { get; set; }

            /// <summary>
            /// 計算後単位略称
            /// </summary>
            public string CALC_UNIT_NAME_RYAKU { get; set; }

            /// <summary>
            /// 数量合計
            /// </summary>
            public decimal SUM_SUURYOU { get; set; }
        }

        /// <summary>
        /// 現場グループ
        /// </summary>
        public class GenbaGroup
        {
            /// <summary>
            /// 業者CD
            /// </summary>
            public string GYOUSHA_CD { get; set; }

            /// <summary>
            /// 表示用業者名
            /// </summary>
            public string DISP_DETAIL_GYOUSHA_NAME { get; set; }

            /// <summary>
            /// 現場CD
            /// </summary>
            public string GENBA_CD { get; set; }

            /// <summary>
            /// 表示用現場名
            /// </summary>
            public string DISP_DETAIL_GENBA_NAME { get; set; }

            /// <summary>
            /// 荷降業者CD
            /// </summary>
            public string NIOROSHI_GYOUSHA_CD { get; set; }

            /// <summary>
            /// 表示用荷降業者名
            /// </summary>
            public string DISP_NIOROSHI_GYOUSHA_NAME { get; set; }

            /// <summary>
            /// 荷降現場CD
            /// </summary>
            public string NIOROSHI_GENBA_CD { get; set; }

            /// <summary>
            /// 表示用荷降現場名
            /// </summary>
            public string DISP_NIOROSHI_GENBA_NAME { get; set; }
        }

        /// <summary>
        /// 荷降業者CD
        /// </summary>
        public string NIOROSHI_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 荷降現場CD
        /// </summary>
        public string NIOROSHI_GENBA_CD { get; set; }

        /// <summary>
        /// 拠点CD(検索条件)
        /// </summary>
        public SqlInt16 KYOTEN_CD { get; set; }

        /// <summary>
        /// 市区町村CD(検索条件)
        /// </summary>
        public string SHIKUCHOUSON_CD { get; set; }

        /// <summary>
        /// 市区町村略称
        /// </summary>
        public string SHIKUCHOUSON_NAME_RYAKU { get; set; }

        /// <summary>
        /// 期間From(検索条件)
        /// </summary>
        public SqlDateTime KIKAN_FROM { get; set; }

        /// <summary>
        /// 期間To(検索条件)
        /// </summary>
        public SqlDateTime KIKAN_TO { get; set; }

        /// <summary>
        /// 集計対象数量
        /// </summary>
        public int SHUUKEISUURYOU { get; set; }

        /// <summary>
        /// 作業日
        /// </summary>
        public SqlDateTime SAGYOU_DATE { get; set; }

        /// <summary>
        /// 明細業者名1
        /// </summary>
        public string DETAIL_GYOUSHA_NAME1 { get; set; }

        /// <summary>
        /// 明細業者名2
        /// </summary>
        public string DETAIL_GYOUSHA_NAME2 { get; set; }

        /// <summary>
        /// 明細現場名1
        /// </summary>
        public string DETAIL_GENBA_NAME1 { get; set; }

        /// <summary>
        /// 明細現場名2
        /// </summary>
        public string DETAIL_GENBA_NAME2 { get; set; }

        /// <summary>
        /// 荷降業者名1
        /// </summary>
        public string NIOROSHI_GYOUSHA_NAME1 { get; set; }

        /// <summary>
        /// 荷降業者名2
        /// </summary>
        public string NIOROSHI_GYOUSHA_NAME2 { get; set; }

        /// <summary>
        /// 荷降現場名1
        /// </summary>
        public string NIOROSHI_GENBA_NAME1 { get; set; }

        /// <summary>
        /// 荷降現場名2
        /// </summary>
        public string NIOROSHI_GENBA_NAME2 { get; set; }

        /// <summary>
        /// 品名
        /// </summary>
        public string HINMEI_NAME { get; set; }

        /// <summary>
        /// 実績分類CD
        /// </summary>
        public string JISSEKI_BUNRUI_CD { get; set; }

        /// <summary>
        /// 実績分類名
        /// </summary>
        public string JISSEKI_BUNRUI_NAME { get; set; }

        /// <summary>
        /// 単位略称
        /// </summary>
        public string UNIT_NAME_RYAKU { get; set; }

        /// <summary>
        /// 換算後単位略称
        /// </summary>
        public string KANSAN_UNIT_NAME_RYAKU { get; set; }

        /// <summary>
        /// 按分後単位CD
        /// </summary>
        public SqlInt16 ANBUN_UNIT_CD { get; set; }

        /// <summary>
        /// 按分後単位略称
        /// </summary>
        public string ANBUN_UNIT_NAME_RYAKU { get; set; }

        /// <summary>
        /// 表示用明細業者名
        /// </summary>
        public string DISP_DETAIL_GYOUSHA_NAME
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DETAIL_GYOUSHA_NAME2))
                {
                    return string.Format("{0}　{1}", this.DETAIL_GYOUSHA_NAME1, this.DETAIL_GYOUSHA_NAME2);
                }
                else
                {
                    return this.DETAIL_GYOUSHA_NAME1;
                }
            }
        }

        /// <summary>
        /// 表示用明細現場名
        /// </summary>
        public string DISP_DETAIL_GENBA_NAME
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DETAIL_GENBA_NAME2))
                {
                    return string.Format("{0}　{1}", this.DETAIL_GENBA_NAME1, this.DETAIL_GENBA_NAME2);
                }
                else
                {
                    return this.DETAIL_GENBA_NAME1;
                }
            }
        }

        /// <summary>
        /// 表示用荷降業者名
        /// </summary>
        public string DISP_NIOROSHI_GYOUSHA_NAME
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_NAME2))
                {
                    return string.Format("{0}　{1}", this.NIOROSHI_GYOUSHA_NAME1, this.NIOROSHI_GYOUSHA_NAME2);
                }
                else
                {
                    return this.NIOROSHI_GYOUSHA_NAME1;
                }
            }
        }

        /// <summary>
        /// 表示用荷降現場名
        /// </summary>
        public string DISP_NIOROSHI_GENBA_NAME
        {
            get
            {
                if (!string.IsNullOrEmpty(this.NIOROSHI_GENBA_NAME2))
                {
                    return string.Format("{0}　{1}", this.NIOROSHI_GENBA_NAME1, this.NIOROSHI_GENBA_NAME2);
                }
                else
                {
                    return this.NIOROSHI_GENBA_NAME1;
                }
            }
        }

        /// <summary>
        /// 表示用品名
        /// </summary>
        public string DISP_HINMEI_NAME
        {
            get
            {
                //************************************************************************
                // 現場別で表示名が異なると、集計不可能となる為、印字名は表示しない。
                //************************************************************************
                //if (!string.IsNullOrEmpty(this.PRINT_STRING))
                //{
                //    return this.PRINT_STRING;
                //}
                //else
                {
                    return this.HINMEI_NAME;
                }
            }
        }

        /// <summary>
        /// 計算後数量
        /// </summary>
        public decimal CALC_SUURYOU
        {
            get
            {
                if (this.SHUUKEISUURYOU == 1)
                {
                    if (!this.UNIT_CD.IsNull && this.UNIT_CD.Value == 3)
                    {
                        return this.SUURYOU.IsNull ? 0 : this.SUURYOU.Value;
                    }
                    else
                    {
                        return this.KANSAN_SUURYOU.IsNull ? 0 : this.KANSAN_SUURYOU.Value;
                    }
                }
                else
                {
                    return this.ANBUN_SUURYOU.IsNull ? 0 : this.ANBUN_SUURYOU.Value;
                }
            }
        }

        /// <summary>
        /// 計算後単位CD
        /// </summary>
        public SqlInt16 CALC_UNIT_CD
        {
            get
            {
                if (this.SHUUKEISUURYOU == 1)
                {
                    if (!this.UNIT_CD.IsNull && this.UNIT_CD.Value == 3)
                    {
                        return this.UNIT_CD;
                    }
                    else
                    {
                        return this.KANSAN_UNIT_CD;
                    }
                }
                else
                {
                    return this.ANBUN_UNIT_CD;
                }
            }
        }

        /// <summary>
        /// 計算後単位略称
        /// </summary>
        public string CALC_UNIT_NAME_RYAKU
        {
            get
            {
                if (this.SHUUKEISUURYOU == 1)
                {
                    if (!this.UNIT_CD.IsNull && this.UNIT_CD.Value == 3)
                    {
                        return this.UNIT_NAME_RYAKU;
                    }
                    else
                    {
                        return this.KANSAN_UNIT_NAME_RYAKU;
                    }
                }
                else
                {
                    return this.ANBUN_UNIT_NAME_RYAKU;
                }
            }
        }
    }
}
