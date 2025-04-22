using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_BIKO_SENTAKUSHI_NYURYOKU))]
    public interface IM_BIKO_SENTAKUSHI_NYURYOKUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_BIKO_SENTAKUSHI_NYURYOKU")]
        M_BIKO_SENTAKUSHI_NYURYOKU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.BikoSentakushiNyuryoku.IM_BIKO_SENTAKUSHI_NYURYOKUDao_GetAllValidData.sql")]
        M_BIKO_SENTAKUSHI_NYURYOKU[] GetAllValidData(M_BIKO_SENTAKUSHI_NYURYOKU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BIKO_SENTAKUSHI_NYURYOKU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BIKO_SENTAKUSHI_NYURYOKU data);

        int Delete(M_BIKO_SENTAKUSHI_NYURYOKU data);

        [Sql("select M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_CD AS CD,M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_NOTE AS NOTE FROM M_BIKO_SENTAKUSHI_NYURYOKU /*$whereSql*/ group by M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_CD,M_BIKO_SENTAKUSHI_NYURYOKU.BIKO_NOTE")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BIKO_SENTAKUSHI_NYURYOKU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�i���f�[�^</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'���l�������' AS NAME FROM M_BIKO_UCHIWAKE_NYURYOKU WHERE BIKO_CD IN /*BIKO_CD*/('') " +
             "UNION " +
             "SELECT DISTINCT N'���ϓ���' AS NAME FROM T_MITSUMORI_DETAIL_2 WHERE BIKO_CD IN /*BIKO_CD*/('') ")]
        DataTable GetDataBySqlFileCheck(string[] BIKO_CD);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("BIKO_CD = /*cd*/")]
        M_BIKO_SENTAKUSHI_NYURYOKU GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BIKO_SENTAKUSHI_NYURYOKU data, bool deletechuFlg);
    }
}
