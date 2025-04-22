using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SYS_INFO))]
    public interface IM_SYS_INFODao : IS2Dao
    {

        [Sql("SELECT * FROM M_SYS_INFO")]
        M_SYS_INFO[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.SysInfo.IM_SYS_INFODao_GetAllValidData.sql")]
        M_SYS_INFO[] GetAllValidData(M_SYS_INFO data);

        [NoPersistentProps("COPY_MODE", "TIME_STAMP")]
        int Insert(M_SYS_INFO data);

        [NoPersistentProps("SUPPORT_TOOL_URL_PATH", "COPY_MODE", "HISTORY_DELETE_RANGE", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SYS_INFO data);

        int Delete(M_SYS_INFO data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SYS_INFO data);

        [Query("SYS_ID = /*sysId*/")]
        M_SYS_INFO GetAllDataForCode(string sysId);

        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
