// $Id: DAOClass.cs 14924 2014-01-23 06:25:47Z sys_dev_30 $
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
using Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.DAO
{
    [Bean(typeof(M_KOBETSU_HINMEI_TANKA))]
    public interface DAOClass : IS2Dao
    {

        /// <summary>
        /// 適用開始日で絞り込んだ個別単価品名一覧情報取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetKobetsuHinmeiTankaDataSql.sql")]
        DataTable GetIchiranDataSql(M_KOBETSU_HINMEI_TANKA data,string shuruiCD,string bunruiCD);

        /// <summary>
        /// システムIDの最大値取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetMaxSysIdSql.sql")]
        DataTable GetMaxSysIDDataSql(M_KOBETSU_HINMEI_TANKA data);

        /// <summary>
        /// 個別品名単価追加
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_KOBETSU_HINMEI_TANKA data);
        
        /// <summary>
        /// 個別品名単価更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("DENPYOU_KBN_CD", "TORIHIKISAKI_CD", "GYOUSHA_CD", "GENBA_CD", "HINMEI_CD", "DENSHU_KBN_CD", "UNIT_CD", "UNPAN_GYOUSHA_CD", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GENBA_CD", "SYS_ID", "TEKIYOU_END", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "BIKOU")]
        int Update(M_KOBETSU_HINMEI_TANKA data);

    }

    /// <summary>
    /// 適用終了日更新用DAO
    /// </summary>
    [Bean(typeof(M_KOBETSU_HINMEI_TANKA))]
    public interface UpdateDAOCls : IS2Dao
    {
        [NoPersistentProps("DENPYOU_KBN_CD", "TORIHIKISAKI_CD", "GYOUSHA_CD", "TANKA", "GENBA_CD", "HINMEI_CD", "DENSHU_KBN_CD", "UNIT_CD", "UNPAN_GYOUSHA_CD", "NIOROSHI_GYOUSHA_CD", "NIOROSHI_GENBA_CD", "SYS_ID", "CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP", "BIKOU")]
        int Update(M_KOBETSU_HINMEI_TANKA data);
    }

    ///// <summary>
    ///// 業者情報取得用用Dao
    ///// </summary>
    //[Bean(typeof(M_GYOUSHA))]
    //public interface GyoushaDao : IS2Dao
    //{
    //    [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetUnpanGyoushaDataSql.sql")]
    //    M_GYOUSHA GetUnpanGyoushaData(string unpanKaishaCd);

    //    [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetNioroshiGyoushaDataSql.sql")]
    //    M_GYOUSHA GetNioroshiGyoushaData(string nioroshiGyoushaCd);
    //}

    ///// <summary>
    ///// 現場情報取得用Dao
    ///// </summary>
    //[Bean(typeof(M_GENBA))]
    //public interface GenbaDao : IS2Dao
    //{

    //    [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetNioroshiGenbaDataSql.sql")]
    //    M_GENBA GetUnpanGenbaData(M_GENBA data);

    //    [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetGenbaDataSql.sql")]
    //     M_GENBA GetGenbaData(M_GENBA data);
    //}

    /// <summary>
    /// 取引先情報取得用Dao
    /// </summary>
    [Bean(typeof(M_TORIHIKISAKI))]
    public interface TorihikisakiDao : IS2Dao
    {
        [SqlFile("Shougun.Core.Master.KobestuHinmeiTankaIkkatsu.Sql.GetKyotenDataSql.sql")]
        DataTable GetKyotenData(M_TORIHIKISAKI data);

    }
}
