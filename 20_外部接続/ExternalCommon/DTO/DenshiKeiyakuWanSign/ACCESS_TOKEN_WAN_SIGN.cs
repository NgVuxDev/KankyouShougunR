using System;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// アクセストークンのレスポンス
    /// </summary>
    [DataContract]
    public class ACCESS_TOKEN_WAN_SIGN 
    {
        /// <summary>アクセストークン</summary>
        [DataMember(Name = "access_token")]
        public string Access_Token { get; set; }

        /// <summary>有効期限</summary>
        [DataMember(Name = "expiration_date")]
        public string Expiration_Date { get; set; }
    }
}
