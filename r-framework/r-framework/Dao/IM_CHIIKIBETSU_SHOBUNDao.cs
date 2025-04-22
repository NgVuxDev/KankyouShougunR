using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace r_framework.Dao
{
    [Bean(typeof(M_CHIIKIBETSU_SHOBUN))]
    public interface IM_CHIIKIBETSU_SHOBUNDao : IS2Dao
    {
        
        [Sql("SELECT * FROM M_CHIIKIBETSU_SHOBUN")]
        M_CHIIKIBETSU_SHOBUN[] GetAllData();

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("r_framework.Dao.SqlFile.ChiikibetsuShobun.IM_CHIIKIBETSU_SHOBUNDao_GetAllValidData.sql")]
        M_CHIIKIBETSU_SHOBUN[] GetAllValidData(M_CHIIKIBETSU_SHOBUN data);

        [Sql("select M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD AS CD,M_CHIIKIBETSU_SHOBUN.HOUKOKU_SHOBUN_HOUHOU_NAME AS NAME FROM M_CHIIKIBETSU_SHOBUN /*$whereSql*/ group by M_CHIIKIBETSU_SHOBUN.SHOBUN_HOUHOU_CD,M_CHIIKIBETSU_SHOBUN.HOUKOKU_SHOBUN_HOUHOU_NAME")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CHIIKIBETSU_SHOBUN data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_CHIIKIBETSU_SHOBUN data);

        int Delete(M_CHIIKIBETSU_SHOBUN data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_CHIIKIBETSU_SHOBUN data);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        DataTable GetIchiranDataSqlFile(string path, M_CHIIKIBETSU_SHOBUN data, bool deletechuFlg);
    }
}
