// $Id: T_UKETSUKE_SS_ENTRYDao.cs 6407 2013-11-08 13:03:22Z sys_dev_18 $

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
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Dao
{
    [Bean(typeof(T_UKEIRE_ENTRY))]
    public interface T_UKEIRE_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKEIRE_ENTRY data);

        ///// <summary>
        ///// Update
        ///// </summary>
        ///// <param name="data">T_UKETSUKE_BP_ENTRY</param>
        ///// <returns>件数</returns>
        //[NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        //int Update(T_UKEIRE_ENTRY data);

        /// <summary>
        /// Update(論理削除)
        /// </summary>
        /// <param name="data">T_UKEIRE_ENTRY</param>
        /// <returns>件数</returns>
        [NoPersistentProps( "TAIRYUU_KBN",
                            "KYOTEN_CD",
                            "UKEIRE_NUMBER",
                            "DATE_NUMBER",
                            "YEAR_NUMBER",
                            "KAKUTEI_KBN",
                            "DENPYOU_DATE",
                            "URIAGE_DATE",
                            "SHIHARAI_DATE",
                            "TORIHIKISAKI_CD",
                            "TORIHIKISAKI_NAME",
                            "GYOUSHA_CD",
                            "GYOUSHA_NAME",
                            "GENBA_CD",
                            "GENBA_NAME",
                            "NIOROSHI_GYOUSHA_CD",
                            "NIOROSHI_GYOUSHA_NAME",
                            "NIOROSHI_GENBA_CD",
                            "NIOROSHI_GENBA_NAME",
                            "EIGYOU_TANTOUSHA_CD",
                            "EIGYOU_TANTOUSHA_NAME",
                            "NYUURYOKU_TANTOUSHA_CD",
                            "NYUURYOKU_TANTOUSHA_NAME",
                            "SHARYOU_CD",
                            "SHARYOU_NAME",
                            "SHASHU_CD",
                            "SHASHU_NAME",
                            "UNPAN_GYOUSHA_CD",
                            "UNPAN_GYOUSHA_NAME",
                            "UNTENSHA_CD",
                            "UNTENSHA_NAME",
                            "NINZUU_CNT",
                            "KEITAI_KBN_CD",
                            "DAIKAN_KBN",
                            "CONTENA_SOUSA_CD",
                            "MANIFEST_SHURUI_CD",
                            "MANIFEST_TEHAI_CD",
                            "DENPYOU_BIKOU",
                            "TAIRYUU_BIKOU",
                            "UKETSUKE_NUMBER",
                            "KEIRYOU_NUMBER",
                            "RECEIPT_NUMBER",
                            "NET_TOTAL",
                            "URIAGE_SHOUHIZEI_RATE",
                            "URIAGE_KINGAKU_TOTAL",
                            "URIAGE_TAX_SOTO",
                            "URIAGE_TAX_UCHI",
                            "URIAGE_TAX_SOTO_TOTAL",
                            "URIAGE_TAX_UCHI_TOTAL",
                            "HINMEI_URIAGE_KINGAKU_TOTAL",
                            "HINMEI_URIAGE_TAX_SOTO_TOTAL",
                            "HINMEI_URIAGE_TAX_UCHI_TOTAL",
                            "SHIHARAI_SHOUHIZEI_RATE",
                            "SHIHARAI_KINGAKU_TOTAL",
                            "SHIHARAI_TAX_SOTO",
                            "SHIHARAI_TAX_UCHI",
                            "SHIHARAI_TAX_SOTO_TOTAL",
                            "SHIHARAI_TAX_UCHI_TOTAL",
                            "HINMEI_SHIHARAI_KINGAKU_TOTAL",
                            "HINMEI_SHIHARAI_TAX_SOTO_TOTAL",
                            "HINMEI_SHIHARAI_TAX_UCHI_TOTAL",
                            "URIAGE_ZEI_KEISAN_KBN_CD",
                            "URIAGE_ZEI_KBN_CD",
                            "URIAGE_TORIHIKI_KBN_CD",
                            "SHIHARAI_ZEI_KEISAN_KBN_CD",
                            "SHIHARAI_ZEI_KBN_CD",
                            "SHIHARAI_TORIHIKI_KBN_CD",
                            "CREATE_USER",
                            "CREATE_DATE",
                            "CREATE_PC",
                            "TIME_STAMP"
                            )]
        int Update(T_UKEIRE_ENTRY data);

        ///// <summary>
        ///// 論理削除
        ///// </summary>
        ///// <param name="data">T_UKETSUKE_BP_ENTRY</param>
        ///// <returns>件数</returns>
        //[PersistentProps("SYSTEM_ID", "SEQ", "DELETE_FLG", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        //int Update(T_UKEIRE_ENTRY data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_UKEIRE_ENTRY data);

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
        [SqlFile("Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Sql.GetUkeireEntryData.sql")]
        T_UKEIRE_ENTRY[] GetDataForEntity(T_UKEIRE_ENTRY data);

        /// <summary>
        /// 絞り込んで値を取得する
        /// </summary>
        /// <param name="data">検索条件</param>
        /// <returns>DataTable</returns>
        [SqlFile("Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Sql.GetUkeireEntryData.sql")]
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

        /// <summary>
        /// T_UKEIRE_ENTRY.SYSTEM_IDの最高値を取得する
        /// </summary>
        /// <param name="whereSql">絞り込み条件</param>
        /// <returns>SYSTEM_IDのMAX値</returns>
        [Sql("select ISNULL(MAX(SYSTEM_ID),1) FROM T_UKEIRE_ENTRY /*$whereSql*/")]
        SqlInt64 getMaxSystemId(string whereSql);
    }
}
