using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_GENCHAKU_TIME))]
    public interface IM_GENCHAKU_TIMEDao : IS2Dao
    {

        [Sql("SELECT * FROM M_GENCHAKU_TIME")]
        M_GENCHAKU_TIME[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.GenchakuTime.IM_GENCHAKU_TIMEDao_GetAllValidData.sql")]
        M_GENCHAKU_TIME[] GetAllValidData(M_GENCHAKU_TIME data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENCHAKU_TIME data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENCHAKU_TIME data);

        int Delete(M_GENCHAKU_TIME data);

        [Sql("select RIGHT('000'+ CONVERT(nvarchar,M_GENCHAKU_TIME.GENCHAKU_TIME_CD), 3) AS CD,M_GENCHAKU_TIME.GENCHAKU_TIME_NAME_RYAKU AS NAME FROM M_GENCHAKU_TIME /*$whereSql*/ group by M_GENCHAKU_TIME.GENCHAKU_TIME_CD,M_GENCHAKU_TIME.GENCHAKU_TIME_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_GENCHAKU_TIME data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("GENCHAKU_TIME_CD = /*cd*/")]
        M_GENCHAKU_TIME GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_GENCHAKU_TIME data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
