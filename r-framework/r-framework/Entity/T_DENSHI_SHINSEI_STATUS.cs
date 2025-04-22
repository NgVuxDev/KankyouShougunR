using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    /// <summary>
    /// 電子申請状態エンティティ
    /// </summary>
    public class T_DENSHI_SHINSEI_STATUS : SuperEntity
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
        /// 更新回数を取得・設定します
        /// </summary>
        public SqlInt16 UPDATE_NUM { get; set; }

        /// <summary>
        /// 申請状態CDを取得・設定します
        /// </summary>
        public SqlInt16 SHINSEI_STATUS_CD { get; set; }

        /// <summary>
        /// 申請状態を取得・設定します
        /// </summary>
        public String SHINSEI_STATUS { get; set; }

        /// <summary>
        /// 削除フラグを取得・設定します
        /// </summary>
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
