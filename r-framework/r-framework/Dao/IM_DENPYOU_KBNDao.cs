using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENPYOU_KBN))]
    public interface IM_DENPYOU_KBNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENPYOU_KBN")]
        M_DENPYOU_KBN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenpyouKbn.IM_DENPYOU_KBNDao_GetAllValidData.sql")]
        M_DENPYOU_KBN[] GetAllValidData(M_DENPYOU_KBN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENPYOU_KBN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENPYOU_KBN data);

        int Delete(M_DENPYOU_KBN data);

        [Sql("select M_DENPYOU_KBN.DENPYOU_KBN_CD AS CD,M_DENPYOU_KBN.DENPYOU_KBN_NAME_RYAKU AS NAME FROM M_DENPYOU_KBN /*$whereSql*/ group by M_DENPYOU_KBN.DENPYOU_KBN_CD,M_DENPYOU_KBN.DENPYOU_KBN_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_DENPYOU_KBN data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("DENPYOU_KBN_CD = /*cd*/")]
        M_DENPYOU_KBN GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENPYOU_KBN data, bool deletechuFlg);


        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">�`�[�敪�f�[�^</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] DENPYOU_KBN_CD);
    }
}
