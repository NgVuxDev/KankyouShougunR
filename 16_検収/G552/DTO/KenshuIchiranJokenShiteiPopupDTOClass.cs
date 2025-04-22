using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup
{
    public class KenshuIchiranJokenShiteiPopupDTOClass
    {
        /// <summary>
        /// 拠点マスタ／拠点コード
        /// </summary>
        public SqlInt16 KYOTEN_CD { get; set; }

        /// <summary>
        /// 取引先マスタ／取引先コード
        /// </summary>
        public String TORIHIKISAKI_CD { get; set; }

        /// <summary>
        /// 業者マスタ／業者コード
        /// </summary>
        public String GYOUSHA_CD { get; set; }

        /// <summary>
        /// 現場マスタ／現場コード
        /// </summary>
        public String GENBA_CD { get; set; }

        /// <summary>
        /// 品名マスタ／品名コード
        /// </summary>
        public String HINMEI_CD { get; set; }

        /// <summary>
        ///		売上日付	:	SHUKKA_FROM
        /// </summary>		
        public DateTime SHUKKA_FROM { get; set; }

        /// <summary>
        ///		売上日付	:	SHUKKA_TO	
        /// </summary>		
        public DateTime SHUKKA_TO { get; set; }

        /// <summary>
        ///		売上日付	:	KENSHU_FROM
        /// </summary>		
        public DateTime KENSHU_FROM { get; set; }
        /// <summary>
        ///		売上日付	:	KENSHU_TO	
        /// </summary>		
        public DateTime KENSHU_TO { get; set; }
    }
}
