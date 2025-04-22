using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.ManifestNyuryokuIkkatsu
{
    public class DTOClass
    {
        /// <summary>
        /// 検索条件  :社員コード
        /// </summary>
        public string shainCd { get; set; }

        /// <summary>
        /// 検索条件  :システムID
        /// </summary>
        public string systemId { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物区分CD
        /// </summary>
        public string haikiKbnCd { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物種類CD
        /// </summary>
        public string haikiShuruiCd { get; set; }

        /// <summary>
        /// 検索条件  :廃棄物名称CD
        /// </summary>
        public string haikiNameCd { get; set; }

        /// <summary>
        /// 検索条件  :荷姿CD
        /// </summary>
        public string nisugataCd { get; set; }

        /// <summary>
        /// 検索条件  :数量
        /// </summary>
        public int haikiSuu { get; set; }

        /// <summary>
        /// 検索条件  :単位CD
        /// </summary>
        public int unitCd { get; set; }

        /// <summary>
        /// 検索条件  :処分方法CD
        /// </summary>
        public string shobunHouhouCd { get; set; }

        /// <summary>
        /// 検索条件  :業者CD
        /// </summary>
        public string gyoushaCd { get; set; }

        /// <summary>
        /// 検索条件  :車輌CD
        /// </summary>
        public string sharyouCd { get; set; }
    }

    //2013.11.23 naitou add 交付番号重複チェック追加 start
    /// <summary>
    /// 交付番号検索
    /// </summary>
    public class SearchKohuDtoCls
    {
        /// <summary>検索条件  :廃棄物区分CD</summary>
        public string HAIKI_KBN_CD { get; set; }

        /// <summary>検索条件  :交付番号</summary>
        public string MANIFEST_ID { get; set; }
    }
    //2013.11.23 naitou add 交付番号重複チェック追加 end
}
