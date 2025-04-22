using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Adjustment.Shiharaicheckhyo
{
    public class TorihikisakiDTO
    {
        /// <summary>
        /// 検索条件  :DELETE_FLG
        /// </summary>
        public String Delete_Flag { get; set; }

        /// <summary>
        /// 検索条件  :SHIMEBI1
        /// </summary>
        public String Shimebi1 { get; set; }

        /// <summary>
        /// 検索条件  :SHIMEBI2
        /// </summary>
        public String Shimebi2 { get; set; }

        /// <summary>
        /// 検索条件  :SHIMEBI3
        /// </summary>
        public String Shimebi3 { get; set; }

        /// <summary>
        /// 検索条件  :TORIHIKISAKI_CD
        /// </summary>
        public String TorihikisakiCd { get; set; }
    }

    public class ShimeShoriErrDTO
    {
        /// <summary>
        /// 検索条件  :SHORI_KBN
        /// </summary>
        public String ShoriKbn { get; set; }

        /// <summary>
        /// 検索条件  :CHECK_KBN
        /// </summary>
        public String CheckKbn { get; set; }

        /// <summary>
        /// 検索条件  :DENPYOU_SHURUI_CD
        /// </summary>
        public String DenpyouShuruiCD { get; set; }

        /// <summary>
        /// 検索条件  :SYSTEM_ID
        /// </summary>
        public String SystemId { get; set; }

        /// <summary>
        /// 検索条件  :SEQ
        /// </summary>
        public String Seq { get; set; }

        /// <summary>
        /// 検索条件  :DETAIL_SYSTEM_ID
        /// </summary>
        public String DetailSystemId { get; set; }

        /// <summary>
        /// レコード追加  :SHORI_KBN
        /// </summary>
        public String InsShoriKbn { get; set; }

        /// <summary>
        /// レコード追加  :CHECK_KBN
        /// </summary>
        public String InsCheckKbn { get; set; }

        /// <summary>
        /// レコード追加  :DENPYOU_SYURUI_CD
        /// </summary>
        public String InsDenpyoSyuruiCD { get; set; }

        /// <summary>
        /// レコード追加  :SYSTEM_ID
        /// </summary>
        public String InsSystemID { get; set; }

        /// <summary>
        /// レコード追加  :SEQ
        /// </summary>
        public String InsSeq { get; set; }

        /// <summary>
        /// レコード追加  :MEISAI_SYSTEM_ID
        /// </summary>
        public String InsMeisaiSystemID { get; set; }

        /// <summary>
        /// レコード追加  :GYO_NUMBER
        /// </summary>
        public String InsGyoNo { get; set; }

        /// <summary>
        /// レコード追加  :ERROR_NAIYOU
        /// </summary>
        public String InsErrNaiyo { get; set; }

        /// <summary>
        /// レコード追加  :RIYUU
        /// </summary>
        public String InsRiyu { get; set; }

    }
}
