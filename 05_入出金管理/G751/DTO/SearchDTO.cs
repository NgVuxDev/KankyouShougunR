using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomiShusei
{
    /// <summary>
    /// 出金消込画面検索用DTO
    /// </summary>
    public class SearchDTO
    {
        #region プロパティ
        /// <summary>伝票日付from</summary>
        public string SEISAN_DATE_FROM { get; set; }
        
        /// <summary>伝票日付to</summary>
        public string SEISAN_DATE_TO { get; set; }
　　　　
        /// <summary>取引先CD</summary>
        public string TORIHIKISAKI_CD { get; set; }

        // <summary>取引先名</summary>
        public string TORIHIKISAKI_NAME { get; set; }
　　　　
        /// <summary>開始残高</summary>
        public string Zandaka { get; set; }
        #endregion
    }
}
