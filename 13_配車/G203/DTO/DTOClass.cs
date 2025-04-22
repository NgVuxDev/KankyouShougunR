using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件 : DBフィールド名
        /// </summary>
        public String FieldName { get; set; }
        /// <summary>
        /// 検索条件 : 条件値
        /// </summary>
        public String ConditionValue { get; set; }
        /// <summary>
        /// 検索条件 : 業者CD
        /// </summary>
        public String gyoushaCd { get; set; }
        /// <summary>
        /// 検索条件 : 現場CD
        /// </summary>
        public String genbaCd { get; set; }
        /// <summary>
        /// 検索条件 : 検索日付
        /// </summary>
        public String searchDate { get; set; }
    }

    public class SearchDTOClass : SuperEntity
    {
        /// <summary>
        /// 検索条件 : 業者CD
        /// </summary>
        public String GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 業者名
        /// </summary>
        public String GYOUSHA_NAME_RYAKU { get; set; }
        /// <summary>
        /// 検索条件 : 現場CD
        /// </summary>
        public String GENBA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 現場名
        /// </summary>
        public String GENBA_NAME_RYAKU { get; set; }
    }

    public class NioRoShiDTO : SuperEntity
    {
        /// <summary>
        /// 検索条件 : 荷降業者CD
        /// </summary>
        public String NIOROSHI_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 荷降現場CD
        /// </summary>
        public String NIOROSHI_GENBA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 作業日
        /// </summary>
        public SqlDateTime SAGYOU_DATE { get; set; }
    }


}
