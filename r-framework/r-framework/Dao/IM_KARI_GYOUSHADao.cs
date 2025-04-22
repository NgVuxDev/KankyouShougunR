using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    /// ���Ǝ҃}�X�^Dao
    /// </summary>
    [Bean(typeof(M_KARI_GYOUSHA))]
    public interface IM_KARI_GYOUSHADao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_GYOUSHA data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_GYOUSHA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_GYOUSHA data);

        /// <summary>
        /// �R�[�h�����ɋƎ҃f�[�^���擾����
        /// </summary>
        /// <parameparam name="cd">�Ǝ҃R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*cd*/ AND DELETE_FLG = 0")]
        M_KARI_GYOUSHA GetDataByCd(string cd);

        List<M_KARI_GYOUSHA> GetKariGyoushaList(M_KARI_GYOUSHA entity);
    }
}