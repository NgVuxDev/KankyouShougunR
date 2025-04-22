using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_KYOKA))]
    public interface IM_CHIIKIBETSU_KYOKADao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_CHIIKIBETSU_KYOKA")]
        M_CHIIKIBETSU_KYOKA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuKyoka.IM_CHIIKIBETSU_KYOKADao_GetAllValidData.sql")]
        M_CHIIKIBETSU_KYOKA[] GetAllValidData(M_CHIIKIBETSU_KYOKA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_KYOKA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_KYOKA data);

        int Delete(M_CHIIKIBETSU_KYOKA data);

        /// <summary>
        /// ��L�[�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("KYOKA_KBN = /*data.KYOKA_KBN*/ AND GYOUSHA_CD = /*data.GYOUSHA_CD*/ AND GENBA_CD = /*data.GENBA_CD*/ AND CHIIKI_CD = /*data.CHIIKI_CD*/")]
        M_CHIIKIBETSU_KYOKA GetDataByPrimaryKey(M_CHIIKIBETSU_KYOKA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_KYOKA data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_KYOKA data, bool deletechuFlg);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="kyokashoKbn">���؋敪</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFileForKigenKanri(string path, M_CHIIKIBETSU_KYOKA data, string kyokashoKbn);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDataForStringSql(string sql);
    }
}
