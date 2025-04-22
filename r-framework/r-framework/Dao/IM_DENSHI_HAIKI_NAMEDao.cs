using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_HAIKI_NAME))]
    public interface IM_DENSHI_HAIKI_NAMEDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_HAIKI_NAME")]
        M_DENSHI_HAIKI_NAME[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_HAIKI_NAME data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_HAIKI_NAME data);

        int Delete(M_DENSHI_HAIKI_NAME data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_HAIKI_NAME data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_HAIKI_NAME data, bool deletechuFlg);

        /// <summary>
        /// �ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(EDI_MEMBER_ID),0)+1 FROM M_DENSHI_HAIKI_NAME WHERE ISNUMERIC(EDI_MEMBER_ID) = 1")]
        int GetMaxPlusKey();

        /// �����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiHaikiName.M_DENSHI_HAIKI_NAME_GetAllValidData.sql")]
        M_DENSHI_HAIKI_NAME[] GetAllValidData(M_DENSHI_HAIKI_NAME data);
    }
}
