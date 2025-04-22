using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.HaikibutuTyoubo.DAO
{
    /// <summary>
    /// 帳票データ取得用Dao
    /// </summary>
    [Bean(typeof(M_GENBA))]
    public interface DaoClass : IS2Dao
    {
        /// <summary>
        /// LAYOUT1(運搬帳票)「紙のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintUnpanChoubo.sql")]
        DataTable GetPrintUnpanChoubo(DTOClass dto);

        /// <summary>
        /// LAYOUT1(運搬帳票)「電子のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintDenshiUnpanChoubo.sql")]
        DataTable GetPrintDenshiUnpanChoubo(DTOClass dto);

        /// <summary>
        /// LAYOUT2(運搬委託帳簿)「紙のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintUnpanItakuChoubo.sql")]
        DataTable GetPrintUnpanItakuChoubo(DTOClass dto);
        
        /// <summary>
        /// LAYOUT2(運搬委託帳簿)「電子のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintDenshiUnpanItakuChoubo.sql")]
        DataTable GetPrintDenshiUnpanItakuChoubo(DTOClass dto);
       
        /// <summary>
        /// LAYOUT4(最終処分委託帳簿)「紙のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintSaishuushobunItakuChoubo.sql")]
        DataTable GetPrintSaishuushobunItakuChoubo(DTOClass dto);
        
        /// <summary>
        /// LAYOUT4(最終処分委託帳簿)「電子のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintDenshiSaishuushobunItakuChoubo.sql")]
        DataTable GetPrintDenshiSaishuushobunItakuChoubo(DTOClass dto);

        /// <summary>
        /// LAYOUT3(最終処分委託帳簿)「紙のみ」「一次完結のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintChuukanshoriChoubo_1.sql")]
        DataTable GetPrintChuukanshoriChoubo_1(DTOClass dto);
        
        /// <summary>
        /// LAYOUT3(最終処分委託帳簿)「紙のみ」「一次完結を除く」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintChuukanshoriChoubo_2.sql")]
        DataTable GetPrintChuukanshoriChoubo_2(DTOClass dto);

        /// <summary>
        /// LAYOUT3(最終処分委託帳簿)「電子のみ」「一次完結のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintDenshiChuukanshoriChoubo_1.sql")]
        DataTable GetPrintDenshiChuukanshoriChoubo_1(DTOClass dto);

        /// <summary>
        /// LAYOUT3(最終処分委託帳簿)「電子のみ」「一次完結を除く」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintDenshiChuukanshoriChoubo_2.sql")]
        DataTable GetPrintDenshiChuukanshoriChoubo_2(DTOClass dto);
        
        /// <summary>
        /// LAYOUT5(最終処分帳簿)「紙のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintSaishuushobunChoubo.sql")]
        DataTable GetPrintSaishuushobunChoubo(DTOClass dto);
        
        /// <summary>
        /// LAYOUT5(最終処分帳簿)「電子のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintDenshiSaishuushobunChoubo.sql")]
        DataTable GetPrintDenshiSaishuushobunChoubo(DTOClass dto);

        /// <summary>
        /// LAYOUT6(収集運搬帳簿)「紙のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintShuushuuunpanChoubo.sql")]
        DataTable GetPrintShuushuuunpanChoubo(DTOClass dto);
        
        /// <summary>
        /// LAYOUT6(収集運搬帳簿)「電子のみ」データ取得用
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns name="DataTable">データテーブル</returns>
        [SqlFile("Shougun.Core.PaperManifest.HaikibutuTyoubo.Sql.PrintDenshiShuushuuunpanChoubo.sql")]
        DataTable GetPrintDenshiShuushuuunpanChoubo(DTOClass dto);
    }
}
