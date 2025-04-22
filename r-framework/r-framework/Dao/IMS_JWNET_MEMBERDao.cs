using System.Data;
using Seasar.Dao.Attrs;
using r_framework.Entity;

namespace r_framework.Dao
{
    [Bean(typeof(MS_JWNET_MEMBER))]
    public interface IMS_JWNET_MEMBERDao : IS2Dao
    {
        [Sql("SELECT * FROM MS_JWNET_MEMBER")]
        MS_JWNET_MEMBER[] GetAllData();

        int Insert(MS_JWNET_MEMBER data);

        int Update(MS_JWNET_MEMBER data);

        int Delete(MS_JWNET_MEMBER data);

        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, MS_JWNET_MEMBER data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("EDI_MEMBER_ID = /*cd*/")]
        MS_JWNET_MEMBER GetDataByCd(string cd);
    }
}
