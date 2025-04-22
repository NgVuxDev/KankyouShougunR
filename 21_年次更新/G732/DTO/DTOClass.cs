using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.AnnualUpdates.AnnualUpdatesDEL
{
    internal class DTOClass
    {
        internal M_OLD_DATA_DEL OldDataDelEntity;
        public DTOClass()
        {
            this.OldDataDelEntity = new M_OLD_DATA_DEL();
        }
    }
}
