using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku
{
    /// <summary>
    /// 入金消込画面初期化用DTO
    /// 起動元画面のデータを連携するためのもの
    /// </summary>
    public class WindowInitDataDTO
    {
        #region プロパティ
        /// <summary>伝票日付</summary>
        public string DenpyoDate { get; set; }

        /// <summary>入金先CD</summary>
        public string TorihikisakiCd { get; set; }

        /// <summary>入金先名</summary>
        public string TorihikisakiName { get; set; }

        /// <summary>今回入金額</summary>
        public string KonkaiNyuukingaku { get; set; }

        /// <summary>今回割振額</summary>
        public string KonkaiWarifurigaku { get; set; }

        /// <summary>T_NYUUKIN_SUM_ENTRYのNYUUKIN_NUMBER</summary>
        public string NyuukinNumber { get; set; }

        /// <summary>T_NYUUKIN_SUM_ENTRYのSYSTEM_ID</summary>
        public string SumSystemId { get; set; }

        #endregion

        #region メソッド
        /// <summary>
        /// プロパティ名とプロパティ値を文字列で出力
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{ ");
            sb.Append(string.Format("DenpyoDate : {0}", DenpyoDate));
            sb.Append(string.Format(", NyuukinsakiCd : {0}", TorihikisakiCd));
            sb.Append(string.Format(", NyuukinsakiName : {0}", TorihikisakiName));
            sb.Append(string.Format(", KonkaiNyuukingaku : {0}", KonkaiNyuukingaku));
            sb.Append(string.Format(", KonkaiWarifurigaku : {0}", KonkaiWarifurigaku));
            sb.Append(string.Format(", NyuukinNumber : {0}", NyuukinNumber));
            sb.Append(string.Format(", SumSystemId : {0}", SumSystemId));

            sb.Append(" }");

            return base.ToString();
        }
        #endregion
    }
}
