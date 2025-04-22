using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using System;

namespace r_framework.Dao
{
    /// <summary>
    /// �Ј��}�X�^Dao
    /// </summary>
    [Bean(typeof(M_SBNB_PATTERN))]
    public interface IM_SBNB_PATTERNDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SBNB_PATTERN data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SBNB_PATTERN data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_SBNB_PATTERN data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_SBNB_PATTERN")]
        M_SBNB_PATTERN[] GetAllData();

        /// <summary>
        /// �p�^�[���f�[�^�̎擾���s��
        /// </summary>
        /// <param name="path">�p�^�[����</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetPatternDataSqlFile(string path, M_SBNB_PATTERN data);

        /// <summary>
        /// �V�X�e��ID�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(SYSTEM_ID),0)+1 FROM M_SBNB_PATTERN WHERE ISNUMERIC(SYSTEM_ID) = 1")]
        Int64 GetMaxPlusKey();
    }
}