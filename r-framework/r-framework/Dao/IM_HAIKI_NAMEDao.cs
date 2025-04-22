using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_HAIKI_NAME))]
    public interface IM_HAIKI_NAMEDao : IS2Dao
    {

        [Sql("SELECT * FROM M_HAIKI_NAME")]
        M_HAIKI_NAME[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.HaikiName.IM_HAIKI_NAMEDao_GetAllValidData.sql")]
        M_HAIKI_NAME[] GetAllValidData(M_HAIKI_NAME data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_HAIKI_NAME data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_HAIKI_NAME data);

        int Delete(M_HAIKI_NAME data);

        [Sql("select M_HAIKI_NAME.HAIKI_NAME_CD AS CD,M_HAIKI_NAME.HAIKI_NAME_RYAKU AS NAME FROM M_HAIKI_NAME /*$whereSql*/ group by M_HAIKI_NAME.HAIKI_NAME_CD,M_HAIKI_NAME.HAIKI_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] HAIKI_NAME_CD);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_HAIKI_NAME data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("HAIKI_NAME_CD = /*cd*/")]
        M_HAIKI_NAME GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_HAIKI_NAME data, bool deletechuFlg);
    }
}
