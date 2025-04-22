using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NYUUKINSAKI))]
    public interface IM_NYUUKINSAKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_NYUUKINSAKI")]
        M_NYUUKINSAKI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Nyuukinsaki.IM_NYUUKINSAKIDao_GetAllValidData.sql")]
        M_NYUUKINSAKI[] GetAllValidData(M_NYUUKINSAKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUKINSAKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUUKINSAKI data);

        int Delete(M_NYUUKINSAKI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUUKINSAKI data);

        /// <summary>
        /// ������R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(N.NYUUKINSAKI_CD),0)+1 FROM M_NYUUKINSAKI N LEFT JOIN M_TORIHIKISAKI T ON T.TORIHIKISAKI_CD = N.NYUUKINSAKI_CD where ISNUMERIC(N.NYUUKINSAKI_CD) = 1 AND (T.SHOKUCHI_KBN IS NULL OR T.SHOKUCHI_KBN = 0)")]
        int GetMaxPlusKey();

        /// <summary>
        /// ������R�[�h�̍ŏ��̋󂫔Ԃ��擾����
        /// </summary>
        /// <param name="data">null��n��</param>
        /// <returns>�ŏ��̋󂫔�</returns>
        [SqlFile("r_framework.Dao.SqlFile.Nyuukinsaki.IM_NYUUKINSAKIDao_GetMinBlankNo.sql")]
        int GetMinBlankNo(M_NYUUKINSAKI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NYUUKINSAKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);

        /// <summary>
        /// ������R�[�h�����Ƀf�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="cd">�Ј��R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("NYUUKINSAKI_CD = /*cd*/")]
        M_NYUUKINSAKI GetDataByCd(string cd);

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
        /// <param name="data">������}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_NYUUKINSAKI data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}
