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
using Shougun.Core.Common.BusinessCommon.Dto;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.BusinessCommon.Dao
{
    /// <summary>
    /// 電子系マスタデータ取得DAOクラス
    /// </summary>
    [Bean(typeof(DT_R18))]
    public interface DenshiMasterDataSearchDao : IS2Dao
    {
        //電子系マスタ検索用
        /// <summary>
        /// 電子廃棄物種類コード名称検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiHaikiShuruiSearchAndCheckSql.sql")]
        DataTable GetDenshiHaikiShuruiForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子廃棄物名称コードと名称検索用
        /// </summary>
        //  [M_DENSHI_HAIKI_NAME]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiHaikiNameSearchAndCheckSql.sql")]
        DataTable GetDenshiHaikiNameForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子業者検索用
        /// </summary>
        ///  [M_DENSHI_JIGSHA]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiGyoushaSearchAndCheckSql.sql")]
        DataTable GetDenshiGyoushaForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子事業場検索用Dao
        /// </summary>
        //  [M_DENSHI_JIGYOUJOU]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiGenbaSearchAndCheckSql.sql")]
        DataTable GetDenshiGenbaForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 有害物質検索用
        /// </summary>
        //  [M_DENSHI_YUUGAI_BUSSHITSU]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiYougaibushituSearchAndCheckSql.sql")]
        DataTable GetYougaibutujituForEntity(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 電子担当者検索用
        /// </summary>
        //  [M_DENSHI_TANTOUSHA]
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiTantoushaSearchAndCheckSql.sql")]
        DataTable GetTantoushaForEntity(DenshiSearchParameterDtoCls data);
        /// <summary>
        /// 単位検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiUnitSearchAndCheckSql.sql")]
        DataTable GetDenshiUnitInfo(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 荷姿検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiNISUGATASearchAndCheckSql.sql")]
        DataTable GetDenshiNISUGATAInfo(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 処分方法検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiSBN_HOUHOUSearchAndCheckSql.sql")]
        DataTable GetDenshiSBN_HOUHOUInfo(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 運搬方法検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiUNPAN_HOUHOUSearchAndCheckSql.sql")]
        DataTable GetDenshiUNPAN_HOUHOUInfo(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 車輌検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiCARSearchAndCheckSql.sql")]
        DataTable GetDenshiCARInfo(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 車輌検索用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiSharyouSearchAndCheckSql.sql")]
        DataTable GetDenshiSharyouInfo(DenshiSearchParameterDtoCls data);
        /// <summary>
        /// 交付番号存在チェック用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiManifestNoSearchAndCheckSql.sql")]
        DataTable SearchDenshiManifestNo(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 換算式と換算値を取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.DenshiKansanSearchSql.sql")]
        DataTable GetDenshiKansanInfo(DenshiSearchParameterDtoCls data);

        /// <summary>
        /// 減容率を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.BusinessCommon.Sql.DenshiMasterSql.GetGenYourituSql.sql")]
        DataTable GetGenyouritsu(DenshiSearchParameterDtoCls data);
    }

}
