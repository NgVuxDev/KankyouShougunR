using System;
using System.Collections;
using System.Data.SqlTypes;
namespace Shougun.Core.PaperManifest.ManifestKansanSaikeisanIchiran
{
   
    //検索条件格納Dto
    public class SearchInfoDto
    {
        /// <summary>
        /// 検索条件  :拠点CD
        /// </summary>
        public String KYOTEN_CD { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物区分
        /// </summary>
        public String HAIKI_KBN_CD { get; set; }

        /// <summary>
        /// 検索条件  :日付選択区分
        /// 1.交付年月日
        /// 2.運搬終了日
        /// 3.処分終了日
        /// 4.最終処分終了日
        /// </summary>
        public Int16 DATE_KBN { get; set; }

        /// <summary>
        /// 検索条件  :日付/開始日
        /// </summary>
        public String DATE_FR { get; set; }

        /// <summary>
        /// 検索条件  :日付/終了日
        /// </summary>
        public String DATE_TO { get; set; }

        /// <summary>
        /// 検索条件　：排出事業者CD
        /// </summary>
        public String HST_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件　：排出事業場CD
        /// </summary>
        public String HST_GENBA_CD { get; set; }

        /// <summary>
        /// 検索条件　：運搬業者CD
        /// </summary>
        public String UPN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 検索条件　：処分業者CD
        /// </summary>
        public String SBN_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 検索条件　：処分場CD
        /// </summary>
        public String SBN_GENBA_CD { get; set; }//157951

        /// <summary>
        /// 検索条件　：報告書分類CD
        /// </summary>
        public String HOUKOKUSHO_BUNRUI_CD { get; set; }

        /// <summary>
        /// 検索条件　：廃棄物種類CD
        /// </summary>
        public String HAIKI_SHURUI_CD { get; set; }

        /// <summary>
        /// 検索条件　：廃棄物名称CD
        /// </summary>
        public String HAIKI_NAME_CD { get; set; }

        /// <summary>
        /// 検索条件　：帳票数量
        /// </summary>
        public Int16 MANIFEST_REPORT_SUU_KBN { get; set; }

        // <summary>
        /// 検索条件　：処分方法CD
        /// </summary>
        public String SBN_HOUHOU_CD { get; set; }

        // <summary>
        /// 検索条件　：処分方法が未入力のみ表示
        /// </summary>
        public Boolean SHOBUN_CHECK { get; set; }

        /// <summary>
        /// 検索条件を初期化
        /// </summary>
        public void ConditionInit()
        {
            //this.KYOTEN_CD = "''";
            //this.HAIKI_KBN_CD = "''";
            //this.DATE_KBN = 0;
            //this.DATE_FR = "''";
            //this.DATE_TO = "''";
            //this.HST_GYOUSHA_CD = "''";
            //this.HST_GENBA_CD = "''";
            //this.UPN_GYOUSHA_CD = "''";
            //this.SBN_GYOUSHA_CD = "''";
            //this.HOUKOKUSHO_BUNRUI_CD = "''";
            //this.HAIKI_SHURUI_CD = "''";
            //this.HAIKI_NAME_CD = "''";
            this.KYOTEN_CD = string.Empty;
            this.HAIKI_KBN_CD = string.Empty;
            this.DATE_KBN = 0;
            this.DATE_FR = string.Empty;
            this.DATE_TO = string.Empty;
            this.HST_GYOUSHA_CD = string.Empty;
            this.HST_GENBA_CD = string.Empty;
            this.UPN_GYOUSHA_CD = string.Empty;
            this.SBN_GYOUSHA_CD = string.Empty;
            this.HOUKOKUSHO_BUNRUI_CD = string.Empty;
            this.HAIKI_SHURUI_CD = string.Empty;
            this.HAIKI_NAME_CD = string.Empty;
            this.MANIFEST_REPORT_SUU_KBN = 0;
            this.SBN_HOUHOU_CD = string.Empty;
            this.SHOBUN_CHECK = false;
        }
    }

}
