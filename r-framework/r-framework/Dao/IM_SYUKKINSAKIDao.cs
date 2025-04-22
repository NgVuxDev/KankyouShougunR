using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SYUKKINSAKI))]
    public interface IM_SYUKKINSAKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SYUKKINSAKI")]
        M_SYUKKINSAKI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Syukkinsaki.IM_SYUKKINSAKIDao_GetAllValidData.sql")]
        M_SYUKKINSAKI[] GetAllValidData(M_SYUKKINSAKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SYUKKINSAKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SYUKKINSAKI data);

        int Delete(M_SYUKKINSAKI data);

        /// <summary>
        /// �o����R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(S.SYUKKINSAKI_CD),0)+1 FROM M_SYUKKINSAKI S LEFT JOIN M_TORIHIKISAKI T ON T.TORIHIKISAKI_CD = S.SYUKKINSAKI_CD  where ISNUMERIC(S.SYUKKINSAKI_CD) = 1 AND (T.SHOKUCHI_KBN IS NULL OR T.SHOKUCHI_KBN = 0)")]
        int GetMaxPlusKey();

        /// <summary>
        /// �o����R�[�h�̍ŏ��̋󂫔Ԃ��擾����
        /// </summary>
        /// <param name="data">null��n��</param>
        /// <returns>�ŏ��̋󂫔�</returns>
        [SqlFile("r_framework.Dao.SqlFile.Syukkinsaki.IM_SYUKKINSAKIDao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_SYUKKINSAKI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SYUKKINSAKI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SYUKKINSAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// �o����R�[�h�����Ƀf�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="cd">�o����R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SYUKKINSAKI_CD = /*cd*/")]
        M_SYUKKINSAKI GetDataByCd(string cd);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        /// <summary>
        /// �Z���̈ꕔ�f�[�^���������@�\
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="data">�o����}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_SYUKKINSAKI data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}
