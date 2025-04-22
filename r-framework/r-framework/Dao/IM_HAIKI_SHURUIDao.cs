using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_HAIKI_SHURUI))]
    public interface IM_HAIKI_SHURUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_HAIKI_SHURUI")]
        M_HAIKI_SHURUI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.HaikiShurui.IM_HAIKI_SHURUIDao_GetAllValidData.sql")]
        M_HAIKI_SHURUI[] GetAllValidData(M_HAIKI_SHURUI data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����(�}�X�^���ʃ|�b�v�A�b�v)
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.HaikiShurui.IM_HAIKI_SHURUIDao_GetAllValidDataForPopUp.sql")]
        DataTable GetAllValidDataForPopUp(M_HAIKI_SHURUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HAIKI_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HAIKI_SHURUI data);

        int Delete(M_HAIKI_SHURUI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] HAIKI_SHURUI_CD);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HAIKI_SHURUI data);

        /// <summary>
        /// Where����w�肵�ăf�[�^���擾����
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.HaikiShurui.IM_HAIKI_SHURUIDao_GetHaikiShuruiDataSql.sql")]
        DataTable GetHaikiShuruiDataSql(string whereSql);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�擾�����f�[�^</returns>
        [Query("HAIKI_KBN_CD = /*data.HAIKI_KBN_CD*/ and HAIKI_SHURUI_CD = /*data.HAIKI_SHURUI_CD*/")]
        M_HAIKI_SHURUI GetDataByCd(M_HAIKI_SHURUI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_HAIKI_SHURUI data, bool deletechuFlg);

        [Sql("select right('0000' + convert(varchar, M_HAIKI_SHURUI.HAIKI_SHURUI_CD), 4) AS CD, "
                + "M_HAIKI_SHURUI.HAIKI_SHURUI_NAME_RYAKU AS NAME, "
                + "MAX(M_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD) AS BUNRUI_CD, "
                + "MAX(M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME) AS BUNRUI_NAME "
                + "from M_HAIKI_SHURUI left join (select HOUKOKUSHO_BUNRUI_CD, HOUKOKUSHO_BUNRUI_NAME from M_HOUKOKUSHO_BUNRUI) as M_HOUKOKUSHO_BUNRUI "
                + "on M_HAIKI_SHURUI.HOUKOKUSHO_BUNRUI_CD = M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD "
                + "/*$whereSql*/ group by M_HAIKI_SHURUI.HAIKI_KBN_CD, M_HAIKI_SHURUI.HAIKI_SHURUI_CD, M_HAIKI_SHURUI.HAIKI_SHURUI_NAME_RYAKU ORDER BY CD")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
