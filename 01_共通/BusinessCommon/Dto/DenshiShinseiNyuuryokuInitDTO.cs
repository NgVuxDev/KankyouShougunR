using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using r_framework.Entity;

namespace Shougun.Core.Common.BusinessCommon.Dto
{
    /// <summary>
    /// 電子申請入力の初期化用DTO
    /// </summary>
    public class DenshiShinseiNyuuryokuInitDTO
    {
        /// <summary>引合取引先CD</summary>
        public string HikiaiTorihikisakiCd { get; set; }

        /// <summary>引合取引先名</summary>
        public string HikiaiTorihikisakiNameRyaku { get; set; }

        /// <summary>引合業者CD</summary>
        public string HikiaiGyoushaCd { get; set; }

        /// <summary>引合業者名</summary>
        public string HikiaiGyoushaNameRyaku { get; set; }

        /// <summary>引合現場CD</summary>
        public string HikiaiGenbaCd { get; set; }

        /// <summary>引合現場名</summary>
        public string HikiaiGenbaNameRyaku { get; set; }

        /// <summary>取引先CD</summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>取引先名</summary>
        public string TorihikisakiNameRyaku { get; set; }

        /// <summary>業者CD</summary>
        public string GyoushaCd { get; set; }

        /// <summary>業者名</summary>
        public string GyoushaNameRyaku { get; set; }

        /// <summary>現場CD</summary>
        public string GenbaCd { get; set; }

        /// <summary>現場名</summary>
        public string GenbaNameRyaku { get; set; }

        /// <summary>申請内容名CD</summary>
        public string NaiyouCd { get; set; }
    }
}
