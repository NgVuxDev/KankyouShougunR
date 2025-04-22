using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KONGOU_HAIKIBUTSU))]
    public interface IM_KONGOU_HAIKIBUTSUDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_KONGOU_HAIKIBUTSU")]
        M_KONGOU_HAIKIBUTSU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouHaikibutsu.IM_KONGOU_HAIKIBUTSUDao_GetAllValidData.sql")]
        M_KONGOU_HAIKIBUTSU[] GetAllValidData(M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.KongouHaikibutsu.IM_KONGOU_HAIKIBUTSUDao_GetAllValidDataForPupUp.sql")]
        DataTable GetAllValidDataForPopUp(M_KONGOU_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KONGOU_HAIKIBUTSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KONGOU_HAIKIBUTSU data);

        int Delete(M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/ and KONGOU_SHURUI_CD = /*data.KONGOU_SHURUI_CD*/ and HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*/")]
        M_KONGOU_HAIKIBUTSU GetDataByCd(M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KONGOU_HAIKIBUTSU data);

        /// <summary>
        /// ���[�U�w��̍X�V�����ɂ��f�[�^�X�V
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_KONGOU_HAIKIBUTSU data, M_KONGOU_HAIKIBUTSU updateKey);
    }
}
