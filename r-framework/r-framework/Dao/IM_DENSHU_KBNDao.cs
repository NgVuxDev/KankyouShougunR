using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_DENSHU_KBN))]
    public interface IM_DENSHU_KBNDao : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHU_KBN")]
        M_DENSHU_KBN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshuKbn.IM_DENSHU_KBNDao_GetAllValidData.sql")]
        M_DENSHU_KBN[] GetAllValidData(M_DENSHU_KBN data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHU_KBN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHU_KBN data);

        int Delete(M_DENSHU_KBN data);

        [Sql("select M_DENSHU_KBN.DENSHU_KBN_CD as CD,M_DENSHU_KBN.DENSHU_KBN_NAME_RYAKU as NAME FROM M_DENSHU_KBN /*$whereSql*/ group by M_DENSHU_KBN.DENSHU_KBN_CD,M_DENSHU_KBN.DENSHU_KBN_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_DENSHU_KBN data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_DENSHU_KBN data, bool deletechuFlg);

        /// <summary>
        /// �Ј��R�[�h�����Ƀf�[�^�̎擾���s��
        /// </summary>
        /// <parameparam name="cd">�Ј��R�[�h</parameparam>
        /// <returns>�擾�����f�[�^</returns>
        [Query("DENSHU_KBN_CD = /*cd*/")]
        M_DENSHU_KBN GetDataByCd(string cd);
    }
}
