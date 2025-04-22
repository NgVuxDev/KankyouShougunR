using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass
{
    /// <summary>
    /// 認証API - 送信
    /// </summary>
    [DataContract]
    public class REQ_TOKEN
    {
        /// <summary>認証タイプ</summary>
        [DataMember(Name = "grant_type")]
        public string Grant_Type { get; set; }

        /// <summary>ユーザー名</summary>
        [DataMember(Name = "username")]
        public string UserName { get; set; }

        /// <summary>パスワード</summary>
        [DataMember(Name = "password")]
        public string PassWord { get; set; }
    }
}
