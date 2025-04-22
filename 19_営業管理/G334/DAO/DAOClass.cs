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
using System.Data;
using Seasar.Dao.Attrs;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Dto;
using Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Entity;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Dao
{
    [Bean(typeof(T_RESULT_SQL))]
    internal interface TorihikiRirekiItiranDao : IS2Dao
    {
        /// <summary>
        /// 取引履歴一覧のデータを取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.Sql.GetTorihikiRirekiItiranData.sql")]
        List<T_RESULT_SQL> GetTorihikiRirekiItiranDataAllForEntity(SearchDTOClass data);
    }

    internal class DAOClass : IS2Dao
    {
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
    }
}
