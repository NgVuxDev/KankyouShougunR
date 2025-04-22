using System.Runtime.Serialization;
using System;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 契約文書
    /// </summary>
    [DataContract]
    public class KEIYAKU_DOCUMENTS_WAN : IApiDto
    {
        /// <summary>関連コード</summary>
        [DataMember(Name = "control_number")]
        public string Control_Number { get; set; }

        /// <summary>文書名</summary>
        [DataMember(Name = "document_name")]
        public string Document_Name { get; set; }
    }
}
