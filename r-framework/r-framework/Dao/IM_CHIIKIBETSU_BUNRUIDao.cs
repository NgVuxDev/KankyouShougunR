using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_BUNRUI))]
    public interface IM_CHIIKIBETSU_BUNRUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CHIIKIBETSU_BUNRUI")]
        M_CHIIKIBETSU_BUNRUI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuBunrui.IM_CHIIKIBETSU_BUNRUIDao_GetAllValidData.sql")]
        M_CHIIKIBETSU_BUNRUI[] GetAllValidData(M_CHIIKIBETSU_BUNRUI data);

        [Sql("select distinct M_CHIIKIBETSU_BUNRUI.HOUKOKU_BUNRUI_CD AS CD,M_CHIIKIBETSU_BUNRUI.HOUKOKU_BUNRUI_NAME AS NAME FROM M_CHIIKIBETSU_BUNRUI /*$whereSql*/ group by M_CHIIKIBETSU_BUNRUI.HOUKOKU_BUNRUI_CD,M_CHIIKIBETSU_BUNRUI.HOUKOKU_BUNRUI_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_BUNRUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_BUNRUI data);

        int Delete(M_CHIIKIBETSU_BUNRUI data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_BUNRUI data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_BUNRUI data, bool deletechuFlg);
    }
}
