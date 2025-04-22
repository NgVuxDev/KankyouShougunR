using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.BusinessCommon
{
    /// <summary>
    /// 委託チェックDto
    /// </summary>
    public class ItakuCheckDTO
    {
        /// <summary>false: マニフェスト、true: マニフェスト以外</summary>
        public bool MANIFEST_FLG { get; set; }

        /// <summary>
        /// 廃棄区分: 1.直行, 2.建廃, 3.積替, 4.電子
        /// マニフェスト以外の場合：設定不要
        /// </summary>
        public int HAIKI_KBN_CD { get; set; }

        /// <summary>作業日</summary>
        public string SAGYOU_DATE { get; set; }

        /// <summary>業者</summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>現場</summary>
        public string GENBA_CD { get; set; }

        /// <summary>
        /// 報告書分類チェック使用
        /// マニフェストの場合 : 廃棄種類リスト
        /// マニフェスト以外の場合: 品名リスト
        /// </summary>
        public List<DetailDTO> LIST_HINMEI_HAIKISHURUI { get; set; }
    }

    /// <summary>
    /// 委託チェック明細Dto
    /// </summary>
    public class DetailDTO
    {
        public string CD { get; set; }
        public string NAME { get; set; }
    }
}
