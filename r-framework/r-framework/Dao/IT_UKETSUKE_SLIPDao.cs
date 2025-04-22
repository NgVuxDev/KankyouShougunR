using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// ��t�`�[�f�[�^Dao
    /// </summary>
    [Bean(typeof(T_UKETSUKE_SLIP))]
    public interface IT_UKETSUKE_SLIPDao : IS2Dao
    {
        /// <summary>
        /// �S���R�[�h�擾����
        /// </summary>
        [Sql("SELECT * FROM T_UKETSUKE_SLIP")]
        T_UKETSUKE_SLIP[] GetAllData();

        /// <summary>
        /// Insert����
        /// </summary>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SLIP data);

        /// <summary>
        /// �X�V�����i"CREATE_USER", "CREATE_DATE", "CREATE_PC"���X�V�ΏۂɊ܂߂Ȃ��j
        /// </summary>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SLIP data);

        /// <summary>
        /// ���R�[�h�폜����
        /// </summary>
        int Delete(T_UKETSUKE_SLIP data);

        /// <summary>
        /// �_���폜�t���O�X�V�����i"DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC"�݂̂��X�V����j
        /// </summary>
        [PersistentProps("DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC")]
        int UpdateLogicalDeleteFlag(T_UKETSUKE_SLIP data);

        /// <summary>
        /// ��t�ԍ��̍ő�l���擾���鏈��
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),1) FROM T_UKETSUKE_SLIP")]
        int GetMaxKey();

        /// <summary>
        /// ��t�ԍ��̍ő�l+1���擾���鏈��
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),0)+1 FROM T_UKETSUKE_SLIP")]
        int GetMaxPlusKey();

        /// <summary>
        /// ��t�ԍ��̍ŏ��l���擾���鏈��
        /// </summary>
        [Sql("SELECT ISNULL(MIN(UKETSUKE_NO),1) FROM T_UKETSUKE_SLIP")]
        int GetMinKey();

        /// <summary>
        /// �_���폜���s���Ă��Ȃ��f�[�^�ɑ΂��Ď�t�ԍ��̍ő�l���擾���鏈��
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),1) FROM T_UKETSUKE_SLIP")]
        int GetMaxKeyIsNotDelete();

        /// <summary>
        /// �_���폜���s���Ă��Ȃ��f�[�^�ɑ΂��Ď�t�ԍ��̍ő�l+1���擾���鏈��
        /// </summary>
        [Sql("SELECT ISNULL(MAX(UKETSUKE_NO),0)+1 FROM T_UKETSUKE_SLIP")]
        int GetMaxPlusKeyNotDelete();

        /// <summary>
        /// ��t�ԍ�����`�[�̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Query("UKETSUKE_NO = /*data.UKETSUKE_NO*/")]
        T_UKETSUKE_SLIP GetUketsukeData(T_UKETSUKE_SLIP data);

        /// <summary>
        /// ��t�ԍ��ɂđΏۂ̃f�[�^�����݂��Ă��邩���m�F����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT Count(*) FROM T_UKETSUKE_SLIP where UKETSUKE_NO = /*data.UKETSUKE_NO*/")]
        int GetExtistCheck(T_UKETSUKE_SLIP data);

        /// <summary>
        /// �`�[�O���[�v�ԍ��̍ő�l+1���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("select MAX(SLIP_GROUP_NO)+1 from T_UKETSUKE_SLIP")]
        int GetMaxPlusDenpyoGroupNo();

        /// <summary>
        /// ���݂̎�t�ԍ���菬�������A���̒��ň�ԑ傫����t�ԍ������f�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("select * from T_UKETSUKE_SLIP SLIP2, (select max(UKETSUKE_NO) as UKETSUKE_NO from T_UKETSUKE_SLIP where T_UKETSUKE_SLIP.UKETSUKE_NO < /*data.UKETSUKE_NO*/) SLIP1 where SLIP2.UKETSUKE_NO = SLIP1.UKETSUKE_NO")]
        T_UKETSUKE_SLIP GetBeforeUketsukeNo(T_UKETSUKE_SLIP data);

        /// <summary>
        /// ���݂̎�t�ԍ����傫�����A���̒��ň�ԏ����Ȏ�t�ԍ������f�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("select * from T_UKETSUKE_SLIP SLIP2, (select MIN(UKETSUKE_NO) as UKETSUKE_NO from T_UKETSUKE_SLIP where T_UKETSUKE_SLIP.UKETSUKE_NO > /*data.UKETSUKE_NO*/) SLIP1 where SLIP2.UKETSUKE_NO = SLIP1.UKETSUKE_NO")]
        T_UKETSUKE_SLIP GetAfterUketsukeNo(T_UKETSUKE_SLIP data);

        /// <summary>
        /// ���݂̎�t�ԍ��̎��̎�t�ԍ���������O���[�v�̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetAfterDaisuu.sql")]
        T_UKETSUKE_SLIP GetAfterDaisuu(T_UKETSUKE_SLIP data);

        /// <summary>
        /// ���݂̎�t�ԍ��̑O�̎�t�ԍ���������O���[�v�̃f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetBeforeDaisuu.sql")]
        T_UKETSUKE_SLIP GetBeforeDaisuu(T_UKETSUKE_SLIP data);

        /// <summary>
        /// �ő��t�ԍ������f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMaxDate.sql")]
        T_UKETSUKE_SLIP GetMaxUketsukeNoDate();

        /// <summary>
        /// �ŏ���t�ԍ������f�[�^���擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMinDate.sql")]
        T_UKETSUKE_SLIP GetMinUketsukeNoDate();

        /// <summary>
        /// ����O���[�v���ɂčő�̎�t�ԍ������f�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMaxDateForGroup.sql")]
        T_UKETSUKE_SLIP GetMaxUketsukeNoDateForGroup(int groupNo);

        /// <summary>
        /// ����O���[�v���ɂčŏ��̎�t�ԍ������f�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UketsukeSlip.IT_UKETSUKE_SLIPDao_GetMinDateForGroup.sql")]
        T_UKETSUKE_SLIP GetMinUketsukeNoDateForGroup(int groupNo);

        /// <summary>
        /// �`�[�O���[�v�ԍ��Ƒ䐔_���q����Ɏ�t�ԍ����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>��t�ԍ�</returns>
        [Sql("SELECT UKETSUKE_NO FROM T_UKETSUKE_SLIP where SLIP_GROUP_NO = /*data.SLIP_GROUP_NO*/ and DAISUU_NUMERATOR = /*data.DAISUU_NUMERATOR*/")]
        int GetUketsukeDataForGroup(T_UKETSUKE_SLIP data);

        T_UKETSUKE_SLIP GetUketsukeDataForSqlFile(string path, T_UKETSUKE_SLIP data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, T_UKETSUKE_SLIP data);
    }
}