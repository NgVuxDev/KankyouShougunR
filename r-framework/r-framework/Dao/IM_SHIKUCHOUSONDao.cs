using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHIKUCHOUSON))]
    public interface IM_SHIKUCHOUSONDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHIKUCHOUSON")]
        M_SHIKUCHOUSON[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Shikuchouson.IM_SHIKUCHOUSONDao_GetAllValidData.sql")]
        M_SHIKUCHOUSON[] GetAllValidData(M_SHIKUCHOUSON data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHIKUCHOUSON data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHIKUCHOUSON data);

        int Delete(M_SHIKUCHOUSON data);

        [Sql("select M_SHIKUCHOUSON.SHIKUCHOUSON_CD AS CD,M_SHIKUCHOUSON.SHIKUCHOUSON_NAME_RYAKU AS NAME FROM M_SHIKUCHOUSON /*$whereSql*/ group by M_SHIKUCHOUSON.SHIKUCHOUSON_CD,M_SHIKUCHOUSON.SHIKUCHOUSON_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHIKUCHOUSON data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHIKUCHOUSON_CD = /*cd*/")]
        M_SHIKUCHOUSON GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHIKUCHOUSON data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
