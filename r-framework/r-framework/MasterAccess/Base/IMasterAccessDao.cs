using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.MasterAccess.Base
{
    /// <summary>
    /// マスタアクセスで使うテーブルに必要なメソッド基本
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public interface IMasterAccessDao<E> : Dao.IS2Dao
        where E:Entity.SuperEntity 
    {
        /// <summary>
        /// 削除フラグがたっていない適用期間内の情報を取得する
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        E[] GetAllValidData(E entity);

        /// <summary>
        /// 削除フラグが立っていない全データを返す
        /// </summary>
        /// <returns></returns>
        E[] GetAllData();

    }


}
