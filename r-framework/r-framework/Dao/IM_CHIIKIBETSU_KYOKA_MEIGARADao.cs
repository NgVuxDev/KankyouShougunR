using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_KYOKA_MEIGARA))]
    public interface IM_CHIIKIBETSU_KYOKA_MEIGARADao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_CHIIKIBETSU_KYOKA_MEIGARA")]
        M_CHIIKIBETSU_KYOKA_MEIGARA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuKyokaMeigara.IM_CHIIKIBETSU_KYOKA_MEIGARADao_GetAllValidData.sql")]
        M_CHIIKIBETSU_KYOKA_MEIGARA[] GetAllValidData(M_CHIIKIBETSU_KYOKA_MEIGARA data);

        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuKyokaMeigara.IM_CHIIKIBETSU_KYOKA_MEIGARADao_UpdateData.sql")]
        int UpdateData(M_CHIIKIBETSU_KYOKA_MEIGARA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_KYOKA_MEIGARA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_KYOKA_MEIGARA data);

        int Delete(M_CHIIKIBETSU_KYOKA_MEIGARA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_KYOKA_MEIGARA data);

        /// <summary>
        /// �n��ʋ��ԍ�PK�ɂ��폜
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Sql("DELETE FROM M_CHIIKIBETSU_KYOKA_MEIGARA WHERE KYOKA_KBN=/*data.KYOKA_KBN*/ AND GYOUSHA_CD=/*data.GYOUSHA_CD*/ AND GENBA_CD=/*data.GENBA_CD*/ AND CHIIKI_CD=/*data.CHIIKI_CD*/")]
        int DeleteByChiikibetsuKyoka(M_CHIIKIBETSU_KYOKA data);
    }
}
