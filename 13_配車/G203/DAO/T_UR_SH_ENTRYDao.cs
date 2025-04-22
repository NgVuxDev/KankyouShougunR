using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
{
    [Bean(typeof(T_UR_SH_ENTRY))]
    public interface T_UR_SH_ENTRYDao : IS2Dao
    {

        [Sql("SELECT * FROM T_UR_SH_ENTRY")]
        T_UR_SH_ENTRY[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UR_SH_ENTRY data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_UR_SH_ENTRY data);

        int Delete(T_UR_SH_ENTRY data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        [SqlFile("Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku.Sql.GetURSHData.sql")]
        DataTable GetURSHData(T_UR_SH_ENTRY data);
    }
}
