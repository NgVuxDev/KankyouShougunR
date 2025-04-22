using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.APP.Base;
using Seasar.Dao.Attrs;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;

// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Master.GenchakuJikanHoshu.DAO
{
    [Bean(typeof(M_GENCHAKU_TIME))]
    public interface DAOClass : IS2Dao
    {
        [Sql("SELECT * FROM M_GENCHAKU_TIME")]
        M_GENCHAKU_TIME[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.GenchakuTime.IM_GENCHAKU_TIMEDao_GetAllValidData.sql")]
        M_GENCHAKU_TIME[] GetAllValidData(M_GENCHAKU_TIME data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_GENCHAKU_TIME data);


        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_GENCHAKU_TIME data);

        int Delete(M_GENCHAKU_TIME data);

        //[Sql("select M_CONTENA_SHURUI.CONTENA_SHURUI_CD AS CD,M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU AS NAME FROM M_CONTENA_SHURUI /*$whereSql*/ group by  M_CONTENA_SHURUI.CONTENA_SHURUI_CD,M_CONTENA_SHURUI.CONTENA_SHURUI_NAME_RYAKU")]
        //DataTable GetAllMasterDataForPopup(string whereSql);

        [Sql("select M_GENCHAKU_TIME.GENCHAKU_TIME_CD AS CD,M_GENCHAKU_TIME.GENCHAKU_TIME_NAME AS NAME FROM M_GENCHAKU_TIME /*$whereSql*/ group by  M_GENCHAKU_TIME.GENCHAKU_TIME_CD,M_GENCHAKU_TIME.GENCHAKU_TIME_NAME_RYAKU")]
        DataTable GetAllMasterDataForPopup(string whereSql);

        /// <summary>
        /// ユーザ指定の検索条件による一覧用データ取得
        /// </summary>
        /// <param name="path">SQLファイルパス</param>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        DataTable GetDataBySqlFile(string path, M_MANIFEST_TEHAI data);

        /// <summary>
        /// コードをもとにデータを取得する
        /// </summary>
        /// <returns>取得したデータ</returns>
        [Query("GENCHAKU_TIME_CD = /*cd*/")]
        M_GENCHAKU_TIME GetDataByCd(string cd);

        /// <summary>
        /// 現着時間入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="tekiyounaiFlg">適用中フラグ</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <param name="tekiyougaiFlg">適用期間外フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.GenchakuJikanHoshu.Sql.GetIchiranDataSql.sql")]
        DataTable GetIchiranDataSql(M_GENCHAKU_TIME data, bool deletechuFlg);
    }
}
