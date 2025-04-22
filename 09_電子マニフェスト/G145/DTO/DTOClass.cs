using System;

namespace Shougun.Core.ElectronicManifest.ShobunShuryouHoukoku
{
    ////システム設定
    //public class MSIDtoCls
    //{
    //    /// <summary>
    //    /// 検索条件  :ID
    //    /// </summary>
    //    public String SYS_ID { get; set; }

    //    /// <summary>
    //    /// 検索条件  :削除フラグ
    //    /// </summary>
    //    public String DELETE_FLG { get; set; }
    //}

    //マニフェスト
    public class TMEDtoCls
    {
        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public String SYSTEM_ID { get; set; }

        /// <summary>
        /// 検索条件  :枝番
        /// </summary>
        public String SEQ { get; set; }

        /// <summary>
        /// 検索条件  :一括登録区分
        /// </summary>
        public String LIST_REGIST_KBN { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物区分CD
        /// </summary>
        public String HAIKI_KBN_CD { get; set; }

        /// <summary>
        /// 検索条件  :一次マニフェスト区分
        /// </summary>
        public String FIRST_MANIFEST_KBN { get; set; }

        /// <summary>
        /// 検索条件  :パターン名
        /// </summary>
        public String PATTERN_NAME { get; set; }

        /// <summary>
        /// 検索条件  :排出事業者CD
        /// </summary>
        public String JIGYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :排出事業者CD
        /// </summary>
        public String JIGYOUBA_CD { get; set; }

        /// <summary>
        /// 検索条件  :排出事業者名
        /// </summary>
        public String HST_GYOUSHA_NAME { get; set; }

        /// <summary>
        /// 検索条件  :排出事業場名
        /// </summary>
        public String HST_GENBA_NAME { get; set; }

        /// <summary>
        /// 検索条件  :削除フラグ
        /// </summary>
        public String DELETE_FLG { get; set; }

        /// <summary>
        /// 検索条件  :管理番号
        /// </summary>
        public String KANRI_ID { get; set; }

        /// <summary>
        /// 検索条件  :検索cd
        /// </summary>
        public String Search_CD { get; set; }
    }
}
