using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_BUNRUI))]
    public interface IM_BUNRUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_BUNRUI")]
        M_BUNRUI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Bunrui.IM_BUNRUIDao_GetAllValidData.sql")]
        M_BUNRUI[] GetAllValidData(M_BUNRUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BUNRUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BUNRUI data);

        int Delete(M_BUNRUI data);

        [Sql("select M_BUNRUI.BUNRUI_CD AS CD,M_BUNRUI.BUNRUI_NAME_RYAKU AS NAME FROM M_BUNRUI /*$whereSql*/ group by M_BUNRUI.BUNRUI_CD,M_BUNRUI.BUNRUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BUNRUI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�i���f�[�^</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'�i���}�X�^' AS NAME FROM M_HINMEI WHERE BUNRUI_CD IN /*BUNRUI_CD*/('') AND DELETE_FLG = 'False'")]
        DataTable GetDataBySqlFileCheck(string[] BUNRUI_CD);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("BUNRUI_CD = /*cd*/")]
        M_BUNRUI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BUNRUI data, bool deletechuFlg);
    }
}
