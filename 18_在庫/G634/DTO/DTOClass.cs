using System;

namespace Shougun.Core.Stock.ZaikoKanriHyo.DTO
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :dateFrom
        /// </summary>
        public String dateFrom { get; set; }

        /// <summary>
        /// 検索条件  :dateTo
        /// </summary>
        public String dateTo { get; set; }

        /// <summary>
        /// 検索条件  :gyoushaFrom
        /// </summary>
        public String gyoushaFrom { get; set; }

        /// <summary>
        /// 検索条件  :gyoushaTo
        /// </summary>
        public String gyoushaTo { get; set; }

        /// <summary>
        /// 検索条件  :genbaFrom
        /// </summary>
        public String genbaFrom { get; set; }

        /// <summary>
        /// 検索条件  :genbaTo
        /// </summary>
        public String genbaTo { get; set; }

        /// <summary>
        /// 検索条件  :zaikoHinmeiFrom
        /// </summary>
        public String zaikoHinmeiFrom { get; set; }

        /// <summary>
        /// 検索条件  :zaikoHinmeiTo
        /// </summary>
        public String zaikoHinmeiTo { get; set; }
    }

    public class ZaikoDTO
    {
        public Decimal preZaikoRyou { get; set; }

        public Decimal preZaikoKingaku { get; set; }

        public Decimal ukeireRyou { get; set; }

        public Decimal shukkaRyou { get; set; }

        public Decimal chouseiIdouRyou { get; set; }

        public Decimal nowZaikoRyou { get; set; }

        public Decimal nowZaikoKingaku { get; set; }

        public Decimal totalZaikoRyou { get; set; }

        public Decimal totalZaikoKingaku { get; set; }

        public Decimal zaikoTanka { get; set; }

    }

    public class GetsujiDTO
    {
        public string gyoushaCd { get; set; }

        public string genbaCd { get; set; }

        public string zaikoHinmeiCd { get; set; }

        public int year { get; set; }

        public int month { get; set; }

    }
}
