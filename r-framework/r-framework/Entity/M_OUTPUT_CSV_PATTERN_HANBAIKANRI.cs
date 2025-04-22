using System.Data.SqlTypes;

namespace r_framework.Entity
{
    public class M_OUTPUT_CSV_PATTERN_HANBAIKANRI : SuperEntity
    {
        public SqlInt64 SYSTEM_ID { get; set; }
        public SqlInt32 SEQ { get; set; }

        /// <summary>
        /// 伝票種類_受入
        /// </summary>
        public SqlBoolean DENPYOU_SHURUI_UKEIRE { get; set; }

        /// <summary>
        /// 伝票種類_出荷
        /// </summary>
        public SqlBoolean DENPYOU_SHURUI_SHUKKA { get; set; }

        /// <summary>
        /// 伝票種類_売上/支払
        /// </summary>
        public SqlBoolean DENPYOU_SHURUI_UR_SH { get; set; }

        /// <summary>
        /// 伝票種類_代納
        /// </summary>
        public SqlBoolean DENPYOU_SHURUI_DAINOU { get; set; }

        /// <summary>
        /// 伝票区分_売上
        /// </summary>
        public SqlBoolean DENPYOU_KBN_URIAGE { get; set; }

        /// <summary>
        /// 伝票区分_支払
        /// </summary>
        public SqlBoolean DENPYOU_KBN_SHIHARAI { get; set; }

        /// <summary>
        /// 取引区分
        /// </summary>
        public SqlInt16 TORIHIKI_KBN { get; set; }

        /// <summary>
        /// 確定区分
        /// </summary>
        public SqlInt16 KAKUTEI_KBN { get; set; }

        /// <summary>
        /// 締処理状況
        /// </summary>
        public SqlInt16 SHIME_SHORI_JOUKYOU { get; set; }
    }
}