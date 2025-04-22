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
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, MS_JWNET_MEMBER data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("EDI_MEMBER_ID = /*cd*/")]
        MS_JWNET_MEMBER GetDataByCd(string cd);
    }
}
