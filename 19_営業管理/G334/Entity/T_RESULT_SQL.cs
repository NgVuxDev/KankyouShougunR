using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Entity
{
    /// <summary>
    /// 取引履歴一覧(SQL実行後の一行分)
    /// </summary>
    [Serializable()]
    public class T_RESULT_SQL : SuperEntity
    {
        public string TORIHIKISAKI_CD { get; set; }
        public string TORIHIKISAKI_NAME_RYAKU { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GYOUSHA_NAME_RYAKU { get; set; }
        public string GENBA_CD { get; set; }
        public string GENBA_NAME_RYAKU { get; set; }
        public SqlDateTime DENPYOU_DATE { get; set; }
        public int DENPYOU_KBN { get; set; }
        public SqlInt64 DENPYOU_NUMBER { get; set; }
        public SqlBoolean DAINOU_FLG { get; set; }
    }
}

