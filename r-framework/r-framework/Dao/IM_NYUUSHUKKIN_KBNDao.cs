using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ���o���敪�}�X�^dao
    /// </summary>
    [Bean(typeof(M_NYUUSHUKKIN_KBN))]
    public interface IM_NYUUSHUKKIN_KBNDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_NYUUSHUKKIN_KBN")]
        M_NYUUSHUKKIN_KBN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.NyuushukkinKbn.IM_NYUUSHUKKIN_KBNDao_GetAllValidData.sql")]
        M_NYUUSHUKKIN_KBN[] GetAllValidData(M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// �R�[�h�����ɓ��o���敪�����擾����
        /// </summary>
        /// <parameparam name="cd">���o���敪�R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("NYUUSHUKKIN_KBN_CD = /*cd*/")]
        M_NYUUSHUKKIN_KBN GetDataByCd(int cd);

        [Sql("select right('00' + convert(varchar, M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD), 2) AS CD,M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME AS NAME FROM M_NYUUSHUKKIN_KBN /*$whereSql*/ group by M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_CD,M_NYUUSHUKKIN_KBN.NYUUSHUKKIN_KBN_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUUSHUKKIN_KBN data);

        /// <summary>
        /// PK�L�[�z��̌��������ɂ�鑼�f�[�^�g�p�L������p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="NYUUSHUKKIN_KBN_CD">���o���敪CD���X�g</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] NYUUSHUKKIN_KBN_CD);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NYUUSHUKKIN_KBN data, bool deletechuFlg);
    }
}