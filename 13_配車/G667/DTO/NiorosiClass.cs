// $Id: DTOCls.cs 8292 2013-11-26 07:02:41Z sys_dev_22 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Allocation.MobileJoukyouIchiran
{
    public class NiorosiClass
    {
        /// <summary>
        /// 配車番号
        /// </summary>
        public string TEIKI_HAISHA_NUMBER { get; set; }

        /// <summary>
        /// 荷降番号
        /// </summary>
        public string NIOROSHI_NUMBER { get; set; }

        /// <summary>
        /// 搬入シーケンシャルナンバー
        /// </summary>
        public SqlInt64 HANYU_SEQ_NO { get; set; }

    }
}
