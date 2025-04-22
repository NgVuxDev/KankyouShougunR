using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_UNPAN_HOUHOU))]
    public interface IM_UNPAN_HOUHOUDao : IS2Dao
    {

        [Sql("SELECT * FROM M_UNPAN_HOUHOU")]
        M_UNPAN_HOUHOU[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.UnpanHouhou.IM_UNPAN_HOUHOUDao_GetAllValidData.sql")]
        M_UNPAN_HOUHOU[] GetAllValidData(M_UNPAN_HOUHOU data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_UNPAN_HOUHOU data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_UNPAN_HOUHOU data);

        int Delete(M_UNPAN_HOUHOU data);

        [Sql("select M_UNPAN_HOUHOU.UNPAN_HOUHOU_CD AS CD,M_UNPAN_HOUHOU.UNPAN_HOUHOU_NAME_RYAKU AS NAME FROM M_UNPAN_HOUHOU /*$whereSql*/ group by M_UNPAN_HOUHOU.UNPAN_HOUHOU_CD,M_UNPAN_HOUHOU.UNPAN_HOUHOU_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_UNPAN_HOUHOU data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("UNPAN_HOUHOU_CD = /*cd*/")]
        M_UNPAN_HOUHOU GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_UNPAN_HOUHOU data, bool deletechuFlg);
    }
}
