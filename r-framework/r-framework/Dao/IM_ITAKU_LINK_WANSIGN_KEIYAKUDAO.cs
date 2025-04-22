using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �ϑ��_��WANSIGN�A�g
    /// </summary>
    [Bean(typeof(M_ITAKU_LINK_WANSIGN_KEIYAKU))]
    public interface IM_ITAKU_LINK_WANSIGN_KEIYAKUDAO : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_LINK_WANSIGN_KEIYAKU data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_LINK_WANSIGN_KEIYAKU data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("WANSIGN_SYSTEM_ID = /*wanSignSystemId*/ AND SYSTEM_ID = /*systemId*/")]
        M_ITAKU_LINK_WANSIGN_KEIYAKU GetDataBySystemId(long wanSignSystemId, long systemId);

        /// <summary>
        /// �ϑ��_��WANSIGN�A�g�i�V�e�[�u���j����WANSIGN_�V�X�e��ID�@�ɕR�Â������폜�@
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Sql("DELETE FROM M_ITAKU_LINK_WANSIGN_KEIYAKU WHERE WANSIGN_SYSTEM_ID = /*wanSignSystemId*/ AND SYSTEM_ID = /*systemId*/")]
        int DeleteBySystemId(long wanSignSystemId, long systemId);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("WANSIGN_SYSTEM_ID = /*wanSignSystemId*/")]
        M_ITAKU_LINK_WANSIGN_KEIYAKU[] GetDataByWanSignSystemId(long wanSignSystemId);

        /// <summary>
        /// �ϑ��_��WANSIGN�A�g�i�V�e�[�u���j����WANSIGN_�V�X�e��ID�@�ɕR�Â������폜�@
        /// </summary>
        /// <param name="wanSignSystemId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        [Sql("DELETE FROM M_ITAKU_LINK_WANSIGN_KEIYAKU WHERE WANSIGN_SYSTEM_ID = /*wanSignSystemId*/")]
        int DeleteByWanSignSystemId(long wanSignSystemId);
    }
}