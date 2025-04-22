using System.Data;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using Shougun.Core.Master.ZaicohinnmeiHoshu.DTO;
namespace Shougun.Core.Master.ZaicohinnmeiHoshu.Dao
{
    [Bean(typeof(M_ZAIKO_HINMEI))]
    public interface IMZAIKOHINMEIDao : IS2Dao
    {

        [Sql("SELECT * FROM M_ZAIKO_HINMEI")]
        M_CONTENA_SHURUI[] GetAllData();
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_ZAIKO_HINMEI data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
        int Update(M_ZAIKO_HINMEI data);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(M_ZAIKO_HINMEI data);

        /// <summary>
        /// �R�[�h�����ƂɃf�[�^���擾����
        /// </summary>
        /// <returns>�擾�����f�[�^</returns>
        [Query("ZAIKO_HINMEI_CD = /*cd*/")]
        M_ZAIKO_HINMEI GetDataByCd(string cd);

        /// <summary>
        /// �}�X�^��ʗp�̈ꗗ�f�[�^���擾
        /// </summary>
        /// <param name="data">DTOClass</param>
        /// <param name="tekiyounaiFlg">�K�p���t���O</param>
        /// <param name="deletechuFlg">�폜�t���O</param>
        /// <param name="tekiyougaiFlg">�K�p���ԊO�t���O</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ZaicohinnmeiHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranData(DTOClass data, bool tekiyounaiFlg, bool deletechuFlg, bool tekiyougaiFlg);
      
    }
}
