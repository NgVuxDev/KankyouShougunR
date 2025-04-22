// $Id: IM_KARI_GENBADao.cs 26123 2014-07-18 08:54:09Z ria_koec $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.BusinessManagement.GenbaKakunin.Dao
{
    /// <summary>
    /// ��������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_KARI_GENBA))]
    public interface IM_KARI_GENBADao : IS2Dao
    {
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_KARI_GENBA")]
        M_KARI_GENBA[] GetAllData();

        /// <summary>
        /// ����R�[�h�����Ɍ�������擾
        /// </summary>
        /// <param name="data"></param>
        [Query("GYOUSHA_CD = /*data.GYOUSHA_CD*/ and GENBA_CD = /*data.GENBA_CD*/")]
        M_KARI_GENBA GetDataByCd(M_KARI_GENBA data);
    }
}