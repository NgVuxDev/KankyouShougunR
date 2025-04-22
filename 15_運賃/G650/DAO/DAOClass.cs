// $Id: DAOClass.cs 36292 2014-12-02 02:43:29Z fangjk@oec-h.com $
using System;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Carriage.UnchinDaichou
{
    internal class DAOClass : IS2Dao
    {
        public DAOClass()
        {
        }
        #region Utility
        public int Insert(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Update(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public int Delete(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
        {
            throw new NotImplementedException();
        }

        public SuperEntity GetDataForEntity(SuperEntity date)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable GetDateForStringSql(string sql)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    /// <summary>
    /// 運賃DAO
    /// </summary>
    [Bean(typeof(T_UNCHIN_ENTRY))]
    internal interface TUNCHINDao : IS2Dao
    {
        /// <summary>
        /// 検索条件に合った値を取得する
        /// </summary>
        /// <param name="sysId">受入入力．システムID</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Carriage.UnchinDaichou.Sql.GetUnchinDaichou.sql")]
        DataTable GetUnchinDaichou(DTOClass data);
    }
}
