// $Id: DataDto.cs 12324 2013-12-23 12:55:25Z ishibashi $
using r_framework.Entity;

namespace KansanHoshu.Dto
{
    public class DataDto
    {
        public DataDto()
        {
            this.entity = new M_KANSAN();
            this.updateKey = new M_KANSAN();
        }

        public M_KANSAN entity;
        public M_KANSAN updateKey;
    }
}
