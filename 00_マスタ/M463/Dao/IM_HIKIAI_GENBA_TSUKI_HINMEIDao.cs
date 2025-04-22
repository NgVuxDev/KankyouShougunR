// $Id: IM_HIKIAI_GENBA_TSUKI_HINMEIDao.cs 12286 2013-12-22 14:15:20Z gai $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiGenbaHoshu.Dao
{
    /// <summary>
    /// ��������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_HIKIAI_GENBA_TSUKI_HINMEI))]
    public interface IM_HIKIAI_GENBA_TSUKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// ��������Ɋ֘A����f�[�^�擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetTsukiHinmeiDataSql.sql")]
        DataTable GetTsukiHinmeiData(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// ��������Ɋ֘A����f�[�^�\���̎擾���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�擾����DataTable</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.GetTsukiHinmeiStructSql.sql")]
        DataTable GetTsukiHinmeiStruct(M_HIKIAI_GENBA_TSUKI_HINMEI data);

        /// <summary>
        /// ��������Ɋ֘A����f�[�^�폜���s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns>�폜��������</returns>
        [SqlFile("Shougun.Core.Master.HikiaiGenbaHoshu.Sql.DeleteTsukiHinmeiSql.sql")]
        int DeleteTsukiHinmei(M_HIKIAI_GENBA_TSUKI_HINMEI data);
    }
}