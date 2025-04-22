using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoPopup
{

    public class DtoCls
    {

        /// <summary>
        /// 検索条件  :提出先
        /// </summary>
        public String TEISHUTUSAKI_KBN { get; set; }
        /// <summary>
        /// 検索条件  :都道府県政令市CD
        /// </summary>
        public String CHIIKI_CD { get; set; }
        /// <summary>
        /// 検索条件  :都道府県政令市NM
        /// </summary>
        public String CHIIKI_NM { get; set; }
        /// <summary>
        /// 検索条件  :混合
        /// </summary>
        public String KONGOU_KBN { get; set; }
        /// <summary>
        /// 検索条件  :交付年月日（From)
        /// </summary>
        public String KOUFU_DATE_FROM { get; set; }
        /// <summary>
        /// 検索条件  :交付年月日（To)
        /// </summary>
        public String KOUFU_DATE_TO { get; set; }
        /// <summary>
        /// 検索条件  :出力区分
        /// </summary>
        public String SHUTURYOKU_KBN { get; set; }
        /// <summary>
        /// 検索条件  :作成日
        /// </summary>
        public String CREAD_DATE { get; set; }
        /// <summary>
        /// 検索条件  :他社運搬許可番号の記載 
        /// </summary>
        public String UPN_KYOKA_KBN { get; set; }
        /// <summary>
        /// 検索条件  :他社処分許可番号の記載
        /// </summary>
        public String SBN_KYOKA_KBN { get; set; }
        /// <summary>
        /// 検索条件  :表題１ 
        /// </summary>
        public String TITLE1 { get; set; }
        /// <summary>
        /// 検索条件  :表題２  
        /// </summary>
        public String TITLE2 { get; set; }
        /// <summary>
        /// 検索条件  :業種 
        /// </summary>
        public String GYOUSHU { get; set; }
        /// <summary>
        /// 検索条件  :提出業者設定
        /// </summary>
        public String GYOUSHASET_KBN1 { get; set; }
        /// <summary>
        /// 検索条件  :提出業者設定 
        /// </summary>
        public String GYOUSHASET_KBN2 { get; set; }
        /// <summary>
        /// 検索条件  :提出業者CD（From)   
        /// </summary>
        public String GYOUSHA_CD_FROM { get; set; }
        /// <summary>
        /// 検索条件  :提出業者CD（To)   
        /// </summary>
        public String GYOUSHA_CD_TO { get; set; }
        /// <summary>
        /// 検索条件  :提出現場CD（From)   
        /// </summary>
        public String GENBA_CD_FROM { get; set; }
        /// <summary>
        /// 検索条件  :提出現場CD（To)   
        /// </summary>
        public String GENBA_CD_TO { get; set; }
        /// <summary>
        /// 検索条件  :現場一括集計  
        /// </summary>
        public String GENBASHUKEI_KBN { get; set; }
        /// <summary>
        /// 検索条件  :事業場の名称
        /// </summary>
        public String JGB_NAME { get; set; }
        /// <summary>
        /// 検索条件  :事業場の所在地  
        /// </summary>
        public String JGB_ADDRESS { get; set; }
        /// <summary>
        ///検索条件    :地域名の印字
        /// </summary>
        public String CHIIKINM_KBN { get; set; }

    }  
}
