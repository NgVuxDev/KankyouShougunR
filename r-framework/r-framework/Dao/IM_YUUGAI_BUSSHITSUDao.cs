using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_YUUGAI_BUSSHITSU))]
    public interface IM_YUUGAI_BUSSHITSUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_YUUGAI_BUSSHITSU")]
        M_YUUGAI_BUSSHITSU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.YuugaiBusshitsu.IM_YUUGAI_BUSSHITSUDao_GetAllValidData.sql")]
        M_YUUGAI_BUSSHITSU[] GetAllValidData(M_YUUGAI_BUSSHITSU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_YUUGAI_BUSSHITSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_YUUGAI_BUSSHITSU data);

        int Delete(M_YUUGAI_BUSSHITSU data);

        [Sql("select M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_CD AS CD,M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_NAME_RYAKU AS NAME FROM M_YUUGAI_BUSSHITSU /*$whereSql*/ group by M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_CD,M_YUUGAI_BUSSHITSU.YUUGAI_BUSSHITSU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_YUUGAI_BUSSHITSU data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("YUUGAI_BUSSHITSU_CD = /*cd*/")]
        M_YUUGAI_BUSSHITSU GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_YUUGAI_BUSSHITSU data, bool deletechuFlg);
    }
}
