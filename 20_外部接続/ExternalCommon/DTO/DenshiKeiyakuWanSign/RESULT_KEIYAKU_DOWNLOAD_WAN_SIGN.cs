using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書レスポンス
    /// </summary>
    [DataContract]
    public class RESULT_KEIYAKU_DOWNLOAD_WAN_SIGN : RESPONSE_WAN
    {
        /// <summary>
        /// リクエスト結果
        /// </summary>
        [DataMember(Name = "result")]
        public KEIYAKU_DOWNLOAD_WAN_SIGN Result { get; set; }
    }
}
