using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Dto
{
    internal class SearchGetujiCondition
    {

        #region 検索条件
        /// <summary>検索条件</summary>
        public string busyouCD { get; set; }// 部署コード
        public string nendo { get; set; }// 年度
        public string denpyouKbn { get; set; }// 伝票区分
        public DateTime nendoFirstDay { get; set; }// 年度の最初日
        public DateTime nendoLastDay { get; set; }// 年度の最終日
        public string month1 { get; set; }// 年度の1月目(yyyyMM形式)
        public string month2 { get; set; }// 年度の2月目(yyyyMM形式)
        public string month3 { get; set; }// 年度の3月目(yyyyMM形式)
        public string month4 { get; set; }// 年度の4月目(yyyyMM形式)
        public string month5 { get; set; }// 年度の5月目(yyyyMM形式)
        public string month6 { get; set; }// 年度の6月目(yyyyMM形式)
        public string month7 { get; set; }// 年度の7月目(yyyyMM形式)
        public string month8 { get; set; }// 年度の8月目(yyyyMM形式)
        public string month9 { get; set; }// 年度の9月目(yyyyMM形式)
        public string month10 { get; set; }// 年度の10月目(yyyyMM形式)
        public string month11 { get; set; }// 年度の11月目(yyyyMM形式)
        public string month12 { get; set; }// 年度の12月目(yyyyMM形式)
        #endregion
    }
}
