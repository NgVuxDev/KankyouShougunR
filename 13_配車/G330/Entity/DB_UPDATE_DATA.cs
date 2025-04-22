using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Entity
{
    /// <summary>
    /// 挿入データ
    /// </summary>
    [Serializable()]
    class DB_UPDATE_DATA
    {
        /// <summary>
        /// Insertデータ（売上／支払：入力テーブル）
        /// </summary>
        public T_UR_SH_ENTRY InsertEntry { get; set; }
        /// <summary>
        /// Insertデータ（売上／支払：明細テーブル）
        /// </summary>
        public List<T_UR_SH_DETAIL> InsertDetailList { get; set; }
        /// <summary>
        /// Updateデータ（定期実績：入力テーブル）
        /// </summary>
        public List<T_TEIKI_JISSEKI_ENTRY> UpdateEntryList { get; set; }
        /// <summary>
        /// Updateデータ（定期実績：明細テーブル）
        /// </summary>
        public List<T_TEIKI_JISSEKI_DETAIL> UpdateDetailList { get; set; }

        /// <summary>
        /// Deleteデータ (売上/支払：入力テーブル)
        /// </summary>
        public T_UR_SH_ENTRY DeleteUrShEntry { get; set; }
    }
}
