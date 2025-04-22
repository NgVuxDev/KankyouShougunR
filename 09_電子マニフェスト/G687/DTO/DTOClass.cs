using System;

namespace Shougun.Core.ElectronicManifest.DenmaniSaishuShobun
{
    //マニフェスト
    public class TMEDtoCls
    {
        /// <summary>
        /// 検索条件  :実行区分
        /// </summary>
        public String pageExeKbn { get; set; }

        /// <summary>
        /// 検索条件  :引渡日FROM
        /// </summary>
        public String HIKIWATASHI_DATE_FROM { get; set; }

        /// <summary>
        /// 検索条件  :引渡日TO
        /// </summary>
        public String HIKIWATASHI_DATE_TO { get; set; }

        /// <summary>
        /// 検索条件  :マニフェスト番号FROM
        /// </summary>
        public String MANIFEST_ID_FROM { get; set; }

        /// <summary>
        /// 検索条件  :マニフェスト番号TO
        /// </summary>
        public String MANIFEST_ID_TO { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物種類CD
        /// </summary>
        public String HAIKI_SHURUI_CD { get; set; }

        /// <summary>
        /// 検索条件  :HAIKI_DAI_CODE
        /// </summary>
        public String HAIKI_DAI_CODE { get; set; }

        /// <summary>
        /// 検索条件  :HAIKI_CHU_CODE
        /// </summary>
        public String HAIKI_CHU_CODE { get; set; }

        /// <summary>
        /// 検索条件  :HAIKI_SHO_CODE
        /// </summary>
        public String HAIKI_SHO_CODE { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物名称CD
        /// </summary>
        public String HAIKI_NAME_CD { get; set; }

        /// <summary>
        /// 検索条件  :排出事業者CD
        /// </summary>
        public String HST_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :排出事業場CD
        /// </summary>
        public String HST_GENBA_CD { get; set; }

        /// <summary>
        /// 検索条件  :収集運搬業者CD
        /// </summary>
        public String UPN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件  :報告処分業者CD
        /// </summary>
        public String SBN_GYOUSHA_CD { get; set; }
    }
    
    /// <summary>
    /// 最大seq取得
    /// </summary>
    public class GetMaxSeqDtoCls
    {
        /// <summary>
        /// 検索条件 :管理番号
        /// </summary>
        public String kanriId { get; set; }

    }
}
