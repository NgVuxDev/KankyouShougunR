// $Id: IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao.cs 4688 2013-10-24 00:47:47Z sys_dev_20 $
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Dao
{
    [Bean(typeof(M_HIKIAI_TORIHIKISAKI_SEIKYUU))]
    public interface IM_HIKIAI_TORIHIKISAKI_SEIKYUUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_HIKIAI_TORIHIKISAKI_SEIKYUU")]
        M_HIKIAI_TORIHIKISAKI_SEIKYUU[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HIKIAI_TORIHIKISAKI_SEIKYUU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HIKIAI_TORIHIKISAKI_SEIKYUU data);

        int Delete(M_HIKIAI_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HIKIAI_TORIHIKISAKI_SEIKYUU data);

        /// <summary>
        /// �����CD�R�[�h�����ƂɎ����_�������}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_HIKIAI_TORIHIKISAKI_SEIKYUU GetDataByCd(string cd);

        /// <summary>
        /// �Z���̈ꕔ�f�[�^���������@�\
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="data">����搿�����}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_HIKIAI_TORIHIKISAKI_SEIKYUU data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}
