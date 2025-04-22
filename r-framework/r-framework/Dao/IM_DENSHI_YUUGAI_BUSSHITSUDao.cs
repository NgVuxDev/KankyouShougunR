using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_YUUGAI_BUSSHITSU))]
    public interface IM_DENSHI_YUUGAI_BUSSHITSUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_YUUGAI_BUSSHITSU")]
        M_DENSHI_YUUGAI_BUSSHITSU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_YUUGAI_BUSSHITSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_YUUGAI_BUSSHITSU data);

        int Delete(M_DENSHI_YUUGAI_BUSSHITSU data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_YUUGAI_BUSSHITSU data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_YUUGAI_BUSSHITSU data,bool deletechuFlg);

        /// <summary>
        /// �ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(YUUGAI_BUSSHITSU_CD),0)+1 FROM M_DENSHI_YUUGAI_BUSSHITSU WHERE ISNUMERIC(YUUGAI_BUSSHITSU_CD) = 1")]
        int GetMaxPlusKey();

        /// �����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiYuugaiBusshitsu.IM_DENSHI_YUUGAI_BUSSHITSUDao_GetAllValidData.sql")]
        M_DENSHI_YUUGAI_BUSSHITSU[] GetAllValidData(M_DENSHI_YUUGAI_BUSSHITSU data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("YUUGAI_BUSSHITSU_CD = /*cd*/")]
        M_DENSHI_YUUGAI_BUSSHITSU GetDataByCd(string cd);

    }
}
