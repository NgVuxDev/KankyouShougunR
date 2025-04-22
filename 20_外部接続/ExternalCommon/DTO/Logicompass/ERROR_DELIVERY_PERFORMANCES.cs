using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// エラー
    /// </summary>
    [DataContract]
    public class ERROR_DELIVERY_PERFORMANCES
    {
        /// <summary>エラーメッセージ</summary>
        [DataMember(Name = "message")]
        public string message { get; set; }
    }
}
