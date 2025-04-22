using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_YOUKI))]
    public interface IM_YOUKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_YOUKI")]
        M_YOUKI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Youki.IM_YOUKIDao_GetAllValidData.sql")]
        M_YOUKI[] GetAllValidData(M_YOUKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_YOUKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_YOUKI data);

        int Delete(M_YOUKI data);

        [Sql("select M_YOUKI.YOUKI_CD AS CD,M_YOUKI.YOUKI_NAME_RYAKU AS NAME,M_YOUKI.YOUKI_JYURYO FROM M_YOUKI /*$whereSql*/ group by M_YOUKI.YOUKI_CD,M_YOUKI.YOUKI_NAME_RYAKU,M_YOUKI.YOUKI_JYURYO")]
        DataTable GetAllMasterDataForPopup(string whereSql);


        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] YOUKI_CD);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_YOUKI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("YOUKI_CD = /*cd*/")]
        M_YOUKI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_YOUKI data, bool deletechuFlg);
    }
}
