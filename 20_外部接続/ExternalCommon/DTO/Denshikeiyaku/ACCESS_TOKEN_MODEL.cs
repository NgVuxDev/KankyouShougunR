using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Denshikeiyaku
{
    /// <summary>
    /// アクセストークンのレスポンス
    /// </summary>
    [DataContract]
    public class ACCESS_TOKEN_MODEL : IApiDto
    {
        /// <summary>アクセストークンの値</summary>
        [DataMember(Name = "access_token")]
        public string Access_Token { get; set; }

        /// <summary>トークン種別</summary>
        [DataMember(Name = "token_type")]
        public string Token_Type { get; set; }

        /// <summary>アクセストークンが有効な残り秒数</summary>
        [DataMember(Name = "expires_in")]
        public long Expires_In { get; set; }
    }
}
