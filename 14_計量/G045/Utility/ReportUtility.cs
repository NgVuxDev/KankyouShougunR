using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Scale.Keiryou.Utility
{
    /// <summary>
    /// 帳票関連のユーティリティ
    /// </summary>
    public class ReportUtility
    {
        /// <summary>
        /// 領収書用ダミーデータ取得
        /// </summary>
        /// <returns></returns>
        public static string[] GetRoushuushoReportDummyData()
        {
            // 敬称1
            string keishou1 = "御中";
            // 敬称2
            string keishou2 = "御中";
            // 但し書き
            string tadashigaki = "処分料";

            string[] dummyData = new string[3];
            dummyData[0] = keishou1;
            dummyData[1] = keishou2;
            dummyData[2] = tadashigaki;

            return dummyData;
        }
    }
}
