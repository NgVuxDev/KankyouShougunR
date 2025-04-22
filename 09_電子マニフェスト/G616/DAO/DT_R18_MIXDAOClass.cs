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
using System.Data.SqlTypes;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake
{
    /// <summary>
    /// DT_R18_MIX用DAO
    /// </summary>
    [Bean(typeof(DT_R18_MIX))]
    public interface DT_R18_MIXDAOClass : IS2Dao
    {
        [NoPersistentProps("TIME_STAMP")]
        int Insert(DT_R18_MIX data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(DT_R18_MIX data);

        /// <summary>
        /// DT_R18_MIXの情報から混合廃棄物振分画面で必要な情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.Sql.GetDtR18MixAndRelationInfo.sql")]
        DataTable GetDtR18MixAndRelationInfo(DT_R18_MIX data, string EDIMemberID);

        /// <summary>
        /// DT_R18_MIXの情報を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.Sql.GetDtR18Mix.sql")]
        DT_R18_MIX[] GetDtR18Mix(DT_R18_MIX data);

        /// <summary>
        /// 最終処分終了報告(保留)の情報を取得する
        /// </summary>
        /// <param name="kanriId"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.Sql.GetLastSbnSuspend.sql")]
        long GetLastSbnSuspend(string kanriId);
    }

    /// <summary>
    /// 混合種別名検索
    /// </summary>
    [Bean(typeof(M_KONGOU_SHURUI))]
    public interface GetKongouNameDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.Sql.GetKongouName.sql")]
        new DataTable GetDataForEntity(GetKongouNameDtoCls data);
    }

    /// <summary>
    /// 混合廃棄物マスタ検索
    /// </summary>
    [Bean(typeof(M_KONGOU_HAIKIBUTSU))]
    public interface KongouHaikibutuDaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.Sql.GetKongouHaikibutu.sql")]
        new DataTable GetDataForEntity(GetKongouNameDtoCls data);
    }

    /// <summary>
    /// 混合廃棄物マスタ検索
    /// </summary>
    [Bean(typeof(MS_JWNET_MEMBER))]
    public interface EDI_PASSWORD_DaoCls : IS2Dao
    {
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [SqlFile("Shougun.Core.ElectronicManifest.KongouHaikibutsuFuriwake.Sql.GetEDI_PASSWORD.sql")]
        new DataTable GetDataForEntity(string SAI_UPN_SHA_EDI_MEMBER_ID);
    }

    /// <summary>
    /// 電子廃棄物種類マスタ検索
    /// </summary>
    [Bean(typeof(M_DENSHI_HAIKI_SHURUI))]
    public interface DenshiHaikiShuruiDaoCls : IS2Dao
    {
        /// <summary>
        /// 廃棄物種類CDから電子廃棄物種類マスタ取得
        /// </summary>
        /// <param name="haikiShuruiCd">廃棄物種類CD</param>
        /// <returns></returns>
        [Query("HAIKI_SHURUI_CD = /*haikiShuruiCd*/")]
        M_DENSHI_HAIKI_SHURUI GetDataByCd(string haikiShuruiCd);
    }
}
