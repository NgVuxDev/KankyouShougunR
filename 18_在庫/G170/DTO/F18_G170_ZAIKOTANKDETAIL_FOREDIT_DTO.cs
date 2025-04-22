using System.Data.SqlTypes;
using System.Collections.Generic;
using Shougun.Core.Stock.ZaikoShimeSyori.Entity;

namespace r_framework.Entity
{
    /// <summary>
    /// 編集用≪在庫締明細　T_ZAIKO_TANK_DETAIL≫データ
    /// </summary>
    public class F18_G170_ZAIKOTANKDETAIL_FOREDIT_DTO
    {
        /// <summary>
        /// 先月情報
        /// </summary>
        public ShimeInfo prewMonthInfo { get; set; }

        /// <summary>
        /// レコードのkey部情報
        /// </summary>
        /// <summary>
        /// システムID
        /// </summary>
        public SqlInt64 SYSTEM_ID { get; set; }
        /// <summary>
        /// 行番号
        /// </summary>
        public SqlInt32 ROW_NO { get; set; }
        /// <summary>
        /// 在庫品名コード
        /// </summary>
        public string ZAIKO_HINMEI_CD { get; set; }

        /// <summary>
        /// レコードの計算用情報部
        /// </summary>
        public List<ShimeTargetInfo> calculationList { get; set; }
    }
}