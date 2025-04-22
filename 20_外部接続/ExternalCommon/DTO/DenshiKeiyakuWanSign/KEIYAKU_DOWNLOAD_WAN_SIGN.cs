using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書レスポンス
    /// </summary>
    [DataContract]
    public class KEIYAKU_DOWNLOAD_WAN_SIGN
    {
        /// <summary>文書データURIスキーム</summary>
        [DataMember(Name = "documents_data")]
        public string Documents_Data { get; set; }

        /// <summary>有効期限</summary>
        [DataMember(Name = "documents_url")]
        public string Documents_Url { get; set; }
    }
}
