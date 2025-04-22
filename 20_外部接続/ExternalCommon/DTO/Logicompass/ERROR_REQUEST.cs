using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// リクエストエラー
    /// </summary>
    [DataContract]
    public class ERROR_REQUEST
    {
        /// <summary>エラーメッセージ</summary>
        [DataMember(Name = "message")]
        public string Message;

        /// <summary>検証エラー情報</summary>
        [DataMember(Name = "errors")]
        public List<INFO_ERROR> errors;
    }
}
