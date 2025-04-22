using Seasar.Dao.Attrs;
using r_framework.Entity;
using System.Collections.Generic;
namespace r_framework.Dao
{
    [Bean(typeof(M_KARI_TORIHIKISAKI_SHIHARAI))]
    public interface IM_KARI_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KARI_TORIHIKISAKI_SHIHARAI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KARI_TORIHIKISAKI_SHIHARAI data);

        int Delete(M_KARI_TORIHIKISAKI_SHIHARAI data);

        /// <summary>
        ///�����R�[�h�����Ƃɉ������_�x�����}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_KARI_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);

        List<M_KARI_TORIHIKISAKI_SHIHARAI> GetKariTorihikisakiShiharaiList(M_KARI_TORIHIKISAKI_SHIHARAI entity);
    }
}
