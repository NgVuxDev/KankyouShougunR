using System;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// アクセストークンのレスポンス
    /// </summary>
    [DataContract]
    public class RESULT_ACCESS_TOKEN_WAN_SIGN : RESPONSE_WAN
    {
        [DataMember(Name = "result")]
        public ACCESS_TOKEN_WAN_SIGN Result { get; set; }
    }
}
