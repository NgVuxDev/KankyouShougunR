using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ��s�}�X�^��Dao�N���X
    /// </summary>
    [Bean(typeof(M_SYS_PREV_VALUE))]
    public interface IM_SYS_PREV_VALUEDao : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_SYS_PREV_VALUE")]
        M_SYS_PREV_VALUE[] GetAllData();
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SYS_PREV_VALUE data);
        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SYS_PREV_VALUE data);
        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SYS_PREV_VALUE data);
        /// <summary>
        /// ��s�R�[�h�����Ƃɕ����̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GAMEN_ID = /*gamenId*/")]
        M_SYS_PREV_VALUE[] GetAllByGamenId(string gamenId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamenId"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        [Query("GAMEN_ID = /*gamenId*/ AND FIELD_NAME = /*fieldName*/")]
        M_SYS_PREV_VALUE GetById(string gamenId, string fieldName);
    }
}