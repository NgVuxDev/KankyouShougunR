// $Id: DataDto.cs 12324 2013-12-23 12:55:25Z ishibashi $
using r_framework.Entity;

namespace ManifestKansanHoshu.Dto
{
    public class DataDto
    {
        public DataDto()
        {
            this.entity = new M_MANIFEST_KANSAN();
            this.updateKey = new M_MANIFEST_KANSAN();
        }

        public M_MANIFEST_KANSAN entity;
        public M_MANIFEST_KANSAN updateKey;
    }
}
