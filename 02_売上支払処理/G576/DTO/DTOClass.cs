// $Id: DTOClass.cs 51163 2015-06-01 07:48:00Z chenzz@oec-h.com $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku.DTO
{
    /// <summary>
    /// 伝票確定入力用DTO
    /// </summary>
    public class DTOClass
    {
        /// <summary>
        /// 拠点CD
        /// </summary>
        public SqlInt16 KyotenCD { get; set; }

        /// <summary>
        /// 日付区分
        /// </summary>
        /// <remarks>
        /// 1.伝票日付
        /// 2.入力日付
        /// </remarks>
        public SqlInt16 DateKbn { get; set; }
        
        /// <summary>
        /// 日付From
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// 日付To
        /// </summary>
        public string DateTo { get; set; }

        /// <summary>
        /// 確定区分
        /// </summary>
        /// <remarks>
        /// NULL.全て
        /// 1.確定
        /// 2.未確定
        /// </remarks>
        public SqlInt16 KakuteiKbn { get; set; }

        /// <summary>
        /// 伝票種類:受入
        /// </summary>
        public bool DenpyouShuruiUkeire { get; set; }
        
        /// <summary>
        /// 伝票種類:出荷
        /// </summary>
        public bool DenpyouShuruiShukka { get; set; }
        
        /// <summary>
        /// 伝票種類:売上/支払
        /// </summary>
        public bool DenpyouShuruiUrSh { get; set; }

        /// <summary>
        /// 伝票種類:代納
        /// </summary>
        public bool DenpyouShuruiDainou { get; set; }
        
        /// <summary>
        /// 伝票区分
        /// </summary>
        /// <remarks>
        /// 1.売上
        /// 2.支払
        /// 3.全て
        /// </remarks>
        public SqlInt16 DenpyouKbn { get; set; }
    }
}
