using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    /// <summary>
    /// ドキュメントリストのレスポンス
    /// </summary>
    [DataContract]
    public class DOCUMENT_LIST_MODEL : IApiDto
    {
        /// <summary>該当する書類の総数</summary>
        [DataMember(Name = "total")]
        public long Total { get; set; }

        /// <summary>指定されたページ番号。ページあたりの書類数は最大100件。</summary>
        [DataMember(Name = "page")]
        public long Page { get; set; }

        /// <summary>書類データ</summary>
        [DataMember(Name = "documents")]
        public List<DOCUMENT_MODEL> Documents { get; set; }
    }
}
