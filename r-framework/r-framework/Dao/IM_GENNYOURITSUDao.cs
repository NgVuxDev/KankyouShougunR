using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_GENNYOURITSU))]
    public interface IM_GENNYOURITSUDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_GENNYOURITSU")]
        M_GENNYOURITSU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Gennyouritsu.IM_GENNYOURITSUDao_GetAllValidData.sql")]
        M_GENNYOURITSU[] GetAllValidData(M_GENNYOURITSU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENNYOURITSU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENNYOURITSU data);

        int Delete(M_GENNYOURITSU data);

        [Sql("select M_GENNYOURITSU.HOUKOKUSHO_BUNRUI_CD,M_GENNYOURITSU.HAIKI_NAME_CD,M_GENNYOURITSU.SHOBUN_HOUHOU_CD,M_GENNYOURITSU.GENNYOURITSU FROM M_GENNYOURITSU /*$whereSql*/")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GENNYOURITSU data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GENNYOURITSU data, bool deletechuFlg);

        /// <summary>
        /// Entity�����Ƀf�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("HOUKOKUSHO_BUNRUI_CD = /*data.HOUKOKUSHO_BUNRUI_CD*/ and HAIKI_NAME_CD = /*data.HAIKI_NAME_CD*/ and SHOBUN_HOUHOU_CD = /*data.SHOBUN_HOUHOU_CD*/")]
        M_GENNYOURITSU GetDataByCd(M_GENNYOURITSU data);

        /// <summary>
        /// ���[�U�w��̍X�V�����ɂ��f�[�^�X�V
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="updateKey"></param>
        /// <returns></returns>
        int UpdateBySqlFile(string path, M_GENNYOURITSU data, M_GENNYOURITSU updateKey);
    }
}
