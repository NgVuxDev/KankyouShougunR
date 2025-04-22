using System;
using System.Data.SqlTypes;

namespace r_framework.Entity
{
    /// <summary>
    /// 電子申請明細承認否認エンティティ
    /// </summary>
    public class T_DENSHI_SHINSEI_DETAIL_ACTION : SuperEntity
    {
        /// <summary>
        /// 明細システムIDを取得・設定します
        /// </summary>
        public SqlInt64 DETAIL_SYSTEM_ID { get; set; }

        /// <summary>
        /// 枝番を取得・設定します
        /// </summary>
        public SqlInt32 SEQ { get; set; }

        /// <summary>
        /// 申請番号を取得・設定します
        /// </summary>
        public SqlInt64 SHINSEI_NUMBER { get; set; }

        /// <summary>
        /// 行番号を取得・設定します
        /// </summary>
        public SqlInt16 ROW_NO { get; set; }

        /// <summary>
        /// 確認日を取得・設定します
        /// </summary>
        public SqlDateTime CHECK_DATE { get; set; }

        /// <summary>
        /// 決裁フラグを取得・設定します
        /// </summary>
        public SqlInt16 ACTION_FLG { get; set; }

        /// <summary>
        /// 決済コメントを取得・設定します
        /// </summary>
        public String ACTION_COMMENT { get; set; }

        /// <summary>
        /// 削除フラグを取得・設定します
        /// </summary>
        public SqlBoolean DELETE_FLG { get; set; }
    }
}
