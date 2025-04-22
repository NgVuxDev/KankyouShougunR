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

namespace OboegakiIkkatuHoshu.Dao
{
     [Bean(typeof(T_ITAKU_MEMO_IKKATSU_ENTRY))]
    public interface ItakuMemoIkkatsuEntryDAO : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         int Insert(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
         int Update(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_ITAKU_MEMO_IKKATSU_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuEntrySql.sql")]
        new DataTable GetDataForEntryEntity(OboegakiIkkatuHoshuDTO data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSql.sql")]
        new DataTable GetDataForDetailByDenpyouNumberEntity(T_ITAKU_MEMO_IKKATSU_ENTRY data);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSearchSql.sql")]
        new DataTable GetDataForDetailByJyokenEntity(OboegakiIkkatuHoshuDTO data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);   

        [SqlFile("OboegakiIkkatuHoshu.Sql.GetpatternNameSql.sql")]
        DataTable GetpatternName(M_SBNB_PATTERN data);


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
    }

     [Bean(typeof(T_ITAKU_MEMO_IKKATSU_DETAIL))]
     public interface ItakuMemoIkkatsuDetailDAO : IS2Dao
     {
         /// <summary>
         /// Insert
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         int Insert(T_ITAKU_MEMO_IKKATSU_DETAIL data);

         /// <summary>
         /// Update
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC","TIME_STAMP")]
         int Update(T_ITAKU_MEMO_IKKATSU_DETAIL data);

         /// <summary>
         /// Delete
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         int Delete(T_ITAKU_MEMO_IKKATSU_DETAIL data);

         /// <summary>
         /// 使用しない
         /// </summary>
         /// <param name="whereSql"></param>
         /// <returns></returns>
         System.Data.DataTable GetAllMasterDataForPopup(string whereSql);        

         /// <summary>
         /// Entityで絞り込んで値を取得する
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         [SqlFile("OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSql.sql")]
         new DataTable GetDataForDetailByDenpyouNumberEntity(T_ITAKU_MEMO_IKKATSU_ENTRY data);
         /// <summary>
         /// Entityで絞り込んで値を取得する
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         [SqlFile("OboegakiIkkatuHoshu.Sql.GetItakuMemoIkkatsuDetailSearchSql.sql")]
         new DataTable GetDataForDetailByJyokenEntity(OboegakiIkkatuHoshuDTO data);

         /// <summary>
         /// 使用しない
         /// </summary>
         /// <param name="data"></param>
         /// <returns></returns>
         System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

         /// <summary>
         /// SQL構文からデータの取得を行う
         /// </summary>
         /// <param name="sql">作成したSQL分</param>
         /// <returns>取得したDataTable</returns>
         [Sql("/*$sql*/")]
         new DataTable GetDataForStringSql(string sql);
     }

     [Bean(typeof(M_ITAKU_KEIYAKU_OBOE))]
     public interface ItakuKeiyakuOboeDao : IS2Dao
     {
          [SqlFile("OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuOboeByItakuKeiyakuNoToSysId.sql")]
         DataTable GetDataByItakuKeiyakuNo(M_ITAKU_KEIYAKU_OBOE data);

          [SqlFile("OboegakiIkkatuHoshu.Sql.GetItakuKeiyakuOboeMaxSeqSql.sql")]
          DataTable GetMaxSeq(M_ITAKU_KEIYAKU_OBOE data);
         
     }

}
