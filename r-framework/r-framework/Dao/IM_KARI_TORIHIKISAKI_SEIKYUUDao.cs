using Seasar.Dao.Attrs;
using r_framework.Entity;
using System.Collections.Generic;
namespace r_framework.Dao
{
    [Bean(typeof(M_KARI_TORIHIKISAKI_SEIKYUU))]
    public interface IM_KARI_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_TORIHIKISAKI_SEIKYUU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_TORIHIKISAKI_SEIKYUU data);

        int Delete(M_KARI_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// �����CD�R�[�h�����Ƃɉ������_�������}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);

        List<M_KARI_TORIHIKISAKI_SEIKYUU> GetKariTorihikisakiSeikyuuList(M_KARI_TORIHIKISAKI_SEIKYUU entity);
    }
}
