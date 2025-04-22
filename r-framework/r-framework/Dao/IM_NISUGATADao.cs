using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_NISUGATA))]
    public interface IM_NISUGATADao : MasterAccess.Base.IMasterAccessDao<M_NISUGATA>
    {

        [Sql("SELECT * FROM M_NISUGATA")]
        M_NISUGATA[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.Nisugata.IM_NISUGATADao_GetAllValidData.sql")]
        M_NISUGATA[] GetAllValidData(M_NISUGATA data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_NISUGATA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_NISUGATA data);

        int Delete(M_NISUGATA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_NISUGATA data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFileCheck(string path, string[] NISUGATA_CD);
        
        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("NISUGATA_CD = /*cd*/")]
        M_NISUGATA GetDataByCd(string cd);

        [Sql("select M_NISUGATA.NISUGATA_CD AS CD,M_NISUGATA.NISUGATA_NAME_RYAKU AS NAME FROM M_NISUGATA /*$whereSql*/ group by M_NISUGATA.NISUGATA_CD,M_NISUGATA.NISUGATA_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_NISUGATA data, bool deletechuFlg);
    }
}
