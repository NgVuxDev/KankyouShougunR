using System.Data;
using System.Data.SqlTypes;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KOBETSU_HINMEI))]
    public interface IM_KOBETSU_HINMEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_KOBETSU_HINMEI")]
        M_KOBETSU_HINMEI[] GetAllData();

        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmei.IM_KOBETSU_HINMEIDao_GetDataByCd.sql")]
        M_KOBETSU_HINMEI GetDataByCd(M_KOBETSU_HINMEI data);

        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmei.IM_KOBETSU_HINMEIDao_GetDataByHinmei.sql")]
        M_KOBETSU_HINMEI GetDataByHinmei(M_KOBETSU_HINMEI data, M_HINMEI hinmei);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KobetsuHinmei.IM_KOBETSU_HINMEIDao_GetAllValidData.sql")]
        M_KOBETSU_HINMEI[] GetAllValidData(M_KOBETSU_HINMEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KOBETSU_HINMEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KOBETSU_HINMEI data);

        int Delete(M_KOBETSU_HINMEI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KOBETSU_HINMEI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KOBETSU_HINMEI data, bool deletechuFlg);
    }
}
