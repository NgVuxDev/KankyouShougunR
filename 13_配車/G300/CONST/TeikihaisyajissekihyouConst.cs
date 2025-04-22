using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Allocation.Teikihaisyajissekihyou
{
    internal class TeikihaisyajissekihyouConst
    {
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public class ExceptionErrMsg
        {
            public const string HAITA = "排他エラーが発生しました。";
            public const string REIGAI = "例外エラーが発生しました。";
        }

        /// <summary>出力区分</summary>
        public const string Chushutsu = "1 または 2 ";

        /// <summary>印刷時の最大現場数</summary>
        internal static readonly int MaxPrintGenbaNumber = 1000;
    }
}
