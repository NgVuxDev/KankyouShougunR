using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using r_framework.Dao;
using r_framework.Entity;
using Seasar.Dao.Attrs;

namespace Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Dao
{
    [Bean(typeof(M_DENSHI_SHINSEI_JYUYOUDO))]
    public interface DaoCls : IS2Dao
    {

        [Sql("SELECT * FROM M_DENSHI_SHINSEI_JYUYOUDO")]
        M_DENSHI_SHINSEI_JYUYOUDO[] GetAllData();

        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <parameparam name="data">Entity</parameparam>
        /// <returns>取得したデータのリスト</returns>
        [SqlFile("r_framework.Dao.SqlFile.DenshiShinseiJyuyoudoHoshu.IM_DENSHI_SHINSEI_JYUYOUDODao_GetAllValidData.sql")]
        M_DENSHI_SHINSEI_JYUYOUDO[] GetAllValidData(M_DENSHI_SHINSEI_JYUYOUDO data);

        [NoPersistentProps("TIME_STAMP")]
        int Insert(M_DENSHI_SHINSEI_JYUYOUDO data);

        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(M_DENSHI_SHINSEI_JYUYOUDO data);

        int Delete(M_DENSHI_SHINSEI_JYUYOUDO data);

        [Sql("select M_DENSHI_SHINSEI_JYUYOUDO.JYUYOUDO_CD AS CD,M_DENSHI_SHINSEI_JYUYOUDO.JYUYOUDO_NAME AS NAME FROM M_DENSHI_SHINSEI_JYUYOUDO /*$whereSql*/ group by  M_DENSHI_SHINSEI_JYUYOUDO.JYUYOUDO_CD,M_DENSHI_SHINSEI_JYUYOUDO.JYUYOUDO_NAME")]
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
        [Query("JYUYOUDO_CD = /*cd*/")]
        M_DENSHI_SHINSEI_JYUYOUDO GetDataByCd(string cd);

        /// <summary>
        /// 重要度入力画面用の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <param name="deletechuFlg">削除フラグ</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Master.DenshiShinseiJyuyoudoHoshu.Sql.GetIchiranDataSql.sql")]
        //[Sql("SELECT * FROM M_DENSHI_SHINSEI_JYUYOUDO")]
        DataTable GetIchiranDataSql(M_DENSHI_SHINSEI_JYUYOUDO data, bool deletechuFlg);
    }
}
