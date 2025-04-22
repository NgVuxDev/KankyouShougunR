using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_JUUSHO))]
    public interface IM_CHIIKIBETSU_JUUSHODao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_CHIIKIBETSU_JUUSHO")]
        M_CHIIKIBETSU_JUUSHO[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuJuusho.IM_CHIIKIBETSU_JUUSHODao_GetAllValidData.sql")]
        M_CHIIKIBETSU_JUUSHO[] GetAllValidData(M_CHIIKIBETSU_JUUSHO data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_JUUSHO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_JUUSHO data);

        int Delete(M_CHIIKIBETSU_JUUSHO data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_JUUSHO data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_JUUSHO data, bool deletechuFlg);
    }
}
