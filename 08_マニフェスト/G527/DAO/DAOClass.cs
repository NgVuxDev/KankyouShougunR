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

namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
{   
    /// <summary>
    /// マニフェスト
    /// </summary>
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface DAOClass: IS2Dao
    {      
        /// <summary>
        ///  1.排出(紙)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetKamiManiHstGyosha.sql")]
        DataTable GetKamiHaishutuData(DTOClass data);

        // 1.排出(電子)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetDenManiHstGyosha.sql")]
        DataTable GetDenHaishutuData(DTOClass data);

        // 2.運搬(紙)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetKamiManiUpnGyosha.sql")]
        DataTable GetKamiUnpanData(DTOClass data);

        // 2.運搬(電子)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetDenManiUpnGyosha.sql")]
        DataTable GetDenUnpanData(DTOClass data);

        // 3.処分(紙)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetKamiManiSyobunGyosha.sql")]
        DataTable GetKamiShobunData(DTOClass data);

        // 3.処分(電子)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetDenManiSyobunGyosha.sql")]
        DataTable GetDenShobunData(DTOClass data);

        // 4.最終(紙)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetKamiManiLastGenba.sql")]
        DataTable GetKamiSaishuuData(DTOClass data);

        // 4.最終(電子)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetDenManiLastGenba.sql")]
        DataTable GetDenSaishuuData(DTOClass data);

        // 5.廃棄(紙)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetKamiManiHaiki.sql")]
        DataTable GetKamiHaikiData(DTOClass data);

        // 5.廃棄(電子)
        [SqlFile("Shougun.Core.PaperManifest.ManifestsuiihyoIchiran.Sql.GetDenManiHaiki.sql")]
        DataTable GetDenHaikiData(DTOClass data);
        /// <summary>
        /// 単位
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT TOP 1 M_UNIT.UNIT_NAME FROM M_SYS_INFO INNER JOIN M_UNIT ON M_SYS_INFO.KANSAN_KIHON_UNIT_CD = M_UNIT.UNIT_CD WHERE M_SYS_INFO.SYS_ID = 0")]
        string GetUnit();
        /// <summary>
        ///  会社名
        /// </summary>
        /// <returns></returns>
        [Sql("SELECT TOP 1 CORP_NAME FROM M_CORP_INFO")]
        string GetCorpName();
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetGyoushay(string sql);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetGenba(string sql);
        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        [Sql("/*$sql*/")]
        DataTable GetHaikiShurui(string sql);
    }
}
