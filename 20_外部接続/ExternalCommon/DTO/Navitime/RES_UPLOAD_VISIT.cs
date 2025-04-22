using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// 訪問先マスタ一括登録のレスポンス
    /// </summary>
    [DataContract]
    public class RES_UPLOAD_VISIT : IApiDto
    {
        [DataMember(Name = "success")]
        public bool Success;

        [DataMember(Name = "processingId")]
        public string ProcessingId;

        [DataMember(Name = "errorMessage")]
        public List<string> ErrorMessage;
    }
}
