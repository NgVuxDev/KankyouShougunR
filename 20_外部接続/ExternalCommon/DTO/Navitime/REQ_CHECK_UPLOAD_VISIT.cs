using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// POSTに使用するパラメータ
    /// </summary>
    [DataContract]
    public class REQ_CHECK_UPLOAD_VISIT : IApiDto
    {
        [DataMember(Name = "processingId")]
        public string processingId;
    }
}
