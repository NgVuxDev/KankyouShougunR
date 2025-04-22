// 20140708 ria No.947 �c�ƊǗ��@�\���C start
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;
using System.Data.SqlTypes;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    /// <summary>
    /// �ʏ�}�X�^Dao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface IM_TORIHIKISAKIMASTERDao : IS2Dao
    {
        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKISAKI data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_TORIHIKISAKI data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_TORIHIKISAKI")]
        M_TORIHIKISAKI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Torihikisaki.IM_TORIHIKISAKIDao_GetAllValidData.sql")]
        M_TORIHIKISAKI[] GetAllValidData(M_TORIHIKISAKI data);

        /// <summary>
        /// �����R�[�h�̍ő�l���擾����
        /// </summary>
        /// <returns>�ő�l</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),1) FROM M_TORIHIKISAKI  where ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxKey();

        /// <summary>
        /// �����R�[�h�̍ŏ��l���擾����
        /// </summary>
        /// <returns>�ŏ��l</returns>
        [Sql("SELECT ISNULL(MIN(TORIHIKISAKI_CD),1) FROM M_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMinKey();

        /// <summary>
        /// �����R�[�h�̍ő�l+1���擾����
        /// </summary>
        /// <returns>�ő�l+1</returns>
        [Sql("SELECT ISNULL(MAX(TORIHIKISAKI_CD),0)+1 FROM M_TORIHIKISAKI WHERE ISNUMERIC(TORIHIKISAKI_CD) = 1 and SHOKUCHI_KBN = 0")]
        int GetMaxPlusKey();

        /// <summary>
        /// �����R�[�h�����ƂɎ����̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.MakeMtorikisakiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int MoveData(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD, SqlDateTime CREATE_DATE, string CREATE_USER, string CREATE_PC);

    }

    /// <summary>
    /// �ʏ�}�X�^Dao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SEIKYUU))]
    public interface IM_TORIHIKISAKI_SEIKYUUMASTERDao : IS2Dao
    {
        /// <summary>
        /// �����R�[�h�����ƂɎ����̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.MakeMtorikisakiSeikyuuData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int MoveData(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD, SqlInt16 NYUUKINSAKI_KBN);
    }

    /// <summary>
    /// �ʏ�}�X�^Dao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface IM_TORIHIKISAKI_SHIHARAIMASTERDao : IS2Dao
    {
        /// <summary>
        /// �����R�[�h�����ƂɎ����̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [SqlFile("Shougun.Core.Master.HikiaiTorihikisakiHoshu.Sql.MakeMtorikisakiShiharaiData.sql")]
        [NoPersistentProps("TIME_STAMP")]
        int MoveData(string oldTORIHIKISAKI_CD, string newTORIHIKISAKI_CD);
    }

    // 20140715 ria EV005233 ��������悩������ֈڍs����ۂɓ�����E�o���悪�쐬����Ȃ� start
    /// <summary>
    /// ������}�X�^Dao
    /// </summary>
    [Bean(typeof(M_NYUUKINSAKI))]
    public interface IM_NYUUKINSAKIMASTERDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUKINSAKI data);
    }

    /// <summary>
    /// �o����}�X�^Dao
    /// </summary>
    [Bean(typeof(M_SYUKKINSAKI))]
    public interface IM_SYUKKINSAKIMASTERDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SYUKKINSAKI data);
    }
    // 20140715 ria EV005233 ��������悩������ֈڍs����ۂɓ�����E�o���悪�쐬����Ȃ� end

}
// 20140708 ria No.947 �c�ƊǗ��@�\���C end