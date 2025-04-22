using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ��t���׃f�[�^Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_DETAIL))]
    public interface IT_UKETSUKE_DETAILDao : IS2Dao
    {
        /// <summary>
        /// �S���R�[�h�擾����
        /// </summary>
        [Sql("SELECT * FROM T_UKETSUKE_DETAIL")]
        T_UKETSUKE_DETAIL[] GetAllData();

        /// <summary>
        /// insert����
        /// </summary>
        int Insert(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// �X�V�����i"CREATE_USER", "CREATE_DATE", "CREATE_PC"���X�V�ΏۂɊ܂߂Ȃ��j
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// �폜����
        /// </summary>
        int Delete(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// �_���폜�t���O�X�V�����i"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"�݂̂��X�V����j
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(T_UKETSUKE_DETAIL data);

        /// <summary>
        /// ��t�ԍ�����Ώۂ̖��׃f�[�^���擾����
        /// </summary>
        [Query("UKETSUKE_NO = /*UketsukeNo*/")]
        T_UKETSUKE_DETAIL[] GetUketsukeData(int uketsukeNo);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, T_UKETSUKE_DETAIL data);
    }
}