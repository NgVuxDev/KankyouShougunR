using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_SHOBUN_MOKUTEKI))]
    public interface IM_SHOBUN_MOKUTEKIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_SHOBUN_MOKUTEKI")]
        M_SHOBUN_MOKUTEKI[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ShobunMokuteki.IM_SHOBUN_MOKUTEKIDao_GetAllValidData.sql")]
        M_SHOBUN_MOKUTEKI[] GetAllValidData(M_SHOBUN_MOKUTEKI data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_SHOBUN_MOKUTEKI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_SHOBUN_MOKUTEKI data);

        int Delete(M_SHOBUN_MOKUTEKI data);

        [Sql("select M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_CD AS CD,M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_NAME_RYAKU AS NAME FROM M_SHOBUN_MOKUTEKI /*$whereSql*/ group by M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_CD,M_SHOBUN_MOKUTEKI.SHOBUN_MOKUTEKI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_SHOBUN_MOKUTEKI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("SHOBUN_MOKUTEKI_CD = /*cd*/")]
        M_SHOBUN_MOKUTEKI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_SHOBUN_MOKUTEKI data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
    }
}
