using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 関連コードリクエスト
    /// </summary>
    [DataContract]
    public class RequestControlNumber : RequestBase
    {
        /// <summary>アクセストークン</summary>
        [DataMember(Name = "access_token")]
        public string Access_Token { get; set; }
    }
}
