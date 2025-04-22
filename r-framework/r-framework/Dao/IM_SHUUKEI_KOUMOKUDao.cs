using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHUUKEI_KOUMOKU))]
    public interface IM_SHUUKEI_KOUMOKUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHUUKEI_KOUMOKU")]
        M_SHUUKEI_KOUMOKU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ShuukeiKoumoku.IM_SHUUKEI_KOUMOKUDao_GetAllValidData.sql")]
        M_SHUUKEI_KOUMOKU[] GetAllValidData(M_SHUUKEI_KOUMOKU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHUUKEI_KOUMOKU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHUUKEI_KOUMOKU data);

        int Delete(M_SHUUKEI_KOUMOKU data);

        [Sql("select M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_CD as CD,M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU as NAME FROM M_SHUUKEI_KOUMOKU /*$whereSql*/ group by M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_CD,M_SHUUKEI_KOUMOKU.SHUUKEI_KOUMOKU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHUUKEI_KOUMOKU data);

        /// <summary>
        /// PK�L�[�z��̌��������ɂ�鑼�f�[�^�g�p�L������p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="SHUUKEI_KOUMOKU_CD">�W�v����CD���X�g</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] SHUUKEI_KOUMOKU_CD);

        /// <summary>
        /// �R�[�h�����ɏW�v���ڃf�[�^���擾����
        /// </summary>
        /// <parameparam name="cd">�W�v���ڃR�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHUUKEI_KOUMOKU_CD = /*cd*/")]
        M_SHUUKEI_KOUMOKU GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHUUKEI_KOUMOKU data, bool deletechuFlg);
    }
}
