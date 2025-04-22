using System.Data.SqlTypes;

namespace KensakuKyoutsuuPopupForMultiKey.DTO
{
    /// <summary>
    /// 個別品名単価DTO
    /// </summary>
    public class DTOClass
    {
        /// <summary>
        /// 伝種区分
        /// </summary>
        public SqlInt16 DENSHU_KBN_CD { get; set; }
        /// <summary>
        /// 伝票区分
        /// </summary>
        public SqlInt16 DENPYOU_KBN_CD { get; set; }
        /// <summary>
        /// 取引先CD
        /// </summary>
        public string TORIHIKISAKI_CD { get; set; }
        /// <summary>
        /// 業者CD
        /// </summary>
        public string GYOUSHA_CD { get; set; }
        /// <summary>
        /// 現場CD
        /// </summary>
        public string GENBA_CD { get; set; }
        /// <summary>
        /// 運搬業者CD
        /// </summary>
        public string UNPAN_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 荷降業者CD
        /// </summary>
        public string NIOROSHI_GYOUSHA_CD { get; set; }
        /// <summary>
        /// 荷降現場CD
        /// </summary>
        public string NIOROSHI_GENBA_CD { get; set; }
        /// <summary>
        /// 種類検索区分
        /// </summary>
        public SqlInt16 SHURUI_KBN_CD { get; set; }
        /// <summary>
        /// 種類検索内容
        /// </summary>
        public string SHURUI_KBN_INFO { get; set; }
        /// <summary>
        /// 品名検索区分
        /// </summary>
        public SqlInt16 HINMEI_KBN_CD { get; set; }
        /// <summary>
        /// 品名検索内容
        /// </summary>
        public string HINMEI_KBN_INFO { get; set; }
        /// <summary>
        /// 伝票日付
        /// </summary>
        public SqlDateTime DENPYOU_DATE { get; set; }
    }
}