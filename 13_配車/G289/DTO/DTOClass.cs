// $Id: DTOClass.cs 15793 2014-02-10 04:08:44Z nagata $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :SYSTEM_ID
        /// </summary>
        public long SystemId { get; set; }
        /// <summary>
        /// 検索条件 : SEQ
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 検索条件 : 明細システムID
        /// </summary>
        public String DetailSystemId { get; set; }
        /// <summary>
        /// 検索条件 : 定期実績番号
        /// </summary>
        public String TeikiJissekiNumber { get; set; }
        /// <summary>
        /// 検索条件 : 定期配車番号
        /// </summary>
        public String TeikiHaishaNumber { get; set; }
        /// <summary>
        /// 検索条件 : 曜日CD
        /// </summary>
        public int dayCd { get; set; }
        /// <summary>
        /// 検索条件 : 曜日
        /// </summary>
        public string dayYoNi { get; set; }
        /// <summary>
        /// 検索条件 : コース名称CD
        /// </summary>
        public String courseNameCd { get; set; }
        /// <summary>
        /// 検索条件 : 拠点CD
        /// </summary>
        public String kyotenCd { get; set; }
        /// <summary>
        /// 検索条件 :業者CD
        /// </summary>
        public String GyoushaCd { get; set; }
        /// <summary>
        /// 検索条件 :現場CD
        /// </summary>
        public String GenbaCd { get; set; }
        /// <summary>
        /// 検索条件 :品名CD
        /// </summary>
        public String HinmeiCd { get; set; }
        /// <summary>
        /// 検索条件 :単位CD
        /// </summary>
        public int UnitCd { get; set; }
        /// <summary>
        /// 適用基準日
        /// </summary>
        public string tekiyouDate { get; set; }
        /// <summary>
        /// 伝票区分
        /// </summary>
        public int DenpyouKbnCd { get; set; }
        /// <summary>
        /// 回数
        /// </summary>
        public int RoundNo { get; set; }
    }
}
