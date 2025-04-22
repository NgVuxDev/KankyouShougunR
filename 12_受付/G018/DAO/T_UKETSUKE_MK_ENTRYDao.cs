// $Id: T_UKETSUKE_MK_ENTRYDao.cs 36306 2014-12-02 04:03:00Z diq@oec-h.com $

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

namespace Shougun.Core.Reception.UketsukeMochikomiNyuuryoku
{
    [Bean(typeof(T_UKETSUKE_MK_ENTRY))]
    public interface T_UKETSUKE_MK_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_MK_ENTRY data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("KYOTEN_CD", "UKETSUKE_NUMBER", "YOYAKU_JOKYO_CD", "YOYAKU_JOKYO_NAME", "UKETSUKE_DATE","TORIHIKISAKI_CD", "TORIHIKISAKI_NAME",
            "GYOUSHA_CD", "GYOUSHA_NAME", "GYOSHA_TEL", "GENBA_CD", "GENBA_NAME", "GENBA_TEL", "TANTOSHA_NAME",
            "TANTOSHA_TEL", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_NAME",
            "NIOROSHI_GENBA_CD", "NIOROSHI_GENBA_NAME", "EIGYOU_TANTOUSHA_CD", "EIGYOU_TANTOUSHA_NAME", 
            "SAGYOU_DATE", "GENCHAKU_TIME_CD", "GENCHAKU_TIME_NAME", "GENCHAKU_TIME",
            "SHASHU_DAISU_GROUP_NUMBER", "SHASHU_DAISU_NUMBER",
            "SHARYOU_CD", "SHARYOU_NAME", "SHASHU_CD", "SHASHU_NAME", 
            "UKETSUKE_BIKOU1", "UKETSUKE_BIKOU2", "UKETSUKE_BIKOU3",
            "KINGAKU_TOTAL", "SHOUHIZEI_RATE",
            "TAX_SOTO", "TAX_UCHI", "TAX_SOTO_TOTAL", "TAX_UCHI_TOTAL", 
            "SHOUHIZEI_TOTAL", "GOUKEI_KINGAKU_TOTAL",
            "CREATE_USER", "CREATE_DATE", "CREATE_PC", 
            "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_MK_ENTRY data);
        /// <summary>
        /// 車種台数を更新（削除の場合、後の台数を－1）
        /// </summary>
        /// <param name="groupNumber">車種台数グループID</param>
        /// <param name="shashuDaisuNumber">枝番</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Sql.UpdateShashuDaisu.sql")]
        int UpdateShashuDaisuNumber(long groupNumber, Int16 shashuDaisuNumber);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKETSUKE_MK_ENTRY data);

        /// <summary>
        /// 使用しない
        /// </summary>
        /// <param name="whereSql"></param>
        /// <returns></returns>
        [Obsolete("使用しないでください")]
        System.Data.DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Sql.GetUketsukeMKEntryData.sql")]
        DataTable GetDataToDataTable(DTOClass data);

        /// <summary>
        /// Get Inxs CarryOn request data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Sql.GetCarryOnRequestInxsData.sql")]
        DataTable GetCarryOnRequestInxsData(DTOClass data);

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

        /// <summary>
        /// T_UKEIRE_ENTRY.SYSTEM_IDの最高値を取得する
        /// </summary>
        /// <param name="whereSql">絞り込み条件</param>
        /// <returns>SYSTEM_IDのMAX値</returns>
        [Sql("select ISNULL(MAX(SYSTEM_ID),1) FROM T_UKEIRE_ENTRY /*$whereSql*/")]
        SqlInt64 getMaxSystemId(string whereSql);

        //CongBinh 20210713 #152804 S
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="toriCd"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Sql.GetHinmei3MonthData.sql")]
        DataTable GetHinmei3MonthData(string dateFrom, string dateTo, string toriCd, string gyoushaCd, string genbaCd);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <param name="gyoushaCd"></param>
        /// <param name="genbaCd"></param>
        /// <param name="unpanGyoushaCd"></param>
        /// <param name="sharyouCd"></param>
        /// <param name="shashuCd"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Sql.GetSharyou3MonthData.sql")]
        DataTable GetSharyou3MonthData(string dateFrom, string dateTo, string gyoushaCd, string genbaCd, string unpanGyoushaCd, string sharyouCd, string shashuCd);
        //CongBinh 20210713 #152804 E

        /// <summary>
        /// SEQの最高値を取得する
        /// </summary>
        /// <returns>SEQのMAX値</returns>
        [Sql("SELECT ISNULL(MAX(SEQ),1) FROM T_UKETSUKE_MK_ENTRY WHERE UKETSUKE_NUMBER = /*uketsukeNumber*/")]
        string GetMaxSeq(string uketsukeNumber);

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ送信状況取得
        /// </summary>
        /// <returns>ｼｮｰﾄﾒｯｾｰｼﾞ送信状況</returns>
        [SqlFile("Shougun.Core.Reception.UketsukeMochikomiNyuuryoku.Sql.GetSmsJokyoData.sql")]
        string GetSmsJokyo(string uketsukeNumber);
    }
}
