using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.MasterAccess.Base
{
    /// <summary>
    /// フォーカスアウトチェックで利用するチェックメソッド
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IMasterDataAccess<T>
        where T:Entity.SuperEntity
    {
        T Entity { get; set; }

        /// <summary>
        /// コードチェックandセッティング
        /// </summary>
        /// <returns></returns>
        string CodeCheckAndSetting();
        /// <summary>
        /// コード存在チェック(存在しない場合エラー)
        /// </summary>
        /// <returns></returns>
        string CodePresenceCheck();
        /// <summary>
        /// コード非存在チェック(存在する場合エラー)
        /// </summary>
        /// <returns></returns>
        string CodeDeletedCheck();

    }
}
