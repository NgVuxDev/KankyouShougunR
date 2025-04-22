using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Collections.Generic;

namespace r_framework.Dao
{
    /// <summary>
    /// ������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_KARI_GENBA))]
    public interface IM_KARI_GENBADao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_GENBA data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_GENBA data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_KARI_GENBA data);

        /// <summary>
        /// �ƎҁA����R�[�h�����ɍ폜����Ă��Ȃ������擾
        /// </summary>
        /// <parameparam name="genbaCd">����R�[�h</parameparam>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/ AND DELETE_FLG = 0")]
        M_KARI_GENBA GetKariGenbaData(string gyoushaCd, string genbaCd);

        List<M_KARI_GENBA> GetKariGenbaList(M_KARI_GENBA entity);
    }
}