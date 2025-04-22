using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_ITAKU_KEIYAKU_KIHON))]
    public interface IM_ITAKU_KEIYAKU_KIHONDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ITAKU_KEIYAKU_KIHON")]
        M_ITAKU_KEIYAKU_KIHON[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ItakuKeiyakuKihon.IM_ITAKU_KEIYAKU_KIHONDao_GetAllValidData.sql")]
        M_ITAKU_KEIYAKU_KIHON[] GetAllValidData(M_ITAKU_KEIYAKU_KIHON data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ITAKU_KEIYAKU_KIHON data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ITAKU_KEIYAKU_KIHON data);

        int Delete(M_ITAKU_KEIYAKU_KIHON data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ITAKU_KEIYAKU_KIHON data);

        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM M_ITAKU_KEIYAKU_KIHON where ISNUMERIC(SYSTEM_ID) = 1 ")]
        int GetMaxPlusKey();

        [Query("SYSTEM_ID = /*data.SYSTEM_ID*/")]
        M_ITAKU_KEIYAKU_KIHON GetDataBySystemId(M_ITAKU_KEIYAKU_KIHON data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        //#160047 20220328 CongBinh S
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keiyakuNo"></param>
        /// <returns></returns>
        [Query("ITAKU_KEIYAKU_NO = /*keiyakuNo*/")]
        M_ITAKU_KEIYAKU_KIHON[] GetDataByKeiyakuNo(string keiyakuNo);
        //#160047 20220328 CongBinh E
    }
}
