using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using Seasar.Dao.Attrs;
using r_framework.Dao;
using System.Data;
using System.Data.SqlTypes;

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{

    /// <summary>
    /// 交付番号検索
    /// </summary>
    public class GetKoufuDtoCls
    {
        /// <summary>検索条件  :交付番号</summary>
        public string MANIFEST_ID { get; set; }
        /// <summary>検索条件  :廃棄区分コード</summary>
        public SqlInt16 HAIKI_KBN_CD { get; set; }
    }
    /// <summary>
    /// 交付番号検索
    /// </summary>
    public class CheckItaku
    {
        /// <summary>日付</summary>
        public bool chkHidukeError { get; set; }
        /// <summary>業者</summary>
        public bool chkGyoushaError { get; set; }
        /// <summary>現場</summary>
        public bool chkGenbaError { get; set; }
        /// <summary>廃棄</summary>
        public bool[] chkHaikiError { get; set; }
    }
}
