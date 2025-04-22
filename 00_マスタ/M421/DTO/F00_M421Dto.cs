using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Master.OboeGakiIkkatuIchiran
{
    public class OboeGakiIkktuImputIchiranDTO
    {
        /// <summary>
        ///	システムID	
        /// </summary>		
        public long  SystemId { get; set; }
        /// <summary>
        ///	画面ID	
        /// </summary>		
        public int GamenId { get; set; }
        /// <summary>
        ///	（新規、修正、削除）	
        /// </summary>		
        public int GamenKbn { get; set; }

        /// <summary>
        ///	伝票番号	
        /// </summary>		
        public long DenpyouNumber { get; set; }

        /// <summary>
        ///	SEQ	
        /// </summary>		
        public int Seq { get; set; }


    }
}
