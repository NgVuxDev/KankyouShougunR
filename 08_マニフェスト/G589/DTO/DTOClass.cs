using System;

namespace Shougun.Core.PaperManifest.Himodukeichiran
{
    //マニフェスト
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public String SYSTEM_ID { get; set; }

        /// <summary>
        /// 検索条件  :SEQ
        /// </summary>
        public String SEQ { get; set; }

        /// <summary>
        /// 検索条件  :管理ID
        /// </summary>
        public String KANRI_ID { get; set; }

        /// <summary>
        /// 検索条件  :最終SEQ
        /// </summary>
        public String LATEST_SEQ { get; set; }

        /// <summary>
        /// 検索条件  :抽出対象区分
        /// </summary>
        public String FIRST_MANIFEST_KBN { get; set; }

        /// <summary>
        /// 検索条件  :マニフェスト種類
        /// </summary>
        public String HAIKI_KBN_CD { get; set; }
    }
}
