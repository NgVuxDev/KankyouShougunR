using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.SMS
{
    /// <summary>
    /// SMS長文分割送信APIのレスポンス
    /// </summary>
    [DataContract]
    public class RES_LONG_SMS_SPLIT_SEND_API : IApiDto
    {
        /// <summary>メッセージID</summary>
        [DataMember(Name = "messageId")]
        public string MessageId { get; set; }

        /// <summary>ステータスコード</summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
