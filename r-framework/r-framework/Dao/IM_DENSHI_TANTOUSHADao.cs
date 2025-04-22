using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_TANTOUSHA))]
    public interface IM_DENSHI_TANTOUSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_TANTOUSHA")]
        M_DENSHI_TANTOUSHA[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_TANTOUSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_TANTOUSHA data);

        int Delete(M_DENSHI_TANTOUSHA data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_TANTOUSHA data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_TANTOUSHA data,bool deletechuFlg);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(EDI_MEMBER_ID),0)+1 FROM M_DENSHI_JIGYOUJOU WHERE ISNUMERIC(EDI_MEMBER_ID) = 1")]
        int GetMaxPlusKey();

        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiTantousha.IM_DENSHI_TANTOUSHADao_GetAllValidData.sql")]
        M_DENSHI_TANTOUSHA[] GetAllValidData(M_DENSHI_TANTOUSHA data);
    }
}
