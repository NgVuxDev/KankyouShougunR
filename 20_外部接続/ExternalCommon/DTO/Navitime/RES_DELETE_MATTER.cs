using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime
{
    /// <summary>
    /// 案件削除のレスポンス
    /// </summary>
    [DataContract]
    public class RES_DELETE_MATTER : IApiDto
    {
        [DataMember(Name = "success")]
        public bool Success;

        [DataMember(Name = "errorMessage")]
        public List<string> ErrorMessage;
    }
}
