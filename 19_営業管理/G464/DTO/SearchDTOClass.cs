using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku
{
    internal class JuchuuMokuhyouDto
    {
        /// <summary>
        /// 検索条件  :年度
        /// </summary>
        public string NENDO { get; set; }

        /// <summary>
        /// 検索条件  :年度開始
        /// </summary>
        public string STARTNENDO { get; set; }

        /// <summary>
        /// 検索条件  :年度終了
        /// </summary>
        public string ENDNENDO { get; set; }

        /// <summary>
        /// 拠部署コード
        /// </summary>
        public string BUSHO_CD { get; set; }
    }
}
