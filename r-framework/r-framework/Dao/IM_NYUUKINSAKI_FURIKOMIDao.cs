using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NYUUKINSAKI_FURIKOMI))]
    public interface IM_NYUUKINSAKI_FURIKOMIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_NYUUKINSAKI_FURIKOMI")]
        M_NYUUKINSAKI_FURIKOMI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NYUUKINSAKI_FURIKOMI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NYUUKINSAKI_FURIKOMI data);

        int Delete(M_NYUUKINSAKI_FURIKOMI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NYUUKINSAKI_FURIKOMI data);

        /// <summary>
        /// ������CD�����Ƀf�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="nyuukinsakiCd">������R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("NYUUKINSAKI_CD = /*nyuukinsakiCd*/")]
        M_NYUUKINSAKI_FURIKOMI[] GetDataByNyuukinsakiCd(string nyuukinsakiCd);
    }
}
