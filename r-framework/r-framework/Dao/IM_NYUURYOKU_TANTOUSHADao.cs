using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NYUURYOKU_TANTOUSHA))]
    public interface IM_NYUURYOKU_TANTOUSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_NYUURYOKU_TANTOUSHA")]
        M_NYUURYOKU_TANTOUSHA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.NyuuryokuTantousha.IM_NYUURYOKU_TANTOUSHADao_GetAllValidData.sql")]
        M_NYUURYOKU_TANTOUSHA[] GetAllValidData(M_NYUURYOKU_TANTOUSHA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUURYOKU_TANTOUSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUURYOKU_TANTOUSHA data);

        int Delete(M_NYUURYOKU_TANTOUSHA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUURYOKU_TANTOUSHA data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_NYUURYOKU_TANTOUSHA GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NYUURYOKU_TANTOUSHA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
