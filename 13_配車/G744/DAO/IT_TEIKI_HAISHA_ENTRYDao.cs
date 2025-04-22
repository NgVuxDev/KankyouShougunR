// $Id: IT_TEIKI_HAISHA_ENTRYDao.cs 36292 2014-12-02 02:43:29Z fangjk@oec-h.com $
using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using System.Data.SqlTypes;
namespace Shougun.Core.Allocation.CarTransferTeiki
{
    /// <summary>
    /// ����z�ԃ}�X�^Dao
    /// </summary>
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface IT_TEIKI_HAISHA_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT * FROM T_TEIKI_HAISHA_ENTRY")]
        T_TEIKI_HAISHA_ENTRY[] GetAllData();

        /// <summary>
        /// Entity�����ɃC���T�[�g�������s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entity�����ɃA�b�v�f�[�g�������s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entity�����ɍ폜�������s��
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        int Delete(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// �z�Ԕԍ��ɂ��A����z�ԏ����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetAllValidDataByHaishaNumber.sql")]
        T_TEIKI_HAISHA_ENTRY[] GetAllValidDataByHaishaNumber(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// �폜�t���O�������Ă��Ȃ��K�p���ԓ��̏����擾����
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>�擾�����f�[�^�̃��X�g</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetAllValidData.sql")]
        T_TEIKI_HAISHA_ENTRY[] GetAllValidData(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// ����z�Ԍ����|�b�v�A�b�v��ʗp�̃f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetAllDataSql.sql")]
        new DataTable GetAllTeikiData(DTOClass data);

        /// <summary>
        /// ���o�C���A�g�\���`�F�b�N���A�f�[�^���擾����
        /// </summary>
        /// <param name="data">��������</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetDetailForMiTourokuHaisha.sql")]
        DataTable GetDataToMRDataTable(DTOClass data);

        /// <summary>
        /// ����z�ԉ׍~�s�擾
        /// </summary>
        /// <param name="HAISHA_DENPYOU_NO">HAISHA_DENPYOU_NO</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetMobilNioroshiData.sql")]
        DataTable GetMobilNioroshiData(SqlInt64 TEIKI_HAISHA_NUMBER, int NIOROSHI_NUMBER);

        /// <summary>
        /// ����z�ԉ׍~�f�[�^���擾
        /// </summary>
        /// <param name="SYSTEM_ID">SYSTEM_ID</param>
        /// <param name="SEQ">SEQ</param>
        /// <param name="NIOROSHI_NUMBER">NIOROSHI_NUMBER</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.CarTransferTeiki.Sql.GetTeikiHaishaNioroshiData.sql")]
        DataTable GetTeikiHaishaNioroshiData(SqlInt64 SYSTEM_ID, SqlInt32 SEQ, SqlInt32 NIOROSHI_NUMBER);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="whereSql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);
    }
}
