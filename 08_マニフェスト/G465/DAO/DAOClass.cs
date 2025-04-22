using System;
using System.Collections.Generic;
using System.Data;
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
using Shougun.Core.Common.BusinessCommon.Dto;
using System.Data.SqlTypes;
using Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.DTO;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran
{
    [Bean(typeof(T_MANIFEST_ENTRY))]
    public interface GetManifestIchiranDaoCls : IchiranBaseDao
    {
        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.getPaperManifestIchiranData.sql")]
        DataTable GetPaperManifestIchiranData(SearchInfoDto searchInfo);

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.getDenshiManifestIchiranData.sql")]
        DataTable GetDenshiManifestIchiranData(SearchInfoDto searchInfo);

        /// <summary>
        /// sql構文からデータの取得を行う
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns>取得したdatatable</returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.getDenshiManifestKongouHaikiIchiranData.sql")]
        DataTable GetDenshiManifestKongouHaikiIchiranData(SearchInfoDto searchInfo);

        /// <summary>
        /// マニフェスト明細の更新（換算後数量、減容後数量）
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.UpdateMani.sql")]
        int UpdateMani(SqlDecimal KANSAN_SUU, SqlDecimal GENNYOU_SUU, string SBN_HOUHOU_CD, SqlInt64 SYSTEM_ID, SqlInt32 SEQ, SqlInt64 DETAIL_SYSTEM_ID, int type);

        /// <summary>
        /// マニフェストの更新(換算後数量と減容後数量の合計値)
        /// </summary>
        /// <param name="sql">作成したsql文</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.UpdateTotalSuu.sql")]
        int UpdateTotalSuu(SqlInt64 SYSTEM_ID, SqlInt32 SEQ);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.GetDenshiHaikiShuruiByCd.sql")]
        DataTable GetDenshiHaikiShuruiByCd(M_DENSHI_HAIKI_SHURUI data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.GetDenshiHaikiNameByCd.sql")]
        DataTable GetDenshiHaikiNameByCd(M_DENSHI_HAIKI_NAME data);

        /// <summary>
        /// Entityで絞り込んで値を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.DenshiHaikiNameSearchAndCheckSql.sql")]
        DataTable DenshiHaikiNameSearchAndCheckSql(M_DENSHI_HAIKI_NAME data);

        /// <summary>
        ///減容率を取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran.Sql.GetGenYourituSql.sql")]
        DataTable GetGenYourituData(SearchDTOForDTExClass data);
    }
}
