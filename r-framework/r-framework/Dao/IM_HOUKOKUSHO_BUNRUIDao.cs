using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_HOUKOKUSHO_BUNRUI))]
    public interface IM_HOUKOKUSHO_BUNRUIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_HOUKOKUSHO_BUNRUI")]
        M_HOUKOKUSHO_BUNRUI[] GetAllData();


        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.HoukokushoBunrui.IM_HOUKOKUSHO_BUNRUIDao_GetAllValidData.sql")]
        M_HOUKOKUSHO_BUNRUI[] GetAllValidData(M_HOUKOKUSHO_BUNRUI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HOUKOKUSHO_BUNRUI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HOUKOKUSHO_BUNRUI data);

        int Delete(M_HOUKOKUSHO_BUNRUI data);

        [Sql("select M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD AS CD,M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS NAME FROM M_HOUKOKUSHO_BUNRUI /*$whereSql*/ group by M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD,M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        [Sql("select M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD AS CD,M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_NAME_RYAKU AS NAME,M_CHIIKIBETSU_KYOKA_MEIGARA.KYOKA_KBN,M_CHIIKIBETSU_KYOKA_MEIGARA.GYOUSHA_CD,M_CHIIKIBETSU_KYOKA_MEIGARA.GENBA_CD,M_CHIIKIBETSU_KYOKA_MEIGARA.CHIIKI_CD,M_CHIIKIBETSU_KYOKA_MEIGARA.TOKUBETSU_KANRI_KBN,M_CHIIKIBETSU_KYOKA_MEIGARA.TSUMIKAE FROM M_HOUKOKUSHO_BUNRUI LEFT JOIN M_CHIIKIBETSU_KYOKA_MEIGARA ON M_CHIIKIBETSU_KYOKA_MEIGARA.HOUKOKUSHO_BUNRUI_CD = M_HOUKOKUSHO_BUNRUI.HOUKOKUSHO_BUNRUI_CD /*$whereSql*/ /*$whereSql1*/")]
        DataTable GetAllMasterDataForPopup1(string whereSql, string whereSql1);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] HOUKOKUSHO_BUNRUI_CD);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HOUKOKUSHO_BUNRUI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("HOUKOKUSHO_BUNRUI_CD = /*cd*/")]
        M_HOUKOKUSHO_BUNRUI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_HOUKOKUSHO_BUNRUI data, bool deletechuFlg);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�񍐏�����Entity</param>
        /// <param name="data">�p�������Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranHokokuDataSqlFile(string path, M_HOUKOKUSHO_BUNRUI data_B, M_HAIKI_SHURUI data_S, bool deletechuFlg);
    }
}
