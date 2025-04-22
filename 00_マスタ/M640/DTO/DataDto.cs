// $Id: DataDto.cs 66702 2015-12-07 02:59:30Z takeda $
using r_framework.Entity;

namespace Shougun.Core.Master.UnchinTankaHoshu.Dto
{
    public class DataDto
    {
        public DataDto()
        {
            this.entity = new M_UNCHIN_TANKA();
            this.updateKey = new M_UNCHIN_TANKA();
        }

        public M_UNCHIN_TANKA entity;
        public M_UNCHIN_TANKA updateKey;
    }
}
