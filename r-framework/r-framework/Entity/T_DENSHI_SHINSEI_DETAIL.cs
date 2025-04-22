using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    /// <summary>
    /// 電子申請明細エンティティ
    /// </summary>
    public class T_DENSHI_SHINSEI_DETAIL : SuperEntity
    {
        /// <summary>
        /// システムIDを取得・設定します
        /// </summary>
        public SqlInt64 SYSTEM_ID { get; set; }

        /// <summary>
        /// 枝番を取得・設定します
        /// </summary>
        public SqlInt32 SEQ { get; set; }

        /// <summary>
        /// 明細システムIDを取得・設定します
        /// </summary>
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }

        /// <summary>
        /// 申請番号を取得・設定します
        /// </summary>
        public SqlInt64 SHINSEI_NUMBER { get; set; }

        /// <summary>
        /// 行番号を取得・設定します
        /// </summary>
        public SqlInt16 ROW_NO { get; set; }

        /// <summary>
        /// 部署CDを取得・設定します
        /// </summary>
        public String BUSHO_CD { get; set; }

        /// <summary>
        /// 社員CDを取得・設定します
        /// </summary>
        public String SHAIN_CD { get; set; }

        /// <summary>
        /// 代理決裁者CDを取得・設定します
        /// </summary>
        public String DAIRI_KESSAI_SHAIN_CD { get; set; }
    }
}
