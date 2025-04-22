using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// エラー
    /// </summary>
    [DataContract]
    public class ERROR
    {
        /// <summary>エラーメッセージ</summary>
        [DataMember(Name = "Message")]
        public string Message;
    }
}
