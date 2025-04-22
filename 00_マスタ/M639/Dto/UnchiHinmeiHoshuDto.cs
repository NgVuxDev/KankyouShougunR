using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Master.UnchiHinmeiHoshu.Dto
{
    public class UnchiHinmeiHoshuDto
    {
        //public M_UNIT UnitSearchString { get; set; }
        public M_UNCHIN_HINMEI HinmeiSearchString { get; set; }

        internal void GetHashCode(string p, int densyuKbnCd)
        {
            throw new NotImplementedException();
        }
    }
}
