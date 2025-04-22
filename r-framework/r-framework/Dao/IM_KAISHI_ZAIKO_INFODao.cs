using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KAISHI_ZAIKO_INFO))]
    public interface IM_KAISHI_ZAIKO_INFODao : IS2Dao
    {

        [Sql("SELECT * FROM M_KAISHI_ZAIKO_INFO")]
        M_KAISHI_ZAIKO_INFO[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ZaikoHinmei.IM_KAISHI_ZAIKO_INFODao_GetAllValidData.sql")]
        M_KAISHI_ZAIKO_INFO[] GetAllValidData(M_KAISHI_ZAIKO_INFO data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KAISHI_ZAIKO_INFO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KAISHI_ZAIKO_INFO data);

        int Delete(M_KAISHI_ZAIKO_INFO data);

       
        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KAISHI_ZAIKO_INFO data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GYOUSHA_CD = /*gyoushaCd*/ AND GENBA_CD = /*genbaCd*/ AND ZAIKO_HINMEI_CD = /*zaikoHinmeiCd*/")]
        M_KAISHI_ZAIKO_INFO GetDataByCd(string gyoushaCd, string genbaCd, string zaikoHinmeiCd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KAISHI_ZAIKO_INFO data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
