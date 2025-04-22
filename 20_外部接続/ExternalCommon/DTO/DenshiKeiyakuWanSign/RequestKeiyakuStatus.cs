using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書状態リクエスト
    /// </summary>
    [DataContract]
    public class RequestKeiyakuStatus : RequestControlNumber
    {
        /// <summary>トランザクションID</summary>
        [DataMember(Name = "xid")]
        public string Xid { get; set; }
    }
}
