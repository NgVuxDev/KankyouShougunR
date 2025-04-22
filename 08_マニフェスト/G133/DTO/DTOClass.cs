using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.HaikibutuTyoubo
{
    public class DTOClass
    {
        /// <summary>
        /// 出力区分
        /// </summary>
        public string SYUTURYOKU_KBN { get; set; }

        /// <summary>
        /// 廃棄物区分
        /// </summary>
        public List<SqlInt16> HAIKIBUTSU_KBN { get; set; }

        /// <summary>
        /// 出力内容
        /// </summary>
        public string SYUTURYOKU_NAYIYO { get; set; }

        /// <summary>
        /// 中間処理
        /// </summary>
        public string TYUKANSYORI { get; set; }

        /// <summary>
        /// 日付種類
        /// </summary>
        public string HIDUKESYURUI { get; set; }
        
        /// <summary> 
        /// 年月日(FROM)
        /// </summary>
        public string DATE_FROM { get; set; }

        /// <summary> 
        /// 年月日(TO)
        /// </summary>
        public string DATE_TO { get; set; }

        /// <summary> 
        /// 拠点
        /// </summary>
        public string KYOTEN_CD { get; set; }

        /// <summary>
        /// 処分受託者
        /// </summary>
        public string SBN_GYOUSHA_CD { get; set; }

        /// <summary>
        /// 処分事業場(FROM)
        /// </summary>
        public string SBN_GENBA_CD { get; set; }

        /// <summary>
        /// 処分事業場(TO)
        /// </summary>
        public string SBN_GENBA_CD_TO { get; set; }

        /// <summary>
        /// マニフェスト数量フォーマット(SQLのROUND関数で使用する小数点以下の桁数)
        /// </summary>
        public int MANIFEST_SUURYOU_FORMAT { get; set; }

    }
}
