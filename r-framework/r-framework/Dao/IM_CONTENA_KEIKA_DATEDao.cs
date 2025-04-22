using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CONTENA_KEIKA_DATE))]
    public interface IM_CONTENA_KEIKA_DATEDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_KEIKA_DATE")]
        M_CONTENA_KEIKA_DATE[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_KEIKA_DATE data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CONTENA_KEIKA_DATE data);

        int Delete(M_CONTENA_KEIKA_DATE data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CONTENA_KEIKA_DATE data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GENCHAKU_TIME_CD = /*cd*/")]
        M_CONTENA_KEIKA_DATE GetDataByCd(string cd);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
