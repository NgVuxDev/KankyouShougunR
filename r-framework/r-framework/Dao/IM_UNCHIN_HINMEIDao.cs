using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_UNCHIN_HINMEI))]
    public interface IM_UNCHIN_HINMEIDao : IS2Dao
    {
        [Sql("SELECT * FROM M_UNCHIN_HINMEI")]
        M_UNCHIN_HINMEI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UnchinHinmei.IM_UNCHIN_HINMEIDao_GetAllValidData.sql")]
        M_UNCHIN_HINMEI[] GetAllValidData(M_UNCHIN_HINMEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_UNCHIN_HINMEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_UNCHIN_HINMEI data);

        int Delete(M_UNCHIN_HINMEI data);

        [Sql("SELECT M_UNCHIN_HINMEI.UNCHIN_HINMEI_CD AS CD, M_UNCHIN_HINMEI.UNCHIN_HINMEI_NAME AS NAME" +
            " FROM M_UNCHIN_HINMEI /*$whereSql*/" +
            " GROUP BY M_UNCHIN_HINMEI.UNCHIN_HINMEI_CD, M_UNCHIN_HINMEI.UNCHIN_HINMEI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_UNCHIN_HINMEI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("UNCHIN_HINMEI_CD = /*cd*/")]
        M_UNCHIN_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_UNCHIN_HINMEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}