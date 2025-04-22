using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.Untenshakyudounyuuryoku
{
    class ConstClass
    {

        /// <summary>
        /// 社員マスタのカラム名
        /// </summary>
        public class ColName
        {
            public const string SHAIN_CD = "SHAIN_CD";
            public const string SHAIN_NAME_RYAKU = "SHAIN_NAME_RYAKU";
        }

        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }
    }
}
