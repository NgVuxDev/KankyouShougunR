// $Id: DTOCls.cs 8292 2013-11-26 07:02:41Z sys_dev_22 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.TeikiHaisyaIchiran
{
    internal class DTOClass
    {
        public string DAY_CD { get; set; }
        public string COURSE_NAME_CD { get; set; }
        public string GYOUSHA_CD { get; set; }
        public string GENBA_CD { get; set; }
        public string HINMEI_CD { get; set; }
    }
    public class SummaryKeyCode
    {
        public string HIDDEN_COLUMN_HAISHA_NUMBER { get; set; }
    }
}
