using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    [Bean(typeof(M_TORIHIKISAKI_SHIHARAI))]
    public interface IM_TORIHIKISAKI_SHIHARAIDao : IS2Dao
    {
        [Sql("SELECT * FROM M_TORIHIKISAKI_SHIHARAI")]
        M_TORIHIKISAKI_SHIHARAI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_TORIHIKISAKI_SHIHARAI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_TORIHIKISAKI_SHIHARAI data);

        int Delete(M_TORIHIKISAKI_SHIHARAI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_TORIHIKISAKI_SHIHARAI data);

        /// <summary>
        ///�����R�[�h�����ƂɎ����_�x�����}�X�^�̃f�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("TORIHIKISAKI_CD = /*cd*/")]
        M_TORIHIKISAKI_SHIHARAI GetDataByCd(string cd);

        /// <summary>
        /// �Z���̈ꕔ�f�[�^���������@�\
        /// </summary>
        /// <param name="path">SQL�t�@�C���̃p�X</param>
        /// <param name="data">�����x�����}�X�^�G���e�B�e�B</param>
        /// <param name="oldPost">���X�֔ԍ�</param>
        /// <param name="oldAddress">���Z��</param>
        /// <param name="newPost">�V�X�֔ԍ�</param>
        /// <param name="newAddress">�V�Z��</param>
        /// <returns></returns>
        int UpdatePartData(string path, M_TORIHIKISAKI_SHIHARAI data, string oldPost, string oldAddress, string newPost, string newAddress);
    }
}
