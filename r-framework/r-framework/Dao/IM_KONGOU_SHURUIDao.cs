using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KONGOU_SHURUI))]
    public interface IM_KONGOU_SHURUIDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_KONGOU_SHURUI")]
        M_KONGOU_SHURUI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouShurui.IM_KONGOU_SHURUIDao_GetAllValidData.sql")]
        M_KONGOU_SHURUI[] GetAllValidData(M_KONGOU_SHURUI data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouShurui.IM_KONGOU_SHURUIDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KONGOU_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KONGOU_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KONGOU_SHURUI data);

        int Delete(M_KONGOU_SHURUI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KONGOU_SHURUI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">������ރf�[�^</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] KONGOU_SHURUI_CD, int HAIKI_KBN_CD);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/ and KONGOU_SHURUI_CD = /*data.KONGOU_SHURUI_CD*/")]
        M_KONGOU_SHURUI GetDataByCd(M_KONGOU_SHURUI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KONGOU_SHURUI data, bool deletechuFlg);

        [Sql("select KONGOU_SHURUI_CD AS CD, KONGOU_SHURUI_NAME_RYAKU AS NAME from M_KONGOU_SHURUI /*$whereSql*/ group by HAIKI_KBN_CD, KONGOU_SHURUI_CD, KONGOU_SHURUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
