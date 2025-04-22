using r_framework.Entity;
using Seasar.Dao.Attrs;
using System.Data;

namespace r_framework.Dao
{
    [Bean(typeof(M_BIKO_UCHIWAKE_NYURYOKU))]
    public interface IM_BIKO_UCHIWAKE_NYURYOKUDao : IS2Dao
    {
        [Sql("SELECT * FROM M_BIKO_UCHIWAKE_NYURYOKU")]
        M_BIKO_UCHIWAKE_NYURYOKU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.BikoUchiwakeNyuryoku.IM_BIKO_UCHIWAKE_NYURYOKUDao_GetAllValidData.sql")]
        M_BIKO_UCHIWAKE_NYURYOKU[] GetAllValidData(M_BIKO_UCHIWAKE_NYURYOKU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_BIKO_UCHIWAKE_NYURYOKU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_BIKO_UCHIWAKE_NYURYOKU data);

        int Delete(M_BIKO_UCHIWAKE_NYURYOKU data);

        [Sql("select M_BIKO_UCHIWAKE_NYURYOKU.BIKO_CD AS CD,M_BIKO_UCHIWAKE_NYURYOKU.BIKO_NOTE AS NOTE FROM M_BIKO_UCHIWAKE_NYURYOKU /*$whereSql*/ group by M_BIKO_UCHIWAKE_NYURYOKU.BIKO_CD,M_BIKO_UCHIWAKE_NYURYOKU.BIKO_NOTE")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_BIKO_UCHIWAKE_NYURYOKU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�i���f�[�^</param>
        /// <returns></returns>
        [Sql("SELECT DISTINCT N'���ϓ���' AS NAME FROM T_MITSUMORI_ENTRY WHERE BIKO_KBN_CD IN /*BIKO_KBN_CD*/('') AND DELETE_FLG = 'False'")]
        DataTable GetDataBySqlFileCheck(string[] BIKO_KBN_CD);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("BIKO_KBN_CD = /*data.BIKO_KBN_CD*/ AND BIKO_CD = /*data.BIKO_CD*/")]
        M_BIKO_UCHIWAKE_NYURYOKU GetDataByCd(M_BIKO_UCHIWAKE_NYURYOKU data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_BIKO_UCHIWAKE_NYURYOKU data); //, bool deletechuFlg
    }
}