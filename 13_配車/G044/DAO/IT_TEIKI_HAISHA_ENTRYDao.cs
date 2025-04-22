// $Id: IT_TEIKI_HAISHA_ENTRYDao.cs 5893 2013-11-05 00:21:12Z sys_dev_20 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using r_framework.Entity;
using r_framework.Dao;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei
{
    [Bean(typeof(T_TEIKI_HAISHA_ENTRY))]
    public interface IT_TEIKI_HAISHA_ENTRYDao : IS2Dao
    {
        /// <summary>
        /// Entityを元にインサート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entityを元にアップデート処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        [NoPersistentProps("KYOTEN_CD", "TEIKI_HAISHA_NUMBER", "DENPYOU_DATE", "SAGYOU_DATE", "SAGYOU_BEGIN_HOUR", "SAGYOU_BEGIN_MINUTE", "SAGYOU_END_HOUR", "SAGYOU_END_MINUTE", "COURSE_NAME_CD", "SHARYOU_CD", "SHASHU_CD", "UNTENSHA_CD", "UNPAN_GYOUSHA_CD", "HOJOIN_CD", "FURIKAE_HAISHA_KBN", "DAY_CD", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "UPDATE_USER", "UPDATE_DATE", "UPDATE_PC", "TIME_STAMP")]
        int Update(T_TEIKI_HAISHA_ENTRY data);

        /// <summary>
        /// Entityを元に削除処理を行う
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        int Delete(T_TEIKI_HAISHA_ENTRY data);

        //新規のみ表示
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetShinkiNomiData.sql")]
        new DataTable GetShinkiNomiData(DTOClass data);

        //登録済みも表示
        [SqlFile("Shougun.Core.Allocation.TeikiHaishaIkkatsuSakusei.Sql.GetAllData.sql")]
        new DataTable GetAllData(DTOClass data);

        [Sql("SELECT * FROM T_TEIKI_HAISHA_ENTRY WHERE SYSTEM_ID=/*systemid*/ AND SEQ = /*seq*/")]
        T_TEIKI_HAISHA_ENTRY GetDataByCd(string systemid,string seq);
    }
}
