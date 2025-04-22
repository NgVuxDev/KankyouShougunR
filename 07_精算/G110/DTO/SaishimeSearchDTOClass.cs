using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Adjustment.Shiharaishimesyori
{
    public class SaishimeSearchDTO
    {
        /// <summary>
        /// 伝票種類CD
        /// </summary>
        public Int16 DENPYOU_SHURUI_CD { get; set; }

        /// <summary>
        /// 処理区分
        /// </summary>
        public Int16 SHORI_KBN { get; set; }

        /// <summary>
        /// 拠点コード
        /// </summary>
        public int KYOTEN_CD { get; set; }

        /// <summary>
        /// 締日
        /// </summary>
        public Int16 SHIMEBI { get; set; }

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
    }
}
