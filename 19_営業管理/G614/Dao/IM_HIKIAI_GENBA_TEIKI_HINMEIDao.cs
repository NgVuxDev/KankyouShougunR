// $Id: IM_HIKIAI_GENBA_TEIKI_HINMEIDao.cs 12286 2013-12-22 14:15:20Z gai $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.Dao
{
    /// <summary>
    /// ��������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA_TEIKI_HINMEI))]
    public interface IM_HIKIAI_GENBA_TEIKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// ��������Ɋ֘A����f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.BusinessManagement.GenbaKakunin.Sql.GetTeikiHinmeiDataSql.sql")]
        DataTable GetTeikiHinmeiData(M_HIKIAI_GENBA_TEIKI_HINMEI data);

    }
}