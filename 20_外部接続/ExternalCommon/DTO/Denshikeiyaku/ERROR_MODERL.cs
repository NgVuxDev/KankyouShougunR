using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    /// <summary>
    /// エラーのレスポンス
    /// </summary>
    [DataContract]
    public class ERROR_MODERL : IApiDto
    {
        /// <summary>エラーの種類</summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }

        /// <summary>エラーメッセージ</summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
