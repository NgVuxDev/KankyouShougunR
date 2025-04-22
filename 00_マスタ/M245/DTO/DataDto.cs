// $Id: DataDto.cs 66702 2015-12-07 02:59:30Z takeda $
using r_framework.Entity;

namespace Shougun.Core.Master.ZaikoHiritsuHoshu.DTO
{
    public class DataDto
    {
        public DataDto()
        {
            this.entity = new M_ZAIKO_HIRITSU();
            this.updateKey = new M_ZAIKO_HIRITSU();
        }

        public M_ZAIKO_HIRITSU entity;
        public M_ZAIKO_HIRITSU updateKey;
    }
}
