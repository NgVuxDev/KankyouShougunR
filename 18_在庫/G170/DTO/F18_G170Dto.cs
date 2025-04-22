using System;

/// <summary>
/// 検索条件DTO
/// </summary>
namespace Shougun.Core.Stock.ZaikoShimeSyori.DTO
{
    public class F18_G170Dto
    {
        /// <summary>
        /// 検索条件:業者コード
        /// </summary>
        public String gyoushaCD { get; set; }

        /// <summary>
        /// 検索条件:現場コード
        /// </summary>
        public String genbaCD { get; set; }

        /// <summary>
        /// 検索条件:締対象期間from
        /// </summary>
        public DateTime simeTaisyouKikanFrom { get; set; }

        /// <summary>
        /// 検索条件:締対象期間to
        /// </summary>
        public DateTime simeTaisyouKikanTo { get; set; }
    }
}
