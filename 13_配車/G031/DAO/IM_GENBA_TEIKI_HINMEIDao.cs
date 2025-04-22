// $Id: IM_GENBA_TEIKI_HINMEIDao.cs 9436 2013-12-04 00:50:08Z sys_dev_23 $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    /// <summary>
    /// �������i���}�X�^Dao
    /// </summary>
    [Bean(typeof(M_GENBA_TEIKI_HINMEI))]
    public interface IM_GENBA_TEIKI_HINMEIDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
         [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENBA_TEIKI_HINMEI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENBA_TEIKI_HINMEI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GENBA_TEIKI_HINMEI data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_GENBA_TEIKI_HINMEI")]
        M_GENBA_TEIKI_HINMEI[] GetAllData();

        /// <summary>
        /// �������i���f�[�^�擾
        /// </summary>
        /// <parameparam name="data">��������</parameparam>
        [SqlFile("Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku.Sql.GetGenbaTeikiHinmeiData.sql")]
        M_GENBA_TEIKI_HINMEI[] GetDataForEntity(M_GENBA_TEIKI_HINMEI data);
    }
}