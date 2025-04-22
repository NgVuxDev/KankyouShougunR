using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Billing.SeikyuShimeShori
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
    }
}
