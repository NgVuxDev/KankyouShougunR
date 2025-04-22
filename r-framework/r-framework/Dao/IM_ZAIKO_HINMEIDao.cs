using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_ZAIKO_HINMEI))]
    public interface IM_ZAIKO_HINMEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ZAIKO_HINMEI")]
        M_ZAIKO_HINMEI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ZaikoHinmei.IM_ZAIKO_HINMEIDao_GetAllValidData.sql")]
        M_ZAIKO_HINMEI[] GetAllValidData(M_ZAIKO_HINMEI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HINMEI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_ZAIKO_HINMEI data);

        int Delete(M_ZAIKO_HINMEI data);

        [Sql("select M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD, M_ZAIKO_HINMEI.ZAIKO_HINMEI_NAME_RYAKU, M_ZAIKO_HINMEI.ZAIKO_TANKA FROM M_ZAIKO_HINMEI /*$whereSql*/ group by M_ZAIKO_HINMEI.ZAIKO_HINMEI_CD, M_ZAIKO_HINMEI.ZAIKO_HINMEI_NAME_RYAKU, M_ZAIKO_HINMEI.ZAIKO_TANKA")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_ZAIKO_HINMEI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("ZAIKO_HINMEI_CD = /*cd*/")]
        M_ZAIKO_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_ZAIKO_HINMEI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
