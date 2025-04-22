using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// M_JISSEKI_BUNRUI Dao Class.
    /// </summary>
    [Bean(typeof(M_JISSEKI_BUNRUI))]
    public interface IM_JISSEKI_BUNRUIDao : IS2Dao
    {
        /// <summary>
        /// �S�f�[�^�擾
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM M_JISSEKI_BUNRUI")]
        M_JISSEKI_BUNRUI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.JissekiBunrui.IM_JISSEKI_BUNRUIDao_GetAllValidData.sql")]
        M_JISSEKI_BUNRUI[] GetAllValidData(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// �}��
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// �X�V
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// �폜
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// �S�f�[�^�擾�p
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Sql("select M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_CD AS CD, M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_NAME_RYAKU AS NAME FROM M_JISSEKI_BUNRUI /*$whereSql*/ group by M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_CD, M_JISSEKI_BUNRUI.JISSEKI_BUNRUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.JissekiBunrui.GetJissekiBunruiDataSql.sql")]
        DataTable GetJissekiBunruiData(M_JISSEKI_BUNRUI data);

        /// <summary>
        /// �ǂ̃}�X�^�Ŏg�p����Ă��邩���}�X�^����������
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">���ѕ���CD</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] JISSEKI_BUNRUI_CD);

        /// <summary>
        /// ���ѕ��ރR�[�h�����ƂɎ��ѕ��ނ̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("JISSEKI_BUNRUI_CD = /*cd*/")]
        M_JISSEKI_BUNRUI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        [SqlFile("r_framework.Dao.SqlFile.JissekiBunrui.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSqlFile(M_JISSEKI_BUNRUI data, bool deletechuFlg);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile2(string path, M_JISSEKI_BUNRUI data, bool deletechuFlg);
    }
}
