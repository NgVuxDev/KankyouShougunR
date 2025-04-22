using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace  Shougun.Core.PayByProxy.ShukeiHyoJokenShiteiPoppup
{
    public class ShukeiHyoJokenShiteiPoppupDTO
    {
        /// <summary>
        /// 拠点マスタ／拠点コード
        /// </summary>
        public SqlInt16 KYOTEN_CD { get; set; }

        /// <summary>
        /// 取引先マスタ／取引先コード
        /// </summary>
        public String TORIHIKISAKI_CD { get; set; }

        public String GYOUSHA_CD { get; set; }

        public String GENBA_CD { get; set; }

        public String HINMEI_CD { get; set; }

        public string CORP_NAME { get; set; }
        
        
    }        
}
