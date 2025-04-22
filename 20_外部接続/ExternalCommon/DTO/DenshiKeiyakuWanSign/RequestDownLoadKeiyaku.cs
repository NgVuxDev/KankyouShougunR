using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.DenshiKeiyakuWanSign
{
    /// <summary>
    /// 文書リクエスト
    /// </summary>
    [DataContract]
    public class RequestDownLoadKeiyaku : RequestKeiyakuInfo
    {
        /// <summary>文書ダウンロード形式</summary>in
        [DataMember(Name = "download_type")]
        public string Download_Type { get; set; }

        /// <summary>操作</summary>
        [DataMember(Name = "operation")]
        public string Operation { get; set; }
    }
}
