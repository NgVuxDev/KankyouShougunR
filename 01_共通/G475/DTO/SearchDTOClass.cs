// $Id: SearchDTOClass.cs 11876 2013-12-18 12:27:31Z sys_dev_27 $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Common.ItakuKeiyakuSearch
{
    internal class ItakuKeiyakuSearchDto
    {

        /// <summary>
        /// 検索条件  :排出事業者CD
        /// </summary>
        public string JIGYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :排出事業場CD
        /// </summary>
        public string JIGYOUJOU_CD { get; set; }

        /// <summary>
        /// 検索条件  :運搬受託者CD
        /// </summary>
        public string UNPANSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :処分受託者CD
        /// </summary>
        public string SHOBUNSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :日付範囲
        ///             1:作成日、2:送付日、3:返送日、4:完了日、5:有効期間開始日
        ///             6:有効期間終了日、7:自動更新終了日、8:日付なし
        /// </summary>
        public int DAY_HANI { get; set; }

        /// <summary>
        /// 検索条件  :ステータス
        ///             1:作成、2:送付、3:返送、4:完了、5:解約、6:全て
        /// </summary>
        public int ITAKU_STATUS { get; set; }

        /// <summary>
        /// 検索条件  :日付範囲指定(FROM)
        /// </summary>
        public string DAY_FROM { get; set; }

        /// <summary>
        /// 検索条件  :日付範囲指定(TO)
        /// </summary>
        public string DAY_TO { get; set; }
        
        /// <summary>
        /// 検索条件  :業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :現場CD
        /// </summary>
        public string GENBA_CD { get; set; }
        
        /// <summary>
        /// 検索条件  :検索項目設定(0:排出、1:運搬、2:処分)
        /// </summary>
        public int SHUTOKU_KBN { get; set; }

    }
}
