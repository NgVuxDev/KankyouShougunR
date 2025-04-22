using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHURUI))]
    public interface IM_SHURUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHURUI")]
        M_SHURUI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shurui.IM_SHURUIDao_GetAllValidData.sql")]
        M_SHURUI[] GetAllValidData(M_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHURUI data);

        int Delete(M_SHURUI data);

        [Sql("select M_SHURUI.SHURUI_CD AS CD,M_SHURUI.SHURUI_NAME_RYAKU AS NAME FROM M_SHURUI /*$whereSql*/ group by M_SHURUI.SHURUI_CD,M_SHURUI.SHURUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHURUI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHURUI_CD = /*cd*/")]
        M_SHURUI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHURUI data, bool deletechuFlg);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">��ރf�[�^</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHURUI_CD);

        //20250325
        [Sql("SELECT SHURUI_CD, SHURUI_NAME_RYAKU FROM M_SHURUI WHERE DELETE_FLG = 0")]
        DataTable GetAllDataForCbb();
    }
}
