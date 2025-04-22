using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.KokyakuKarute.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    class KokyakuKaruteConstans
    {
        public enum SEARCH_FLG : int
        {
            NONE = 0,
            /// <summary>取引先</summary>
            TORIHIKISAKI_FLAG = 1,
            /// <summary>業者</summary>
            GYOUSHA_FLAG = 2,
            /// <summary>現場</summary>
            GENBA_FLAG = 3,
            /// <summary>一覧</summary>
            ICHIRAN__FLAG = 4
        }

        /// <summary>
        /// 入出区分
        /// </summary>
        public enum IN_OUT_KBN : int
        {
            /// <summary>受入</summary>
            IN = 1,
            /// <summary>出荷</summary>
            OUT = 2
        }

        /// <summary>設置引揚区分</summary>
        public static readonly Int16 CONTENA_SET_KBN_SECCHI = 1;
        public static readonly Int16 CONTENA_SET_KBN_HIKIAGE = 2;

        /// <summary>コンテナ管理方法(1：数量管理、2：個体管理)</summary>
        public static readonly Int16 CONTENA_KANRI_HOUHOU_SUURYOU = 1;
        public static readonly Int16 CONTENA_KANRI_HOUHOU_KOTAI = 2;
    }
}
