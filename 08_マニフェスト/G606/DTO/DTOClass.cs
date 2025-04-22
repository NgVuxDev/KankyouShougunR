using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using System.Data.SqlTypes;

namespace Shougun.Core.PaperManifest.JissekiHokokuUnpan
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SearchDto
    {
        /// <summary>報告担当者名</summary>
        public string HOUKOKU_TANTOUSHA_NAME { get; set; }

        /// <summary>電子マニフェスト</summary>
        public int DENSHI_MANIFEST_KBN { get; set; }

        /// <summary>CSV集計区分</summary>
        public int CSV_SHUKEI_KBN { get; set; }

        /// <summary>帳票名</summary>
        public string CHOUHYO_NAME { get; set; }

        /// <summary>提出日</summary>
        public DateTime TEISHUTU_DATE { get; set; }

        /// <summary>報告事業者CD</summary>
        public string HOUKOKU_JIGYOUSHA_CD { get; set; }

        /// <summary>自社業種区分</summary>
        public int JISHA_GYOUSHU_KBN { get; set; }

        /// <summary>提出先CD</summary>
        public string TEISHUTUSAKI_CD { get; set; }

        /// <summary>提出書式</summary>
        public string TEISHUTU_SHOSHIKI { get; set; }

        /// <summary>報告書見出1</summary>
        public string HOUKOKUSHO_TITLE1 { get; set; }

        /// <summary>報告書見出2</summary>
        public string HOUKOKUSHO_TITLE2 { get; set; }

        /// <summary>廃棄物区分</summary>
        public int HAIKIBUTU_KBN { get; set; }

        /// <summary>対象期間FROMの年</summary>
        public string DATE_FROM_YEAR { get; set; }

        /// <summary>対象期間FROM</summary>
        public DateTime DATE_FROM { get; set; }

        /// <summary>対象期間TO</summary>
        public DateTime DATE_TO { get; set; }

        /// <summary>排出事業者名</summary>
        public string HST_GYOUSHA_KBN { get; set; }

        /// <summary>抽出条件区分</summary>
        public string CHUSHUTU_JOKEN_KBN { get; set; }

        /// <summary>抽出条件区分</summary>
        public int[] CHUSHUTU_JOKEN_KBN_ARRAY { get; set; }

        /// <summary>積替保管区分</summary>
        public int TUMIKAE_HOKAN_KBN { get; set; }

        /// <summary>住所抽出条件</summary>
        public int JUSHO_CHUSHUTU_JOKEN { get; set; }

        /// <summary>運搬再委託先欄抽出条件</summary>
        public int UNPAN_SAIITAKU_JOKEN { get; set; }

        /// <summary>自社排出分抽出条件</summary>
        public int JISHA_KBN { get; set; }

        /// <summary>他社委託先許可番号</summary>
        public int TASHA_ITAKU_KYOKA_NO { get; set; }
    }
}
