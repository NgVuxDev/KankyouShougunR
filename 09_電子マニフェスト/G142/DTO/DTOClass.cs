using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.ElectronicManifest.SousinhoryuuTouroku
{
    /// <summary>
    /// 紐付された一次マニフェスト情報の検索条件DTO
    /// </summary>
    public class TOUROKUJYOUHOU_DTOCls
    {
        /// <summary>
        /// 検索条件  :マニフェスト区分
        /// </summary>
        public String MANIFEST_KBN { get; set; }
        /// <summary>
        /// 検索条件  :引渡し日From
        /// </summary>
        public String START_HIKIWATASHI_DATE { get; set; }
        /// <summary>
        /// 検索条件  :引渡し日To
        /// </summary>
        public String END_HIKIWATASHI_DATE { get; set; }
        /// <summary>
        /// 検索条件  :マニフェスト番号From
        /// </summary>
        public String START_MANIFEST_ID { get; set; }
        /// <summary>
        /// 検索条件  :マニフェスト番号To
        /// </summary>
        public String END_MANIFEST_ID { get; set; }
        /// <summary>
        ///  検索条件  :排出事業者CD
        /// </summary>
        public String HST_SHA_EDI_MEMBER_ID { get; set; }
        /// <summary>
        /// 検索条件  :連絡番号1
        /// </summary>
        public String RENRAKU_ID { get; set; }
        /// <summary>
        /// 検索条件  :状態区分
        /// </summary>
        public String FUNCTION_ID { get; set; }

    }
    /// <summary>
    /// 紐付された一次マニフェスト情報の検索条件DTO
    /// </summary>
    public class SONZAICHECK_DTOCls
    {
        /// <summary>
        /// 検索条件  :マニフェスト区分
        /// </summary>
        public String EDI_MEMBER_ID { get; set; }

    }
}
