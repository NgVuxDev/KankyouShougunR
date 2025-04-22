using System;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using System.Data;
using r_framework.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.ChiikiIkkatsu
{
    [Bean(typeof(M_GYOUSHA))]
    public interface DAO_M_GYOUSHA : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ChiikiIkkatsu.Sql.GYOUSHA_SEARCH.sql")]
        new DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ChiikiIkkatsu.Sql.GYOUSHA_UPDATE.sql")]
        int UpdateGyoushaChiiki(string GYOUSHA_CD, string CHIIKI_CD, string UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD, string ChiikiMasterName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ChiikiIkkatsu.Sql.CHIIKI_UPDATE.sql")]
        int UpdateChiikiMaster(string CHIIKI_CD_OLD, string CHIIKI_CD_NEW, string UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD);
    }

    [Bean(typeof(M_GENBA))]
    public interface DAO_M_GENBA : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ChiikiIkkatsu.Sql.GENBA_SEARCH.sql")]
        new DataTable GetDataForEntity(DTOClass data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ChiikiIkkatsu.Sql.GENBA_UPDATE.sql")]
        int UpdateGenbaChiiki(string GYOUSHA_CD, string GENBA_CD, string CHIIKI_CD, string UPN_HOUKOKUSHO_TEISHUTSU_CHIIKI_CD, string ChiikiMasterName);
    }

    [Bean(typeof(M_CHIIKI))]
    public interface DAO_M_CHIIKI : IS2Dao
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.ChiikiIkkatsu.Sql.CHIIKI_SEARCH.sql")]
        new DataTable GetDataForEntity(string CHIIKI_CD);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Sql("select CHIIKI_CD FROM M_CHIIKI where CHIIKI_CD = /*cd*/ and DELETE_FLG = 0 ")]
        M_CHIIKI GetDataByCd(string cd);
    }
}
