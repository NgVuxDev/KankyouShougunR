// $Id: T_UKETSUKE_SK_ENTRYDao.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
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

namespace Shougun.Core.Allocation.CarTransferSpot
{
    [Bean(typeof(T_UKETSUKE_SK_ENTRY))]
    public interface T_UKETSUKE_SK_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SK_ENTRY data);

        ///// <summary>
        ///// Update
        ///// </summary>
        ///// <param name="data">T_UKETSUKE_SK_ENTRY</param>
        ///// <returns>件数</returns>
        //[NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        //int Update(T_UKETSUKE_SK_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KYOTEN_CD", "UKETSUKE_NUMBER", "UKETSUKE_DATE", "HAISHA_JOKYO_CD",
            "HAISHA_JOKYO_NAME", "HAISHA_SHURUI_CD", "HAISHA_SHURUI_NAME", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME",
            "GYOUSHA_CD", "GYOUSHA_NAME", "GYOSHA_TEL", "GENBA_CD", "GENBA_NAME", "GENBA_TEL", "TANTOSHA_NAME",
            "TANTOSHA_TEL", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME", "NIZUMI_GYOUSHA_CD", "NIZUMI_GYOUSHA_NAME",
            "NIZUMI_GENBA_CD", "NIZUMI_GENBA_NAME", "EIGYOU_TANTOUSHA_CD", "EIGYOU_TANTOUSHA_NAME", "NIZUMI_DATE",
            "SAGYOU_DATE", "SAGYOU_DATE_BEGIN", "SAGYOU_DATE_END", "GENCHAKU_TIME_CD", "GENCHAKU_TIME_NAME", "GENCHAKU_TIME",
            "SAGYOU_TIME", "SAGYOU_TIME_BEGIN", "SAGYOU_TIME_END", "SHASHU_DAISU_GROUP_NUMBER", "SHASHU_DAISU_NUMBER",
            "SHARYOU_CD", "SHARYOU_NAME", "SHASHU_CD", "SHASHU_NAME", "UNTENSHA_CD", "UNTENSHA_NAME", "HOJOIN_CD",
            "HOJOIN_NAME", "MANIFEST_SHURUI_CD", "MANIFEST_TEHAI_CD", "COURSE_KUMIKOMI_CD", "COURSE_NAME_CD", "HAISHA_SIJISHO_FLG",
            "UKETSUKE_BIKOU1", "UKETSUKE_BIKOU2", "UKETSUKE_BIKOU3", "UNTENSHA_SIJIJIKOU1", "UNTENSHA_SIJIJIKOU2",
            "UNTENSHA_SIJIJIKOU3", "KINGAKU_TOTAL", "SHOUHIZEI_RATE", "TAX_SOTO", "TAX_UCHI", "TAX_SOTO_TOTAL",
            "TAX_UCHI_TOTAL", "SHOUHIZEI_TOTAL", "GOUKEI_KINGAKU_TOTAL", "MAIL_SEND_FLG", "CREATE_USER",
            "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SK_ENTRY data);

        ///// <summary>
        ///// 印刷フラグ更新
        ///// </summary>
        ///// <param name="systemID">システムID</param>
        ///// <param name="SEQ">枝番</param>
        ///// <returns></returns>
        //[SqlFile("Shougun.Core.Reception.UketsukeSyukkaNyuuryoku.Sql.UpdatePrintFlg.sql")]
        //int UpdatePrintFlg(SqlInt64 systemID, int SEQ);

        ///// <summary>
        ///// 車種台数を更新（削除の場合、後の台数を－1）
        ///// </summary>
        ///// <param name="groupNumber">車種台数グループID</param>
        ///// <param name="shashuDaisuNumber">枝番</param>
        ///// <returns></returns>
        //[SqlFile("Shougun.Core.Reception.UketsukeSyukkaNyuuryoku.Sql.UpdateShashuDaisu.sql")]
        //int UpdateShashuDaisuNumber(long groupNumber, Int16 shashuDaisuNumber);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKETSUKE_SK_ENTRY data);

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
        [SqlFile("Shougun.Core.Allocation.CarTransferSpot.Sql.GetUketsukeSKEntryData.sql")]
        T_UKETSUKE_SK_ENTRY[] GetDataForEntity(T_UKETSUKE_SK_ENTRY data);

        ///// <summary>
        ///// 絞り込んで値を取得する
        ///// </summary>
        ///// <param name="data">検索条件</param>
        ///// <returns>DataTable</returns>
        //[SqlFile("Shougun.Core.Reception.UketsukeSyukkaNyuuryoku.Sql.GetUketsukeSKEntryData.sql")]
        //DataTable GetDataToDataTable(DTOClass data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllValidDataForPopUp(SuperEntity data);

        ///// <summary>
        ///// SQL構文からデータの取得を行う
        ///// </summary>
        ///// <param name="whereSql">作成したSQL文</param>
        ///// <returns>取得したDataTable</returns>
        //[Sql("/*$sql*/")]
        //System.Data.DataTable GetDateForStringSql(string sql);

        ///// <summary>
        ///// T_UKEIRE_ENTRY.SYSTEM_IDの最高値を取得する
        ///// </summary>
        ///// <param name="whereSql">絞り込み条件</param>
        ///// <returns>SYSTEM_IDのMAX値</returns>
        //[Sql("select ISNULL(MAX(SYSTEM_ID),1) FROM T_UKEIRE_ENTRY /*$whereSql*/")]
        //SqlInt64 getMaxSystemId(string whereSql);

        ///// <summary>
        ///// モバイル連携可能かチェックし、データを取得する
        ///// </summary>
        ///// <param name="data">検索条件</param>
        ///// <returns>DataTable</returns>
        //[SqlFile("Shougun.Core.Reception.UketsukeSyukkaNyuuryoku.Sql.GetDetailForMiTourokuUketuke.sql")]
        //DataTable GetDataToMRDataTable(DTOClass data);

    }
}
