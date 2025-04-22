using System;
using System.Data;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.AnnualUpdates.AnnualUpdatesDEL
{

    [Bean(typeof(M_SYS_INFO))]
    internal interface ConvertDaoCls : IS2Dao
    {
        /// <summary>
        /// テーブルの項目名取得
        /// </summary>
        /// <param name="TableName">元テーブル名(T_xx)</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.AnnualUpdates.AnnualUpdatesDEL.Sql.GetTableColumn.sql")]
        DataTable GetTableColumn(string TableName);

        /// <summary>
        /// テーブルコピー(T_xx→OLD_T_xx)
        /// </summary>
        /// <param name="OldTableName">移動先テーブル名(OLD_T_xx)</param>
        /// <param name="TableKomoku">項目名(xx)</param>
        /// <param name="TableName">元テーブル名(T_xx)</param>
        /// <param name="Joken">WHERE条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.AnnualUpdates.AnnualUpdatesDEL.Sql.InsertOLDTable.sql")]
        int INSERTOldTable(string InsertTableName, string InsertKomoku, string SelectKomoku, string SelectJoken);

        /// <summary>
        /// テーブル物理削除(T_xx)
        /// </summary>
        /// <param name="TableName">元テーブル名(T_xx)</param>
        /// <param name="Joken">WHERE条件</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.AnnualUpdates.AnnualUpdatesDEL.Sql.DeleteTable.sql")]
        int DeleteTable(string TableName, string Joken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Joken"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.AnnualUpdates.AnnualUpdatesDEL.Sql.TableDeleteFLG.sql")]
        DataTable TableDeleteFLG(string TableName, string Joken = "", string KomokuName = "UPDATE_DATE");

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
