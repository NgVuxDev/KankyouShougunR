using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 結果レスポンス
    /// </summary>
    [DataContract]
    public class RESPONSE_WAN : IApiDto
    {
        /// <summary>ステータス</summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>メッセージ</summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>説明</summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>レスポンス日時</summary>
        [DataMember(Name = "response_time")]
        public string Response_Time { get; set; }
    }
}
