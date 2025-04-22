using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 認証API - 結果
    /// </summary>
    [DataContract]
    public class RES_TOKEN
    {
        /// <summary>トークン</summary>
        [DataMember(Name = "access_token")]
        public string Access_Token { get; set; }

        /// <summary>トークン種別</summary>
        [DataMember(Name = "token_type")]
        public string Token_Type { get; set; }

        /// <summary>有効期限</summary>
        [DataMember(Name = "expires_in")]
        public int Expires_In { get; set; }

        /// <summary>エラー内容</summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}
