using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI))]
    public interface IM_DENSHI_HAIKI_SHURUI_SAIBUNRUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_HAIKI_SHURUI_SAIBUNRUI")]
        M_DENSHI_HAIKI_SHURUI_SAIBUNRUI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        int Delete(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        DataTable GetDataBySqlFile(string path, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data, bool deletechuFlg);

        /// <summary>
        /// �ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(EDI_MEMBER_ID),0)+1 FROM M_DENSHI_HAIKI_SHURUI_SAIBUNRUI WHERE ISNUMERIC(EDI_MEMBER_ID) = 1")]
        int GetMaxPlusKey();

        /// �����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiHaikiShuruiSaibunrui.M_DENSHI_HAIKI_SHURUI_SAIBUNRUI_GetAllValidData.sql")]
        M_DENSHI_HAIKI_SHURUI_SAIBUNRUI[] GetAllValidData(M_DENSHI_HAIKI_SHURUI_SAIBUNRUI data);

        /// <summary>
        /// �}�X�^���ʃ|�b�v�A�b�v�p���擾
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Sql("SELECT M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_SAIBUNRUI_CD AS CD, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_NAME AS NAME FROM M_DENSHI_HAIKI_SHURUI_SAIBUNRUI /*$whereSql*/ GROUP BY M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.EDI_MEMBER_ID, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_CD, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_SAIBUNRUI_CD, M_DENSHI_HAIKI_SHURUI_SAIBUNRUI.HAIKI_SHURUI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);
    }
}
