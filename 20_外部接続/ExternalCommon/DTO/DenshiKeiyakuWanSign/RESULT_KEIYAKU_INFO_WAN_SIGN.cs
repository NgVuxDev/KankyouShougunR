using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書詳細レスポンス
    /// </summary>
    [DataContract]
    public class RESULT_KEIYAKU_INFO_WAN_SIGN : RESPONSE_WAN
    {
        /// <summary>
        /// リクエスト結果
        /// </summary>
        [DataMember(Name = "result")]
        public LIST_KEIYAKU_INFO_WAN_SIGN Result { get; set; }
    }
}
