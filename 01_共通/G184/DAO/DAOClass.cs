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
using Shougun.Core.Common.ContenaShitei.DTO;


// http://s2dao.net.seasar.org/ja/index.html

namespace Shougun.Core.Common.ContenaShitei.DAO
{

    [Bean(typeof(T_CONTENA_RESULT))]
    public interface SetCONRETDaoCls : IS2Dao
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("TIME_STAMP")]
        int Insert(T_CONTENA_RESULT data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NoPersistentProps("CREATE_USER", "CREATE_DATE", "CREATE_PC", "TIME_STAMP")]
        int Update(T_CONTENA_RESULT data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Delete(T_CONTENA_RESULT data);
    }

    /// <summary>
    /// チェック用のコンテナ情報取得DAO
    /// </summary>
    [Bean(typeof(SearchResultDto))]
    public interface CheckCONRETDaoCls : IS2Dao
    {
        /// <summary>
        /// 設置コンテナ一覧画面用(固体管理)の一覧データを取得
        /// </summary>
        /// <param name="data">Entity</param>
        /// <returns></returns>
        [SqlFile("Shougun.Core.Common.ContenaShitei.Sql.GetJissekiContenaDataSql.sql")]
        List<SearchResultDto> GetJissekiContenaDataSql(SearchConditionDto data);

        /// <summary>
        /// コンテナ検索ポップアップ用の一覧データを取得
        /// </summary>
        /// <param name="dto">検索条件DTO</param>
        /// <returns>検索結果のリスト</returns>
		[SqlFile("Shougun.Core.Common.ContenaShitei.Sql.GetContenaData.sql")]
		List<SearchResultDto> GetContenaData(SearchConditionDto dto);
	}

}