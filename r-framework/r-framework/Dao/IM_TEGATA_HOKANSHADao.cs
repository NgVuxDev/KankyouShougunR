using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_TEGATA_HOKANSHA))]
    public interface IM_TEGATA_HOKANSHADao : IS2Dao
    {

        [Sql("SELECT * FROM M_TEGATA_HOKANSHA")]
        M_TEGATA_HOKANSHA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.TegataHokansha.IM_TEGATA_HOKANSHADao_GetAllValidData.sql")]
        M_TEGATA_HOKANSHA[] GetAllValidData(M_TEGATA_HOKANSHA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TEGATA_HOKANSHA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TEGATA_HOKANSHA data);

        int Delete(M_TEGATA_HOKANSHA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TEGATA_HOKANSHA data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_TEGATA_HOKANSHA GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_TEGATA_HOKANSHA data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
