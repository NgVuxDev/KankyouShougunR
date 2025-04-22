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

namespace Shougun.Core.SalesPayment.Tairyuichiran
{
    [Bean(typeof(T_KEIRYOU_ENTRY))]
    public interface DAOClass : IS2Dao
    {
        /// <summary>
        /// 受入/出荷 表示用滞留数/台数取得
        /// </summary>
        /// <param name="target">対象（1：受入、2：出荷）</param>
        /// <param name="tairyuKbn">滞留登録区分（0 通常登録　1 滞留登録）</param>
        /// <param name="honjitsuKbn">本日のみ取得する場合は1をセット、それ以外は0</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns>件数</returns>
        [SqlFile("Shougun.Core.SalesPayment.Tairyuichiran.Sql.GetEntryCount.sql")]
        long GetEntryCount(int target, int tairyuKbn, int honjitsuKbn, string kyotenCd);

        /// <summary>
        /// 受入/出荷 表示用数量取得
        /// </summary>
        /// <param name="target">対象（1：受入、2：出荷）</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <returns>数量</returns>
        [SqlFile("Shougun.Core.SalesPayment.Tairyuichiran.Sql.GetNetTotal.sql")]
        decimal GetNetTotal(int target, string kyotenCd);

        /// <summary>
        /// 受入/出荷 データ件数取得
        /// </summary>
        /// <param name="target">対象（1：受入、2：出荷）</param>
        /// <param name="number">受入/出荷番号</param>
        /// <returns>件数</returns>
        [SqlFile("Shougun.Core.SalesPayment.Tairyuichiran.Sql.GetEntryExists.sql")]
        DataTable GetEntryExists(int target, string number);

        /// <summary>
        /// 指定したSQLによるデータ取得
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);

    }
    //internal class DAOClass : IS2Dao
    //{
    //    public int Insert(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Update(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public int Delete(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Data.DataTable GetAllMasterDataForPopup(string whereSql)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public SuperEntity GetDataForEntity(SuperEntity date)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public System.Data.DataTable GetDateForStringSql(string sql)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
