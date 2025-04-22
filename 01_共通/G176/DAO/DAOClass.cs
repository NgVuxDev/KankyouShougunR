using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.KensakuKekkaIchiran.DAO
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface  DAOClass : IS2Dao
    {
        //public int Insert(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Delete(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
        //{
        //    throw new NotImplementedException();
        //}

        //public SuperEntity GetDataForEntity(SuperEntity date)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
        //{
        //    throw new NotImplementedException();
        //}

        //public System.Data.DataTable GetDateForStringSql(string sql)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [Sql("/*$sql*/")]
        DataTable getdateforstringsql(string sql);
    }
}
