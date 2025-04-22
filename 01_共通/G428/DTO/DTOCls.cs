using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.TenpyouTankaIkatsuHenkou.DTO
{
    public class DTOCls
    {
        /// <summary>
        /// 検索条件  :伝票種類（1:受入　2:出荷　3:　売上/支払　4:全て）
        /// </summary>
        public String DENPYOU_SHURUI { get; set; }
        /// <summary>
        /// 検索条件 : 拠点CD
        /// </summary>
        public String KYOTEN_CD { get; set; }
        /// <summary>
        /// 検索条件 : 伝票日付開始	
        /// </summary>
        public String DENPYOU_DATE_TO { get; set; }
        /// <summary>
        /// 検索条件 : 伝票日付終了	
        /// </summary>
        public String DENPYOU_DATE_FROM { get; set; }
        /// <summary>
        /// 検索条件 : 作成日付開始
        /// </summary>
        public String CREATE_DATE_TO { get; set; }
        /// <summary>
        /// 検索条件 : 作成日付終了
        /// </summary>
        public String CREATE_DATE_FROM { get; set; }
        /// <summary>
        /// 検索条件 : 取引先CD
        /// </summary>
        public String TORIHIKISAKI_CD { get; set; }
        /// <summary>
        /// 検索条件 : 業者CD
        /// </summary>
        public String GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 現場CD
        /// </summary>
        public String GENBA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 品名CD	
        /// </summary>
        public String HINMEI_CD { get; set; }
        /// <summary>
        /// 検索条件 : 確定区分（1:確定 2: 未確定 ）
        /// </summary>
        public String KAKUTEI_KBN { get; set; }
        /// <summary>
        /// 検索条件  :伝票区分CD（1:売上 2: 支払 ）
        /// </summary>
        public String DENPYOU_KBN_CD { get; set; }
        /// <summary>
        /// 検索条件 : 運搬業者CD	
        /// </summary>
        public String UNPAN_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 荷卸業者CD	
        /// </summary>
        public String NIOROSHI_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 荷卸現場CD	
        /// </summary>
        public String NIOROSHI_GENBA_CD { get; set; }
        /// <summary>
        /// 検索条件 : 単位CD
        /// </summary>
        public String UNIT_CD { get; set; }
        /// <summary>
        /// 検索条件 : システムID
        /// </summary>
        public String SYSTEM_ID { get; set; }
        /// <summary>
        /// 検索条件 : 枝番
        /// </summary>
        public String SEQ { get; set; }

    }
}
