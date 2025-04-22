// $Id: DataDto.cs 66702 2015-12-07 02:59:30Z takeda $
using r_framework.Entity;

namespace KongouHaikibutsuHoshu.Dto
{
    public class DataDto
    {
        public DataDto()
        {
            this.entity = new M_KONGOU_HAIKIBUTSU();
            this.updateKey = new M_KONGOU_HAIKIBUTSU();
        }

        public M_KONGOU_HAIKIBUTSU entity;
        public M_KONGOU_HAIKIBUTSU updateKey;
    }
}
