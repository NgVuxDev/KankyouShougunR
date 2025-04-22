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

namespace Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup
{
    [Bean(typeof(M_OUTPUT_PATTERN))]
    public interface KenshuIchiranJokenShiteiPopupDAOClass : IS2Dao
    {

        /// <summary>
        /// Entityで絞り込んで拠点マスタ値を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetKyotenData.sql")]
        DataTable GetKyotenDataForEntity(KenshuIchiranJokenShiteiPopupDTOClass data);
        
        /// <summary>
        /// Entityで絞り込んで取引先マスタ値を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetTorihikisakiData.sql")]
        DataTable GetTorihikisakiDataForEntity(KenshuIchiranJokenShiteiPopupDTOClass data);

        /// <summary>
        /// Entityで絞り込んで業者マスタ値を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetGyoushaData.sql")]
        DataTable GetGyoushaDataForEntity(KenshuIchiranJokenShiteiPopupDTOClass data);

        /// <summary>
        /// Entityで絞り込んで業者マスタ値を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetGenbaData.sql")]
        DataTable GetGenbaDataForEntity(KenshuIchiranJokenShiteiPopupDTOClass data);

        /// <summary>
        /// Entityで絞り込んで品名マスタ値を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetHinmeiData.sql")]
        DataTable GetHinmeiDataForEntity(KenshuIchiranJokenShiteiPopupDTOClass data);

        /// <summary>
        /// 検収一覧画面専用テーブルの有効データ(DELETE_FLG=false)を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetKenshuAllDataAcceptance.sql")]
        DataTable GetKenshuAllDataAcceptance(KensyuuIchiranDTOCls data);

        /// <summary>
        /// 検収一覧画面専用テーブルの有効データ(DELETE_FLG=false)を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetKenshuAllDataNotAcceptance.sql")]
        DataTable GetKenshuAllDataNotAcceptance(KensyuuIchiranDTOCls data);

        /// <summary>
        /// 検収一覧画面専用テーブルの有効データ(DELETE_FLG=false)を取得する
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetKenshuAllDataNotNeedAcceptance.sql")]
        DataTable GetKenshuAllDataNotNeedAcceptance(KensyuuIchiranDTOCls data);

        /// <summary>
        ///帳票データを取得する
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup.Sql.GetReportData.sql")]
        new DataTable GetReportData(KensyuuIchiranDTOCls data);

        /// <summary>SQL構文からデータの取得を行う</summary>
        /// <param name="sql">作成したSQL文</param>
        /// <returns>データーテーブル</returns>
        [Sql("/*$sql*/")]
        DataTable GetDateForStringSql(string sql);
    }
}
