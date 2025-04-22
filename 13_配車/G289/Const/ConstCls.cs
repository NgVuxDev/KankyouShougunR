// $Id: ConstCls.cs 25413 2014-07-11 10:45:11Z j-kikuchi $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class ConstCls
    {
        /// <summary>
        /// Kgの単位CD固定：３
        /// </summary>
        public static readonly string mKg_UnitCdKg = "3";
        /// <summary>
        /// 定期実績明細のカラム名
        /// </summary>
        public class DetailColName
        {
            public const string DELETE_FLG = "DELETE_FLG";

            public const string KAKUTEI_FLG = "KAKUTEI_FLG";
            public const string INPUT_KBN = "INPUT_KBN";
            public const string INPUT_KBN_NAME = "INPUT_KBN_NAME";
            public const string ROUND_NO = "ROUND_NO";
            public const string ROW_NUMBER = "ROW_NUMBER";
            public const string GYOUSHA_CD = "GYOUSHA_CD";
            public const string GYOUSHA_NAME_RYAKU = "GYOUSHA_NAME_RYAKU";
            public const string GENBA_CD = "GENBA_CD";
            public const string GENBA_NAME_RYAKU = "GENBA_NAME_RYAKU";
            public const string HINMEI_CD = "HINMEI_CD";
            public const string HINMEI_NAME_RYAKU = "HINMEI_NAME_RYAKU";
            public const string DENPYOU_KBN_CD_NM = "DENPYOU_KBN_CD_NM";
            public const string SUURYOU = "SUURYOU";
            public const string UNIT_CD = "UNIT_CD";
            public const string UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";
            public const string KANSANCHI = "KANSANCHI";
            public const string KANSAN_SUURYOU = "KANSAN_SUURYOU";
            public const string KANSAN_UNIT_CD = "KANSAN_UNIT_CD";
            public const string UNITKANSAN_NAME = "UNITKANSAN_NAME";
            public const string ANBUN_SUURYOU = "ANBUN_SUURYOU";
            public const string NIOROSHI_NUMBER_DETAIL = "NIOROSHI_NUMBER_DETAIL";

            public const string KEIYAKU_KBN = "KEIYAKU_KBN";
            public const string KEIYAKU_KBN_NM = "KEIYAKU_KBN_NM";
            public const string TSUKIGIME_KBN = "TSUKIGIME_KBN";
            public const string TSUKIGIME_KBN_NM = "TSUKIGIME_KBN_NM";

            public const string SHUUSHUU_TIME = "SHUUSHUU_TIME";
            public const string SHUUSHUU_HOUR = "SHUUSHUU_HOUR";
            public const string SHUUSHUU_MIN = "SHUUSHUU_MIN";
            public const string HINMEI_BIKOU = "HINMEI_BIKOU";

            public const string DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";
            public const string TIME_STAMP_DETAIL = "TIME_STAMP_DETAIL";

            public const string KAISHUU_BIKOU = "KAISHUU_BIKOU";

            public const string KANSAN_UNIT_MOBILE_OUTPUT_FLG = "KANSAN_UNIT_MOBILE_OUTPUT_FLG";
        }

        /// <summary>
        /// 定期実績荷卸のカラム名
        /// </summary>
        public class NioroshiColName
        {
            public const string NIOROSHI_NUMBER = "NIOROSHI_NUMBER";
            public const string NIOROSHI_GYOUSHA_CD = "NIOROSHI_GYOUSHA_CD";
            public const string NIOROSHI_GYOUSHA_NAME_RYAKU = "NIOROSHI_GYOUSHA_NAME_RYAKU";
            public const string NIOROSHI_GENBA_CD = "NIOROSHI_GENBA_CD";
            public const string NIOROSHI_GENBA_NAME_RYAKU = "NIOROSHI_GENBA_NAME_RYAKU";
            public const string NIOROSHI_RYOU = "NIOROSHI_RYOU";
            public const string HANNYUU_DATE = "HANNYUU_DATE";
            public const string HANNYUU_HOUR = "HANNYUU_HOUR";
            public const string HANNYUU_MIN = "HANNYUU_MIN";
            public const string TIME_STAMP_NIOROSHI = "TIME_STAMP_NIOROSHI";
        }
        public static readonly string INPUT_KBN_1 = "直接入力";
        public static readonly string INPUT_KBN_2 = "組込";
    }
}
