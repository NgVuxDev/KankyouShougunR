using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_JIGYOUJOU))]
    public interface IM_DENSHI_JIGYOUJOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_JIGYOUJOU")]
        M_DENSHI_JIGYOUJOU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_JIGYOUJOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_JIGYOUJOU data);

        int Delete(M_DENSHI_JIGYOUJOU data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_JIGYOUJOU data);

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
        [SqlFile("r_framework.Dao.SqlFile.DenshiJigyoujo.IM_DENSHI_JIGYOUJOUDao_GetAllValidData.sql")]
        M_DENSHI_JIGYOUJOU[] GetAllValidData(M_DENSHI_JIGYOUJOU data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
