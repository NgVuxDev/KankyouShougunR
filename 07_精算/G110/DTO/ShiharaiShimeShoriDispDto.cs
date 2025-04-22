using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Adjustment.Shiharaishimesyori.DTO
{
    public class ShiharaiShimeShoriDispDto
    {
        /// <summary>
        /// 伝票種類
        /// </summary>
        public int DENPYO_SHURUI { get; set; }

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
        /// 支払締日FROM
        /// </summary>
        public string SHIHARAISHIMEBI_FROM { get; set; }

        /// <summary>
        /// 支払締日TO
        /// </summary>
        public string SHIHARAISHIMEBI_TO { get; set; }

        /// <summary>
        /// 支払先コード
        /// </summary>
        public string SHIHARAI_CD { get; set; }
        /// <summary>
        /// 伝票番号 ([受入番号] or [出荷番号] or [売上/支払番号])
        /// </summary>
        public Int64 DENPYOU_NUMBER { get; set; }//160017
    }
}
