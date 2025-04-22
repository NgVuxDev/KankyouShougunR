// $Id: DataDto.cs 66702 2015-12-07 02:59:30Z takeda $
using r_framework.Entity;

namespace GennyouritsuHoshu.Dto
{
    public class DataDto
    {
        public DataDto()
        {
            this.entity = new M_GENNYOURITSU();
            this.updateKey = new M_GENNYOURITSU();
        }

        public M_GENNYOURITSU entity;
        public M_GENNYOURITSU updateKey;
    }
}
