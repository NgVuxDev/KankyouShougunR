using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Entity
{
    /// <summary>
    /// 締処理済みかチェック用のデータ
    /// </summary>
    class SHIME_DATA
    {
        /// <summary>取引先CD</summary>
        public string TORIHIKISAKI_CD { get; set; }

        /// <summary>売上支払番号</summary>
        public SqlInt64 UR_SH_NUMBER { get; set; }

        /// <summary>請求番号</summary>
        public SqlInt64 SEIKYUU_NUMBER { get; set; }

        /// <summary>精算番号</summary>
        public SqlInt64 SEISAN_NUMBER { get; set; }
    }
}
