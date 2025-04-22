using System.Data;
using System.Data.SqlTypes;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �`��̔�Dao
    /// </summary>
    [Bean(typeof(S_NUMBER_DENSHU))]
    public interface IS_NUMBER_DENSHUDao : IS2Dao
    {
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM S_NUMBER_DENSHU")]
        S_NUMBER_DENSHU[] GetAllData();

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(S_NUMBER_DENSHU data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(S_NUMBER_DENSHU data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        int Delete(S_NUMBER_DENSHU data);

        /// <summary>
        /// ��L�[�����Ƃɍ폜����Ă��Ȃ��`��̔Ԃ̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/")]
        S_NUMBER_DENSHU GetNumberDenshuData(S_NUMBER_DENSHU data);

        /// <summary>
        /// �y�e�[�u�����b�N�z�e�[�u�������b�N����L�[�����Ƃɓ`��̔Ԃ̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Sql("SELECT * FROM S_NUMBER_DENSHU WITH(TABLOCKX) WHERE DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/")]
        S_NUMBER_DENSHU GetNumberDenshuDataWithTableLock(S_NUMBER_DENSHU data);

        /// <summary>
        /// �`��A�Ԃ̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(CURRENT_NUMBER),1) FROM S_NUMBER_DENSHU WHERE DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/")]
        int GetMaxKey(S_NUMBER_DENSHU data);

        /// <summary>
        /// �`��A�Ԃ̍ŏ��l���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(CURRENT_NUMBER),1) FROM S_NUMBER_DENSHU WHERE DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/")]
        int GetMinKey(S_NUMBER_DENSHU data);

        /// <summary>
        /// �`��A�Ԃ̍ő�l+1���擾����
        /// </summary>
        /// <param name="data"></param>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(CURRENT_NUMBER),0)+1 FROM S_NUMBER_DENSHU WHERE DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/")]
        int GetMaxPlusKey(S_NUMBER_DENSHU data);

        /// <summary>
        /// �`��A�Ԃ̍ő�l+�������擾����
        /// </summary>
        /// <param name="data"></param>
        /// <param name="incrementNumber">����</param>
        /// <returns>�ő�l+����</returns>
        [Sql("SELECT ISNULL(MAX(CURRENT_NUMBER),0)+/*incrementNumber*/ FROM S_NUMBER_DENSHU WHERE DENSHU_KBN_CD = /*data.DENSHU_KBN_CD*/")]
        int GetMaxIncrementKey(S_NUMBER_DENSHU data, SqlInt64 incrementNumber);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, S_NUMBER_DENSHU data);
    }
}
