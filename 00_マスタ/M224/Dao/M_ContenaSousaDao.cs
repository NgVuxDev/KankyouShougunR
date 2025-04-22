using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;

namespace Shougun.Core.Master.ContenaSousaHoshu
{
    [Bean(typeof(M_CONTENA_SOUSA))]
    public interface M_ContenaSousaDao : IS2Dao
    {

        [Sql("SELECT * FROM M_CONTENA_SOUSA")]
        M_CONTENA_SOUSA[] GetAllData();

      
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_CONTENA_SOUSA data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_CONTENA_SOUSA data);

        int Delete(M_CONTENA_SOUSA data);

 
        /// <summary>
        /// ���[�U�w��̌��������ɂ��ꗗ�p�f�[�^�擾
        /// </summary>
        /// <param name="path">SQL�t�@�C���p�X</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        //DataTable GetDataBySqlFile(string path, M_MANIFEST_TEHAI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("CONTENA_SOUSA_CD = /*cd*/")]
        M_CONTENA_SOUSA GetDataByCd(string cd);

        /// <summary>
        /// �R���e�i�󋵉�ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ContenaSousaHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_CONTENA_SOUSA data, bool deletechuFlg);
    }
}
