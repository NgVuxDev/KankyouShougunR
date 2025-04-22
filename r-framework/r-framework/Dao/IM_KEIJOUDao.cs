using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_KEIJOU))]
    public interface IM_KEIJOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_KEIJOU")]
        M_KEIJOU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Keijou.IM_KEIJOUDao_GetAllValidData.sql")]
        M_KEIJOU[] GetAllValidData(M_KEIJOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KEIJOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_KEIJOU data);

        int Delete(M_KEIJOU data);

        [Sql("select M_KEIJOU.KEIJOU_CD AS CD,M_KEIJOU.KEIJOU_NAME_RYAKU AS NAME FROM M_KEIJOU /*$whereSql*/ group by M_KEIJOU.KEIJOU_CD,M_KEIJOU.KEIJOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_KEIJOU data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("KEIJOU_CD = /*cd*/")]
        M_KEIJOU GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_KEIJOU data, bool deletechuFlg);
    }
}
