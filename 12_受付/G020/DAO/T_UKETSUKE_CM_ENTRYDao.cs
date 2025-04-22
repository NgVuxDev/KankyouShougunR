// $Id: T_UKETSUKE_CM_ENTRYDao.cs 36306 2014-12-02 04:03:00Z diq@oec-h.com $

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
using System.Data.SqlTypes;
using Seasar.Extension.ADO;
using Seasar.Quill.Attrs;
using System.Data;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Reception.UketsukeKuremuNyuuryoku
{
    [Bean(typeof(T_UKETSUKE_CM_ENTRY))]
    public interface T_UKETSUKE_CM_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_CM_ENTRY data);

        /// <summary>
        /// Update(論理削除)
        /// </summary>
        /// <param name="data">T_UKETSUKE_CM_ENTRY</param>
        /// <returns>件数</returns>
        [NoPersistentProps("KYOTEN_CD","UKETSUKE_NUMBER","UKETSUKE_DATE","TORIHIKISAKI_CD","TORIHIKISAKI_NAME","GYOUSHA_CD","GYOUSHA_NAME","GENBA_CD","GENBA_NAME"
                          ,"EIGYOU_TANTOUSHA_CD","EIGYOU_TANTOUSHA_NAME","TAIOU_END__DATE","TITLE_NAME","SENPOU_TOIAWASE_USER","NAIYOU_1","NAIYOU_2"
                          ,"NAIYOU_3","NAIYOU_4","NAIYOU_5","NAIYOU_6","NAIYOU_7","NAIYOU_8"
                          , "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_CM_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKETSUKE_CM_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Reception.UketsukeKuremuNyuuryoku.Sql.GetUketsukeCMEntryData.sql")]
        T_UKETSUKE_CM_ENTRY[] GetDataForEntity(T_UKETSUKE_CM_ENTRY data);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Reception.UketsukeKuremuNyuuryoku.Sql.GetUketsukeCMEntryData.sql")]
        DataTable GetDataToDataTable(DTOClass data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        /// <summary>
        /// SQL構文からデータの取得を行う
        /// </summary>
        /// <param name="whereSql">作成したSQL文</param>
        /// <returns>取得したDataTable</returns>
        [Sql("/*$sql*/")]
        System.Data.DataTable GetDateForStringSql(string sql);
    }
}
