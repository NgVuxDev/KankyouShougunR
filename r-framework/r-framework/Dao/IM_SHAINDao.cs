using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ј��}�X�^Dao
    /// </summary>
    [Bean(typeof(M_SHAIN))]
    public interface IM_SHAINDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHAIN data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHAIN data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_SHAIN data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_SHAIN")]
        M_SHAIN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shain.IM_SHAINDao_GetAllValidData.sql")]
        M_SHAIN[] GetAllValidData(M_SHAIN data);

        /// <summary>
        /// �_���폜�t���O�X�V�����i"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"�݂̂��X�V����j
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(M_SHAIN data);

        /// <summary>
        /// �Ј��R�[�h�����Ƀf�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="cd">�Ј��R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHAIN_CD = /*cd*/")]
        M_SHAIN GetDataByCd(string cd);

        /// <summary>
        /// �Ј��R�[�h�����ɒS���ҏ����擾����
        /// </summary>
        /// <parameparam name="shainCd">�Ј��R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shain.IM_SHAINDao_GetTantoushaDate.sql")]
        M_SHAIN GetTantoushaDate(string shainCd);

        [Sql("select M_SHAIN.SHAIN_CD AS CD,M_SHAIN.SHAIN_NAME_RYAKU AS NAME FROM M_SHAIN /*$whereSql*/ group by M_SHAIN.SHAIN_CD,M_SHAIN.SHAIN_NAME_RYAKU order by M_SHAIN.SHAIN_CD")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�Ј��f�[�^</param>
        /// <returns></returns>
        DataTable GetShainDataSqlFile(string path, M_SHAIN data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�Ј��f�[�^</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHAIN_CD);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHAIN data, bool deletechuFlg);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}