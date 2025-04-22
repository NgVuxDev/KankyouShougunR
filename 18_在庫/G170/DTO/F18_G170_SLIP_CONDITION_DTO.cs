using System;

/// <summary>
/// 検索条件DTO
/// </summary>
namespace Shougun.Core.Stock.ZaikoShimeSyori.DTO
{
    public class F18_G170_SLIP_CONDITION_Dto : F18_G170Dto
    {
        /// <summary>
        /// 評価方法名
        /// </summary>
        public String hyoukaHouhou { get; set; }
    }
}
