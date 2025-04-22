using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.DenshiShinseiNaiyouSentakuNyuuryoku
{
    /// <summary>
    /// 申請内容選択入力用DTO
    /// </summary>
    public class DTOClass
    {
        /// <summary>申請内容１</summary>
        public string shinseiKbn1 { get; set; }

        /// <summary>申請内容２</summary>
        public string shinseiKbn2 { get; set; }

        /// <summary>営業担当者CD</summary>
        public string eigyouTantoushaCd { get; set; }

        /// <summary>業者区分</summary>
        public string gyoushaKbn { get; set; }

        /// <summary>業者区分CD</summary>
        public string gyoushaCd { get; set; }
    }
}
