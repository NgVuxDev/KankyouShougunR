using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;
namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
{
    [Bean(typeof(T_TEIKI_HAISHA_NIOROSHI))]
    public interface T_TEIKI_HAISHA_NIOROSHIDao : IS2Dao
    {

        [Sql("SELECT * FROM T_TEIKI_HAISHA_NIOROSHI")]
        T_TEIKI_HAISHA_NIOROSHI[] GetAllData();

        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_NIOROSHI data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_NIOROSHI data);

        int Delete(T_TEIKI_HAISHA_NIOROSHI data);

        /// <summary>
        /// SQL�\������f�[�^�̎擾���s��
        /// </summary>
        /// <param name="sql">�쐬����SQL��</param>
        /// <returns>�擾����DataTable</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

        [SqlFile("Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku.Sql.GetTeikiHaishaNioData.sql")]
        DataTable GetTeikiHaishaNioData(NioRoShiDTO data);
    }
}
