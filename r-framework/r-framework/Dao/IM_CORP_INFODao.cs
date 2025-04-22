using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_CORP_INFO))]
    public interface IM_CORP_INFODao : IS2Dao
    {
        [Sql("SELECT * FROM M_CORP_INFO")]
        M_CORP_INFO[] GetAllData();

        [Sql("SELECT" +
            " CORP_RYAKU_NAME," +
            " KISHU_MONTH," +
            " BANK_CD, BANK_SHITEN_CD, KOUZA_SHURUI, KOUZA_NO, KOUZA_NAME," +
            " BANK_CD_2, BANK_SHITEN_CD_2, KOUZA_SHURUI_2, KOUZA_NO_2, KOUZA_NAME_2," +
            " BANK_CD_3, BANK_SHITEN_CD_3, KOUZA_SHURUI_3, KOUZA_NO_3, KOUZA_NAME_3" +
            " FROM" +
            " M_CORP_INFO")]
        M_CORP_INFO[] GetAllDataMinCols();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.CorpInfo.IM_CORP_INFODao_GetAllValidData.sql")]
        M_CORP_INFO[] GetAllValidData(M_CORP_INFO data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CORP_INFO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CORP_INFO data);

        int Delete(M_CORP_INFO data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CORP_INFO data);
    }
}