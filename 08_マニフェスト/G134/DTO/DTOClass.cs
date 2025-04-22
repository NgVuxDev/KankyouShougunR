using System;
using System.Collections;

namespace Shougun.Core.PaperManifest.JissekiHokoku
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
        /// 検索条件  :拠点CD
        /// </summary>
        public String KYOTEN_CD { get; set; }

        /// <summary>
        /// 検索条件  :部門CD
        /// </summary>
        public String BUMON_CD { get; set; }

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
        public ArrayList KANRI_ID { get; set; }
    }

    //報告書分類
    public class HokokushoDtoCls
    {
        /// <summary>
        /// 検索条件  :報告書分類CD
        /// </summary>
        public String HOUKOKUSHO_BUNRUI_CD { get; set; }
    }

    //廃棄物種類
    public class HaikiShuruiDtoCls
    {
        /// <summary>
        /// 検索条件  :廃棄区分
        /// </summary>
        public String HAIKI_KBN_CD { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物種類CD
        /// </summary>
        public String HAIKI_SHURUI_CD { get; set; }
    }

    //廃棄物名称
    public class HaikiNameDtoCls
    {
        /// <summary>
        /// 検索条件  :廃棄物名称CD
        /// </summary>
        public String HAIKI_NAME_CD { get; set; }
    }

    // 20140610 katen 不具合No.4712 start‏
    /// <summary>
    /// 存在するチェック検索条件DTO
    /// </summary>
    public class SearchExistDTOCls
    {
        /// <summary>
        /// 検索条件 :一次マニフェストSYSTEM_ID
        /// </summary>
        public String SYSTEM_ID { get; set; }
        /// <summary>
        /// 検索条件 :管理番号
        /// </summary>
        public String KANRI_ID { get; set; }
        /// <summary>
        /// 検索条件 :交付番号
        /// </summary>
        public String MANIFEST_ID { get; set; }
        /// <summary>
        /// 検索条件 :電子廃棄物種類CD 
        /// </summary>
        public String HAIKI_SHURUI_CD { get; set; }
        /// <summary>
        /// EDI加入者番号(電子廃棄物名称テーブルに複数主キー)
        /// </summary>
        public String EDI_MEMBER_ID { get; set; }
        /// <summary>
        /// 電子廃棄物名称CD
        /// </summary>
        public String HAIKI_NAME_CD { get; set; }

    }
    // 20140610 katen 不具合No.4712 end‏
}
