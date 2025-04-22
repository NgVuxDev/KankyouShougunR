using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書詳細レスポンス
    /// </summary>
    [DataContract]
    public class LIST_KEIYAKU_INFO_WAN_SIGN
    {
        /// <summary>
        /// 文書詳細情報をリストで返却します
        /// </summary>
        [DataMember(Name = "document_detail_info")]
        public List<KEIYAKU_INFO_WAN_SIGN> Document_Detail_Info { get; set; }
    }
}
