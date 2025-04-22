using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// リクエストベース
    /// </summary>
    [DataContract]
    public class RequestBase : IApiDto
    {
        /// <summary>シークレットキー</summary>
        [DataMember(Name = "secret_key")]
        public string Secret_Key { get; set; }

        /// <summary>顧客ID</summary>
        [DataMember(Name = "cus_id")]
        public string Cus_Id { get; set; }
    }
}
