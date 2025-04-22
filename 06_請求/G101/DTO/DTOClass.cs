using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Billing.SeikyuShimeShori
{
    public class SeikyuShimeShoriDispDto
    {
        //期間締め条件========================
        /// <summary>
        /// 検索条件  :締日(SHIMEBI)
        /// </summary>
        public Int16 SHIMEBI { get; set; }

        //伝票締め条件========================
        /// <summary>
        /// 拠点コード
        /// </summary>
        public int KYOTEN_CD { get; set; }

        /// <summary>
        /// 請求締日FROM
        /// </summary>
        public string SEIKYUSHIMEBI_FROM { get; set; }

        /// <summary>
        /// 請求締日TO
        /// </summary>
        public string SEIKYUSHIMEBI_TO { get; set; }

        /// <summary>
        /// 請求(取引)先コード
        /// </summary>
        public string SEIKYU_CD { get; set; }

        /// <summary>
        /// 伝票番号 ([受入番号] or [出荷番号] or [売上/支払番号])
        /// </summary>
        public Int64 DENPYOU_NUMBER { get; set; }//160013

    }
}
