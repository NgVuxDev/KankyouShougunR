using System;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 関連コードのレスポンス
    /// </summary>
    [DataContract]
    public class CONTROL_NUMBER_WAN_SIGN
    {
        /// <summary>関連コード</summary>
        [DataMember(Name = "control_number")]
        public string Control_Number { get; set; }

        /// <summary>トランザクションID</summary>
        [DataMember(Name = "xid")]
        public string Xid { get; set; }

        /// <summary>文書名</summary>
        [DataMember(Name = "document_name")]
        public string Document_Name { get; set; }

        /// <summary>署名完了日時</summary>
        [DataMember(Name = "signing_datetime")]
        public string Signing_Date { get; set; }

        /// <summary>文書登録日時</summary>
        [DataMember(Name = "created_at")]
        public string Created_Date { get; set; }
    }
}