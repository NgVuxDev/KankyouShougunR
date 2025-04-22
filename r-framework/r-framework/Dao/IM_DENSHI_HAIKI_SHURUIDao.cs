using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface IM_DENSHI_HAIKI_SHURUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_HAIKI_SHURUI")]
        M_DENSHI_HAIKI_SHURUI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_HAIKI_SHURUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_HAIKI_SHURUI data);

        int Delete(M_DENSHI_HAIKI_SHURUI data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_HAIKI_SHURUI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_HAIKI_SHURUI data, bool deletechuFlg);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�p�������CD�̔z��</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] HAIKI_SHURUI_CD);

        /// <summary>
        /// �ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(HAIKI_SHURUI_CD),0)+1 FROM M_DENSHI_HAIKI_SHURUI WHERE ISNUMERIC(HAIKI_SHURUI_CD) = 1")]
        int GetMaxPlusKey();

        /// <summary>
        /// �����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiHaikiShurui.M_DENSHI_HAIKI_SHURUI_GetAllValidData.sql")]
        M_DENSHI_HAIKI_SHURUI[] GetAllValidData(M_DENSHI_HAIKI_SHURUI data);

        /// <summary>
        /// �}�X�^���ʃ|�b�v�A�b�v�p���擾
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Sql("SELECT M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD AS CD,M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME AS NAME FROM M_DENSHI_HAIKI_SHURUI /*$whereSql*/ GROUP BY M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_CD,M_DENSHI_HAIKI_SHURUI.HAIKI_SHURUI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
