using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_JIGYOUSHA))]
    public interface IM_DENSHI_JIGYOUSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_JIGYOUSHA")]
        M_DENSHI_JIGYOUSHA[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_JIGYOUSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_JIGYOUSHA data);

        int Delete(M_DENSHI_JIGYOUSHA data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_JIGYOUSHA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�����Ҕԍ�</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string EDI_MEMBER_ID);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiJigyousha.IM_DENSHI_JIGYOUSHADao_GetAllValidData.sql")]
        M_DENSHI_JIGYOUSHA[] GetAllValidData(M_DENSHI_JIGYOUSHA data);

        /// <summary>
        /// �Ǝ҃R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(EDI_MEMBER_ID),0)+1 FROM M_GENBA WHERE ISNUMERIC(EDI_MEMBER_ID) = 1")]
        int GetMaxPlusKey();

        [Sql("select M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID AS CD,M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME AS NAME FROM M_DENSHI_JIGYOUSHA /*$whereSql*/ group by M_DENSHI_JIGYOUSHA.EDI_MEMBER_ID,M_DENSHI_JIGYOUSHA.JIGYOUSHA_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

    }
}
