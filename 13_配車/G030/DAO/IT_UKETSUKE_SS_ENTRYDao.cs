// $Id: IT_UKETSUKE_SS_ENTRYDao.cs 36292 2014-12-02 02:43:29Z fangjk@oec-h.com $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.TeikiHaishaNyuuryoku
{
    [Bean(typeof(T_UKETSUKE_SS_ENTRY))]
    public interface IT_UKETSUKE_SS_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_UKETSUKE_SS_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う（論理削除）
        /// </summary>
        /// <parameparam name="data">T_UKETSUKE_SS_ENTRY</parameparam>
        /// <returns>件数</returns>
        [NoPersistentProps("KYOTEN_CD", "UKETSUKE_NUMBER", "UKETSUKE_DATE", "HAISHA_JOKYO_CD",
            "HAISHA_JOKYO_NAME", "HAISHA_SHURUI_CD", "HAISHA_SHURUI_NAME", "TORIHIKISAKI_CD", "TORIHIKISAKI_NAME",
            "GYOUSHA_CD", "GYOUSHA_NAME", "GYOSHA_TEL", "GENBA_CD", "GENBA_NAME", "GENBA_TEL", "TANTOSHA_NAME",
            "TANTOSHA_TEL", "UNPAN_GYOUSHA_CD", "UNPAN_GYOUSHA_NAME", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GYOUSHA_NAME",
            "NIOROSHI_GENBA_CD", "NIOROSHI_GENBA_NAME", "EIGYOU_TANTOUSHA_CD", "EIGYOU_TANTOUSHA_NAME", "NIOROSHI_DATE",
            "SAGYOU_DATE", "SAGYOU_DATE_BEGIN", "SAGYOU_DATE_END", "GENCHAKU_TIME_CD", "GENCHAKU_TIME_NAME", "GENCHAKU_TIME",
            "SAGYOU_TIME", "SAGYOU_TIME_BEGIN", "SAGYOU_TIME_END", "SHASHU_DAISU_GROUP_NUMBER", "SHASHU_DAISU_NUMBER",
            "SHARYOU_CD", "SHARYOU_NAME", "SHASHU_CD", "SHASHU_NAME", "UNTENSHA_CD", "UNTENSHA_NAME", "HOJOIN_CD",
            "HOJOIN_NAME", "MANIFEST_SHURUI_CD", "MANIFEST_TEHAI_CD", "CONTENA_SOUSA_CD", "COURSE_KUMIAI_CD",
            "COURSE_NAME_CD", "HAISHA_SIJISHO_FLG", "UKETSUKE_BIKOU1", "UKETSUKE_BIKOU2", "UKETSUKE_BIKOU3",
            "UNTENSHA_SIJIJIKOU1", "UNTENSHA_SIJIJIKOU2", "UNTENSHA_SIJIJIKOU3", "KINGAKU_TOTAL", "SHOUHIZEI_RATE",
            "TAX_SOTO", "TAX_UCHI", "TAX_SOTO_TOTAL", "TAX_UCHI_TOTAL", "SHOUHIZEI_TOTAL", "GOUKEI_KINGAKU_TOTAL",
            "MAIL_SEND_FLG", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_UKETSUKE_SS_ENTRY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">T_UKETSUKE_SS_ENTRY</parameparam>
        int Delete(T_UKETSUKE_SS_ENTRY data);

        /// <summary>
        /// 受付番号をもとに受付（収集）入力のデータを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaNyuuryoku.Sql.GetUketsukeSSEntryData.sql")]
        T_UKETSUKE_SS_ENTRY GetUketsukeSSEntryData(DTOClass data);
    }
}
