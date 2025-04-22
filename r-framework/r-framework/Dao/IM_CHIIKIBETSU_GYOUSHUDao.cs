using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_GYOUSHU))]
    public interface IM_CHIIKIBETSU_GYOUSHUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CHIIKIBETSU_GYOUSHU")]
        M_CHIIKIBETSU_GYOUSHU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuGyoushu.IM_CHIIKIBETSU_GYOUSHUDao_GetAllValidData.sql")]
        M_CHIIKIBETSU_GYOUSHU[] GetAllValidData(M_CHIIKIBETSU_GYOUSHU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_GYOUSHU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_GYOUSHU data);

        int Delete(M_CHIIKIBETSU_GYOUSHU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_GYOUSHU data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_GYOUSHU data, bool deletechuFlg);
    }
}
