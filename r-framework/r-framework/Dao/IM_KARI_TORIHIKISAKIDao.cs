using Seasar.Dao.Attrs;
using r_framework.Entity;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    /// �������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_KARI_TORIHIKISAKI))]
    public interface IM_KARI_TORIHIKISAKIDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_TORIHIKISAKI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_TORIHIKISAKI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�����Ƃɍ폜����Ă��Ȃ��������̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/ AND DELETE_FLG = 0")]
        M_KARI_TORIHIKISAKI GetDataByCd(string cd);

        List<M_KARI_TORIHIKISAKI> GetKariTorihikisakiList(M_KARI_TORIHIKISAKI entity);
    }
}