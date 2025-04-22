using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace r_framework.Dao
{
    /// <summary>
    /// �P�ʃ}�X�^Dao
    /// </summary>
    [Bean(typeof(M_GURUPU_NYURYOKU))]
    public interface IM_GURUPU_NYURYOKUDao : IS2Dao
    {
        /// <summary>
        /// �폜�t���O�������Ă��Ȃ����ׂẴf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [Sql("SELECT * FROM M_GURUPU_NYURYOKU")]
        M_GURUPU_NYURYOKU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.GurupuNyuryoku.IM_GURUPU_NYURYOKUDao_GetAllValidData.sql")]
        M_GURUPU_NYURYOKU[] GetAllValidData(M_GURUPU_NYURYOKU data);

        //20250319
        [SqlFile("r_framework.Dao.SqlFile.GurupuNyuryoku.IM_GURUPU_URIAGEDao_GetAllValidData.sql")]
        M_GURUPU_NYURYOKU[] GetAllValidDataUriage(M_GURUPU_NYURYOKU data);

        [SqlFile("r_framework.Dao.SqlFile.GurupuNyuryoku.IM_GURUPU_SHIHARAIDao_GetAllValidData.sql")]
        M_GURUPU_NYURYOKU[] GetAllValidDataShiharai(M_GURUPU_NYURYOKU data);

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam> 
        [NoPersistentProps("TIME_STAMP", "GURUPU_ID")]  //20250324
        int Insert(M_GURUPU_NYURYOKU data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "TIME_STAMP")] //, "CREATE_PC"
        int Update(M_GURUPU_NYURYOKU data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(M_GURUPU_NYURYOKU data);

        [Sql("select right('000' + convert(varchar, M_GURUPU_NYURYOKU.GURUPU_CD), 3) AS CD,M_GURUPU_NYURYOKU.GURUPU_NAME AS NAME FROM M_GURUPU_NYURYOKU /*$whereSql*/ group by M_GURUPU_NYURYOKU.GURUPU_CD,M_GURUPU_NYURYOKU.GURUPU_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GURUPU_NYURYOKU data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] GURUPU_CD);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GURUPU_CD = /*cd*/")]
        M_GURUPU_NYURYOKU GetDataByCd(string cd);

        //20250321
        [Query("GURUPU_CD = /*cd*/ AND DENPYOU_KBN_CD = /*den_cd*/")]
        M_GURUPU_NYURYOKU GetDataByCdAndDencd(string cd, int den_cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GURUPU_NYURYOKU data, bool deletechuFlg);
    }
}